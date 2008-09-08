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
	/// Description of SlovnykOrgDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Slovnyk")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class SlovnykOrgDictionary : BilingualDictionary
	{
		public SlovnykOrgDictionary()
		{
			langToKey.Add(Language.English, "en-us");
			langToKey.Add(Language.English_GB, "en-gb");
			langToKey.Add(Language.English_US, "en-us");
			langToKey.Add(Language.Belarusian, "be-by");
			langToKey.Add(Language.Bulgarian, "bg-bg");
			langToKey.Add(Language.Dutch, "nl-nl");
			langToKey.Add(Language.Greek, "el-gr");
			langToKey.Add(Language.Danish, "da-dk");
			langToKey.Add(Language.Esperanto, "eo-xx");
			langToKey.Add(Language.Estonian, "et-ee");
			langToKey.Add(Language.Icelandic, "is-is");
			langToKey.Add(Language.Spanish, "es-es");
			langToKey.Add(Language.Italian, "it-it");
			langToKey.Add(Language.Latvian, "lv-lv");
			langToKey.Add(Language.Latin, "la-va");
			langToKey.Add(Language.Lithuanian, "lt-lt");
			langToKey.Add(Language.Macedonian, "mk-mk");
			langToKey.Add(Language.German, "de-de");
			langToKey.Add(Language.Norwegian, "no-no");
			langToKey.Add(Language.Polish, "pl-pl");
			langToKey.Add(Language.Portuguese, "pt-pt");
			langToKey.Add(Language.Russian, "ru-ru");
			langToKey.Add(Language.Romanian, "ro-ro");
			langToKey.Add(Language.Serbian, "sr-rs");
			langToKey.Add(Language.Slovak, "sk-sk");
			langToKey.Add(Language.Slovenian, "sl-si");
			langToKey.Add(Language.Hungarian, "hu-hu");
			langToKey.Add(Language.Ukrainian, "uk-ua");
			langToKey.Add(Language.Finnish, "fi-fi");
			langToKey.Add(Language.French, "fr-fr");
			langToKey.Add(Language.Croatian, "hr-hr");
			langToKey.Add(Language.Czech, "cs-cz");
			langToKey.Add(Language.Swedish, "sv-se");
			
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				foreach(Language to in tmp.Keys)
				{
					if( (from != Language.English || (to != Language.English_GB && to != Language.English_US)) &&
						(to != Language.English || (from != Language.English_GB && from != Language.English_US)) &&
						from != to
					  )
					  AddSupportedTranslation(new LanguagePair(from, to));
				}
			}
			
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 64;
		
			IsQuestionMaskSupported = true;
			IsAsteriskMaskSupported = true;
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
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://slovnyk.org/fcgi-bin/dic.fcgi?iw={0}&hn=pre&il={1}&ol={2}&ul=en-us";
			query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase), ConvertLanguage(languagesPair.From), ConvertLanguage(languagesPair.To));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse().Trim();
			if(string.IsNullOrEmpty(responseFromServer))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				string translation = StringParser.Parse("<PRE>", "</PRE>",  responseFromServer).Trim();
				if(string.IsNullOrEmpty(translation))
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
				
				string subphrase, subtranslation;
				int startIdx = 0; 
				int newLineIdx = 0;
				int tabIdx = translation.IndexOf('\t', startIdx);
				bool firstRun = true;
				Result subres = result;
				
				while(tabIdx >= 0)
				{
					newLineIdx = translation.IndexOf('\n', startIdx);
					if(newLineIdx < 0)
						newLineIdx = translation.Length;
					subphrase = translation.Substring(startIdx, tabIdx - startIdx);
					subtranslation = translation.Substring(tabIdx + 1, newLineIdx - tabIdx - 1);
					startIdx = newLineIdx + 1;
					if(startIdx < translation.Length)
						tabIdx = translation.IndexOf('\t', startIdx);	
					else
						tabIdx = -1;
					if(firstRun && tabIdx < 0 && string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0)
					{
						result.Translations.Add(subtranslation);
						return;
					}
					
					if(firstRun)
					{
						subres = CreateNewResult(subphrase, languagesPair, subject);
						result.Childs.Add(subres);
					}	
						
					firstRun = false;
					
					if(string.Compare(subphrase, subres.Phrase, true, CultureInfo.InvariantCulture) !=0)
					{
						subres = CreateNewResult(subphrase, languagesPair, subject);
						result.Childs.Add(subres);
					}	
					

					subres.Translations.Add(subtranslation);
				}
			}
			
		}
		
	}
}
