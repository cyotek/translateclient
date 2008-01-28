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
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	/// <summary>
	/// Description of MetaUaMultiWordTranslator.
	/// </summary>
	public class MetaUATranslator : Translator
	{
		public MetaUATranslator()
		{
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Ukrainian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Ukrainian));
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.Ukrainian));
			
			AddSupportedTranslation(new LanguagePair(Language.English, Language.German));
			AddSupportedTranslation(new LanguagePair(Language.German, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Latvian));
			AddSupportedTranslation(new LanguagePair(Language.Latvian, Language.Russian));

			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Latvian));
			AddSupportedTranslation(new LanguagePair(Language.Latvian, Language.Ukrainian));

			AddSupportedTranslation(new LanguagePair(Language.English, Language.Latvian));
			AddSupportedTranslation(new LanguagePair(Language.Latvian, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.German, Language.Latvian));
			AddSupportedTranslation(new LanguagePair(Language.Latvian, Language.German));
			
			AddSupportedSubject(SubjectConstants.Common, "**");
			AddSupportedSubject("Aviation", "AV");
			AddSupportedSubject("Auto", "AU");
			AddSupportedSubject("Anatomy", "AN");
			AddSupportedSubject("Bank", "BN");
			AddSupportedSubject("Bible", "BB");
			AddSupportedSubject("Business","BZ" );
			AddSupportedSubject("Military", "ML");
			AddSupportedSubject("Law", "LW");
			AddSupportedSubject("Informatics", "IN");
			AddSupportedSubject("Art", "AR");
			AddSupportedSubject("Space", "SP");
			AddSupportedSubject("Medicine","MD" );
			AddSupportedSubject("Music","MU" );
			AddSupportedSubject("Sex", "SX");
			AddSupportedSubject("Sport", "SO");
			AddSupportedSubject("Tech", "EG");
			AddSupportedSubject("Philosophy","PI" );
			AddSupportedSubject("Chemistry","CH" );
			AddSupportedSubject("Economy", "EC");
			AddSupportedSubject("Electronics", "EN");
			
		}
		
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
		public static string ConvertLanguage(Language language)
		{
			switch(language)
			{
				case Language.English:
					return "Eng";
				case Language.Russian:
					return "Rus";
				case Language.Ukrainian:
					return "Ukr";
				case Language.German:
					return "Ger";
				case Language.Latvian:
					return "Lat";
			}
			throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
		}
		
		
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://translate.meta.ua/"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			
			//query
			//hl=en&ie=UTF8&text=small+test&langpair=en%7Cru			
			string lang_from = ConvertLanguage(languagesPair.From);
			string lang_to = ConvertLanguage(languagesPair.To);
			StringBuilder queryBuilder = new StringBuilder();
			queryBuilder.AppendFormat("Dialog=Rus&Format=TXT&TranFrom={0}&TranTo={1}&Translate=++%CF%E5%F0%E5%E2%E5%F1%F2%E8++&", lang_from, lang_to);
			queryBuilder.AppendFormat("SrcTxt={0}", HttpUtility.UrlEncode(phrase, System.Text.Encoding.GetEncoding(1251)));
			queryBuilder.AppendFormat("&language={0}-{1}&subject={2}&Translate=++%CF%E5%F0%E5%E2%E5%F1%F2%E8++&DstTxt=", lang_from, lang_to, GetSubject(subject));
			string query = queryBuilder.ToString();
			helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
		
			result.Translations.Add(StringParser.Parse("name=\"DstTxt\" wrap=\"virtual\">", "</textarea>", responseFromServer));
		}
	}
}
