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

namespace Translate
{
	/// <summary>
	/// Description of LiveTranslator.
	/// </summary>
	public class LiveTranslator: Translator
	{
		public LiveTranslator()
		{
			CharsLimit = 3500;
			WordsLimit = 500;
			Name = "_translator";

			AddSupportedSubject(SubjectConstants.Common);
			
			AddSupportedTranslationToEnglish(Language.Arabic);
			AddSupportedTranslation(Language.Chinese_CN, Language.Chinese_TW);
			AddSupportedTranslationToEnglish(Language.Chinese_CN);
			AddSupportedTranslation(Language.Chinese_TW, Language.Chinese_CN);
			AddSupportedTranslationToEnglish(Language.Chinese_TW);
			AddSupportedTranslationToEnglish(Language.Dutch);
			AddSupportedTranslationFromEnglish(Language.Arabic);
			AddSupportedTranslationFromEnglish(Language.Chinese_CN);
			AddSupportedTranslationFromEnglish(Language.Chinese_TW);
			AddSupportedTranslationFromEnglish(Language.Dutch);
			AddSupportedTranslationFromEnglish(Language.French);
			AddSupportedTranslationFromEnglish(Language.German);
			AddSupportedTranslationFromEnglish(Language.Italian);
			AddSupportedTranslationFromEnglish(Language.Japanese);
			AddSupportedTranslationFromEnglish(Language.Korean);
			AddSupportedTranslationFromEnglish(Language.Portuguese);
			AddSupportedTranslationFromEnglish(Language.Spanish);
			AddSupportedTranslationToEnglish(Language.French);
			AddSupportedTranslationToEnglish(Language.German);
			AddSupportedTranslationToEnglish(Language.Italian);
			AddSupportedTranslationToEnglish(Language.Japanese);
			AddSupportedTranslationToEnglish(Language.Korean);
			AddSupportedTranslationToEnglish(Language.Portuguese);
			AddSupportedTranslationToEnglish(Language.Russian);
			AddSupportedTranslationToEnglish(Language.Spanish);
		}
		
		static LiveTranslator()
		{
			langToKey.Add(Language.English, "en");
			langToKey.Add(Language.English_GB, "en");
			langToKey.Add(Language.English_US, "en");
			langToKey.Add(Language.Chinese_CN, "zh-chs");
			langToKey.Add(Language.Chinese_TW, "zh-cht");
			langToKey.Add(Language.Dutch, "nl");
			langToKey.Add(Language.Arabic, "ar");
			langToKey.Add(Language.French, "fr");			
			langToKey.Add(Language.German, "de");
			langToKey.Add(Language.Italian, "it");
			langToKey.Add(Language.Japanese, "ja");
			langToKey.Add(Language.Korean, "ko");
			langToKey.Add(Language.Portuguese, "pt");
			langToKey.Add(Language.Russian, "ru");
			langToKey.Add(Language.Spanish, "es");
		}
		
		static SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		public static string ConvertLanguage(Language language)
		{
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}
		
		public static string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			string result =  ConvertLanguage(languagesPair.From) + "_" + 
				ConvertLanguage(languagesPair.To);
			return result;	
		}
		
		static string viewState = "";
		static string eventValidation = "";
		static CookieContainer cookieContainer = new CookieContainer();
		static DateTime coockieTime = DateTime.Now.AddHours(-5);
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			lock(viewState)
			{
				if(string.IsNullOrEmpty(viewState) || coockieTime < DateTime.Now.AddMinutes(-30))
				{  //emulate first access to site
					WebRequestHelper helpertop = 
						new WebRequestHelper(result, new Uri("http://www.windowslivetranslator.com/Default.aspx"), 
							networkSetting, 
							WebRequestContentType.UrlEncodedGet);
							
					helpertop.CookieContainer = cookieContainer;
					string responseFromServertop = helpertop.GetResponse();
					viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServertop);
					eventValidation = StringParser.Parse("id=\"__EVENTVALIDATION\" value=\"", "\"", responseFromServertop);
					coockieTime = DateTime.Now;
				}
			}
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.windowslivetranslator.com/Default.aspx"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
						
			//query
			lock(viewState)
			{
			string query = "__VIEWSTATE={0}&InputTextVal={1}&BrowserLanguagePreference=en&MaxInputChars=3500&LiveTranslationPostURL=http%3A%2F%2Flivetranslation.com%2Faff%2Fpartner.aspx&LiveTranslationPartnerID=500&LiveTranslationLanguagePairs=en_eses_enen_dede_enen_itit_enen_frfr_enen_ptpt_en&InputURL=http%3A%2F%2F&LangPair%24DDL={2}&BtnTransText=Translate+Text&__EVENTVALIDATION={3}";
			query = string.Format(query, 
				HttpUtility.UrlEncode(viewState),
				HttpUtility.UrlEncode(phrase),
				ConvertLanguagesPair(languagesPair),
				HttpUtility.UrlEncode(eventValidation));
				helper.AddPostData(query);
				
				cookieContainer.Add(new Uri("http://www.windowslivetranslator.com"), new Cookie("lp", ConvertLanguagesPair(languagesPair)));
				helper.CookieContainer = cookieContainer;
			}	
			
			string responseFromServer = helper.GetResponse();
		
			string translation = StringParser.Parse("id=\"OutputText\" class=\"mttextarea\">", "</textarea>", responseFromServer);
			
			result.Translations.Add(translation);
			lock(viewState)
			{
				viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServer);
				eventValidation = StringParser.Parse("id=\"__EVENTVALIDATION\" value=\"", "\"", responseFromServer);
			}
			
		}
	}
}
