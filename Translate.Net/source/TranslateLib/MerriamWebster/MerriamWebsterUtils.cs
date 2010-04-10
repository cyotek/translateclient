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
using System.Collections.Generic;


namespace Translate
{
	/// <summary>
	/// Description of MerriamWebsterUtils.
	/// </summary>
	public static class MerriamWebsterUtils
	{
	
		public static void SetResult(Result result, string data, string host)
		{
		
			if(data.IndexOf("</strong> can be found at Merriam-WebsterUnabridged.com.") > 0)
			{
				result.Translations.Add(result.Phrase);
				return;
			}
			
			if(data.Contains("No entries found that match "))
			{
				result.Translations.Add(result.Phrase);
				return;
			}
			
			result.HasAudio = data.Contains("title=\"Listen to the pronunciation of");
			
			if(data.IndexOf("<div>Function: ") > 0)
			{
				string abbr = StringParser.Parse("<div>Function: ", "</div>", data);
				abbr = abbr.Replace("<em>", "");
				abbr = abbr.Replace("</em>", "");
				abbr = abbr.Replace("<span class=\"fl\">", "");
				abbr = abbr.Replace("</span>", "");
				
				result.Abbreviation = abbr;
			}
			
			if(data.IndexOf("<div>Pronunciation: ") > 0)
			{
				string abbr = StringParser.Parse("<div>Pronunciation: ", "</div>", data);
				abbr = StringParser.RemoveAll("<span", ">", abbr);
				abbr = abbr.Replace("</span>", "");
				abbr = abbr.Replace("<em>", "");
				abbr = abbr.Replace("</em>", "");
				if(!string.IsNullOrEmpty(result.Abbreviation))
					result.Abbreviation += ", ";
				result.Abbreviation += abbr;
			}
			
			if(data.IndexOf("<dd class=\"use\">") > 0)
			{
				string abbr = StringParser.Parse("<dd class=\"use\">", "</dd>", data);
				abbr = abbr.Replace("<em>", "");
				abbr = abbr.Replace("</em>", "");
				if(!string.IsNullOrEmpty(result.Abbreviation))
					result.Abbreviation += ", ";
				result.Abbreviation += "Usage: " + abbr;
			}

			if(data.IndexOf("<div>Etymology: ") > 0)
			{
				string abbr = StringParser.Parse("<div>Etymology: ", "</div>", data);
				abbr = abbr.Replace("<em>", "(");
				abbr = abbr.Replace("</em>", ")");
				abbr = StringParser.RemoveAll("<a href", ">", abbr);
				abbr = abbr.Replace("</a>", "");

				if(!string.IsNullOrEmpty(result.Abbreviation))
					result.Abbreviation += ", ";
				result.Abbreviation += "Etymology: " + abbr;
			}

			if(data.IndexOf("<div>Date: ") > 0)
			{
				string abbr = StringParser.Parse("<div>Date: ", "</div>", data);
				abbr = abbr.Replace("<em>", "(");
				abbr = abbr.Replace("</em>", ")");
				if(!string.IsNullOrEmpty(result.Abbreviation))
					result.Abbreviation += ", ";
				result.Abbreviation += "Date: " + abbr;
			}
			
			if(!data.Contains("class=\"d\">") && data.Contains("<span class=\"variant\">"))
			{  //variant, like a blew - past of blow
				string variant = StringParser.Parse("<span class=\"variant\">", "</div>", data);
				SetAdditionalLinks(result, variant, host);
				variant = StringParser.RemoveAll("<span", ">", variant);
				variant = StringParser.RemoveAll("<a href", ">", variant);
				variant = variant.Replace("</a>", "");
				variant = variant.Replace("</span>", "");
				variant = variant.Replace("<em>", "");
				variant = variant.Replace("</em>", "");
				variant = variant.Replace("</dd>", " ");
				variant = variant.Replace("</dl>", "");
				
				result.Translations.Add(variant.Trim());
				return;
			}
			
			string defs = StringParser.Parse("class=\"d\">", "<script", data);
			defs = defs.Replace(" a</span>", " a ");
			defs = defs.Replace("</span>", "");
			defs = defs.Replace("<em>", "");
			defs = defs.Replace("</em>", "");
			defs = defs.Replace("<strong>Synonyms</strong>", "Synonyms: ");
			defs = defs.Replace("<strong>Related Words</strong>", "Related Words: ");
			defs = defs.Replace("<strong>Antonyms</strong>", "Antonyms: ");
			//defs = defs.Replace("<strong>", "");
			//defs = defs.Replace("</strong>", "");
			defs = defs.Replace(":�<a", "- <a");
			
			defs = defs.Replace("<span class=\"dxn\">", "");
			SetAdditionalLinks(result, defs, host);
			defs = StringParser.RemoveAll("<a href", ">", defs);
			defs = defs.Replace("</a>", "");
			defs = defs.Replace("<span class=\"sense_content\">", "");
			//defs = StringParser.RemoveAll("<span class=\"sense_label\">", ">", defs);
			defs = defs.Replace("<span>", "");
			defs = defs.Replace("�", "");
			defs = defs.Replace("<span class=\"vi\">", "");
			defs = defs.Replace("<span class=\"text\">", "");
			
			defs = defs.Replace("<span class=\"\n            sense_label", "<span class=\"sense_label");
			defs = defs.Replace("<span class=\"unicode\">", "");
			defs = defs.Replace("ʼ", "'");
			
			
			
			
			
			if(defs.IndexOf("<br/>") < 0)
			{ //single def
				if(defs.IndexOf("<span class=\"syn\">") < 0 &&
				   defs.IndexOf("<span class=\"rel\">") < 0 &&
				   defs.IndexOf("<span class=\"ant\">") < 0
				)
				{
					defs = StringParser.RemoveAll("<", ">", defs);
					result.Translations.Add(defs.Trim());
					return;
				}
			}
			
			
			defs = "<start>" + defs;
			{
				defs = defs.Replace("<span class=\"verb_class\">", "<end><start>");
				defs = defs.Replace("<div class=\"synonym\">", "<end><start>");
				defs = defs.Replace("<span class=\"syn\">", "<end><start>");
				defs = defs.Replace("<span class=\"rel\">", "<end><start>");
				defs = defs.Replace("<span class=\"ant\">", "<end><start>");
				defs = defs.Replace("<br/>", "<end><start>");
				defs += "<end>"; //ending mark
				
				StringParser parser = new StringParser(defs);
				string[] defs_items = parser.ReadItemsList("<start>", "<end>");
				string translation;
				foreach(string item in defs_items)
				{
					translation = item.Trim();
					/*if(translation[0] <= '9' && translation[0] >= '0')
						translation = item.Substring(1).Trim(); //skip idx
					
					if(translation[0] <= '9' && translation[0] >= '0')
						translation = translation.Substring(1).Trim(); //skip idx
					
					if(translation.StartsWith(":"))
						translation = translation.Substring(1); 
					*/
						
					if(translation.StartsWith("<strong>"))
						translation = StringParser.ExtractRight("</strong>", translation);
						
					string subsense_tag = "<em class=\"su\">";
					if(false && translation.IndexOf(subsense_tag) > 0)
					{ //extract senses
						string toptranslaton = translation.Substring(0, translation.IndexOf(subsense_tag));
						toptranslaton = StringParser.RemoveAll("<", ">", toptranslaton);
						if(toptranslaton.StartsWith("a :"))
							toptranslaton = toptranslaton.Substring(3);
							
						result.Translations.Add(toptranslaton.Trim());
					
						translation = translation.Substring(translation.IndexOf(subsense_tag));
						translation = translation.Replace(subsense_tag, "<end>" + subsense_tag);
						translation += "<end>";
						
						StringParser subparser = new StringParser(translation);
						string[] sub_defs_items = subparser.ReadItemsList(subsense_tag, "<end>");
						foreach(string subtranslation in sub_defs_items)
						{
							translation = subtranslation.Substring(1).Trim(); //skip idx
							if(translation.StartsWith(":"))
								translation = translation.Substring(1); //skip idx

							translation = StringParser.RemoveAll("<", ">", translation);
							result.Translations.Add(translation.Trim());
						}
					}
					else
					{
						translation = StringParser.RemoveAll("<", ">", translation);
						result.Translations.Add(translation.Trim());
					}
				}
			}	
		}

		public static void SetAdditionalLinks(Result result, string data, string host)		
		{
			string[] urls = StringParser.ParseItemsList("<a href=\"", "</a>", data);
			string link;
			string text;
			List<string> texts = new List<string>();
			
			foreach(string url in urls)
			{
				link = StringParser.ExtractLeft("\"", url);
				link = link.Replace(" ", "+");
				if(link.StartsWith("/"))
					link = "http://" + (new Uri(host)).Host + link;
				else
					link = host + link;
					
				text = StringParser.ExtractRight(">", url);
				if(text.EndsWith(" 1"))
					text = StringParser.ExtractLeft(" 1", text);
				else if(text.EndsWith(" 2"))
					text = StringParser.ExtractLeft(" 2", text);
				
				if(!texts.Contains(text))
				{
					result.RelatedLinks.Add(text, new Uri(link));
					texts.Add(text);
				}	
			}
		}
		
		public static void DoTranslate(ServiceItem serviceItem, string host, string bookName, string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
		
			string query = host + "/{0}";
			//ISO-8859-1
			query = string.Format(query, HttpUtility.UrlEncode(phrase, System.Text.Encoding.UTF8 ));
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
			//helper.UseGoogleCache = true;
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.IndexOf("The word you've entered isn't in the dictionary.") >= 0
				|| responseFromServer.IndexOf("No entries found.\n<br/>") >= 0
			    || responseFromServer.IndexOf("The word you've entered isn't in the thesaurus.") >= 0
			   )
			{
				if(responseFromServer.IndexOf("<PRE>") >= 0)
				{	//suggestions
					StringParser suggestions_parser = new StringParser("<PRE>", "</PRE>", responseFromServer);
					string[] suggestions = suggestions_parser.ReadItemsList(">", "</a>");
					foreach(string item in suggestions)
					{
						string part = item;
						string link = "html!<p><a href=\""+ host + "/{0}\" title=\""+ host + "/{0}\">{0}</a></p>";
						link = string.Format(link,
							part);
						result.Translations.Add(link);
					}
					return;
				}
				else
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
			}
			
			result.ArticleUrl = host + "/" + phrase;
			if(responseFromServer.Contains("One entry found.\n<br/>") || responseFromServer.Contains("One entry found.\n<br />"))
			{
				SetResult(result, responseFromServer, host + "/");
			}
			else
			{
				if(responseFromServer.Contains("'list' value=\"va:"))
				{
					string count_str = StringParser.Parse("'list' value=\"va:", ",", responseFromServer);
					int count;
					if(int.TryParse(count_str, out count))
						result.MoreEntriesCount = count;
				}
			
				StringParser parser = new StringParser("<ol class=\"results\"", "</ol>", responseFromServer);
				string[] items = parser.ReadItemsList("href=\"/" + bookName.ToLower() + "/", "\"");
				
				bool first = true;
				foreach(string item in items)
				{
					string part = item;
					
					string part_name = StringParser.RemoveAll("[", "]", part);
					if(string.Compare(part_name, phrase, true) == 0)
					{  
						Result subres = serviceItem.CreateNewResult(part_name, languagesPair, subject);
						result.Childs.Add(subres);

						subres.ArticleUrl = host + "/" + part;

						if(first)
						{
							SetResult(subres, responseFromServer, host + "/");
							first = false;
						}	
						else
						{ //get translation 
							//jump=blood%5B1%2Cnoun%5D&book=Dictionary&quer=blood&list=45%2C1%2C3602592%2C0%3Bblood%5B1%2Cnoun%5D%3D113554%3Bblood%5B2%2Ctransitive+verb%5D%3D113572%3BABO+blood+group%3D2000002431%3Bbad+blood%3D2000074812%3Bblood-and-guts%3D2000113598%3Bblood-brain+barrier%3D2000113627%3Bblood+brother%3D2000113664%3Bblood+cell%3D2000113685%3Bblood+count%3D2000113697%3Bblood+doping%3D2000113725
							
							/*string quer_value = StringParser.Parse("<input type='hidden' name='quer' value=\"", "\"", responseFromServer);
							string list_value = StringParser.Parse("<input type='hidden' name='list' value=\"", "\"", responseFromServer);
							string post_data_value = "jump={0}&book=" + bookName + "&quer={1}&list={2}";
							post_data_value = string.Format(post_data_value , 
								HttpUtility.UrlEncode(part),
								HttpUtility.UrlEncode(quer_value),
								HttpUtility.UrlEncode(list_value)
								);
							*/	
							
							helper = 
								new WebRequestHelper(result, new Uri(subres.ArticleUrl), 
									networkSetting, 
									WebRequestContentType.UrlEncodedGet);
							//helper.AddPostData(post_data_value);				
							//helper.UseGoogleCache = true;
							responseFromServer = helper.GetResponse();
							SetResult(subres, responseFromServer, host + "/");
						}
					}
				}
				
				if(result.MoreEntriesCount != 0)
					result.MoreEntriesCount -= result.Childs.Count;
			}
		}
	
	}
}
