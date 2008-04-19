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
	/// Description of SystranTranslator.
	/// </summary>
	public class SystranTranslator : Translator
	{
		public SystranTranslator()
		{
		
			langToKey.Add(Language.Arabic, "ar");
			langToKey.Add(Language.Chinese, "zh");
			langToKey.Add(Language.Dutch, "nl");
			langToKey.Add(Language.English, "en");
			langToKey.Add(Language.French, "fr");
			langToKey.Add(Language.German, "de");
			langToKey.Add(Language.Greek, "el");
			langToKey.Add(Language.Italian, "it");
			langToKey.Add(Language.Japanese, "ja");
			langToKey.Add(Language.Korean, "ko");
			langToKey.Add(Language.Polish, "pl");
			langToKey.Add(Language.Portuguese, "pt");
			langToKey.Add(Language.Russian, "ru");
			langToKey.Add(Language.Spanish, "es");
			langToKey.Add(Language.Swedish, "sv");

			AddSupportedTranslation(new LanguagePair(Language.Arabic, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Chinese, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Arabic));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.French));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Dutch));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Greek));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Japanese));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Korean));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Russian));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Spanish));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Swedish));
			
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Dutch));			
			AddSupportedTranslation(new LanguagePair(Language.French, Language.English));			
			AddSupportedTranslation(new LanguagePair(Language.French, Language.German));	
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Greek));	
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Italian));	
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Portuguese));	
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Spanish));	

			AddSupportedTranslation(new LanguagePair(Language.German, Language.English));	
			AddSupportedTranslation(new LanguagePair(Language.German, Language.French));	
			
			AddSupportedTranslation(new LanguagePair(Language.Greek, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Greek, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.Japanese, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Korean, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Polish, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.French));
			
			AddSupportedTranslation(new LanguagePair(Language.Swedish, Language.English));
			
			AddSupportedSubject(SubjectConstants.Common);
			
			//CharsLimit = 1000;
		}
		
		SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		string ConvertLanguage(Language language)
		{
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}
		
		 string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			return ConvertLanguage(languagesPair.From) + "_" + ConvertLanguage(languagesPair.To);
		}
		
		
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www2.systranbox.com/sai?gui=sbox/normal/systran/systranEN&lp={0}&service=translate";
			query = string.Format(query, ConvertLanguagesPair(languagesPair));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			
			helper.AddPostData(phrase);
			
			string responseFromServer = helper.GetResponse();
			
			string status = responseFromServer.Substring(6);
			result.Translations.Add(status);
			/*
			if(status != "2")
			{
				throw new TranslationException(responseFromServer.Substring(10));
			}
			else
			{	if(responseFromServer.Substring(17) == "Translation direction is not correct")
					throw new TranslationException("Translation direction is not correct");
				result.Translations.Add(responseFromServer.Substring(17));
			}
			*/
		}
	}
}
