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
			
			if(responseFromServer.IndexOf("did not return any results</div>") >= 0)
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
				string translation = StringParser.Parse("<hr style=\"border:0; background-color: grey; height:1px; width:92%; text-align:center\" />", "<hr style=\"border:0; background-color: grey; height:1px; width:92%; text-align:center\" />", responseFromServer);
				StringsTree tree = StringParser.ParseTreeStructure("<table", "</table>", translation);
				if(tree.Childs.Count != 1)
						throw new TranslationException("Wrong data structure");

				tree = tree.Childs[0];

				if(tree.Childs.Count != 1)
						throw new TranslationException("Wrong data structure");

				tree = tree.Childs[0];
				
				Result wordres = null;	
					
				foreach(StringsTree words_tree in tree.Childs)
				{
					string word = StringParser.Parse("/\">", "</a>", words_tree.Data);
					if(tree.Childs.Count == 1)
					{
						wordres = result;
					}
					else
					{
						wordres = CreateNewResult(word, languagesPair, subject);
						result.Childs.Add(wordres);
					}
				
					Result abbrres = null;	
					foreach(StringsTree abbr_tree in words_tree.Childs)
					{
						string abbr = StringParser.Parse("none\">", "</a>", abbr_tree.Data);
						if(words_tree.Childs.Count == 1)
						{
							wordres.Abbreviation = abbr;
							abbrres = wordres;
						}
						else
						{
							Result tmpRes = CreateNewResult(abbr, languagesPair, subject);
							wordres.Childs.Add(tmpRes);
							abbrres = tmpRes;
						}
						
						Result areares = null;	
						foreach(StringsTree area_tree in abbr_tree.Childs)
						{
							string area = StringParser.Parse("none\">", "</a>", area_tree.Data);
							if(abbr_tree.Childs.Count == 1)
							{
								if(area != "General")
									abbrres.Abbreviation += " " + area;
								areares = abbrres;	
							}
							else
							{
								Result tmpRes = CreateNewResult(area, languagesPair, subject);
								abbrres.Childs.Add(tmpRes);
								areares = tmpRes;
							}
							
							foreach(StringsTree translation_tree in area_tree.Childs)
							{
								string trans = StringParser.Parse("/\">", "</a>", translation_tree.Data);
								areares.Translations.Add(trans);
							}
						}
					}
				}
			}
			
		}
		
	}
}
