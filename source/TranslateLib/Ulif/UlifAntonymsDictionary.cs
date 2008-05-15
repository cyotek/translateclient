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
	/// Description of UlifAntonymsDictionary.
	/// </summary>
	public class UlifAntonymsDictionary : AntonymsDictionary
	{
		public UlifAntonymsDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Ukrainian));
			
			AddSupportedSubject(SubjectConstants.Common);
			
			CharsLimit = 255;
			Name = "_antonyms_dictionary";
		}

		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string responseFromServer = UlifHelper.GetAntonymsPage(phrase, networkSetting);
			if(string.IsNullOrEmpty(responseFromServer))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
		
			responseFromServer = StringParser.Parse("<div class=\"p_cl\">", "</div>", responseFromServer);
			responseFromServer = StringParser.RemoveAll("<a ondblclick", ">", responseFromServer);
			responseFromServer = StringParser.RemoveAll("<font color", ">", responseFromServer);
			responseFromServer = StringParser.RemoveAll("<p align", ">", responseFromServer);
			
			
			responseFromServer = responseFromServer.Replace("</a>", "");
			responseFromServer = responseFromServer.Replace("<b>", "");
			responseFromServer = responseFromServer.Replace("</b>", "");
			responseFromServer = responseFromServer.Replace("<i>", "");
			responseFromServer = responseFromServer.Replace("</i>", "");
			responseFromServer = responseFromServer.Replace("◘", "");
			responseFromServer = responseFromServer.Replace("◊", "");
			responseFromServer = responseFromServer.Replace("○", "");
			responseFromServer = responseFromServer.Replace("□", "");
			responseFromServer = responseFromServer.Replace("</font>", "");
			responseFromServer = responseFromServer.Replace("</p>", "");
			
			
			StringParser parser = new StringParser(responseFromServer);
			string[] blocks = parser.ReadItemsList("<tr>", "</tr>", "3495783-4572385");
			
			Result subres;
			
			string left = "";
			string right = "";
			if(blocks.Length > 0)
			{
				parser = new StringParser(blocks[0].ToLowerInvariant());
				string[] first_line = parser.ReadItemsList("<td>", "</td>", "3495783-4572385");
				result.Translations.Add(first_line[0] + " - " + first_line[1]);
				left = first_line[0];
				right = first_line[1];
			}
			
			for(int i = 1; i < blocks.Length; i++)
			{

				if(blocks[i].Contains("<td>"))				
				{
					subres = CreateNewResult("html!<hr style=\"width: 100%; height: 1px;\">", languagesPair, subject);
					result.Childs.Add(subres);
				
					parser = new StringParser(blocks[i].ToLowerInvariant());
					string[] second_line = parser.ReadItemsList("<td>", "</td>", "3495783-4572385");
					
					subres = CreateNewResult(left, languagesPair, subject);
					result.Childs.Add(subres);
					subres.Translations.Add(second_line[0]);
	
					subres = CreateNewResult(right, languagesPair, subject);
					result.Childs.Add(subres);
					subres.Translations.Add(second_line[1]);
					
					subres = CreateNewResult("html!<hr style=\"width: 100%; height: 1px;\">", languagesPair, subject);
					result.Childs.Add(subres);
					
				}
				else
				{
					string line = StringParser.Parse("<td colspan=\"2\">", "</td>", blocks[i]);
					subres = CreateNewResult("", languagesPair, subject);
					result.Childs.Add(subres);
					subres.Translations.Add(line);
				
				}
			}
		}
	}
}
