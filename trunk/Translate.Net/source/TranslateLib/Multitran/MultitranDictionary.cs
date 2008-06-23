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
	/// Description of MultitranDictionary.
	/// </summary>
	public class MultitranDictionary : BilingualDictionary
	{

		public MultitranDictionary()
		{
			AddSupportedSubject(SubjectConstants.Common);
			
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Russian));
			
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Spanish));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.French));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Dutch));
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Latvian));
			AddSupportedTranslation(new LanguagePair(Language.Latvian, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Estonian));
			AddSupportedTranslation(new LanguagePair(Language.Estonian, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Japanese));
			AddSupportedTranslation(new LanguagePair(Language.Japanese, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Afrikaans));
			AddSupportedTranslation(new LanguagePair(Language.Afrikaans, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.German, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.German));

			AddSupportedTranslation(new LanguagePair(Language.Japanese, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Japanese));
			
			CharsLimit = 50;
		}
		
		static MultitranDictionary()
		{
			//1 - English, 2 - Russian, 3 - German, 4 - French, 
			//5 - Spanish, 23 - Italian, 24 - Dutch, 26 - Estonian, 
			//27 - Latvian, 28 - Japanese, 31 - Afrikaans, 
			langToKey.Add(Language.English, 1);
			langToKey.Add(Language.Russian, 2);
			langToKey.Add(Language.German, 3);
			langToKey.Add(Language.French, 4);
			langToKey.Add(Language.Spanish, 5);
			langToKey.Add(Language.Italian, 23);
			langToKey.Add(Language.Dutch, 24);
			langToKey.Add(Language.Estonian, 26);
			langToKey.Add(Language.Latvian, 27);
			langToKey.Add(Language.Japanese, 28);
			langToKey.Add(Language.Afrikaans, 31);
		}
	
		static SortedDictionary<Language, int> langToKey = new SortedDictionary<Language, int>();
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		public static string ConvertLanguage(Language language)
		{
			int result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result.ToString();
		}
		
		public static string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			string result =  "l1=" + ConvertLanguage(languagesPair.From) + 
				"&l2=" + ConvertLanguage(languagesPair.To);
			if(languagesPair.To == Language.Russian)	
				result += "&HL=2";
			else 	
				result += "&HL=1";
			return result;	
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www.multitran.ru/c/m.exe?s={0}&";
			
			query = string.Format(query, HttpUtility.UrlEncode(phrase, Encoding.GetEncoding(1251)));
			query += ConvertLanguagesPair(languagesPair);
			result.EditArticleUrl = query;
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet, Encoding.GetEncoding(1251));
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.IndexOf("ask in forum</a>") >= 0 || 
				responseFromServer.IndexOf("спросить в форуме</a>") >= 0 ||
				responseFromServer.IndexOf("<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">") < 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				//string translation = StringParser.Parse("<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">", "</table>", responseFromServer);
				string translation = StringParser.Parse("<div id=\"search_suggest\"></div>", "</table>", responseFromServer);
				//translation = translation.Replace("<B>", "");
				translation = translation.Replace("</td><tr>", "</td></tr><tr>");
				translation = translation.Replace("</a><tr>", "</a></td></tr><tr>");
				translation = translation.Replace("\"><tr>", "\"></td></tr><tr>");				
				
				
				StringParser parser = new StringParser(translation);
				string[] translations = parser.ReadItemsList("<tr>", "</td></tr>");
				
				string subpart;
				string subphrase = "";
				string subtranslation;
				string abbreviation;
				Result subres = null;
				Result subsubres = null;
				foreach(string part in translations)
				{
					subpart = part;
					//subpart = StringParser.RemoveAll("<td width=\"2%\">", "</td><td width=\"40%\">", subpart);
					//subpart = subpart.Replace("<td></td><td>", "");
					
					if(subpart.StartsWith("<td bgcolor=\"#EBEBEB\" width=\"100%\" colspan=\"2\">"))
					{ //new subres
						subpart = subpart.Replace("<td bgcolor=\"#EBEBEB\" width=\"100%\" colspan=\"2\">","");	
						subphrase = StringParser.Parse("\">", "</a>", subpart);
						abbreviation = StringParser.Parse("<em>", "</em>", subpart);
						subres = CreateNewResult(subphrase, languagesPair, subject);
						subres.Abbreviation = abbreviation;
						result.Childs.Add(subres);
					}
					else
					{
						abbreviation = StringParser.Parse("title=\"", "\"", subpart);
						abbreviation += "("; 
						abbreviation += StringParser.Parse("<i>", "</i>", subpart);
						abbreviation += ")";
						subsubres = CreateNewResult("", languagesPair, subject);
						subsubres.Abbreviation = abbreviation;
						subres.Childs.Add(subsubres);
						subtranslation = subpart.Substring(subpart.IndexOf("<td>") + 4);
						subtranslation = StringParser.RemoveAll("<span", ">", subtranslation);
						subtranslation = subtranslation.Replace("<a href=\"m.exe?t=", "<end><begin><a");
						subtranslation = StringParser.RemoveAll("<a", ">", subtranslation);
						subtranslation = subtranslation.Replace("</a>", "");
						subtranslation = subtranslation.Replace("<i>", "");
						subtranslation = subtranslation.Replace("</i>", "");
						subtranslation = subtranslation.Replace("</span>", "");
						subtranslation = subtranslation.Replace("<sub>", "");
						subtranslation = subtranslation.Replace("</sub>", "");
						subtranslation = subtranslation.Replace("<b>", "");
						subtranslation = subtranslation.Replace("</b>", "");
						
						subtranslation += "<end>";
						parser = new StringParser(subtranslation);
						string[] subtranslations = parser.ReadItemsList("<begin>", "<end>");
						foreach(string sub in subtranslations)
						{
							subtranslation = sub.Trim();
							if(subtranslation.EndsWith(";"))
								subtranslation = subtranslation.Substring(0, subtranslation.Length - 1);
							subsubres.Translations.Add(subtranslation);
						}
					}
				}
			}
			
		}		
	}
}
