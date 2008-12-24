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
using System.Diagnostics.CodeAnalysis;
using System.Web; 
using System.Text; 
using System.Globalization;
using System.Collections.Generic;
using System.Xml; 
using System.IO; 

namespace Translate
{
	/// <summary>
	/// Description of OpenTranEuDictionary.
	/// </summary>
	public class OpenTranEuDictionary: BilingualDictionary
	{
		public OpenTranEuDictionary()
		{
			CharsLimit = 100;
			LinesLimit = 1;
			Name = "_dictionary";
			
			SortedDictionary<Language, string> tmp = new SortedDictionary<Language, string>(langToKey);
			
			foreach(Language from in langToKey.Keys)
			{
				foreach(Language to in tmp.Keys)
				{
					if(from != to)
					{
					  	AddSupportedTranslation(new LanguagePair(from, to));					
					}
				}
			}	
			AddSupportedSubject(SubjectConstants.Informatics);
		}
		
		static SortedDictionary<Language, string> langToKey = new SortedDictionary<Language, string>();
		static OpenTranEuDictionary()
		{
			langToKey.Add(Language.Afrikaans, "af");
			langToKey.Add(Language.Arabic,"ar");
			langToKey.Add(Language.Azerbaijani,"az");
			
			langToKey.Add(Language.Belarusian,"be");
			langToKey.Add(Language.Bulgarian,"bg");
			langToKey.Add(Language.Bengali,"bn");
			langToKey.Add(Language.Breton, "br");
			langToKey.Add(Language.Bosnian, "bs");
			
			langToKey.Add(Language.Catalan,"ca");
			langToKey.Add(Language.Czech,"cs");
			langToKey.Add(Language.Welsh,"cy");
			
			langToKey.Add(Language.Danish,"da");
			langToKey.Add(Language.German,"de");
			
			langToKey.Add(Language.Greek,"el");
			langToKey.Add(Language.English,"en");
			langToKey.Add(Language.English_GB,"en_gb");
			langToKey.Add(Language.Esperanto,"eo");
			langToKey.Add(Language.Spanish,"es");
			langToKey.Add(Language.Estonian,"et");
			langToKey.Add(Language.Basque,"eu");
			
			langToKey.Add(Language.Persian,"fa");
			langToKey.Add(Language.Finnish,"fi");
			langToKey.Add(Language.French,"fr");
			langToKey.Add(Language.Frisian,"fy");
			
			langToKey.Add(Language.Irish,"ga");
			langToKey.Add(Language.Galician,"gl");
			langToKey.Add(Language.Gujarati,"gu");
			
			langToKey.Add(Language.Hebrew,"he");
			langToKey.Add(Language.Hindi,"hi");
			langToKey.Add(Language.Croatian,"hr");
			langToKey.Add(Language.Hungarian,"hu");
			
			
			langToKey.Add(Language.Indonesian,"id");
			langToKey.Add(Language.Icelandic,"is");
			langToKey.Add(Language.Italian,"it");
			langToKey.Add(Language.Japanese,"ja");
			
			langToKey.Add(Language.Georgian,"ka");
			langToKey.Add(Language.Kazakh,"kk");
			langToKey.Add(Language.Khmer,"km");
			langToKey.Add(Language.Korean,"ko");
			langToKey.Add(Language.Kurdish,"ku");
			
			langToKey.Add(Language.Laothian,"lo");
			langToKey.Add(Language.Latvian,"lv");
			langToKey.Add(Language.Lithuanian,"lt");
			

			langToKey.Add(Language.Macedonian,"mk");
			langToKey.Add(Language.Malayalam,"ml");
			langToKey.Add(Language.Mongolian,"mn");
			langToKey.Add(Language.Marathi,"mr");
			langToKey.Add(Language.Malay,"ms");
			langToKey.Add(Language.Maltese,"mt");

			langToKey.Add(Language.Norwegian_Bokmal,"nb");
			langToKey.Add(Language.Nepali,"ne");
			langToKey.Add(Language.Dutch,"nl");
			langToKey.Add(Language.Norwegian_Nynorsk,"nn");
			
			langToKey.Add(Language.Oriya,"or");

			langToKey.Add(Language.Punjabi,"pa");
			langToKey.Add(Language.Polish,"pl");
			langToKey.Add(Language.Portuguese,"pt");
			langToKey.Add(Language.Portuguese_BR,"pt_br");

			langToKey.Add(Language.Romanian,"ro");
			langToKey.Add(Language.Russian,"ru");
			//rw	

			//se Sámegiella
			
			langToKey.Add(Language.Slovak,"sk");
			langToKey.Add(Language.Slovenian,"sl");
			langToKey.Add(Language.Albanian, "sq");
			langToKey.Add(Language.Serbian,"sr");
			//ss
			langToKey.Add(Language.Swedish,"sv");
					

 			langToKey.Add(Language.Tamil,"ta");
			langToKey.Add(Language.Telugu,"te");
 			langToKey.Add(Language.Tajik,"tg");
 			langToKey.Add(Language.Thai,"th");
 			langToKey.Add(Language.Turkish,"tr");

			langToKey.Add(Language.Ukrainian,"uk");
 			langToKey.Add(Language.Uzbek,"uz");
					
			langToKey.Add(Language.Vietnamese,"vi");					

			//wa			
			//xh
			langToKey.Add(Language.Chinese_CN,"zh_cn");
			langToKey.Add(Language.Chinese_TW,"zh_tw");
			
			//zu
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
		
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://open-tran.eu/RPC2"), 
					networkSetting, 
					WebRequestContentType.XmlRpc);
			
			helper.Referer = "http://translateclient.googlepages.com/";
			
			string from = ConvertLanguage(languagesPair.From);
			string to = ConvertLanguage(languagesPair.To);
			
			helper.InitXmlRpcMethodCall("suggest2", new object[]{phrase, from, to});
			
			string responseFromServer = helper.CallXmlRpcMethod();
			TextReader stringReader = new StringReader(responseFromServer);

			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreComments = true;
			settings.IgnoreProcessingInstructions = true;
			settings.IgnoreWhitespace = true;
			
			using (XmlReader reader = XmlReader.Create(stringReader, settings)) 
			{
				reader.Read(); //xml
				reader.Read(); //<methodResponse>
				int count = 0;
				string translation = "";
				string subphrase, servicename;
				Result subres;
				
				while(reader.ReadToFollowing("int"))
				{
					count = reader.ReadElementContentAsInt();
					if(reader.ReadToFollowing("string"))
						translation = reader.ReadElementContentAsString();
					reader.ReadToFollowing("array");	
					
					subres = CreateNewResult(translation, languagesPair, subject);
					if(count != 1)
						subres.Abbreviation = " {" + count.ToString() + "}";
					result.Childs.Add(subres);
					//subres.Translations.Add(translation);
					
					
					//projects
					using(XmlReader projectsReader = reader.ReadSubtree())
					{
						while(projectsReader.ReadToFollowing("struct"))
						{
							using(XmlReader inner = reader.ReadSubtree())
							{
								inner.ReadToFollowing("int");	//count
								count = reader.ReadElementContentAsInt();
								
								inner.ReadToFollowing("string");	//path
								servicename = reader.ReadElementContentAsString().Substring(2);
								inner.ReadToFollowing("int");	//flags
								inner.ReadToFollowing("string");	//name
								servicename = reader.ReadElementContentAsString() + " " + servicename;
								inner.ReadToFollowing("string");	//orig_phrase
								subphrase = inner.ReadElementContentAsString();
								
								subphrase += " - " + servicename;
								subphrase += " {" + count.ToString() + "}";

								
								subres.Translations.Add(subphrase);
								inner.Close();
							}
						}
						projectsReader.Close();
					}
					reader.ReadToFollowing("int");	 //level (value)
				}
			}
			
			if(result.Childs.Count > 0)
			{
				result.ArticleUrl = "http://" + from + "." + to + ".open-tran.eu/suggest/" + phrase;
				result.ArticleUrlCaption = phrase;
			}
			else
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}

			
		
		}		
		
	}
}
