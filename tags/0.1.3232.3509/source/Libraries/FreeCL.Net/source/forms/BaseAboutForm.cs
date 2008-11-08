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
using System.Drawing;
using System.Windows.Forms;
using FreeCL.RTL;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of BaseAboutForm.
	/// </summary>
	public class BaseAboutForm : FreeCL.Forms.BaseForm
	{
		public BaseAboutForm()
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
			bOk.Text = TranslateString("OK");
			lVersion.Text = LangPack.TranslateString("Version")+ " " + ApplicationInfo.ProductVersion;
			Text = LangPack.TranslateString("About")+ " " + lAppTitle.Text;
		}
		
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent()
		{
			this.picApp = new System.Windows.Forms.PictureBox();
			this.lVersion = new System.Windows.Forms.Label();
			this.lAppCompany = new System.Windows.Forms.Label();
			this.lAppTitle = new System.Windows.Forms.Label();
			this.lCopyright = new System.Windows.Forms.Label();
			this.bOk = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picApp)).BeginInit();
			this.SuspendLayout();
			// 
			// picApp
			// 
			this.picApp.Location = new System.Drawing.Point(12, 12);
			this.picApp.Name = "picApp";
			this.picApp.Size = new System.Drawing.Size(48, 32);
			this.picApp.TabIndex = 1;
			this.picApp.TabStop = false;
			// 
			// lVersion
			// 
			this.lVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lVersion.Location = new System.Drawing.Point(87, 77);
			this.lVersion.Name = "lVersion";
			this.lVersion.Size = new System.Drawing.Size(252, 22);
			this.lVersion.TabIndex = 5;
			this.lVersion.Text = "Version";
			this.lVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lVersion.UseCompatibleTextRendering = true;
			this.lVersion.UseMnemonic = false;
			// 
			// lAppCompany
			// 
			this.lAppCompany.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lAppCompany.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(204)));
			this.lAppCompany.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppCompany.Location = new System.Drawing.Point(87, 56);
			this.lAppCompany.Name = "lAppCompany";
			this.lAppCompany.Size = new System.Drawing.Size(252, 21);
			this.lAppCompany.TabIndex = 4;
			this.lAppCompany.Text = "Company";
			this.lAppCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppCompany.UseCompatibleTextRendering = true;
			this.lAppCompany.UseMnemonic = false;
			// 
			// lAppTitle
			// 
			this.lAppTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lAppTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(204)));
			this.lAppTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppTitle.Location = new System.Drawing.Point(87, 35);
			this.lAppTitle.Name = "lAppTitle";
			this.lAppTitle.Size = new System.Drawing.Size(252, 21);
			this.lAppTitle.TabIndex = 3;
			this.lAppTitle.Text = "Title";
			this.lAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lAppTitle.UseCompatibleTextRendering = true;
			this.lAppTitle.UseMnemonic = false;
			// 
			// lCopyright
			// 
			this.lCopyright.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lCopyright.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lCopyright.Location = new System.Drawing.Point(12, 99);
			this.lCopyright.Name = "lCopyright";
			this.lCopyright.Size = new System.Drawing.Size(327, 22);
			this.lCopyright.TabIndex = 6;
			this.lCopyright.Text = "Copyright";
			this.lCopyright.UseCompatibleTextRendering = true;
			this.lCopyright.UseMnemonic = false;
			// 
			// bOk
			// 
			this.bOk.Location = new System.Drawing.Point(264, 136);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(75, 23);
			this.bOk.TabIndex = 7;
			this.bOk.Text = "Ok";
			this.bOk.UseCompatibleTextRendering = true;
			this.bOk.UseVisualStyleBackColor = true;
			this.bOk.Click += new System.EventHandler(this.BOkClick);
			// 
			// BaseAboutForm
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(350, 164);
			this.Controls.Add(this.bOk);
			this.Controls.Add(this.lCopyright);
			this.Controls.Add(this.lVersion);
			this.Controls.Add(this.lAppCompany);
			this.Controls.Add(this.lAppTitle);
			this.Controls.Add(this.picApp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BaseAboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "BaseAboutForm";
			((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
			this.ResumeLayout(false);
		}
		protected System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Label lCopyright;
		protected System.Windows.Forms.Label lAppTitle;
		private System.Windows.Forms.Label lAppCompany;
		private System.Windows.Forms.Label lVersion;
		private System.Windows.Forms.PictureBox picApp;
		#endregion
		
		void BOkClick(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
