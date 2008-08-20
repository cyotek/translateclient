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

using FreeCL.RTL;
using FreeCL.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Translate
{
	/// <summary>
	/// Description of HtmlHelper.
	/// </summary>
	public static class HtmlHelper
	{
		public const string BodyStyle = "margin: -7px; border-width: 0px;";
		
		static string defaultTextFormat = "font-size: 8.25pt; font-family: Tahoma;";
		public static string DefaultTextFormat {
			get { return defaultTextFormat; }
			set { defaultTextFormat = value; }
		}
		
		public const string DefaultTextStyle = "";
		public const string InfoTextStyle = "color: gray; font-size: 8pt;";
		public const string ErrorTextStyle = "color: #AA0000; " + DefaultTextStyle;
		public const string BoldTextStyle = "font-weight: bold; " + DefaultTextStyle;
		
		public static string ButtonTextStyle {
			get { return "text-align: center; padding-left: 0px; padding-right: 0px;margin: 0px;" + DefaultTextFormat; }
		}
		
		public const string ServiceNameFormat = "<a href=\"{0}\">{1}</a>";

		public static void InitDocument(WebBrowser wBrowser)
		{
			WebBrowserHelper.InvokeScript(wBrowser, "SetTableStyle", new object[]{DefaultTextFormat});
		}
		
		
		public static void CreateTable(WebBrowser wBrowser, string parentName, string name)
		{
			WebBrowserHelper.InvokeScript(wBrowser, "CreateTable", 
				new object[]{parentName, name, DefaultTextFormat});
		}
		
		
		public const string IconFormat = "<a href=\"{0}\"><img style=\"border: 0px solid ; width: 16px; height: 16px;\" alt=\"{0}, {1}, {2}\" src=\"{3}\" align=\"top\"></a>";
		public static string GetServiceIconCellHtml(ServiceItem serviceItem)
		{
			return GetServiceIconCellHtml(serviceItem, false);
		}
		
		public static string GetServiceIconCellHtml(ServiceItem serviceItem, bool useOuterIconUrl)
		{
			if(serviceItem == null)
				throw new ArgumentNullException("service");
		
			string result = string.Format(CultureInfo.InvariantCulture, 
				IconFormat, 
				serviceItem.Service.Url,
				serviceItem.Service.Copyright, 
				ServiceSettingsContainer.GetServiceItemType(serviceItem), 
				useOuterIconUrl ? serviceItem.Service.IconUrl : 
				WebUI.ResultsWebServer.GetIconUrl(serviceItem.Service.Name));
			return result;
		}
		
		
		public static void OpenUrl(Uri url)
		{
			if(url.AbsoluteUri.Contains("wikipedia.org") 
				|| url.AbsoluteUri.Contains("wiktionary.org")
				|| url.AbsoluteUri.StartsWith("http://click.adbrite.com/mb/click.php?")
			)
				ProcessStartHelper.Start(url.AbsoluteUri);
			else
				ProcessStartHelper.Start(Constants.RedirectPageUrl + "?l=" + HttpUtility.UrlEncode(url.AbsoluteUri));
		}

		public static void AddTranslationCell(WebBrowser wBrowser, string parentName, bool isClean, string dataCellHtml, ServiceItem serviceItem, bool useOuterIconUrl)
		{
			string iconCellHtml = HtmlHelper.GetServiceIconCellHtml(serviceItem, useOuterIconUrl);
			AddTranslationCell(wBrowser, parentName, isClean, dataCellHtml, iconCellHtml);
		}

		public static void AddTranslationCell(WebBrowser wBrowser, bool isClean, string dataCellHtml, ServiceItem serviceItem)
		{
			AddTranslationCell(wBrowser, isClean, dataCellHtml, serviceItem, false);
		}
			
		public static void AddTranslationCell(WebBrowser wBrowser, bool isClean, string dataCellHtml, ServiceItem serviceItem, bool useOuterIconUrl)
		{
			string iconCellHtml = HtmlHelper.GetServiceIconCellHtml(serviceItem, useOuterIconUrl);
			AddTranslationCell(wBrowser, null, isClean, dataCellHtml, iconCellHtml);
		}
				
		public static void AddTranslationCell(WebBrowser wBrowser, bool isClean, string dataCellHtml)
		{
			AddTranslationCell(wBrowser, null, isClean, dataCellHtml, "");
		}
		
		public static void AddTranslationCell(WebBrowser wBrowser, string parentName, bool isClean, string dataCellHtml, string iconCellHtml)
		{
			WebBrowserHelper.InvokeScript(wBrowser, "AddTranslationCell", 
				new object[]{parentName, isClean, DefaultTextFormat, iconCellHtml, dataCellHtml});
		}
		
		public static void SetNodeInnerHtml(WebBrowser wBrowser, string nodeName, string nodeHTML)
		{
			WebBrowserHelper.InvokeScript(wBrowser, "SetNodeInnerHtml", 
				new object[]{nodeName, nodeHTML});
		}
		
		public static bool ClearTranslations(WebBrowser wBrowser)
		{
			return WebBrowserHelper.ObjectToBool(
					WebBrowserHelper.InvokeScript(wBrowser, "ClearTranslations", new object[]{})
				);
		}
		
		public static bool RemoveElement(WebBrowser wBrowser, string elementName)
		{
			return WebBrowserHelper.ObjectToBool(
					WebBrowserHelper.InvokeScript(wBrowser, "RemoveElement", new object[]{elementName})
				);					
		}
		
		public static string GetSelection(WebBrowser wBrowser)
		{
			return WebBrowserHelper.ObjectToString(
					WebBrowserHelper.InvokeScript(wBrowser, "GetCurrentSelection", new object[]{})
				);					
		}
		
	}
}
