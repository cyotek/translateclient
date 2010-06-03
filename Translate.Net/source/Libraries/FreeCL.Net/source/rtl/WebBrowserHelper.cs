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
using System.Windows.Forms;
using System.Collections.Generic;

namespace FreeCL.RTL
{
	/// <summary>
	/// Description of WebBrowserHelper.
	/// </summary>
	public static class WebBrowserHelper
	{
		static WebBrowserHelper()
		{
		}
		
		public static void Wait(WebBrowser wBrowser)
		{
			int exceptionsCount = 0;
			bool isBusy = true;
			try
			{
				isBusy = /*wBrowser.IsBusy || */wBrowser.ReadyState != WebBrowserReadyState.Complete;
			}
			catch(UnauthorizedAccessException)
			{
				exceptionsCount++;
			}
			
			while(isBusy)
			{
				Application.DoEvents();
				System.Threading.Thread.Sleep(100);

				try
				{
					isBusy = /*wBrowser.IsBusy || */ wBrowser.ReadyState != WebBrowserReadyState.Complete;
					if(isBusy && 
						!wBrowser.IsBusy &&
						GetDocument(wBrowser) != null &&
						GetDocument(wBrowser).Body != null
						)
					{	//when it freeze by bug in ie ? 
						break;
					}
				}
				catch(UnauthorizedAccessException)
				{
					if(exceptionsCount > 10)
						throw;
					exceptionsCount++;
				}
				
			}	

			while(GetDocument(wBrowser) == null)
			{
				Application.DoEvents();
				System.Threading.Thread.Sleep(100);
			}	

			while(GetDocument(wBrowser).Body == null)
			{
				Application.DoEvents();
				System.Threading.Thread.Sleep(100);
			}	
				
		}
		
		public static HtmlDocument GetDocument(WebBrowser wBrowser)
		{
			HtmlDocument result = null;
			
			try 
			{
				result = wBrowser.Document;
			} 
			catch (System.UnauthorizedAccessException)
			{
				
			}
			return result;
		}
		
		public static bool ObjectToBool(object obj)
		{
			bool result = false;
			if(obj != null)
			{
				if(obj is bool)
				{
					result = (bool)obj;
				}
				else if(obj is string)
				{
					string val = (string)obj;
					if(val != "")
						result = val[0] == 't';
				}
			}
			return result; 
		}

		public static string ObjectToString(object obj)
		{
			string result = "";
			if(obj != null && obj is string)
			{
				result = obj as string;
			}
			return result; 
		}
		
		public static object InvokeScript(WebBrowser wBrowser, string scriptName, Object[] args)
		{
			//MessageBox.Show(scriptName);
			object res = null;
			/*if(wBrowser == null || GetDocument(wBrowser) == null)
				return null;	
			*/
			Wait(wBrowser);
			if(MonoHelper.IsUnix)
			{
				if(args != null && args.Length > 0)
				{
					string[] strArgs = new string[args.Length];
					for (int i = 0; i < args.Length; i++) 
					{
						if(args[i] != null)
						{
							if(args[i] is String)
								strArgs[i] = "\"" + args[i].ToString().Replace("\"", "\\\\\\\"").Replace("'", "\\\\\\'") + "\"";
								//strArgs[i] = "\"" + args[i].ToString().Replace("\"", "&dquote;").Replace("'", "&squote;") + "\"";
								//strArgs[i] = "\"" + args[i].ToString().Replace("\"", "\\\"").Replace("'", "\\\'") + "\"";
								//strArgs[i] = "\\\"" + args[i].ToString().Replace("\"", "'") + "\\\"";
								//strArgs[i] = "\\\"" + args[i].ToString().Replace("'", "\\\\\\\"") + "\\\"";
							else if(args[i] is Boolean)
								strArgs[i] = (Boolean)args[i]? "true" : "false"; 
							else
								strArgs[i] = args[i].ToString();
						}	
						else
							strArgs[i] = "null";
					}
					//string scriptNameTmp = "alert(\"" +scriptName+ ":" + String.Join (",", strArgs).Replace("\"", "'") + "\");" + scriptName + "(" + String.Join (",", strArgs) + ");alert";
					string scriptNameTmp = "true ? " + scriptName + "(" + String.Join (",", strArgs) + ") : alert";
					//string scriptNameTmp = "alert(\"&#34;\");alert";
					//if(scriptName == "AddTranslationCell")
					//	MessageBox.Show(scriptNameTmp);
					
					//Console.WriteLine(scriptNameTmp);
					//if(scriptName != "SetTableStyle")					
					res = GetDocument(wBrowser).InvokeScript(scriptNameTmp);
				}
				else
				{
					//res = GetDocument(wBrowser).InvokeScript("alert(typeof " +scriptName+ ");" + scriptName + "();alert");
					//res = GetDocument(wBrowser).InvokeScript(scriptName + "();" + "alert(\"" +scriptName+ "\");" + "alert");
					//Console.WriteLine(scriptName);
					res = GetDocument(wBrowser).InvokeScript(scriptName);
				}
				AddToBatch(wBrowser, scriptName, args);
			}	
			else
			{
				res = GetDocument(wBrowser).InvokeScript(scriptName, args);
			}	
			//Console.WriteLine("res : " + res);
			return res;
		}
		
		public static void ExecCopy(WebBrowser wBrowser)
		{
			if(!MonoHelper.IsUnix)
				 ExecCopyWin32(wBrowser);
			///TODO:implement this
		}
			
		public static void ExecCopyWin32(WebBrowser wBrowser)
		{
			GetDocument(wBrowser).ExecCommand("Copy", false, null);
		}
		
		public static void ExecSelectAll(WebBrowser wBrowser)
		{
			if(!MonoHelper.IsUnix)
				 ExecSelectAllWin32(wBrowser);
			///TODO:implement this
		}
			
		public static void ExecSelectAllWin32(WebBrowser wBrowser)
		{
			GetDocument(wBrowser).ExecCommand("SelectAll", false, null);
		}
		

		static Dictionary<WebBrowser, List< KeyValuePair<string, Object[]> > > batches = new Dictionary<WebBrowser, List< KeyValuePair<string, Object[]> > >();
		static List<WebBrowser> ignoreAddingToBatch = new List<WebBrowser>();
		
		public static void StartBatch(WebBrowser wBrowser)
		{
			if(!MonoHelper.IsUnix)
				return;
			
			if(batches.ContainsKey(wBrowser))
				batches[wBrowser].Clear();
			else
				batches[wBrowser] = new List<KeyValuePair<string, Object[]> >();
		}

		public static void AddToBatch(WebBrowser wBrowser, string scriptName, Object[] args)
		{
			if(!MonoHelper.IsUnix)
				return;
			
			lock(ignoreAddingToBatch)
			{
				if(ignoreAddingToBatch.Contains(wBrowser))
					return;
			}
			if(batches.ContainsKey(wBrowser))
			{
				batches[wBrowser].Add(new KeyValuePair<string, Object[]>(scriptName, args));
			}	
		}
		
		public static void ResetBatch(WebBrowser wBrowser)
		{
			if(!MonoHelper.IsUnix)
				return;
			
			batches.Remove(wBrowser);
		}

		public static void PlayBatch(WebBrowser wBrowser)
		{
			if(!MonoHelper.IsUnix)
				return;
			
			if(batches.ContainsKey(wBrowser))
			{
				lock(ignoreAddingToBatch) 
					ignoreAddingToBatch.Add(wBrowser);
				try
				{
					List< KeyValuePair<string, Object[]> > list = batches[wBrowser];
					foreach(KeyValuePair<string, Object[]> data in list)
					{
						InvokeScript(wBrowser, data.Key, data.Value);
					}
				}
				finally
				{
					lock(ignoreAddingToBatch) 
						ignoreAddingToBatch.Remove(wBrowser);
				}
			}			
		}
		
	}
}
