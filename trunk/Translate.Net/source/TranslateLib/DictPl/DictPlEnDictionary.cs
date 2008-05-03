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
	/// Description of DictPlEnDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class DictPlEnDictionary : BilingualDictionary
	{
		public DictPlEnDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Polish));
			AddSupportedTranslation(new LanguagePair(Language.Polish, Language.English));
			AddSupportedSubject(SubjectConstants.Common);
			
			Name = "_pl_en";
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//http://www.dict.pl/dict?word=test+tool&words=&lang=EN
			string query = "http://www.dict.pl/dict?word={0}&words=&lang=EN";
			query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase));
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.IndexOf("has not been found - please post it to the") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				string translation = StringParser.Parse("<table cellspacing=", "</table>", responseFromServer);
				
				StringParser parser = new StringParser(translation);
				string[] translations = parser.ReadItemsList("td class=\"resWordCol\">", "</td>", "787654323");
				
				int cnt = translations.Length;
				string tmp;
				for(int i = 0; i < cnt; i++)
				{
					tmp = translations[i];
					tmp = StringParser.RemoveAll("<a href=", ">", tmp);
					tmp = StringParser.RemoveAll("<font", ">", tmp);
					tmp = tmp.Replace("</font>", "");
					tmp = tmp.Replace("</a>", "");
					translations[i] = tmp;
				}
				
				cnt = translations.Length/2;
				string polish, english;
				string subphrase, subtranslation;
				Result subres = null;
				for(int i = 0; i < cnt; i++)
				{
					polish = translations[i*2];
					english = translations[i*2 + 1];
					
					if(languagesPair.From == Language.English)
					{
						subphrase = english;
						subtranslation = polish;
					}
					else
					{
						subphrase = polish;
						subtranslation = english;
					}
					
					if(translations.Length == 2 && string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0)
					{
						result.Translations.Add(subtranslation);
						return;
					}
					
					subres = CreateNewResult(subphrase, languagesPair, subject);
					subres.Translations.Add(subtranslation);
					result.Childs.Add(subres);
				}
			}				
		}
	}

}
