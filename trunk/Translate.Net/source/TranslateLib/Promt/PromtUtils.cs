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
	/// Description of PromtUtils.
	/// </summary>
	internal static class PromtUtils
	{
		static PromtUtils()
		{
			langToKey.Add(Language.Autodetect, "a");
			langToKey.Add(Language.English, "e");
			langToKey.Add(Language.English_GB, "e");
			langToKey.Add(Language.English_US, "e");
			langToKey.Add(Language.Russian, "r");
			langToKey.Add(Language.German, "g");
			langToKey.Add(Language.French, "f");
			langToKey.Add(Language.Spanish, "s");
			langToKey.Add(Language.Italian, "i");
			langToKey.Add(Language.Portuguese, "p");
			
			subjectToKey.Add(SubjectConstants.Common, "General");
			subjectToKey.Add(SubjectConstants.Auto, "Automotive");
			subjectToKey.Add(SubjectConstants.Bank, "Banking");
			subjectToKey.Add(SubjectConstants.Business, "Business");
			subjectToKey.Add(SubjectConstants.Internet, "Internet");
			subjectToKey.Add(SubjectConstants.Logistics, "Logistics");
			subjectToKey.Add(SubjectConstants.Informatics, "Software");
			subjectToKey.Add(SubjectConstants.Sport, "Sport");
			subjectToKey.Add(SubjectConstants.Travel, "Travel");
			subjectToKey.Add(SubjectConstants.Football, "Football");
			subjectToKey.Add(SubjectConstants.Phrasebook, "Phrasebook");
			subjectToKey.Add(SubjectConstants.Perfumery, "Perfumery");
		}
		
		static SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		public static string ConvertLanguage(Language language)
		{
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}
		
		public static string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			string result =  ConvertLanguage(languagesPair.From) + 
				ConvertLanguage(languagesPair.To);
			return result;	
		}
		
		static SortedDictionary<string, string> subjectToKey = new SortedDictionary<string, string>();

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		public static string GetSubject(string subject)
		{
			string result;
			if(!subjectToKey.TryGetValue(subject, out result))
				throw new ArgumentException("Subject : " + subject + " not supported" , "subject");
			else
				return result;
		}
		
		public static void InitServiceItem(ServiceItem serviceItem)
		{
			serviceItem.CharsLimit = 3000;

			serviceItem.AddSupportedSubject(SubjectConstants.Common);
		}
		
		
	}
}
