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
	/// Description of SeznamCzDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class SeznamCzDictionary : BilingualDictionary
	{
		static SeznamCzDictionary() 
		{
			langToKey.Add(Language.English, "en");
			langToKey.Add(Language.Czech, "cz");
			langToKey.Add(Language.German, "de");
			langToKey.Add(Language.French, "fr");
			langToKey.Add(Language.Italian, "it");
			langToKey.Add(Language.Spanish, "es");
			langToKey.Add(Language.Russian, "ru");
		}
		
		public SeznamCzDictionary()
		{
		
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				if(from != Language.Czech)
				{
					if(from != Language.English)
					{
						AddSupportedTranslation(from, Language.Czech);
						AddSupportedTranslation(Language.Czech, from);
					}	
					else	
					{
						AddSupportedTranslationFromEnglish(Language.Czech);
						AddSupportedTranslationToEnglish(Language.Czech);
					}	
				}
			}
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 50;
		}

		static SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		static string ConvertLanguage(Language language)
		{
			if(language == Language.English_GB || language == Language.English_US)
				language = Language.English;
				
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}

		static string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			return ConvertLanguage(languagesPair.From) + "_" + ConvertLanguage(languagesPair.To);
		}

		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//http://slovnik.seznam.cz/?q=test&lang=en_cz
			string query = "http://slovnik.seznam.cz/?q={0}&lang={1}";
			query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase), ConvertLanguagesPair(languagesPair));
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.Contains("</strong>&quot; nebylo ve Slovníku nic nalezeno.</h2>"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				result.ArticleUrl = query;
				result.ArticleUrlCaption = phrase;
			
				//translation by self
				if(responseFromServer.Contains("<table id=\"words\">"))
				{
					string translation = StringParser.Parse("<table id=\"words\">", "</table>", responseFromServer);
					StringParser parser = new StringParser(translation);
					string[] translations = parser.ReadItemsList("<tr>", "</tr>");
					Result subres = null;
					foreach(string str in translations)
					{
						string word = StringParser.Parse(">", "<", StringParser.Parse("<a", "/a>", str));
						subres = CreateNewResult(word, languagesPair, subject);
						result.Childs.Add(subres);
						
						StringParser subparser = new StringParser("<td class=\"translated\">", "</td>", str);
						string[] subtranslations = subparser.ReadItemsList("<a", "/a>") ;
						foreach(string sub_str in subtranslations)
						{
							if(!sub_str.Contains("<img src"))
								subres.Translations.Add(StringParser.Parse(">", "<", sub_str));
						}	
					}
				}

				//phrases
				if(responseFromServer.Contains("<div id=\"collocations\">"))
				{
					string translation = StringParser.Parse("<div id=\"collocations\">", "</div>", responseFromServer);
					StringParser parser = new StringParser(translation);
					string[] translations = parser.ReadItemsList("<dt>", "</dd>");
					Result subres = null;
					foreach(string str in translations)
					{
						string word = StringParser.Parse(">", "<", StringParser.Parse("<a", "/a>", str));
						subres = CreateNewResult(word, languagesPair, subject);
						result.Childs.Add(subres);
						
						StringParser subparser = new StringParser("<dd>", "</dd>", str + "</dd>");
						string[] subtranslations = subparser.ReadItemsList("<a", "/a>") ;
						foreach(string sub_str in subtranslations)
						{
								subres.Translations.Add(StringParser.Parse(">", "<", sub_str));
						}	
						
						
					}
					
					if(responseFromServer.Contains("&amp;from=31\">2</a></span>"))
					{ //more phrases
					
						string link = "html!<p><a href=\"{0}\" title=\"{0}\">{1}</a></p>";
						link = string.Format(link,
							query + "&from=31",
							"More phrases ...");
						subres = CreateNewResult(link, languagesPair, subject);
						result.Childs.Add(subres);
					}
				}
				
				if(result.Childs.Count == 0)
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}

			}				
		}
	}

}
