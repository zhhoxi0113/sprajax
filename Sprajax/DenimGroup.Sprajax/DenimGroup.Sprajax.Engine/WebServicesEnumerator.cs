using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Thinktecture.Tools.Web.Services.DynamicProxy;
using Thinktecture.Tools.Web.Services.Extensions;

using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Engine
{
    public class WebServiceEnumerator
    {
        private DynamicWebServiceProxy _DynamicProxy;
        private string _WSDL;
        private WebServiceCollection _Services;

        public WebServiceEnumerator(string wsdl)
        {
            _WSDL = wsdl;
            _DynamicProxy = new DynamicWebServiceProxy(WSDL);
            _Services = new WebServiceCollection(WSDL);
        }

        public WebServiceCollection Enumerate()
        {
            //  Clear the cache every time.  If we are running this on a site we should
            //  be seeing everything for the first time (hence no cache benefit), and
            //  for development the cache just causes problems with changes not being
            //  recognized.
            try
            {
                DynamicWebServiceProxy.ClearCache(WSDL);
            }
            catch
            {
                //  The file handling in the DynWSLib cache is kind of strange, so this usually isn't a
                //  big problem.  Usually  :)  It often means that the DLL has already been loaded by this
                //  process and can't be unloaded.
            }

            StringBuilder text = new StringBuilder();

            DynamicProxy.EnableMessageAccess = true;
            DynamicProxy.Wsdl = WSDL;

            Type[] types = DynamicProxy.ProxyAssembly.GetTypes();

            foreach (Type t in types)
            {
                WebService currentWebService;

                if (t.BaseType == typeof(SoapHttpClientProtocolExtended))
                {
                    text.Append("Found a WebService: ");
                    text.Append(t.Name);
                    text.Append("\n");

                    // Services.WebServices.Add(new WebService(t.Name));
                    currentWebService = new WebService(Services, t.Name);
                    Services.WebServices.Add(currentWebService);

                    MethodInfo[] mi = t.GetMethods(BindingFlags.Public |
                                    BindingFlags.Instance |
                                    BindingFlags.DeclaredOnly);

                    foreach (MethodInfo m in mi)
                    {
                        if (!(m.Name.StartsWith("Begin") || m.Name.StartsWith("End")))
                        {
                            text.Append("Found a method: ");
                            text.Append(m.Name);
                            text.Append(" with return type: ");
                            text.Append(m.ReturnType);
                            text.Append("\n");

                            Method currentMethod = new Method(currentWebService, m.Name, m.ReturnType);
                            currentWebService.Methods.Add(currentMethod);

                            ParameterInfo[] pi = m.GetParameters();
                            // paramInfo = pi;

                            foreach (ParameterInfo p in pi)
                            {
                                text.Append("Found a parameter: ");
                                text.Append(p.Name);
                                text.Append(":");
                                text.Append(p.ParameterType);
                                text.Append("\n");

                                Parameter currentParameter;
                                currentParameter = new Parameter(p.Name, p.ParameterType);
                                currentMethod.Parameters.Add(currentParameter);
                            }
                        }
                    }
                }
                else
                {
                    text.Append("Found non-standard Type: ");
                    text.Append(t.Name);
                    text.Append("\n");
                }
            }

            //  TODO - Make this have no return value and use the WebServicesCollection.ToString instead.
            System.Console.WriteLine(text.ToString());
            // return (text.ToString());
            return (_Services);
        }

        public DynamicWebServiceProxy DynamicProxy
        {
            get { return _DynamicProxy; }
        }

        public string WSDL
        {
            get { return (_WSDL); }
        }

        public WebServiceCollection Services
        {
            get { return (this._Services); }
        }
    }
}
