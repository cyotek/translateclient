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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;

namespace Translate
{
	/// <summary>
	/// Description of MetaUaMultiWordTranslator.
	/// </summary>
	public class MetaUATranslator : Translator
	{
		public MetaUATranslator()
		{
			langToKey.Add(Language.English, "en");
			langToKey.Add(Language.German, "de");
			langToKey.Add(Language.Latvian, "lv");
			langToKey.Add(Language.Polish, "pl");
			langToKey.Add(Language.Russian, "ru");
			langToKey.Add(Language.Ukrainian, "ua");
			langToKey.Add(Language.French, "fr");

			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				foreach(Language to in tmp.Keys)
				{
					if( from != to)
					  AddSupportedTranslation(new LanguagePair(from, to));
				}
			}
			
			AddSupportedSubject(SubjectConstants.Common, "**");
			AddSupportedSubject(SubjectConstants.Aviation, "AV");
			AddSupportedSubject(SubjectConstants.Auto, "AU");
			AddSupportedSubject(SubjectConstants.Anatomy, "AN");
			AddSupportedSubject(SubjectConstants.Bank, "BN");
			AddSupportedSubject(SubjectConstants.Bible, "BB");
			AddSupportedSubject(SubjectConstants.Business,"BZ" );
			AddSupportedSubject(SubjectConstants.Military, "ML");
			AddSupportedSubject(SubjectConstants.Law, "LW");
			AddSupportedSubject(SubjectConstants.Informatics, "IN");
			AddSupportedSubject(SubjectConstants.Art, "AR");
			AddSupportedSubject(SubjectConstants.Space, "SP");
			AddSupportedSubject(SubjectConstants.Medicine,"MD" );
			AddSupportedSubject(SubjectConstants.Music,"MU" );
			AddSupportedSubject(SubjectConstants.Sex, "SX");
			AddSupportedSubject(SubjectConstants.Sport, "SO");
			AddSupportedSubject(SubjectConstants.Tech, "EG");
			AddSupportedSubject(SubjectConstants.Philosophy,"PI" );
			AddSupportedSubject(SubjectConstants.Chemistry,"CH" );
			AddSupportedSubject(SubjectConstants.Economy, "EC");
			AddSupportedSubject(SubjectConstants.Electronics, "EN");
		}
		
		SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		
		StringsDictionary subjects = new StringsDictionary(30);
		
		protected void AddSupportedSubject(string subject, string data)
		{
			AddSupportedSubject(subject);
			subjects[subject] = data;
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String)")]
		string GetSubject(string subject)
		{
			string res;
			if(!subjects.TryGetValue(subject, out res))
				throw new ArgumentException("Subject : " + subject + " not supported");
			return res;
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		string ConvertLanguage(Language language)
		{
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}
		
		static CookieContainer cookieContainer = new CookieContainer();
		static DateTime coockieTime = DateTime.Now.AddHours(-5);
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			lock(cookieContainer)
			{
				if(coockieTime < DateTime.Now.AddHours(-1))
				{  //emulate first access to site
					WebRequestHelper helpertop = 
						new WebRequestHelper(result, new Uri("http://translate.meta.ua/"), 
							networkSetting, 
							WebRequestContentType.UrlEncodedGet);
					helpertop.CookieContainer = cookieContainer;
					string responseFromServertop = helpertop.GetResponse();
					coockieTime = DateTime.Now;
				}
			}

			string lang_from = ConvertLanguage(languagesPair.From);
			string lang_to = ConvertLanguage(languagesPair.To);
		
			string responseFromServer = null;
			lock(cookieContainer)
			{
				WebRequestHelper helper = 
					new WebRequestHelper(result, new Uri("http://translate.meta.ua/ajax/?sn=save_source"), 
						networkSetting, 
						WebRequestContentType.UrlEncoded);
				helper.CookieContainer = cookieContainer;		
				
				//query
				//text_source=проверка&lang_to=ua&lang_from=ru&dict=**
				StringBuilder queryBuilder = new StringBuilder();
				queryBuilder.AppendFormat("text_source={0}&", phrase);
				queryBuilder.AppendFormat("lang_to={0}&lang_from={1}&", lang_to, lang_from);
				queryBuilder.AppendFormat("dict=", GetSubject(subject));
				string query = queryBuilder.ToString();
				helper.AddPostData(query);
				responseFromServer = helper.GetResponse();
				coockieTime = DateTime.Now;
			}
			
			if(!String.IsNullOrEmpty(responseFromServer))
			{	
				//{"r":true,"pc":1,"ui":"4c1ea0e46198f"}
				string code = StringParser.Parse("ui\":\"", "\"}", responseFromServer);
				//http://translate.meta.ua/ajax/?sn=get_translate&translate_uniqid=4c1ea0e46198f&lang_to=ua&lang_from=ru&translate_part=0
				string query = "http://translate.meta.ua/ajax/?sn=get_translate&translate_uniqid={0}&lang_to={1}&lang_from={2}&translate_part=0";
				string url = String.Format(query,code, lang_to, lang_from);
				lock(cookieContainer)
				{
					WebRequestHelper helper = 
						new WebRequestHelper(result, new Uri(url), 
							networkSetting, 
							WebRequestContentType.UrlEncodedGet);
					helper.CookieContainer = cookieContainer;		
					responseFromServer = helper.GetResponse();
					coockieTime = DateTime.Now;
				}
				if(!String.IsNullOrEmpty(responseFromServer))
				{
					//{"source":"\u043f\u0440\u043e\u0432\u0435\u0440\u043a\u0430","translate":" \u043f\u0435\u0440\u0435\u0432\u0456\u0440\u043a\u0430","translate_part":"0","type":"p","index":0,"r":true}
					string translation = StringParser.Parse("translate\":\"", "\"", responseFromServer);
					result.Translations.Add(HttpUtilityEx.HtmlDecode(translation));
				}
				else
					throw new TranslationException("Nothing returned from call to " + url);
				
			}
			else
				throw new TranslationException("Nothing returned from call to http://translate.meta.ua/ajax/?sn=save_source");
		
			
		}
	}
}
