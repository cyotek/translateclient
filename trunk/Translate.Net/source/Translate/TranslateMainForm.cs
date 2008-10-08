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
			
			if(TranslateOptions.Instance.MainFormSize.Height == 0 || 
				TranslateOptions.Instance.MainFormLocation.X <= 0 ||
				TranslateOptions.Instance.MainFormLocation.Y <= 0
				)
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
			
			tbFrom.Font = TranslateOptions.Instance.FontsOptions.TextControlFontProp;
			
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
			
			PlaceResultViewVertical(TranslateOptions.Instance.ResultWindowOptions.DockAtTop);
			PlaceResultViewHorizontal(TranslateOptions.Instance.ResultWindowOptions.DockAtLeft);
			ApplyToolbarsOptions();
			
			aTranslate.Shortcut = Keys.Control | Keys.Enter;
			aSearchInGoogle.Shortcut = Keys.Control | Keys.Shift | Keys.Enter;
			miFile.DropDownItems.Remove(miTranslate);
			miFile.DropDownItems.Insert(0, miTranslate);
			
			miHelp.DropDownItems.Remove(miCheckUpdates);
			miHelp.DropDownItems.Insert(0, miCheckUpdates);
			
			miHelp.DropDownItems.Remove(miHelpSeparator1);
			miHelp.DropDownItems.Insert(1, miHelpSeparator1);

			miHelp.DropDownItems.Remove(miHelpSeparator2);
			miHelp.DropDownItems.Insert(0, miHelpSeparator2);

			bool visibleDonate = !(FreeCL.RTL.LangPack.CurrentLanguage == "Russian" || FreeCL.RTL.LangPack.CurrentLanguage == "Ukrainian");
			if(visibleDonate)
			{
				miHelp.DropDownItems.Remove(miDonate);
				miHelp.DropDownItems.Insert(0, miDonate);
			}

			miHelp.DropDownItems.Remove(miFeedback);
			miHelp.DropDownItems.Insert(0, miFeedback);
			
			miHelp.DropDownItems.Remove(miWebsite);
			miHelp.DropDownItems.Insert(0, miWebsite);

			miHelp.DropDownItems.Remove(miOnlineHelp);
			miHelp.DropDownItems.Insert(0, miOnlineHelp);
			
			
			msMain.Items.Remove(miView);
			msMain.Items.Insert(2, miView);
			
			msMain.Items.Remove(miProfiles);
			msMain.Items.Insert(3, miProfiles);

			aScrollResultPageDown.Shortcut = Keys.Control | Keys.PageDown;
			aScrollResultPageUp.Shortcut = Keys.Control | Keys.PageUp;
			
			for(int i = 1; i < 10; i++)
			{
				FreeCL.UI.Actions.Action action = new FreeCL.UI.Actions.Action();
				action.Shortcut = Keys.Control | (Keys)Enum.Parse(typeof(Keys), "D" + i.ToString());
				action.Execute += ProfileSwitchOnCtrl1Execute;
				profileSwitchOnCtrl1Actions.Add(action);
				al.Actions.Add(action);				
			}
			
			UpdateTbFromStat();
			UpdateProfiles();
			
			miAnimatedIcon.ToolTipText = aWebsite.Text;
		}
		
		List<FreeCL.UI.Actions.Action> profileSwitchOnCtrl1Actions = new List<FreeCL.UI.Actions.Action>();
		
		void TranslateMainFormLoad(object sender, EventArgs e)
		{
			KeyboardHook.Hotkey += OnSystemHotkey;		
			resBrowser.Clear();	
		}
		
		int SpecialButtonsCount = 3;
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void OnLanguageChanged()
		{
			miFile.Text = TranslateString("&Translate");
			aShowMainForm.Text = TranslateString("Show\\Hide");
			aTranslate.Text = TranslateString("Translate");
			aTranslate.Hint = aTranslate.Text;
			aSearchInGoogle.Text = TranslateString("Search in Google");
			aSearchInGoogle.Hint = aSearchInGoogle.Text;
			miSearchInGoogle.Text = aSearchInGoogle.Text;
			miSearchInGoogle.ToolTipText = aSearchInGoogle.Text;
			
			aStopTranslate.Text = TranslateString("Stop");
			aStopTranslate.Hint = TranslateString("Stop current operation");
			tsbStop.DisplayStyle = ToolStripItemDisplayStyle.Image;
			
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
			aDonate.Text = TranslateString("Donate ...");
			aOnlineHelp.Text = TranslateString("Online Help");
			miOnlineHelp2.ToolTipText = aOnlineHelp.Text;
			
			bool visibleDonate = !(FreeCL.RTL.LangPack.CurrentLanguage == "Russian" || FreeCL.RTL.LangPack.CurrentLanguage == "Ukrainian");
			miHelp.DropDownItems.Remove(miDonate);
			if(visibleDonate)
			{
				miHelp.DropDownItems.Insert(2, miDonate);
			}
			
			aAddProfile.Hint = TranslateString("Add new user profile");
			aAddProfile.Text = aAddProfile.Hint + " ...";
			
			aRemoveProfile.Hint = TranslateString("Remove user profile");
			aRemoveProfile.Text = aRemoveProfile.Hint + " ...";
			
			miProfiles.Text = TranslateString("Profile");
			miView.Text = TranslateString("View");
			
			aSetProfileProperties.Hint = TranslateString("Set profile properties");
			aSetProfileProperties.Text = aSetProfileProperties.Hint + " ...";;
			
			aEditProfileServices.Hint = TranslateString("Edit services");
			aEditProfileServices.Text = aEditProfileServices.Hint + " ...";;
			
			aShowProfileServices.Hint = TranslateString("Show services list");
			aShowProfileServices.Text = aShowProfileServices.Hint;
			
			aShowProfileLanguages.Hint = TranslateString("Show languages list");
			aShowProfileLanguages.Text = aShowProfileLanguages.Hint;
			
			aShowProfileSubjects.Hint = TranslateString("Show subjects list");
			aShowProfileSubjects.Text = aShowProfileSubjects.Hint;
			
			
			miProfileView.Text = TranslateString("View");
			miProfileView2.Text = miProfileView.Text;
			
			aShowStatistics.Text = TranslateString("Show query time and other information");
			aShowErrors.Text = TranslateString("Mark by red color untranslated words");
			aHideWithoutResult.Text = TranslateString("Don't show \"Nothing found\" results");
			aShowTranslationDirection.Text = TranslateString("Show direction of translation");
			aShowServiceName.Text = TranslateString("Show names of services");
			aShowAccents.Text = TranslateString("Show accents");
			
			aIncludeMonolingualDicts.Hint = TranslateString("Include monolingual dictionaries in translation");
			aIncludeMonolingualDicts.Text = aIncludeMonolingualDicts.Hint;
			
			aFilterLanguages.Text = TranslateString("Filter of languages") + " ...";
			aFilterLanguages.Hint = TranslateString("Allow to choose languages for use");

			for(int i = SpecialButtonsCount + 1; i < tsTranslate.Items.Count; i++)
			{
				UserTranslateProfile pf = tsTranslate.Items[i].Tag as UserTranslateProfile;
				if(pf == null)
					tsTranslate.Items[i].Text = TranslateString(TranslateOptions.Instance.DefaultProfile.Name);
				else
					tsTranslate.Items[i].ToolTipText = GetProfileName(pf);
			}

			miResultViewPlace.Text = TranslateString("Result view placement");
			aPlaceResultViewTop.Text = TranslateString("Top");
			aPlaceResultViewTop.Hint = TranslateString("Place result view at top");
			aPlaceResultViewBottom.Text = TranslateString("Bottom");
			aPlaceResultViewBottom.Hint = TranslateString("Place result view at bottom");
			aPlaceResultViewLeft.Text = TranslateString("Left");
			aPlaceResultViewLeft.Hint = TranslateString("Place result view at left");
			aPlaceResultViewRight.Text = TranslateString("Right");
			aPlaceResultViewRight.Hint = TranslateString("Place result view at right");
			
			UpdateCaption();
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void UpdateCaption()
		{
			string TranslationComment = TranslateString("TranslationComment");
			string caption = "";
			if(TranslationComment != "TranslationComment")
				caption += TranslationComment;
			
			UserTranslateProfile pf = currentProfile as UserTranslateProfile;
			if(pf != null && languageSelector.Selection != pf.TranslationDirection)
				caption += " " + pf.Name + " ";

			string selectionName = "";
			if(languageSelector.Selection != null)
				selectionName = languageSelector.SelectionName;
			caption += "  {" + selectionName + "}";
				
			Text = Constants.AppName + " " + caption;
			
		
			if(!string.IsNullOrEmpty(selectionName))
				lSelectedLangsPair.Text = ServiceSettingsContainer.GetShortNameOfTranslateDirection(languageSelector.Selection);
			else 		
				lSelectedLangsPair.Text = "";
					
			lInputLang.Text = InputLanguage.CurrentInputLanguage.Culture.Parent.EnglishName.Substring(0,2).ToUpper(CultureInfo.InvariantCulture);		
			if(currentProfile != null)
			{
				if(pf == null)
					miSelectedProfile.Text = TranslateString(currentProfile.Name);
				else	
					miSelectedProfile.Text = currentProfile.Name;
			}	
		}
		
		TranslateProfile currentProfile;
		
		public void UpdateProfiles()
		{
			LockUpdate(true);
			try
			{
				ignoreProfileReposition = true;
				currentProfile = TranslateOptions.Instance.CurrentProfile;
				languageSelector.Profile = currentProfile;
				checkedProfileButton = null;
				
				foreach(FreeCL.UI.Actions.Action a in profileSwitchOnCtrl1Actions)
					a.Tag = null;
					
				while(tsTranslate.Items.Count > SpecialButtonsCount)
					tsTranslate.Items.RemoveAt(tsTranslate.Items.Count - 1);
				
				if(TranslateOptions.Instance.Profiles.Count > 1)
				{
					tsTranslate.Items.Add(tsSeparatorTranslate);	
					int actionIdx = 0;
					foreach(TranslateProfile pf in TranslateOptions.Instance.Profiles)
					{
						ToolStripButton tsButton = new ToolStripButton();
						if(pf == TranslateOptions.Instance.DefaultProfile)
						{
							tsButton.Text = TranslateString(pf.Name);
							tsButton.ToolTipText = tsButton.Text;
						}	
						else 
						{
							tsButton.Text = pf.Name;
							tsButton.ToolTipText = GetProfileName(pf as UserTranslateProfile);
						}	
						
						if(actionIdx < 9)
						{
							profileSwitchOnCtrl1Actions[actionIdx].Tag = tsButton;
							string displayString =	System.ComponentModel.TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(profileSwitchOnCtrl1Actions[actionIdx].Shortcut);
							tsButton.ToolTipText += " (" + displayString + ")";
						}
						actionIdx++;
						
						tsButton.Tag = pf;
						tsButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
						tsButton.TextAlign = ContentAlignment.MiddleCenter;
						tsButton.Click +=  OnProfileButtonClick;
						tsButton.LocationChanged += TsbTranslateLocationChanged;
						tsButton.MouseDown += OnProfileMouseDown;
						
						if(pf == currentProfile)
						{
							checkedProfileButton = tsButton;
							tsButton.Checked = true;
						}
							
						tsTranslate.Items.Add(tsButton);	
					}
				}
				UpdateLanguageSelector();
				tsTranslate.AllowItemReorder = tsTranslate.Items.Count > SpecialButtonsCount;
				ignoreProfileReposition = false;
				profilePositionChanged = false;
			} 
			finally
			{
				LockUpdate(false);
			}
			
			tbFrom.Focus();
		}
		
		void ActivateProfile(TranslateProfile profile)
		{
			if(currentProfile == profile)
				return;
				
			foreach(ToolStripItem tsi in tsTranslate.Items)
			{
				if(tsi.Tag == profile)
				{
					OnProfileButtonClick(tsi, new EventArgs());
					return;
				}
			}
		}
		
		ToolStripButton checkedProfileButton = null; 
		void OnProfileButtonClick(object sender, EventArgs e)
		{
			if(checkedProfileButton == sender)
				return;
		
			LockUpdate(true);
			try
			{
				if(checkedProfileButton != null)
					checkedProfileButton.Checked = false;
					
				ToolStripButton tsButton = sender as ToolStripButton;
				tsButton.Checked = true;
				checkedProfileButton = tsButton;
				
				TranslateProfile pf = tsButton.Tag as TranslateProfile;
				
				
				TranslateOptions.Instance.CurrentProfile = pf;
				currentProfile = TranslateOptions.Instance.CurrentProfile;
			
				languageSelector.Profile = currentProfile;
				UpdateLanguageSelector();
			} 
			finally
			{
				LockUpdate(false);
			}
			
			tbFrom.Focus();
		}
		
		
		void OnProfileMouseDown(object sender, EventArgs e)
		{
			if(MouseButtons == MouseButtons.Right)
			{
				OnProfileButtonClick(sender, e);
			}
		}
		
		void UpdateLanguageSelector()
		{
			LockUpdate(true);
			try
			{
				UserTranslateProfile upf = currentProfile as UserTranslateProfile;
				
				if(upf == null)
				{
					if(!pRight.Visible)
					{
						splitterRight.Enabled = true;
						pRight.Visible = true;
						pRight.Enabled = true;
					}
				}
				else
				{
					if(!upf.ShowLanguages && !upf.ShowServices && !upf.ShowSubjects)
					{
						splitterRight.Enabled = false;
						pRight.Visible = false;
						pRight.Enabled = false;
					}
					else if(!pRight.Visible)
					{
						splitterRight.Enabled = true;
						pRight.Visible = true;
						pRight.Enabled = true;
					}
				}
				UpdateCaption();
			} 
			finally
			{
				LockUpdate(false);
			}
			
		}
		
		string GetProfileName(UserTranslateProfile profile)
		{
			string nameBase = "";
			nameBase += profile.Name;
			nameBase += " ( ";
			
			nameBase += LangPack.TranslateLanguage(profile.TranslationDirection.From);
				
			nameBase += "->";
			
			nameBase += LangPack.TranslateLanguage(profile.TranslationDirection.To);
					
			if(profile.Subject != SubjectConstants.Any && profile.Subject != SubjectConstants.Common)
				nameBase += "->" + LangPack.TranslateString(profile.Subject);

			nameBase += " )";
			return nameBase;
		}
		
		
		void TranslateMainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			
			if(e.CloseReason == CloseReason.UserClosing && !RealClosing)
			{
				e.Cancel = true;
				
				if(WindowState == FormWindowState.Normal && Size.Height != 0)
				{
					TranslateOptions.Instance.MainFormSize = Size;
					if(Location.X >= 0 && Location.Y >= 0)
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
					if(Location.X >= 0 && Location.Y >= 0)
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
			StatManager.IncrementServiceCalls();
		}
		
		internal void TranslateCompletedEventHandler(object sender, TranslateCompletedEventArgs e)
		{
			if(TranslateOptions.Instance.ResultWindowOptions.ShowQueryStatistics)
				resBrowser.SetStatistics(DateTime.Now.Ticks - startTranslateTicks);
			
			resBrowser.SetEndData(e.TranslateState);
			
			ResourceManager resources = new ResourceManager("Translate.Common.Icons", Assembly.GetExecutingAssembly());
			miAnimatedIcon.Image = (((System.Drawing.Icon)(resources.GetObject("StaticIcon")))).ToBitmap();
			
			ignoreProfileReposition = true;
			tsbTranslate.Image = (((System.Drawing.Icon)(resources.GetObject("StaticIcon")))).ToBitmap();
			ignoreProfileReposition = false;
			
			//don't generate any statistics calls to avoid overloading of googlepages
			//if(!e.Cancelled && e.Error == null && e.TranslateState.Results.Count > 0)
			//	resBrowser.AddAdvertisement(e.TranslateState);
			
			if(activeTranslateState == e.TranslateState)
			{
				activeTranslateState = null;
			}
			pbMain.Visible = false;
			StatManager.IncrementCalls();
		}
		
		long startTranslateTicks;
		
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		void ATranslateExecute(object sender, EventArgs e)
		{
			StopCurrentTranslation(); 
			
			//force refresh of services 
			TimerRecheckServicesTick(null, null);
			
			ReadOnlyServiceSettingCollection settings = languageSelector.GetServiceSettings();//currentProfile.GetServiceSettings(tbFrom.Text, languageSelector.Selection);
			
			if(settings.Count > 50)
			{
				if(MessageBox.Show(FindForm(), 
					string.Format(
						TranslateString("The translation will produce {0} calls to different services, that can overload servers.\nDo you want to interrupt the process of translation ?"), 
							settings.Count) , 
						Constants.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					return;
				}
			}
			
			if(TranslateOptions.Instance.ResultWindowOptions.ShowQueryStatistics)
				startTranslateTicks = DateTime.Now.Ticks;
		
			tbFrom.Text = tbFrom.Text.Trim();
			ResourceManager resources = new ResourceManager("Translate.Common.Icons", Assembly.GetExecutingAssembly());
			miAnimatedIcon.Image = ((System.Drawing.Image)(resources.GetObject("AnimatedIcon")));
			
			ignoreProfileReposition = true;
			tsbTranslate.Image = ((System.Drawing.Image)(resources.GetObject("AnimatedIcon")));
			ignoreProfileReposition = false;
			
			if(timerRecheckServices.Enabled)
			{ //pending update of services list - force recheck
				timerRecheckServices.Stop();
				TimerRecheckServicesTick(sender, e);
			}
			
			
			
			resBrowser.Stop();
			resBrowser.Clear();
			resBrowser.Wait();
			
			if(settings.Count > 0)
			{
				activeTranslateState = TranslateManager.TranslateAsync(languageSelector.Selection, tbFrom.Text, settings, TranslateProgressChanged, TranslateCompletedEventHandler);
				pbMain.Value = 7;
				pbMain.Visible = true;
				languageSelector.AddSelectionToHistory();
			}
			else
			{
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
		
		void AStopTranslateExecute(object sender, EventArgs e)
		{
			StopCurrentTranslation(); 
			resBrowser.Stop();
		}
		
		
		void ATranslateUpdate(object sender, EventArgs e)
		{
			aTranslate.Enabled = tbFrom.Text.Length > 0;	
			aStopTranslate.Enabled = activeTranslateState != null;
		}
		
		void AInvertTranslationDirectionExecute(object sender, EventArgs e)
		{
			languageSelector.Invert();
		}
		
		void AInvertTranslationDirectionUpdate(object sender, EventArgs e)
		{
			UserTranslateProfile pf = currentProfile as UserTranslateProfile;
			aInvertTranslationDirection.Enabled = pf == null || pf.ShowLanguages;
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
			tsTranslate.Focus();
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
			CheckOrderOfProfiles();
			
			UpdateTbFromStat();
			
			if(!InputLanguageManager.IsInputLanguageChanged)
				return;
				
			if(languageSelector.Selection == null)	
				return;
				
			if(!InputLanguageManager.IsLanguageSupported(languageSelector.Selection.From))
			{

				bool default_selected = currentProfile == TranslateOptions.Instance.DefaultProfile;
				
				//step 1. seek in current if not default
				UserTranslateProfile upf = currentProfile as UserTranslateProfile;
				if(upf != null)
				{
					if(upf.TranslationDirection.From == Language.Any || InputLanguageManager.IsLanguageSupported(upf.TranslationDirection.From))
					{
						tbFrom.Focus();
						return;
					}
					
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
				
				//step 2. Generate list of profiles. default - last
				TranslateProfilesCollection profiles = new TranslateProfilesCollection();
				foreach(TranslateProfile pf in TranslateOptions.Instance.Profiles)
				{
					if(pf == TranslateOptions.Instance.DefaultProfile)
							continue;

					if(pf == currentProfile)
							continue;
							
					profiles.Add(pf);		
				}	
				profiles.Add(TranslateOptions.Instance.DefaultProfile);

				
				//step 2. seek in other not default profiles
				foreach(TranslateProfile pf in profiles)
				{
					foreach(LanguagePair lp in pf.History)
					{
						if(InputLanguageManager.IsLanguageSupported(lp.From))
						{
							try
							{
								skipChangeInput = true;
								ActivateProfile(pf);
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
				
					upf = pf as UserTranslateProfile;
					if(upf != null)
					{
						if(InputLanguageManager.IsLanguageSupported(upf.TranslationDirection.From))
						{
							skipChangeInput = true;
							ActivateProfile(upf);
							tbFrom.Focus();
							return;
						}
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
			if(TranslateOptions.Instance.MinimizeToTrayOnStartup && !CommandLineHelper.IsCommandSwitchSet("nohide"))
			{
				WindowState = FormWindowState.Minimized;
			}

			if(!TranslateOptions.Instance.DefaultProfile.DisabledLanguagesAlreadySet)
			{
				ActivateProfile(TranslateOptions.Instance.DefaultProfile);
				AFilterLanguagesExecute(null, null);
			}
			
			if(UpdatesManager.IsNewInstall)
			{
				UpdatesManager.IsNewInstall = false;
				UpdatesManager.CheckUpdates();
			}	
			
		}
		
		void AShowHtmlSourceExecute(object sender, EventArgs e)
		{
			resBrowser.ShowSource();			
		}
		
		void SbMainResize(object sender, EventArgs e)
		{
			lStatus.Width = sbMain.Width - 30 - 
				lInputLang.Width - 
				lTextBoxStat.Width - 
				lSelectedLangsPair.Width - 
				(pbMain.Visible ? pbMain.Width : 0);
			//sbMain.Refresh();	
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

		void TimerRecheckServicesTick(object sender, EventArgs e)
		{
			timerRecheckServices.Stop();
			LockUpdate(true);
			try
			{
				if(languageSelector.Phrase != tbFrom.Text.Trim())
					languageSelector.Phrase = tbFrom.Text.Trim();
			} 
			finally
			{
				LockUpdate(false);
			}
		}
		
		void TbFromTextChanged(object sender, EventArgs e)
		{
			timerRecheckServices.Stop();
			timerRecheckServices.Start();
			UpdateTbFromStat();
		}
		
		void UpdateTbFromStat()
		{
			string stat = tbFrom.SelectionStart.ToString();
			stat += " : ";
			stat += tbFrom.Text.Length.ToString();
			if(stat != lTextBoxStat.Text)
				lTextBoxStat.Text = stat;
		}
		
		
		
		void AWebsiteExecute(object sender, EventArgs e)
		{
			ProcessStartHelper.Start(Constants.HomeUrl);
		}
		
		void ADonateExecute(object sender, EventArgs e)
		{
			ProcessStartHelper.Start(Constants.DonateUrl);
		}
		
		void AOnlineHelpExecute(object sender, EventArgs e)
		{
			ProcessStartHelper.Start(Constants.HelpUrl);
		}

		void AFeedbackExecute(object sender, EventArgs e)
		{
			MailTo.Send(ApplicationInfo.SupportEmail, 
				TranslateString("Feedback for :") + " " + ApplicationInfo.ProductName + " " + ApplicationInfo.ProductVersion,
				TranslateString("<< Enter your feedback or bug report here (English, Ukrainian, Russian). >>"));
			
		}
		
		void AAddProfileExecute(object sender, EventArgs e)
		{
			UserTranslateProfile pf = new UserTranslateProfile();
			
			SetProfileNameForm nameForm = new SetProfileNameForm(pf, TranslateOptions.Instance.Profiles); 
			DialogResult dr = nameForm.ShowDialog(FindForm());
			nameForm.Dispose();
			if(dr == DialogResult.Cancel)
				return;
	
			CustomProfileServicesForm form = new CustomProfileServicesForm(pf);
			form.ShowDialog(this);
			form.Dispose();
			
			TranslateOptions.Instance.Profiles.Add(pf);
			TranslateOptions.Instance.CurrentProfile = pf;			
			
			pf.Subjects.AddRange(pf.GetSupportedSubjects());
			
			UpdateProfiles();
		}
		
		void ARemoveProfileExecute(object sender, EventArgs e)
		{
			if(currentProfile == TranslateOptions.Instance.DefaultProfile)
				return;
			if(MessageBox.Show(FindForm(), string.Format(TranslateString("The profile {0} will be deleted.\r\nAre you sure ?"), currentProfile.Name) , Constants.AppName, MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				TranslateOptions.Instance.Profiles.Remove(currentProfile);
				TranslateOptions.Instance.CurrentProfile = TranslateOptions.Instance.DefaultProfile;
				UpdateProfiles();
			}
		}
		
		void ARemoveProfileUpdate(object sender, EventArgs e)
		{
			aRemoveProfile.Enabled = currentProfile != TranslateOptions.Instance.DefaultProfile;
			aSetProfileProperties.Enabled = aRemoveProfile.Enabled;
			aEditProfileServices.Enabled = aRemoveProfile.Enabled;
			aShowProfileLanguages.Enabled = aRemoveProfile.Enabled;
			aShowProfileServices.Enabled = aRemoveProfile.Enabled;
			aShowProfileSubjects.Enabled = aRemoveProfile.Enabled;
			
			aRemoveProfile.Visible = aRemoveProfile.Enabled;
			aSetProfileProperties.Visible = aRemoveProfile.Enabled;
			aEditProfileServices.Visible = aRemoveProfile.Enabled;
			aShowProfileLanguages.Visible = aRemoveProfile.Enabled;
			aShowProfileServices.Visible = aRemoveProfile.Enabled;
			aShowProfileSubjects.Visible = aRemoveProfile.Enabled;
			
			tsSepProfiles.Visible = aRemoveProfile.Enabled;
			tsSepProfiles3.Visible = aRemoveProfile.Enabled;
			miProfileView.Visible = aRemoveProfile.Enabled;
			miProfileView2.Visible = aRemoveProfile.Enabled;
			
			UserTranslateProfile upf = currentProfile as UserTranslateProfile;
			if(upf == null)
				return;
			aShowProfileLanguages.Checked = upf.ShowLanguages;
			aShowProfileServices.Checked = upf.ShowServices;
			aShowProfileSubjects.Checked = upf.ShowSubjects;
		}
		
		void ASetProfilePropertiesExecute(object sender, EventArgs e)
		{
			UserTranslateProfile pf = currentProfile as UserTranslateProfile;
			if(pf == null)
				return;
			
			string oldName = pf.Name;
			SetProfileNameForm nameForm = new SetProfileNameForm(pf, TranslateOptions.Instance.Profiles); 
			
			DialogResult dr = nameForm.ShowDialog(FindForm());
			nameForm.Dispose();
			if(dr == DialogResult.Cancel)
			{
				pf.Name = oldName;
				return;
			}	
			
			int actionIdx = 0;
			foreach(ToolStripItem tsi in tsTranslate.Items)
			{
				if(tsi.Tag == currentProfile)
				{
					tsi.Text = pf.Name;
					tsi.ToolTipText = GetProfileName(pf);
					if(actionIdx < 9)
					{
						string displayString =	System.ComponentModel.TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(profileSwitchOnCtrl1Actions[actionIdx].Shortcut);
						tsi.ToolTipText += " (" + displayString + ")";
					}
					break;
				}
				actionIdx++;
			}
			UpdateCaption();
		}
		
		void AEditProfileServicesExecute(object sender, EventArgs e)
		{
			UserTranslateProfile pf = currentProfile as UserTranslateProfile;
			if(pf == null)
				return;
		
			CustomProfileServicesForm form = new CustomProfileServicesForm(pf);
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				pf.Subjects.Clear();
				pf.Subjects.AddRange(pf.GetSupportedSubjects());
				pf.History.Clear();
				languageSelector.Profile = currentProfile;
			}
			form.Dispose();
		}
		
		void AShowProfileServicesExecute(object sender, EventArgs e)
		{
			UserTranslateProfile pf = currentProfile as UserTranslateProfile;
			if(pf == null)
				return;
			pf.ShowServices = !pf.ShowServices;
			languageSelector.Profile = currentProfile;
			UpdateLanguageSelector();
		}
		
		void AShowProfileLanguagesExecute(object sender, EventArgs e)
		{
			UserTranslateProfile pf = currentProfile as UserTranslateProfile;
			if(pf == null)
				return;
			pf.ShowLanguages = !pf.ShowLanguages;
			languageSelector.Profile = currentProfile;			
			UpdateLanguageSelector();
		}
		
		void AShowProfileSubjectsExecute(object sender, EventArgs e)
		{
			UserTranslateProfile pf = currentProfile as UserTranslateProfile;
			if(pf == null)
				return;
			pf.ShowSubjects = !pf.ShowSubjects;
			languageSelector.Profile = currentProfile;			
			UpdateLanguageSelector();
		}
		
		bool profilePositionChanged;
		bool firstCheckOfOrderOfProfiles = true;
		void CheckOrderOfProfiles()
		{
			if(firstCheckOfOrderOfProfiles)
			{  //initial run
				profilePositionChanged = false;
				firstCheckOfOrderOfProfiles = false;
			}
			
			if(profilePositionChanged && MouseButtons == MouseButtons.None 
				&& tsTranslate.Items.Count > SpecialButtonsCount)
			{
				ignoreProfileReposition = true;
				LockUpdate(true);
				try 
				{
					tsTranslate.Items.Remove(tsbTranslate);
					tsTranslate.Items.Remove(tsbSearchInGoogle);
					tsTranslate.Items.Remove(tsbStop);
					tsTranslate.Items.Remove(tsSeparatorTranslate);
					
					TranslateOptions.Instance.Profiles.Clear();
					
					for(int i = 0; i < tsTranslate.Items.Count; i++)
					{
						TranslateOptions.Instance.Profiles.Add(tsTranslate.Items[i].Tag as TranslateProfile);
					}
					tsTranslate.Items.Clear();
					tsTranslate.Items.Insert(0, tsbStop);
					tsTranslate.Items.Insert(0, tsbSearchInGoogle);
					tsTranslate.Items.Insert(0, tsbTranslate);
					
					
					UpdateProfiles();
				} 
				finally
				{
					LockUpdate(false);
				}
			}
		}
		
		bool ignoreProfileReposition = true;
		void TsbTranslateLocationChanged(object sender, EventArgs e)
		{
			
			if(ignoreProfileReposition)
				return;
			
			profilePositionChanged = true;
			
		}
		

		void ANextProfileUpdate(object sender, EventArgs e)
		{
			aNextProfile.Enabled = tsTranslate.Items.Count != SpecialButtonsCount;
			aPreviousProfile.Enabled = aNextProfile.Enabled;
		}
		
		void ANextProfileExecute(object sender, EventArgs e)
		{
			int idx = 0;
			for(int i = SpecialButtonsCount + 1; i < tsTranslate.Items.Count; i++)
			{
				if(tsTranslate.Items[i].Tag == currentProfile)
				{
					idx = i + 1;
					if(idx >= tsTranslate.Items.Count)
						idx = SpecialButtonsCount + 1;
						
					OnProfileButtonClick(tsTranslate.Items[idx], new EventArgs());
					return;
				}
			}
		}
		
		
		void APreviousProfileExecute(object sender, EventArgs e)
		{
			int idx = 0;
			for(int i = SpecialButtonsCount + 1; i < tsTranslate.Items.Count; i++)
			{
				if(tsTranslate.Items[i].Tag == currentProfile)
				{
					idx = i - 1;
					if(idx < SpecialButtonsCount + 1)
						idx = tsTranslate.Items.Count - 1;
						
					OnProfileButtonClick(tsTranslate.Items[idx], new EventArgs());
					return;
				}
			}
		}
		
		void ProfileSwitchOnCtrl1Execute(object sender, EventArgs e)
		{
			FreeCL.UI.Actions.Action action = sender as FreeCL.UI.Actions.Action;		
			if(action == null)
				return;
				
			ToolStripButton button = action.Tag as ToolStripButton;
			
			if(button == null)
				return;
				
			OnProfileButtonClick(button, new EventArgs());	
		}
		
		
		
		void AShowAccentsExecute(object sender, EventArgs e)
		{
			ResultWindowOptions current = TranslateOptions.Instance.ResultWindowOptions;
			current.ShowAccents = !current.ShowAccents;
		}
		
		void AShowErrorsExecute(object sender, EventArgs e)
		{
			ResultWindowOptions current = TranslateOptions.Instance.ResultWindowOptions;
			current.MarkErrorsWithRed = !current.MarkErrorsWithRed;
		}
		
		void AShowServiceNameExecute(object sender, EventArgs e)
		{
			ResultWindowOptions current = TranslateOptions.Instance.ResultWindowOptions;
			current.ShowServiceName = !current.ShowServiceName;
		}
		
		void AShowStatisticsExecute(object sender, EventArgs e)
		{
			ResultWindowOptions current = TranslateOptions.Instance.ResultWindowOptions;
			current.ShowQueryStatistics = !current.ShowQueryStatistics;
			
		}
		
		void AShowTranslationDirectionExecute(object sender, EventArgs e)
		{
			ResultWindowOptions current = TranslateOptions.Instance.ResultWindowOptions;
			current.ShowTranslationDirection = !current.ShowTranslationDirection;
		}
		
		void AHideWithoutResultExecute(object sender, EventArgs e)
		{
			ResultWindowOptions current = TranslateOptions.Instance.ResultWindowOptions;
			current.HideWithoutResult = !current.HideWithoutResult;
		}
		
		void AHideWithoutResultUpdate(object sender, EventArgs e)
		{
			ResultWindowOptions current = TranslateOptions.Instance.ResultWindowOptions;
			aShowStatistics.Checked = current.ShowQueryStatistics;
			aShowErrors.Checked = current.MarkErrorsWithRed;
			aHideWithoutResult.Checked = current.HideWithoutResult;
			aShowTranslationDirection.Checked = current.ShowTranslationDirection;
			aShowServiceName.Checked = current.ShowServiceName;
			aShowAccents.Checked = current.ShowAccents;
		}
		
		void AIncludeMonolingualDictsExecute(object sender, EventArgs e)
		{
			DefaultTranslateProfile pf = currentProfile as DefaultTranslateProfile;
			if(pf == null)
				return;
			pf.IncludeMonolingualDictionaryInTranslation = !pf.IncludeMonolingualDictionaryInTranslation;
			languageSelector.Profile = currentProfile;			
			UpdateLanguageSelector();
			
		}
		
		void AIncludeMonolingualDictsUpdate(object sender, EventArgs e)
		{
			DefaultTranslateProfile pf = currentProfile as DefaultTranslateProfile;
		
			aIncludeMonolingualDicts.Enabled = pf != null;	
			aIncludeMonolingualDicts.Checked = !(pf == null || !pf.IncludeMonolingualDictionaryInTranslation); 
			aIncludeMonolingualDicts.Visible = aIncludeMonolingualDicts.Enabled;
			aFilterLanguages.Visible = aIncludeMonolingualDicts.Enabled;
			aFilterLanguages.Enabled = aIncludeMonolingualDicts.Enabled;
			
		}
		
		
		void AFilterLanguagesExecute(object sender, EventArgs e)
		{
			DefaultTranslateProfile pf = currentProfile as DefaultTranslateProfile;
			if(pf == null)
				return;
		
			pf.DisabledLanguagesAlreadySet = true;
			DefaultProfileLanguagesForm form = new DefaultProfileLanguagesForm(pf);
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				SubjectCollection subjects = pf.GetSupportedSubjects();
				SubjectCollection subjectsToDelete = new SubjectCollection();
				
				foreach(string subject in pf.Subjects)
				{
					if(!subjects.Contains(subject))
						subjectsToDelete.Add(subject);
				}
				
				foreach(string subject in subjectsToDelete)
					pf.Subjects.Remove(subject);
				
				LanguagePairCollection toDelete = new LanguagePairCollection();
				foreach(LanguagePair lp in pf.History)
				{
					if(pf.DisabledSourceLanguages.Contains(lp.From) ||
						pf.DisabledTargetLanguages.Contains(lp.To))
					{
						toDelete.Add(lp);
					}
				}
				
				foreach(LanguagePair lp in toDelete)
					pf.History.Remove(lp);
				languageSelector.Profile = currentProfile;
			}
			form.Dispose();
		}

		
		
		
		class MenuTranslateData
		{
		
			public MenuTranslateData(TranslateProfile translateProfile, string selection, LanguagePair languagePair)
			{
				this.translateProfile = translateProfile;
				this.selection = selection;
				this.languagePair = languagePair;
			}
			
			TranslateProfile translateProfile;
			string selection;
			LanguagePair languagePair;
			
			public TranslateProfile Profile {
				get { return translateProfile; }
				set { translateProfile = value; }
			}
			
			public string Selection {
				get { return selection; }
				set { selection = value; }
			}
			
			public LanguagePair LanguagePair {
				get { return languagePair; }
				set { languagePair = value; }
			}
			
		}
		
		void MsBrowserOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string selection = resBrowser.GetSelection();
			if(string.IsNullOrEmpty(selection))
			{
				miBrowserTranslateSel.Visible = false;
				miBrowserSep2.Visible = false;
				miSearchInGoogle.Visible = false;
			}
			else
			{
				selection = selection.Trim();
				string small_selection = selection;
				if(small_selection.Length > 25)
					small_selection = small_selection.Substring(0, 25) + " ...";
			
				miSearchInGoogle.Visible = true;
				miSearchInGoogle.Tag = selection;
				miBrowserTranslateSel.Visible = true;
				miBrowserSep2.Visible = true;
				miBrowserTranslateSel.Text = 
					TranslateString("Translate") + " «" + small_selection + "»";
					
				miBrowserTranslateSel.DropDownItems.Clear();
				
				UserTranslateProfile upf = currentProfile as UserTranslateProfile;
				if(currentProfile == TranslateOptions.Instance.DefaultProfile || 
					(upf != null && upf.ShowLanguages)
				)
				{ //add direct and reverted directions
					LanguagePair current_dir = languageSelector.Selection;
					ToolStripMenuItem mi;
					if(current_dir.To != current_dir.From)
					{
						LanguagePair inverted_dir = new LanguagePair(current_dir.To, current_dir.From);
						if(currentProfile.GetLanguagePairs().Contains(inverted_dir) )
						{
							mi = new ToolStripMenuItem();
							miBrowserTranslateSel.DropDownItems.Add(mi);
							mi.Text = LangPack.TranslateLanguage(inverted_dir.From) + 
								"->" + 
								LangPack.TranslateLanguage(inverted_dir.To);
							mi.Tag = new MenuTranslateData(currentProfile, selection, inverted_dir);
							mi.Click += OnMenuTranslate;
						}
					}
					
					mi = new ToolStripMenuItem();
					miBrowserTranslateSel.DropDownItems.Add(mi);
					mi.Text = LangPack.TranslateLanguage(current_dir.From) + 
						"->" + 
						LangPack.TranslateLanguage(current_dir.To);
					mi.Tag = new MenuTranslateData(currentProfile, selection, current_dir);
					mi.Click += OnMenuTranslate;

				}
				else if(upf != null && !upf.ShowLanguages)
				{
					ToolStripMenuItem mi;
					mi = new ToolStripMenuItem();
					miBrowserTranslateSel.DropDownItems.Add(mi);
					mi.Text = GetProfileName(upf);
					mi.Tag = new MenuTranslateData(upf, selection, null);
					mi.Click += OnMenuTranslate;
				}
				
				foreach(TranslateProfile pf in TranslateOptions.Instance.Profiles)
				{
					if(currentProfile == pf)
						continue;
						
					upf = pf as UserTranslateProfile;
					if(pf == TranslateOptions.Instance.DefaultProfile || 
						(upf != null && upf.ShowLanguages)
					)
					{ //add direct and reverted directions
						LanguagePair current_dir = pf.SelectedLanguagePair;
						ToolStripMenuItem mi;
						ToolStripMenuItem parent_mi = miBrowserTranslateSel;
						if(current_dir.To != current_dir.From)
						{
							//add submenu
							parent_mi = new ToolStripMenuItem();
							miBrowserTranslateSel.DropDownItems.Add(parent_mi);
							if(pf == TranslateOptions.Instance.DefaultProfile)
								parent_mi.Text = TranslateString(pf.Name);
							else 	
								parent_mi.Text = pf.Name;
							
							LanguagePair inverted_dir = new LanguagePair(current_dir.To, current_dir.From);
							if(pf.GetLanguagePairs().Contains(inverted_dir) )
							{
								mi = new ToolStripMenuItem();
								parent_mi.DropDownItems.Add(mi);
								mi.Text = LangPack.TranslateLanguage(inverted_dir.From) + 
									"->" + 
									LangPack.TranslateLanguage(inverted_dir.To);
								mi.Tag = new MenuTranslateData(pf, selection, inverted_dir);
								mi.Click += OnMenuTranslate;
							}
						}
						
						mi = new ToolStripMenuItem();
						parent_mi.DropDownItems.Add(mi);
						
						if(parent_mi == miBrowserTranslateSel)
						{
							if(pf == TranslateOptions.Instance.DefaultProfile)
								mi.Text = TranslateString(pf.Name);
							else 	
								mi.Text = pf.Name;
								
							mi.Text += LangPack.TranslateLanguage(current_dir.From) + 
								"->" + 
								LangPack.TranslateLanguage(current_dir.To);
						}
						else
							mi.Text = LangPack.TranslateLanguage(current_dir.From) + 
								"->" + 
								LangPack.TranslateLanguage(current_dir.To);
						
						mi.Tag = new MenuTranslateData(pf, selection, current_dir);
						mi.Click += OnMenuTranslate;
	
					}
					else if(upf != null && !upf.ShowLanguages)
					{
						ToolStripMenuItem mi;
						mi = new ToolStripMenuItem();
						miBrowserTranslateSel.DropDownItems.Add(mi);
						mi.Text = GetProfileName(upf);
						mi.Tag = new MenuTranslateData(upf, selection, null);
						mi.Click += OnMenuTranslate;
					}
						
				}
			}
		}
		
		void OnMenuTranslate(object sender, EventArgs e)					
		{
			ToolStripMenuItem mi = sender as ToolStripMenuItem;
			if(mi == null)
				return;
				
			object tag = mi.Tag;
			MenuTranslateData data = tag as MenuTranslateData;
			if(data != null)
			{  //default
				ActivateProfile(data.Profile);
				UserTranslateProfile upf = data.Profile as UserTranslateProfile;
				if(data.Profile == TranslateOptions.Instance.DefaultProfile || 
					(upf != null && upf.ShowLanguages && data.LanguagePair != null)
				)
				{
					languageSelector.Selection = data.LanguagePair;
				}
				UpdateCaption();
				tbFrom.Text = data.Selection;
				ATranslateExecute(sender, e);
			}
		}
		
		
		void ASearchInGoogleExecute(object sender, EventArgs e)
		{
			HtmlHelper.OpenUrl(new Uri("http://www.google.com/search?rls=translateclient&q="  +  
				tbFrom.Text.Trim()));
		}
		
		void ASearchInGoogleUpdate(object sender, EventArgs e)
		{
			aSearchInGoogle.Enabled = !string.IsNullOrEmpty(tbFrom.Text.Trim());
		}
		
		void MiSearchInGoogleClick(object sender, EventArgs e)
		{
			ToolStripMenuItem mi = sender as ToolStripMenuItem;
			if(mi == null)
				return;
				
			object sel = mi.Tag;
			if(sel == null)
				return;
				
			string selection = sel as string;
			if(string.IsNullOrEmpty(selection))
				return;
				
			HtmlHelper.OpenUrl(new Uri("http://www.google.com/search?rls=translateclient&q="  +  
				selection.Trim()));
		}

		public void PlaceResultViewVertical(bool top)		
		{
			DockStyle placeStyle = top ? DockStyle.Bottom : DockStyle.Top;
			if(tsTranslate.Dock == placeStyle)
				return;
			LockUpdate(true);
			try
			{
				tsTranslate.Dock = placeStyle;
				tbFrom.Dock = placeStyle;
				splitterTranslate.Dock = placeStyle;
				TranslateOptions.Instance.ResultWindowOptions.DockAtTop = top;
			} 
			finally
			{
				LockUpdate(false);
			}
		}
		
		void APlaceResultViewBottomExecute(object sender, EventArgs e)
		{
			PlaceResultViewVertical(false);
		}
		
		void APlaceResultViewTopExecute(object sender, EventArgs e)
		{
			PlaceResultViewVertical(true);
		}
		
		void APlaceResultViewTopUpdate(object sender, EventArgs e)
		{
			aPlaceResultViewTop.Checked = TranslateOptions.Instance.ResultWindowOptions.DockAtTop;
			aPlaceResultViewBottom.Checked = !aPlaceResultViewTop.Checked;			
			aPlaceResultViewLeft.Checked = TranslateOptions.Instance.ResultWindowOptions.DockAtLeft;
			aPlaceResultViewRight.Checked = !aPlaceResultViewLeft.Checked;
			aPlaceResultViewLeft.Enabled = pRight.Visible;
			aPlaceResultViewRight.Enabled = pRight.Visible;
		}

		public void PlaceResultViewHorizontal(bool left)		
		{
			DockStyle placeStyle = left ? DockStyle.Right : DockStyle.Left;
			if(pRight.Dock == placeStyle)
				return;
			LockUpdate(true);
			try
			{
				pRight.Dock = placeStyle;
				splitterRight.Dock = placeStyle;
				TranslateOptions.Instance.ResultWindowOptions.DockAtLeft = left;
			} 
			finally
			{
				LockUpdate(false);
			}
		}
		
		void APlaceResultViewLeftExecute(object sender, EventArgs e)
		{
			PlaceResultViewHorizontal(true);
		}
		
		void APlaceResultViewRightExecute(object sender, EventArgs e)
		{
			PlaceResultViewHorizontal(false);
		}
		
		public void ApplyToolbarsOptions()
		{
			FontsOptions fontOptions = TranslateOptions.Instance.FontsOptions;
			tsTranslate.Font = fontOptions.ToolbarsFont;
			ptEdit.Font = fontOptions.ToolbarsFont;
		}
	}
}
