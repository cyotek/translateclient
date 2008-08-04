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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	/// <summary>
	/// Description of YahooTranslator.
	/// </summary>
	public class YahooTranslator : Translator
	{
		static YahooTranslator()
		{
			langToKey.Add(Language.Chinese,"zh");
			langToKey.Add(Language.Chinese_CN,"zh");
			langToKey.Add(Language.Chinese_TW,"zt");
			langToKey.Add(Language.Dutch,"nl");
			langToKey.Add(Language.English,"en");
			langToKey.Add(Language.English_US,"en");
			langToKey.Add(Language.English_GB,"en");
			langToKey.Add(Language.French,"fr");
			langToKey.Add(Language.German,"de");
			langToKey.Add(Language.Greek,"el");
			langToKey.Add(Language.Italian,"it");
			langToKey.Add(Language.Japanese,"ja");
			langToKey.Add(Language.Korean,"ko");
			langToKey.Add(Language.Portuguese,"pt");
			langToKey.Add(Language.Russian,"ru");
			langToKey.Add(Language.Spanish,"es");
		}
		
		public YahooTranslator()
		{
			AddSupportedTranslationToEnglish(Language.Chinese);
			AddSupportedTranslationToEnglish(Language.Chinese_CN);
			
			AddSupportedTranslation(new LanguagePair(Language.Chinese_CN, Language.Chinese_TW));
			AddSupportedTranslation(new LanguagePair(Language.Chinese, Language.Chinese_TW));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.Chinese_CN));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.Chinese));
			
			AddSupportedTranslationToEnglish(Language.Chinese_TW);
			
			AddSupportedTranslationFromEnglish(Language.Chinese);
			AddSupportedTranslationFromEnglish(Language.Chinese_CN);
			AddSupportedTranslationFromEnglish(Language.Chinese_TW);

			AddSupportedTranslationFromEnglish(Language.Dutch);

			AddSupportedTranslationFromEnglish(Language.French);

			AddSupportedTranslationFromEnglish(Language.German);

			AddSupportedTranslationFromEnglish(Language.Greek);

			AddSupportedTranslationFromEnglish(Language.Italian);

			AddSupportedTranslationFromEnglish(Language.Japanese);

			AddSupportedTranslationFromEnglish(Language.Korean);

			AddSupportedTranslationFromEnglish(Language.Portuguese);

			AddSupportedTranslationFromEnglish(Language.Russian);

			AddSupportedTranslationFromEnglish(Language.Spanish);
			
			AddSupportedTranslationToEnglish(Language.Dutch);
			
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Dutch));
			
			AddSupportedTranslationToEnglish(Language.French);

			AddSupportedTranslation(new LanguagePair(Language.French, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Greek));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Spanish));

			AddSupportedTranslationToEnglish(Language.German);
			
			AddSupportedTranslation(new LanguagePair(Language.German, Language.French));
			
			AddSupportedTranslationToEnglish(Language.Italian);
			
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.French));

			AddSupportedTranslationToEnglish(Language.Japanese);

			AddSupportedTranslationToEnglish(Language.Korean);

			AddSupportedTranslationToEnglish(Language.Portuguese);

			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.French));

			AddSupportedTranslationToEnglish(Language.Russian);

			AddSupportedTranslationToEnglish(Language.Spanish);
			
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.French));
		
			AddSupportedSubject(SubjectConstants.Common);
			WordsCount = 150;
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

		public static string ConvertTranslatorLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			return ConvertLanguage(languagesPair.From) + "_" + ConvertLanguage(languagesPair.To);
		}
		
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://babelfish.yahoo.com/translate_txt"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			helper.AcceptCharset = "utf-8";		
			
			//query
			//ei=UTF-8&doit=done&fr=bf-res&intl=1&tt=urltext&trtext=test+it&lp=en_ru&btnTrTxt=Translate


			string langpair= ConvertTranslatorLanguagesPair(languagesPair);
			string query = "ei=UTF-8&doit=done&fr=bf-res&intl=1&tt=urltext&trtext=" + 
				HttpUtility.UrlEncode(phrase, System.Text.Encoding.UTF8 ) + 
				"&lp=" + langpair + 
				"&btnTrTxt=Translate";
				
			helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
			result.Translations.Add(StringParser.Parse("<div style=\"padding:0.6em;\">", "</div>", responseFromServer));
		}
	}
}
