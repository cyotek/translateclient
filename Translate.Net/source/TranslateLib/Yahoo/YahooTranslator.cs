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
			AddSupportedTranslation(new LanguagePair(Language.Chinese, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_CN, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Chinese, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_CN, Language.English_US));
			
			AddSupportedTranslation(new LanguagePair(Language.Chinese_CN, Language.Chinese_TW));
			AddSupportedTranslation(new LanguagePair(Language.Chinese, Language.Chinese_TW));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.Chinese_CN));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.Chinese));
			
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.English_US));
			
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese_CN));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese_TW));

			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Chinese));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Chinese_CN));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Chinese_TW));

			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Chinese));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Chinese_CN));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Chinese_TW));
			
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Dutch));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Dutch));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Dutch));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.French));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.French));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.French));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.German));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Greek));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Greek));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Greek));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Italian));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Japanese));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Japanese));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Japanese));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Korean));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Korean));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Korean));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Portuguese));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Russian));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Russian));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Spanish));
			AddSupportedTranslation(new LanguagePair(Language.English_GB, Language.Spanish));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.Spanish));
			
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.English_US));
			
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Dutch));
			
			AddSupportedTranslation(new LanguagePair(Language.French, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.English_US));

			AddSupportedTranslation(new LanguagePair(Language.French, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Greek));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Spanish));

			AddSupportedTranslation(new LanguagePair(Language.German, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.English_US));
			
			AddSupportedTranslation(new LanguagePair(Language.German, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.English_US));
			
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.French));

			AddSupportedTranslation(new LanguagePair(Language.Japanese, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Japanese, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Japanese, Language.English_US));

			AddSupportedTranslation(new LanguagePair(Language.Korean, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Korean, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Korean, Language.English_US));

			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.English_US));

			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.French));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English_US));

			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.English_GB));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.English_US));
			
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.French));
		
			AddSupportedSubject(SubjectConstants.Common);
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
