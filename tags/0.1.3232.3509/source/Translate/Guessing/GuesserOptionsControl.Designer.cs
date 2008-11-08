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
	partial class GuesserOptionsControl
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
			this.cbLanguageDetection = new System.Windows.Forms.CheckBox();
			this.gbIntillegentSwitching = new System.Windows.Forms.GroupBox();
			this.tbMinLength = new System.Windows.Forms.MaskedTextBox();
			this.lMinLength = new System.Windows.Forms.Label();
			this.cbSwitchDirectionBasedOnLanguage = new System.Windows.Forms.CheckBox();
			this.cbSwitchDirectionBasedOnLayout = new System.Windows.Forms.CheckBox();
			this.lCharacters = new System.Windows.Forms.Label();
			this.pBody.SuspendLayout();
			this.gbIntillegentSwitching.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.gbIntillegentSwitching);
			this.pBody.Controls.Add(this.cbLanguageDetection);
			// 
			// cbLanguageDetection
			// 
			this.cbLanguageDetection.Location = new System.Drawing.Point(8, 13);
			this.cbLanguageDetection.Name = "cbLanguageDetection";
			this.cbLanguageDetection.Size = new System.Drawing.Size(373, 24);
			this.cbLanguageDetection.TabIndex = 6;
			this.cbLanguageDetection.Text = "Language Detection";
			this.cbLanguageDetection.UseVisualStyleBackColor = true;
			// 
			// gbIntillegentSwitching
			// 
			this.gbIntillegentSwitching.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gbIntillegentSwitching.Controls.Add(this.lCharacters);
			this.gbIntillegentSwitching.Controls.Add(this.tbMinLength);
			this.gbIntillegentSwitching.Controls.Add(this.lMinLength);
			this.gbIntillegentSwitching.Controls.Add(this.cbSwitchDirectionBasedOnLanguage);
			this.gbIntillegentSwitching.Controls.Add(this.cbSwitchDirectionBasedOnLayout);
			this.gbIntillegentSwitching.Location = new System.Drawing.Point(8, 43);
			this.gbIntillegentSwitching.Name = "gbIntillegentSwitching";
			this.gbIntillegentSwitching.Size = new System.Drawing.Size(383, 120);
			this.gbIntillegentSwitching.TabIndex = 7;
			this.gbIntillegentSwitching.TabStop = false;
			this.gbIntillegentSwitching.Text = "Intelligent switching of profiles and directions";
			// 
			// tbMinLength
			// 
			this.tbMinLength.AsciiOnly = true;
			this.tbMinLength.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
			this.tbMinLength.Location = new System.Drawing.Point(57, 88);
			this.tbMinLength.Mask = "9999999";
			this.tbMinLength.Name = "tbMinLength";
			this.tbMinLength.PromptChar = ' ';
			this.tbMinLength.Size = new System.Drawing.Size(51, 20);
			this.tbMinLength.TabIndex = 9;
			// 
			// lMinLength
			// 
			this.lMinLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lMinLength.Location = new System.Drawing.Point(19, 62);
			this.lMinLength.Name = "lMinLength";
			this.lMinLength.Size = new System.Drawing.Size(354, 23);
			this.lMinLength.TabIndex = 8;
			this.lMinLength.Text = "Switch by detected language only when text length greater of ";
			this.lMinLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbSwitchDirectionBasedOnLanguage
			// 
			this.cbSwitchDirectionBasedOnLanguage.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSwitchDirectionBasedOnLanguage.Location = new System.Drawing.Point(3, 40);
			this.cbSwitchDirectionBasedOnLanguage.Name = "cbSwitchDirectionBasedOnLanguage";
			this.cbSwitchDirectionBasedOnLanguage.Size = new System.Drawing.Size(377, 24);
			this.cbSwitchDirectionBasedOnLanguage.TabIndex = 1;
			this.cbSwitchDirectionBasedOnLanguage.Text = "Based on detected language";
			this.cbSwitchDirectionBasedOnLanguage.UseVisualStyleBackColor = true;
			// 
			// cbSwitchDirectionBasedOnLayout
			// 
			this.cbSwitchDirectionBasedOnLayout.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSwitchDirectionBasedOnLayout.Location = new System.Drawing.Point(3, 16);
			this.cbSwitchDirectionBasedOnLayout.Name = "cbSwitchDirectionBasedOnLayout";
			this.cbSwitchDirectionBasedOnLayout.Size = new System.Drawing.Size(377, 24);
			this.cbSwitchDirectionBasedOnLayout.TabIndex = 0;
			this.cbSwitchDirectionBasedOnLayout.Text = "Based on keyboard layout";
			this.cbSwitchDirectionBasedOnLayout.UseVisualStyleBackColor = true;
			// 
			// lCharacters
			// 
			this.lCharacters.Location = new System.Drawing.Point(114, 85);
			this.lCharacters.Name = "lCharacters";
			this.lCharacters.Size = new System.Drawing.Size(100, 23);
			this.lCharacters.TabIndex = 10;
			this.lCharacters.Text = "characters";
			this.lCharacters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GuesserOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "GuesserOptionsControl";
			this.pBody.ResumeLayout(false);
			this.gbIntillegentSwitching.ResumeLayout(false);
			this.gbIntillegentSwitching.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lCharacters;
		private System.Windows.Forms.MaskedTextBox tbMinLength;
		private System.Windows.Forms.Label lMinLength;
		private System.Windows.Forms.CheckBox cbSwitchDirectionBasedOnLanguage;
		private System.Windows.Forms.CheckBox cbSwitchDirectionBasedOnLayout;
		private System.Windows.Forms.GroupBox gbIntillegentSwitching;
		private System.Windows.Forms.CheckBox cbLanguageDetection;
	}
}
