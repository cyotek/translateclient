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
using System.Collections.Generic; 


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
			if(responseFromServer.Contains("<span class=\"dct-tp\">/"))
			{
				string pronuncation = StringParser.Parse("<span class=\"dct-tp\">/", "/</span>", responseFromServer);
				pronuncation = pronuncation.Trim();
				result.Abbreviation = pronuncation;
			}
			
			
			//translations
			//string translations = StringParser.Parse("<div class=\"dct-srch-rslt\">", "</div>", responseFromServer);
			

			
			string translations = "";

			//TODO: additional sences like in "water" - "water down" not supported
			
			if(responseFromServer.Contains("<div class=\"sen\">"))
				translations = StringParser.Parse("<ul class=\"dct-e2\" id=\"pr-root\" >", "</ul>\n<div class=\"sen\">", responseFromServer);
			else if(responseFromServer.Contains("<h3>Related phrases</h3>"))
				translations = StringParser.Parse("<ul class=\"dct-e2\" id=\"pr-root\" >", "</ul>\n<h3>Related phrases</h3>", responseFromServer);
			else if(responseFromServer.Contains("<h3>Web definitions</h3>"))	
				translations = StringParser.Parse("<ul class=\"dct-e2\" id=\"pr-root\" >", "</ul>\n<h3>Web definitions</h3>", responseFromServer);
			else	
				translations = StringParser.Parse("<ul class=\"dct-e2\" id=\"pr-root\" >", "</ul>", responseFromServer);

			
			StringParser parser = null;
			List<string> subtranslations = new List<string>();
			if(translations.Contains("<li class=\"dct-ec\""))
			{
				//"</li>\n</ul>\n</li>"
				parser = new StringParser(translations);
				string[] subtranslation_list = parser.ReadItemsList("<li class=\"dct-ec\"", "</li>\n</ul>\n</li>", "3485730457203");
				subtranslations.AddRange(subtranslation_list);
			}
			else if(translations.Contains("<div style=\"font-weight:bold\">Synonyms:</div>"))
			{
				Result synonyms_tr = CreateNewResult("Synonyms", languagesPair, subject);
				result.Childs.Add(synonyms_tr);
				
				string synonyms = StringParser.Parse("<div style=\"font-weight:bold\">Synonyms:</div>", "</div>", translations);
				parser = new StringParser(synonyms);
				string[] syn_group_list = parser.ReadItemsList("<li>", "</li>", "3485730457203");
				foreach(string syngroup in syn_group_list)
				{
					string syn_group_name = StringParser.Parse("title=\"Part-of-speech\">", "</span>", syngroup);
					Result syn_tr = CreateNewResult(syn_group_name, languagesPair, subject);
					synonyms_tr.Childs.Add(syn_tr);
					parser = new StringParser(syngroup);
					string[] syn_list = parser.ReadItemsList("<a", "</a>", "3485730457203");
					foreach(string syn in syn_list)
					{
						string synonym = StringParser.ExtractRight(">", syn);
						syn_tr.Translations.Add(synonym);
					}
				}

				subtranslations.Add(translations);
			}
			else	
			{
				subtranslations.Add(translations);
			}
			
			Result subres_tr = result;
			Result sub2res_tr = null;
			Result sub3res_tr = null;
			string abbr_str;
			
			foreach(string subtranslation in subtranslations)
			{
				if(subtranslation.Contains("<div  class=\"dct-ec\">"))
				{
					abbr_str = StringParser.Parse("title=\"Part-of-speech\">", "</span>", subtranslation);
					subres_tr = CreateNewResult(abbr_str, languagesPair, subject);
					result.Childs.Add(subres_tr);
				}
				
				parser = new StringParser(subtranslation.Replace("<li class=\"dct-em\"", "<end><begin>") + "<end>");
				string[] subsubtranslation_list = parser.ReadItemsList("<begin>", "<end>", "3485730457203");
				
				foreach(string subsubtanslation in subsubtranslation_list)
				{
					sub2res_tr = CreateNewResult("", languagesPair, subject);
					subres_tr.Childs.Add(sub2res_tr);
					
					if(subsubtanslation.Contains(">See also</span>"))
						sub2res_tr.Translations.Add("See also");	
					
					StringParser parser2 = new StringParser(subsubtanslation.Replace("<span class=\"dct-tt\">", "<end><begin>") + "<end>");
					string[] sub3translation_list = parser2.ReadItemsList("<begin>", "<end>", "3485730457203");
					
					foreach(string sub3tanslation in sub3translation_list)
					{
						string text_translation = "";
						string text_abbr = "";
						if(sub3tanslation.Contains("<span") )
						{
							text_translation = StringParser.ExtractLeft("<span", sub3tanslation);
							if(text_translation.Contains("</span") )
								text_translation = StringParser.ExtractLeft("</span", text_translation);
							text_abbr = StringParser.Parse("<span", "</span" , sub3tanslation);
							text_abbr = StringParser.ExtractRight(">", text_abbr);
						}
						else
							 text_translation = StringParser.ExtractLeft("</span>", sub3tanslation);
						
						text_translation = StringParser.RemoveAll("<", ">", text_translation);
						
						if(sub2res_tr.Translations.Count == 0)
						{
							sub2res_tr.Translations.Add(text_translation);
							sub2res_tr.Abbreviation = text_abbr;
						}
						else
						{
							sub3res_tr = CreateNewResult("", languagesPair, subject);
							sub3res_tr.Translations.Add(text_translation);
							//sub3res_tr.Phrase = text_abbr;
							sub2res_tr.Childs.Add(sub3res_tr);
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
						string related_str = related_s.Replace("\n", "").Trim();
						string subphrase = StringParser.Parse("<div>", "</div>", related_str);
						subphrase = StringParser.RemoveAll("<", ">", subphrase);
						subphrase = subphrase.Replace("&nbsp", " ").Replace("\n", "").Trim(); 
						
						
						string subphrasetrans = StringParser.ExtractRight("</div>", related_str);
						
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
				string related = StringParser.ExtractRight("<ul class=\"gls\">", responseFromServer);
				if(!string.IsNullOrEmpty(related))
				{				
					{				
					
						Result subres_wd = CreateNewResult(phrase, languagesPair, subject);
						result.Childs.Add(subres_wd);
					
						parser = new StringParser(related);
						string[] related_list = parser.ReadItemsList("<li>", "</li>");
						
						foreach(string related_s in related_list)
						{
							string related_str = related_s;
							related_str = related_str.Replace("<br/>", "").Trim();
							related_str = StringParser.RemoveAll("<", ">", related_str);
							related_str = related_str.Replace("&nbsp", " ").Replace("\n", "").Trim(); 
							subres_wd.Translations.Add(related_str);
						}
					}
				}
			}
		
		}
		

		
		
	} 
}
