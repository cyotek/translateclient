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
			langToKey.Add(Language.English, "Eng");
			langToKey.Add(Language.Latvian, "Lat");
			langToKey.Add(Language.German, "Ger");
			langToKey.Add(Language.Polish, "Pol");
			langToKey.Add(Language.Russian, "Rus");
			langToKey.Add(Language.Ukrainian, "Ukr");
			langToKey.Add(Language.French, "Fre");
			
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
			AddSupportedSubject(SubjectConstants.Business,"BZ" );
			AddSupportedSubject(SubjectConstants.Informatics, "IN");
			AddSupportedSubject(SubjectConstants.Tech, "EG");
			AddSupportedSubject(SubjectConstants.Law, "LW");
			AddSupportedSubject(SubjectConstants.Sport, "SO");
			
			CharsLimit = 1000;
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
				new WebRequestHelper(result, new Uri("http://www.trident.com.ua/translation/tran.php"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			
			//query
			//?Adr=62.149.17.70&DlgLang=Rus&TranTo=Rus&Subject=**&SrcTxt=%D0%B1%D0
			string lang_to = ConvertLanguage(languagesPair.To);
			string query = "Adr=62.149.17.70&DlgLang=Eng&TranTo={0}&Subject={1}&SrcTxt={2}";
			query = string.Format(CultureInfo.InvariantCulture, 
				query, 
				lang_to,
				GetSubject(subject),
				HttpUtility.UrlEncode(phrase, helper.Encoding));
			helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
			
			string status = responseFromServer.Substring(8, 1);
			if(status != "2")
			{
				throw new TranslationException(responseFromServer.Substring(10));
			}
			else
			{	if(responseFromServer.Substring(17) == "Translation direction is not correct")
					throw new TranslationException("Translation direction is not correct");
				result.Translations.Add(responseFromServer.Substring(17));
			}
		}
	}
}
