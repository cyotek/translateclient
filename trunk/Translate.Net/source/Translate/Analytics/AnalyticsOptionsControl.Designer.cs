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
	partial class AnalyticsOptionsControl
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
		private void InitializeComponent()
		{
			this.panel2 = new System.Windows.Forms.Panel();
			this.rbNotAllow = new System.Windows.Forms.RadioButton();
			this.rbAllow = new System.Windows.Forms.RadioButton();
			this.llAlreadyCollected = new System.Windows.Forms.LinkLabel();
			this.gbWillNot = new System.Windows.Forms.GroupBox();
			this.lWillNot = new System.Windows.Forms.Label();
			this.gbWill = new System.Windows.Forms.GroupBox();
			this.lWill = new System.Windows.Forms.Label();
			this.lTop = new System.Windows.Forms.Label();
			this.pBody.SuspendLayout();
			this.panel2.SuspendLayout();
			this.gbWillNot.SuspendLayout();
			this.gbWill.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.panel2);
			this.pBody.Size = new System.Drawing.Size(399, 370);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.rbNotAllow);
			this.panel2.Controls.Add(this.rbAllow);
			this.panel2.Controls.Add(this.llAlreadyCollected);
			this.panel2.Controls.Add(this.gbWillNot);
			this.panel2.Controls.Add(this.gbWill);
			this.panel2.Controls.Add(this.lTop);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(10, 10);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.panel2.Size = new System.Drawing.Size(379, 291);
			this.panel2.TabIndex = 7;
			// 
			// rbNotAllow
			// 
			this.rbNotAllow.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbNotAllow.Location = new System.Drawing.Point(10, 253);
			this.rbNotAllow.Name = "rbNotAllow";
			this.rbNotAllow.Size = new System.Drawing.Size(359, 24);
			this.rbNotAllow.TabIndex = 11;
			this.rbNotAllow.Text = "No, I would not like information collecting";
			this.rbNotAllow.UseVisualStyleBackColor = true;
			// 
			// rbAllow
			// 
			this.rbAllow.Checked = true;
			this.rbAllow.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbAllow.Location = new System.Drawing.Point(10, 229);
			this.rbAllow.Name = "rbAllow";
			this.rbAllow.Size = new System.Drawing.Size(359, 24);
			this.rbAllow.TabIndex = 10;
			this.rbAllow.TabStop = true;
			this.rbAllow.Text = "Yes, I allow to collect anonymous information";
			this.rbAllow.UseVisualStyleBackColor = true;
			// 
			// llAlreadyCollected
			// 
			this.llAlreadyCollected.Dock = System.Windows.Forms.DockStyle.Top;
			this.llAlreadyCollected.Enabled = false;
			this.llAlreadyCollected.Location = new System.Drawing.Point(10, 190);
			this.llAlreadyCollected.Name = "llAlreadyCollected";
			this.llAlreadyCollected.Size = new System.Drawing.Size(359, 39);
			this.llAlreadyCollected.TabIndex = 9;
			this.llAlreadyCollected.TabStop = true;
			this.llAlreadyCollected.Text = "You can check already collected data here";
			this.llAlreadyCollected.Visible = false;
			this.llAlreadyCollected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlAlreadyCollectedLinkClicked);
			// 
			// gbWillNot
			// 
			this.gbWillNot.Controls.Add(this.lWillNot);
			this.gbWillNot.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbWillNot.Location = new System.Drawing.Point(10, 119);
			this.gbWillNot.Name = "gbWillNot";
			this.gbWillNot.Size = new System.Drawing.Size(359, 71);
			this.gbWillNot.TabIndex = 8;
			this.gbWillNot.TabStop = false;
			this.gbWillNot.Text = "Tool will not";
			// 
			// lWillNot
			// 
			this.lWillNot.Dock = System.Windows.Forms.DockStyle.Top;
			this.lWillNot.Location = new System.Drawing.Point(3, 16);
			this.lWillNot.Name = "lWillNot";
			this.lWillNot.Size = new System.Drawing.Size(353, 52);
			this.lWillNot.TabIndex = 0;
			this.lWillNot.Text = "Collect your name, address, or any other personally identifiable information. The" +
			" information collected is anonymous.";
			// 
			// gbWill
			// 
			this.gbWill.AutoSize = true;
			this.gbWill.Controls.Add(this.lWill);
			this.gbWill.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbWill.Location = new System.Drawing.Point(10, 48);
			this.gbWill.Name = "gbWill";
			this.gbWill.Size = new System.Drawing.Size(359, 71);
			this.gbWill.TabIndex = 7;
			this.gbWill.TabStop = false;
			this.gbWill.Text = "Tool will";
			// 
			// lWill
			// 
			this.lWill.Dock = System.Windows.Forms.DockStyle.Top;
			this.lWill.Location = new System.Drawing.Point(3, 16);
			this.lWill.Name = "lWill";
			this.lWill.Size = new System.Drawing.Size(353, 52);
			this.lWill.TabIndex = 0;
			this.lWill.Text = "Collect anonymous information about tool usage : tool working time, version, OS v" +
			"ersion";
			// 
			// lTop
			// 
			this.lTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.lTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lTop.Location = new System.Drawing.Point(10, 0);
			this.lTop.Name = "lTop";
			this.lTop.Size = new System.Drawing.Size(359, 48);
			this.lTop.TabIndex = 6;
			this.lTop.Text = "Do you want to allow collecting of basic information about application usage ? ";
			this.lTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// AnalyticsOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "AnalyticsOptionsControl";
			this.Size = new System.Drawing.Size(399, 410);
			this.pBody.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.gbWillNot.ResumeLayout(false);
			this.gbWill.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.RadioButton rbAllow;
		private System.Windows.Forms.RadioButton rbNotAllow;
		private System.Windows.Forms.Label lTop;
		private System.Windows.Forms.Label lWill;
		private System.Windows.Forms.GroupBox gbWill;
		private System.Windows.Forms.Label lWillNot;
		private System.Windows.Forms.GroupBox gbWillNot;
		private System.Windows.Forms.LinkLabel llAlreadyCollected;
		private System.Windows.Forms.Panel panel2;
	}
}
