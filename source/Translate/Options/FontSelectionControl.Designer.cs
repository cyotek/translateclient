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
	partial class FontSelectionControl
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
			this.lName = new System.Windows.Forms.Label();
			this.cbFontName = new System.Windows.Forms.ComboBox();
			this.cbFontSize = new System.Windows.Forms.ComboBox();
			this.lSize = new System.Windows.Forms.Label();
			this.lTest = new System.Windows.Forms.Label();
			this.cbSystem = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lName
			// 
			this.lName.AutoSize = true;
			this.lName.Location = new System.Drawing.Point(6, 5);
			this.lName.Name = "lName";
			this.lName.Size = new System.Drawing.Size(41, 13);
			this.lName.TabIndex = 0;
			this.lName.Text = "Name :";
			this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbFontName
			// 
			this.cbFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFontName.FormattingEnabled = true;
			this.cbFontName.Location = new System.Drawing.Point(6, 22);
			this.cbFontName.Name = "cbFontName";
			this.cbFontName.Size = new System.Drawing.Size(270, 21);
			this.cbFontName.TabIndex = 1;
			this.cbFontName.SelectedIndexChanged += new System.EventHandler(this.CbFontNameSelectedIndexChanged);
			this.cbFontName.TextChanged += new System.EventHandler(this.CbFontNameTextChanged);
			// 
			// cbFontSize
			// 
			this.cbFontSize.FormattingEnabled = true;
			this.cbFontSize.Location = new System.Drawing.Point(287, 22);
			this.cbFontSize.Name = "cbFontSize";
			this.cbFontSize.Size = new System.Drawing.Size(58, 21);
			this.cbFontSize.TabIndex = 3;
			this.cbFontSize.TextChanged += new System.EventHandler(this.CbFontSizeTextChanged);
			// 
			// lSize
			// 
			this.lSize.AutoSize = true;
			this.lSize.Location = new System.Drawing.Point(287, 5);
			this.lSize.Name = "lSize";
			this.lSize.Size = new System.Drawing.Size(33, 13);
			this.lSize.TabIndex = 2;
			this.lSize.Text = "Size :";
			this.lSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lTest
			// 
			this.lTest.AutoSize = true;
			this.lTest.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.lTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lTest.Location = new System.Drawing.Point(6, 76);
			this.lTest.MaximumSize = new System.Drawing.Size(339, 500);
			this.lTest.MinimumSize = new System.Drawing.Size(339, 101);
			this.lTest.Name = "lTest";
			this.lTest.Size = new System.Drawing.Size(339, 101);
			this.lTest.TabIndex = 4;
			this.lTest.Text = "Latin: AaZz\r\nCyrillic: АаІіЯя\r\nGerman: üß\r\nJapanese: テスト\r\nChinese: 测试";
			this.lTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cbSystem
			// 
			this.cbSystem.Location = new System.Drawing.Point(6, 49);
			this.cbSystem.Name = "cbSystem";
			this.cbSystem.Size = new System.Drawing.Size(270, 24);
			this.cbSystem.TabIndex = 5;
			this.cbSystem.Text = "Default font";
			this.cbSystem.UseVisualStyleBackColor = true;
			this.cbSystem.CheckedChanged += new System.EventHandler(this.CbSystemCheckedChanged);
			// 
			// FontSelectionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.cbSystem);
			this.Controls.Add(this.lTest);
			this.Controls.Add(this.cbFontSize);
			this.Controls.Add(this.lSize);
			this.Controls.Add(this.cbFontName);
			this.Controls.Add(this.lName);
			this.Name = "FontSelectionControl";
			this.Size = new System.Drawing.Size(351, 184);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox cbSystem;
		private System.Windows.Forms.Label lName;
		private System.Windows.Forms.Label lSize;
		private System.Windows.Forms.ComboBox cbFontName;
		private System.Windows.Forms.ComboBox cbFontSize;
		private System.Windows.Forms.Label lTest;
	}
}
