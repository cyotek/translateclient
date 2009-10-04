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
 * Portions created by the Initial Developer are Copyright (C) 2009
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
	/// Description of SaDirBgDictionary.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class SederetDictionary : BilingualDictionary
	{
		public SederetDictionary()
		{
			AddSupportedTranslationFromEnglish(Language.Indonesian);
			AddSupportedTranslationToEnglish(Language.Indonesian);
			AddSupportedSubject(SubjectConstants.Common);
			
			Name = "_dictionary";
			
			WordsLimit = 5;
			CharsLimit = 50;
		}
		
		static Encoding encoding = Encoding.GetEncoding(1252);
		
		static string sederetCode = "";
		static CookieContainer cookieContainer = new CookieContainer();
		static DateTime coockieTime = DateTime.Now.AddHours(-5);
		
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			List<string> words = StringParser.SplitToWords(phrase);
			foreach(string word in words)
			{
				TranslateWord(word, languagesPair, subject, result, networkSetting);
			}
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
 	 	void TranslateWord(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			lock(sederetCode)
			{
				if(string.IsNullOrEmpty(sederetCode) || coockieTime < DateTime.Now.AddHours(-1))
				{  //emulate first access to site
					WebRequestHelper helpertop = 
						new WebRequestHelper(result, new Uri("http://www.sederet.com/translate.php"), 
							networkSetting, 
							WebRequestContentType.UrlEncodedGet, encoding);
					helpertop.CookieContainer = cookieContainer;
					coockieTime = DateTime.Now;
					string responseFromServertop = helpertop.GetResponse();
					sederetCode = StringParser.Parse("<input type=\"hidden\" name=\"var\" value=\"", "\"", responseFromServertop);
				}
			}
			
			
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.sederet.com/translate.php"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded, encoding);
			helper.CookieContainer = cookieContainer;
			
			//eng2indo, indo2eng
			string query = "from_to={0}&kata={1}&var={2}";
			
			if(languagesPair.From == Language.Indonesian)
				query = string.Format(CultureInfo.InvariantCulture, query, "indo2eng", HttpUtility.UrlEncode(phrase), sederetCode);
			else
				query = string.Format(CultureInfo.InvariantCulture, query, "eng2indo", HttpUtility.UrlEncode(phrase), sederetCode);
			helper.AddPostData(query);

			string responseFromServer = helper.GetResponse();
			
			lock(sederetCode)
			{
				sederetCode = StringParser.Parse("<input type=\"hidden\" name=\"var\" value=\"", "\"", responseFromServer);			
			}	
			
			string translation = StringParser.Parse("<span id=\"result_title\"", "id=\"part_right\">", responseFromServer);
			
			Result child;
			StringParser subparser;
			if(translation.Contains("<b id=\"match_title\">Exact Match:</b>"))
			{
				child = CreateNewResult(phrase, languagesPair, subject);
				result.Childs.Add(child);
				string subblock = StringParser.Parse("<b id=\"match_title\">Exact Match:</b>", "</table>", translation); 
				subparser = new StringParser(subblock);
				string[] subtranslation_list = subparser.ReadItemsList("<td id='result_td'>", "</td>");
				for(int i = 0; i < subtranslation_list.Length; i+=2)
				{
					string subtranslation = subtranslation_list[i+1];
					subtranslation = StringParser.RemoveAll("<", ">", subtranslation);
					child.Translations.Add(HttpUtility.HtmlDecode(subtranslation));
				}
			}
			
			if(translation.Contains("<b id=\"match_title\">Other Match(es):</b>"))
			{
				string subblock = StringParser.Parse("<b id=\"match_title\">Other Match(es):</b>", "</table>", translation); 
				subparser = new StringParser(subblock);
				string[] subtranslation_list = subparser.ReadItemsList("<td id='result_td'>", "</td>");
				for(int i = 0; i < subtranslation_list.Length; i+=2)
				{
					string subphrase = subtranslation_list[i];
					subphrase = StringParser.RemoveAll("<", ">", subphrase);

					child = CreateNewResult(subphrase, languagesPair, subject);
					result.Childs.Add(child);
					
					string subtranslation = subtranslation_list[i+1];
					subtranslation = StringParser.RemoveAll("<", ">", subtranslation);
					child.Translations.Add(HttpUtility.HtmlDecode(subtranslation));
				}
			}
			
			result.ResultNotFound = result.Childs.Count == 0;
			
		}
		
	}
}
