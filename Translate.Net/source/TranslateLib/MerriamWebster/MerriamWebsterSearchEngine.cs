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
	/// Description of MerriamWebsterTranslator.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class MerriamWebsterDictionarySearchEngine : MonolingualSearchEngine
	{
		public MerriamWebsterDictionarySearchEngine()
		{
			CharsLimit = 50;
			Name = "_dict_search";
		
			AddSupportedTranslation(new LanguagePair(Language.English, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.English_US));
			
			AddSupportedSubject(SubjectConstants.Common);
		}
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			InternalDoTranslate(phrase, languagesPair, subject, result, networkSetting, "");
		}
		
		void InternalDoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting, string post_data)
		{
			
			WebRequestHelper helper = null;
			
			if(string.IsNullOrEmpty(post_data))
			{
				string query = "http://www.merriam-webster.com/dictionary/{0}";
				query = string.Format(query, HttpUtility.UrlEncode(phrase));
			
				helper = 
					new WebRequestHelper(result, new Uri(query), 
						networkSetting, 
						WebRequestContentType.UrlEncodedGet);
			}
			else
			{
				helper = 
					new WebRequestHelper(result, new Uri("http://www.merriam-webster.com/dictionary"), 
						networkSetting, 
						WebRequestContentType.UrlEncoded);
				helper.AddPostData(post_data);				
			}
			
			string responseFromServer = helper.GetResponse();
			helper = null;
			
			if(responseFromServer.IndexOf("The word you've entered isn't in the dictionary.") >= 0)
			{
				if(responseFromServer.IndexOf("<PRE>") < 0)
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
				else
				{  //get suggestions
					StringParser parser = new StringParser("<PRE>", "</PRE>", responseFromServer);
					string[] items = parser.ReadItemsList("\">", "<", "345873409587");
					foreach(string item in items)
					{
						string part = item;
						string link = "html!<a href=\"http://www.merriam-webster.com/dictionary/{0}\">{0}</a>";
						link = string.Format(link,
							part);
						result.Translations.Add(link);
					}
					return;
				}
			}
			
			if(responseFromServer.IndexOf("One entry found.<br>") < 0)
			{
				StringParser parser = new StringParser("<select name='jump'", "</td>", responseFromServer);
				string[] items = parser.ReadItemsList("<option", "\n", "345873409587");
				
				items[0] = items[0].Replace("selected", "");
				foreach(string item in items)
				{
					string part = item.Replace(">", "");
					string link = "html!<a href=\"http://www.merriam-webster.com/dictionary/{0}\">{0}</a>";
					link = string.Format(link,
						part);
					result.Translations.Add(link);
				}
				
				if(responseFromServer.IndexOf("name='incr'") > 0)
				{ //we has more items
					//incr=Next+5&jump=dragon%27s+blood&book=Dictionary&quer=blood&list=45%2C31%2C3602592%2C0%3Bdragon%27s+blood%3D2000318535%3Bflesh+and+blood%3D2000400359%3Bfull-blood%5B1%2Cadjective%5D%3D2000425490%3Bfull-blood%5B2%2Cnoun%5D%3D2000425517%3Bhalf-blood%3D2000475964%3Bhalf+blood%3D2000475978%3Bhigh+blood+pressure%3D2000498596%3Blow+blood+pressure%3D2000629024%3Bnew+blood%3D2000712110%3Bpure-blooded%3D2000860991
					string incr_value = StringParser.Parse("<input type='submit' value='", "'", responseFromServer);
					string jump_value = StringParser.Parse("<option selected>", "\n", responseFromServer);
					string quer_value = StringParser.Parse("<input type='hidden' name='quer' value=\"", "\"", responseFromServer);
					string list_value = StringParser.Parse("<input type='hidden' name='list' value=\"", "\"", responseFromServer);
					string post_data_value = "incr={0}&jump={1}&book=Dictionary&quer={2}&list={3}";
					post_data_value = string.Format(post_data_value , 
						incr_value,
						HttpUtility.UrlEncode(jump_value),
						HttpUtility.UrlEncode(quer_value),
						HttpUtility.UrlEncode(list_value)
						);
						
					//some cleaning
					responseFromServer = null;
					
					InternalDoTranslate(phrase, languagesPair, subject, result, networkSetting, post_data_value);
				}
			}
			else
			{
				string part = StringParser.Parse("<span class=\"variant\">", "</span>", responseFromServer);
				
				string link = "html!<a href=\"http://www.merriam-webster.com/dictionary/{0}\">{0}</a>";
				link = string.Format(link,
					part);
				result.Translations.Add(link);
			}
		}
		
	} 
}
