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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Thinktecture.Tools.Web.Services.DynamicProxy
{
	/// <summary>
	/// Summary description for CompiledAssemblyCache.
	/// </summary>
	internal class CompiledAssemblyCache
	{
		private static string _libPath = "";

		/// <summary>
		/// Checks the cache.
		/// </summary>
		/// <returns></returns>
		internal static Assembly CheckCacheForAssembly(string wsdl)
		{
            string path = Path.GetTempPath() + GetMd5Sum(wsdl) + CodeConstants.TEMPDLLEXTENSION;

			if(File.Exists(path))
			{
				Assembly ass = Assembly.LoadFrom(path);

				return ass;
			}
			
			return null;
		}

		/// <summary>
		/// Clears the cache.
		/// </summary>
		/// <param name="wsdlLocation">WSDL location.</param>
		internal static void ClearCache(string wsdlLocation)
		{
			// clear the cached assembly file for this WSDL
			try
			{
				string path = GetLibTempPath();

				//string path = Path.GetTempPath();
				string newFilename = path + GetMd5Sum(wsdlLocation) + CodeConstants.TEMPDLLEXTENSION;

				File.Delete(newFilename);
			}
			catch(Exception ex)
			{
				throw new TemporaryCacheException("Problem occured when trying to clear temporary local assembly cache for WSDL: " + wsdlLocation + ".", ex);
			}
			catch
			{
                throw new TemporaryCacheException("Problem occured when trying to clear temporary local assembly cache for WSDL: " + wsdlLocation + ".");
			}
		}

        /// <summary>
        /// Clears all cached DLLs.
        /// </summary>
        internal static void ClearAllCached()
        {
            string path = GetLibTempPath();
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] dllFiles = di.GetFiles("*" + CodeConstants.TEMPDLLEXTENSION);
            foreach (FileInfo fi in dllFiles)
            {
                try
                {
                    fi.Delete();
                }
                catch (Exception ex)
                {
                    throw new TemporaryCacheException("Problem occurred when trying to clear temporary local assembly cache.", ex);
                }
            }

        }

		/// <summary>
		/// Renames the temp assembly.
		/// </summary>
		/// <param name="pathToAssembly">Path to assembly.</param>
		internal static void RenameTempAssembly(string pathToAssembly, string wsdl)
		{			
			string path = Path.GetDirectoryName(pathToAssembly);
			string newFilename = path + @"\" + CompiledAssemblyCache.GetMd5Sum(wsdl) + "_Thinktecture_tmp.dll";
			
			File.Copy(pathToAssembly, newFilename);
		}

		/// <summary>
		/// Gets the MD5 sum.
		/// </summary>
		/// <param name="stringToHash">String to hash.</param>
		/// <returns></returns>
		internal static string GetMd5Sum(string stringToHash)
		{
			// First we need to convert the string into bytes, which
			// means using a text encoder
			Encoder enc = Encoding.Unicode.GetEncoder();

			// Create a buffer large enough to hold the string
			byte[] unicodeText = new byte[stringToHash.Length * 2];
			enc.GetBytes(stringToHash.ToCharArray(), 0, stringToHash.Length, unicodeText, 0, true);

			// Now that we have a byte array we can ask the CSP to hash it
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(unicodeText);

			// Build the final string by converting each byte
			// into hex and appending it to a StringBuilder
			StringBuilder sb = new StringBuilder();
			for (int i=0;i<result.Length;i++)
			{
				sb.Append(result[i].ToString("X2", CultureInfo.CurrentCulture));
			}

			return sb.ToString();
		}

		/// <summary>
		/// Gets the app temp path.
		/// </summary>
		/// <returns></returns>
		internal static string GetLibTempPath()
		{
			string tempPath = _libPath;

			if(tempPath.Length == 0)
				tempPath = System.Configuration.ConfigurationSettings.AppSettings[CodeConstants.LIBTEMPDIR]; 
			if (tempPath == null || tempPath.Length == 0)
				tempPath = Path.GetTempPath();

			return tempPath;    
		}

		/// <summary>
		/// Sets the lib temp path.
		/// </summary>
		/// <param name="path">Path.</param>
		internal static void SetLibTempPath(string path)
		{
			_libPath = path;
		}
	}
}
