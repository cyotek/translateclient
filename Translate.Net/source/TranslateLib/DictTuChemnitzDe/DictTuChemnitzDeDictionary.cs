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
	/// Description of DictTuChemnitzDeDictionary.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class DictTuChemnitzDeDictionary : BilingualDictionary
	{
		public DictTuChemnitzDeDictionary()
		{
			AddSupportedTranslationFromEnglish(Language.German);
			AddSupportedTranslationToEnglish(Language.German);
			AddSupportedTranslation(Language.German, Language.Spanish);
			AddSupportedTranslation(Language.Spanish, Language.German);
			
			AddSupportedTranslation(Language.German, Language.Portuguese);
			AddSupportedTranslation(Language.Portuguese, Language.German);
			
			AddSupportedSubject(SubjectConstants.Common);
			
			Name = "_dictionary";
			
			CharsLimit = 70;
		}
		static Encoding encoding = Encoding.GetEncoding("iso-8859-1");
		
		static string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			string result = "deen";
			if(languagesPair.From == Language.Spanish || languagesPair.To == Language.Spanish)
				result = "dees";
			else if(languagesPair.From == Language.Portuguese || languagesPair.To == Language.Portuguese)	 
				result = "dept";
			return result;	
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://dict.tu-chemnitz.de/dings.cgi?lang=en&service={0}&opterrors=0&optpro=0&query={1}&iservice=&dlink=self&comment=";
			query = string.Format(CultureInfo.InvariantCulture, query, 
				ConvertLanguagesPair(languagesPair),
				HttpUtility.UrlEncode(phrase, encoding)
				);

			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
			helper.Encoding = encoding;		 
					
		
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains(">0 <span class=\"h\">similar</span> results for "))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			string[] translations = StringParser.ParseItemsList("<td align=\"right\"><br", "</tr>", responseFromServer);
			
			if(translations.Length == 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			string en_string, ge_string;  
			Result child = result;
			string subphrase = "";
			foreach(string translation in translations)
			{
				string[] subtranslations = StringParser.ParseItemsList("<td class=\"", "</td>", translation);
				if(subtranslations.Length != 2)
					throw new TranslationException("Can't found translations in string : " + translation);
				
				en_string = StringParser.ExtractRight(">", subtranslations[1]);
				en_string = StringParser.RemoveAll("<", ">", en_string);
				ge_string = StringParser.ExtractRight(">", subtranslations[0]);				
				ge_string = StringParser.RemoveAll("<", ">", ge_string);
				
				if(languagesPair.From == Language.German)
				{
					if(subphrase != ge_string)
					{
						child = new Result(result.ServiceItem, ge_string, result.LanguagePair, result.Subject);
						subphrase = ge_string;
						result.Childs.Add(child);
					}
					child.Translations.Add(en_string);
				}
				else
				{
					if(subphrase != en_string)
					{
						child = new Result(result.ServiceItem, en_string, result.LanguagePair, result.Subject);
						subphrase = en_string;
						result.Childs.Add(child);
					}
					
					child.Translations.Add(ge_string);
				}
				
			}
		}
	}
}
