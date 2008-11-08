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
	/// Description of CybermovaComDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Slovnenya")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class SlovnenyaComDictionary : BilingualDictionary
	{
		public SlovnenyaComDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Ukrainian));
			AddSupportedSubject(SubjectConstants.Common);
			
			IsQuestionMaskSupported = false;
			IsAsteriskMaskSupported = false;
			
			WordsCount = 313917;
			CharsLimit = 300;
			LinesLimit = 1;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www.slovnenya.com/dictionary/{0}";
			query = string.Format(query, HttpUtility.UrlEncode(phrase).Replace("+", "%20"));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
			
			helper.AcceptLanguage = "en-us,en";
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains("did not return any results</div>") || 
				responseFromServer.Contains("</span>` did not return any results"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else if(responseFromServer.IndexOf("Query contains extraneous symbol(s)<") >= 0)
			{
				throw new TranslationException("Query contains extraneous symbols");
			}
			else
			{
				result.ArticleUrl = query;
				result.ArticleUrlCaption = phrase;
			
				string translation = StringParser.Parse("<hr style=\"border:0; background-color: grey; height:1px; width:92%; text-align:center\" />", "<hr style=\"border:0; background-color: grey; height:1px; width:92%; text-align:center\" />", responseFromServer);
				StringsTree tree = StringParser.ParseTreeStructure("<table", "</table>", translation);
				if(tree.Childs.Count != 1)
						throw new TranslationException("Wrong data structure");

				tree = tree.Childs[0];

				if(tree.Childs.Count != 1)
						throw new TranslationException("Wrong data structure");

				tree = tree.Childs[0];

				Result wordres = result;	

				if(tree.Childs.Count == 0)
						throw new TranslationException("Wrong data structure");
						

				//get word 
				if(tree.Childs[0].Childs.Count != 1)
					throw new TranslationException("Wrong data structure");
					

				string word = StringParser.Parse("font-size:14pt;\">", "<", tree.Childs[0].Childs[0].Data);
					
				for(int i = 1; i < tree.Childs.Count; i++)
				{
					StringsTree abbr_tree = tree.Childs[i];
					Result abbrres = null;	
					
					string abbr = StringParser.Parse("font-size:12pt;\">", "<", abbr_tree.Data);
					Result tmpRes = CreateNewResult(abbr, languagesPair, subject);
					wordres.Childs.Add(tmpRes);
					abbrres = tmpRes;
					
					StringParser parser = new StringParser(abbr_tree.Childs[0].Data);
					string[] translations = parser.ReadItemsList("font-size:12pt;\">", "<");
					foreach(string trans in translations)
						abbrres.Translations.Add(trans);
				}
			}
			
		}
		
	}
}
