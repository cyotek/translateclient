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

namespace Translate
{
	/// <summary>
	/// Description of ProlingofficeTranslator.
	/// </summary>
	public class ProlingofficeDictionary : BilingualDictionary
	{
		public ProlingofficeDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Ukrainian));
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Russian));
			
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 255;
			Name = "_dictionary";
		}

		static string viewState;
		static string eventValidation;
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			if(string.IsNullOrEmpty(viewState))
			{  //emulate first access to site
				WebRequestHelper helpertop = 
					new WebRequestHelper(result, new Uri("http://www.prolingoffice.com/page.aspx?l1=43"), 
						networkSetting, 
						WebRequestContentType.UrlEncodedGet, 
						Encoding.GetEncoding(1251));
						
				string responseFromServertop = helpertop.GetResponse();
				viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServertop);
				eventValidation = StringParser.Parse("id=\"__EVENTVALIDATION\" value=\"", "\"", responseFromServertop);
			}
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.prolingoffice.com/page.aspx?l1=43"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded, 
						Encoding.GetEncoding(1251));
						
			//query
			string langDirection = "R";
			if(languagesPair.From == Language.Ukrainian)
				langDirection = "U";
			//__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}
			//&q={1}
			//&_ctl1%3AButton1=%CF%E5%F0%E5%E2%E5%F1%F2%E8&_ctl1%3AtsLang=rbLang{2}&LanguageH=RUS
			//&__EVENTVALIDATION={3}
			string query = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&q={1}&_ctl1%3AButton1=%CF%E5%F0%E5%E2%E5%F1%F2%E8&_ctl1%3AtsLang=rbLang{2}&LanguageH=RUS&__EVENTVALIDATION={3}";
			query = string.Format(query, 
				HttpUtility.UrlEncode(viewState, helper.Encoding),
				HttpUtility.UrlEncode(phrase, helper.Encoding),
				langDirection,
				HttpUtility.UrlEncode(eventValidation, helper.Encoding));
				
			helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
		
			if(responseFromServer.IndexOf("Перекладу цього слова не знайдено. Спробуйте записати слово інакше, або ознайомтеся з інформацією, яка міститься у <a") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else if(responseFromServer.IndexOf("В слове содержатся ошибки. Возможно имелось в виду:</b>") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
		
			
			string translation = StringParser.Parse("<span class=\"wrd\" xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\">", "</td></tr>", responseFromServer);
			string abbr = StringParser.Parse("title=\"", "\"", translation);
			abbr += " " + StringParser.Parse("xslt\">", "</span>", translation).Trim();
			//result.Abbreviation = abbr;
			
			StringParser parser = new StringParser(translation);
			string[] translations = parser.ReadItemsList("<li style=\"margin-bottom:10\">", "</li>", "3495783-4572385");
			
			foreach(string subtranslation in translations)
			{
				if(subtranslation.IndexOf("span class=\"tolkm\">") >= 0)
					translation = StringParser.Parse("span class=\"tolkm\">", "</span>", subtranslation);
				else if(subtranslation.IndexOf("<span class=\"flag1\"") >= 0)
					translation = StringParser.Parse("<span class=\"flag1\" title=\"", "\"", subtranslation);
				else 
					translation = "";
				Result subres;
				if(translations.Length > 1)
				{
					subres = CreateNewResult(translation, languagesPair, subject);
					result.Childs.Add(subres);
				}
				else 
					subres = result;
				
				parser = new StringParser(subtranslation);
				string[] subtranslations = parser.ReadItemsList("<span class=\"trwrd\"", "/span>", "3495783-4572385");
				foreach(string s in subtranslations)
					subres.Translations.Add(StringParser.Parse(">", "<", s));
			}
			
			viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServer);
			eventValidation = StringParser.Parse("id=\"__EVENTVALIDATION\" value=\"", "\"", responseFromServer);
			
		}
	}
}
