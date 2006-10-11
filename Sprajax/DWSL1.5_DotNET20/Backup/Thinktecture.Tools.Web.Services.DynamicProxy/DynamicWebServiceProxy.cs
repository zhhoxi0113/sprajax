#region Copyright (c) 2003-2004, thinktecture (http://www.thinktecture.com). All rights reserved.
/*
// Copyright (c) 2003-2004, thinktecture (http://www.thinktecture.com).
// All rights reserved.

Copyright
---------
The software and all other content of the distributed package, if not otherwise stated,
is copyright 2003-2004 thinktecture (http://www.thinktecture.com/).
All rights reserved.

Terms of Use
------------
Permission is hereby granted to use this software, for both commercial and non-commercial purposes,
free of charge. Permission is hereby granted to copy and distribute the software for non-commercial
purposes. A commercial distribution is NOT allowed without prior written permission of the author(s).
Further, redistribution and use in source and binary forms, with or without modification, are permitted
only provided that the following conditions are met:

(1) Redistributions of source code must retain the above copyright notice, this list of conditions and
the following warranty disclaimer. 
(2) Redistributions in binary form must reproduce the above copyright notice, this list of conditions
and the following warranty disclaimer in the documentation and/or other materials provided with the distribution. 
(3) Neither the name of thinktecture nor the names of its author(s) may be used to endorse or promote
products derived from this software without specific prior written permission.

Warranty
--------
This software is supplied "AS IS". The author(s) disclaim all warranties, expressed or implied, including,
without limitation, the warranties of merchantability and of fitness for any purpose. The author(s) assume
no liability for direct, indirect, incidental, special, exemplary, or consequential damages, which may
result from the use of this software, even if advised of the possibility of such damage.

Submissions
-----------
The author(s) encourage the submission of comments and suggestions concerning this software.
All suggestions will be given serious technical consideration. By submitting material to the author(s),
you are granting the right to make any use of the material deemed appropriate, i.e. any communication
or material that you transmit to the author(s) by electronic mail or otherwise, including any data,
questions, comments, suggestions or the like, is, and will be treated as, non-confidential and
nonproprietary information. The author(s) may use such communication or material for any purpose whatsoever
including, but not limited to, reproduction, disclosure, transmission, publication, broadcast and further
posting. Further, the author(s) are free to use any ideas, concepts, know-how or techniques contained in any
communication or material you send for any purpose whatsoever, including, but not limited to, developing,
manufacturing and marketing products.
*/
#endregion

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.CSharp;
using Thinktecture.Tools.Web.Services.Extensions;
using Thinktecture.Tools.Web.Services.Helpers;

namespace Thinktecture.Tools.Web.Services.DynamicProxy
{
	public enum Protocol
	{
		HttpGet,
		HttpPost,
		HttpSoap
	}

	public class DynamicWebServiceProxy
	{
		private Assembly ass;
		private object proxyInstance;
		private string wsdl;
		private string wsdlSource;
		private string typeName;
		private string methodName;
		private string protocolName = "Soap";
		private ArrayList methodParams = new ArrayList();
		private string proxySource;
		private ServiceDescriptionImporter sdi;
		private XmlSchemas schemas;
		private bool enableMessageAccess;
		private static bool pipelineProperlyConfigured;
		private ArrayList outParams = new ArrayList();
		
		/// <summary>
		/// Creates a new <see cref="DynamicWebServiceProxy"/> instance.
		/// </summary>
		public DynamicWebServiceProxy()
		{
		}

		/// <summary>
		/// Creates a new <see cref="DynamicWebServiceProxy"/> instance.
		/// </summary>
		/// <param name="wsdlLocation">WSDL location.</param>
		public DynamicWebServiceProxy(string wsdlLocation)
		{
			wsdl = wsdlLocation;
			BuildProxy();
		}
		
		/// <summary>
		/// Creates a new <see cref="DynamicWebServiceProxy"/> instance.
		/// </summary>
		/// <param name="wsdlLocation">WSDL location.</param>
		/// <param name="inTypeName">Name of the in type.</param>
		public DynamicWebServiceProxy(string wsdlLocation, string inTypeName)
		{
			wsdl = wsdlLocation;
			typeName = inTypeName;
			BuildProxy();
		}

		/// <summary>
		/// Creates a new <see cref="DynamicWebServiceProxy"/> instance.
		/// </summary>
		/// <param name="wsdlLocation">WSDL location.</param>
		/// <param name="inTypeName">Name of the in type.</param>
		/// <param name="inMethodName">Name of the in method.</param>
		public DynamicWebServiceProxy(string wsdlLocation, string inTypeName, string inMethodName)
		{
			wsdl = wsdlLocation;
			typeName = inTypeName;
			methodName = inMethodName;
			BuildProxy();
		}

		/// <summary>
		/// Creates a new <see cref="DynamicWebServiceProxy"/> instance.
		/// </summary>
		/// <param name="wsdlLocation">WSDL location.</param>
		/// <param name="typeName">Name of the type.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="methodParameters">Method parameters.</param>
		public DynamicWebServiceProxy(string wsdlLocation, string typeName, string methodName, params object[] methodParameters)
		{
			wsdl = wsdlLocation;
			typeName = typeName;
			methodName = methodName;
			methodParams = new ArrayList(methodParameters);
			BuildProxy();
		}

	
		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		public void AddParameter(object parameter)
		{
			methodParams.Add(parameter);
		}

		/// <summary>
		/// Invokes the call.
		/// </summary>
		/// <returns></returns>
		public object InvokeCall()
		{
			try
			{
				MethodInfo mi = proxyInstance.GetType().GetMethod(methodName);
				object result = mi.Invoke(proxyInstance, (object[])methodParams.ToArray(typeof(object)));
			
				int i = 0;
				foreach(ParameterInfo pi in mi.GetParameters())
				{
					if(pi.IsOut) outParams.Add(methodParams[i]);
					i++;
				}
				
				// clean up the original params array
				methodParams.Clear();

				return result;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw new MessageStorageException(String.Format(CultureInfo.CurrentCulture, "An error occured while calling service."), ex);
			}
			catch
			{
				throw new MessageStorageException(String.Format(CultureInfo.CurrentCulture, "An error occured while calling service."));
			}
		}

		/// <summary>
		/// Begins the invoke call.
		/// </summary>
		/// <param name="callback">Callback.</param>
		/// <param name="asyncState">State of the async.</param>
		/// <returns></returns>
		public IAsyncResult BeginInvokeCall(AsyncCallback callback, object asyncState)
		{
			try
			{
				ArrayList parameters = new ArrayList(methodParams);
				parameters.Add(callback);
				parameters.Add(asyncState);

				MethodInfo mi = proxyInstance.GetType().GetMethod(CodeConstants.BEGIN + methodName);
				IAsyncResult result = (IAsyncResult)mi.Invoke(proxyInstance, (object[])parameters.ToArray(typeof(object)));
			
				return result;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw new MessageStorageException("Problem asynchronously calling the Web service.", ex);
			}
			catch
			{
				throw new MessageStorageException("Problem asynchronously calling the Web service.");
			}
		}

		/// <summary>
		/// Ends the invoke call.
		/// </summary>
		/// <param name="asyncResult">Async result.</param>
		/// <returns></returns>
		public object EndInvokeCall(IAsyncResult asyncResult)
		{
			try
			{
				MethodInfo mi = proxyInstance.GetType().GetMethod(CodeConstants.END + methodName);
				object result = mi.Invoke(proxyInstance, new object[]{asyncResult});
			
				return result;
			}
			catch(MessageStorageException e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value></value>
		public object Instance
		{
			get
			{
				return proxyInstance;
			}
		}


		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value></value>
		public Uri Url
		{
			get
			{
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Url");
				object result = propInfo.GetValue(proxyInstance, null);
				
				return new Uri((string)result);
			}
			set
			{
				string urlValue = value.AbsoluteUri;
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Url");
				propInfo.SetValue(proxyInstance, urlValue,
					BindingFlags.NonPublic | BindingFlags.Static |
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField,
					null, null, null
					);
			}
		}

		/// <summary>
		/// Gets or sets the WSDL.
		/// </summary>
		/// <value></value>
		public string Wsdl
		{
			// TODO: move the init process to an explicit method Init() ...
			get
			{
				return wsdl;
			}
			set
			{
				wsdl = value;
				ResetInternalState();
				BuildProxy();
			}
		}

		/// <summary>
		/// Gets or sets the name of the type.
		/// </summary>
		/// <value></value>
		public string TypeName
		{
			get
			{
				return typeName;
			}
			set
			{
				typeName = value;
				// trigger new proxy assembly creation
				proxyInstance = CreateInstance(typeName);
			}
		}

		/// <summary>
		/// Gets or sets the name of the method.
		/// </summary>
		/// <value></value>
		public string MethodName
		{
			get
			{
				return methodName;
			}
			set
			{
				methodName = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the protocol.
		/// </summary>
		/// <value></value>
		public Protocol ProtocolName
		{
			get
			{
				switch(protocolName)
				{
					case "HttpGet":
						return Protocol.HttpGet;
					case "HttpPost":
						return Protocol.HttpPost;
					case "Soap":
						return Protocol.HttpSoap;
					default:
						return Protocol.HttpSoap;
				}
			}
			set
			{
				switch(value)
				{
					case Protocol.HttpGet:
						protocolName = "HttpGet";
						break;
					case Protocol.HttpPost:
						protocolName = "HttpPost";
						break;
					case Protocol.HttpSoap:
						protocolName = "Soap";
						break;
				}
			}
		}

		/// <summary>
		/// Clears the cache.
		/// </summary>
		/// <param name="wsdlLocation">WSDL location.</param>
		public static void ClearCache(string wsdlLocation)
		{
			CompiledAssemblyCache.ClearCache(wsdlLocation);
		}

		/// <summary>
		/// Builds the assembly from WSDL.
		/// </summary>
		/// <param name="strWsdl">STR WSDL.</param>
		/// <returns></returns>
		private Assembly BuildAssemblyFromWsdl(string strWsdl)
		{
			// Use an XmlTextReader to get the Web Service description
			StringReader  wsdlStringReader = new StringReader(strWsdl);
			XmlTextReader tr = new XmlTextReader(wsdlStringReader);
			ServiceDescription.Read(tr);
			tr.Close();

			// WSDL service description importer 
			CodeNamespace cns = new CodeNamespace(CodeConstants.CODENAMESPACE);
			sdi = new ServiceDescriptionImporter();
			//sdi.AddServiceDescription(sd, null, null);
			
			// check for optional imports in the root WSDL
			CheckForImports(wsdl);

			sdi.ProtocolName = protocolName;
			sdi.Import(cns, null);

			// change the base class
			// get all available Service classes - not only the default one
			ArrayList newCtr = new ArrayList();
			 
			foreach (CodeTypeDeclaration ctDecl in cns.Types)
			{
				if(ctDecl.BaseTypes.Count > 0)
				{
					if(ctDecl.BaseTypes[0].BaseType == CodeConstants.DEFAULTBASETYPE)
					{
						newCtr.Add(ctDecl);
					}
				}
			}
			
			foreach (CodeTypeDeclaration ctDecl in newCtr)
			{
				cns.Types.Remove(ctDecl);
				ctDecl.BaseTypes[0] = new CodeTypeReference(CodeConstants.CUSTOMBASETYPE);
				cns.Types.Add(ctDecl);
			}

			// source code generation
			CSharpCodeProvider cscp = new CSharpCodeProvider();
			ICodeGenerator icg = cscp.CreateGenerator();
			StringBuilder srcStringBuilder = new StringBuilder();
			StringWriter sw = new StringWriter(srcStringBuilder, CultureInfo.CurrentCulture);

			if (schemas != null)
			{
				foreach (XmlSchema xsd in schemas)
				{
					if (XmlSchemas.IsDataSet(xsd))
					{
						MemoryStream mem = new MemoryStream();
						mem.Position = 0;
						xsd.Write(mem);
						mem.Position = 0;
						DataSet dataSet1 = new DataSet();
						dataSet1.Locale = CultureInfo.InvariantCulture;
						dataSet1.ReadXmlSchema(mem);
						TypedDataSetGenerator.Generate(dataSet1, cns, icg);
					}
				}
			}

			icg.GenerateCodeFromNamespace(cns, sw, null);
			proxySource = srcStringBuilder.ToString();
			sw.Close();

			// assembly compilation
			string location = "";
			
			if(HttpContext.Current != null)
			{
				location = HttpContext.Current.Server.MapPath(".");
				location += @"\bin\";
			}

			CompilerParameters cp = new CompilerParameters();
			cp.ReferencedAssemblies.Add("System.dll");
			cp.ReferencedAssemblies.Add("System.Xml.dll");
			cp.ReferencedAssemblies.Add("System.Web.Services.dll");
			cp.ReferencedAssemblies.Add("System.Data.dll");
			cp.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
			cp.ReferencedAssemblies.Add(location + "Thinktecture.Tools.Web.Services.Extensions.Messages.dll");

			cp.GenerateExecutable = false;
			cp.GenerateInMemory = false;
			cp.IncludeDebugInformation = false; 
			cp.TempFiles = new TempFileCollection(CompiledAssemblyCache.GetLibTempPath());

			ICodeCompiler icc = cscp.CreateCompiler();
			CompilerResults cr = icc.CompileAssemblyFromSource(cp, proxySource);
			
			if(cr.Errors.Count > 0)
				throw new DynamicCompilationException(string.Format(CultureInfo.CurrentCulture, @"Building dynamic assembly failed: {0} errors", cr.Errors.Count));

			Assembly compiledAssembly = cr.CompiledAssembly;
			
			//rename temporary assembly in order to cache it for later use
			CompiledAssemblyCache.RenameTempAssembly(cr.PathToAssembly, wsdl);
			
			return compiledAssembly;
		}
		
		/// <summary>
		/// Creates the instance.
		/// </summary>
		/// <param name="objTypeName">Name of the obj type.</param>
		/// <returns></returns>
		private object CreateInstance(string objTypeName) 
		{
			try
			{
				foreach (Type ty in ProxyAssembly.GetTypes())
				{
					if(ty.BaseType == typeof(SoapHttpClientProtocolExtended))
					{
						if(objTypeName == null || objTypeName.Length == 0 || ty.Name == objTypeName)
						{
							objTypeName = ty.Name;
							break;
						}
					}
				}
		
				Type t = ass.GetType(CodeConstants.CODENAMESPACE + "." + objTypeName);
				
				return Activator.CreateInstance(t);
			}
			catch(Exception ex)
			{
				throw new ProxyTypeInstantiationException("An error occured while instantiating the proxy type.", ex);
			}
			catch
			{
				throw new ProxyTypeInstantiationException("An error occured while instantiating the proxy type.");
			}
		}

		/// <summary>
		/// Resets the state of the internal.
		/// </summary>
		private void ResetInternalState()
		{
			typeName = "";
			methodName = "";
			protocolName = "Soap";
			methodParams.Clear();
			sdi = null;
		}

		/// <summary>
		/// Builds the proxy.
		/// </summary>
		private void BuildProxy()
		{
			if(enableMessageAccess)
			{
				PipelineConfiguration.InjectExtension(typeof(SoapMessageAccessClientExtension));
				pipelineProperlyConfigured = true;
			}
	
			//check cache first
			Assembly cachedAssembly = CompiledAssemblyCache.CheckCacheForAssembly(wsdl);
			if (cachedAssembly == null)
			{
				wsdlSource = WsdlHelper.GetWsdl(wsdl);
				ass = BuildAssemblyFromWsdl(wsdlSource);
			}
			else
			{
				ass = cachedAssembly;
			}
						 
			proxyInstance = CreateInstance(typeName);
		}
		
		/// <summary>
		/// Checks the for imports.
		/// </summary>
		/// <param name="baseWSDLUrl">Base WSDL URL.</param>
		private void CheckForImports(string baseWSDLUrl)
		{
			DiscoveryClientProtocol dcp = new DiscoveryClientProtocol();
			dcp.DiscoverAny(baseWSDLUrl);
			dcp.ResolveAll();

			foreach (object osd in dcp.Documents.Values)
			{
				if (osd is ServiceDescription) sdi.AddServiceDescription((ServiceDescription)osd, null, null);
				if (osd is XmlSchema) 
				{
					// store in global schemas variable
					if (schemas == null) schemas = new XmlSchemas();
					schemas.Add((XmlSchema)osd);

					sdi.Schemas.Add((XmlSchema)osd);
				}
			}
		}

		/// <summary>
		/// Gets the SOAP request.
		/// </summary>
		/// <value></value>
		public string SoapRequest
		{
			get
			{
				if(enableMessageAccess && pipelineProperlyConfigured)
				{
					PropertyInfo propInfo = proxyInstance.GetType().GetProperty("SoapRequestString");
					object result = propInfo.GetValue(proxyInstance, null);
				
					return (string)result;
				}
				else
					return "SOAP message access feature not enabled.";
			}
		}

		/// <summary>
		/// Gets the SOAP response.
		/// </summary>
		/// <value></value>
		public string SoapResponse
		{
			get
			{
				if(enableMessageAccess && pipelineProperlyConfigured)
				{
					PropertyInfo propInfo = proxyInstance.GetType().GetProperty("SoapResponseString");
					object result = propInfo.GetValue(proxyInstance, null);
					
					return (string)result;
				}
				else
					return "SOAP message access feature not enabled.";
			}
		}

		/// <summary>
		/// Gets or sets the credentials.
		/// </summary>
		/// <value></value>
		public ICredentials Credentials
		{
			set
			{
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Credentials");
				propInfo.SetValue(proxyInstance, value, null);
			}

			get
			{
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Credentials");
				ICredentials result = (ICredentials)propInfo.GetValue(proxyInstance, null);

				return result;
			}
		}

		/// <summary>
		/// Gets or sets the timeout.
		/// </summary>
		/// <value></value>
		public int Timeout
		{
			set
			{
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Timeout");
				propInfo.SetValue(proxyInstance, value, null);
			}

			get
			{
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Timeout");
				int result = (int)propInfo.GetValue(proxyInstance, null);

				return result;
			}
		}

		/// <summary>
		/// Gets or sets the proxy.
		/// </summary>
		/// <value></value>
		public IWebProxy Proxy
		{
			set
			{
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Proxy");
				propInfo.SetValue(proxyInstance, value, null);
			}

			get
			{
				PropertyInfo propInfo = proxyInstance.GetType().GetProperty("Proxy");
				IWebProxy result = (IWebProxy)propInfo.GetValue(proxyInstance, null);

				return result;
			}
		}

		/// <summary>
		/// Gets the proxy assembly.
		/// </summary>
		/// <value></value>
		public Assembly ProxyAssembly
		{
			get
			{
				return ass;
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether [enable message access].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [enable message access]; otherwise, <c>false</c>.
		/// </value>
		public bool EnableMessageAccess
		{
			get
			{
				return enableMessageAccess && pipelineProperlyConfigured;
			}
			
			set
			{
				PipelineConfiguration.InjectExtension(typeof(SoapMessageAccessClientExtension));
				enableMessageAccess = value;
			}
		}

		/// <summary>
		/// Gets or sets the dynamic and cached assembly's temporary path.
		/// </summary>
		/// <value></value>
		public string AssemblyTemporaryPath
		{
			get
			{
				return CompiledAssemblyCache.GetLibTempPath();
			}

			set
			{
				CompiledAssemblyCache.SetLibTempPath(value);
			}
		}

		/// <summary>
		/// Gets the out params.
		/// </summary>
		/// <value></value>
		public ArrayList OutParameters
		{
			get { return outParams; }
		}
	}
}
