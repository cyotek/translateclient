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
	/// Description of WikiEncyclopediaEngine.
	/// </summary>
	public class WikiEncyclopediaEngine : Encyclopedia
	{
		public WikiEncyclopediaEngine(WikiSearchEngine searchEngine, string searchHost)
		{
			this.searchEngine = searchEngine;
			this.searchHost = searchHost;
			
			foreach(Language from in WikiUtils.LangToKey.Keys)
			{
				  AddSupportedTranslation(new LanguagePair(from, from));
			}
			
			AddSupportedSubject(SubjectConstants.Common);
			
			Name = "_encyclopedia";
			
		}
		
		WikiSearchEngine searchEngine;
		string searchHost;
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			Result searchResult = searchEngine.Translate(phrase, languagesPair, subject, networkSetting);
			if(!searchResult.HasData || searchResult.Translations.Count < 1)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			string url = StringParser.Parse("<a href=\"", "\">", searchResult.Translations[0]);
			string searched_name = url.Substring(url.LastIndexOf("/") + 1);
			
			if(string.Compare(phrase, searched_name, true) != 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			//http://en.wikipedia.org/w/api.php?action=parse&prop=text&format=xml&page=Ukraine
			string query = "http://{0}.{1}/w/api.php?action=parse&prop=text|revid&format=xml&page={2}";
			string lang = WikiUtils.ConvertLanguage(languagesPair.From);
			query = string.Format(query, lang, 
				searchHost,
				HttpUtility.UrlEncode(searched_name));
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.IndexOf("<parse revid=\"0\">") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}

			string res = StringParser.Parse("<text>", "</text>", responseFromServer);
			res = "html!<div style='width:{allowed_width}px;overflow:scroll'>" + HttpUtility.HtmlDecode(res) + "&nbsp</div>";
			
			res = res.Replace("<h2>", "");
			res = res.Replace("</h2>", "");
			
			res = StringParser.RemoveAll("<span class=\"editsection\">[<a", "</a>]", res);
			res = StringParser.RemoveAll("href=\"#", "\"", res);
			
			
			url = string.Format("a href=\"http://{0}.{1}/", lang, searchHost);
			res = res.Replace("a href=\"/", url);
			
			url = string.Format("img src=\"http://{0}.{1}/", lang, searchHost);
			res = res.Replace("img src=\"/", url);
			result.Translations.Add(res);
		}
	}
}
