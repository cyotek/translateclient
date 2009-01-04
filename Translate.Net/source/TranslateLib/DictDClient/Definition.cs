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
using System.Collections.Generic;

namespace Translate.DictD
{

    /// <summary>
    /// Represents a word or phrase defition obtained during a search through one ore more databases.
    /// </summary>
	public class Definition
	{
        private Database database;
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="Definition" /> class with no arguments.
        /// </summary>
        private Definition () {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Definition" /> class with a description and
        /// a <see cred="Database" /> where it was found.
        /// </summary>
        /// <param name="description">Word/phrase description.</param>
        /// <param name="database"><see cref="Database" /> where this definition was found.</param>
	    public Definition (string description, Database database)
	    {
	        this.database = database;
	        this.description = description;
	    }

        /// <summary>
        /// <see cref="Database" /> where the definition was found.
        /// </summary>
	    public Database Database
	    {
	        get { return database; }
	    }

        /// <summary>
        /// Word/phrase description.
        /// </summary>
	    public string Description
	    {
	        get { return description; }
	    }
	}
	
	public class DefinitionCollection : List<Definition>
	{
	
	}
	
}
