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
			CharsLimit = 15000;
			WordsLimit = 500;
			Name = "_translator";

			AddSupportedSubject(SubjectConstants.Common);
			
			LanguageCollection tmp = new LanguageCollection();
			LanguageCollection languages = new LanguageCollection();
			foreach(KeyValuePair<Language, string> kvp in langToKey)
			{
				if(kvp.Key != Language.English_GB && kvp.Key != Language.English_US)
				{
					tmp.Add(kvp.Key);
					languages.Add(kvp.Key);
				}	
			}
			
			foreach(Language from in languages)
			{
				foreach(Language to in tmp)
				{
					if(from != to && to != Language.Autodetect)
					{
						if(from == Language.English)
						{
							AddSupportedTranslationFromEnglish(to);
						}
						else if(to == Language.English)
						{
							AddSupportedTranslationToEnglish(from);
						}
						else
						{
							AddSupportedTranslation(from, to);
						}	
					}
				}
			}
			
		}
		
		static LiveTranslator()
		{
			langToKey.Add(Language.Autodetect, "");
			langToKey.Add(Language.Arabic, "ar");
			langToKey.Add(Language.Bulgarian, "bg");
			langToKey.Add(Language.Chinese_CN, "zh-CHS");
			langToKey.Add(Language.Chinese_TW, "zh-CHT");
			langToKey.Add(Language.Czech, "cs");
			langToKey.Add(Language.Danish, "da");
			langToKey.Add(Language.Dutch, "nl");
			langToKey.Add(Language.English, "en");
			langToKey.Add(Language.English_GB, "en");
			langToKey.Add(Language.English_US, "en");
			langToKey.Add(Language.Finnish, "fi");
			langToKey.Add(Language.French, "fr");			
			langToKey.Add(Language.German, "de");
			langToKey.Add(Language.Greek, "el");
			langToKey.Add(Language.Hebrew, "he");
			langToKey.Add(Language.Hungarian, "hu");
			langToKey.Add(Language.Italian, "it");
			langToKey.Add(Language.Japanese, "ja");
			langToKey.Add(Language.Korean, "ko");
			langToKey.Add(Language.Lithuanian, "lt");
			langToKey.Add(Language.Norwegian, "no");
			langToKey.Add(Language.Polish, "pl");
			langToKey.Add(Language.Portuguese, "pt");
			langToKey.Add(Language.Romanian, "ro");
			langToKey.Add(Language.Russian, "ru");
			langToKey.Add(Language.Slovak, "sk");
			langToKey.Add(Language.Slovenian, "sl");
			langToKey.Add(Language.Spanish, "es");
			langToKey.Add(Language.Swedish, "sv");
			langToKey.Add(Language.Thai, "th");
			langToKey.Add(Language.Turkish, "tr");
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
						new WebRequestHelper(result, new Uri("http://www.microsofttranslator.com/Default.aspx"), 
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
				new WebRequestHelper(result, new Uri("http://www.microsofttranslator.com/Default.aspx"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
						
			//query
			lock(viewState)
			{
			string query = "__VIEWSTATE={0}&__EVENTVALIDATION={1}&InputTextVal={2}&InputTextInit=&BrowserLanguagePreference=ru&MaxInputChars=15000&LangPair_FromDDL_svid={3}&LangPair_FromDDL_textid=&LangPair_ToDDL_svid={4}&LangPair_ToDDL_textid=&LangPair%24FromLangLAD=&BtnTransText=%D0%9F%D0%B5%D1%80%D0%B5%D0%B2%D0%B5%D1%81%D1%82%D0%B8&LiveTranslationPostURL=http%3A%2F%2Flivetranslation.com%2Faff%2Fpartner.aspx&LiveTranslationPartnerID=500&LiveTranslationLanguagePairs=";
			query = string.Format(query, 
				HttpUtility.UrlEncode(viewState),
				HttpUtility.UrlEncode(eventValidation),
				HttpUtility.UrlEncode(phrase),
				ConvertLanguage(languagesPair.From),
				ConvertLanguage(languagesPair.To)
				);
				helper.AddPostData(query);
				
				cookieContainer.Add(new Uri("http://www.microsofttranslator.com"), new Cookie("from", ConvertLanguage(languagesPair.From)));
				cookieContainer.Add(new Uri("http://www.microsofttranslator.com"), new Cookie("to", ConvertLanguage(languagesPair.To)));
				helper.CookieContainer = cookieContainer;
			}	
			
			string responseFromServer = helper.GetResponse();
		
			string translation = StringParser.Parse("<textarea name=\"OutputText\"", "</textarea>", responseFromServer);
			translation = StringParser.ExtractRight(">", translation);
			
			result.Translations.Add(translation);
			lock(viewState)
			{
				viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServer);
				eventValidation = StringParser.Parse("id=\"__EVENTVALIDATION\" value=\"", "\"", responseFromServer);
			}
			
		}
	}
}
