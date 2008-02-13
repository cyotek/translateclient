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
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
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
using System.Windows.Forms;
using FreeCL.RTL;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of BaseSplashForm.	
	/// </summary>
	public class BaseSplashForm : FreeCL.Forms.BaseForm
	{
		private FreeCL.UI.Panel pCopyright;
		private FreeCL.UI.Panel pAppCompany;
		private System.Windows.Forms.Label lVersion;
		private System.Windows.Forms.Label lAppTitle;
		private FreeCL.UI.Panel pAppTitle;
		private System.Windows.Forms.Label lCopyright;
		private System.Windows.Forms.Label lAppCompany;
		private FreeCL.UI.Panel pVersion;
		private System.Windows.Forms.PictureBox picApp;
		public BaseSplashForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			InitInfo();
			
			FreeCL.RTL.LangPack.RegisterLanguageEvent(OnLanguageChanged);
			OnLanguageChanged();
			
		}
		
		void InitInfo()
		{
			lAppTitle.Text = ApplicationInfo.ProductName;
			lAppCompany.Text = ApplicationInfo.CompanyName;
			lCopyright.Text = ApplicationInfo.Copyright;
			picApp.Image = FreeCL.UI.ShellFileInfo.LargeIcon(System.Windows.Forms.Application.ExecutablePath).ToBitmap();
		}
		
		void OnLanguageChanged()
		{
			lVersion.Text = LangPack.TranslateString("Version")+ " " + ApplicationInfo.ProductVersion;
		}
		
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent() {
			this.picApp = new System.Windows.Forms.PictureBox();
			this.pVersion = new FreeCL.UI.Panel();
			this.lVersion = new System.Windows.Forms.Label();
			this.lAppCompany = new System.Windows.Forms.Label();
			this.lCopyright = new System.Windows.Forms.Label();
			this.pAppTitle = new FreeCL.UI.Panel();
			this.lAppTitle = new System.Windows.Forms.Label();
			this.pAppCompany = new FreeCL.UI.Panel();
			this.pCopyright = new FreeCL.UI.Panel();
			((System.ComponentModel.ISupportInitialize)(this.picApp)).BeginInit();
			this.pVersion.SuspendLayout();
			this.pAppTitle.SuspendLayout();
			this.pAppCompany.SuspendLayout();
			this.pCopyright.SuspendLayout();
			this.SuspendLayout();
			// 
			// picApp
			// 
			this.picApp.Location = new System.Drawing.Point(16, 15);
			this.picApp.Name = "picApp";
			this.picApp.Size = new System.Drawing.Size(48, 37);
			this.picApp.TabIndex = 0;
			this.picApp.TabStop = false;
			// 
			// pVersion
			// 
			this.pVersion.AutoUpdateDockPadding = false;
			this.pVersion.BevelOuter = FreeCL.UI.BevelStyle.Lowered;
			this.pVersion.Controls.Add(this.lVersion);
			this.pVersion.Location = new System.Drawing.Point(88, 119);
			this.pVersion.Name = "pVersion";
			this.pVersion.Padding = new System.Windows.Forms.Padding(2);
			this.pVersion.Size = new System.Drawing.Size(256, 30);
			this.pVersion.TabIndex = 6;
			// 
			// lVersion
			// 
			this.lVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lVersion.Location = new System.Drawing.Point(2, 2);
			this.lVersion.Name = "lVersion";
			this.lVersion.Size = new System.Drawing.Size(252, 26);
			this.lVersion.TabIndex = 2;
			this.lVersion.Text = "Version";
			this.lVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lVersion.UseCompatibleTextRendering = true;
			this.lVersion.UseMnemonic = false;
			// 
			// lAppCompany
			// 
			this.lAppCompany.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lAppCompany.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(204)));
			this.lAppCompany.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppCompany.Location = new System.Drawing.Point(2, 2);
			this.lAppCompany.Name = "lAppCompany";
			this.lAppCompany.Size = new System.Drawing.Size(252, 25);
			this.lAppCompany.TabIndex = 2;
			this.lAppCompany.Text = "Company";
			this.lAppCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppCompany.UseCompatibleTextRendering = true;
			this.lAppCompany.UseMnemonic = false;
			// 
			// lCopyright
			// 
			this.lCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lCopyright.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lCopyright.Location = new System.Drawing.Point(2, 2);
			this.lCopyright.Name = "lCopyright";
			this.lCopyright.Size = new System.Drawing.Size(351, 26);
			this.lCopyright.TabIndex = 2;
			this.lCopyright.Text = "Copyright";
			this.lCopyright.UseCompatibleTextRendering = true;
			this.lCopyright.UseMnemonic = false;
			// 
			// pAppTitle
			// 
			this.pAppTitle.AutoUpdateDockPadding = false;
			this.pAppTitle.BevelOuter = FreeCL.UI.BevelStyle.Lowered;
			this.pAppTitle.Controls.Add(this.lAppTitle);
			this.pAppTitle.Location = new System.Drawing.Point(88, 45);
			this.pAppTitle.Name = "pAppTitle";
			this.pAppTitle.Padding = new System.Windows.Forms.Padding(2);
			this.pAppTitle.Size = new System.Drawing.Size(256, 29);
			this.pAppTitle.TabIndex = 3;
			// 
			// lAppTitle
			// 
			this.lAppTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lAppTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(204)));
			this.lAppTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppTitle.Location = new System.Drawing.Point(2, 2);
			this.lAppTitle.Name = "lAppTitle";
			this.lAppTitle.Size = new System.Drawing.Size(252, 25);
			this.lAppTitle.TabIndex = 2;
			this.lAppTitle.Text = "Title";
			this.lAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppTitle.UseCompatibleTextRendering = true;
			this.lAppTitle.UseMnemonic = false;
			// 
			// pAppCompany
			// 
			this.pAppCompany.AutoUpdateDockPadding = false;
			this.pAppCompany.BevelOuter = FreeCL.UI.BevelStyle.Lowered;
			this.pAppCompany.Controls.Add(this.lAppCompany);
			this.pAppCompany.Location = new System.Drawing.Point(88, 82);
			this.pAppCompany.Name = "pAppCompany";
			this.pAppCompany.Padding = new System.Windows.Forms.Padding(2);
			this.pAppCompany.Size = new System.Drawing.Size(256, 29);
			this.pAppCompany.TabIndex = 4;
			// 
			// pCopyright
			// 
			this.pCopyright.AutoUpdateDockPadding = false;
			this.pCopyright.BevelOuter = FreeCL.UI.BevelStyle.Lowered;
			this.pCopyright.Controls.Add(this.lCopyright);
			this.pCopyright.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pCopyright.Location = new System.Drawing.Point(0, 166);
			this.pCopyright.Name = "pCopyright";
			this.pCopyright.Padding = new System.Windows.Forms.Padding(2);
			this.pCopyright.Size = new System.Drawing.Size(355, 30);
			this.pCopyright.TabIndex = 5;
			// 
			// BaseSplashForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(355, 196);
			this.ControlBox = false;
			this.Controls.Add(this.pVersion);
			this.Controls.Add(this.pCopyright);
			this.Controls.Add(this.pAppCompany);
			this.Controls.Add(this.pAppTitle);
			this.Controls.Add(this.picApp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BaseSplashForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BaseSplashForm";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
			this.pVersion.ResumeLayout(false);
			this.pAppTitle.ResumeLayout(false);
			this.pAppCompany.ResumeLayout(false);
			this.pCopyright.ResumeLayout(false);
			this.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
			this.pVersion.ResumeLayout(false);
			this.pAppTitle.ResumeLayout(false);
			this.pAppCompany.ResumeLayout(false);
			this.pCopyright.ResumeLayout(false);
			this.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
			this.pVersion.ResumeLayout(false);
			this.pAppTitle.ResumeLayout(false);
			this.pAppCompany.ResumeLayout(false);
			this.pCopyright.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion
	}
}
