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

namespace Translate
{
	/// <summary>
	/// Description of UpdaterForm.
	/// </summary>
	public partial class UpdaterForm : BaseForm
	{
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		public UpdaterForm()
		{
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
			lStatus.Text = "";
			
			System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
			Location = new Point(workingRectangle.Width - DesktopBounds.Width, 30);
			
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void OnLanguageChanged()
		{
			bCancel.Text = TranslateString("Cancel");
			bMinimize.Text = TranslateString("Minimize");
			Text = string.Format(CultureInfo.InvariantCulture, TranslateString("{0} Updater"), Constants.AppName);
		}

		void UpdaterFormVisibleChanged(object sender, EventArgs e)
		{
			UpdatesManager.DoProcess();						
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.MessageBox.Show(System.Windows.Forms.IWin32Window,System.String,System.String,System.Windows.Forms.MessageBoxButtons)")]
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		void TimerUpdateTick(object sender, EventArgs e)
		{
			switch(UpdatesManager.State)
			{
				case UpdateState.None:
					lStatus.Text = TranslateString("Preparing");
					break;
				case UpdateState.CheckVersion:
					lStatus.Text = TranslateString("Check version");
					break;
				case UpdateState.UpdateDownloading:
					lStatus.Text = string.Format(CultureInfo.InvariantCulture, TranslateString("Downloaded {0}Kb from {1}Kb with speed {2}Kb/s"), UpdatesManager.KBReceived, UpdatesManager.TotalKBToReceive, UpdatesManager.DownloadSpeedKBPerSecond) +
						"\r\n" +
						UpdatesManager.FileName;
					pbMain.Style = ProgressBarStyle.Blocks;
					pbMain.Value = (int)UpdatesManager.ProgressPercentage;
					break;
				case UpdateState.UpdateDownloaded:
					lStatus.Text = TranslateString("Update downloaded");
					timerUpdate.Enabled = false;
					pbMain.Visible = false;
					if(WindowState == FormWindowState.Minimized)
						WindowState = FormWindowState.Normal;
					Activate();
					
					if(UpdatesManager.CanRunUpdate && MessageBox.Show(this, TranslateString("New version is downloaded. Do you want to stop " + Constants.AppName + " and run installer of new version ?"), Constants.AppName, MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						UpdatesManager.RunUpdate();
					}
					else 
					{
						SaveFileDialog sfd = new SaveFileDialog();
						sfd.Filter = "Exe files (*.exe)|*.exe";
						sfd.FilterIndex = 1;
						sfd.DefaultExt = "exe";
						sfd.FileName = UpdatesManager.FileName;
						sfd.Title = TranslateString("Please select where to save installer of new version which you can run later");
						if(sfd.ShowDialog(this) == DialogResult.OK)
						{
							UpdatesManager.SaveUpdate(sfd.FileName);
						}
						else
						{
							UpdatesManager.DeleteUpdate();
						}
						lStatus.Text = UpdatesManager.Message;
						timerClose.Enabled = true;
					}
					break;
				case UpdateState.Ending:
				case UpdateState.Error:
					lStatus.Text = UpdatesManager.Message;
					timerUpdate.Enabled = false;
					timerClose.Enabled = true;
					pbMain.Visible = false;
					break;
			}
		}
		
		
		
		void BMinimizeClick(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}
		
		void BCancelClick(object sender, EventArgs e)
		{
			timerUpdate.Enabled = false;
			timerClose.Enabled = false;
			UpdatesManager.Stop();
			Close();
		}

		void UpdaterFormFormClosing(object sender, FormClosingEventArgs e)
		{
			UpdatesManager.Stop();
		}
		
		int closeCount;
		void TimerCloseTick(object sender, EventArgs e)
		{
			closeCount++;
			if(closeCount == 10)
			{
				timerClose.Enabled = false;
				UpdatesManager.Stop();
				Close();
			}
			else
			{
				bCancel.Text = TranslateString("Close") + " [" + closeCount.ToString(CultureInfo.InvariantCulture) + "]";
			}
		}
	}
}
