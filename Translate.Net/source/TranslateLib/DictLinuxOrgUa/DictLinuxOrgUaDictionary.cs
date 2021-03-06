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
	/// Description of R2uOrgUaDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class DictLinuxOrgUaDictionary : BilingualDictionary
	{
		public DictLinuxOrgUaDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Ukrainian));
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.English));
			AddSupportedSubject(SubjectConstants.Common);
			AddSupportedSubject(SubjectConstants.Informatics);
			AddSupportedSubject(SubjectConstants.Electronics);
			
			CharsLimit = 50;
			LinesLimit = 1;			
			WordsCount = 45000;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//http://dict.linux.org.ua/dict/db/table_adv.php?word_str=help&expr=any&A=on&P=on&O=on
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://dict.linux.org.ua/db/table_adv.php"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			//helper.Encoding = Encoding.GetEncoding(21866); //koi8-u
			string query = "word={0}&expr=any&A=on&P=on&O=on";
			
			query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase, helper.Encoding));
			helper.AddPostData(query);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.IndexOf("не знайдено<br>") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else if(responseFromServer.EndsWith("Рядок пошуку містить недозволені символи."))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Query contains extraneous symbols");
			}
			else if(!responseFromServer.Contains("</html>"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				string translation = StringParser.Parse("<table BORDER WIDTH=", "</table>", responseFromServer);
				translation = translation.Replace("<B>", "");
				translation = translation.Replace("</B>", "");
				
				StringParser parser = new StringParser(translation);
				string[] translations = parser.ReadItemsList("<tr>", "</td></tr>", "787654323");
				
				string subpart;
				string subphrase;
				string subtranslation;
				Result subres = null;
				foreach(string part in translations)
				{
					subpart = part;
					subpart = StringParser.RemoveAll("<td width=\"2%\">", "</td><td width=\"40%\">", subpart);
					subpart = subpart.Replace("<td></td><td>", "");
					
					if(subpart.StartsWith("<A HREF"))
					{
						subphrase = StringParser.Parse("\">", "</A>", subpart);
						subtranslation = StringParser.Parse("\"40%\">", "</td>", subpart);
						
						if(translations.Length == 1 && string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0)
						{
							result.Translations.Add(subtranslation);
							return;
						}
						
						subres = CreateNewResult(subphrase, languagesPair, subject);
						subres.Translations.Add(subtranslation);
						result.Childs.Add(subres);
					}
					else if(!subpart.StartsWith(" "))
					{
						int idx = subpart.IndexOf("</td>");
						if(idx < 0)
						{
							subphrase = "Parse Error";
							subtranslation = "Parse Error";
						}
						else
						{
							subphrase = subpart.Substring(0, idx);
							subpart = subpart.Substring(idx + 5);
							subpart = subpart.Replace("<td width=\"40%\">", "");
							subtranslation = StringParser.Parse("\">", "</A>", subpart);
						}
						
						if(translations.Length == 1 && string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0)
						{
							result.Translations.Add(subtranslation);
							return;
						}
						
						subres = CreateNewResult(subphrase, languagesPair, subject);
						subres.Translations.Add(subtranslation);
						result.Childs.Add(subres);
					}
					else
					{
						subtranslation = StringParser.Parse("\"40%\">", "</td>", subpart);
						if(subres != null)
							subres.Translations.Add(subtranslation);						
					}
					
				}
				
			}				
		}
	}

}
