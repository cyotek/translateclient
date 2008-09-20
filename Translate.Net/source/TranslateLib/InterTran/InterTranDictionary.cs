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
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Globalization;

namespace Translate
{
	/// <summary>
	/// Description of InterTranDictionary.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class InterTranDictionary  : BilingualDictionary
	{
		public InterTranDictionary()
		{
			CharsLimit = 64;
			Name = "_dictionary";
			AddSupportedSubject(SubjectConstants.Common);
			
			langToKey.Add(Language.English, "eng");
			langToKey.Add(Language.English_GB, "eng");
			langToKey.Add(Language.English_US, "eng");
			langToKey.Add(Language.Portuguese_BR, "pob");
			langToKey.Add(Language.Bulgarian, "bul");
			langToEncoding.Add(Language.Bulgarian, "windows-1251");
			langToKey.Add(Language.Croatian, "cro");
			langToEncoding.Add(Language.Croatian, "windows-1250");
			langToKey.Add(Language.Czech, "che");
			langToEncoding.Add(Language.Czech, "windows-1250");
			langToKey.Add(Language.Danish, "dan");
			langToKey.Add(Language.Dutch, "dut");
			langToKey.Add(Language.Spanish, "spa");
			langToKey.Add(Language.Finnish, "fin");
			langToKey.Add(Language.French, "fre");
			langToKey.Add(Language.German, "ger");
			langToKey.Add(Language.Greek, "grk");
			langToKey.Add(Language.Hungarian, "hun");
			langToEncoding.Add(Language.Hungarian, "windows-1250");
			langToKey.Add(Language.Icelandic, "ice");
			langToKey.Add(Language.Italian, "ita");
			langToKey.Add(Language.Japanese, "jpn");
			langToEncoding.Add(Language.Japanese, "shift_jis");
			langToKey.Add(Language.Spanish_LA, "spl");
			langToKey.Add(Language.Norwegian, "nor");
			langToKey.Add(Language.Filipino, "tag");
			langToKey.Add(Language.Polish, "pol");
			langToEncoding.Add(Language.Polish, "iso-8859-2");
			langToKey.Add(Language.Portuguese, "poe");
			langToKey.Add(Language.Romanian, "rom");
			langToEncoding.Add(Language.Romanian, "windows-1250");
			langToKey.Add(Language.Russian, "rus");
			langToEncoding.Add(Language.Russian, "windows-1251");
			langToKey.Add(Language.Serbian, "sel");
			langToEncoding.Add(Language.Serbian, "windows-1250");
			langToKey.Add(Language.Slovenian, "slo");
			langToEncoding.Add(Language.Slovenian, "windows-1250");
			langToKey.Add(Language.Swedish, "swe");
			langToKey.Add(Language.Welsh, "wel");
			langToKey.Add(Language.Turkish, "tur");
			langToEncoding.Add(Language.Turkish, "windows-1254");
			langToKey.Add(Language.Latin, "ltt");	
			
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				foreach(Language to in tmp.Keys)
				{
					if( (from != Language.English || (to != Language.English_GB && to != Language.English_US)) &&
						(to != Language.English || (from != Language.English_GB && from != Language.English_US)) &&
						( 
							(from != to || from == Language.English || 
								from == Language.English_GB || from == Language.English_US)
						)
					  )
					  AddSupportedTranslation(new LanguagePair(from, to));
				}
			}
			
		}
		
		SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		SortedDictionary<Language, string> langToEncoding = new SortedDictionary<Language, string>();

		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		string ConvertLanguage(Language language)
		{
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}

		string GetEncoding(Language language)
		{
			string result;
			if(!langToEncoding.TryGetValue(language, out result))
				return "windows-1252";
			else
				return result;
		}
		
		Encoding GetEncoding(Language from, Language to)
		{
			string fromEnc = GetEncoding(from);
			string toEnc = GetEncoding(to);
			if(fromEnc == toEnc)
				return Encoding.GetEncoding(fromEnc);
			else if(fromEnc == "windows-1252")	
				return Encoding.GetEncoding(toEnc);
			else if(toEnc == "windows-1252")	
				return Encoding.GetEncoding(fromEnc);
			else 	
				return Encoding.GetEncoding(toEnc);
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://intertran.tranexp.com/Translate/result.shtml"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			helper.Encoding = GetEncoding(languagesPair.From, languagesPair.To);//Encoding.GetEncoding(GetEncoding(languagesPair.To)); 
			string query = "Submit.x=56&Submit.y=10&from={0}&keyb=non&text={1}&to={2}&translation=";
			
			query = string.Format(CultureInfo.InvariantCulture, 
					query, 
					ConvertLanguage(languagesPair.From),
					HttpUtility.UrlEncode(phrase, Encoding.GetEncoding(GetEncoding(languagesPair.From))),
					ConvertLanguage(languagesPair.To)
				);
			helper.AddPostData(query);
		
			string responseFromServer = helper.GetResponse();
		
			if(string.IsNullOrEmpty(responseFromServer))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else if(responseFromServer.Contains("there was no translation for your original query.</center>"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else if(responseFromServer.Contains("name=\"translation\">An error occurred during translation. Please try again later.</textarea>"))
			{
				//result.ResultNotFound = true;
				throw new TranslationException("An error occurred during translation. Please try again later.");
			}
			else if(responseFromServer.Contains("<center>Warning:<br> Could not open"))
			{
				//result.ResultNotFound = true;
				throw new TranslationException("An error occurred during translation. Please try again later.");
			}
			else
			{
				string translation = StringParser.Parse("<INPUT TYPE=\"hidden\" NAME=\"alltrans\" VALUE=\"", "\">",  responseFromServer).Trim();
				if(string.IsNullOrEmpty(translation))
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
				
				string subphrase;
				bool firstRun = true;
				Result subres = result;
				StringParser parser = new StringParser(translation);
				string[] translations = parser.ReadItemsList("{{", "}}");				
				
				foreach(string subtranslation in translations)
				{
					string subtrans = "|" + subtranslation + "|";
					parser = new StringParser(subtrans);
					string[] subtranslations = parser.ReadItemsList("|", "|");	
					if(subtranslations.Length > 1)
					{
						subphrase = subtranslations[0];
						if(firstRun && translation.Length == 1 && string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0)
						{
							for(int i = 1; i < subtranslations.Length; i++)
								result.Translations.Add(subtranslations[i]);
							return;
						}
						firstRun = false;
						
						subres = CreateNewResult(subphrase, languagesPair, subject);
						result.Childs.Add(subres);
						
						for(int i = 1; i < subtranslations.Length; i++)
							subres.Translations.Add(subtranslations[i]);
					}
				}
			}
			
		}
		
		
	}
}
