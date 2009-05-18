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

namespace Translate
{
	partial class SetupAnalyticsForm
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
			this.bOptions = new FreeCL.UI.Panel();
			this.panel1 = new FreeCL.UI.Panel();
			this.bNo = new System.Windows.Forms.Button();
			this.bYes = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.llAlreadyCollected = new System.Windows.Forms.LinkLabel();
			this.lDisable = new System.Windows.Forms.Label();
			this.gbWillNot = new System.Windows.Forms.GroupBox();
			this.lWillNot = new System.Windows.Forms.Label();
			this.gbWill = new System.Windows.Forms.GroupBox();
			this.lWill = new System.Windows.Forms.Label();
			this.lTop = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.al)).BeginInit();
			this.bOptions.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.gbWillNot.SuspendLayout();
			this.gbWill.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOptions
			// 
			this.bOptions.AutoUpdateDockPadding = false;
			this.bOptions.Controls.Add(this.panel1);
			this.bOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bOptions.Location = new System.Drawing.Point(0, 283);
			this.bOptions.Name = "bOptions";
			this.bOptions.Padding = new System.Windows.Forms.Padding(2);
			this.bOptions.Size = new System.Drawing.Size(376, 32);
			this.bOptions.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.panel1.Controls.Add(this.bNo);
			this.panel1.Controls.Add(this.bYes);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(91, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(283, 28);
			this.panel1.TabIndex = 3;
			// 
			// bNo
			// 
			this.bNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.bNo.Location = new System.Drawing.Point(185, 3);
			this.bNo.Name = "bNo";
			this.bNo.Size = new System.Drawing.Size(88, 23);
			this.bNo.TabIndex = 1;
			this.bNo.Text = "No";
			this.bNo.UseVisualStyleBackColor = true;
			this.bNo.Click += new System.EventHandler(this.BNoClick);
			// 
			// bYes
			// 
			this.bYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.bYes.Location = new System.Drawing.Point(91, 3);
			this.bYes.Name = "bYes";
			this.bYes.Size = new System.Drawing.Size(88, 23);
			this.bYes.TabIndex = 0;
			this.bYes.Text = "Yes";
			this.bYes.UseVisualStyleBackColor = true;
			this.bYes.Click += new System.EventHandler(this.BOkClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.llAlreadyCollected);
			this.panel2.Controls.Add(this.lDisable);
			this.panel2.Controls.Add(this.gbWillNot);
			this.panel2.Controls.Add(this.gbWill);
			this.panel2.Controls.Add(this.lTop);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(10);
			this.panel2.Size = new System.Drawing.Size(376, 283);
			this.panel2.TabIndex = 6;
			// 
			// llAlreadyCollected
			// 
			this.llAlreadyCollected.Dock = System.Windows.Forms.DockStyle.Fill;
			this.llAlreadyCollected.Location = new System.Drawing.Point(10, 240);
			this.llAlreadyCollected.Name = "llAlreadyCollected";
			this.llAlreadyCollected.Size = new System.Drawing.Size(356, 33);
			this.llAlreadyCollected.TabIndex = 9;
			this.llAlreadyCollected.TabStop = true;
			this.llAlreadyCollected.Text = "You can check already collected data here";
			this.llAlreadyCollected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlAlreadyCollectedLinkClicked);
			// 
			// lDisable
			// 
			this.lDisable.Dock = System.Windows.Forms.DockStyle.Top;
			this.lDisable.Location = new System.Drawing.Point(10, 200);
			this.lDisable.Name = "lDisable";
			this.lDisable.Size = new System.Drawing.Size(356, 40);
			this.lDisable.TabIndex = 10;
			this.lDisable.Text = "You can disable data collecting at any time in \"Options\" dialog";
			// 
			// gbWillNot
			// 
			this.gbWillNot.Controls.Add(this.lWillNot);
			this.gbWillNot.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbWillNot.Location = new System.Drawing.Point(10, 129);
			this.gbWillNot.Name = "gbWillNot";
			this.gbWillNot.Size = new System.Drawing.Size(356, 71);
			this.gbWillNot.TabIndex = 8;
			this.gbWillNot.TabStop = false;
			this.gbWillNot.Text = "Tool will not";
			// 
			// lWillNot
			// 
			this.lWillNot.Dock = System.Windows.Forms.DockStyle.Top;
			this.lWillNot.Location = new System.Drawing.Point(3, 16);
			this.lWillNot.Name = "lWillNot";
			this.lWillNot.Size = new System.Drawing.Size(350, 52);
			this.lWillNot.TabIndex = 0;
			this.lWillNot.Text = "Collect your name, address, or any other personally identifiable information. The" +
			" information collected is anonymous.";
			// 
			// gbWill
			// 
			this.gbWill.AutoSize = true;
			this.gbWill.Controls.Add(this.lWill);
			this.gbWill.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbWill.Location = new System.Drawing.Point(10, 58);
			this.gbWill.Name = "gbWill";
			this.gbWill.Size = new System.Drawing.Size(356, 71);
			this.gbWill.TabIndex = 7;
			this.gbWill.TabStop = false;
			this.gbWill.Text = "Tool will";
			// 
			// lWill
			// 
			this.lWill.Dock = System.Windows.Forms.DockStyle.Top;
			this.lWill.Location = new System.Drawing.Point(3, 16);
			this.lWill.Name = "lWill";
			this.lWill.Size = new System.Drawing.Size(350, 52);
			this.lWill.TabIndex = 0;
			this.lWill.Text = "Collect anonymous information about tool usage : tool working time, version, OS v" +
			"ersion";
			// 
			// lTop
			// 
			this.lTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.lTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lTop.Location = new System.Drawing.Point(10, 10);
			this.lTop.Name = "lTop";
			this.lTop.Size = new System.Drawing.Size(356, 48);
			this.lTop.TabIndex = 6;
			this.lTop.Text = "Do you want to allow collecting of basic information about application usage ? ";
			this.lTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SetupAnalyticsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(376, 315);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.bOptions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetupAnalyticsForm";
			this.Text = "SetupAnalyticsForm";
			((System.ComponentModel.ISupportInitialize)(this.al)).EndInit();
			this.bOptions.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.gbWillNot.ResumeLayout(false);
			this.gbWill.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lDisable;
		private System.Windows.Forms.LinkLabel llAlreadyCollected;
		private System.Windows.Forms.Label lWillNot;
		private System.Windows.Forms.Label lWill;
		private System.Windows.Forms.GroupBox gbWillNot;
		private System.Windows.Forms.GroupBox gbWill;
		private System.Windows.Forms.Label lTop;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button bYes;
		private System.Windows.Forms.Button bNo;
		private FreeCL.UI.Panel panel1;
		private FreeCL.UI.Panel bOptions;
	}
}
