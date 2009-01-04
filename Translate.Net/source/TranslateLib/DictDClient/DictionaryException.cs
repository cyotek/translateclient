#region License and copyright
/*
Copyright (c) 2005, Milan Negovan 
http://www.AspNetResources.com
http://aspnetresources.com/tools/dictionary.aspx
All rights reserved.

Redistribution and use in source and binary forms, with or without 
modification, are permitted provided that the following conditions 
are met:

    * Redistributions of source code must retain the above copyright 
      notice, this list of conditions and the following disclaimer.
      
    * Redistributions in binary form must reproduce the above copyright 
      notice, this list of conditions and the following disclaimer in 
      the documentation and/or other materials provided with the 
      distribution.
      
    * The name of the author may not be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED 
TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; 
OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR 
OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF 
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

// --------------------------------------------------------------------------
// Further changes made by Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com) 2008
// and available under MPL 1.1/GPL 2.0/LGPL 2.1
//
// Changes list:
// Ported to .Net 2.0 (generics)
// --------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Translate;

namespace Translate.DictD
{
    /// <summary>
    /// This exception is thrown when the client encounters an error code from the dictionary server
    /// it connects to. This class cannot be inherited.
    /// </summary>
    [Serializable]
	public sealed class DictionaryServerException : TranslationException
	{
        private int errorCode;
		Uri url;        
		string command;
		

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryServerException" /> class with no arguments.
        /// </summary>
        public DictionaryServerException () : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryServerException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DictionaryServerException (string message) : base (message)
        {
        }
        
		public DictionaryServerException(Uri uri, string message): base (message)
		{
			this.url = uri;
		}
        

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryServerException" /> class with a specified error message and 
        /// a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the <em>innerException</em>
        /// parameter is not a null reference, the current exception is raised in a <strong>catch</strong> block that handles 
        /// the inner exception.</param>
        public DictionaryServerException (string message, Exception inner) : base (message, inner)
        {
        }

        public DictionaryServerException (Uri uri, string message, Exception inner) : base (message, inner)
        {
        	this.url = uri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryServerException" /> class with a 
        /// raw error code coming from a dictionary server and a specified error message.
        /// </summary>
        /// <param name="errorCode">Raw error code. See <see cref="DictionaryServerException.ErrorCode">ErrorCode</see>.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DictionaryServerException (int errorCode, string message) : base (message)
        {
            this.errorCode = errorCode;
        }

        public DictionaryServerException (Uri uri, int errorCode, string message) : base (message)
        {
            this.errorCode = errorCode;
			this.url = uri;            
        }
        
        public DictionaryServerException (Uri uri, int errorCode, string command, string message) : base (message)
        {
            this.errorCode = errorCode;
			this.url = uri;            
			this.command = command;
        }
        
        

        /// <exclude />
        private DictionaryServerException (SerializationInfo info, StreamingContext context) : base (info, context)
        {
            this.errorCode = info.GetInt32 ("ErrorCode");
        }

        /// <exclude />
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData (SerializationInfo info, StreamingContext context)
        {
            info.AddValue ("ErrorCode", this.errorCode);
            base.GetObjectData (info, context);
        }


        /// <summary>
        /// <p>The original error code returned by the dictionary server. The RFC 2229 defines error
        /// codes in the range of 400-599:</p>
        /// <list type="table">
        /// <item><term>4yz</term><description>Transient Negative Completion reply</description></item>
        /// <item><term>5yz</term><description>Permanent Negative Completion reply</description></item>
        /// </list>
        /// </summary>
	    public int ErrorCode
	    {
	        get { return errorCode; }
	        set { errorCode = value; }
	    }
	    
		public Uri Url
		{
			get { return url; }
			set { url = value; }
		}
		
		public string Command
		{
			get { return command; }
			set { command = value; }
		}
	    
	}
}
