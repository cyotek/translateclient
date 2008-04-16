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
	/// Description of WikiUtils.
	/// </summary>
	public static class WikiUtils
	{
		static WikiUtils()
		{
			langToKey.Add(Language.Arabic, "ar");
			langToKey.Add(Language.Belarusian, "be");
			langToKey.Add(Language.Bulgarian, "bg");
			langToKey.Add(Language.Chinese, "zh");
			langToKey.Add(Language.Croatian, "hr");
			langToKey.Add(Language.Czech, "cs");
			langToKey.Add(Language.Danish, "da");
			langToKey.Add(Language.Dutch, "nl");
			langToKey.Add(Language.English, "en");
			langToKey.Add(Language.Esperanto, "eo");
			langToKey.Add(Language.Estonian, "et");
			langToKey.Add(Language.Icelandic, "is");
			langToKey.Add(Language.Finnish, "fi");
			langToKey.Add(Language.French, "fr");
			langToKey.Add(Language.German, "de");
			langToKey.Add(Language.Greek, "el");
			langToKey.Add(Language.Hungarian, "hu");
			langToKey.Add(Language.Italian, "it");
			langToKey.Add(Language.Japanese, "ja");
			langToKey.Add(Language.Korean, "ko");
			langToKey.Add(Language.Latin, "la");
			langToKey.Add(Language.Latvian, "lv");
			langToKey.Add(Language.Lithuanian, "lt");
			langToKey.Add(Language.Macedonian, "mk");
			langToKey.Add(Language.Norwegian, "no");
			langToKey.Add(Language.Polish, "pl");
			langToKey.Add(Language.Portuguese, "pt");
			langToKey.Add(Language.Romanian, "ro");
			langToKey.Add(Language.Russian, "ru");
			langToKey.Add(Language.Serbian, "sr");
			langToKey.Add(Language.Slovak, "sk");
			langToKey.Add(Language.Slovenian, "sl");
			langToKey.Add(Language.Spanish, "es");
			langToKey.Add(Language.Swedish, "sv");
			langToKey.Add(Language.Ukrainian, "uk");
		}
		
		static SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		
		public static SortedDictionary<Language, string> LangToKey {
			get { return langToKey; }
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		public static string ConvertLanguage(Language language)
		{
			string result;
			if(!langToKey.TryGetValue(language, out result))
				throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
			else
				return result;
		}
		
	}
}
