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
using System.Reflection;
using System.Web.Services.Protocols;

namespace Thinktecture.Tools.Web.Services.Helpers
{
	/// <summary>
	/// Summary description for PipelineConfiguration.
	/// </summary>
	internal class PipelineConfiguration
	{
		/// <summary>
		/// Injects the extension.
		/// </summary>
		/// <param name="extension">Extension.</param>
		internal static void InjectExtension(Type extension)
		{
			Assembly assBase;
			Type webServiceConfig;
			object currentProp;
			PropertyInfo propInfo;
			object[] value;
			Type myType;
			object[] objArray;
			object myObj;
			FieldInfo myField;

			try
			{
				assBase = typeof(SoapExtensionAttribute).Assembly;
				webServiceConfig =
					assBase.GetType("System.Web.Services.Configuration.WebServicesConfiguration");

				if (webServiceConfig == null)
					throw new PipelineConfigurationException("Error configuring pipeline.");

				currentProp = webServiceConfig.GetProperty("Current",
					BindingFlags.NonPublic | BindingFlags.Static |
					BindingFlags.Instance | BindingFlags.Public
					).GetValue(null, null);
				propInfo = webServiceConfig.GetProperty("SoapExtensionTypes",
					BindingFlags.NonPublic | BindingFlags.Static |
					BindingFlags.Instance | BindingFlags.Public
					);
				value = (object[])propInfo.GetValue(currentProp, null);
				myType = value.GetType().GetElementType();
				objArray = (object[])Array.CreateInstance(myType, value.Length + 1);
				Array.Copy(value, objArray, value.Length);

				myObj = Activator.CreateInstance(myType);
				myField = myType.GetField("Type",
					BindingFlags.NonPublic | BindingFlags.Static |
					BindingFlags.Instance | BindingFlags.Public
					);
				myField.SetValue(myObj, extension,
					BindingFlags.NonPublic | BindingFlags.Static |
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField,
					null, null
					);
				objArray[objArray.Length - 1] = myObj;
				propInfo.SetValue(currentProp, objArray,
					BindingFlags.NonPublic | BindingFlags.Static |
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
					null, null, null
					);
			}
			catch (Exception ex)
			{
				throw new PipelineConfigurationException("Problem occured when trying to inject SoapExtension into pipeline", ex);
			}
			catch
			{
				throw new PipelineConfigurationException("Problem occured when trying to inject SoapExtension into pipeline");
			}
		}
	}
}
