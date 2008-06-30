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
	/// Description of CybermovaComDictionary.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Cybermova")]
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public class CybermovaComDictionary : BilingualDictionary
	{
		public CybermovaComDictionary()
		{
			AddSupportedTranslation(new LanguagePair(Language.Ukrainian, Language.English));
			AddSupportedTranslation(new LanguagePair(Language.English, Language.Ukrainian));
			AddSupportedSubject(SubjectConstants.Common);
			
			IsQuestionMaskSupported = false;
			IsAsteriskMaskSupported = false;
			
			WordsCount = 50000;
			CharsLimit = 40;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		protected  override void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			string query = "http://www.cybermova.com/cgi-bin/olenuapro.pl";
			if(languagesPair.From == Language.Ukrainian)
				query = "http://www.cybermova.com/cgi-bin/oluaenpro.pl";
			WebRequestHelper helper = 
				new WebRequestHelper(result, new Uri(query), 
					networkSetting, 
					WebRequestContentType.UrlEncoded);
			helper.Encoding = Encoding.GetEncoding(1251);
			
			query = "Word={0}&EnUaBtn=En+--%3E+Ua";
			query = string.Format(query, HttpUtility.UrlEncode(phrase, helper.Encoding));
			helper.AddPostData(query);
			//"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.- "
			//"АБВГҐДЕЄЖЗИIЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюя'.- !?/,:;()\'\"\""
					
			string responseFromServer = helper.GetResponse();
			if(responseFromServer.IndexOf("<span id=result><B>") == -1)
			{
				result.ResultNotFound = true;
				throw new TranslationException("Nothing found");
			}
			else
			{
				string translation = StringParser.Parse("<body bgColor=LightSkyBlue>", "<FORM ACTION=", responseFromServer);
				
				StringParser phrasesParser = new StringParser(translation);
				string[] phrases = phrasesParser.ReadItemsList("<span id=result><B>", "</span>", "787654323");
				
				string subphrase;
				string subtranslation;
				Result subres = null;
				foreach(string data in phrases)
				{
					int idx = data.IndexOf("</B>");
					if(idx < 0)
						throw new TranslationException("Can't found </B> tag");
					subphrase = data.Substring(0, idx);	
					bool has_root = subphrase.IndexOf("||") > 0;
					string root = "";
					if(has_root)
					{
						root = subphrase.Substring(0, subphrase.IndexOf("||"));
						subphrase = subphrase.Replace("||", "");
					}
					
					subtranslation = data.Substring(idx + 4);	

					subtranslation = StringParser.RemoveAll("<IMG", ">", subtranslation);
					
					if(subphrase.Length > 1)
					{
						subtranslation = subtranslation.Replace(subphrase.Substring(0,1) + ". ", subphrase + " ");
						subtranslation = subtranslation.Replace(subphrase.Substring(0,1) + "</B>. ", subphrase + "</B> ");
					}

					if(has_root)						
					{
						subtranslation = subtranslation.Replace("~", root);
						subtranslation = subtranslation.Replace("||", "");
					}
					
					subtranslation = subtranslation.Replace("<B>", "");
					subtranslation = subtranslation.Replace("</B>", "");
					subtranslation = subtranslation.Replace("<I>", "");
					subtranslation = subtranslation.Replace("</I>", "");

						
					if(subtranslation.IndexOf("1.") < 0 &&  subtranslation.IndexOf("1)") < 0)
					{
						if(string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0 && phrases.Length == 1)
						{
							//single answer
							result.Translations.Add(subtranslation);
							return;
						}
	
						subres = CreateNewResult(subphrase, languagesPair, subject);
						subres.Translations.Add(subtranslation);
						result.Childs.Add(subres);
					}
					else
					{ //parsing on parts
						if(subtranslation.IndexOf("1.") < 0)
						{
							int subsubdefinitionIdx = subtranslation.IndexOf("1)");
							string abbr = subtranslation.Substring(0, subsubdefinitionIdx).Trim();
							subtranslation = subtranslation.Substring(subsubdefinitionIdx + 2);
							List<string> subsubsubtranslations = new List<string>();
							for(int i = 2; i < 100; i++)
							{
								int numIdx = subtranslation.IndexOf(i.ToString(CultureInfo.InvariantCulture) + ")");
								if(numIdx < 0)
								{  //last def
									if(subtranslation.Trim() != ".")
										subsubsubtranslations.Add(subtranslation.Trim());
									else
									{
										subsubsubtranslations.Add(abbr);
										abbr = "";
									}
									break;
								}
								else
								{
									string Definition = subtranslation.Substring(0, numIdx);
									subtranslation = subtranslation.Substring(numIdx + 2);
									subsubsubtranslations.Add(Definition.Trim());
								}
							}
							
							if(string.Compare(subphrase, phrase, true, CultureInfo.InvariantCulture) ==0 && phrases.Length == 1)
							{
								//single answer
								result.Abbreviation = abbr;
								foreach(string s in subsubsubtranslations)
									result.Translations.Add(s);
								return;
							}

							
							subres = CreateNewResult(subphrase, languagesPair, subject);
							subres.Abbreviation = abbr;
							foreach(string s in subsubsubtranslations)
									subres.Translations.Add(s);
							result.Childs.Add(subres);
						}
						else
						{
							int subdefinitionIdx = subtranslation.IndexOf("1.");
							string tmp_subtranslation = subtranslation.Substring(subdefinitionIdx + 2).Trim();
							if(string.IsNullOrEmpty(tmp_subtranslation))
								subtranslation = subtranslation.Substring(0, subdefinitionIdx).Trim();
							else 	
								subtranslation = tmp_subtranslation;
							List<string> subsubtranslations = new List<string>();
							for(int i = 2; i < 100; i++)
							{
								int numIdx = subtranslation.IndexOf(i.ToString(CultureInfo.InvariantCulture) + ".");
								if(numIdx < 0)
								{  //last def
									subsubtranslations.Add(subtranslation.Trim());
									break;
								}
								else
								{
									string Definition = subtranslation.Substring(0, numIdx);
									subtranslation = subtranslation.Substring(numIdx + 2);
									subsubtranslations.Add(Definition.Trim());
								}
							}

							subres = CreateNewResult(subphrase, languagesPair, subject);
							//subres.Translations.Add(" ");
							result.Childs.Add(subres);
							Result subsubres;
							
							foreach(string subsubtranslation in subsubtranslations)
							{
								if(subsubtranslation.IndexOf("1)") < 0)
								{
									subsubres = CreateNewResult("", languagesPair, subject);
									subsubres.Translations.Add(subsubtranslation);
									subres.Childs.Add(subsubres);
								}
								else
								{
									string subdata = subsubtranslation;
									int subsubdefinitionIdx = subdata.IndexOf("1)");
									string abbr = subdata.Substring(0, subsubdefinitionIdx).Trim();
									subdata = subdata.Substring(subsubdefinitionIdx + 2);
									List<string> subsubsubtranslations = new List<string>();
									for(int i = 2; i < 100; i++)
									{
										int numIdx = subdata.IndexOf(i.ToString(CultureInfo.InvariantCulture) + ")");
										if(numIdx < 0)
										{  //last def
											if(subdata.Trim() != ".")
												subsubsubtranslations.Add(subdata.Trim());
											else
											{
												subsubsubtranslations.Add(abbr);
												abbr = "";
											}
											break;
										}
										else
										{
											string Definition = subdata.Substring(0, numIdx);
											subdata = subdata.Substring(numIdx + 2);
											subsubsubtranslations.Add(Definition.Trim());
										}
									}
									
									subsubres = CreateNewResult(abbr, languagesPair, subject);
									foreach(string s in subsubsubtranslations)
										subsubres.Translations.Add(s);
									subres.Childs.Add(subsubres);
								}
							
							}
							
						}
					}
				}
				
			}
			
		}
		
	}
}
