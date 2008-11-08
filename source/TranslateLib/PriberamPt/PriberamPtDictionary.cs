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
	/// Description of PriberamPtDictionary.
	/// </summary>
	public class PriberamPtDictionary : MonolingualDictionary
	{
		public PriberamPtDictionary()
		{
			CharsLimit = 50;
			Name = "_dictionary";
		
			AddSupportedTranslation(new LanguagePair(Language.Portuguese, Language.Portuguese));
			AddSupportedSubject(SubjectConstants.Common);
		}

		static void SetResult(Result result, string data)
		{
			string firstInfo = "";
			string cat = "<font class=\"categoria\">";
			string translation;
			int idx = data.IndexOf(cat);
			if(idx >= 0)
			{	
				firstInfo = data.Substring(0, idx);
				firstInfo = StringParser.RemoveAll("<", ">", firstInfo);
				firstInfo = firstInfo.Replace("\r\n", "");
				firstInfo = firstInfo.Replace("\n", "");
				firstInfo = firstInfo.Replace("&lt;", "<");
				result.Translations.Add(firstInfo);
				translation = data.Substring(idx + cat.Length);
			}
			else
			{
				cat = "<div style=\"padding-left:20px; line-height:16px;\">";
				idx = data.IndexOf(cat);
				if(idx < 0)
					throw new TranslationException("Can't found start tag : " + cat+ " in string : " +  data);
					
				firstInfo = data.Substring(0, idx);
				firstInfo = StringParser.RemoveAll("<", ">", firstInfo);
				firstInfo = firstInfo.Replace("\r\n", "");
				firstInfo = firstInfo.Replace("\n", "");
				firstInfo = firstInfo.Replace("&lt;", "<");
				result.Translations.Add(firstInfo);
				translation = data.Substring(idx);
				cat = "<font class=\"categoria\">";
			}
		
			
			translation = translation.Replace("<font class=\"propriedade\">", cat);
			
			translation = "<begin>" + translation.Replace(cat, "<end><begin>") + "<end>";
			StringParser parser = new StringParser(translation);
			string[] blocks = parser.ReadItemsList("<begin>", "<end>");
			cat = "<div style=\"padding-left:20px; line-height:16px;\">";
			foreach(string block in blocks)
			{
				Result child = new Result(result.ServiceItem, "", result.LanguagePair, result.Subject);
				result.Childs.Add(child);
				
				
				idx = block.IndexOf(cat);
				if(idx >= 0)
				{	
					firstInfo = block.Substring(0, idx);
					firstInfo = StringParser.RemoveAll("<", ">", firstInfo);
					firstInfo = firstInfo.Replace("\r\n", "");
					firstInfo = firstInfo.Replace("\n", "");
					firstInfo = firstInfo.Replace("&lt;", "<");
					child.Abbreviation = firstInfo;
					
					parser = new StringParser(block);
					string[] translations = parser.ReadItemsList(cat, "</div>");
					foreach(string subtrans in translations)
					{
						child.Translations.Add(StringParser.RemoveAll("<", ">", subtrans)); 
					}
				}
			}
			
		}

		static string viewState;
		protected override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			if(string.IsNullOrEmpty(viewState))
			{  //emulate first access to site
				WebRequestHelper helpertop = 
					new WebRequestHelper(result, new Uri("http://www.priberam.pt/dlpo/dlpo.aspx"), 
						networkSetting, 
						WebRequestContentType.UrlEncodedGet, 
						Encoding.GetEncoding("iso-8859-1"));
						
				string responseFromServertop = helpertop.GetResponse();
				viewState = StringParser.Parse("name=\"__VIEWSTATE\" value=\"", "\"", responseFromServertop);
			}
			
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri("http://www.priberam.pt/dlpo/definir_resultados.aspx"), 
					networkSetting, 
					WebRequestContentType.UrlEncoded, 
						Encoding.GetEncoding("iso-8859-1"));

			string queryStr = "__EVENTTARGET=&__EVENTARGUMENT=&NOTVIEWSTATE={0}" +
				"&definActionTarget=%2Fdlpo%2Fdefinir_resultados.aspx" +
				"&pesqActionTarget=%2Fdlpo%2Fpesquisar_resultados.aspx" +
				"&conjugaActionTarget=%2Fdlpo%2Fconjugar_resultados.aspx" +
				"&CVdefinActionTarget=%2Fdcvpo%2Fdefinir_resultados.aspx" +
				"&CVpesqActionTarget=%2Fdcvpo%2Fpesquisar_resultados.aspx" +
				"&CVconjugaActionTarget=%2Fdcvpo%2Fconjugar_resultados.aspx" +
				"&h_seccao_index=&h_vista=&h_abrev_cat=1&h_abrev_flex=1&h_abrev_dom=1" +
				"&h_abrev_exemp=1&h_abrev_etim=1&h_abrev_outras=1&h_filtro=&h_dominio=" +
				"&h_var_geografica=&h_categoria=&h_reg_linguistico=&h_etimologia=" +
				"&h_desc_dominio=&h_desc_var_geografica=&h_desc_categoria=" +
				"&h_desc_reg_linguistico=&h_desc_etimologia=&accao=" +
				"&h_texto_pesquisa={1}&h_n={2}&accao=" +
				"&pal={1}&Dicionario%3ApalVisible={3}";


			
			string query = string.Format(queryStr, 
				HttpUtility.UrlEncode(viewState, helper.Encoding),
				HttpUtility.UrlEncode(phrase, helper.Encoding),
				0,
				HttpUtility.UrlEncode(phrase, helper.Encoding)
			);
				
			helper.AddPostData(query);
			
			string responseFromServer = helper.GetResponse();
		
			viewState = StringParser.Parse("name=\"__VIEWSTATE\" value=\"", "\"", responseFromServer);

			if(responseFromServer.Contains("A palavra não foi encontrada.</span>"))
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
		
			if(responseFromServer.Contains("<blockquote>"))
			{
				StringParser parser = new StringParser(responseFromServer);
				string[] subblocks = parser.ReadItemsList("<blockquote>", "</blockquote>");
				if(subblocks.Length == 1)
				{
					SetResult(result, subblocks[0]);
				}
				else
				{
					foreach(string block in subblocks)
					{
						Result child = new Result(result.ServiceItem, phrase, result.LanguagePair, result.Subject);
						result.Childs.Add(child);
						SetResult(child, block);
					}
				}
			}
			else if(responseFromServer.Contains("javascript:SeleccionaEntrada(&quot"))
			{
				StringParser parser = new StringParser(responseFromServer);
				string[] sublinks = parser.ReadItemsList("javascript:SeleccionaEntrada(&quot;", "&quot;)\""); 
				if(sublinks.Length <= 1)
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
				
				string key = "&quot;,&quot;";
				foreach(string sublink in sublinks)
				{
					int idx = sublink.IndexOf(key);
					if(idx < 0)
						throw new TranslationException("Can't found start tag : \"&quot;,&quot;\" in string : " +  sublink);
						
					string val = sublink.Substring(0, idx);	
					string num = sublink.Substring(idx + key.Length);
					query = string.Format(queryStr, 
						HttpUtility.UrlEncode(viewState, helper.Encoding),
						HttpUtility.UrlEncode(val, helper.Encoding),
						num,
						HttpUtility.UrlEncode(phrase, helper.Encoding));
						
					helper = 
						new WebRequestHelper(result, new Uri("http://www.priberam.pt/dlpo/definir_resultados.aspx"), 
							networkSetting, 
							WebRequestContentType.UrlEncoded, 
								Encoding.GetEncoding("iso-8859-1"));
					helper.AddPostData(query);
					
					responseFromServer = helper.GetResponse();
				
					viewState = StringParser.Parse("name=\"__VIEWSTATE\" value=\"", "\"", responseFromServer);
					
					if(responseFromServer.Contains("<blockquote>"))
					{
						parser = new StringParser(responseFromServer);
						string[] subblocks = parser.ReadItemsList("<blockquote>", "</blockquote>");
						foreach(string block in subblocks)
						{
							Result child = new Result(result.ServiceItem, val, result.LanguagePair, result.Subject);
							result.Childs.Add(child);
							SetResult(child, block);
						}
					}
				}
				
				if(result.Childs.Count == 0)
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
			}
			else
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			
		}

		
	}
}
