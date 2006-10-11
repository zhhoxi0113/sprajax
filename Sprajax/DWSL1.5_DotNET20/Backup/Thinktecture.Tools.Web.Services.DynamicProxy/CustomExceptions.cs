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
using System.Runtime.Serialization;

namespace Thinktecture.Tools.Web.Services
{
	[Serializable]
	public class InvocationException : Exception
	{
		/// <summary>
		/// Creates a new <see cref="MessageStorageException"/> instance.
		/// </summary>
		public InvocationException()
		{
		}

		/// <summary>
		/// Creates a new <see cref="MessageStorageException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		public InvocationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new <see cref="MessageStorageException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner.</param>
		public InvocationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new <see cref="MessageStorageException"/> instance.
		/// </summary>
		/// <param name="serializationInfo">Serialization info.</param>
		/// <param name="serializationContext">Serialization context.</param>
		protected InvocationException(SerializationInfo serializationInfo, StreamingContext serializationContext) : base(serializationInfo, serializationContext)
		{}
	}

	[Serializable]
	public class DynamicCompilationException : Exception
	{
		/// <summary>
		/// Creates a new <see cref="DynamicCompilationException"/> instance.
		/// </summary>
		public DynamicCompilationException()
		{
		}

		/// <summary>
		/// Creates a new <see cref="DynamicCompilationException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		public DynamicCompilationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new <see cref="DynamicCompilationException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner.</param>
		public DynamicCompilationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new <see cref="DynamicCompilationException"/> instance.
		/// </summary>
		/// <param name="serializationInfo">Serialization info.</param>
		/// <param name="serializationContext">Serialization context.</param>
		protected DynamicCompilationException(SerializationInfo serializationInfo, StreamingContext serializationContext) : base(serializationInfo, serializationContext)
		{}
	}

	[Serializable]
	public class PipelineConfigurationException : Exception
	{
		/// <summary>
		/// Creates a new <see cref="PipelineConfigurationException"/> instance.
		/// </summary>
		public PipelineConfigurationException()
		{
		}

		/// <summary>
		/// Creates a new <see cref="PipelineConfigurationException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		public PipelineConfigurationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new <see cref="PipelineConfigurationException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner.</param>
		public PipelineConfigurationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new <see cref="PipelineConfigurationException"/> instance.
		/// </summary>
		/// <param name="serializationInfo">Serialization info.</param>
		/// <param name="serializationContext">Serialization context.</param>
		protected PipelineConfigurationException(SerializationInfo serializationInfo, StreamingContext serializationContext) : base(serializationInfo, serializationContext)
		{}
	}

	[Serializable]
	public class WsdlFormatException : Exception
	{
		/// <summary>
		/// Creates a new <see cref="WsdlFormatException"/> instance.
		/// </summary>
		public WsdlFormatException()
		{
		}

		/// <summary>
		/// Creates a new <see cref="WsdlFormatException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		public WsdlFormatException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new <see cref="WsdlFormatException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner.</param>
		public WsdlFormatException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new <see cref="WsdlFormatException"/> instance.
		/// </summary>
		/// <param name="serializationInfo">Serialization info.</param>
		/// <param name="serializationContext">Serialization context.</param>
		protected WsdlFormatException(SerializationInfo serializationInfo, StreamingContext serializationContext) : base(serializationInfo, serializationContext)
		{}
	}

	[Serializable]
	public class TemporaryCacheException : Exception
	{
		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		public TemporaryCacheException()
		{
		}

		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		public TemporaryCacheException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner.</param>
		public TemporaryCacheException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		/// <param name="serializationInfo">Serialization info.</param>
		/// <param name="serializationContext">Serialization context.</param>
		protected TemporaryCacheException(SerializationInfo serializationInfo, StreamingContext serializationContext) : base(serializationInfo, serializationContext)
		{}
	}
	[Serializable]
	public class ProxyTypeInstantiationException : Exception
	{
		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		public ProxyTypeInstantiationException()
		{
		}

		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		public ProxyTypeInstantiationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="inner">Inner.</param>
		public ProxyTypeInstantiationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new <see cref="TemporaryCacheException"/> instance.
		/// </summary>
		/// <param name="serializationInfo">Serialization info.</param>
		/// <param name="serializationContext">Serialization context.</param>
		protected ProxyTypeInstantiationException(SerializationInfo serializationInfo, StreamingContext serializationContext) : base(serializationInfo, serializationContext)
		{}
	}

}
