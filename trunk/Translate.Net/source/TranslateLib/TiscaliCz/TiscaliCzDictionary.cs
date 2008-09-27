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
	/// Description of TiscaliCzDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class TiscaliCzDictionary : BilingualDictionary
	{
		static TiscaliCzDictionary() 
		{
			langToKey.Add(Language.English, "a");
			langToKey.Add(Language.Czech, "c");
			langToKey.Add(Language.German, "n");
			langToKey.Add(Language.French, "f");
			langToKey.Add(Language.Italian, "i");
			langToKey.Add(Language.Spanish, "s");
			langToKey.Add(Language.Russian, "r");
		}
		
		public TiscaliCzDictionary()
		{
		
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				if(from != Language.Czech)
				{
					if(from != Language.English)
					{
						AddSupportedTranslation(from, Language.Czech);
						AddSupportedTranslation(Language.Czech, from);
					}	
					else	
					{
						AddSupportedTranslationFromEnglish(Language.Czech);
						AddSupportedTranslationToEnglish(Language.Czech);
					}	
				}
			}
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 50;
		}

		static SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		static string ConvertLanguage(Language language)
		{
			if(language == Language.English_GB || language == Language.English_US)
				language = Language.English;
		
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}

		static string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			return "dict_" + ConvertLanguage(languagesPair.From) + ConvertLanguage(languagesPair.To);
		}

		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//http://slovnik.tiscali.cz/index.php?od=0&slovnik=dict_ac&dotaz=test
			string query = "http://slovnik.tiscali.cz/index.php?od=0&slovnik={0}&dotaz={1}";
			query = string.Format(CultureInfo.InvariantCulture, query, ConvertLanguagesPair(languagesPair), HttpUtility.UrlEncode(phrase));
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.Contains("<strong>Zadané slovo nebylo nalezeno !!!</strong>"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				if(responseFromServer.Contains("<div class=\"vysledek\">"))
				{
					string translation = StringParser.Parse("<div class=\"vysledek\">", "</div>", responseFromServer);
					StringParser parser = new StringParser(translation);
					string[] translations = parser.ReadItemsList("<a", "<br />");
					Result subres = null;
					foreach(string str in translations)
					{
						string word = StringParser.Parse("<strong>", "</strong>", str);
						string subtranslation = StringParser.Parse(">", "<", StringParser.Parse("<a", "/a>", str));
						
						if (subres == null || subres.Phrase != word ) 
						{
							subres = CreateNewResult(word, languagesPair, subject);
							result.Childs.Add(subres);
						}
						subres.Translations.Add(subtranslation);
						
					}
				}

				//more 
				if(responseFromServer.Contains("<strong>Další >></strong>"))
				{
					query = "http://slovnik.tiscali.cz/index.php?od=24&slovnik={0}&dotaz={1}";
					query = string.Format(CultureInfo.InvariantCulture, query, ConvertLanguagesPair(languagesPair), HttpUtility.UrlEncode(phrase));
				
					string link = "html!<a href=\"{0}\" title=\"{0}\">{1}</a>";
					link = string.Format(link,
						query,
						"More phrases ...");
					Result subres = CreateNewResult(link, languagesPair, subject);
					result.Childs.Add(subres);
				}
				
				if(result.Childs.Count == 0)
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}

			}				
		}
	}

}
