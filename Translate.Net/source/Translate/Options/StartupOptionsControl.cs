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
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using FreeCL.Forms;
using FreeCL.RTL;
using Microsoft.Win32;

namespace Translate
{
	/// <summary>
	/// Description of StartupOptionsControl.
	/// </summary>
	public partial class StartupOptionsControl : FreeCL.Forms.BaseOptionsControl
	{
		public StartupOptionsControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
			portable = CommandLineHelper.IsCommandSwitchSet("portable");
		}
		
		void OnLanguageChanged()
		{
			cbAutorun.Text = TranslateString("Launch at system startup");
			toolTip.SetToolTip(cbAutorun, 
				string.Format(TranslateString(
					"When enabled {0} will run when OS started"
					), ApplicationInfo.ProductName));
					
			cbMinimizeToTray.Text = TranslateString("Minimize to tray");
			toolTip.SetToolTip(cbMinimizeToTray, 
				string.Format(TranslateString(
					"{0} will be minimized to tray when OS started"
					), ApplicationInfo.ProductName));
			
			cbSingleInstance.Text = TranslateString("Single Instance");
			toolTip.SetToolTip(cbSingleInstance, 
				string.Format(TranslateString(
					"Check at startup, if another running instance of {0} is already running, in this case, the new application activate old one and stop by self"
					), ApplicationInfo.ProductName));
			
		}

		string keyName = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
		
		bool initialAutorun;
		TranslateOptions options;
		bool portable;
		public override void Init()
		{
			if(!portable)	
			{
				string current = (string)Registry.GetValue(keyName, Constants.AppName, "Not set");
				initialAutorun = current != null && current != "Not set";
				cbAutorun.Checked = initialAutorun;
			}
			else
			{
				cbAutorun.Checked = false;
				cbAutorun.Enabled = false;
			}
			
			options = TranslateOptions.Instance;
			cbMinimizeToTray.Checked = options.MinimizeToTrayOnStartup;
			cbSingleInstance.Checked = options.SingleInstance;
		}
		
		public override void Apply()
		{
			if(!portable)	
			{
				if(cbAutorun.Checked != initialAutorun)
				{
					if(cbAutorun.Checked)
						Registry.SetValue(keyName, Constants.AppName, System.Windows.Forms.Application.ExecutablePath + " -skipsplash");
					else
					{
						RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
						rk.DeleteValue(Constants.AppName, false);
					 }
				}
			}	
			options.MinimizeToTrayOnStartup = cbMinimizeToTray.Checked;
			options.SingleInstance = cbSingleInstance.Checked;
		}
		
		public override bool IsChanged()
		{
			cbMinimizeToTray.Enabled = cbAutorun.Checked;
			return cbAutorun.Checked != initialAutorun || 
				options.MinimizeToTrayOnStartup != cbMinimizeToTray.Checked ||
				cbSingleInstance.Checked != options.SingleInstance;
		}
		
	}
}
