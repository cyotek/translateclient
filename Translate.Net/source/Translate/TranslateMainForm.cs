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
			
			if(TranslateOptions.Instance.FontsOptions.TextControlFont != null)
				tbFrom.Font = TranslateOptions.Instance.FontsOptions.TextControlFont.GetFont();
			
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
			
			UpdateProfiles();
			
		}
		
		List<FreeCL.UI.Actions.Action> profileSwitchOnCtrl1Actions = new List<FreeCL.UI.Actions.Action>();
		
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

			for(int i = 2; i < tsTranslate.Items.Count; i++)
			{
				UserTranslateProfile pf = tsTranslate.Items[i].Tag as UserTranslateProfile;
				if(pf == null)
					tsTranslate.Items[i].Text = TranslateString(TranslateOptions.Instance.DefaultProfile.Name);
				else
					tsTranslate.Items[i].ToolTipText = GetProfileName(pf);
			}

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
				lSelectedLangsPair.Text = LangPack.TranslateLanguage(languageSelector.Selection.From).Substring(0, 3) +
					"->" + 
					LangPack.TranslateLanguage(languageSelector.Selection.To).Substring(0, 3);
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
			ignoreProfileReposition = true;
			currentProfile = TranslateOptions.Instance.CurrentProfile;
			languageSelector.Profile = currentProfile;
			checkedProfileButton = null;
			
			foreach(FreeCL.UI.Actions.Action a in profileSwitchOnCtrl1Actions)
				a.Tag = null;
				
			while(tsTranslate.Items.Count > 1)
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
			tsTranslate.AllowItemReorder = tsTranslate.Items.Count > 1;
			ignoreProfileReposition = false;
			profilePositionChanged = false;
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
			
			ignoreProfileReposition = true;
			tsbTranslate.Image = (((System.Drawing.Icon)(resources.GetObject("StaticIcon")))).ToBitmap();
			ignoreProfileReposition = false;
			
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
			
			ignoreProfileReposition = true;
			tsbTranslate.Image = ((System.Drawing.Image)(resources.GetObject("AnimatedIcon")));
			ignoreProfileReposition = false;
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
		void CheckOrderOfProfiles()
		{
			if(profilePositionChanged && MouseButtons == MouseButtons.None && tsTranslate.Items.Count > 1)
			{
				ignoreProfileReposition = true;
				LockUpdate(true);
				try 
				{
					tsTranslate.Items.Remove(tsbTranslate);
					tsTranslate.Items.Remove(tsSeparatorTranslate);
					
					TranslateOptions.Instance.Profiles.Clear();
					
					for(int i = 0; i < tsTranslate.Items.Count; i++)
					{
						TranslateOptions.Instance.Profiles.Add(tsTranslate.Items[i].Tag as TranslateProfile);
					}
					tsTranslate.Items.Clear();
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
			
			if(ignoreProfileReposition || tsTranslate.Items.Count == 1)
				return;
			
			profilePositionChanged = true;
			
		}
		

		void ANextProfileUpdate(object sender, EventArgs e)
		{
			aNextProfile.Enabled = tsTranslate.Items.Count != 1;
			aPreviousProfile.Enabled = aNextProfile.Enabled;
		}
		
		void ANextProfileExecute(object sender, EventArgs e)
		{
			int idx = 0;
			for(int i = 2; i < tsTranslate.Items.Count; i++)
			{
				if(tsTranslate.Items[i].Tag == currentProfile)
				{
					idx = i + 1;
					if(idx >= tsTranslate.Items.Count)
						idx = 2;
						
					OnProfileButtonClick(tsTranslate.Items[idx], new EventArgs());
					return;
				}
			}
		}
		
		
		void APreviousProfileExecute(object sender, EventArgs e)
		{
			int idx = 0;
			for(int i = 2; i < tsTranslate.Items.Count; i++)
			{
				if(tsTranslate.Items[i].Tag == currentProfile)
				{
					idx = i - 1;
					if(idx < 2)
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
			
		}
	}
}
