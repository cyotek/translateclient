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
	public class ProlingofficeTranslator : Translator
	{
		public ProlingofficeTranslator()
		{
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Ukrainian));
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Russian));
			
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 1000;
			Name = "_translator";
		}

		static string viewState;
		static string eventValidation;
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			if(string.IsNullOrEmpty(viewState))
			{  //emulate first access to site
				WebRequestHelper helpertop = 
					new WebRequestHelper(result, new Uri("http://www.prolingoffice.com/page.aspx?l1=28"), 
						networkSetting, 
						WebRequestContentType.UrlEncodedGet, 
						Encoding.GetEncoding(1251));
						
				string responseFromServertop = helpertop.GetResponse();
				viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServertop);
				eventValidation = StringParser.Parse("id=\"__EVENTVALIDATION\" value=\"", "\"", responseFromServertop);
			}
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.prolingoffice.com/page.aspx?l1=28"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded, 
						Encoding.GetEncoding(1251));
						
			//query
			string langDirection = "RU";
			if(languagesPair.From == Language.Ukrainian)
				langDirection = "UR";
			//templ=%CD%E5%F2+%EF%E5%F0%E5%E2%EE%E4%E0&
			//__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&_ctl1%3Asource={1}
			//&_ctl1%3Alng={2}&_ctl1%3AButton1=%CF%E5%F0%E5%E2%E5%F1%F2%E8&
			//_ctl1%3Aathbox=&_ctl1%3Amailbox=&_ctl1%3Aerr_f=&_ctl1%3Aadd_txt=&
			//_ctl1%3Acb_forum=on&LanguageH=RUS&__EVENTVALIDATION={3}
			string query = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&_ctl1%3Asource={1}&_ctl1%3Alng={2}&_ctl1%3AButton1=%CF%E5%F0%E5%E2%E5%F1%F2%E8&_ctl1%3Aathbox=&_ctl1%3Amailbox=&_ctl1%3Aerr_f=&_ctl1%3Aadd_txt=&_ctl1%3Acb_forum=on&LanguageH=RUS&__EVENTVALIDATION={3}";
			query = string.Format(query, 
				HttpUtility.UrlEncode(viewState, helper.Encoding),
				HttpUtility.UrlEncode(phrase, helper.Encoding),
				langDirection,
				HttpUtility.UrlEncode(eventValidation, helper.Encoding));
				
			helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
		
			string translation = StringParser.Parse("onclickk=\"shword();\">", "</DIV>", responseFromServer);
			translation = translation.Replace("<font color=red>", "");
			translation = translation.Replace("</font>", "");
			
			result.Translations.Add(translation);
			viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServer);
			eventValidation = StringParser.Parse("id=\"__EVENTVALIDATION\" value=\"", "\"", responseFromServer);
			
		}
	}
}
