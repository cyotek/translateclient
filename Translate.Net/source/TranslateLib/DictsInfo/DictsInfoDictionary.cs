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
	/// Description of DictsInfoDictionary.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class DictsInfoDictionary  : BilingualDictionary
	{
		public DictsInfoDictionary()
		{
			CharsLimit = 15;
			Name = "_dictionary";
			AddSupportedSubject(SubjectConstants.Common);
			
			langToKey.Add(Language.Afrikaans, "afrikaans");
			langToKey.Add(Language.Albanian, "albanian");
			langToKey.Add(Language.Arabic, "arabic");
			langToKey.Add(Language.Armenian, "armenian");
			langToKey.Add(Language.Basque, "basque");
			langToKey.Add(Language.Bengali, "bengali");
			langToKey.Add(Language.Bosnian, "bosnian");
			langToKey.Add(Language.Portuguese_BR, "brazilian_portuguese");
			langToKey.Add(Language.Breton, "breton");
			langToKey.Add(Language.Bulgarian, "bulgarian");
			langToKey.Add(Language.Catalan, "catalan");
			langToKey.Add(Language.Chinese, "chinese");
			langToKey.Add(Language.Croatian, "croatian");
			langToKey.Add(Language.Czech, "czech");
			langToKey.Add(Language.Danish, "danish");
			langToKey.Add(Language.Dutch, "dutch");
			langToKey.Add(Language.Esperanto, "esperanto");
			langToKey.Add(Language.Estonian, "estonian");
			langToKey.Add(Language.Faroese, "faroese");
			langToKey.Add(Language.Finnish, "finnish");
			langToKey.Add(Language.French, "french");
			langToKey.Add(Language.Frisian, "frisian");
			langToKey.Add(Language.Galician, "galician");
			langToKey.Add(Language.Georgian, "georgian");
			langToKey.Add(Language.German, "german");
			langToKey.Add(Language.Greek, "greek");
			langToKey.Add(Language.Hebrew, "hebrew");
			langToKey.Add(Language.Hindi, "hindi");
			langToKey.Add(Language.Hungarian, "hungarian");
			langToKey.Add(Language.Icelandic, "icelandic");
			langToKey.Add(Language.Indonesian, "indonesian");
			langToKey.Add(Language.Interlingua, "interlingua");
			langToKey.Add(Language.Irish, "irish");
			langToKey.Add(Language.Italian, "italian");
			langToKey.Add(Language.Japanese, "japanese");
			langToKey.Add(Language.Japanese_Romaji, "japanese_romaji");
			langToKey.Add(Language.Korean, "korean");
			langToKey.Add(Language.Latin, "latin");
			langToKey.Add(Language.Latvian, "latvian");
			langToKey.Add(Language.Lithuanian, "lithuanian");
			langToKey.Add(Language.Macedonian, "macedonian");
			langToKey.Add(Language.Maltese, "maltese");
			langToKey.Add(Language.Mongolian, "mongolian");
			langToKey.Add(Language.Nepali, "nepali");
			langToKey.Add(Language.Norwegian_Bokmal, "norwegian_bokmal");
			langToKey.Add(Language.Norwegian_Nynorsk, "norwegian_nynorsk");
			langToKey.Add(Language.Papiamento, "papiamento");
			langToKey.Add(Language.Persian, "persian");
			langToKey.Add(Language.Polish, "polish");
			langToKey.Add(Language.Portuguese, "portuguese");
			langToKey.Add(Language.Romanian, "romanian");
			langToKey.Add(Language.Russian, "russian");
			langToKey.Add(Language.Serbian, "serbian");
			langToKey.Add(Language.Slovak, "slovak");
			langToKey.Add(Language.Slovenian, "slovene");
			langToKey.Add(Language.Spanish, "spanish");
			langToKey.Add(Language.Swahili, "swahili");
			langToKey.Add(Language.Swedish, "swedish");
			langToKey.Add(Language.Filipino, "tagalog");
			langToKey.Add(Language.Tamil, "tamil");
			langToKey.Add(Language.Telugu, "telugu");
			langToKey.Add(Language.Thai, "thai");
			langToKey.Add(Language.Turkish, "turkish");
			langToKey.Add(Language.Vietnamese, "vietnamese");
			langToKey.Add(Language.Welsh, "welsh");
			
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				AddSupportedTranslationFromEnglish(from);
				AddSupportedTranslationToEnglish(from);
				foreach(Language to in tmp.Keys)
				{
					if(from != to)
					{
						AddSupportedTranslation(new LanguagePair(from, to));
					  
					  	if(from == Language.Filipino)
					  		AddSupportedTranslation(new LanguagePair(Language.Tagalog, to));
					  		
					  	if(to == Language.Filipino)
					  		AddSupportedTranslation(new LanguagePair(from, Language.Tagalog));

					  	if(from == Language.Norwegian_Bokmal)
					  		AddSupportedTranslation(new LanguagePair(Language.Norwegian, to));
					  		
					  	if(to == Language.Norwegian_Bokmal)
					  		AddSupportedTranslation(new LanguagePair(from, Language.Norwegian));
					}  
				}
			}

			langToKey.Add(Language.Tagalog, "tagalog");
			langToKey.Add(Language.Norwegian, "norwegian_bokmal");
			langToKey.Add(Language.English,"");
			langToKey.Add(Language.English_US,"");
			langToKey.Add(Language.English_GB,"");
			
		}
		
		SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		SortedDictionary<Language, string> langToEncoding = new SortedDictionary<Language, string>();

		
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
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.dicts.info/ud.php"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
					
			string fromLanguage = ConvertLanguage(languagesPair.From);
			string toLanguage = ConvertLanguage(languagesPair.To);
			
			int fromLangColumn,  toLangColumn;
			string lang_query = "lan1={0}&lan2=";
			if(string.IsNullOrEmpty(fromLanguage))
			{
				fromLangColumn = 0;
				toLangColumn = 1;
				lang_query = string.Format(lang_query, toLanguage, "");
			}
			else if(string.IsNullOrEmpty(toLanguage))
			{
				fromLangColumn = 1;
				toLangColumn = 0;
				lang_query = string.Format(lang_query, fromLanguage, "");
			}
			else
			{
				fromLangColumn = 1;
				toLangColumn = 2;
				lang_query = string.Format(lang_query, fromLanguage, toLanguage);
			}

			string query = "{0}&word={1}&rad=ftsearch&go=Search";
			
			query = string.Format(CultureInfo.InvariantCulture, 
					query,
					lang_query,
					HttpUtility.UrlEncode(phrase)
				);
			helper.AddPostData(query);
		
			string responseFromServer = helper.GetResponse();
		
			if(!responseFromServer.Contains("<table class=\"t1\""))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}

			//offcet
			fromLangColumn += 2;
			toLangColumn += 2;
			
			string translation = StringParser.Parse("<table class=\"t1\"", "</table>",  responseFromServer);
			string[] translations = StringParser.ParseItemsList("<tr bgcolor=\"", "</tr>", translation);
			
			if(translations.Length == 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			string sub_phrase, sub_translation;
			Result subres = null;
			foreach(string subtranslation in translations)
			{
				string[] columns = StringParser.ParseItemsList("<td", "</td>", subtranslation);
				
				sub_phrase = StringParser.ExtractRight(">", columns[fromLangColumn]).Trim();
				sub_phrase = StringParser.RemoveAll("<", ">", sub_phrase);
				sub_translation = StringParser.ExtractRight(">", columns[toLangColumn]).Trim();
				sub_translation = StringParser.RemoveAll("<", ">", sub_translation);
				
				if(!string.IsNullOrEmpty(sub_phrase) && !string.IsNullOrEmpty(sub_translation))
				{
					if(subres == null ||
						subres.Phrase != sub_phrase)
					{	
						subres = CreateNewResult(sub_phrase, languagesPair, subject);
						result.Childs.Add(subres);
					}
					
					if(!subres.Translations.Contains(sub_translation))
						subres.Translations.Add(sub_translation);
				}
			}
			
			if(result.Childs.Count == 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
		}
		
	}
}
