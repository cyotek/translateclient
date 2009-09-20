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
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;
using FreeCL.RTL;

namespace Translate
{
	/// <summary>
	/// Description of FontsOptions.
	/// </summary>
	[Serializable()]
	public class FontsOptions
	{
		static Font defaultTextFont = null;
		public static Font DefaultTextFont
		{
			get
			{
				return defaultTextFont; 
			}
		}
		
		static FontsOptions()
		{
			/*
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "MenuFont", SystemFonts.MenuFont.Name, SystemFonts.MenuFont.SizeInPoints ));
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "DefaultFont", SystemFonts.DefaultFont.Name, SystemFonts.DefaultFont.SizeInPoints ));
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "CaptionFont", SystemFonts.CaptionFont.Name, SystemFonts.CaptionFont.SizeInPoints ));
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "DialogFont", SystemFonts.DialogFont.Name, SystemFonts.DialogFont.SizeInPoints ));
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "IconTitleFont", SystemFonts.IconTitleFont.Name, SystemFonts.IconTitleFont.SizeInPoints ));
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "SmallCaptionFont", SystemFonts.SmallCaptionFont.Name, SystemFonts.SmallCaptionFont.SizeInPoints ));
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "StatusFont", SystemFonts.StatusFont.Name, SystemFonts.StatusFont.SizeInPoints ));
			Console.WriteLine(string.Format("{0} name : {1}, size : {2}", "MessageBoxFont", SystemFonts.MessageBoxFont.Name, SystemFonts.MessageBoxFont.SizeInPoints ));
			*/
			
			if(MonoHelper.IsUnix) //monobug - MenuFont is Arial 11
				defaultTextFont = SystemFonts.DefaultFont;
			else
				defaultTextFont = SystemFonts.MenuFont;
		}
		
		public FontsOptions()
		{
		}
		
		FontData textControlFont = null;
		public FontData TextControlFont {
			get { return textControlFont; }
			set { textControlFont = value; }
		}
		
		[ XmlIgnore()]
		public Font TextControlFontProp
		{
			get
			{
				if(textControlFont == null)
					return SystemFonts.DefaultFont;
				else
					return textControlFont.GetFont(); 
			}	

			set
			{
				if(FontSelectionControl.FontEquals(value,SystemFonts.DefaultFont))
					textControlFont = null;
				else
					textControlFont = new FontData(value);
			}
		}
		
		
		FontData resultViewFont = null;
		public FontData ResultViewFont {
			get { return resultViewFont; }
			set { resultViewFont = value; }
		}
		
		[ XmlIgnore()]
		public Font ResultViewFontProp
		{
			get
			{
				if(resultViewFont == null)
					return defaultTextFont;
				else if(resultViewFont.FontName == "Tahoma" && resultViewFont.FontSize == 8.25f)
				{
					resultViewFont = null;
					return defaultTextFont;
				}	
				else
					return resultViewFont.GetFont(); 
			}	

			set
			{
				if(FontSelectionControl.FontEquals(value,defaultTextFont))
					resultViewFont = null;
				else
					resultViewFont = new FontData(value);
			}
		}
		
		[ XmlIgnore()]
		public Font ToolbarsFont
		{
			get
			{
				if(toolbarsFontData == null)
					return defaultTextFont;
				else
					return toolbarsFontData.GetFont();
			}
			
			set
			{
				if(FontSelectionControl.FontEquals(value,defaultTextFont))
					toolbarsFontData = null;
				else
					toolbarsFontData = new FontData(value);
			}
		}
		
		FontData toolbarsFontData = null;
		public FontData ToolbarsFontData {
			get { return toolbarsFontData; }
			set { toolbarsFontData = value; }
		}	
			
		public void Apply()
		{
			//HtmlHelper.DefaultTextFormat = "font-size: 8.25pt; font-family: Tahoma;";
			HtmlHelper.DefaultTextFormat = 
				string.Format("font-size: {0}pt; font-family: {1};", 
					ResultViewFontProp.SizeInPoints, ResultViewFontProp.Name).Replace(",", ".");
		}
	}
	
	[Serializable()]
	public class FontData
	{
		public FontData()
		{
		}

		public FontData(string fontName, float fontSize)
		{
			this.fontName = fontName;
			this.fontSize = fontSize;
		}

		public FontData(Font font)
		{
			this.fontName = font.Name;
			this.fontSize = font.SizeInPoints;
		}
				
		string fontName;
		public string FontName {
			get { return fontName; }
			set { fontName = value; }
		}
		
		float fontSize;
		public float FontSize {
			get { return fontSize; }
			set { fontSize = value; }
		}
		
		
		public Font GetFont()
		{
			return new Font(fontName, fontSize);
		}
	}
}
