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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Web; 
using System.Globalization;
using System.Resources;
using System.IO;
using System.Reflection;
using System.Collections.Generic;


namespace Translate
{
	/// <summary>
	/// Description of ServicesListHtmlGenerator.
	/// </summary>
	public static class ServicesListHtmlGenerator
	{
		public static void Generate()
		{
			foreach(string lang in FreeCL.RTL.LangPack.GetLanguages())
			{
				FreeCL.RTL.LangPack.Load(lang);
				string langcode = lang.ToLowerInvariant().Substring(0, 2);
				string unpacked_file = string.Format("..\\site\\services.unpackeddata.{0}.html", langcode);
				string java_file = string.Format("..\\site\\servicesdata_{0}.java", langcode);
				BuildFile(unpacked_file, java_file);
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
			}
			
			//FreeCL.RTL.LangPack.Load("Ukrainian");
			//BuildFile("..\\site\\services.unpackeddata.uk.html", "..\\site\\servicesdata_uk.java");
		}
		
		static void BuildFile(string fileName, string classFileName)
		{
			WebBrowser wBrowser = new WebBrowser();
			wBrowser.CreateControl();
			wBrowser.Navigate("about:blank");
			
			while(wBrowser.IsBusy)
				Application.DoEvents();

			while(wBrowser.Document == null)
				Application.DoEvents();

			while(wBrowser.Document.Body == null)
				Application.DoEvents();
			
			string template = ResultBrowser.GetCleanHtml();
			template = template.Replace("</head>", "</head>\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"main.css\">");
			template = template.Replace("Translate.Net result page", "Translate.Net services list");
			template = template.Replace("<body>", "<body>" +
				"<script language=\"javascript\">\r\n" +
				"function treeView(section)\r\n" +
				"{\r\n" +
				"  section_element = document.getElementById(section);" +
				"if(section_element.style.display == 'none')\r\n" +
				"  section_element.style.display = 'inline';\r\n" +
				"else\r\n" +
				"  section_element.style.display = 'none';\r\n" +
				"}\r\n" +
				"</script>\r\n");
			HtmlDocument doc = wBrowser.Document;
			doc.Write(template);
			
			while(wBrowser.IsBusy)
				Application.DoEvents();

			while(wBrowser.Document == null)
				Application.DoEvents();

			while(wBrowser.Document.Body == null)
				Application.DoEvents();
			
			GenerateDocument(doc);
			int bodyidx = template.IndexOf("<body>");
			template = template.Substring(0, bodyidx);
			StringBuilder body = new StringBuilder(doc.Body.OuterHtml);
			body.Replace("FONT-SIZE: 8.25pt;", "");
			body.Replace("FONT-FAMILY: Tahoma;", "");
			body.Replace("MARGIN: -7px;", "");
			body.Replace("</BODY>", "<br><span style='color: gray;'>Generated by : " + FreeCL.RTL.ApplicationInfo.ProductName + ", version :"+ FreeCL.RTL.ApplicationInfo.ProductVersion + "</span></body>" );
						
			
			string result = template + body.ToString() + "\r\n</html>";
			FileStream fs = new FileStream(fileName, FileMode.Create);
			StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
			sw.Write(result);
			sw.Flush();
			sw.Dispose();
			wBrowser.Dispose();
			
			fs = new FileStream(classFileName, FileMode.Create);
			string className = Path.GetFileNameWithoutExtension(classFileName);
			sw = new StreamWriter(fs, Encoding.BigEndianUnicode);
			sw.Write("import java.applet.*;\r\n\r\n");
			sw.Write("public class ");
			sw.Write(className);
			sw.Write(" extends Applet{\r\npublic String d(){\r\n");
			int i = 0;
			int cnt;
			StringBuilder substr;
			int var_num = 0;
			while(i < result.Length)
			{
				cnt = 1024;
				if(i + cnt > result.Length)
					cnt = result.Length - i;
				substr = new StringBuilder(result.Substring(i, cnt));
				substr.Replace("\"", "\\\"");
				substr.Replace("\r", "\\r");
				substr.Replace("\n", "\\n");
				sw.Write("String s_");
				sw.Write(var_num.ToString());
					var_num++;
				sw.Write(" = ");	
				sw.Write("\"" + substr.ToString() + "\";\r\n");
				i += 1024;
			}
			sw.Write("return ");	
			for(i = 0; i < var_num; i++)
			{
				sw.Write("s_");
				sw.Write(i.ToString());
				if(i + 1 < var_num)
					sw.Write("+");
			}
			sw.Write(";\r\n}\r\n}");
			sw.Flush();
			sw.Dispose();
			wBrowser.Dispose();
			
		}
		
		static void GenerateDocument(HtmlDocument doc)
		{
			HtmlHelper.InitDocument(doc);
			HtmlElement row = HtmlHelper.CreateDataRow(doc, true);
			
			HtmlElement tableCell = doc.CreateElement("TD");
			row.AppendChild(tableCell);
			tableCell.Style = HtmlHelper.DataCellStyle;
			GenerateListByUrlHtml(tableCell);
			
			row = HtmlHelper.CreateDataRow(doc, true);
			tableCell = doc.CreateElement("TD");
			row.AppendChild(tableCell);
			tableCell.Style = HtmlHelper.DataCellStyle;
			GenerateListByLangHtml(tableCell);
			
		}
		
		static string GetLangsPairsCount(int count)
		{
			string format = " (" + LangPack.TranslateString("{0} language pairs") + ")";
			return string.Format(format, count);
		}

		static string GetServicesCount(int count)
		{
			string format = " (" + LangPack.TranslateString("{0} services") + ")";
			return string.Format(format, count);
		}
		
		static void GenerateServiceItemSell(HtmlDocument doc, ServiceItem si, HtmlElement parent, bool first, bool generateLangs)
		{
			HtmlElement tableRow = HtmlHelper.CreateDataRow(doc, parent, first);
			//icon
			tableRow.AppendChild(HtmlHelper.CreateServiceIconCell(doc, si));
					
			//translate			
			HtmlElement tableCell = doc.CreateElement("TD");
			tableRow.AppendChild(tableCell);
			tableCell.Style = HtmlHelper.DataCellStyle;
			StringBuilder htmlString = new StringBuilder();
			
			htmlString.AppendFormat(CultureInfo.InvariantCulture, 
					HtmlHelper.ServiceNameFormat, 
					si.Service.Url, 
					HttpUtility.HtmlEncode(si.Service.Url.AbsoluteUri));
					
			htmlString.Append(", " + LangPack.TranslateString(si.Service.FullName));		
			
			if(si is MonolingualDictionary)
			{
				htmlString.Append(", ");
				htmlString.Append(LangPack.TranslateLanguage(si.SupportedTranslations[0].From));
			}
			
			htmlString.Append(", ");			
			htmlString.Append(ServiceSettingsContainer.GetServiceItemType(si));
					
			htmlString.Append(", ");
			htmlString.Append(HttpUtility.HtmlEncode(si.Service.Copyright));
		
			if(si is MonolingualDictionary)
			{
				tableCell.InnerHtml = htmlString.ToString();
				return;
			}	
			
			if(!generateLangs)
			{
				tableCell.InnerHtml = htmlString.ToString();
				return;
			}
				
			string langNodeName = si.FullName + "_langs";
			htmlString.Append("<br>" + GenerateTopNode(langNodeName, LangPack.TranslateString("Languages") + GetLangsPairsCount(si.SupportedTranslations.Count), 0.5));
			tableCell.InnerHtml = htmlString.ToString();
			
			SortedDictionary<string, SortedDictionary<string, string>> langs = new SortedDictionary<string, SortedDictionary<string, string>>();
			foreach(LanguagePair lp in si.SupportedTranslations)
			{
				string fromlang = LangPack.TranslateLanguage(lp.From);
				SortedDictionary<string, string> inner_list;
				if(!langs.TryGetValue(fromlang, out inner_list))
				{
					inner_list = new SortedDictionary<string, string>();
					langs.Add(fromlang, inner_list);
				}
				inner_list.Add(LangPack.TranslateLanguage(lp.To), "");
			}	
			
			HtmlElement langsNode = doc.GetElementById(langNodeName);	
			
			if(si.SupportedTranslations.Count <= 10)		
			{
				htmlString = new StringBuilder();
				foreach(KeyValuePair<string, SortedDictionary<string, string>> kvp_langs in langs)	
				{
					foreach(KeyValuePair<string, string> kvp_to_langs in kvp_langs.Value)
					{
						htmlString.Append("<li>" + kvp_langs.Key + "->" + kvp_to_langs.Key + "</li>");
					}
				}
				langsNode.InnerHtml = htmlString.ToString();
			}
			else
			{
				htmlString = new StringBuilder();
				foreach(KeyValuePair<string, SortedDictionary<string, string>> kvp_langs in langs)	
				{
					string nodeName = si.FullName + "_lang_" + kvp_langs.Key;
					htmlString.Append(GenerateTopNode(nodeName, kvp_langs.Key + "->" + GetLangsPairsCount(kvp_langs.Value.Count) , 1));
				}
				langsNode.InnerHtml = htmlString.ToString();
				
				foreach(KeyValuePair<string, SortedDictionary<string, string>> kvp_langs in langs)	
				{
					string nodeName = si.FullName + "_lang_" + kvp_langs.Key;
					HtmlElement node = doc.GetElementById(nodeName);	
					htmlString = new StringBuilder();
					foreach(KeyValuePair<string, string> kvp_to_langs in kvp_langs.Value)
					{
						htmlString.Append("<li>" + kvp_to_langs.Key + "</li>");
					}
					node.InnerHtml = htmlString.ToString();
				}
				
			}
			
		}
		
		static void GenerateListByUrlHtml(HtmlElement parent)
		{
			string nodeName = "list_by_url";
			parent.InnerHtml = GenerateTopNode(nodeName, LangPack.TranslateString("Grouped by Service's Url") + " - " + Manager.Services.Count.ToString());
			HtmlDocument doc = parent.Document;
			HtmlHelper.CreateTable(doc, doc.GetElementById(nodeName), nodeName + "_table");
			
			SortedDictionary<string, List<ServiceItem>> list = new SortedDictionary<string, List<ServiceItem>>();
			foreach(Service service in Manager.Services)
			{
				List<ServiceItem> inner_list = new List<ServiceItem>();
				list.Add(service.Url.AbsoluteUri, inner_list);
				foreach(Translator translator in service.Translators)
					inner_list.Add(translator);
				foreach(BilingualDictionary dictionary in service.BilingualDictionaries)
					inner_list.Add(dictionary);
				foreach(MonolingualDictionary dictionary in service.MonolingualDictionaries)					
					inner_list.Add(dictionary);
			}
			
			bool is_first = true; 
			foreach(KeyValuePair<string, List<ServiceItem>> kvp in list)
			{
				foreach(ServiceItem si in kvp.Value)
				{
					GenerateServiceItemSell(doc, si, doc.GetElementById(nodeName + "_table_body"), is_first, true);
					if(is_first) is_first = false;
				}
			}
		}

		static void GenerateListByLangHtml(HtmlElement parent)
		{
			string nodeName = "list_by_lang";
			parent.InnerHtml = GenerateTopNode(nodeName, LangPack.TranslateString("Grouped by Language") + GetLangsPairsCount(Manager.LanguagePairServiceItems.Count));
			HtmlDocument doc = parent.Document;
			HtmlHelper.CreateTable(doc, doc.GetElementById(nodeName), nodeName + "_table");
			
			SortedDictionary<string, SortedDictionary<string, List<ServiceItem>>> langs = new SortedDictionary<string, SortedDictionary<string, List<ServiceItem>>>();
			
			foreach(KeyValuePair<LanguagePair, ServiceItemsCollection> kvpData in Manager.LanguagePairServiceItems)
			{
				string fromlang = LangPack.TranslateLanguage(kvpData.Key.From);
				string tolang = LangPack.TranslateLanguage(kvpData.Key.To);
				SortedDictionary<string, List<ServiceItem>> inner_list;
				if(!langs.TryGetValue(fromlang, out inner_list))
				{
					inner_list = new SortedDictionary<string, List<ServiceItem>>();
					langs.Add(fromlang, inner_list);
				}
				List<ServiceItem> items;
				if(!inner_list.TryGetValue(tolang, out items))
				{
					items = new List<ServiceItem>();
					inner_list.Add(tolang, items);
				}
				
				foreach(ServiceItem si in kvpData.Value)
				{
					if(!(si is MonolingualSearchEngine || si is BilingualSearchEngine))
						items.Add(si);
				}	
			}

			
			HtmlElement top = doc.GetElementById(nodeName + "_table_body");
			foreach(KeyValuePair<string, SortedDictionary<string, List<ServiceItem>>> kvp in langs)
			{
				HtmlElement tableRow = HtmlHelper.CreateDataRow(doc, top, true);
				HtmlElement tableCell = doc.CreateElement("TD");
				tableRow.AppendChild(tableCell);
				tableCell.Style = HtmlHelper.DataCellStyle;
				string htmlString = "";
			
				string childnodeName = "by_lang_" + kvp.Key;
				htmlString += GenerateTopNode(childnodeName, "-" + kvp.Key + " ->" + GetLangsPairsCount(kvp.Value.Count));
				tableCell.InnerHtml = htmlString;
				
				
				HtmlHelper.CreateTable(doc, doc.GetElementById(childnodeName), childnodeName + "_table");
				HtmlElement lang_node = doc.GetElementById(childnodeName + "_table_body");
				foreach(KeyValuePair<string, List<ServiceItem>> kvpToLangs in kvp.Value)
				{
					if(kvpToLangs.Value.Count == 0)
						continue;
					tableRow = HtmlHelper.CreateDataRow(doc, lang_node, true);
					tableCell = doc.CreateElement("TD");
					tableRow.AppendChild(tableCell);
					tableCell.Style = HtmlHelper.DataCellStyle;
					htmlString = "";
				
					childnodeName = "by_lang_" + kvp.Key + "_" + kvpToLangs.Key;
					htmlString += GenerateTopNode(childnodeName, kvp.Key + "->" + kvpToLangs.Key + " -" + GetServicesCount(kvpToLangs.Value.Count) , 1);
					tableCell.InnerHtml = htmlString;
					
					HtmlHelper.CreateTable(doc, doc.GetElementById(childnodeName), childnodeName + "_table");
					HtmlElement sub_lang_node = doc.GetElementById(childnodeName + "_table_body");
					
					SortedDictionary<string, List<ServiceItem>> sortedServices = new SortedDictionary<string, List<ServiceItem>>();
					foreach(ServiceItem si in kvpToLangs.Value)
					{
						List<ServiceItem> inner_list;
						if(!sortedServices.TryGetValue(si.Service.Url.AbsoluteUri, out inner_list))
						{
							inner_list = new List<ServiceItem>();
							sortedServices.Add(si.Service.Url.AbsoluteUri, inner_list);
						}
						inner_list.Add(si);
					}
					
					bool is_first = true; 
					foreach(KeyValuePair<string, List<ServiceItem>> kvpServices in sortedServices)
					{
						foreach(ServiceItem si in kvpServices.Value)
						{
							GenerateServiceItemSell(doc, si, sub_lang_node, is_first, false);
							if(is_first) is_first = false;
						}
					}
					
				}
			}

		}
		
		static string GenerateTopNode(string nodeName, string nodeCaption)
		{
			return GenerateTopNode(nodeName, nodeCaption, 0);
		}
		
		static string GenerateTopNode(string nodeName, string nodeCaption, double indent)
		{
			StringBuilder sb = new StringBuilder();
			if(indent == 0)
				sb.Append("<div class=\"no_margins\" style= \"margin-left: 0em;\">");
			else
				sb.AppendFormat("<div  class=\"no_margins\" style= \"margin-left: {0}em;\">", indent);
				
			sb.Append("<a href=\"javascript:treeView('");
			sb.Append(nodeName);
			sb.Append("');\">");
			sb.Append(nodeCaption);
			sb.Append("</a><br><div ");
			sb.Append("id='");
			sb.Append(nodeName);
			sb.Append("' style=\"display: none;\">");
			sb.Append("</div></div>");
			return sb.ToString();
		}
		
	}
}
