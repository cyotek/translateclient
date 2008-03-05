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

namespace Translate
{
	/// <summary>
	/// Description of PcDigestNetDictionary.
	/// </summary>
	public class PcDigestNetDictionary : MonolingualDictionary
	{
		public PcDigestNetDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Ukrainian));
			
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 255;
			Name = "_dictionary";
			WordsCount = 23630;
		}

		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www.pcdigest.net/cgi-bin/u/book/sis.pl?Qry={0}&found=10&action=search";
			query = string.Format(query, HttpUtility.UrlEncode(phrase, Encoding.GetEncoding(1251)));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet, 
						Encoding.GetEncoding(1251));
						
			string responseFromServer = helper.GetResponse();
		
			responseFromServer = StringParser.Parse("<!-- START CONTENT -->", "<!-- RIGHT SHORT TABLE -->", responseFromServer);
			responseFromServer = StringParser.Parse("<P align=justify>", "</table>", responseFromServer);
			responseFromServer = responseFromServer.Replace("<font color=darkgreen>", "");
			responseFromServer = responseFromServer.Replace("<font color=red>", "");
			responseFromServer = responseFromServer.Replace("</font>", "");
			
			responseFromServer = responseFromServer.Replace("<B>", "");
			responseFromServer = responseFromServer.Replace("</B>", "");
			responseFromServer = responseFromServer.Replace("<I>", "");
			responseFromServer = responseFromServer.Replace("</I>", "");

			responseFromServer = responseFromServer.Replace("<i>", "");
			responseFromServer = responseFromServer.Replace("</i>", "");
			
			responseFromServer = responseFromServer.Replace("<u>", "");
			responseFromServer = responseFromServer.Replace("</u>", "");
			
						
			StringParser parser = new StringParser(responseFromServer);
			string[] translations = parser.ReadItemsList("<P align=justify>", "</P>", "3495783-4572385");
			
			if(translations.Length == 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}

			string 	translation, abrr, data;
			int idx;
			foreach(string subtranslation in translations)
			{
				idx = subtranslation.IndexOf("(");
				if(idx < 0)
					idx = subtranslation.IndexOf("-");
					
				translation = subtranslation.Substring(0, idx);
				
				if(subtranslation.IndexOf("(") >= 0)
					abrr = StringParser.Parse("(", ")", subtranslation);
				else
					abrr = "";
					
				idx = subtranslation.IndexOf("-");	
				if(idx >= 0)
					data = subtranslation.Substring(idx + 1);
				else
					data = "";
				
				Result subres;
				if(translations.Length > 1)
				{
					subres = CreateNewResult(translation, languagesPair, subject);
					result.Childs.Add(subres);
				}
				else 
					subres = result;
				
				if(!string.IsNullOrEmpty(abrr))
					subres.Abbreviation = abrr;
				if(!string.IsNullOrEmpty(data))	
					subres.Translations.Add(data);
			}
		}
	}
}
