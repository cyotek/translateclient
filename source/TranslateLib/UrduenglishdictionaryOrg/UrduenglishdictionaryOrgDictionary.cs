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
	/// Description of UrduenglishdictionaryOrgDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class UrduenglishdictionaryOrgDictionary : BilingualDictionary
	{
		public UrduenglishdictionaryOrgDictionary()
		{

			AddSupportedTranslationFromEnglish(Language.Urdu);
			AddSupportedTranslationToEnglish(Language.Urdu);
		
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 50;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//http://www.urduenglishdictionary.org/English-To-Urdu-Translation/test/Page-1.htm
			string query = "http://www.urduenglishdictionary.org/English-To-Urdu-Translation/{0}/Page-1.htm";
			if(languagesPair.From == Language.Urdu)
				query = "http://www.urduenglishdictionary.org/Urdu-To-English-Translation/{0}/Page-1.htm";
			
			query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase));
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.Contains("</B> could not be found!</h2>") && 
				responseFromServer.Contains("<U>Suggestions:</U><BR><BR><BR><BR><BR><BR></div>")
			)	
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else if(responseFromServer.Contains("</B> could not be found!</h2>") && 
				responseFromServer.Contains("<U>Suggestions:</U><BR><BR><a")
			)	
			{
				//suggestions exists 
				string suggestions = StringParser.Parse("<U>Suggestions:</U><BR><BR>", "</div>", responseFromServer);
				StringParser parser = new StringParser(suggestions);
				string[] suggestions_list = parser.ReadItemsList("<a href='", "</a>");
				foreach(string str in suggestions_list)
				{
					result.Translations.Add("html!<a href='http://www.urduenglishdictionary.org" + str + "</a>");
				}
			}
			else
			{
				if(responseFromServer.Contains("<tr class='head-row'>"))
				{
					string translation = StringParser.Parse("<tr class='head-row'>", "</table>", responseFromServer);
					StringParser parser = new StringParser(translation);
					string[] translations = parser.ReadItemsList("<tr class='data-row'>", "</tr>");
					Result subres = null;
					foreach(string str in translations)
					{
						string word = "";
						if(str.Contains("align=left valign=top nowrap>"))
							word = StringParser.Parse("align=left valign=top nowrap>", "<sup>", str).Trim();
						else 	
							word = StringParser.Parse("<td class='data-cell' align=left valign=top>", "<", 
								StringParser.Parse("<td class='data-cell' align=left valign=top>", "sup>", str)).Trim();
								
						string abbr = StringParser.Parse("<span class='feature'>", "</span>", str).Trim();
						translation  = StringParser.Parse("<td class='urdu-cell' align=right valign=top>", "<", str).Trim();
						
						if(translations.Length == 1)
							subres = result;
						else if(languagesPair.From == Language.Urdu)
						{
							subres = CreateNewResult(translation, languagesPair, subject);
							result.Childs.Add(subres);							
						}
						else
						{
							subres = CreateNewResult(word, languagesPair, subject);
							result.Childs.Add(subres);
						}
						
						subres.Abbreviation = abbr;
						if(languagesPair.From == Language.Urdu)
							subres.Translations.Add(word);
						else
							subres.Translations.Add(translation);
						
					}
				}

				//more 
				if(responseFromServer.Contains("Page-2.htm"))
				{
					query = "http://www.urduenglishdictionary.org/English-To-Urdu-Translation/{0}/Page-2.htm";
					if(languagesPair.From == Language.Urdu)
						query = "http://www.urduenglishdictionary.org/Urdu-To-English-Translation/{0}/Page-2.htm";
					
					query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase));
				
					string link = "html!<a href=\"{0}\">{1}</a>";
					link = string.Format(link,
						query,
						"More ...");
					Result subres = CreateNewResult(link, languagesPair, subject);
					result.Childs.Add(subres);
				}
				
				if(result.Childs.Count == 0 && result.Translations.Count == 0)
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
				
			}				
		}
	}

}
