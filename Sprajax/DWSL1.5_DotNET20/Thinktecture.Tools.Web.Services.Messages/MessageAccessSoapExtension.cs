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
using System.IO;
using System.Web.Services.Protocols;

namespace Thinktecture.Tools.Web.Services.Extensions
{
	public class SoapMessageAccessClientExtension : SoapExtension, IDisposable
	{ 
		private Stream oldStream;
		private Stream newStream;
		private bool mustStoreSoapMessage;

		/// <summary>
		/// Gets the initializer.
		/// </summary>
		/// <param name="methodInfo">Method info.</param>
		/// <param name="attribute">Attribute.</param>
		/// <returns></returns>
		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute) 
		{
			return null;
		}

		/// <summary>
		/// Gets the initializer.
		/// </summary>
		/// <param name="t">T.</param>
		/// <returns></returns>
		public override object GetInitializer(Type t) 
		{
			//return typeof(SoapMessageAccessClientExtension);
			if (t.BaseType == typeof(Thinktecture.Tools.Web.Services.Extensions.SoapHttpClientProtocolExtended))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
    
		/// <summary>
		/// Initializes the specified initializer.
		/// </summary>
		/// <param name="initializer">Initializer.</param>
		public override void Initialize(object initializer) 
		{
			mustStoreSoapMessage = (bool)initializer;
		}
	    
		/// <summary>
		/// Processs the message.
		/// </summary>
		/// <param name="message">Message.</param>
		public override void ProcessMessage(SoapMessage message) 
		{
			switch (message.Stage) 
			{
				case SoapMessageStage.BeforeSerialize:
					break;

				case SoapMessageStage.AfterSerialize:
					StoreRequestMessage(message);
					// Pass it off as the actual stream
					//Copy(newStream, oldStream);
					// Indicate for the return that we don't wish to chain anything in
					break;

				case SoapMessageStage.BeforeDeserialize:
					StoreResponseMessage(message);
					// Pass it off as the actual stream
					break;

				case SoapMessageStage.AfterDeserialize:
					break;

				default:
					throw new ArgumentException("Invalid message stage [" + message.Stage + "]", "message");
			}
		}
	    
		/// <summary>
		/// Chains the stream.
		/// </summary>
		/// <param name="stream">Stream.</param>
		/// <returns></returns>
		public override Stream ChainStream(Stream stream) 
		{
			// Store old
			oldStream = stream;
			newStream = new MemoryStream();

			// Return new stream
			return newStream;
		}

		/// <summary>
		/// Stores the request message.
		/// </summary>
		/// <param name="message">Message.</param>
		private void StoreRequestMessage(SoapMessage message) 
		{
			// Rewind the source stream
			newStream.Position = 0;
			
			if (mustStoreSoapMessage)
			{
				try
				{
					// Store message in our slot in the SoapHttpClientProtocol-derived class
					byte[] bufEncSoap = new Byte[newStream.Length];
					newStream.Read(bufEncSoap, 0, bufEncSoap.Length);
					((SoapHttpClientProtocolExtended)(((SoapClientMessage)message).Client)).SoapRequestInternal = bufEncSoap;
				}
				catch(Exception ex)
				{
					throw new MessageStorageException("An error occured while trying to access the SOAP stream.", ex);
				}
				catch
				{
					throw new MessageStorageException("An error occured while trying to access the SOAP stream.");
				}
			}

			Copy(newStream, oldStream);
		}

		/// <summary>
		/// Stores the response message.
		/// </summary>
		/// <param name="message">Message.</param>
		private void StoreResponseMessage(SoapMessage message) 
		{
			Stream tempStream = new MemoryStream();
			Copy(oldStream, tempStream);

			if (mustStoreSoapMessage)
			{
				try
				{
					// Store message in our slot in the SoapHttpClientProtocol-derived class
					byte[] bufEncSoap = new Byte[tempStream.Length];
					tempStream.Read(bufEncSoap, 0, bufEncSoap.Length);
					((SoapHttpClientProtocolExtended)(((SoapClientMessage)message).Client)).SoapResponseInternal = bufEncSoap;
				}
				catch(Exception ex)
				{
					throw new MessageStorageException("An error occured while trying to access the SOAP stream.", ex);
				}
				catch
				{
					throw new MessageStorageException("An error occured while trying to access the SOAP stream.");
				}	
			}

			Copy(tempStream, newStream);
		}

		/// <summary>
		/// Copys the specified from.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		private static void Copy(Stream from, Stream to) 
		{
			if (from.CanSeek == true)
				from.Position = 0;
			TextReader reader = new StreamReader(from);
			TextWriter writer = new StreamWriter(to);
			writer.WriteLine(reader.ReadToEnd());
			writer.Flush();
			if (to.CanSeek == true)
				to.Position = 0;
		}

		#region IDisposable Members

		/// <summary>
		/// Disposes this instance.
		/// </summary>
		public void Dispose() 
		{
			Dispose(true);
			GC.SuppressFinalize(this); 
		}

		/// <summary>
		/// Disposes the specified disposing.
		/// </summary>
		/// <param name="disposing">Disposing.</param>
		protected virtual void Dispose(bool disposing) 
		{
			if (disposing) 
			{
				// Free other state (managed objects)
			}

			// Free your own state (unmanaged objects)
			// Set large fields to null
			if(oldStream != null)
			{
				oldStream.Close();
				oldStream = null;
			}

			if(newStream != null)
			{
				newStream.Close();
				newStream = null;
			}
		}

		/// <summary>
		/// 'Destruct' the SOAP message access client extension.
		/// </summary>
		~SoapMessageAccessClientExtension()
		{
			// Simply call Dispose(false)
			Dispose (false);
		}

		#endregion
	}
}