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
	/// Description of MetaUaMultiWordTranslator.
	/// </summary>
	public class TridentComUaTranslator : Translator
	{
		public TridentComUaTranslator()
		{
			langToKey.Add(Language.Autodetect, "Detect");
			langToKey.Add(Language.English, "Eng");
			langToKey.Add(Language.Latvian, "Lat");
			langToKey.Add(Language.German, "Ger");
			langToKey.Add(Language.Polish, "Pol");
			langToKey.Add(Language.Russian, "Rus");
			langToKey.Add(Language.Ukrainian, "Ukr");
			langToKey.Add(Language.French, "Fre");
			langToKey.Add(Language.Kazakh, "Kaz");
			
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
			AddSupportedSubject(SubjectConstants.Auto,"AU" );
			AddSupportedSubject(SubjectConstants.Bank,"BN" );
			AddSupportedSubject(SubjectConstants.Business,"BZ" );
			AddSupportedSubject(SubjectConstants.Informatics, "IN");
			AddSupportedSubject(SubjectConstants.Tech, "EG");
			AddSupportedSubject(SubjectConstants.Law, "LW");
			AddSupportedSubject(SubjectConstants.Sex, "SX");
			AddSupportedSubject(SubjectConstants.Sport, "SO");
			AddSupportedSubject(SubjectConstants.Travel, "TI");
			
			CharsLimit = 155;
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
		
		
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.trident.com.ua/translation_online/"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			
			//query
			//?SrcTxt=test&Subject=**&LangFrom=Detect&LangTo=Rus&Translate=++Translate+++&DstTxt=&DlgLang=english
			string lang_to = ConvertLanguage(languagesPair.To);
			string lang_from = ConvertLanguage(languagesPair.From);
			string query = "SrcTxt={2}&Subject={1}&LangFrom={3}&LangTo={0}&Translate=++Translate+++&DstTxt=&DlgLang=english";
			query = string.Format(CultureInfo.InvariantCulture, 
				query, 
				lang_to,
				GetSubject(subject),
				HttpUtility.UrlEncode(phrase, helper.Encoding),
			    lang_from);
			helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
			
			string translation = StringParser.Parse("<textarea rows=\"7\" cols=\"50\" name=\"DstTxt\"", "</textarea>", responseFromServer); 
			translation = StringParser.ExtractRight(">",translation).Trim();
			if(!String.IsNullOrEmpty(translation))
				result.Translations.Add(translation);
			else
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}				
				
		}
	}
}
