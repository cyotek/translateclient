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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FreeCL.Forms;
using System.Reflection;
using System.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using FreeCL.RTL;

namespace Translate
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class TranslateMainForm : MainForm
	{
		public TranslateMainForm()
		{
			InitializeComponent();
			
			newToolStripButton.Visible = false;
			openToolStripButton.Visible = false;
			saveToolStripButton.Visible = false;
			printToolStripButton.Visible = false;
			tsEditSep1.Visible = false;
			tsEditSep4.Visible = false;
			helpToolStripButton.Visible = false;
			al.SetAction(languageSelector.sbInvert, aInvertTranslationDirection);
			languageSelector.sbInvert.MouseLeave += sbInvertMouseLeave;
			RegisterLanguageEvent(OnLanguageChanged);
			
			if(TranslateOptions.Instance.MainFormSize.Height == 0)
			{ //reposition
				System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;

				Location = new Point(workingRectangle.Width - DesktopBounds.Width, 30);
			}
			else
			{
				Size = TranslateOptions.Instance.MainFormSize;
				Location = TranslateOptions.Instance.MainFormLocation;
				TranslateOptions.Instance.MainFormSize = Size;
				TranslateOptions.Instance.MainFormLocation = Location;
				
				if(TranslateOptions.Instance.MainFormMaximized)
					WindowState = FormWindowState.Maximized;
			}
			
			if(TranslateOptions.Instance.LanguageSelectorWidth != 0)
			{
				pRight.Width = TranslateOptions.Instance.LanguageSelectorWidth;
			}
			
			if(TranslateOptions.Instance.HistoryHeight != 0)
			{
				languageSelector.pBottom.Height = TranslateOptions.Instance.HistoryHeight;
			}

			if(TranslateOptions.Instance.SourceHeight != 0)
			{
				tbFrom.Height = TranslateOptions.Instance.SourceHeight;
			}
			
			pLeft.Enabled = false;
			pLeft.Visible = false;
			splitterLeft.Enabled = false;
			splitterLeft.Visible = false;
			
			pTop.Enabled = false;
			pTop.Visible = false;
			splitterTop.Enabled = false;
			splitterTop.Visible = false;

			pBottom.Enabled = false;
			pBottom.Visible = false;
			splitterBottom.Enabled = false;
			splitterBottom.Visible = false;
			
			languageSelector.Profile = currentProfile;
			
			aTranslate.Shortcut = Keys.Control | Keys.Enter;
			miFile.DropDownItems.Remove(miTranslate);
			miFile.DropDownItems.Insert(0, miTranslate);
			
			miHelp.DropDownItems.Remove(miCheckUpdates);
			miHelp.DropDownItems.Insert(0, miCheckUpdates);
			
			miHelp.DropDownItems.Remove(miHelpSeparator1);
			miHelp.DropDownItems.Insert(1, miHelpSeparator1);

			miHelp.DropDownItems.Remove(miHelpSeparator2);
			miHelp.DropDownItems.Insert(0, miHelpSeparator2);

			miHelp.DropDownItems.Remove(miFeedback);
			miHelp.DropDownItems.Insert(0, miFeedback);
			
			miHelp.DropDownItems.Remove(miWebsite);
			miHelp.DropDownItems.Insert(0, miWebsite);
			

			lInputLang.Text = InputLanguage.CurrentInputLanguage.Culture.Parent.EnglishName.Substring(0,2).ToUpper(CultureInfo.InvariantCulture);
			
			aScrollResultPageDown.Shortcut = Keys.Control | Keys.PageDown;
			aScrollResultPageUp.Shortcut = Keys.Control | Keys.PageUp;
		}
		
		void TranslateMainFormLoad(object sender, EventArgs e)
		{
			KeyboardHook.Hotkey += OnSystemHotkey;		
			resBrowser.Clear();	
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void OnLanguageChanged()
		{
			miFile.Text = TranslateString("&Translate");
			aShowMainForm.Text = TranslateString("Show\\Hide");
			aTranslate.Text = TranslateString("Translate");
			aTranslate.Hint = TranslateString("Translate");
			aInvertTranslationDirection.Hint = TranslateString("Reverse translation direction");
			
			//fix bug with tray menu
			aAbout.Text = TranslateString("&About ...");
			aExit.Hint = TranslateString("Exit from application");
			aExit.Text = TranslateString("E&xit");
			aCheckUpdates.Text = TranslateString("Check Updates ...");
			aShowHtmlSource.Text  = TranslateString("Show HTML source");
			
			aControlCC.Text  = TranslateString("Activate on Ctrl+C+C hotkey"); 
			aControlInsIns.Text  = TranslateString("Activate on Ctrl+Ins+Ins hotkey"); 
			
			aFeedback.Text = TranslateString("Send feedback or bugreport ...");

			UpdateCaption();
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void UpdateCaption()
		{
			string TranslationComment = TranslateString("TranslationComment");
			string caption = "";
			if(TranslationComment != "TranslationComment")
				caption += TranslationComment;
			
			string selectionName = languageSelector.SelectionName;
			caption += "  {" + selectionName + "}";
				
			Text = Constants.AppName + " " + caption;
			
		
			if(!string.IsNullOrEmpty(selectionName))
				lSelectedLangsPair.Text = languageSelector.Selection.From.ToString().Substring(0, 3) +
					"->" + 
					languageSelector.Selection.To.ToString().Substring(0, 3);
		}
		
		TranslateProfile currentProfile = TranslateOptions.Instance.CurrentProfile;
		
		
		void TranslateMainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			
			if(e.CloseReason == CloseReason.UserClosing && !RealClosing)
			{
				e.Cancel = true;
				
				if(WindowState == FormWindowState.Normal && Size.Height != 0)
				{
					TranslateOptions.Instance.MainFormSize = Size;
					TranslateOptions.Instance.MainFormLocation = Location;
					TranslateOptions.Instance.LanguageSelectorWidth = pRight.Width;
					TranslateOptions.Instance.HistoryHeight = languageSelector.pBottom.Height;
					TranslateOptions.Instance.SourceHeight = tbFrom.Height;
				}
				
				TranslateOptions.Instance.MainFormMaximized = WindowState == FormWindowState.Maximized;			
				
				WindowState = FormWindowState.Minimized;
			}
			else
			{
				if(WindowState == FormWindowState.Normal && Size.Height != 0)
				{
					TranslateOptions.Instance.MainFormSize = Size;
					TranslateOptions.Instance.MainFormLocation = Location;
					TranslateOptions.Instance.LanguageSelectorWidth = pRight.Width;
					TranslateOptions.Instance.HistoryHeight = languageSelector.pBottom.Height;
					TranslateOptions.Instance.SourceHeight = tbFrom.Height;
				}
				
				TranslateOptions.Instance.MainFormMaximized = WindowState == FormWindowState.Maximized;			
			}
			
			
		}
		
		void TranslateMainFormResize(object sender, EventArgs e)
		{
			if(Visible && WindowState == FormWindowState.Minimized && TranslateOptions.Instance.MinimizeToTray)
			{
				Visible = false;
			}
			
			if(WindowState != FormWindowState.Minimized)
				TranslateOptions.Instance.MainFormMaximized = WindowState == FormWindowState.Maximized;
		}
		
		bool RealClosing;
		void AExitExecute(object sender, EventArgs e)
		{
		
			//real close
			RealClosing = true;
			Close();
		}
		
		void AShowMainFormExecute(object sender, EventArgs e)
		{
			if(Visible && TranslateOptions.Instance.MinimizeToTray)
				WindowState = FormWindowState.Minimized;
			else
			{
				Visible = true;
				if(TranslateOptions.Instance.MainFormMaximized)
				{
					if(WindowState != FormWindowState.Maximized)
						WindowState = FormWindowState.Maximized;
				}
				else
				{
					if(WindowState != FormWindowState.Normal)
						WindowState = FormWindowState.Normal;
				}
				
				if(Form.ActiveForm != this)
					Activate();				
				BringToForeground();
			}
		}
		
		void NiMainMouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
				AShowMainFormExecute(sender, e);
		}
		
		
		internal void TranslateProgressChanged(object sender, TranslateProgressChangedEventArgs e)
		{
			resBrowser.SetResult(e.TranslateResult, e.TranslateState.LanguagePair);
			pbMain.Value = e.ProgressPercentage;
		}
		
		internal void TranslateCompletedEventHandler(object sender, TranslateCompletedEventArgs e)
		{
			if(TranslateOptions.Instance.ResultWindowOptions.ShowQueryStatistics)
				resBrowser.SetStatistics(DateTime.Now.Ticks - startTranslateTicks);
			ResourceManager resources = new ResourceManager("Translate.Common.Icons", Assembly.GetExecutingAssembly());
			miAnimatedIcon.Image = (((System.Drawing.Icon)(resources.GetObject("StaticIcon")))).ToBitmap();
			tsbTranslate.Image = (((System.Drawing.Icon)(resources.GetObject("StaticIcon")))).ToBitmap();
			
			if(!e.Cancelled && e.Error == null && e.TranslateState.Results.Count > 0)
				resBrowser.AddAdvertisement(e.TranslateState);
			
			if(activeTranslateState == e.TranslateState)
			{
				activeTranslateState = null;
			}
			pbMain.Visible = false;
		}
		
		long startTranslateTicks;
		
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		void ATranslateExecute(object sender, EventArgs e)
		{
			StopCurrentTranslation(); 
			if(TranslateOptions.Instance.ResultWindowOptions.ShowQueryStatistics)
				startTranslateTicks = DateTime.Now.Ticks;
		
			tbFrom.Text = tbFrom.Text.Trim();
			ResourceManager resources = new ResourceManager("Translate.Common.Icons", Assembly.GetExecutingAssembly());
			miAnimatedIcon.Image = ((System.Drawing.Image)(resources.GetObject("AnimatedIcon")));
			tsbTranslate.Image = ((System.Drawing.Image)(resources.GetObject("AnimatedIcon")));
			ReadOnlyServiceSettingCollection settings = languageSelector.GetServiceSettings();//currentProfile.GetServiceSettings(tbFrom.Text, languageSelector.Selection);
			
			if(settings.Count > 0)
			{
				resBrowser.Clear();
				activeTranslateState = TranslateManager.TranslateAsync(languageSelector.Selection, tbFrom.Text, settings, TranslateProgressChanged, TranslateCompletedEventHandler);
				pbMain.Value = 7;
				pbMain.Visible = true;
				languageSelector.AddSelectionToHistory();
			}
			else
			{
				resBrowser.Clear();
				MessageBox.Show(this, TranslateString("Size or format of query don't supported by available translation services"), Constants.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StopCurrentTranslation()
		{
			if(activeTranslateState != null)
			{
				TranslateManager.CancelAsync(activeTranslateState);
				System.Windows.Forms.Application.DoEvents();
			}
		}
		
		AsyncTranslateState activeTranslateState;
		
		
		void ATranslateUpdate(object sender, EventArgs e)
		{
			aTranslate.Enabled = tbFrom.Text.Length > 0;	
		}
		
		void AInvertTranslationDirectionExecute(object sender, EventArgs e)
		{
			languageSelector.Invert();
		}
		
		//avoiding bug with tooltip bug
		void sbInvertMouseLeave(object sender, EventArgs e)
		{
			string tip = al.ToolTip.GetToolTip(languageSelector.sbInvert);
			al.ToolTip.SetToolTip(languageSelector.sbInvert, null);
			al.ToolTip.SetToolTip(languageSelector.sbInvert, tip);
		}
		
		
		
		void TranslateMainFormActivated(object sender, EventArgs e)
		{
			tbFrom.SelectAll();
			tbFrom.Focus();
		}
		
		
		void ResBrowserStatusTextChanged(object sender, EventArgs e)
		{
			lStatus.Text = resBrowser.StatusText;
		}
		
		void LanguageSelectorSelectionChanged(object sender, EventArgs e)
		{
			if(skipChangeInput)
				return;
				
			InputLanguageManager.SetInputLanguage(languageSelector.Selection.From);
			UpdateCaption();
		}
		
		bool skipChangeInput;
		void GlobalEventsIdle(object sender, EventArgs e)
		{
			if(!InputLanguageManager.IsInputLanguageChanged)
				return;
				
			lInputLang.Text = InputLanguage.CurrentInputLanguage.Culture.Parent.EnglishName.Substring(0,2).ToUpper(CultureInfo.InvariantCulture);
			
			if(!InputLanguageManager.IsLanguageSupported(languageSelector.Selection.From))
			{
				foreach(LanguagePair lp in languageSelector.History)
				{
					if(InputLanguageManager.IsLanguageSupported(lp.From))
					{
						try
						{
							skipChangeInput = true;
							languageSelector.Selection = lp;
							UpdateCaption();
						}
						finally
						{
							skipChangeInput = false;
						}
						tbFrom.Focus();
						return;
					}
				}
			}
			tbFrom.Focus();
		}
		
		void ACheckUpdatesExecute(object sender, EventArgs e)
		{
			UpdatesManager.CheckUpdates();
		}
		
		void TimerUpdaterTick(object sender, EventArgs e)
		{
			if(UpdatesManager.NeedCheck)
				UpdatesManager.CheckUpdates();
		}
		
		void ACheckUpdatesUpdate(object sender, EventArgs e)
		{
			aCheckUpdates.Enabled = UpdatesManager.State == UpdateState.None;
		}
		
		void TranslateMainFormShown(object sender, EventArgs e)
		{
			if(TranslateOptions.Instance.MinimizeToTrayOnStartup && !FreeCL.Forms.Application.IsCommandSwitchSet("nohide"))
			{
				WindowState = FormWindowState.Minimized;
			}
		}
		
		void AShowHtmlSourceExecute(object sender, EventArgs e)
		{
			resBrowser.ShowSource();			
		}
		
		void SbMainResize(object sender, EventArgs e)
		{
			lStatus.Width = sbMain.Width - 115 - (pbMain.Visible ? pbMain.Width : 0);
		}
		
		void PbMainVisibleChanged(object sender, EventArgs e)
		{
			SbMainResize(sender, e);
		}
		
		void AScrollResultDownExecute(object sender, EventArgs e)
		{
			resBrowser.DoScroll(Keys.Down);
		}
		
		void AScrollResultUpExecute(object sender, EventArgs e)
		{
			resBrowser.DoScroll(Keys.Up);
		}
		
		void AScrollResultPageDownExecute(object sender, EventArgs e)
		{
			resBrowser.DoScroll(Keys.PageDown);
		}
		
		void AScrollResultPageUpExecute(object sender, EventArgs e)
		{
			resBrowser.DoScroll(Keys.PageUp);
		}
		
		
		void ProcessSystemHotkey()
		{
			if(this != FreeCL.Forms.Application.ActiveForm)
			{
				if(!Visible)
					Visible = true;
				
				
				if(TranslateOptions.Instance.MainFormMaximized)
				{
					if(WindowState != FormWindowState.Maximized)
						WindowState = FormWindowState.Maximized;
				}
				else
				{
					if(WindowState != FormWindowState.Normal)
						WindowState = FormWindowState.Normal;
				}
				
				BringToForeground();
				
				tbFrom.SelectAll();
				tbFrom.Focus();
				if(FreeCL.UI.Clipboard.CanPaste)
					FreeCL.UI.Clipboard.Paste();
				if(KeyboardHook.TranslateOnHotkey)
					aTranslate.DoExecute();
			}
		}
		
		void OnSystemHotkey(object sender, EventArgs e)
		{
			BeginInvoke(new MethodInvoker(ProcessSystemHotkey));
		}
		
		
		
		void AControlCCExecute(object sender, EventArgs e)
		{
			KeyboardHook.ControlCC = !KeyboardHook.ControlCC;
			KeyboardHook.Init();
		}
		
		void AControlInsInsExecute(object sender, EventArgs e)
		{
			KeyboardHook.ControlInsIns = !KeyboardHook.ControlInsIns;
			KeyboardHook.Init();
		}
		
		void AControlInsInsUpdate(object sender, EventArgs e)
		{
			aControlCC.Checked = KeyboardHook.ControlCC;
			aControlInsIns.Checked = KeyboardHook.ControlInsIns;
		}
		
		void TbFromTextChanged(object sender, EventArgs e)
		{
			languageSelector.Phrase = tbFrom.Text.Trim();
		}
		
		void AWebsiteExecute(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Constants.HomeUrl);
		}
		
		void AFeedbackExecute(object sender, EventArgs e)
		{
			MailTo.Send(ApplicationInfo.SupportEmail, 
				TranslateString("Feedback for :") + " " + ApplicationInfo.ProductName + " " + ApplicationInfo.ProductVersion,
				TranslateString("<< Enter your feedback or bug report here (English, Ukrainian, Russian). >>"));
			
		}
	}
}
