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
	partial class AboutForm
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
			this.lStats = new System.Windows.Forms.Label();
			this.bDonate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.al)).BeginInit();
			this.SuspendLayout();
			// 
			// bOk
			// 
			this.bOk.Location = new System.Drawing.Point(264, 179);
			// 
			// lStats
			// 
			this.lStats.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lStats.Location = new System.Drawing.Point(12, 141);
			this.lStats.Name = "lStats";
			this.lStats.Size = new System.Drawing.Size(327, 35);
			this.lStats.TabIndex = 8;
			this.lStats.Text = "label1";
			this.lStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// bDonate
			// 
			this.bDonate.Location = new System.Drawing.Point(183, 179);
			this.bDonate.Name = "bDonate";
			this.bDonate.Size = new System.Drawing.Size(75, 23);
			this.bDonate.TabIndex = 9;
			this.bDonate.Text = "Donate ...";
			this.bDonate.UseVisualStyleBackColor = true;
			this.bDonate.Click += new System.EventHandler(this.BDonateClick);
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(344, 211);
			this.Controls.Add(this.bDonate);
			this.Controls.Add(this.lStats);
			this.Name = "AboutForm";
			this.Text = "AboutForm";
			this.VisibleChanged += new System.EventHandler(this.AboutFormVisibleChanged);
			this.Controls.SetChildIndex(this.lAppTitle, 0);
			this.Controls.SetChildIndex(this.bOk, 0);
			this.Controls.SetChildIndex(this.lStats, 0);
			this.Controls.SetChildIndex(this.bDonate, 0);
			((System.ComponentModel.ISupportInitialize)(this.al)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button bDonate;
		private System.Windows.Forms.Label lStats;
	}
}
