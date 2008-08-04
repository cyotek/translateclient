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


namespace Translate
{
	/// <summary>
	/// Description of PromtTranslator.
	/// </summary>
	public class PromtBaseTranslator: Translator
	{
		public PromtBaseTranslator()
		{
			PromtUtils.InitServiceItem(this);
		}
		
		static string viewState = "";
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			lock(viewState)
			{
				if(string.IsNullOrEmpty(viewState))
				{  //emulate first access to site
					WebRequestHelper helpertop = 
						new WebRequestHelper(result, new Uri("http://www.online-translator.com/text_Translation.aspx"), 
							networkSetting, 
							WebRequestContentType.UrlEncodedGet);
							
					string responseFromServertop = helpertop.GetResponse();
					viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServertop);
				}
			}
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.online-translator.com/text_Translation.aspx"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
						
			//query
			lock(viewState)
			{
			string query = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&ctl00%24SiteContent%24ucTextTranslator%24tbFromAddr=&ctl00%24SiteContent%24ucTextTranslator%24tbToAddr=&ctl00%24SiteContent%24ucTextTranslator%24tbCCAddr=&ctl00%24SiteContent%24ucTextTranslator%24tbSubject=&ctl00%24SiteContent%24ucTextTranslator%24tbBody=&ctl00%24SiteContent%24ucTextTranslator%24templates={1}&ctl00%24SiteContent%24ucTextTranslator%24checkShowVariants=on&ctl00%24SiteContent%24ucTextTranslator%24dlTemplates=General&ctl00%24SiteContent%24ucTextTranslator%24sourceText={2}&resultText=&ctl00%24SiteContent%24ucTextTranslator%24dlDirections={3}&ctl00%24SiteContent%24ucTextTranslator%24bTranslate=Translate&ctl00%24tbEmail=&ctl00%24tbName=&ctl00%24tbComment=&ctl00%24pollDiv%24tbSiteLang=en";
			query = string.Format(query, 
				HttpUtility.UrlEncode(viewState),
				PromtUtils.GetSubject(subject),
				HttpUtility.UrlEncode(phrase),
				PromtUtils.ConvertLanguagesPair(languagesPair));
				helper.AddPostData(query);
			}	
				
			
			
			string responseFromServer = helper.GetResponse();
		
			string translation = StringParser.Parse("class=\"rwin\">", "</div>", responseFromServer);
			
			result.Translations.Add(translation);
			lock(viewState)
			{
			viewState = StringParser.Parse("id=\"__VIEWSTATE\" value=\"", "\"", responseFromServer);
			}
			
		}
		
	}
	
	public class PromtTranslator : PromtBaseTranslator
	{
		public PromtTranslator()
		{
			Name = "_common_translator";
			
			AddSupportedTranslationFromEnglish(Language.Spanish);
			AddSupportedTranslationFromEnglish(Language.French);
			AddSupportedTranslationFromEnglish(Language.Portuguese);
			AddSupportedTranslation(Language.Russian, Language.Spanish);
			AddSupportedTranslationToEnglish(Language.French);
			AddSupportedTranslation(Language.French, Language.Spanish);
			AddSupportedTranslation(Language.Spanish, Language.Russian);
			AddSupportedTranslationToEnglish(Language.Spanish);
			AddSupportedTranslation(Language.Spanish, Language.French);
			AddSupportedTranslationToEnglish(Language.Portuguese);
			AddSupportedTranslation(Language.Italian, Language.Russian);
			AddSupportedTranslationToEnglish(Language.Italian);
		}
	}
	
	public class PromtErTranslator : PromtBaseTranslator
	{
		public PromtErTranslator()
		{
			Name = "_er_translator";
			AddSupportedSubject(SubjectConstants.Auto);
			AddSupportedSubject(SubjectConstants.Bank);
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Internet);
			AddSupportedSubject(SubjectConstants.Logistics);
			AddSupportedSubject(SubjectConstants.Informatics);
			AddSupportedSubject(SubjectConstants.Sport);
			AddSupportedSubject(SubjectConstants.Travel);
			
			AddSupportedTranslationFromEnglish(Language.Russian);
			
		}
	}

	public class PromtEgTranslator : PromtBaseTranslator
	{
		public PromtEgTranslator()
		{
			Name = "_eg_translator";
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Informatics);
			AddSupportedSubject(SubjectConstants.Football);
			
			AddSupportedTranslationFromEnglish(Language.German);
		}
	}

	public class PromtGrTranslator : PromtBaseTranslator
	{
		public PromtGrTranslator()
		{
			Name = "_gr_translator";
			AddSupportedSubject(SubjectConstants.Auto);
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Informatics);
			AddSupportedSubject(SubjectConstants.Internet);
			AddSupportedSubject(SubjectConstants.Football);
			
			AddSupportedTranslation(Language.German, Language.Russian);
		}
	}

	public class PromtGeTranslator : PromtBaseTranslator
	{
		public PromtGeTranslator()
		{
			Name = "_ge_translator";
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Informatics);
			AddSupportedSubject(SubjectConstants.Football);
			
			AddSupportedTranslationToEnglish(Language.German);
		}
	}
	
	public class PromtGFSTranslator : PromtBaseTranslator
	{
		public PromtGFSTranslator()
		{
			Name = "_gfs_translator";
			AddSupportedSubject(SubjectConstants.Football);
			
			AddSupportedTranslation(Language.German, Language.French);
			AddSupportedTranslation(Language.German, Language.Spanish);
			AddSupportedTranslation(Language.French, Language.German);
			AddSupportedTranslation(Language.Spanish, Language.German);

		}
	}
	

	public class PromtReTranslator : PromtBaseTranslator
	{
		public PromtReTranslator()
		{
			Name = "_re_translator";
			AddSupportedSubject(SubjectConstants.Auto);
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Internet);
			AddSupportedSubject(SubjectConstants.Logistics);
			AddSupportedSubject(SubjectConstants.Informatics);
			AddSupportedSubject(SubjectConstants.Travel);
			AddSupportedSubject(SubjectConstants.Phrasebook);
			
			AddSupportedTranslationToEnglish(Language.Russian);

		}
	}

	public class PromtRgTranslator : PromtBaseTranslator
	{
		public PromtRgTranslator()
		{
			Name = "_rg_translator";
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Internet);
			AddSupportedSubject(SubjectConstants.Football);
			
			AddSupportedTranslation(Language.Russian, Language.German);
		}
	}

	public class PromtRfTranslator : PromtBaseTranslator
	{
		public PromtRfTranslator()
		{
			Name = "_rf_translator";
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Internet);
			
			AddSupportedTranslation(Language.Russian, Language.French);
		}
	}
	
	public class PromtFrTranslator : PromtBaseTranslator
	{
		public PromtFrTranslator()
		{
			Name = "_fr_translator";
			AddSupportedSubject(SubjectConstants.Business);
			AddSupportedSubject(SubjectConstants.Internet);
			AddSupportedSubject(SubjectConstants.Perfumery);
			
			AddSupportedTranslation(Language.French, Language.Russian);
		}
	}
	
}
