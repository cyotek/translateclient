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
using System.Text;

namespace Translate
{
	/// <summary>
	/// Description of ServiceSettingsContainer.
	/// </summary>
		internal class ServiceSettingsContainer
		{
			public ServiceSettingsContainer(ServiceItemSetting setting, bool showLanguage)
			{
				this.setting = setting;
				this.showLanguage = showLanguage;
				GenerateName();
			}
			
			bool showLanguage;
			public bool ShowLanguage {
				get { return showLanguage; }
				set { showLanguage = value; }
			}
			
			ServiceItemSetting setting;
			public ServiceItemSetting Setting {
				get { return setting; }
			}
			
			string error;
			public string Error {
				get { return error; }
				set { error = value; }
			}
			
			bool enabled;
			public bool Enabled {
				get { return enabled; }
				set { enabled = value; }
			}
			
			
			bool disabledByUser;
			public bool DisabledByUser {
				get { return disabledByUser; }
				set { disabledByUser = value; }
			}
			
			string name;
			public string Name {
				get { return name; }
				set { name = value; }
			}
			
			string type;
			public string Type {
				get { return type; }
				set { type = value; }
			}
			
			bool isQuestionMaskSupported;
			public bool IsQuestionMaskSupported {
				get { return isQuestionMaskSupported; }
				set { isQuestionMaskSupported = value; }
			}
			
			
			
			bool isAsteriskMaskSupported;
			public bool IsAsteriskMaskSupported {
				get { return isAsteriskMaskSupported; }
				set { isAsteriskMaskSupported = value; }
			}
			
			public string GetServiceTooltipText()
			{
				StringBuilder sb = new StringBuilder();
				
				sb.AppendLine(LangPack.TranslateString(setting.ServiceItem.Service.FullName));
				sb.AppendLine(GetServiceItemType(setting.ServiceItem));
				
				string languagePair = LangPack.TranslateLanguage(Setting.LanguagePair.From) +
						"->" + 
						LangPack.TranslateLanguage(Setting.LanguagePair.To);
				
				sb.AppendLine(languagePair);
				
				if(setting.Subject != SubjectConstants.Common)
				{
					sb.AppendLine(LangPack.TranslateString(setting.Subject));
				}

				if(setting.ServiceItem.CharsLimit != -1)
				{
					sb.AppendLine(string.Format(LangPack.TranslateString("Limit {0} : {1} characters"), 
						"", setting.ServiceItem.CharsLimit));
				}
	
				if(setting.ServiceItem.LinesLimit != -1)
				{
					sb.AppendLine(string.Format(LangPack.TranslateString("Limit {0} : {1} lines"), 
						"", setting.ServiceItem.LinesLimit));
				}
	
				if(setting.ServiceItem.WordsLimit != -1)
				{
					sb.AppendLine(string.Format(LangPack.TranslateString("Limit {0} : {1} words"), 
						"", setting.ServiceItem.WordsLimit));
				}
				
				if(IsAsteriskMaskSupported || IsQuestionMaskSupported)
				{
					sb.Append(LangPack.TranslateString("Masks") + " : ");
					if(IsAsteriskMaskSupported)
						sb.Append("'*'"); 
					
					if(IsAsteriskMaskSupported && IsQuestionMaskSupported)
						sb.Append(","); 
						
					if(IsQuestionMaskSupported)
						sb.Append("'?'"); 
					sb.AppendLine();	
				}
				
				sb.AppendLine("---");
				if(DisabledByUser)
				{
					sb.AppendLine(StringParser.RemoveAll("<", ">", LangPack.TranslateString("<b>Status</b> : Disabled")));
				}
				else if(Enabled)
				{
					sb.AppendLine(StringParser.RemoveAll("<", ">", LangPack.TranslateString("<b>Status</b> : Enabled")));
				}
				else
				{
					sb.Append(StringParser.RemoveAll("<", ">", LangPack.TranslateString("<b>Status</b> : Error")));
					sb.AppendLine(" - " + Error);
				}
				
				return sb.ToString().Trim();	
			}
			
			public static string GetServiceItemType(ServiceItem serviceItem)
			{
				string result;
				if(serviceItem is Translator)
				{
					result = LangPack.TranslateString("Translator");
				}
				else if(serviceItem is AntonymsDictionary)
				{
					result = LangPack.TranslateString("Dictionary of antonyms");
				}
				else if(serviceItem is SynonymsDictionary)
				{
					result = LangPack.TranslateString("Dictionary of synonyms");
				}
				else if(serviceItem is PhraseologicalDictionary)
				{
					result = LangPack.TranslateString("Phraseological dictionary");
				}
				else if(serviceItem is Encyclopedia)
				{
					result = LangPack.TranslateString("Encyclopedia");
				}
				else if(serviceItem is MonolingualSearchEngine || serviceItem is BilingualSearchEngine)
				{
					result = LangPack.TranslateString("Search Engine");
				}
				else if(serviceItem is MonolingualDictionary)
				{
					result = LangPack.TranslateString("Monolingual dictionary");
				}
				else if(serviceItem is BilingualPhrasesDictionary)
				{
					result = LangPack.TranslateString("Bilingual phrases dictionary");
				}
				else if(serviceItem is BilingualSentensesDictionary)
				{
					result = LangPack.TranslateString("Bilingual sentenses dictionary");
				}
				else
				{
					result = LangPack.TranslateString("Bilingual dictionary");
				}
				return result;
			}
			
			public void GenerateName()
			{
				name = setting.ServiceItem.Service.Url.Host;
				name += "-";
				
				if(setting.ServiceItem is Translator)
				{
					name += LangPack.TranslateString("T");
				}
				else if(setting.ServiceItem is AntonymsDictionary)
				{
					name += LangPack.TranslateString("AD");
				}
				else if(setting.ServiceItem is SynonymsDictionary)
				{
					name += LangPack.TranslateString("SD");
				}
				else if(setting.ServiceItem is PhraseologicalDictionary)
				{
					name += LangPack.TranslateString("PD");
				}
				else if(setting.ServiceItem is Encyclopedia)
				{
					name += LangPack.TranslateString("EN");
				}
				else if(setting.ServiceItem is MonolingualSearchEngine || setting.ServiceItem is BilingualSearchEngine)
				{
					name += LangPack.TranslateString("SE");
				}
				else if(setting.ServiceItem is MonolingualDictionary)
				{
					name += LangPack.TranslateString("MD");
				}
				else if(setting.ServiceItem is BilingualPhrasesDictionary)
				{
					name += LangPack.TranslateString("BP");
				}
				else if(setting.ServiceItem is BilingualSentensesDictionary)
				{
					name += LangPack.TranslateString("BS");
				}
				else
				{
					name += LangPack.TranslateString("BD");
				}
				type = GetServiceItemType(setting.ServiceItem);
				
				BilingualDictionary dictionary = setting.ServiceItem as BilingualDictionary;
				if(dictionary != null)
				{
					isAsteriskMaskSupported = dictionary.IsAsteriskMaskSupported;
					isQuestionMaskSupported = dictionary.IsQuestionMaskSupported;
				}
				
				if(showLanguage)
				{
					name += "-";
					name += GetShortNameOfTranslateDirection(Setting.LanguagePair);
				}
				
				if(setting.Subject != SubjectConstants.Common)
				{
					name += "-";
					name += LangPack.TranslateString(setting.Subject);
				}
			}
			
			public static string GetShortNameOfTranslateDirection(LanguagePair languagePair)
			{
				string result = "";
				
				result += StringParser.SafeResizeString(LangPack.TranslateLanguage(languagePair.From), 3);
				result += "->";
				result += StringParser.SafeResizeString(LangPack.TranslateLanguage(languagePair.To), 3);
				
				return result;
			}
			
			public override string ToString()
			{
				return name;
			}
			
			public void Check(string phrase)
			{
				try
				{
					string err;
					if(setting.ServiceItem.CheckPhrase(phrase, out err))
					{
						error = "";
						enabled = true;
					}
					else
					{
						error = LangPack.TranslateString(err);
						enabled = false;
					}
				}
				catch(Exception e)
				{
					error = LangPack.TranslateString(e.Message);
					enabled = false;
				}
			}
			
		}

}
