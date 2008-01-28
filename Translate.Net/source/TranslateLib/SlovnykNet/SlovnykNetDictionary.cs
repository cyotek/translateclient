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
 * Portions created by the Initial Developer are Copyright (C) 2007-2008
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
	/// Description of SlovnykNetThesaurus.
	/// </summary>
	public class SlovnykNetDictionary : MonolingualDictionary
	{
		public SlovnykNetDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Ukrainian));
			AddSupportedSubject(SubjectConstants.Common);
			
			IsQuestionMaskSupported = false;
			IsAsteriskMaskSupported = false;
			
			WordsCount = 207000;
		}
		
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www.slovnyk.net/?swrd={0}";
			query = string.Format(query, HttpUtility.UrlEncode(phrase, Encoding.GetEncoding(1251)));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
			helper.Encoding = Encoding.GetEncoding(1251);
		
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.IndexOf("<td align='center' class='art_body'>Не знайдено...<br />") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				string translation = StringParser.Parse("<tr><td class='art_body'>", "</td></tr>", responseFromServer);
				
				translation = translation.Replace("<span style=\"color:red\">", "");
				translation = translation.Replace("</span>", "́");
				
				translation = translation.Replace("</FONT><FONT COLOR=\"#ff0000\">", "");
				translation = translation.Replace("</FONT>", "́");
				
				
				translation = translation.Replace("<i>", "");
				translation = translation.Replace("<I>", "");
				translation = translation.Replace("</i>", "");
				translation = translation.Replace("</I>", "");
				translation = translation.Replace("<p>", "");
				translation = translation.Replace("</p>", "");
				translation = translation.Replace("<P>", "");
				translation = translation.Replace("</P>", "");
				translation = translation.Replace("<B>", "<b>");
				translation = translation.Replace("</B>", "</b>");
				
				
				//get definition
				int startIdx = translation.IndexOf("<b>");
				int endIdx = translation.IndexOf("</b>");
				if(startIdx < 0 || endIdx <= startIdx)
				{
					throw new TranslationException("Parse error, start tags not found");
				}
				result.Translations.Add(translation.Substring(startIdx + 3, endIdx - startIdx - 3).ToLower(CultureInfo.GetCultureInfo(0x0422)));
				translation = translation.Substring(endIdx + 4);
				if(translation[0] == '\'')
				{  //apostrophe
					startIdx = translation.IndexOf("<b>");
					endIdx = translation.IndexOf("</b>");
					if(startIdx < 0 || endIdx <= startIdx)
					{
						throw new TranslationException("Parse error, start tags not found");
					}					
					result.Translations[0] = result.Translations[0] + "'" + translation.Substring(startIdx + 3, endIdx - startIdx - 3).ToLower(CultureInfo.GetCultureInfo(0x0422));
					translation = translation.Substring(endIdx + 4);
				}
				
				if(translation.StartsWith(", "))
					translation = translation.Substring(2);
					
				translation = translation.Replace("<b>", "");
				translation = translation.Replace("</b>", "");
				translation = translation.Replace("**", "");
				
				//links
				translation = StringParser.RemoveAll("<a href=\"http://www.slovnyk.net/?swrd=", "\">", translation);
				translation = translation.Replace("</a>", "");

				//spans
				translation = StringParser.RemoveAll("<span", ">", translation);

				//sups
				translation = StringParser.RemoveAll("<sup>", "</sup>", translation);
				
				//Abbreviation
				int definitionIdx = translation.IndexOf("1.");
				endIdx = translation.IndexOf(".");
				if(endIdx < 0 && definitionIdx < 0)
					throw new TranslationException("Parse error, start of definition not found");
					
				if(definitionIdx > 0)
				{
					result.Abbreviation = translation.Substring(0, definitionIdx);	
					translation = translation.Substring(definitionIdx);
				}
				else
				{
					result.Abbreviation = translation.Substring(0, endIdx);	
					translation = translation.Substring(endIdx + 2);
				}
				
				if(!translation.StartsWith("1."))
				{
					result.Translations.Add(translation);
					return;
				}
				else
				{
					
					for(int i = 2; i < 100; i++)
					{
						int numIdx = translation.IndexOf(i.ToString() + ".");
						if(numIdx < 0)
						{  //last def
						   Result defResult = CreateNewResult("", languagesPair, subject);
						   defResult.Translations.Add(translation.Substring(2).Trim());
						   result.Childs.Add(defResult);
						   return;
						}
						else
						{
							string Definition = translation.Substring(2, numIdx - 2);
							translation = translation.Substring(numIdx);
	  						Result defResult = CreateNewResult("", languagesPair, subject);
							defResult.Translations.Add(Definition.Trim());
							result.Childs.Add(defResult);
						}
					}
				}
			}
			
		}
		
	}
}
