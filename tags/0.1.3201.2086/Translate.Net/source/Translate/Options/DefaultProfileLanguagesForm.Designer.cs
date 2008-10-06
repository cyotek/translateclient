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
	partial class DefaultProfileLanguagesForm
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
			this.pBottom = new System.Windows.Forms.Panel();
			this.bCancel = new System.Windows.Forms.Button();
			this.bOk = new System.Windows.Forms.Button();
			this.gbFrom = new System.Windows.Forms.GroupBox();
			this.lbFrom = new System.Windows.Forms.CheckedListBox();
			this.gbTop = new System.Windows.Forms.GroupBox();
			this.lInfo = new System.Windows.Forms.Label();
			this.gbTo = new System.Windows.Forms.GroupBox();
			this.lbTo = new System.Windows.Forms.CheckedListBox();
			this.pBottom.SuspendLayout();
			this.gbFrom.SuspendLayout();
			this.gbTop.SuspendLayout();
			this.gbTo.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBottom
			// 
			this.pBottom.Controls.Add(this.bCancel);
			this.pBottom.Controls.Add(this.bOk);
			this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBottom.Location = new System.Drawing.Point(0, 425);
			this.pBottom.Name = "pBottom";
			this.pBottom.Size = new System.Drawing.Size(475, 33);
			this.pBottom.TabIndex = 5;
			// 
			// bCancel
			// 
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(375, 7);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(88, 23);
			this.bCancel.TabIndex = 5;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			// 
			// bOk
			// 
			this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOk.Location = new System.Drawing.Point(281, 7);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(88, 23);
			this.bOk.TabIndex = 4;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			this.bOk.Click += new System.EventHandler(this.BOkClick);
			// 
			// gbFrom
			// 
			this.gbFrom.Controls.Add(this.lbFrom);
			this.gbFrom.Dock = System.Windows.Forms.DockStyle.Left;
			this.gbFrom.Location = new System.Drawing.Point(0, 49);
			this.gbFrom.Name = "gbFrom";
			this.gbFrom.Size = new System.Drawing.Size(230, 376);
			this.gbFrom.TabIndex = 6;
			this.gbFrom.TabStop = false;
			this.gbFrom.Text = "From ";
			// 
			// lbFrom
			// 
			this.lbFrom.CheckOnClick = true;
			this.lbFrom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbFrom.FormattingEnabled = true;
			this.lbFrom.Location = new System.Drawing.Point(3, 16);
			this.lbFrom.Name = "lbFrom";
			this.lbFrom.Size = new System.Drawing.Size(224, 349);
			this.lbFrom.Sorted = true;
			this.lbFrom.TabIndex = 0;
			this.lbFrom.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LbFromItemCheck);
			// 
			// gbTop
			// 
			this.gbTop.Controls.Add(this.lInfo);
			this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbTop.Location = new System.Drawing.Point(0, 0);
			this.gbTop.Name = "gbTop";
			this.gbTop.Size = new System.Drawing.Size(475, 49);
			this.gbTop.TabIndex = 7;
			this.gbTop.TabStop = false;
			// 
			// lInfo
			// 
			this.lInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lInfo.Location = new System.Drawing.Point(3, 16);
			this.lInfo.Name = "lInfo";
			this.lInfo.Size = new System.Drawing.Size(469, 30);
			this.lInfo.TabIndex = 0;
			this.lInfo.Text = "Choose languages you want to translate from and to. Disabled languages will be re" +
			"moved from \"Languages\" tab of default profile";
			// 
			// gbTo
			// 
			this.gbTo.Controls.Add(this.lbTo);
			this.gbTo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbTo.Location = new System.Drawing.Point(230, 49);
			this.gbTo.Name = "gbTo";
			this.gbTo.Size = new System.Drawing.Size(245, 376);
			this.gbTo.TabIndex = 8;
			this.gbTo.TabStop = false;
			this.gbTo.Text = "To";
			// 
			// lbTo
			// 
			this.lbTo.CheckOnClick = true;
			this.lbTo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbTo.FormattingEnabled = true;
			this.lbTo.Location = new System.Drawing.Point(3, 16);
			this.lbTo.Name = "lbTo";
			this.lbTo.Size = new System.Drawing.Size(239, 349);
			this.lbTo.Sorted = true;
			this.lbTo.TabIndex = 1;
			this.lbTo.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LbFromItemCheck);
			// 
			// DefaultProfileLanguagesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(475, 458);
			this.Controls.Add(this.gbTo);
			this.Controls.Add(this.gbFrom);
			this.Controls.Add(this.pBottom);
			this.Controls.Add(this.gbTop);
			this.Name = "DefaultProfileLanguagesForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Filter of languages";
			this.SizeChanged += new System.EventHandler(this.DefaultProfileLanguagesFormSizeChanged);
			this.pBottom.ResumeLayout(false);
			this.gbFrom.ResumeLayout(false);
			this.gbTop.ResumeLayout(false);
			this.gbTo.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckedListBox lbTo;
		private System.Windows.Forms.CheckedListBox lbFrom;
		private System.Windows.Forms.Label lInfo;
		private System.Windows.Forms.GroupBox gbTo;
		private System.Windows.Forms.GroupBox gbTop;
		private System.Windows.Forms.GroupBox gbFrom;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Panel pBottom;
	}
}
