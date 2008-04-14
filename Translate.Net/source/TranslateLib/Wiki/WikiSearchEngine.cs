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
using System.Net; 
using System.Text; 
using System.IO; 
using System.Web; 
using System.IO.Compression;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Globalization;

namespace Translate
{
	/// <summary>
	/// Description of WikiSearchEngine.
	/// </summary>
	public class WikiSearchEngine : MonolingualDictionary
	{
	
		//http://s23.org/wikistats/wikipedias_html.php?sort=good_desc
		public WikiSearchEngine(string searchHost)
		{
			this.searchHost = searchHost;
			langToKey.Add(Language.Arabic, "ar");
			langToKey.Add(Language.Belarusian, "be");
			langToKey.Add(Language.Bulgarian, "bg");
			langToKey.Add(Language.Chinese, "zh");
			langToKey.Add(Language.Croatian, "hr");
			langToKey.Add(Language.Czech, "cs");
			langToKey.Add(Language.Danish, "da");
			langToKey.Add(Language.Dutch, "nl");
			langToKey.Add(Language.English, "en");
			langToKey.Add(Language.Esperanto, "eo");
			langToKey.Add(Language.Estonian, "et");
			langToKey.Add(Language.Icelandic, "is");
			langToKey.Add(Language.Finnish, "fi");
			langToKey.Add(Language.French, "fr");
			langToKey.Add(Language.German, "de");
			langToKey.Add(Language.Greek, "el");
			langToKey.Add(Language.Hungarian, "hu");
			langToKey.Add(Language.Italian, "it");
			langToKey.Add(Language.Japanese, "ja");
			langToKey.Add(Language.Korean, "ko");
			langToKey.Add(Language.Latin, "la");
			langToKey.Add(Language.Latvian, "lv");
			langToKey.Add(Language.Lithuanian, "lt");
			langToKey.Add(Language.Macedonian, "mk");
			langToKey.Add(Language.Norwegian, "no");
			langToKey.Add(Language.Polish, "pl");
			langToKey.Add(Language.Portuguese, "pt");
			langToKey.Add(Language.Romanian, "ro");
			langToKey.Add(Language.Russian, "ru");
			langToKey.Add(Language.Serbian, "sr");
			langToKey.Add(Language.Slovak, "sk");
			langToKey.Add(Language.Slovenian, "sl");
			langToKey.Add(Language.Spanish, "es");
			langToKey.Add(Language.Swedish, "sv");
			langToKey.Add(Language.Ukrainian, "uk");
			
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				  AddSupportedTranslation(new LanguagePair(from, from));
			}
			
			AddSupportedSubject(SubjectConstants.Common);
			
			Name = "_search";
		}
		
		string searchHost;

		
		SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		string ConvertLanguage(Language language)
		{
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}
		
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://{0}.{1}/w/api.php?action=query&list=search&srsearch={2}&srlimit=15&format=xml&srwhat=text";
			string lang = ConvertLanguage(languagesPair.From);
			query = string.Format(query, lang, 
				searchHost,
				HttpUtility.UrlEncode(phrase));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.IndexOf("info=\"") >= 0)
			{
				string error = StringParser.Parse("info=\"", "\"", responseFromServer);
				throw new TranslationException(error);
			}
			
			if(responseFromServer.IndexOf("<p ns=\"0\" title=\"") < 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			StringParser parser = new StringParser(responseFromServer);
			string[] items = parser.ReadItemsList("<p ns=\"0\" title=\"", "\"", "787654323");
			
			string link;
			foreach(string part in items)
			{
				//link = "html!";
				link = "html!<a href=\"http://{0}.{1}/wiki/{2}\">{3}</a>";
				link = string.Format(link, lang, 
					searchHost,
					part,
					part);
				result.Translations.Add(link);
			}
		}
	}
}


