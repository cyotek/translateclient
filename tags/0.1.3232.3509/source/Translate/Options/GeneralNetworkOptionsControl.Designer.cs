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


using System.Diagnostics.CodeAnalysis;
 
namespace Translate
{
	partial class GeneralNetworkOptionsControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent()
		{
			this.lProxy = new System.Windows.Forms.Label();
			this.cbProxy = new System.Windows.Forms.ComboBox();
			this.lHost = new System.Windows.Forms.Label();
			this.tbProxy = new System.Windows.Forms.TextBox();
			this.lPort = new System.Windows.Forms.Label();
			this.lTimeout = new System.Windows.Forms.Label();
			this.tbPort = new System.Windows.Forms.MaskedTextBox();
			this.tbTimeout = new System.Windows.Forms.MaskedTextBox();
			this.cbProxyAuthentication = new System.Windows.Forms.CheckBox();
			this.tbDomain = new System.Windows.Forms.TextBox();
			this.lDomain = new System.Windows.Forms.Label();
			this.cbNTLMAuthentication = new System.Windows.Forms.CheckBox();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.lPassword = new System.Windows.Forms.Label();
			this.tbUser = new System.Windows.Forms.TextBox();
			this.lUser = new System.Windows.Forms.Label();
			this.pBody.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.tbDomain);
			this.pBody.Controls.Add(this.lDomain);
			this.pBody.Controls.Add(this.cbNTLMAuthentication);
			this.pBody.Controls.Add(this.tbPassword);
			this.pBody.Controls.Add(this.lPassword);
			this.pBody.Controls.Add(this.tbUser);
			this.pBody.Controls.Add(this.lUser);
			this.pBody.Controls.Add(this.cbProxyAuthentication);
			this.pBody.Controls.Add(this.tbTimeout);
			this.pBody.Controls.Add(this.tbPort);
			this.pBody.Controls.Add(this.lTimeout);
			this.pBody.Controls.Add(this.lPort);
			this.pBody.Controls.Add(this.tbProxy);
			this.pBody.Controls.Add(this.lHost);
			this.pBody.Controls.Add(this.cbProxy);
			this.pBody.Controls.Add(this.lProxy);
			this.pBody.Size = new System.Drawing.Size(399, 234);
			// 
			// lProxy
			// 
			this.lProxy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lProxy.Location = new System.Drawing.Point(13, 5);
			this.lProxy.Name = "lProxy";
			this.lProxy.Size = new System.Drawing.Size(89, 23);
			this.lProxy.TabIndex = 0;
			this.lProxy.Text = "Proxy";
			this.lProxy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbProxy
			// 
			this.cbProxy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbProxy.FormattingEnabled = true;
			this.cbProxy.Location = new System.Drawing.Point(108, 7);
			this.cbProxy.Name = "cbProxy";
			this.cbProxy.Size = new System.Drawing.Size(283, 21);
			this.cbProxy.TabIndex = 1;
			this.cbProxy.SelectedIndexChanged += new System.EventHandler(this.CbProxySelectedIndexChanged);
			// 
			// lHost
			// 
			this.lHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lHost.Location = new System.Drawing.Point(13, 39);
			this.lHost.Name = "lHost";
			this.lHost.Size = new System.Drawing.Size(89, 23);
			this.lHost.TabIndex = 2;
			this.lHost.Text = "HTTP-Proxy";
			this.lHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbProxy
			// 
			this.tbProxy.Location = new System.Drawing.Point(108, 41);
			this.tbProxy.Name = "tbProxy";
			this.tbProxy.Size = new System.Drawing.Size(181, 20);
			this.tbProxy.TabIndex = 3;
			// 
			// lPort
			// 
			this.lPort.Location = new System.Drawing.Point(299, 44);
			this.lPort.Name = "lPort";
			this.lPort.Size = new System.Drawing.Size(35, 23);
			this.lPort.TabIndex = 4;
			this.lPort.Text = "Port";
			// 
			// lTimeout
			// 
			this.lTimeout.Location = new System.Drawing.Point(13, 204);
			this.lTimeout.Name = "lTimeout";
			this.lTimeout.Size = new System.Drawing.Size(89, 23);
			this.lTimeout.TabIndex = 6;
			this.lTimeout.Text = "Timeout";
			// 
			// tbPort
			// 
			this.tbPort.AsciiOnly = true;
			this.tbPort.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
			this.tbPort.Location = new System.Drawing.Point(340, 41);
			this.tbPort.Mask = "99999";
			this.tbPort.Name = "tbPort";
			this.tbPort.PromptChar = ' ';
			this.tbPort.Size = new System.Drawing.Size(51, 20);
			this.tbPort.TabIndex = 5;
			// 
			// tbTimeout
			// 
			this.tbTimeout.AsciiOnly = true;
			this.tbTimeout.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
			this.tbTimeout.Location = new System.Drawing.Point(108, 201);
			this.tbTimeout.Mask = "9999999";
			this.tbTimeout.Name = "tbTimeout";
			this.tbTimeout.PromptChar = ' ';
			this.tbTimeout.Size = new System.Drawing.Size(181, 20);
			this.tbTimeout.TabIndex = 7;
			// 
			// cbProxyAuthentication
			// 
			this.cbProxyAuthentication.Location = new System.Drawing.Point(16, 76);
			this.cbProxyAuthentication.Name = "cbProxyAuthentication";
			this.cbProxyAuthentication.Size = new System.Drawing.Size(295, 24);
			this.cbProxyAuthentication.TabIndex = 12;
			this.cbProxyAuthentication.Text = "Enable Authentication";
			this.cbProxyAuthentication.UseVisualStyleBackColor = true;
			this.cbProxyAuthentication.CheckedChanged += new System.EventHandler(this.CbProxyAuthenticationCheckedChanged);
			// 
			// tbDomain
			// 
			this.tbDomain.Location = new System.Drawing.Point(186, 164);
			this.tbDomain.Name = "tbDomain";
			this.tbDomain.Size = new System.Drawing.Size(103, 20);
			this.tbDomain.TabIndex = 22;
			// 
			// lDomain
			// 
			this.lDomain.Location = new System.Drawing.Point(104, 167);
			this.lDomain.Name = "lDomain";
			this.lDomain.Size = new System.Drawing.Size(65, 23);
			this.lDomain.TabIndex = 21;
			this.lDomain.Text = "Domain";
			// 
			// cbNTLMAuthentication
			// 
			this.cbNTLMAuthentication.Location = new System.Drawing.Point(108, 136);
			this.cbNTLMAuthentication.Name = "cbNTLMAuthentication";
			this.cbNTLMAuthentication.Size = new System.Drawing.Size(283, 24);
			this.cbNTLMAuthentication.TabIndex = 20;
			this.cbNTLMAuthentication.Text = "Use NTLM Authentication";
			this.cbNTLMAuthentication.UseVisualStyleBackColor = true;
			this.cbNTLMAuthentication.CheckedChanged += new System.EventHandler(this.CbNTLMAuthenticationCheckedChanged);
			// 
			// tbPassword
			// 
			this.tbPassword.Location = new System.Drawing.Point(299, 107);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Size = new System.Drawing.Size(92, 20);
			this.tbPassword.TabIndex = 19;
			// 
			// lPassword
			// 
			this.lPassword.Location = new System.Drawing.Point(220, 110);
			this.lPassword.Name = "lPassword";
			this.lPassword.Size = new System.Drawing.Size(69, 23);
			this.lPassword.TabIndex = 18;
			this.lPassword.Text = "Password";
			// 
			// tbUser
			// 
			this.tbUser.Location = new System.Drawing.Point(108, 107);
			this.tbUser.Name = "tbUser";
			this.tbUser.Size = new System.Drawing.Size(103, 20);
			this.tbUser.TabIndex = 17;
			// 
			// lUser
			// 
			this.lUser.Location = new System.Drawing.Point(13, 110);
			this.lUser.Name = "lUser";
			this.lUser.Size = new System.Drawing.Size(89, 23);
			this.lUser.TabIndex = 16;
			this.lUser.Text = "User";
			// 
			// GeneralNetworkOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "GeneralNetworkOptionsControl";
			this.Size = new System.Drawing.Size(399, 274);
			this.pBody.ResumeLayout(false);
			this.pBody.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lDomain;
		private System.Windows.Forms.TextBox tbDomain;
		private System.Windows.Forms.Label lUser;
		private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.Label lPassword;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.CheckBox cbProxyAuthentication;
		private System.Windows.Forms.CheckBox cbNTLMAuthentication;
		private System.Windows.Forms.MaskedTextBox tbTimeout;
		private System.Windows.Forms.Label lTimeout;
		private System.Windows.Forms.MaskedTextBox tbPort;
		private System.Windows.Forms.Label lProxy;
		private System.Windows.Forms.ComboBox cbProxy;
		private System.Windows.Forms.Label lHost;
		private System.Windows.Forms.TextBox tbProxy;
		private System.Windows.Forms.Label lPort;
	}
}
