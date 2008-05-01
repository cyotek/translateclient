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

namespace FreeCL.Forms
{
	partial class UILanguageOptionsControl
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
			this.lbLanguage = new System.Windows.Forms.ListBox();
			this.pInfo = new FreeCL.UI.Panel();
			this.tbData = new System.Windows.Forms.TextBox();
			this.lTranslatedBy = new System.Windows.Forms.Label();
			this.pBody.SuspendLayout();
			this.pInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.lbLanguage);
			this.pBody.Controls.Add(this.pInfo);
			// 
			// lbLanguage
			// 
			this.lbLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbLanguage.FormattingEnabled = true;
			this.lbLanguage.Location = new System.Drawing.Point(10, 10);
			this.lbLanguage.Name = "lbLanguage";
			this.lbLanguage.Size = new System.Drawing.Size(379, 134);
			this.lbLanguage.TabIndex = 2;
			this.lbLanguage.SelectedIndexChanged += new System.EventHandler(this.LbLanguageSelectedIndexChanged);
			// 
			// pInfo
			// 
			this.pInfo.BevelInner = FreeCL.UI.BevelStyle.Lowered;
			this.pInfo.Controls.Add(this.tbData);
			this.pInfo.Controls.Add(this.lTranslatedBy);
			this.pInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pInfo.Location = new System.Drawing.Point(10, 150);
			this.pInfo.Name = "pInfo";
			this.pInfo.Padding = new System.Windows.Forms.Padding(5);
			this.pInfo.Size = new System.Drawing.Size(379, 40);
			this.pInfo.TabIndex = 3;
			// 
			// tbData
			// 
			this.tbData.BackColor = System.Drawing.SystemColors.Control;
			this.tbData.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbData.Location = new System.Drawing.Point(105, 5);
			this.tbData.Multiline = true;
			this.tbData.Name = "tbData";
			this.tbData.ReadOnly = true;
			this.tbData.Size = new System.Drawing.Size(269, 30);
			this.tbData.TabIndex = 1;
			this.tbData.Text = "d\r\nd";
			this.tbData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lTranslatedBy
			// 
			this.lTranslatedBy.Dock = System.Windows.Forms.DockStyle.Left;
			this.lTranslatedBy.Location = new System.Drawing.Point(5, 5);
			this.lTranslatedBy.Name = "lTranslatedBy";
			this.lTranslatedBy.Size = new System.Drawing.Size(100, 30);
			this.lTranslatedBy.TabIndex = 0;
			this.lTranslatedBy.Text = "Translated by :";
			this.lTranslatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// UILanguageOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "UILanguageOptionsControl";
			this.pBody.ResumeLayout(false);
			this.pInfo.ResumeLayout(false);
			this.pInfo.PerformLayout();
			this.ResumeLayout(false);
		}
		private FreeCL.UI.Panel pInfo;
		private System.Windows.Forms.Label lTranslatedBy;
		private System.Windows.Forms.TextBox tbData;
		private System.Windows.Forms.ListBox lbLanguage;
	}
}
