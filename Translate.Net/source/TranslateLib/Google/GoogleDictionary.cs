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
using System.Diagnostics.CodeAnalysis;
using System.Web; 
using System.Text; 
using System.Globalization;


namespace Translate
{
	/// <summary>
	/// Description of WordTranslator.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class GoogleDictionary : BilingualDictionary
	{
		public GoogleDictionary()
		{
			CharsLimit = 50;
			LinesLimit = 1;
			Name = "_dictionary";
		
			AddSupportedTranslation(new LanguagePair(Language.English, Language.English));
		
			AddSupportedTranslation(new LanguagePair(Language.English, Language.French));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.French, Language.French));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.German, Language.German));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.Italian));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Korean));
			AddSupportedTranslation(new LanguagePair(Language.Korean, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Spanish));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.Spanish));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Russian));
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese_TW));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.Chinese_TW));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese_CN));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_CN, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_CN, Language.Chinese_CN));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese));
			AddSupportedTranslation(new LanguagePair(Language.Chinese, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.Portuguese));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Hindi));
			AddSupportedTranslation(new LanguagePair(Language.Hindi, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Dutch, Language.Dutch));
			
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Arabic));
			AddSupportedTranslation(new LanguagePair(Language.Arabic, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Czech));
			AddSupportedTranslation(new LanguagePair(Language.Czech, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Thai));
			AddSupportedTranslation(new LanguagePair(Language.Thai, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Bulgarian));
			AddSupportedTranslation(new LanguagePair(Language.Bulgarian, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Croatian));
			AddSupportedTranslation(new LanguagePair(Language.Croatian, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Finnish));
			AddSupportedTranslation(new LanguagePair(Language.Finnish, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Hebrew));
			AddSupportedTranslation(new LanguagePair(Language.Hebrew, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Greek));
			AddSupportedTranslation(new LanguagePair(Language.Greek, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Serbian));
			AddSupportedTranslation(new LanguagePair(Language.Serbian, Language.English));
			
			AddSupportedSubject(SubjectConstants.Common);
		}
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www.google.com/dictionary?aq=f&langpair={1}&q={0}&hl=en";
			query = string.Format(query, HttpUtility.UrlEncode(phrase, System.Text.Encoding.UTF8 ), GoogleUtils.ConvertLanguagesPair(languagesPair));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
			
			result.ArticleUrl = query;
			result.ArticleUrlCaption = phrase;
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains("No dictionary translations were found for: <strong>"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			if(responseFromServer.Contains("No dictionary definitions were found for: <strong>"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			result.HasAudio = responseFromServer.Contains("<object data=\"/dictionary/flash");
			responseFromServer = StringParser.Parse("<div class=\"dct-srch-otr\">", "<div class=\"dct-rt-sct\">", responseFromServer);
			
			//pronuncation
			if(responseFromServer.Contains("<span class=\"phn\">"))
			{
				string pronuncation = StringParser.Parse("<span class=\"phn\">", "</span>", responseFromServer);
				pronuncation = StringParser.Parse("[", "]", pronuncation).Trim();
				result.Abbreviation = pronuncation;
			}
			
			
			//translations
			//string translations = StringParser.Parse("<div class=\"dct-srch-rslt\">", "</div>", responseFromServer);
			

			
			string translations = "";

			//TODO: additional sences like in "water" - "water down" not supported
			
			if(responseFromServer.Contains("<div class=\"sen\">"))
				translations = StringParser.Parse("<ul class=\"dfnt\">", "</ul>\n<div class=\"sen\">", responseFromServer);
			else if(responseFromServer.Contains("<h3>Related phrases</h3>"))
				translations = StringParser.Parse("<ul class=\"dfnt\">", "</ul>\n<h3>Related phrases</h3>", responseFromServer);
			else if(responseFromServer.Contains("<h3>Web definitions</h3>"))	
				translations = StringParser.Parse("<ul class=\"dfnt\">", "</ul>\n<h3>Web definitions</h3>", responseFromServer);
			else	
				translations = StringParser.Parse("<ul class=\"dfnt\">", "</ul>", responseFromServer);
			//translations = StringParser.Parse("<ol>", "</ol>", responseFromServer);
			//translations = translations.Replace("</h4>", "</h4></li>");

			
			StringParser parser = new StringParser(translations);
			string[] subtranslation_list = parser.ReadItemsList("<h4>", "</ol>", "3485730457203");
			
			Result subres_tr = null;
			Result subsubres_tr = null;
			
			string subtrans_str;
			string abbr_str;
			
			foreach(string subtrans_s in subtranslation_list)
			{
				subtrans_str = subtrans_s;
				abbr_str = StringParser.ExtractLeft("</h4>", subtrans_str);
				subres_tr = CreateNewResult(abbr_str, languagesPair, subject);
				result.Childs.Add(subres_tr);
				
				abbr_str = StringParser.Parse("</h4>", "<ol>", subtrans_str);
				abbr_str = StringParser.RemoveAll("<", ">", abbr_str);
				subres_tr.Abbreviation = abbr_str.Trim();
				
				subtrans_str = subtrans_str.Replace("<span class=\"mn\">", "<end><begin>");
				subtrans_str += "<end>";
				
				StringParser childsParser = new StringParser(subtrans_str);
				string[] childs_list = childsParser.ReadItemsList("<begin>", "<end>", "3485730457203");
				foreach(string child_s in childs_list)
				{
					if(!child_s.Contains("</ul>")) 
					{ //simple translation
						subtrans_str = StringParser.RemoveAll("<", ">", child_s);
						subres_tr.Translations.Add(subtrans_str.Trim());
					}
					else
					{ //more deep case, like English->English
						abbr_str = StringParser.ExtractLeft("<ul", child_s);
						abbr_str = StringParser.RemoveAll("<", ">", abbr_str);
						subsubres_tr = CreateNewResult(abbr_str, languagesPair, subject);
						subres_tr.Childs.Add(subsubres_tr);
				
						subtrans_str = child_s;
						if(subtrans_str.EndsWith("<li>"))
							subtrans_str = subtrans_str.Substring(0, subtrans_str.Length - 4);
						childsParser = new StringParser(subtrans_str);
						string[] subsubsubtranslation_list = childsParser.ReadItemsList("<li>", "</li>", "3485730457203");
						foreach(string subsubsubtrans_s in subsubsubtranslation_list)
						{
							subtrans_str = subsubsubtrans_s;
							subtrans_str = StringParser.RemoveAll("<", ">", subtrans_str);
							subsubres_tr.Translations.Add(subtrans_str.Trim());
						}
					
					}
				}
			}
			
			//related words
			if(responseFromServer.Contains("<h3>Related phrases</h3>"))
			{
				string related = StringParser.Parse("<ul class=\"rlt-snt\">", "</ul>", responseFromServer);
				if(!string.IsNullOrEmpty(related))
				{				
					parser = new StringParser(related);
					string[] related_list = parser.ReadItemsList("<li>", "</li>");
					
					foreach(string related_s in related_list)
					{
						string related_str = related_s;
						related_str = related_str.Replace("</dfn>", "");
						related_str = StringParser.RemoveAll("<span", ">", related_str);
						related_str = related_str.Replace("</span>", "");
						related_str = related_str.Replace("<strong>", "");
						related_str = related_str.Replace("</strong>", "");
						related_str = related_str.Replace("<br />", "");
						related_str = related_str.Replace("<br>", "");
					
						int translationIdx = related_str.IndexOf("<dfn>");
						if(translationIdx < 0)
							throw new TranslationException("Can't found '<dfn>' tag in string : " + related_str);
							
						string subphrase = related_str.Substring(0, translationIdx).Replace("\n", "").Trim(); 
						subphrase = StringParser.RemoveAll("<", ">", subphrase);
						
						string subphrasetrans = related_str.Substring(translationIdx + 5); 
						subphrasetrans = StringParser.RemoveAll("<", ">", subphrasetrans);
						subphrasetrans = subphrasetrans.Replace("&nbsp", " ").Replace("\n", "").Trim(); 
						
						Result subres = CreateNewResult(subphrase, languagesPair, subject);
						subres.Translations.Add(subphrasetrans);
						result.Childs.Add(subres);
					}
				}
			}
			
			//Web definitions
			if(responseFromServer.Contains("<h3>Web definitions</h3>"))
			{
				string related = StringParser.Parse("<ul class=\"gls\">", "</div>", responseFromServer);
				if(!string.IsNullOrEmpty(related))
				{				
					related = StringParser.Parse("<ul>", "<div>", related);
					if(!string.IsNullOrEmpty(related))
					{				
					
						Result subres_wd = CreateNewResult(phrase, languagesPair, subject);
						result.Childs.Add(subres_wd);
					
						parser = new StringParser(related);
						string[] related_list = parser.ReadItemsList("<li>", "</li>");
						
						foreach(string related_s in related_list)
						{
							string related_str = related_s;
							related_str = related_str.Replace("<br/>", "").Trim();
							subres_wd.Translations.Add(related_str);
						}
					}
				}
			}
		
		}
		

		
		
	} 
}
