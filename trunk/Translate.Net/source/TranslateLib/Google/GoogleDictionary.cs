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
		
			AddSupportedTranslation(new LanguagePair(Language.English, Language.French));
			AddSupportedTranslation(new LanguagePair(Language.French, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Korean));
			AddSupportedTranslation(new LanguagePair(Language.Korean, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Spanish));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Russian));
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese_TW));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_TW, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese_CN));
			AddSupportedTranslation(new LanguagePair(Language.Chinese_CN, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Chinese));
			AddSupportedTranslation(new LanguagePair(Language.Chinese, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.English));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Hindi));
			AddSupportedTranslation(new LanguagePair(Language.Hindi, Language.English));
			
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
			
			if(responseFromServer.IndexOf("No dictionary translations were found for: <strong>") >= 0)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
			result.HasAudio = responseFromServer.Contains("<object data=\"/dictionary/flash");
			responseFromServer = StringParser.Parse("<div id=\"dct-srch-otr\">", "<div id=\"dct-rt-sct\">", responseFromServer);
			
			//translations
			string translations = StringParser.Parse("<ul id=\"dfnt\">", "</ul>", responseFromServer);
			translations = StringParser.Parse("<ol>", "</ol>", responseFromServer);
			
			StringParser parser = new StringParser(translations);
			string[] subtranslation_list = parser.ReadItemsList("<li>", "</li>", "3485730457203");
			
			Result subres_tr = CreateNewResult(phrase, languagesPair, subject);
			result.Childs.Add(subres_tr);
			
			foreach(string subtrans_s in subtranslation_list)
			{
				string subtrans_str = subtrans_s;
				subtrans_str = StringParser.RemoveAll("<", ">", subtrans_str);
				subres_tr.Translations.Add(subtrans_str.Trim());
			}
			
			//related words
			if(responseFromServer.Contains("<h3>Related phrases</h3>"))
			{
				string related = StringParser.Parse("<ul id=\"rlt-snt\">", "</ul>", responseFromServer);
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
					
						int translationIdx = related_str.IndexOf("<dfn>");
						if(translationIdx < 0)
							throw new TranslationException("Can't found '<dfn>' tag in string : " + related_str);
							
						string subphrase = related_str.Substring(0, translationIdx); 
						string subphrasetrans = related_str.Substring(translationIdx + 5); 
						subphrasetrans = StringParser.RemoveAll("<", ">", subphrasetrans);
						subphrasetrans = subphrasetrans.Replace("&nbsp", " ");
						
						Result subres = CreateNewResult(subphrase, languagesPair, subject);
						subres.Translations.Add(subphrasetrans);
						result.Childs.Add(subres);
					}
				}
			}
			
			//Web definitions
			if(responseFromServer.Contains("<h3>Web definitions</h3>"))
			{
				string related = StringParser.Parse("<ul id=\"gls\">", "</div>", responseFromServer);
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
