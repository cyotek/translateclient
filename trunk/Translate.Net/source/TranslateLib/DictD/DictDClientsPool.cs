#region License block : MPL 1.1/GPL 2.0/LGPL 2.1
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is the FreeCL.Net library.
 *
 * The Initial Developer of the Original Code is 
 *  Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com).
 * Portions created by the Initial Developer are Copyright (C) 2008
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using Translate.DictD;
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of DictDClientsPool.
	/// </summary>
	internal static class DictDClientsPool
	{
	
		static object lockObject = new object();
		
		static UrlClientDictionary availableClients = new UrlClientDictionary();
		static UrlClientDictionary lockedClients = new UrlClientDictionary();
		static UrlClientDictionaryErrorTime errorClients = new UrlClientDictionaryErrorTime();
		
		class DictionaryClientComparerByConnectTicks : IComparer<DictionaryClient>
		{
			public int Compare(DictionaryClient x, DictionaryClient y)
			{
				return (int)(x.ConnectTicks - y.ConnectTicks);
			}
			
		}
		
		
		internal static DictionaryClient GetPooledClient(UrlList urls)
		{
		
			//list of all to detected connected, etc
			UrlClientDictionaryState states = new UrlClientDictionaryState();
			foreach(Uri url in urls)
			{
				states.Add(url, State.Unknown);
			}
			
			DictionaryClient result = null;
			do
			{
				lock(lockObject)
				{
					//get all available and connected
					List<DictionaryClient> available = new List<DictionaryClient>();
					DictionaryClient dictClient;
					foreach(Uri url in urls)
					{
						if(availableClients.TryGetValue(url, out dictClient))
						{
							if((!errorClients.ContainsKey(url) 
								|| errorClients[url].AddSeconds(5) < DateTime.Now) &&
								states[url] != State.Error)
									available.Add(dictClient);
						}	
					}
					
					if(available.Count > 0)
					{
						if(available.Count > 1)
							available.Sort(new DictionaryClientComparerByConnectTicks());
							
						result = available[0];	
					}
					
					if(result == null)
					{
						foreach(Uri url in urls)
						{
							if(states[url] != State.Error && 
								!lockedClients.ContainsKey(url) &&
								!availableClients.ContainsKey(url))
							{
								result = new DictionaryClient();
								result.Url = url;
								result.AutoConnect = false;
								result.ClientName = "http://translateclient.googlepages.com/";
								break;
							}
						}
					}
					
					if(result != null)
					{
						lockedClients.Add(result.Url, result);	
						availableClients.Remove(result.Url);
						states[result.Url] = State.Busy;
					}
				} //end lock
				
				if(result == null)
				{
					
					int cnt = 0;
					foreach(State state in states.Values)
					{
						if(state == State.Error)
							cnt++;
						else
							break;
					}
					if(cnt < states.Count)
						System.Threading.Thread.Sleep(200);
					else 
					{
						string errorMessage = "Can't connect to any dictd server : ";
						foreach(Uri url in urls)
							errorMessage += url + ", ";
						errorMessage = errorMessage.Substring(0, errorMessage.Length - 2); 	
						throw new TranslationException(errorMessage);
					}
				}	
				else
				{ //try to connect
				
					try
					{
						if(!result.Connected)
							result.Connected = true;
					}
					catch
					{
						states[result.Url] = State.Error;
						lock(lockObject)
						{
							errorClients[result.Url] =  DateTime.Now;
							availableClients.Add(result.Url, result);	
							lockedClients.Remove(result.Url);
						}	
						result = null;
					}
				}
			}
			while(result == null);
			
			return result;
		}
		
		internal static void PushPooledClient(DictionaryClient client)
		{
			lock(lockObject)
			{
				availableClients.Add(client.Url, client);	
				lockedClients.Remove(client.Url);
				errorClients.Remove(client.Url)	;
			}
		}
		
		internal class UrlClientDictionary : Dictionary<Uri, DictionaryClient>
		{
		
		}
	
		internal enum State
		{
			Busy,
			Error,
			Unknown
		}
		
		internal class UrlClientDictionaryState : Dictionary<Uri, State>
		{
		
		}

		internal class UrlClientDictionaryErrorTime : Dictionary<Uri, DateTime>
		{
		
		}
		
	}
	
	
}
