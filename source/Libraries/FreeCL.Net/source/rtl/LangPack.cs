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
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
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
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Collections.ObjectModel;


namespace FreeCL.RTL
{

	[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
	public delegate void OnLanguageChangedEventHandler();

	/// <summary>
	/// Description of LangPack.
	/// </summary>
	public static class LangPack
	{
	
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static LangPack()
		{
			LoadCurrent();
		}
		
		static string ConvertBackslashes(string data)
		{
			StringBuilder res = new StringBuilder();
			for(int i = 0; i < data.Length; i++)
			{
				if((data[i] == '\\') && i < (data.Length - 1))
				{
					if(data[i + 1] == 'n')
						res.Append('\n');
					else if(data[i + 1] == 't')
						res.Append('\t');
					else if(data[i + 1] == 'r')
						res.Append('\r');
					else if(data[i + 1] == '\\')
						res.Append('\\');
					else
					{
						res.Append(data[i]);
						i--;
					}
						
					i++;
				}
				else
					res.Append(data[i]);
			}
			return res.ToString();
		}
			
		public static string TranslateString(string data)
		{
			string res = null;
			if(!hash.TryGetValue(data, out res))
				res = data; 
			return res;
		}
			
		static StringsDictionary hash = new StringsDictionary(1000);
		
		public static void Reset()
		{
			hash.Clear();
		}
		
		private static void LoadLangPack(string FileName)
		{
			string line, engl, local;
			
			StreamReader sr = new StreamReader(FileName);
			
			line = sr.ReadLine();
			while(line != null)
			{
				engl = line.Trim();
				if((engl.Length == 0) || (engl[0] == ';') || !(engl[0] == '[' &&	engl[engl.Length - 1] == ']'))
				{
					line = sr.ReadLine();
					continue;
				}
					
				engl = engl.Substring(1, engl.Length - 2);
				
				engl = ConvertBackslashes(engl);
					
				local = sr.ReadLine();
				
				if(string.IsNullOrEmpty(local))
					local = engl;
				else
					local = ConvertBackslashes(local);
				
				hash[engl] = local;
				line = sr.ReadLine();
			}
			
		}
			
		
		[Conditional("TRACE")]
		public static void Trace(string message)
		{
			System.Diagnostics.Trace.WriteLine(Environment.TickCount.ToString(CultureInfo.InvariantCulture) + ":" +
											System.Threading.Thread.CurrentThread.GetHashCode().ToString(CultureInfo.InvariantCulture) + ":" +
											message
										 );
		}
		
		
		static string currentLang;
		static public string CurrentLanguage {
			get { return currentLang; }
			set { 
					
					Load(value);
				}
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static ReadOnlyCollection<string> GetLanguages()
		{
			List<string> res = new List<string>();
			string dir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
			string[] files = Directory.GetFiles(dir + "\\lang");
			foreach(string fileName in files)
			{
				if(Path.GetExtension(fileName).ToUpper(CultureInfo.InvariantCulture) == ".LNG")
					res.Add(Path.GetFileNameWithoutExtension(fileName));
			}
			res.Sort();
			res.Insert(0, "English");
			res.Remove("Original");
			return  new ReadOnlyCollection<string>(res);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void Load(string langName)
		{
			if(currentLang == langName)
				return;
			
			Reset();
			if(langName != "English")
			{
					string fileName = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
					try
					{
						fileName = fileName + "\\lang\\" + langName + ".lng";
						if(File.Exists(fileName))
						{
							LoadLangPack(fileName);
							currentLang = langName;
							RaiseOnLanguageChanged();
						}
					}
					catch(Exception E)
					{
						FreeCL.RTL.Trace.TraceException(E);
						System.Diagnostics.Debug.Assert(false,"Exception : " + E.Message + "\nwith file: " + fileName);
					}
			
			}
			else
			{
				currentLang = langName; 
				RaiseOnLanguageChanged();
			}
			
		}
		
		static void RaiseOnLanguageChanged()
		{
			if(LanguageChanged == null)
				return;
				
			Delegate[] delegates = LanguageChanged.GetInvocationList();
			
			foreach(Delegate d in delegates)
			{
				System.Windows.Forms.Control ctrl = d.Target as System.Windows.Forms.Control;
				
				if(ctrl == null || !ctrl.IsDisposed)
					d.DynamicInvoke(null);
			}
		}
		
		public static void LoadCurrent()
		{
			Load(System.Threading.Thread.CurrentThread.CurrentUICulture.Parent.EnglishName);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public static event OnLanguageChangedEventHandler LanguageChanged;
		
		public static void RegisterLanguageEvent(OnLanguageChangedEventHandler onLanguageChangedEventHandler)
		{
			LanguageChanged += onLanguageChangedEventHandler;
		}
	}
}
