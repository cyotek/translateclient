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
using System.Net; 
using System.Text; 
using System.IO; 
using System.Web; 
using System.IO.Compression;
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of GoogleMultiWordTranslator.
	/// </summary>
	public class GoogleTranslator : Translator
	{
		public GoogleTranslator()
		{
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(GoogleUtils.LangToKey);
			
			foreach(Language from in GoogleUtils.LangToKey.Keys)
			{
				foreach(Language to in tmp.Keys)
				{
					if( from != to &&
						(from != Language.English || (to != Language.English_GB && to != Language.English_US)) &&
						(to != Language.English || (from != Language.English_GB && from != Language.English_US)) &&
						(to != Language.Autodetect) &&
						(!(to == Language.English_US && from == Language.English_GB)) &&
						(!(from == Language.English_US && to == Language.English_GB)) &&
						(!(from == Language.Filipino && to == Language.Tagalog)) &&
						(!(from == Language.Tagalog && to == Language.Filipino))
						
					  )
					  AddSupportedTranslation(new LanguagePair(from, to));
				}
			}
		
			AddSupportedSubject(SubjectConstants.Common);
			CharsLimit = 500;
		}
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://ajax.googleapis.com/ajax/services/language/translate?" +
				"v=1.0&q={0}&langpair={1}&hl=en&" +
				"key=ABQIAAAA1Xz0dZCPKigOKIhDUJZ6FxQmSA1Htufb6qVqyW_v4yDxIUvb4BRwNjuLUmsgD0bAGP7qnB0dWYfEdg";
			query = string.Format(query, HttpUtility.UrlEncode(phrase, System.Text.Encoding.UTF8 ), GoogleUtils.ConvertLanguagesPair(languagesPair));
				
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet,
					true);
			helper.Referer = "127.0.0.1";		
			
			//query
			//hl=en&ie=UTF8&text=small+test&langpair=en%7Cru			
			//string langpair= GoogleUtils.ConvertTranslatorLanguagesPair(languagesPair);
			//string query = "hl=en&ie=UTF8&text=" + HttpUtility.UrlEncode(phrase, System.Text.Encoding.UTF8 ) + "&" + langpair;
			//helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains(", \"responseStatus\": 200}"))
			{
				string translation = StringParser.Parse("\"translatedText\":\"", "\"", responseFromServer);
				translation = HttpUtilityEx.HtmlDecode(translation);
				result.Translations.Add(translation);
			}
			else
			{
				string error = StringParser.Parse("\"responseDetails\": \"", "\"", responseFromServer);
				string code = StringParser.Parse("\"responseStatus\":", "}", responseFromServer);
				throw new TranslationException(error + ", error code : " + code);
			}
		}
	}
}
