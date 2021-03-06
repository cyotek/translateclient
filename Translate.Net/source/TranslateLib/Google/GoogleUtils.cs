﻿#region License block : MPL 1.1/GPL 2.0/LGPL 2.1
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
using System.Text;
using System.Web; 
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of GoogleUtils.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Utils")]
	public static class GoogleUtils
	{
	
		static GoogleUtils()
		{
			langToKey.Add(Language.Autodetect,"");
			langToKey.Add(Language.Afrikaans, "af");
			langToKey.Add(Language.Albanian, "sq");
			langToKey.Add(Language.Amharic, "am");
			langToKey.Add(Language.Arabic,"ar");
			langToKey.Add(Language.Armenian,"hy");
			langToKey.Add(Language.Azerbaijani,"az");
			langToKey.Add(Language.Basque,"eu");
			langToKey.Add(Language.Belarusian,"be");
			langToKey.Add(Language.Bengali,"bn");
			langToKey.Add(Language.Bihari,"bh");
			langToKey.Add(Language.Bulgarian,"bg");
			langToKey.Add(Language.Burmese,"my");
			langToKey.Add(Language.Catalan,"ca");
			langToKey.Add(Language.Cherokee,"chr");
			langToKey.Add(Language.Chinese,"zh");
			langToKey.Add(Language.Chinese_CN,"zh-CN");
			langToKey.Add(Language.Chinese_TW,"zh-TW");
			langToKey.Add(Language.Croatian,"hr");
			langToKey.Add(Language.Czech,"cs");
			langToKey.Add(Language.Danish,"da");
			langToKey.Add(Language.Dhivehi,"dv");
			langToKey.Add(Language.Dutch,"nl");
			langToKey.Add(Language.English_US,"en");
			langToKey.Add(Language.English_GB,"en");
			langToKey.Add(Language.English,"en");
			langToKey.Add(Language.Esperanto,"eo");
			langToKey.Add(Language.Estonian,"et");
			langToKey.Add(Language.Filipino,"tl");
			langToKey.Add(Language.Tagalog,"tl");
			langToKey.Add(Language.Finnish,"fi");
			langToKey.Add(Language.French,"fr");
			langToKey.Add(Language.Galician,"gl");
			langToKey.Add(Language.Georgian,"ka");
			langToKey.Add(Language.German,"de");
			langToKey.Add(Language.Greek,"el");
			langToKey.Add(Language.Guarani,"gn");
			langToKey.Add(Language.Gujarati,"gu");
			langToKey.Add(Language.Hebrew,"iw");
			langToKey.Add(Language.Hindi,"hi");
			langToKey.Add(Language.Hungarian,"hu");
			langToKey.Add(Language.Icelandic,"is");
			langToKey.Add(Language.Indonesian,"id");
			langToKey.Add(Language.Inuktitut,"iu");
			langToKey.Add(Language.Italian,"it");
			langToKey.Add(Language.Japanese,"ja");
			langToKey.Add(Language.Kazakh,"kk");
			langToKey.Add(Language.Khmer,"km");
			langToKey.Add(Language.Korean,"ko");
			langToKey.Add(Language.Kurdish,"ku");
			langToKey.Add(Language.Kyrgyz,"ky");
			langToKey.Add(Language.Laothian,"lo");
			langToKey.Add(Language.Latvian,"lv");
			langToKey.Add(Language.Lithuanian,"lt");
			langToKey.Add(Language.Macedonian,"mk");
			langToKey.Add(Language.Malay,"ms");
			langToKey.Add(Language.Malayalam,"ml");
			langToKey.Add(Language.Maltese,"mt");
			langToKey.Add(Language.Marathi,"mr");
			langToKey.Add(Language.Mongolian,"mn");
			langToKey.Add(Language.Nepali,"ne");
			langToKey.Add(Language.Norwegian,"no");
			langToKey.Add(Language.Oriya,"or");
			langToKey.Add(Language.Pashto,"ps");
			langToKey.Add(Language.Persian,"fa");
			langToKey.Add(Language.Polish,"pl");
			langToKey.Add(Language.Portuguese,"pt"); //pt-PT !!!
			langToKey.Add(Language.Punjabi,"pa");
			langToKey.Add(Language.Romanian,"ro");
			langToKey.Add(Language.Russian,"ru");
			langToKey.Add(Language.Sanskrit,"sa");
			langToKey.Add(Language.Serbian,"sr");
			langToKey.Add(Language.Sindhi,"sd");
 			langToKey.Add(Language.Sinhalese,"si");
			langToKey.Add(Language.Slovak,"sk");
			langToKey.Add(Language.Slovenian,"sl");
			langToKey.Add(Language.Spanish,"es");
			langToKey.Add(Language.Swahili,"sw");
			langToKey.Add(Language.Swedish,"sv");
 			langToKey.Add(Language.Tajik,"tg");
 			langToKey.Add(Language.Tamil,"ta");
			langToKey.Add(Language.Telugu,"te");
 			langToKey.Add(Language.Thai,"th");
 			langToKey.Add(Language.Tibetan,"bo");
 			langToKey.Add(Language.Turkish,"tr");
			langToKey.Add(Language.Ukrainian,"uk");
 			langToKey.Add(Language.Urdu,"ur");
 			langToKey.Add(Language.Uzbek,"uz");
 			langToKey.Add(Language.Uighur,"ug");
			langToKey.Add(Language.Vietnamese,"vi");
			langToKey.Add(Language.Unknown,"");
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
		
		static SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		
		public static SortedDictionary<Language, string> LangToKey {
			get { return langToKey; }
		}

		public static string ConvertTranslatorLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			return "sl=" + ConvertLanguage(languagesPair.From) + "&tl=" + ConvertLanguage(languagesPair.To);
		}
		
		public static string ConvertLanguagesPair(LanguagePair languagesPair)
		{
			if(languagesPair == null)
				throw new ArgumentNullException("languagesPair");
			
			return HttpUtility.UrlEncode(ConvertLanguage(languagesPair.From) + "|" + ConvertLanguage(languagesPair.To), System.Text.Encoding.UTF8);
		}
		
		
	}
}
