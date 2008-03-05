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

namespace Translate
{
	/// <summary>
	/// Description of ServiceSettingsContainer.
	/// </summary>
		internal class ServiceSettingsContainer
		{
			public ServiceSettingsContainer(ServiceSetting setting, bool showLanguage)
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
			
			ServiceSetting setting;
			public ServiceSetting Setting {
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
				else if(serviceItem is MonolingualDictionary)
				{
					result = LangPack.TranslateString("Monolingual dictionary");
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
				else if(setting.ServiceItem is MonolingualDictionary)
				{
					name += LangPack.TranslateString("MD");
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
					name += LangPack.TranslateLanguage(Setting.LanguagePair.From).Substring(0, 3) +
						"->" + 
						LangPack.TranslateLanguage(Setting.LanguagePair.To).Substring(0, 3);
				}
				
				if(setting.Subject != SubjectConstants.Common)
				{
					name += "-";
					name += LangPack.TranslateString(setting.Subject);
				}
			}
			
			public override string ToString()
			{
				return name;
			}
			
			public void Check(string phrase)
			{
				try
				{
					setting.ServiceItem.CheckPhrase(phrase);
					error = "";
					enabled = true;
				}
				catch(Exception e)
				{
					error = LangPack.TranslateString(e.Message);
					enabled = false;
				}
			}
			
		}

}
