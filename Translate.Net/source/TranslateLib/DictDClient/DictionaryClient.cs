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
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Translate.DictD
{
    /// <summary>
    /// <p>The Dictionary Server Protocol (DICT) is a TCP transaction based query/response 
    /// protocol that allows a client to access dictionary definitions from a set of 
    /// natural language dictionary databases.</p>
    /// 
    /// <p>The DICT protocol is designed to provide access to multiple databases. Word definitions 
    /// can be requested, the word index can be searched (using an easily extended set of 
    /// algorithms), information about the server can be provided (e.g., which index search 
    /// strategies are supported, or which databases are available), and information about 
    /// a database can be provided (e.g., copyright, citation, or distribution information).</p>
    /// 
    /// <p>This is a client which adheres to RFC 2229 and facilitates communication with a DICT-compliant
    /// server.</p>
    /// </summary>
    public class DictionaryClient
    {
        #region Class members

        private TcpClient       client;
        private StreamWriter    outStream;
        private StreamReader    inStream;
        private int             timeout = 5000;

        // This is a localization helper

        private DatabaseCollection cachedDatabases = new DatabaseCollection();
        private StrategyCollection cachedStrategies = new StrategyCollection();

        private static Regex    reStatus = new Regex (@"(\d{3})\s(.*)", RegexOptions.Compiled);
        private static Regex    reDefinitions = new Regex (@"^\b151\b", RegexOptions.Compiled | RegexOptions.Multiline);
        private static Regex    reDatabase = new Regex (@"(\w+)\s""([^""]+)""$", RegexOptions.Compiled);
        private static Regex    reDatabaseAndDescription = new Regex ("(.+)\\s\"([^\"]+)\"", RegexOptions.Compiled);
        private static Regex    reStrategyAndDescription = new Regex ("(.+)\\s\"([^\"]+)\"", RegexOptions.Compiled);
        private static Regex    reDatabaseAndWordMatch = new Regex ("(.+)\\s\"(.+)\"", RegexOptions.Compiled);
        #endregion

        #region Properties

        /// <summary>
        /// The DICT protocol assumes a reliable data stream such as provided by TCP.  When TCP is used, 
        /// a DICT server listens on port 2628. If you know that the server listens on another port you need
        /// to change it via this property.
        /// </summary>
        public int Port
        {
            get 
            {
            	int port = url.Port; 
            	if(port == -1)
            		port = 2628;
            	return port; 
            }
            set 
            {
            	if(value == 2628)
            		url = new Uri("dict://" + Host);
            	else	
            		url = new Uri("dict://" + Host + ":" + value.ToString());
            }
        }

        /// <summary>
        /// By default the client connects to the dictionary server at <a href="http://www.dict.org">http://www.dict.org</a>. 
        /// Change the destination via this property if you're pointing an another dictionary server.
        /// </summary>
        public string Host
        {
            get { return url.Host; }
            set 
            { 
            	if(Port == 2628)
            		url = new Uri("dict://" + value);
            	else	
            		url = new Uri("dict://" + value + ":" + Port.ToString());
            }
        }

		Uri url = new Uri("dict://dict.org");  
		
		public Uri Url
		{
			get { return url; }
			set { url = value; }
		}
		
		
        /// <summary>
        /// To avoid blocking socket calls set a reasonable timeout, which defaults to 5000 milliseconds.
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }
        
        public bool Connected
        {
        	get
        	{	
        		bool result =  client != null && client.Connected;
        		//process idle disconnect 
        		if(result && lastActiveTime.AddMilliseconds(idleTimeoutBeforeDisconnect) < DateTime.Now)
        		{
        			Disconnect();
        			result = false;
        		}
        		return result;
        	}
        	set
        	{
        		if(!value)
        			Disconnect();
        		else if(!Connected && value)	
        			Connect();
        	}
        }
        
        bool autoConnect = true;
        
        /// <summary>
        /// Enable autoconnecting\disconnecting to server on each query
        /// </summary>
		public bool AutoConnect
		{
			get { return autoConnect; }
			set { autoConnect = value; }
		}
        
        /// <summary>
        /// Count ticks of geting "welcome" from server
        /// </summary>
        long connectTicks = long.MaxValue;
		public long ConnectTicks
		{
			get { return connectTicks; }
		}
		
		int idleTimeoutBeforeDisconnect = 600000; //10 min
		public int IdleTimeoutBeforeDisconnect
		{
			get { return idleTimeoutBeforeDisconnect; }
			set { idleTimeoutBeforeDisconnect = value; }
		}
		DateTime lastActiveTime = DateTime.Now;
		
		string clientName;

		/// <summary
		/// Provide information to server about itself for possible logging and statistical purposes
		/// </summary>
		public string ClientName
		{
			get { return clientName; }
			set { clientName = value; }
		}
		

        #endregion

        #region Auxhiliary commands which provide client communication with the designated server

        /// <summary>
        /// Connect to a dictionary server and initialize read and write streams.
        /// </summary>
        /// <remarks>
        /// <p>
        /// Since socket calls are blocking, set a reasonable timeout (defaults to 3 seconds). You
        /// may change this timeout via the <see cref="Timeout" /> property.
        /// </p>
        /// 
        /// <p>
        /// By default the client connects to the database at <a href="http://www.dict.org" />, 
        /// port 2628. You may choose another server by setting the <see cref="DictionaryServer" /> 
        /// property. If the remote dictionary database uses a non-standard port, please set it
        /// via the <see cref="Port">Port</see> property.
        /// </p>
        /// </remarks>
        private void Connect ()
        {
        	if(Connected)
        		return;
            try
            {
            	Disconnect ();
            	connectTicks = long.MaxValue;
            	DateTime start = DateTime.Now;
            	
                client          = new TcpClient (Host, Port);
                inStream        = new StreamReader (client.GetStream ());
                outStream       = new StreamWriter (client.GetStream ());

                /*
                 * NOTE: The reason I'm not passing Encoding.UTF8 in the stream constructors is because
                 * they use this encodig by default:
                 * 
                 * public StreamReader (Stream stream) : this(stream, Encoding.UTF8, true, 0x400) {}
                 */
                client.ReceiveTimeout = Timeout;
                client.SendTimeout = Timeout;

                // Read the initial response which contains the server version.
                string initialResponse = inStream.ReadLine ();
                Match m = reStatus.Match (initialResponse);
                int statusCode = Convert.ToInt32(m.Groups[1].Value);

                if (statusCode >= 400 && statusCode <= 599)
                {
                	Disconnect();
                    string errorMessage = null;

                    switch (statusCode)
                    {
                        case 420: // Server temporarily unavailable
                        case 421: // Server shutting down at operator request
                            errorMessage = "The dictionary server is not available at this time."; break;
                    }

                    throw new DictionaryServerException (url, statusCode, "", errorMessage); 
                }
                

				//inform server about client name      
				//All clients SHOULD send this command after connecting to the server.
				if(!string.IsNullOrEmpty(clientName))
                	ExecuteCommand (string.Concat("CLIENT ", clientName), false, false);
                
                connectTicks = DateTime.Now.Ticks - start.Ticks;
                lastActiveTime = DateTime.Now;
            }
            catch (SocketException se)
            {
                Disconnect();
                throw new DictionaryServerException(url, "Unable to connect to the dictionary server.", se);
            }
        }

        /// <summary>
        /// Disconnect from the dictionary server and dispose of the connection.
        /// </summary>
        private void Disconnect ()
        {
        	if(client != null && client.Connected)
        	{
				try
				{
					ExecuteCommand ("QUIT", false, false);
				}
				catch
				{
				}
			}
        
            if (inStream != null)
            {
                inStream.Close ();
                inStream = null;
            }    

            if (outStream != null)
            {
            	outStream.Close ();
            	outStream = null;
            }    

            if (client != null)
            {
                client.Close ();
                client = null;
            }    
        }


        /// <summary>
        /// Send a command to the dictionary server and read and parse the response.
        /// </summary>
        /// <param name="command">Dictionary server command (see RFC 2229).</param>
        /// <returns>Dictionary server response stripped of the leading and trailing status lines.</returns>
        private string ExecuteCommand (string command)
        {
        	return ExecuteCommand(command, this.autoConnect, true);
        }
		
		bool IsDataArrived()
		{
			try  //monobug - Peek raise IOException 
			{
				return inStream.Peek () != -1;
			}
			catch
			{
				return false;
			}
		}

        /// <summary>
        /// Send a command to the dictionary server and read and parse the response.
        /// </summary>
        /// <param name="command">Dictionary server command (see RFC 2229).</param>
        /// <returns>Dictionary server response stripped of the leading and trailing status lines.</returns>
        private string ExecuteCommand (string command, bool autoConnect, bool readResponse)
        {
            StringBuilder   response = new StringBuilder();
            string          statusLine = null;
            string          temp = null;
            int             statusCode;
            Match           m = null;


            try
            {
            	if(autoConnect)
                	Connect ();

                outStream.WriteLine (command);
                outStream.Flush ();
                
                // ----------------------------------------------------------------------
                // First, we'll read the status line and analyze the response code. If
                // it's in the range of 400-599, some error has occured and no more
                // data will be coming down the pipe. We may as well throw an exception
                // here.
                // ----------------------------------------------------------------------
                statusLine = inStream.ReadLine ();
                lastActiveTime = DateTime.Now;

				if(!string.IsNullOrEmpty(statusLine))	
				{
                	m = reStatus.Match (statusLine);
                	statusCode = Convert.ToInt32(m.Groups[1].Value);
				}
				else
					statusCode = 420;

                if (statusCode >= 400 && statusCode <= 599)
                {
                    string errorMessage = null;

                    switch (statusCode)
                    {
                        case 420: // Server temporarily unavailable
                        case 421: // Server shutting down at operator request
                            errorMessage = "The dictionary server is not available at this time."; break;

                        case 500: errorMessage = "Syntax error, command is not recognized." ; break;
                        case 501: errorMessage = "Syntax error, illegal parameters in command."; break;
                        case 502: errorMessage = "Command is not implemented."; break;
                        case 503: errorMessage = "Command parameter not implemented."; break;
                        case 530: errorMessage = "Access denied."; break;
                        case 531: errorMessage = "Access denied, use \"SHOW INFO\" for server information."; break;
                        case 532: errorMessage = "Access denied, unknown mechanism."; break;
                        case 550: errorMessage = "Invalid database, use \"SHOW DB\" for list of databases."; break;
                        case 551: errorMessage = "Invalid strategy, use \"SHOW STRAT\" for a list of strategies."; break;
                        case 552: errorMessage = "No definitions found."; break;
                        case 554: errorMessage = "No databases present."; break;
                        case 555: errorMessage = "No strategies available."; break;
                    }

                    throw new DictionaryServerException (url, statusCode, command, errorMessage); 
                }

                //  -----------------------------------------------------
                // The body of the response follows next
                //  -----------------------------------------------------
                if(readResponse)
                {
	                response.Append (inStream.ReadLine ());
	                response.Append ("\r\n");
	
	                while (IsDataArrived())
	                {
	                    temp = inStream.ReadLine ();
	
	                    if (temp.Length == 0 || temp == "." || temp.StartsWith ("250"))
	                        continue;
	
	                    response.Append (temp);
	                    response.Append ("\r\n");
	                }
                }
				lastActiveTime = DateTime.Now;
                return response.ToString ();
            }
            finally
            {
                // Clean up resources if all hell breaks loose.
                if(autoConnect)
                	Disconnect ();
            }
        }

        #endregion

		/// <summary>
		///  This command allows the client to provide information about itself
		///  for possible logging and statistical purposes.  All clients SHOULD
		///  send this command after connecting to the server. 
		/// </summary>
		/// <param name="clientName"></param>
		public void SetClient(string clientName)
		{
			ExecuteCommand (string.Concat("CLIENT ", clientName));
		}
		
		/// <summary>
		/// This command is used by the client to cleanly exit the server.  All
		/// DICT servers MUST implement this command.
		/// </summary>
		public void Quit()
		{
			Disconnect();
		}
		
        #region Retrieve a list of available databases

        /// <summary>
        /// Displays the list of currently accessible databases.
        /// </summary>
        /// <returns>A list of currently accessible databases as <see cref="DatabaseCollection" />.</returns>
        public DatabaseCollection GetDatabases ()
        {
            lock (cachedDatabases)
            {
                if (cachedDatabases.Count == 0)
                {
                    // --------------------------------------------------------
                    // Read a list of available databases and cache them
                    // --------------------------------------------------------
                    string          dictionaries = ExecuteCommand ("SHOW DB");
                    StringReader    rdr = new StringReader (dictionaries);
                    string          name = null;
                    string          description = null;
                    string          entry = null;
                    Match           m = null;

                    try
                    {
                        // Parse each line and extract database name and its description
                        while ((entry = rdr.ReadLine ()) != null)
                        {
                            m = reDatabaseAndDescription.Match (entry);
                    
                            if (!m.Success)
                                continue;

                            name = m.Groups[1].Value;
                            description = m.Groups[2].Value;

                            cachedDatabases.Add (new Database (name, description));
                        }
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close ();
                    }
                }
            }

            return cachedDatabases;
        }

        #endregion

        #region Retrieve a list of match strategies

        /// <summary>
        /// Gets the list of currently supported match strategies.
        /// </summary>
        /// <returns>A list of currently supported match strategies as <see cref="StrategyCollection" />.</returns>
        public StrategyCollection GetMatchStrategies ()
        {
            lock (cachedStrategies)
            {
                if (cachedStrategies.Count == 0)
                {
                    // --------------------------------------------------------
                    // Read a list of available databases and cache them
                    // --------------------------------------------------------
                    string          strategies = ExecuteCommand ("SHOW STRAT");
                    StringReader    rdr = new StringReader (strategies);
                    string          strategy = null;
                    string          description = null;
                    string          entry = null;
                    Match           m = null;

                    try
                    {
                        // Parse each line and extract dictionary name and description
                        while ((entry = rdr.ReadLine ()) != null)
                        {
                            m = reStrategyAndDescription.Match (entry);
                    
                            if (!m.Success)
                                continue;

                            strategy = m.Groups[1].Value;
                            description = m.Groups[2].Value;

                            cachedStrategies.Add (new Strategy (strategy, description));
                        }
                    }
                    finally
                    {
                        if (rdr != null)
                            rdr.Close ();
                    }
                }
            }

            return cachedStrategies;
        }

        #endregion

        #region Read server information

        /// <summary>
        /// Displays local server information written by the local administrator.
        /// </summary>
        /// <remarks>This could include information about local databases or strategies,
        /// or administrative information such as who to contact for access to
        /// databases requiring authentication.</remarks>
        /// <returns>Server information.</returns>
        public string GetServerInformation ()
        {
            return ExecuteCommand ("SHOW SERVER");
        }
        #endregion

        #region Read database information

        /// <summary>
        /// Retrieves the source, copyright, and licensing information about the 
        /// specified database.  The information is free-form text and is 
        /// suitable for display to the user in the same manner as a definition.
        /// </summary>
        /// <param name="name">Database name.</param>
        /// <returns>Information about the database.</returns>
        public string GetDatabaseInformation (string name)
        {
            return ExecuteCommand (string.Concat("SHOW INFO ", name));
        }

        #endregion

        #region Match a word/phrase

        /// <summary>
        /// Searches an index for the dictionary, and reports words which were found using a particular strategy.
        /// Not all strategies are useful for all dictionaries, and some dictionaries may support
        /// additional search strategies (e.g., reverse lookup).
        /// </summary>
        /// <param name="text">Word/phrase to match.</param>
        /// <param name="strategy">Search strategy to employ for matching. You can obtain a list of
        /// supported strategies via <see cref="GetMatchStrategies">GetMatchStrategies</see></param>
        /// <returns>A collection of matches as <see cref="WordMatchCollection" />.</returns>
        /// <remarks>
        /// <p>If the database name is specified with an exclamation point (decimal 
        /// code 33, "!"), then all of the databases will be searched until a 
        /// match is found, and all matches in that database will be displayed. </p>
        /// 
        /// <p>If the database name is specified with a star (decimal code 42, "*"), 
        /// then all of the matches in all available databases will be displayed.</p>
        /// 
        /// <p>In both of these special cases, the databases will be searched in the same order 
        /// as that printed by <see cref="GetDatabases">GetDatabases</see>.</p>
        /// </remarks>
        public WordMatchCollection GetMatches (string text, string strategy)
        {
            return GetMatches (text, "*", strategy);
        }

        /// <summary>
        /// searches an index for the dictionary, and reports words which were found using a particular strategy.
        /// </summary>
        /// <param name="text">Word/phrase to match.</param>
        /// <param name="database">Dictionary database. You can obtain a list of available
        /// dictionary databases via <see cref="GetDatabases">GetDatabases</see>.</param>
        /// <param name="strategy">Search strategy to employ for matching. You can obtain a list of
        /// supported strategies via <see cref="GetMatchStrategies">GetMatchStrategies</see></param>
        /// <returns>A collection of matches as <see cref="WordMatchCollection" />.</returns>
        /// <remarks>
        /// <p>If the database name is specified with an exclamation point (decimal 
        /// code 33, "!"), then all of the databases will be searched until a 
        /// match is found, and all matches in that database will be displayed. </p>
        /// 
        /// <p>If the database name is specified with a star (decimal code 42, "*"), 
        /// then all of the matches in all available databases will be displayed.</p>
        /// 
        /// <p>In both of these special cases, the databases will be searched in the same order 
        /// as that printed by <see cref="GetDatabases">GetDatabases</see>.</p>
        /// </remarks>
        public WordMatchCollection GetMatches (string text, string database, string strategy)
        {
            if (database == null || database.Length == 0)
                database = "*";

            string                  command = string.Concat ("MATCH ", database, " ", strategy, " ", text);
            string[]                responses = ExecuteCommand (command).Split ('\n');
            WordMatchCollection     wordMatches = new WordMatchCollection();

            for (int i=0; i<responses.Length; i++)
            {
                if (responses[i].Length == 0)
                    continue;

                Match       m = reDatabaseAndWordMatch.Match (responses[i]);
                string      db = m.Groups[1].Value;
                string      wordMatch = m.Groups[2].Value;

                wordMatches.Add (new WordMatch(wordMatch, db));
            }

            return wordMatches;
        }

        #endregion

        #region Read word/expression definition(s)

        /// <summary>
        /// Look up the specified word in the specified database.
        /// </summary>
        /// <param name="text">A term to look up.</param>
        /// <param name="database">Database name.</param>
        /// <returns>One or more definitions of the term as <see cref="DefinitionCollection" />.</returns>
        /// <remarks>
        /// <p>If the database name is specified with an exclamation point (decimal 
        /// code 33, "!"), then all of the databases will be searched until a 
        /// match is found, and all matches in that database will be displayed. </p>
        /// 
        /// <p>If the database name is specified with a star (decimal code 42, "*"), 
        /// then all of the matches in all available databases will be displayed.</p>
        /// 
        /// <p>In both of these special cases, the databases will be searched in the same order 
        /// as that printed by <see cref="GetDatabases">GetDatabases</see>.</p>
        /// </remarks>
        public DefinitionCollection GetDefinitions (string text, string database)
        {
            return InternalGetDefinitions (text, database);
        }

        /// <summary>
        /// Look up the specified word in all databases.
        /// </summary>
        /// <param name="text">A term to look up.</param>
        /// <returns>One or more definitions of the term as <see cref="DefinitionCollection" />.</returns>
        public DefinitionCollection GetDefinitions (string text)
        {
            return InternalGetDefinitions (text, null);
        }

        /// <summary>
        /// This is an internal helper which looks up a term/expression, parses it and 
        /// returns as a list for convenience.
        /// </summary>
        private DefinitionCollection InternalGetDefinitions (string text, string database)
        {
            if (database == null || database.Length == 0)
                database = "*";
            
            string                  command = string.Concat ("DEFINE ", database, " \"", text, "\"");
            string[]                responses = reDefinitions.Split(ExecuteCommand (command));
            DefinitionCollection    definitionList = new DefinitionCollection ();

         
            for (int i=0; i<responses.Length; i++)
            {
				// Split occasionally produces empty strings
				if (responses[i].Trim().Length == 0)
                    continue;

                StringReader rdr = new StringReader (responses[i]);

                try 
                {
                    string statusLine = rdr.ReadLine ();

                    // --------------------------------------------------------------------------
                    // The status line would look similar to this:
                    // "Shortcake" web1913 "Webster's Revised Unabridged Dictionary (1913)"
                    //
                    // We need to pull out the dictionary name where it was found.
                    // --------------------------------------------------------------------------
                    Match       m = reDatabase.Match (statusLine);
                    string      dbName = null, dbDescription = null;
                    string      termDescription = null;

                    dbName              = m.Groups[1].Value;
                    dbDescription       = m.Groups[2].Value;
                    termDescription     = rdr.ReadToEnd ().ToString ();

                    definitionList.Add (new Definition (termDescription, new Database (dbName, dbDescription)));
                }
                finally
                {
                    rdr.Close ();
                }
            }

            return definitionList;
        }

        #endregion
    }
}
