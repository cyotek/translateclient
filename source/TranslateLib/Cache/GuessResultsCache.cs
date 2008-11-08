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
using System.Collections.Generic;
using System.Threading;


namespace Translate
{
	/// <summary>
	/// Description of GuessResultsCache.
	/// </summary>
	public static class GuessResultsCache
	{
		static  GuessResultsCache()
		{
			collectGarbageTimer = new Timer(new TimerCallback(OnTimer), null, 60000, 60000);
		}
		
		static Timer collectGarbageTimer;
		
		static bool useCache = false;
		
		public static bool UseCache {
			get { return useCache; }
			set { 
					useCache = value; 
					if(!value)
						results_history.Clear();
				}
		}
		
		static Dictionary<string, GuessResultsHashtable> cache = new Dictionary<string, GuessResultsHashtable>();
		
		static List<GuessResult> results_history = new List<GuessResult>();
		
		public static GuessResult GetCachedGuessResult(LanguageGuesser serviceItem, string phrase)
		{
			if(!useCache)
				return new GuessResult(serviceItem, phrase);
				
			string key = phrase.Trim().ToLowerInvariant();
			if(key.Length > 300)
				key = key.Substring(0, 300);
			
			GuessResultsHashtable collection;
			bool collection_exists = true;
			
			lock(cache)
			{
				if(!cache.TryGetValue(key, out collection))
				{
					collection = new GuessResultsHashtable();
					cache.Add(key, collection);
					collection_exists = false;
				}	
			}
			
			int hash = serviceItem.FullName.GetHashCode();
			bool needed_new_result = !collection_exists;

			GuessResult res = null;
			
			lock(collection)
			{
				if(!needed_new_result)
				{
					if(!collection.TryGetValue(hash, out res))
						needed_new_result = true;
					else
					{
						needed_new_result = (res.Error != null && !res.ResultNotFound) || res.Phrase != phrase;
					}
				}
	
				if(needed_new_result)
				{
					res = new GuessResult(serviceItem, phrase);
					collection[hash] = res;
					lock(results_history)
					{
						results_history.Add(res);
					}	
				}
				else
				{
					res.LastUsed = DateTime.Now;
					lock(results_history)
					{
						results_history.Remove(res);
						results_history.Add(res);
					}
				}	
			}
			return res;
		}
		
		static void OnTimer(Object stateInfo)
		{
			List<GuessResult> results_to_delete = new List<GuessResult>();

			lock(results_history)
			{
				int count_to_remove = results_history.Count - 100;
				if(count_to_remove > 0)
				{
					results_to_delete.AddRange(results_history.GetRange(0, count_to_remove));
					results_history.RemoveRange(0, count_to_remove);
				}
			}
			
			
			List<string> collections_to_delete = new List<string>();
			
			GuessResultsHashtable collection;
			foreach(GuessResult r in results_to_delete)
			{
				string key = r.Phrase.Trim().ToLowerInvariant();
				if(key.Length > 300)
					key = key.Substring(0, 300);
				
				lock(cache)
				{
					if(!cache.TryGetValue(key, out collection))
					{
						continue;
					}	
				}
				
				int hash = r.DetectorItem.FullName.GetHashCode();
				
				lock(collection)
				{
					GuessResult res;
					if(!collection.TryGetValue(hash, out res))
					{
						continue;
					}
					
					if(res.Phrase == r.Phrase)
					{
						collection.Remove(hash);
						if(collection.Count == 0)	
							collections_to_delete.Add(key);
					}	
				}
			}
			
			foreach(string key in collections_to_delete)
			{
				lock(cache)
				{
					if(cache.TryGetValue(key, out collection))
					{
						lock(collection)
						{
							if(collection.Count == 0)
							{
								cache.Remove(key);
							}
						}
					}	
				}
			
			}

		}
		
	}
}
