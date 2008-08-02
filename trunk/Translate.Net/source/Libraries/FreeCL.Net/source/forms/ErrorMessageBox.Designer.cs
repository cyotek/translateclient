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

namespace FreeCL.Forms
{
	partial class ErrorMessageBox
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
		private void InitializeComponent()
		{
			this.lMessage = new System.Windows.Forms.Label();
			this.picApp = new System.Windows.Forms.PictureBox();
			this.bOptions = new FreeCL.UI.Panel();
			this.panel1 = new FreeCL.UI.Panel();
			this.bShowExceptionIfno = new System.Windows.Forms.Button();
			this.bOk = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picApp)).BeginInit();
			this.bOptions.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lMessage
			// 
			this.lMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lMessage.AutoEllipsis = true;
			this.lMessage.Location = new System.Drawing.Point(84, 8);
			this.lMessage.Name = "lMessage";
			this.lMessage.Size = new System.Drawing.Size(291, 96);
			this.lMessage.TabIndex = 11;
			this.lMessage.Text = "Message";
			this.lMessage.UseCompatibleTextRendering = true;
			// 
			// picApp
			// 
			this.picApp.Location = new System.Drawing.Point(12, 8);
			this.picApp.Name = "picApp";
			this.picApp.Size = new System.Drawing.Size(48, 33);
			this.picApp.TabIndex = 10;
			this.picApp.TabStop = false;
			// 
			// bOptions
			// 
			this.bOptions.AutoUpdateDockPadding = false;
			this.bOptions.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.bOptions.Controls.Add(this.panel1);
			this.bOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bOptions.Location = new System.Drawing.Point(0, 107);
			this.bOptions.Name = "bOptions";
			this.bOptions.Padding = new System.Windows.Forms.Padding(2);
			this.bOptions.Size = new System.Drawing.Size(387, 32);
			this.bOptions.TabIndex = 18;
			// 
			// panel1
			// 
			this.panel1.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.panel1.Controls.Add(this.bShowExceptionIfno);
			this.panel1.Controls.Add(this.bOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(12, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(373, 28);
			this.panel1.TabIndex = 3;
			// 
			// bShowExceptionIfno
			// 
			this.bShowExceptionIfno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bShowExceptionIfno.Location = new System.Drawing.Point(3, 2);
			this.bShowExceptionIfno.Name = "bShowExceptionIfno";
			this.bShowExceptionIfno.Size = new System.Drawing.Size(206, 23);
			this.bShowExceptionIfno.TabIndex = 1;
			this.bShowExceptionIfno.Text = "Show information about exception";
			this.bShowExceptionIfno.UseVisualStyleBackColor = true;
			this.bShowExceptionIfno.Click += new System.EventHandler(this.BShowExceptionIfnoClick);
			// 
			// bOk
			// 
			this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOk.Location = new System.Drawing.Point(275, 3);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(88, 23);
			this.bOk.TabIndex = 0;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			// 
			// ErrorMessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(387, 139);
			this.Controls.Add(this.bOptions);
			this.Controls.Add(this.lMessage);
			this.Controls.Add(this.picApp);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorMessageBox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ErrorMessageBox";
			((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
			this.bOptions.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button bShowExceptionIfno;
		private System.Windows.Forms.Button bOk;
		private FreeCL.UI.Panel panel1;
		private FreeCL.UI.Panel bOptions;
		private System.Windows.Forms.PictureBox picApp;
		private System.Windows.Forms.Label lMessage;
	}
}
