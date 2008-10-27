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
using System.Collections.Generic;
using System.Globalization;


namespace Translate
{
	/// <summary>
	/// Description of GoogleLanguageGuesser.
	/// </summary>
	public class GoogleLanguageGuesser : LanguageGuesser
	{
		static Dictionary<string, Language> keyToLang = new Dictionary<string, Language>();
		static GoogleLanguageGuesser()
		{
			foreach(KeyValuePair<Language, string> key in GoogleUtils.LangToKey)
			{
				if(key.Key == Language.English_GB || key.Key == Language.English_US)
					continue;
				keyToLang[key.Value] = key.Key;
			}
		}

		
		public GoogleLanguageGuesser()
		{
			//without limit CharsLimit = 250;
			Name = "_guesser";
		}
		
		protected override void DoGuess(string phrase, GuessResult result, NetworkSetting networkSetting)
		{
			string query_base = "http://ajax.googleapis.com/ajax/services/language/detect?" +
				"v=1.0&hl=en&q=";
			int allowed_length = 2070 - query_base.Length;
		
			string query = SplitQuery(phrase, allowed_length);
			
			if(query.Length > 500)
					throw new InvalidOperationException("The length of string is greater of 500 characters." + 
						" string : " + query + 
						", full phrase : " + phrase
						);
			
			string real_query = query_base + HttpUtility.UrlEncode(query);
			if(real_query.Length > 2070)
				throw new InvalidOperationException("The length of query is greater of 2070 characters." + 
					" query : " + real_query +
					" full phrase : " + phrase
					);
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(real_query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet,
					true);
			helper.Referer = "http://translateclient.googlepages.com/";
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains(", \"responseStatus\": 200}"))
			{
				string languageString = StringParser.Parse("\"language\":\"", "\"", responseFromServer);
				string isReliableString = StringParser.Parse("\"isReliable\":", ",", responseFromServer);
				string confidenceString = StringParser.Parse("\"confidence\":", "}", responseFromServer);
				
				Language language;
				if(languageString == "pt-PT")
					language = Language.Portuguese;
				else
				{
					if(!keyToLang.TryGetValue(languageString, out language))
						throw new TranslationException("Language : " + languageString + " not supported");
				}	
					
				bool isReliable = isReliableString == "true";
				//if(phrase.Length < 30)
				//	isReliable = false;
				
				NumberFormatInfo num = NumberFormatInfo.InvariantInfo;
				confidenceString = confidenceString.Replace(",", num.NumberDecimalSeparator);
				confidenceString = confidenceString.Replace(".", num.NumberDecimalSeparator);
				
				double confidence;
				if(!double.TryParse(confidenceString,NumberStyles.Number, num, out confidence))
					throw new TranslationException("Can't parse string : " + confidenceString + " to float");
					
				result.AddScore(language, confidence*100, isReliable);
			}
			else
			{
				string error = StringParser.Parse("\"responseDetails\": \"", "\"", responseFromServer);
				string code = StringParser.Parse("\"responseStatus\":", "}", responseFromServer);
				throw new TranslationException(error + ", error code : " + code);
			}
			
		}
		
		static string SplitQuery(string phrase, int allowedLength)
		{
			StringBuilder sb = new StringBuilder(500);
			int phrase_length = 0;
			int encoded_length = 0;
			List<string> words = new List<string>(phrase.Split(new Char[] {' '}));			
			string sub_query = "";
			string part = "";
			int idx;
			
			foreach(string word in words)
			{
				sub_query = word;
				//remove start\end non-letters
				idx = 0;
				while(idx < word.Length && !Char.IsLetter(word[idx]))
					idx++;
					
				if(idx == word.Length)	
					continue; //non-letters string
				
				if(idx != 0)
					sub_query = sub_query.Substring(idx);
					
					
				idx = word.Length - 1;
				while(idx >= 0 && !Char.IsLetter(word[idx]))
					idx--;
					
				if(idx < 0)	
					continue; //non-letters string
				
				if(idx != word.Length - 1)
					sub_query = sub_query.Substring(0, idx + 1);
					
				part = sub_query;
				sub_query = HttpUtility.UrlEncode(sub_query, Encoding.UTF8);
				
				//add space
				if(encoded_length + 1 < allowedLength && phrase_length + 1 < 500)
				{
					if(encoded_length > 0)
					{
						sb.Append(" ");
						phrase_length ++;
						encoded_length ++;
					}
				}	
				else
					break;
				
				if(encoded_length + sub_query.Length <= allowedLength && phrase_length + part.Length <= 500)
				{
					sb.Append(part);
					phrase_length += part.Length;
					encoded_length += sub_query.Length;
				}
				else
					break;
			}
			
			return sb.ToString().Trim();
		}
	}
}
