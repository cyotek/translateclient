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
	/// Description of MultitranUtils.
	/// </summary>
	internal static class MultitranUtils
	{
		public static void InitServiceItem(ServiceItem serviceItem)
		{
			serviceItem.AddSupportedSubject(SubjectConstants.Common);
			
			serviceItem.AddSupportedTranslationToEnglish(Language.Russian);
			serviceItem.AddSupportedTranslationFromEnglish(Language.Russian);
			
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.German));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.German, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Spanish));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.French));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.French, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Dutch));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Italian));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Italian, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Latvian));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Latvian, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Estonian));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Estonian, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Japanese));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Japanese, Language.Russian));

			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Afrikaans));
			serviceItem.AddSupportedTranslation(new LanguagePair(Language.Afrikaans, Language.Russian));

			serviceItem.AddSupportedTranslationToEnglish(Language.German);
			serviceItem.AddSupportedTranslationFromEnglish(Language.German);

			serviceItem.AddSupportedTranslationToEnglish(Language.Japanese);
			serviceItem.AddSupportedTranslationFromEnglish(Language.Japanese);
			
			serviceItem.CharsLimit = 50;
			serviceItem.LinesLimit = 1;
		}
		
		static MultitranUtils()
		{
			//1 - English, 2 - Russian, 3 - German, 4 - French, 
			//5 - Spanish, 23 - Italian, 24 - Dutch, 26 - Estonian, 
			//27 - Latvian, 28 - Japanese, 31 - Afrikaans, 
			langToKey.Add(Language.English, 1);
			langToKey.Add(Language.English_GB, 1);
			langToKey.Add(Language.English_US, 1);
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

		static Encoding en1251 = Encoding.GetEncoding(1251);
		static Encoding enJapanese = Encoding.GetEncoding("euc-jp");
		
		public static Encoding GetEncoding(LanguagePair languagesPair)
		{
			if(languagesPair.From == Language.Japanese || languagesPair.To == Language.Japanese)
				return enJapanese;
			else
				return en1251;
		}
		public static string EncodePhrase(string phrase, LanguagePair languagesPair)
		{
			string tmp = HttpUtility.UrlEncode(HttpUtility.HtmlEncode(phrase), GetEncoding(languagesPair));
			StringBuilder sb = new StringBuilder();
			//uppercase all %xx codes 
			int pos = tmp.IndexOf("%");
			int prev_pos = 0;
			while(pos >= 0)
			{	
				sb.Append(tmp.Substring(prev_pos, pos - prev_pos));
				sb.Append(tmp.Substring(pos, 3).ToUpperInvariant());
				prev_pos = pos + 3;
				pos = tmp.IndexOf("%", pos + 3);
			}
			sb.Append(tmp.Substring(prev_pos));
			return sb.ToString();			
		}
		
		public static void DoTranslateDictionary(ServiceItem serviceItem, string url, string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = string.Format(url, EncodePhrase(phrase, languagesPair));
			query += ConvertLanguagesPair(languagesPair);
		
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet, GetEncoding(languagesPair));
			
			if(languagesPair.From == Language.Japanese || languagesPair.To == Language.Japanese)
				helper.StreamConvertor = new EucJPStreamFixer();
			
			string responseFromServer = helper.GetResponse();
			
			if((responseFromServer.IndexOf("ask in forum</a>") >= 0 
				&& responseFromServer.IndexOf("&nbsp;single words found") < 0) 
				||
				(responseFromServer.IndexOf("спросить в форуме</a>") >= 0 
					&& responseFromServer.IndexOf("&nbsp; найдены отдельные слова") < 0) 
				||
				responseFromServer.IndexOf("<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">") < 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				string translation = StringParser.Parse("createAutoComplete();", "</table>", responseFromServer);
				translation = translation.Replace("</TD>", "</td>");
				translation = translation.Replace("<TD width", "<td width");
				translation = translation.Replace("</td><tr>", "</td></tr><tr>");
				translation = translation.Replace("</a><tr>", "</a></td></tr><tr>");
				translation = translation.Replace("\"><tr>", "\"></td></tr><tr>");				
				translation = translation.Replace("</td>\r\n  </tr>", "</td></tr>");
				
				
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
					
					if(subpart.StartsWith("<td bgcolor=\"#DBDBDB\" width=\"100%\" colspan=\"2\">"))
					{ //new subres
						subpart = subpart.Replace("<td bgcolor=\"#DBDBDB\" width=\"100%\" colspan=\"2\">","");	
						subphrase = StringParser.Parse("\">", "</a>", subpart);
						subphrase = StringParser.RemoveAll("<span", ">", subphrase);
						subphrase = StringParser.RemoveAll("<a", ">", subphrase);
						subphrase = subphrase.Replace("<FONT SIZE=2>", "");
						subphrase = subphrase.Replace("</FONT>", "");
						
						
						if(subpart.Contains("<em>"))
							abbreviation = StringParser.Parse("<em>", "</em>", subpart);
						else
							abbreviation = "";
						subres = serviceItem.CreateNewResult(subphrase, languagesPair, subject);
						subres.Abbreviation = abbreviation;
						subres.ArticleUrl = query;
						//subres.ArticleUrlCaption = phrase;
						
						result.Childs.Add(subres);
					}
					else
					{
						if(!subpart.Contains("title=\""))
						{
							result.ResultNotFound = true;
							throw new TranslationException("Nothing found");
						}
						
						
						abbreviation = StringParser.Parse("title=\"", "\"", subpart);
						abbreviation += "("; 
						abbreviation += StringParser.Parse("<i>", "</i>", subpart);
						abbreviation += ")";
						//subsubres = serviceItem.CreateNewResult("", languagesPair, subject);
						//subsubres.Abbreviation = abbreviation;
						subsubres = serviceItem.CreateNewResult(abbreviation, languagesPair, subject);
						subsubres.ArticleUrl = "http://www.multitran.ru/c/" + StringParser.Parse("href=\"", "\"", subpart);
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
	
		public static void DoTranslatePhrases(ServiceItem serviceItem, string url, string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = string.Format(url, EncodePhrase(phrase, languagesPair));
			query += ConvertLanguagesPair(languagesPair);
			
		
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet, GetEncoding(languagesPair));
			
			result.ArticleUrl = query;
			result.ArticleUrlCaption = phrase;
			
			if(languagesPair.From == Language.Japanese || languagesPair.To == Language.Japanese)
				helper.StreamConvertor = new EucJPStreamFixer();
			
			string responseFromServer = helper.GetResponse();
			
			if((responseFromServer.IndexOf("ask in forum</a>") >= 0 
				&& responseFromServer.IndexOf("&nbsp;single words found") < 0) 
				||
				(responseFromServer.IndexOf("спросить в форуме</a>") >= 0 
					&& responseFromServer.IndexOf("&nbsp; найдены отдельные слова") < 0) 
				||
				responseFromServer.IndexOf("<table cellspacing=\"0\" border=\"0\" width=\"100%\">") < 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
			
				string translation = StringParser.Parse("createAutoComplete();", "<script><!--", responseFromServer);
				
				translation = translation.Replace("</TD>", "</td>");
				translation = translation.Replace("<TD width", "<td width");
				
				translation = translation.Replace("</td><tr>", "</td></tr><tr>");
				translation = translation.Replace("</a><tr>", "</a></td></tr><tr>");
				translation = translation.Replace("\"><tr>", "\"></td></tr><tr>");	
				translation = translation.Replace("</tr></td>", "</td></tr>");	
				
				
				
				StringParser parser = new StringParser(translation);
				string[] translations = parser.ReadItemsList("<tr>", "</td></tr>");
				
				string subpart;
				string subphrase = "";
				string subtranslation;
				string abbreviation;
				Result subsubres = null;
				string subres_url;
				foreach(string part in translations)
				{
					if(!part.Contains("<td width=\"5%\">"))
						continue;
					subpart = part;
					subphrase = StringParser.Parse("<td width=\"5%\">", "</td>", subpart);
					subres_url = StringParser.Parse("href=\"", "\"", subphrase);

					subphrase = StringParser.RemoveAll("<span", ">", subphrase);
					subphrase = StringParser.RemoveAll("<a", ">", subphrase);
					subphrase = subphrase.Replace("</a>", "");
					subphrase = subphrase.Replace("<i>", "");
					subphrase = subphrase.Replace("</i>", "");
					subphrase = subphrase.Replace("</span>", "");
					subphrase = subphrase.Replace("<sub>", "");
					subphrase = subphrase.Replace("</sub>", "");
					subphrase = subphrase.Replace("<b>", "");
					subphrase = subphrase.Replace("</b>", "");
					
					abbreviation = StringParser.Parse("<i>", "</i>", subpart);
					
					subsubres = serviceItem.CreateNewResult(subphrase, languagesPair, subject);
					subsubres.Abbreviation = abbreviation;
					subsubres.ArticleUrl = "http://www.multitran.ru/c/" + subres_url;
					
					result.Childs.Add(subsubres);

					subtranslation = subpart + "<end>";
					subtranslation = StringParser.Parse("<td width=\"20%\">", "</td>", subtranslation);
					subtranslation = StringParser.RemoveAll("<span", ">", subtranslation);
					subtranslation = StringParser.RemoveAll("<a", ">", subtranslation);
					subtranslation = StringParser.RemoveAll("<td", ">", subtranslation);
					subtranslation = subtranslation.Replace("</td>", " ");
					subtranslation = subtranslation.Replace("</a>", "");
					subtranslation = subtranslation.Replace("<i>", "");
					subtranslation = subtranslation.Replace("</i>", "");
					subtranslation = subtranslation.Replace("</span>", "");
					subtranslation = subtranslation.Replace("<sub>", "");
					subtranslation = subtranslation.Replace("</sub>", "");
					subtranslation = subtranslation.Replace("<b>", "");
					subtranslation = subtranslation.Replace("</b>", "");
					
					subsubres.Translations.Add(subtranslation);
					
					
					subtranslation = subpart + "<end>";
					subtranslation = StringParser.Parse("<td width=\"75%\">", "<end>", subtranslation);
					subtranslation = StringParser.RemoveAll("<span", ">", subtranslation);
					subtranslation = StringParser.RemoveAll("<a", ">", subtranslation);
					subtranslation = StringParser.RemoveAll("<td", ">", subtranslation);
					subtranslation = subtranslation.Replace("</td>", " ");
					subtranslation = subtranslation.Replace("</a>", "");
					subtranslation = subtranslation.Replace("<i>", "");
					subtranslation = subtranslation.Replace("</i>", "");
					subtranslation = subtranslation.Replace("</span>", "");
					subtranslation = subtranslation.Replace("<sub>", "");
					subtranslation = subtranslation.Replace("</sub>", "");
					subtranslation = subtranslation.Replace("<b>", "");
					subtranslation = subtranslation.Replace("</b>", "");
					
					subsubres.Translations.Add(subtranslation);
					

				}
			}		
		}	
		
		public static void DoTranslateSentences(ServiceItem serviceItem, string url, string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = string.Format(url, HttpUtility.UrlEncode(phrase, Encoding.GetEncoding(1251)));
			query += ConvertLanguagesPair(languagesPair);
			
		
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet, Encoding.GetEncoding(1251));
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.IndexOf("</form>") < 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				result.EditArticleUrl = query;
				string translation = StringParser.Parse("</form>", "<table", responseFromServer);
				
				translation = translation.Replace("<p>", "<end><begin>");
				translation += "<end>";
				
				
				StringParser parser = new StringParser(translation);
				string[] translations = parser.ReadItemsList("<begin>", "<end>");
				
				string subpart;
				string subphrase = "";
				string subtranslation;
				Result subsubres = null;
				foreach(string part in translations)
				{
					subpart = part;
					if(subpart.Contains("title=\""))
						subphrase = StringParser.Parse("title=\"", "\"", subpart);
					else
						subphrase = phrase;
					
					subtranslation = StringParser.RemoveAll("<span", ">", subpart);
					subtranslation = StringParser.RemoveAll("<a", ">", subtranslation);
					subtranslation = subtranslation.Replace("</a>", "");
					subtranslation = subtranslation.Replace("<i>", "");
					subtranslation = subtranslation.Replace("</i>", "");
					subtranslation = subtranslation.Replace("</span>", "");
					subtranslation = subtranslation.Replace("<sub>", "");
					subtranslation = subtranslation.Replace("</sub>", "");
					subtranslation = subtranslation.Replace("<b>", "");
					subtranslation = subtranslation.Replace("</b>", "");
					subtranslation = subtranslation.Replace(">>", "");
					
					
					subsubres = serviceItem.CreateNewResult(subphrase, languagesPair, subject);
					result.Childs.Add(subsubres);
					int idx = subtranslation.IndexOf("\n");
					if(idx < 0)
						subsubres.Translations.Add("Parse error. Can't found '\\n' in " + subtranslation);
					else
					{
						subsubres.Translations.Add(subtranslation.Substring(0, idx));
						subsubres.Translations.Add(subtranslation.Substring(idx + 1).Trim());
					}
				}
			}		
		}			
	}
}
