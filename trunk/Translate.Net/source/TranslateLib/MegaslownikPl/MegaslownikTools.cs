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
using System.Diagnostics.CodeAnalysis;
using System.Web; 
using System.Text; 
using System.Globalization;


namespace Translate
{
	/// <summary>
	/// Description of MegaslownikTools.
	/// </summary>
	public static class MegaslownikTools
	{
		static MegaslownikTools()
		{
		}
		
		public static string ConvertLanguagePair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");

			if(languagesPair.From == Language.Polish && languagesPair.To == Language.Polish)
				return "synonimy_antonimy";
			
			string result = "";
			if(languagesPair.To == Language.Polish)
			{
				switch(languagesPair.From)
				{
					case Language.English:
					case Language.English_GB:
					case Language.English_US:
						result = "angielsko_polski";
						break;
					case Language.German:
						result = "niemiecko_polski";
						break;
					case Language.Russian:
						result = "rosyjsko_polski";
						break;
					case Language.French:
						result = "francusko_polski";
						break;
					case Language.Spanish:
						result = "hiszpansko_polski";
						break;
					case Language.Italian:
						result = "wlosko_polski";
						break;
				}
			}
			else
			{
				switch(languagesPair.To)
				{
					case Language.English:
					case Language.English_GB:
					case Language.English_US:
						result = "polsko_angielski";
						break;
					case Language.German:
						result = "polsko_niemiecki";
						break;
					case Language.Russian:
						result = "polsko_rosyjski";
						break;
					case Language.French:
						result = "polsko_francuski";
						break;
					case Language.Spanish:
						result = "polsko_hiszpanski";
						break;
					case Language.Italian:
						result = "polsko_wloski";
						break;
				}			
			}
			
			return result;
		}
		
		public static void DoTranslate(ServiceItem serviceItem, string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			bool synonimsDictionary = languagesPair.From == Language.Polish && languagesPair.To == Language.Polish;
			string query = "http://megaslownik.pl/slownik/{0}/,{1}";
			query = string.Format(query, 
				MegaslownikTools.ConvertLanguagePair(languagesPair),
				HttpUtility.UrlEncode(phrase));

			result.ArticleUrl = query;
			result.ArticleUrlCaption = phrase;

			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
					
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains("<div class=\"slowo\">\r\n             Szukanego słowa nie ma w MEGAsłowniku.\r\n"))
			{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
			}
			
			result.HasAudio = responseFromServer.Contains("class=\"ikona_sluchaj2\">");
			
			string[] translations = StringParser.ParseItemsList("<div class=\"definicja\">", "</div>", responseFromServer);
			
			if(translations.Length == 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			string subsubtranslation;
			string[] subtranslations;
			foreach(string translation in translations)
			{
				subtranslations = StringParser.ParseItemsList("<a href=\"/slownik", "</a>", translation);
				foreach(string subtranslation in subtranslations)
				{
					subsubtranslation = StringParser.ExtractRight(">", subtranslation);
					subsubtranslation = StringParser.RemoveAll("<", ">", subsubtranslation);
					result.Translations.Add(subsubtranslation);
				}
			}

			//synonims
			translations = StringParser.ParseItemsList("<div class=\"synonim\">synonimy:", "</div>", responseFromServer);
			
			foreach(string translation in translations)
			{
				subtranslations = StringParser.ParseItemsList("<a href=\"/slownik", "</a>", translation);
				foreach(string subtranslation in subtranslations)
				{
					subsubtranslation = StringParser.ExtractRight(">", subtranslation);
					subsubtranslation = StringParser.RemoveAll("<", ">", subsubtranslation);
					if(!result.Translations.Contains(subsubtranslation))
						result.Translations.Add(subsubtranslation);
				}
			}
			
			//additional links
			if(!synonimsDictionary)
			{
				string[] links = StringParser.ParseItemsList("<li ><a href=\"/slownik/", "</li>", responseFromServer);
				string linkUrl, linkText, subphrase, subtrans;
				Result child;
				foreach(string link in links)
				{
					linkUrl =  "http://megaslownik.pl/slownik/" + StringParser.ExtractLeft("\"", link);
					linkText = StringParser.ExtractRight(">", link);
					linkText = StringParser.RemoveAll("<", ">", linkText);
					if(linkText.Contains("»") && linkText.Contains(phrase))
					{
						subphrase = StringParser.ExtractLeft("»", linkText);
						subtrans = StringParser.ExtractRight("»", linkText);
						child = serviceItem.CreateNewResult(subphrase, languagesPair, subject);
						result.Childs.Add(child);
						child.Translations.Add(subtrans);
						child.ArticleUrl = linkUrl;
						child.ArticleUrlCaption = subphrase;
					}
				}
				
				links = StringParser.ParseItemsList("<li><a href=\"/slownik/", "</li>", responseFromServer);
				foreach(string link in links)
				{
					linkUrl =  "http://megaslownik.pl/slownik/" + StringParser.ExtractLeft("\"", link);
					linkText = StringParser.ExtractRight(">", link);
					linkText = StringParser.RemoveAll("<", ">", linkText);
					if(linkText.Contains("»") && linkText.Contains(phrase))
					{
						subphrase = StringParser.ExtractLeft("»", linkText);
						subtrans = StringParser.ExtractRight("»", linkText);
						child = serviceItem.CreateNewResult(subphrase, languagesPair, subject);
						result.Childs.Add(child);
						child.Translations.Add(subtrans);
						child.ArticleUrl = linkUrl;
						child.ArticleUrlCaption = subphrase;
					}
					//result.RelatedLinks.Add(linkText, linkUrl);
				}
			}
			else
			{ //synonyms
				string[] links = StringParser.ParseItemsList("<li ><a href=\"/slownik/", "</li>", responseFromServer);
				string linkUrl, linkText;
				foreach(string link in links)
				{
					linkUrl =  "http://megaslownik.pl/slownik/" + StringParser.ExtractLeft("\"", link);
					linkText = StringParser.ExtractRight(">", link);
					linkText = StringParser.RemoveAll("<", ">", linkText);
					if(linkText.Contains(phrase))
						result.RelatedLinks.Add(linkText, linkUrl);
				}
				
				links = StringParser.ParseItemsList("<li><a href=\"/slownik/", "</li>", responseFromServer);
				foreach(string link in links)
				{
					linkUrl =  "http://megaslownik.pl/slownik/" + StringParser.ExtractLeft("\"", link);
					linkText = StringParser.ExtractRight(">", link);
					linkText = StringParser.RemoveAll("<", ">", linkText);
					if(linkText.Contains(phrase))
						result.RelatedLinks.Add(linkText, linkUrl);					
				}
			}
		}
	}
}
