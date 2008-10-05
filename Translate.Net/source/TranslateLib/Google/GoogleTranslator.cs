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
			CharsLimit = 2500;
		}
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string langPair = GoogleUtils.ConvertLanguagesPair(languagesPair);
			string query_base = "http://ajax.googleapis.com/ajax/services/language/translate?" +
				"v=1.0&langpair={0}&hl=en&q=";
			query_base = string.Format(query_base, langPair);
			
			int allowed_length = 2070 - query_base.Length;
				//"key=ABQIAAAA1Xz0dZCPKigOKIhDUJZ6FxQmSA1Htufb6qVqyW_v4yDxIUvb4BRwNjuLUmsgD0bAGP7qnB0dWYfEdg";
				
			
			List<string> queries = new List<string>();
			SplitToQueries(queries, phrase, SplitMode.Separators, allowed_length);
			
			StringBuilder sb = new StringBuilder(phrase.Length);
			string subres = "";
			int prefix_idx;
			int suffix_idx;
			foreach(string subphrase in queries)
			{
				if(subphrase.Length > 500)
					throw new InvalidOperationException("The length of string is greater of 500 characters." + 
						" string : " + subphrase + 
						", full phrase : " + phrase
						);
			
				//remove not-alphas from start and end of subphrase
				prefix_idx = subphrase.Length;
				for(int i = 0; i < subphrase.Length; i++)
				{
					if(char.IsLetter(subphrase[i]))
					{
						prefix_idx = i;
						break;
					}
				}
				
				if(prefix_idx == subphrase.Length)
				{ // no alphas - skip without translation
				   sb.Append(subphrase);
				   continue;
				}
				
				suffix_idx = 0;
				for(int i = subphrase.Length - 1; i >= 0; i--)
				{
					if(char.IsLetter(subphrase[i]))
					{
						suffix_idx = i;
						break;
					}
				}
			
				string real_query = query_base + HttpUtility.UrlEncode(subphrase.Substring(prefix_idx, suffix_idx - prefix_idx + 1));
				if(real_query.Length > 2070)
					throw new InvalidOperationException("The length of query is greater of 2070 characters." + 
						" string : " + subphrase + 
						" query : " + real_query +
						" full phrase : " + phrase
						);
						

				/* debug splitting algo
				 result.Translations.Add(
						" string length : " + subphrase.Length.ToString());
						
				result.Translations.Add(		
						" \r\nstring : " + subphrase);
				result.Translations.Add(		
						" \r\nstring for query length : " + (suffix_idx - prefix_idx + 1).ToString());
				result.Translations.Add(						
						" \r\nstring for query : " + subphrase.Substring(prefix_idx, suffix_idx - prefix_idx + 1));
				result.Translations.Add(						
						" \r\nquery length : " + real_query.Length.ToString());
				*/
				
				if(prefix_idx > 0)
					sb.Append(subphrase.Substring(0, prefix_idx));

				subres = DoInternalTranslate(real_query, result, networkSetting);
				sb.Append(subres);
				
				if(suffix_idx < subphrase.Length - 1)
					sb.Append(subphrase.Substring(suffix_idx + 1));
				
			}
			result.Translations.Add(sb.ToString());
		}
		
		enum SplitMode
		{
			Separators,
			Spaces,
			Size
		}
		
		int SplitToQueries(List<string> queries, string phrase, SplitMode mode, int allowed_length)
		{
			List<string> parts;
			switch(mode)
			{
				case SplitMode.Separators:
					parts = SplitToParts(phrase);
					break;
				case SplitMode.Spaces:
					parts = new List<string>(phrase.Split(new Char[] {' '}));
					for(int i = 0; i < parts.Count - 1; i++)
						parts[i] = parts[i] + " "; //add spaces
					break;
				case SplitMode.Size:
				default: 
					parts = new List<string>();
					//split to 50-chars blocks
					for(int i = 0; i < phrase.Length - 1; i+= 50)
					{
						if(i + 50 <= phrase.Length)
							parts.Add(phrase.Substring(i, 50));
						else	
							parts.Add(phrase.Substring(i, phrase.Length - i));
					}
					break;
			}
			
			StringBuilder sb = new StringBuilder(500);
			int phrase_length = 0;
			int encoded_length = 0;
			string sub_query = "";
			
			foreach(string part in parts)
			{
				sub_query = HttpUtility.UrlEncode(part, Encoding.UTF8);
				if(encoded_length + sub_query.Length <= allowed_length && phrase_length + part.Length <= 500)
				{
					sb.Append(part);
					phrase_length += part.Length;
					encoded_length += sub_query.Length;
				}
				else
				{
					if(sb.Length > 0)
					{
						queries.Add(sb.ToString());
						sb.Length = 0;
						phrase_length = 0;
						encoded_length = 0;
					}
					
					if(sub_query.Length <= allowed_length && part.Length <= 500)
					{
						sb.Append(part);
						phrase_length += part.Length;
						encoded_length += sub_query.Length;
					}
					else
					{ //very long phrase without separators - split it by space
						int remainder_size = 0;
						if(mode == SplitMode.Separators)
							remainder_size = SplitToQueries(queries, part, SplitMode.Spaces, allowed_length);
						else if(mode == SplitMode.Spaces)
							remainder_size = SplitToQueries(queries, part, SplitMode.Size, allowed_length);
						else
							throw new InvalidOperationException("Wrong size of string : " + part);

						//reset
						sb.Length = 0;
						phrase_length = 0;
						encoded_length = 0;
							
						if(remainder_size > 0 && remainder_size < allowed_length && queries[queries.Count - 1].Length < 500)	
						{  //here possible to add some text to end
							sb.Append(queries[queries.Count - 1]);
							phrase_length = sb.Length;
							encoded_length = remainder_size;
							queries.RemoveAt(queries.Count - 1);
						}
					}
				}
			}
			
			if(sb.Length > 0)
			{
				queries.Add(sb.ToString());
			}
			return encoded_length;
		}
		
		string DoInternalTranslate(string query, Result result, NetworkSetting networkSetting)
		{
				
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncodedGet,
					true);
			helper.Referer = "http://translateclient.googlepages.com/";
			
			string responseFromServer = helper.GetResponse();
			
			if(responseFromServer.Contains(", \"responseStatus\": 200}"))
			{
				string translation = StringParser.Parse("\"translatedText\":\"", "\"", responseFromServer);
				translation = HttpUtilityEx.HtmlDecode(translation);
				return translation;
			}
			else
			{
				string error = StringParser.Parse("\"responseDetails\": \"", "\"", responseFromServer);
				string code = StringParser.Parse("\"responseStatus\":", "}", responseFromServer);
				throw new TranslationException(error + ", error code : " + code);
			}
		}
		
		static List<char> delimiterCharsList;
		
		static GoogleTranslator()
		{
			char[] delimiterChars = { ',', '.', ';', '\n', '!', '?'};

			delimiterCharsList = new List<char>(delimiterChars);
			delimiterCharsList.Sort();
		}
		
		static List<string> SplitToParts(string data)
		{ //"some, string. here !" -> "some," " string." " here !"
			List<string> result = new List<string>();
			char[] dataChars = data.ToCharArray();
			
			StringBuilder sb = new StringBuilder();
			foreach(char ch in dataChars)
			{
				sb.Append(ch);
				if(delimiterCharsList.BinarySearch(ch) >= 0)	
				{
					result.Add(sb.ToString());
					sb.Length = 0;
				}
			}
			
			if(sb.Length >= 0)
			{
				result.Add(sb.ToString());
			}
			
			return result;
		}
		
	}
}
