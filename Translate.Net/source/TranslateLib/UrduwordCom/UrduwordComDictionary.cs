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
	/// Description of UrduwordComDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class UrduwordComDictionary : BilingualDictionary
	{
		public UrduwordComDictionary()
		{

			AddSupportedTranslationFromEnglish(Language.Urdu);
			AddSupportedTranslationToEnglish(Language.Urdu);
		
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 50;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			
			//http://www.urduword.com/search.php?English=test
			string query = "http://www.urduword.com/search.php?English={0}";
			if(languagesPair.From == Language.Urdu)
				query = "http://www.urduword.com/search.php?Roman={0}";
			
			query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase));
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.Contains("<p>Could not find translation for <b>"))	
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				if(responseFromServer.Contains("<table border=\"0\" width=\"100%\" align=\"center\" cellpadding=\"3\" cellspacing=\"0\" >"))
				{
					result.ArticleUrl = query;
					result.ArticleUrlCaption = phrase;
				
					string translation = StringParser.Parse("<table border=\"0\" width=\"100%\" align=\"center\" cellpadding=\"3\" cellspacing=\"0\" >", "</table>", responseFromServer);
					StringParser parser = new StringParser(translation);
					string[] translations = parser.ReadItemsList("<tr>", "</tr>");
					Result subres = null;
					foreach(string str in translations)
					{
						if(str.Contains("class=\"tablehead\""))
							continue;
							
						string word = StringParser.Parse(">", "<", StringParser.Parse("<a", "/a>", str));
						translation  = StringParser.Parse("align=\"center\">", "<", str);
						
						if(languagesPair.From == Language.Urdu)
						{
							if(subres == null || subres.Phrase != translation)
							{
								subres = CreateNewResult(translation, languagesPair, subject);
								result.Childs.Add(subres);							
							}
						}
						else
						{
							if(subres == null || subres.Phrase != word)
							{
								subres = CreateNewResult(word, languagesPair, subject);
								result.Childs.Add(subres);
							}	
						}
						
						if(languagesPair.From == Language.Urdu)
							subres.Translations.Add(word);
						else
							subres.Translations.Add(translation);
						
					}
				}

				//more 
				if(responseFromServer.Contains("page=2\">Next"))
				{
					query = "http://www.urduword.com/search.php?English={0}&page=2";
					if(languagesPair.From == Language.Urdu)
						query = "http://www.urduword.com/search.php?Roman={0}&page=2";
					
					query = string.Format(CultureInfo.InvariantCulture, query, HttpUtility.UrlEncode(phrase));
				
					string link = "html!<p><a href=\"{0}\" title=\"{0}\">{1}</a></p>";
					link = string.Format(link,
						query,
						"More ...");
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
