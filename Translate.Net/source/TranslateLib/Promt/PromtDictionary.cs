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
using System.Diagnostics.CodeAnalysis;
using System.Web; 
using System.Text; 
using System.Globalization;


namespace Translate
{
	/// <summary>
	/// Description of PromtDictionary.
	/// </summary>
	public class PromtDictionary : BilingualDictionary
	{
		public PromtDictionary()
		{
			Name = "_dictionary";
			CharsLimit = 50;
			LinesLimit = 1;
			AddSupportedSubject(SubjectConstants.Common);
			
			AddSupportedTranslationFromEnglish(Language.Spanish);
			AddSupportedTranslationFromEnglish(Language.French);
			AddSupportedTranslationFromEnglish(Language.Portuguese);
			AddSupportedTranslation(Language.Russian, Language.Spanish);
			AddSupportedTranslationToEnglish(Language.French);
			AddSupportedTranslation(Language.French, Language.Spanish);
			AddSupportedTranslation(Language.Spanish, Language.Russian);
			AddSupportedTranslationToEnglish(Language.Spanish);
			AddSupportedTranslation(Language.Spanish, Language.French);
			AddSupportedTranslationToEnglish(Language.Portuguese);
			AddSupportedTranslation(Language.Italian, Language.Russian);
			AddSupportedTranslationToEnglish(Language.Italian);
			AddSupportedTranslationFromEnglish(Language.Russian);
			AddSupportedTranslationFromEnglish(Language.German);
			AddSupportedTranslation(Language.German, Language.Russian);
			AddSupportedTranslationToEnglish(Language.German);

			AddSupportedTranslation(Language.German, Language.French);
			AddSupportedTranslation(Language.German, Language.Spanish);
			AddSupportedTranslation(Language.French, Language.German);
			AddSupportedTranslation(Language.Spanish, Language.German);
			AddSupportedTranslationToEnglish(Language.Russian);
			AddSupportedTranslation(Language.Russian, Language.German);
			AddSupportedTranslation(Language.Russian, Language.French);
			AddSupportedTranslation(Language.French, Language.Russian);
			
		}
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www.online-translator.com/text/references.aspx?prmtlang=en&direction={0}&word={1}";
			query = string.Format(query, PromtUtils.ConvertLanguagesPair(languagesPair),
				HttpUtility.UrlEncode(phrase));
				
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.IndexOf("class=\"ref_not_found\"") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			StringParser parser = new StringParser(responseFromServer);
			string[] translation_list = parser.ReadItemsList("<span class=\"ref_source\"", "</table>");
			
			string subphrase;
			string abbreviation;
			Result child;
			StringParser subparser;
			
			foreach(string translation in translation_list)
			{
				subphrase = StringParser.Parse(">","</span>",translation);
				abbreviation = StringParser.Parse("<span class=\"ref_psp\">","</span>",translation);
				child = CreateNewResult(subphrase, languagesPair, subject);
				child.Abbreviation = abbreviation;
				result.Childs.Add(child);
				subparser = new StringParser(translation);
				string[] subtranslation_list = subparser.ReadItemsList("<span class=\"ref_result\">", "</span>");
				foreach(string subtranslation in subtranslation_list)
				{
					child.Translations.Add(HttpUtility.HtmlDecode(subtranslation));
				}
			}
		}			
		
	}
}
