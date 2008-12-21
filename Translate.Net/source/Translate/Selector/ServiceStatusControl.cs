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


using FreeCL.RTL;
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
using Microsoft.Win32;

namespace Translate
{
	/// <summary>
	/// Description of ServiceStatusControl.
	/// </summary>
	public partial class ServiceStatusControl : UserControl
	{
		public ServiceStatusControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ServiceStatusControlLoad(object sender, EventArgs e)
		{
			Clear();
			Status = Status;
		}
		
		bool showLanguage;
		public bool ShowLanguage {
			get { return showLanguage; }
			set { showLanguage = value; }
		}
		
		ServiceSettingsContainer status;
		internal ServiceSettingsContainer Status {
			get { return status; }
			set 
			{
				Clear();
				if(value != null)
				{
					status = value; 
					if(IsHandleCreated)
						LoadStatus();
				}
			}
		}
		
		bool shortView;
		public bool ShortView {
			get { return shortView; }
			set { shortView = value; }
		}
		
		void LoadStatus()
		{
			Clear();
			while(!isClean)
				Application.DoEvents();
			Wait();	
			
			
			string htmlString = string.Format(CultureInfo.InvariantCulture, 
					HtmlHelper.ServiceNameFormat, 
					status.Setting.ServiceItem.Service.Url, 
					HttpUtility.HtmlEncode(LangPack.TranslateString(status.Setting.ServiceItem.Service.FullName)));
			
			htmlString += "<br><b>" + LangPack.TranslateString("Type") + "</b> : " + status.Type;
			
			if(!shortView)
			{
				if(status.Setting.Subject != SubjectConstants.Common)
					htmlString+= "<br>" + "<b>" + LangPack.TranslateString("Subject") + "</b> : " + LangPack.TranslateString(status.Setting.Subject);
			}	
			else
			{
				htmlString+= ", " + "<b>" + LangPack.TranslateString("Subject") + "</b> : " + LangPack.TranslateString(status.Setting.Subject);
			}
			
			if(showLanguage)
			{
				if(!shortView)
				{
					htmlString+= "<br>" + LangPack.TranslateLanguage(status.Setting.LanguagePair.From) +
							"->" + 
							LangPack.TranslateLanguage(status.Setting.LanguagePair.To);
				}		
				else
				{
					htmlString+= "<br><b>" + LangPack.TranslateString("Translation direction") + 
							"</b> : " +
							LangPack.TranslateLanguage(status.Setting.LanguagePair.From) +
							"->" + 
							LangPack.TranslateLanguage(status.Setting.LanguagePair.To);
				
				}
			
			}
			
			if(!shortView)
			{
				if(status.DisabledByUser)
				{
					htmlString+= "<br>" + LangPack.TranslateString("<b>Status</b> : Disabled");
					htmlString+= string.Format("<br><button id=\"btn\" type=\"button\"  align=\"top\" style=\"{0}\">{1}</button>", 
						HtmlHelper.ButtonTextStyle, LangPack.TranslateString("Enable"));
				}
				else if(status.Enabled)
				{
					htmlString+= "<br>" + LangPack.TranslateString("<b>Status</b> : Enabled");
					htmlString+= string.Format("<br><button id=\"btn\" type=\"button\" align=\"top\" style=\"{0}\">{1}</button>", 
						HtmlHelper.ButtonTextStyle, LangPack.TranslateString("Disable"));
					
				}
				else
				{
					htmlString+= "<br>" + LangPack.TranslateString("<b>Status</b> : Error");
					htmlString+= " - " + string.Format("<span style=\"" + HtmlHelper.ErrorTextStyle + "\">{0}</span>",  status.Error);
					htmlString+= string.Format("<br><button id=\"btn\" type=\"button\" align=\"top\" style=\"{0}\">{1}</button>", 
						HtmlHelper.ButtonTextStyle, LangPack.TranslateString("Disable"));
				}
			}	
			
			if(status.Setting.ServiceItem.CharsLimit != -1)
			{
				htmlString+= "<br>" + "<b>";
				htmlString+= string.Format(LangPack.TranslateString("Limit {0} : {1} characters"), 
					"</b>", status.Setting.ServiceItem.CharsLimit);
			}

			if(status.Setting.ServiceItem.LinesLimit != -1)
			{
				htmlString+= "<br>" + "<b>";
				htmlString+= string.Format(LangPack.TranslateString("Limit {0} : {1} lines"), 
					"</b>", status.Setting.ServiceItem.LinesLimit);
			}

			if(status.Setting.ServiceItem.WordsLimit != -1)
			{
				htmlString+= "<br>" + "<b>";
				htmlString+= string.Format(LangPack.TranslateString("Limit {0} : {1} words"), 
					"</b>", status.Setting.ServiceItem.WordsLimit);
			}
			
			if(status.IsAsteriskMaskSupported || status.IsQuestionMaskSupported)
			{
				htmlString+= "<br>" + "<b>" + LangPack.TranslateString("Masks") + "</b> : ";
				if(status.IsAsteriskMaskSupported)
					htmlString+= "'*'"; 
				
				if(status.IsAsteriskMaskSupported && status.IsQuestionMaskSupported)
					htmlString+= ","; 
					
				if(status.IsQuestionMaskSupported)
					htmlString+= "'?'"; 
			}
				
				
			
			//tableCell.InnerHtml = htmlString;
			Wait();
			HtmlHelper.AddTranslationCell(wbStatus, isClean, htmlString, status.Setting.ServiceItem);
			
			HtmlElement button =  WebBrowserHelper.GetDocument(wbStatus).GetElementById("btn");
			if(button != null)
			{
				button.Click += OnButtonClick;
			}

			isClean = false;
			RealRecalcSizes();
		}
		
		public event EventHandler ButtonClick;
		private void OnButtonClick(Object sender, HtmlElementEventArgs e)
		{
			if(ButtonClick != null)
				ButtonClick(this, new EventArgs());
		}
		
		bool isLoaded;
		bool isClean;
		public void Clear()
		{
			if(isClean)
				return;
				
			bool forceCleaning = false;	
			if (WebBrowserHelper.GetDocument(wbStatus) != null)
			{ 
				Wait();	
				
				if(WebBrowserHelper.GetDocument(wbStatus) != null)
				{
					if(!HtmlHelper.ClearTranslations(wbStatus))
					{	//possible disabled javascript or error
						forceCleaning = true;
					}	
					else
						isClean = true;	//avoid double cleaning
				}
				else
					forceCleaning = true;
			}
			
			if(WebBrowserHelper.GetDocument(wbStatus) == null || forceCleaning)
			{
				if(WebUI.ResultsWebServer.Uri != null)
					wbStatus.Navigate(new Uri(WebUI.ResultsWebServer.Uri, "ServiceStatus.aspx"));
			}
			RecalcSizes();
			isClean = true;
		}
		
		public void Wait()
		{
			while(!isLoaded)
			{
				Application.DoEvents();
				System.Threading.Thread.Sleep(100);
			}	
		
			WebBrowserHelper.Wait(wbStatus);
		}
		
		void WbStatusDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			HtmlHelper.InitDocument(wbStatus);
			//wbStatus.Document.Body.Style = wbStatus.Document.Body.Style + 
			//	";background-color :ButtonHighlight;";
			isLoaded = true;
			RecalcSizes();			
		}
		
		bool inResize;
		
		bool needRecalcSize;
		void RecalcSizes()
		{
			needRecalcSize = true;
		}
		
		void RealRecalcSizes()
		{
			
			if(inResize)
				return;
				
			inResize = true;
			bool isHeightChanged = false;
			try
			{
				int allowedWidth = Width + 6;
				
				if(wbStatus.Width != allowedWidth)
				{
					wbStatus.Width = allowedWidth;
				}
					
				int allowedHeight = ClientSize.Height;
				
				if((isClean) || (WebBrowserHelper.GetDocument(wbStatus) == null || WebBrowserHelper.GetDocument(wbStatus).Body == null))
				{
					if(wbStatus.Height != allowedHeight)
					{
						wbStatus.Height = allowedHeight;
						isHeightChanged = true;
					}
				}	
				else if(WebBrowserHelper.GetDocument(wbStatus) != null && WebBrowserHelper.GetDocument(wbStatus).Body != null && WebBrowserHelper.GetDocument(wbStatus).Body.ScrollRectangle.Height != 0)
				{
					int height = WebBrowserHelper.GetDocument(wbStatus).Body.ScrollRectangle.Height; // + 2;
					if(wbStatus.Height != height)
					{
						//wbStatus.Height = height;
						//ClientSize = new Size(ClientSize.Width, height);
						wbStatus.Height = height;
						Height = height-15;
						isHeightChanged = true;
					}
				}
			}
			finally
			{
				inResize = false;
			}
			if(isHeightChanged && IsHandleCreated)
			{
				needRecalcSize = true;
			}
			else
				needRecalcSize = false;
		}
		
		
		void GEventsIdle(object sender, EventArgs e)
		{
			if(needRecalcSize)
				RealRecalcSizes();
		}
		
		void ServiceStatusControlResize(object sender, EventArgs e)
		{
			wbStatus.Width = Width+4;
		}
		
		void WbStatusNavigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if(e.Url.Host == "127.0.0.1" && e.Url.Port == WebUI.ResultsWebServer.Port)
				return;
		
			HtmlHelper.OpenUrl(e.Url);
			e.Cancel = true;
		}
	}
}
