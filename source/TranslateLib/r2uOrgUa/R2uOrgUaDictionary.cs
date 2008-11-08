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
 * Portions created by the Initial Developer are Copyright (C) 2007-2008
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
	/// Description of R2uOrgUaDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class R2uOrgUADictionary : BilingualDictionary
	{
		public R2uOrgUADictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Ukrainian));
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Russian));
			AddSupportedSubject(SubjectConstants.Common);
			
			IsQuestionMaskSupported = true;
			IsAsteriskMaskSupported = true;
			
			CharsLimit = 64;
			
			WordsCount = 95000;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//query = string.Format(query, HttpUtility.UrlEncode(phrase, Encoding.GetEncoding(1251)));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://r2u.org.ua/krym/krym_search.php"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			string query = "word_str={0}&type={1}";
			
			//types
			//"all" - all words
			//"rusb" - general rus words
			//"rus" - all rus words
			//"ukr" - ukr words without quotations
			//"ukrq" - all ukr words
			if(languagesPair.From == Language.Russian)
				query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase), "rus");
			else
				query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase), "ukrq");
			helper.AddPostData(query);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.IndexOf("нічого не знайдено!</h3>") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else if(responseFromServer.IndexOf("Запит сприймає лише кирилицю") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Only cyrillic characters supported");
			}
			else if(responseFromServer.IndexOf("українські літери в запиті по російських позиціях") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Ukrainian characters in russian query");
			}
			else if(responseFromServer.IndexOf("нічого не знайдено!</h4>") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				result.ArticleUrl = "http://r2u.org.ua/krym/krym_search.php?" + query;
				result.ArticleUrlCaption = phrase;
			
				string translation = StringParser.Parse("<table class=\"main_tbl\">", "</table>", responseFromServer);
				translation = translation.Replace("</span>", "");
				translation = translation.Replace("<span class=\"KrymUkr\">", "");
				translation = translation.Replace("<span class=\"KrymRusBold\">", "");
				translation = translation.Replace("<span class=\"KrymRusItalic\">", "");
				translation = translation.Replace("<span class=\"KrymRusItalic\">", "");
				translation = translation.Replace("<span class=\"KrymUkAux\">", "");
				translation = translation.Replace("<span class=\"KrymLat\">", "");
				translation = StringParser.RemoveAll("<a href=\"?word_str=", "\">", translation);
				translation = StringParser.RemoveAll("<b title=\"", "</a></b>", translation);
				translation = translation.Replace("</a>", "");
				translation = translation.Replace("<u class=\"quote\">", "");
				translation = translation.Replace("</u>", "");
				translation = translation.Replace("<span class=\"num\">", "");
				
				
 				

				
				StringParser parser = new StringParser(translation);
				string[] translations = parser.ReadItemsList("<tr><td class=\"result_row", "</td></tr>", "787654323");
				
				int idx = 0;
				string subpart;
				string subphrase;
				foreach(string part in translations)
				{
					subpart = part;
					if(subpart.StartsWith("\">"))
						subpart = subpart.Substring(2);
					else
						subpart = subpart.Substring(8);
						
					int idxOfMinus = subpart.IndexOf("–");
					if(idxOfMinus > 0)
					{
						subphrase = subpart.Substring(0, idxOfMinus).Trim();	
						subpart = subpart.Substring(idxOfMinus + 1).Trim();	
					}
					else
					{
						subphrase = subpart;	
						subpart = "";	
					}
					
					if(idx == 0 && string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0 && translations.Length == 1)
					{
						//single answer
						result.Translations.Add(subpart);
						return;
					}
					
					Result subres = CreateNewResult(subphrase, languagesPair, subject);
					subres.Translations.Add(subpart);
					result.Childs.Add(subres);
				
					idx++;
				}
				
			}				
		}
	}

}
