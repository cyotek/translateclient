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
	/// Description of PereOrgUaMultiWordTranslator.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Pere")]
	public class PereOrgUATranslator: Translator
	{
		public PereOrgUATranslator()
		{
		
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.Russian));
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.Ukrainian));
			AddSupportedTranslation(new LanguagePair(Language.Russian, Language.English));
			
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Ukrainian));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Russian));
			
			AddSupportedSubject(SubjectConstants.Common);
			AddSupportedSubject("Socio-political");
			
			
			CharsLimit = 100000;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		static string ConvertLanguage(Language language)
		{
			switch(language)
			{
				case Language.English:
					return "en-us";
				case Language.Russian:
					return "ru-ru";
				case Language.Ukrainian:
					return "uk-ua";
			}
			throw new ArgumentException("Language : " + Enum.GetName(typeof(Language), language) + " not supported" , "language");
		}
		
		static string ConvertLangsPair(LanguagePair languagesPair)
		{
			return ConvertLanguage(languagesPair.From) + "_" + ConvertLanguage(languagesPair.To) + "_dzer-tyzh";
		}
		
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://pere.org.ua/cgi-bin/pere.cgi"), 
					networkSetting, 
					WebRequestContentType.Multipart);
		
			helper.AddPostKey("lng", "uk-ua");
			helper.AddPostKey("han", "text");
			helper.AddPostKey("wht", "text");
			helper.AddPostKey("cod", ConvertLangsPair(languagesPair));
			helper.AddPostKey("par", "1");
			helper.AddPostKey("txt", phrase);
			helper.AddPostKey("don", "Відправити запит");
			
			
			string responseFromServer = helper.GetResponse();
			result.Translations.Add(StringParser.Parse("<nopere><TEXTAREA ROWS=20 COLS=80>", "</TEXTAREA></nopere>", responseFromServer));
		}
		
	}
}
