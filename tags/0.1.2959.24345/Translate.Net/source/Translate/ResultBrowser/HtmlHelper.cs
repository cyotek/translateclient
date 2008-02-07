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
	/// Description of HtmlHelper.
	/// </summary>
	public static class HtmlHelper
	{
		public const string BodyStyle = "margin: -7px; border-width: 0px;";
		public const string DataCellStyle = "width: 100%;";
		public const string DefaultTextFormat = "font-size: 8.25pt; font-family: Tahoma;";
		public const string TableStyle = "text-align: left;" + DefaultTextFormat + DataCellStyle;
		public const string FirstRowCellStyle = DataCellStyle;
		public const string RowCellStyle = "border-top: 1px solid gray;" +  FirstRowCellStyle;
		public const string IconCellStyle = "width: 16px;";
		public const string DefaultTextStyle = "";
		public const string InfoTextStyle = "color: gray; font-size: 8pt;";
		public const string ErrorTextStyle = "color: #AA0000; " + DefaultTextStyle;
		public const string BoldTextStyle = "font-weight: bold; " + DefaultTextStyle;
		public const string ButtonTextStyle = "text-align: center; padding-left: 0px; padding-right: 0px;margin: 0px;" + DefaultTextFormat;

		public static void InitDocument(HtmlDocument doc)
		{
			if(doc == null)
				throw new ArgumentNullException("doc");
				
			doc.Body.Style = HtmlHelper.BodyStyle;
			
			//result table
			HtmlElement resultTable = doc.CreateElement("table");
			resultTable.Style = TableStyle;
			resultTable.Id = "result_table";
			resultTable.SetAttribute("border", "0");
			resultTable.SetAttribute("cellpadding", "1");
			resultTable.SetAttribute("cellspacing", "3");
			doc.Body.AppendChild(resultTable);
			
			
			HtmlElement tableBody = doc.CreateElement("TBODY");
			resultTable.AppendChild(tableBody);
			tableBody.Id = "result_table_body";
		}
		
		public static HtmlElement CreateDataRow(HtmlDocument doc, bool isClean)
		{
			if(doc == null)
				throw new ArgumentNullException("doc");
		
			HtmlElement tableBody = doc.GetElementById("result_table_body");
			HtmlElement tableRow = doc.CreateElement("TR");
			tableBody.AppendChild(tableRow);
			
			HtmlElement rowCell = doc.CreateElement("TD");
			tableRow.AppendChild(rowCell);
			if(isClean)
				rowCell.Style = HtmlHelper.FirstRowCellStyle;
			else
				rowCell.Style = HtmlHelper.RowCellStyle;
			
			HtmlElement rowTable = doc.CreateElement("table");
			rowCell.AppendChild(rowTable);
			rowTable.Style = HtmlHelper.TableStyle;
			rowTable.SetAttribute("border", "0");
			rowTable.SetAttribute("cellpadding", "1");
			rowTable.SetAttribute("cellspacing", "3");
			
			
			tableBody = doc.CreateElement("TBODY");
			rowTable.AppendChild(tableBody);
			HtmlElement dataRow = doc.CreateElement("TR");
			tableBody.AppendChild(dataRow);
			return dataRow;
		}
		
		
		public const string IconFormat = "<a href=\"{0}\"><img style=\"border: 0px solid ; width: 16px; height: 16px;\" alt=\"{0}, {1}\" src=\"{2}\" align=\"top\"></a>";
		public static HtmlElement CreateServiceIconCell(HtmlDocument doc, Service service)
		{
			if(doc == null)
				throw new ArgumentNullException("doc");

			if(service == null)
				throw new ArgumentNullException("service");
		
			HtmlElement tableCell = doc.CreateElement("TD");

			tableCell.Style = IconCellStyle;
			tableCell.SetAttribute("valign", "top");
			tableCell.InnerHtml = string.Format(CultureInfo.InvariantCulture, IconFormat, service.Url, service.Copyright, service.IconUrl);
			return tableCell;
		}
		
		
	}
}
