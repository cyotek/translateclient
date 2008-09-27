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
using System.Net;

namespace Translate
{
	/// <summary>
	/// Description of MultiWordNetDictionary.
	/// </summary>
	public class MultiWordNetDictionary : MonolingualDictionary
	{
		public MultiWordNetDictionary()
		{
			CharsLimit = 50;
			LinesLimit = 1;
			Name = "_dictionary";
		
			AddSupportedTranslation(new LanguagePair(Language.English, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English_US, Language.English_US));
			AddSupportedTranslation(new LanguagePair(Language.Italian, Language.Italian));
			AddSupportedTranslation(new LanguagePair(Language.Spanish, Language.Spanish));
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.Portuguese));
			AddSupportedTranslation(new LanguagePair(Language.Hebrew, Language.Hebrew));
			AddSupportedTranslation(new LanguagePair(Language.Romanian, Language.Romanian));
			AddSupportedTranslation(new LanguagePair(Language.Latin, Language.Latin));
			
			AddSupportedSubject(SubjectConstants.Common);		
		}
		
		static string ConvertLanguage(Language language)
		{
			if(language == Language.English_US)
				language = Language.English;
				
			string val = Language.GetName(typeof(Language), language).ToLower();
			return val;
		}
		
		static CookieContainer cookieContainer = new CookieContainer();
		static DateTime coockieTime = DateTime.Now.AddHours(-5);

		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			lock(cookieContainer)
			{
				if(coockieTime < DateTime.Now.AddHours(-1))
				{
					coockieTime = DateTime.Now;
					WebRequestHelper helper_cookie = 
						new WebRequestHelper(result, new Uri("http://multiwordnet.itc.it/online/multiwordnet-head.php"), 
							networkSetting, 
							WebRequestContentType.UrlEncodedGet);
				
					helper_cookie.CookieContainer = cookieContainer;
					helper_cookie.GetResponse();
				}
			}
			
			string query = "http://multiwordnet.itc.it/online/multiwordnet-main.php?language={0}&field=word&word={1}&wntype=Overview&pos=";
			query = string.Format(query, 
				ConvertLanguage(languagesPair.From),
				HttpUtility.UrlEncode(phrase));
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet);
					
			helper.CookieContainer = cookieContainer;		
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains("<b> No data found </b>"))
			{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
			}
			
			string table = StringParser.Parse("<table border=1 WIDTH=100% CELLPADDING=1 class=bgpos>", "</table>", responseFromServer);
			string[] nodes =  StringParser.ParseItemsList("<tr>", "</tr>", table);
			
			string nodename, nodeval;
			
			Result child = result;
			
			foreach(string node in nodes)
			{
				if(node.Contains("<td class=bg_posbody COLSPAN=2 >"))
				{
					nodename = StringParser.RemoveAll("<", ">", node);
					child = new Result(result.ServiceItem, nodename, result.LanguagePair, result.Subject);
					result.Childs.Add(child);
				}
				else
				{
					nodeval = StringParser.RemoveAll("<", ">", node);
					child.Translations.Add(nodeval);
				}
			}
		
		}
		
	}
}
