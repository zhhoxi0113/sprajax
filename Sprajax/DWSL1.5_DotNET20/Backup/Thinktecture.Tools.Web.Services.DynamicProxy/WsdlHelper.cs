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

using System.IO;
using System.Net;
using System.Text;

namespace Thinktecture.Tools.Web.Services.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	internal class WsdlHelper
	{
		/// <summary>
		/// Gets the WSDL.
		/// </summary>
		/// <param name="source">Source.</param>
		internal static string GetWsdl(string source) 
		{
			// this could be a valid WSDL representation
			if(source.StartsWith("<?xml version") == true)
			{
				return source;
			}
				// this is a URL to the WSDL
			else if(source.StartsWith("http://") == true)
			{
				return GetWsdlFromUri(source);
			}
							
			// try to get from local file system
			return GetWsdlFromFile(source);
		}
		
		/// <summary>
		/// Gets the WSDL from URI.
		/// </summary>
		/// <param name="uri">URI.</param>
		/// <returns></returns>
		internal static string GetWsdlFromUri(string uri)
		{
			WebRequest req = WebRequest.Create(uri);
			WebResponse result = req.GetResponse();
					
			Stream ReceiveStream = result.GetResponseStream();
			Encoding encode = Encoding.GetEncoding("utf-8");
			StreamReader sr = new StreamReader(ReceiveStream, encode);
					
			string wsdlSourceValue = sr.ReadToEnd();
			sr.Close();
					
			return wsdlSourceValue;
		}
		
		/// <summary>
		/// Gets the WSDL from file.
		/// </summary>
		/// <param name="fileFullPathName">Name of the file full path.</param>
		/// <returns></returns>
		internal static string GetWsdlFromFile(string fileFullPathName)
		{
			FileInfo fi = new FileInfo(fileFullPathName);
					
			if(fi.Extension == ".wsdl")
			{
				FileStream fs = new FileStream(fileFullPathName, FileMode.Open, FileAccess.Read);
				StreamReader sr = new StreamReader(fs);
							
				char[] buffer = new char[(int)fs.Length];
				sr.ReadBlock(buffer, 0, (int)fs.Length);
				sr.Close();
							
				return new string(buffer);
			}
				
			throw new WsdlFormatException("This is not a WSDL file");
		}
	}
}
