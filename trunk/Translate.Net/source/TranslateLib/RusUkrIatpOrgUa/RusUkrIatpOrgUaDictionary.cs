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
	/// Description of RusUkrIatpOrgUaDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class RusUkrIatpOrgUaDictionary : BilingualDictionary
	{
		public RusUkrIatpOrgUaDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Ukrainian));
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 50;
			WordsCount = 6208;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//http://rosukrdic.iatp.org.ua/search.php?fullname=%E0%E2%EE%F1%FC
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://rosukrdic.iatp.org.ua/search.php"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			helper.Encoding = Encoding.GetEncoding(1251); //koi8-u
			string query = "fullname={0}";
			
			query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase.ToLowerInvariant(), helper.Encoding));
			helper.AddPostData(query);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.IndexOf("</b>відсутнє в словнику.") >= 0 ||
				responseFromServer.IndexOf("</b> відсутнє в словнику.") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				string translation = StringParser.Parse("<body>", "</body>", responseFromServer);
				translation = translation.Replace("</u>", "");
				translation = translation.Replace("</span>", "");
				translation = translation.Replace("<span class=\"examples_class\">", "");
				translation = translation.Replace("<em>", "");
				translation = translation.Replace("</em>", "");
				translation = translation.Replace("<p>\n", "<p>");
				translation = translation.Replace("<p>\r\n", "<p>");
				translation = translation.Replace("<h2>", "\n<h2>");
				translation = translation.Replace("</p>", "\n</p>");
				translation = translation.Replace("<span class='examples_class'>", "");
				
				
				
				StringParser phrasesParser = new StringParser(translation);
				string[] phrases = phrasesParser.ReadItemsList("<h2>", "</h2>", "787654323");

				StringParser translationsParser = new StringParser(translation);
				string[] translations = translationsParser.ReadItemsList("<p", "\n", "787654323");
				
				string subphrase;
				string subtranslation;
				Result subres = null;
				for(int i = 0; i < phrases.Length; i++)
				{
					subphrase = phrases[i].Trim();
					if(subphrase.EndsWith("."))
						subphrase = subphrase.Substring(0, subphrase.Length-1);
					subtranslation = translations[i].Substring(1).Trim();
					subtranslation = subtranslation.Replace("<span class=\"style1\">", "");
					subtranslation = subtranslation.Replace("lass=\"style1\">", "");
					subtranslation = subtranslation.Replace("</p>", "");
					
					subres = CreateNewResult(subphrase, languagesPair, subject);
					subres.Translations.Add(subtranslation);
					result.Childs.Add(subres);
					
				}
				
			}				
		}
	}

}
