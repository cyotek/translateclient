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
using FreeCL.Forms;
using System.Net;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	/// <summary>
	/// Description of GeneralNetworkOptionsControl.
	/// </summary>
	public partial class GeneralNetworkOptionsControl : FreeCL.Forms.BaseOptionsControl
	{
		public GeneralNetworkOptionsControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
		}
		
		
		void OnLanguageChanged()
		{
			lHost.Text = TranslateString("HTTP-Proxy");
			lProxy.Text = TranslateString("Proxy");
			lPort.Text = TranslateString("Port");
			lTimeout.Text = TranslateString("Timeout");
			cbProxyAuthentication.Text = TranslateString("Enable Authentication");
			lUser.Text = TranslateString("User"); 
			lPassword.Text = TranslateString("Password"); 
			cbNTLMAuthentication.Text = TranslateString("Use NTLM Authentication");
			lDomain.Text = TranslateString("Domain"); 
			
			int savedIdx = cbProxy.SelectedIndex;
			cbProxy.Items.Clear();
			cbProxy.Items.Add(TranslateString("System"));
			cbProxy.Items.Add(TranslateString("Custom"));
			cbProxy.Items.Add(TranslateString("None"));
			if(savedIdx != -1)
				cbProxy.SelectedIndex = savedIdx;
		
		}
		
		NetworkOptions current;
		
		public override void Init()
		{
			current = TranslateOptions.Instance.NetworkOptions;
			cbProxy.SelectedIndex = (int)current.ProxyMode;
			tbTimeout.Text = current.Timeout.ToString(CultureInfo.InvariantCulture);
		}
		
		public override void Apply()
		{
			current.Timeout = int.Parse(tbTimeout.Text, CultureInfo.InvariantCulture);
			if(!current.SetProxy((ProxyMode)cbProxy.SelectedIndex, tbProxy.Text, 
				tbPort.Text.Length > 0  ? int.Parse(tbPort.Text, CultureInfo.InvariantCulture) : 0,
				cbProxyAuthentication.Checked,
				tbUser.Text,
				tbPassword.Text,
				cbNTLMAuthentication.Checked,
				tbDomain.Text
				)
			)
			{ //error on setting, reset
				cbProxy.SelectedIndex = (int)current.ProxyMode;
			}
			
		}
		
		public override bool IsChanged()
		{
			if(tbTimeout.Text != current.Timeout.ToString(CultureInfo.InvariantCulture))
			{
				return true;
			}
			int savedIdx = cbProxy.SelectedIndex;	
			if(savedIdx != 1)
			{
				return savedIdx != (int)current.ProxyMode;
			}
			else
			{
				bool changed = savedIdx != (int)current.ProxyMode;
				if(!changed)
					changed = tbProxy.Text != current.ProxyHost;
				if(!changed)
					changed = tbPort.Text != current.ProxyPort.ToString(CultureInfo.InvariantCulture);

				if(!changed)
					changed = cbProxyAuthentication.Checked != current.ProxyAuthentication;
					
				if(!changed)
					changed = tbUser.Text != current.ProxyUser;
					
				if(!changed)
					changed = tbPassword.Text != current.ProxyDecryptedPassword;

				if(!changed)
					changed = cbNTLMAuthentication.Checked != current.ProxyNTLMAuthentication;

				if(!changed)
					changed = tbDomain.Text != current.ProxyNTLMDomain;
					
				return changed;
			}
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void CbProxySelectedIndexChanged(object sender, EventArgs e)
		{
			int savedIdx = cbProxy.SelectedIndex;	
			tbProxy.Enabled = savedIdx == 1;
			tbPort.Enabled = savedIdx == 1;
			
			if(savedIdx == 0)
			{
				Uri uri = new Uri("http://google.com");
				Uri proxy = WebRequest.DefaultWebProxy.GetProxy(uri);
				tbProxy.Text = proxy.Host;
				tbPort.Text = proxy.Port.ToString(CultureInfo.InvariantCulture);
			}
			else if(savedIdx == 1)
			{
				tbProxy.Text = current.ProxyHost;
				tbPort.Text = current.ProxyPort.ToString(CultureInfo.InvariantCulture);
				tbUser.Text = current.ProxyUser;
				tbPassword.Text = current.ProxyDecryptedPassword;
				cbProxyAuthentication.Enabled = true;
				cbProxyAuthentication.Checked = current.ProxyAuthentication;
				cbNTLMAuthentication.Checked = current.ProxyNTLMAuthentication;
				tbDomain.Text = current.ProxyNTLMDomain;
			}
			else
			{
				tbProxy.Text = "";
				tbPort.Text = "";
			}
			
			if(savedIdx != 1)
			{
				cbProxyAuthentication.Enabled = false;
				cbProxyAuthentication.Checked = false;
				CbProxyAuthenticationCheckedChanged(this, new EventArgs());
				
				tbUser.Text = "";
				tbPassword.Text = "";
				cbNTLMAuthentication.Checked = false;
				CbNTLMAuthenticationCheckedChanged(this, new EventArgs());
				tbDomain.Text = "";
			}
		}
		
		void CbProxyAuthenticationCheckedChanged(object sender, EventArgs e)
		{
			bool check = cbProxyAuthentication.Checked;
			lUser.Enabled = check;
			tbUser.Enabled = check;
			lPassword.Enabled = check;
			tbPassword.Enabled = check;
			cbNTLMAuthentication.Enabled = check;
			if(!check)
			{
				lDomain.Enabled = check;
				tbDomain.Enabled = check;
			}
			else
				CbNTLMAuthenticationCheckedChanged(this, new EventArgs());
		}
		
		void CbNTLMAuthenticationCheckedChanged(object sender, EventArgs e)
		{
			bool check = cbNTLMAuthentication.Checked;
			lDomain.Enabled = check;
			tbDomain.Enabled = check;
		}
	}
}
