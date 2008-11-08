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
 
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	partial class HotkeysOptionsControl
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
			this.cbControlCC = new System.Windows.Forms.CheckBox();
			this.cbControlInsIns = new System.Windows.Forms.CheckBox();
			this.cbTranslateOnHotkey = new System.Windows.Forms.CheckBox();
			this.pBody.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.cbTranslateOnHotkey);
			this.pBody.Controls.Add(this.cbControlInsIns);
			this.pBody.Controls.Add(this.cbControlCC);
			// 
			// cbControlCC
			// 
			this.cbControlCC.Location = new System.Drawing.Point(8, 13);
			this.cbControlCC.Name = "cbControlCC";
			this.cbControlCC.Size = new System.Drawing.Size(373, 24);
			this.cbControlCC.TabIndex = 2;
			this.cbControlCC.Text = "Activate on Ctrl+C+C hotkey";
			this.cbControlCC.UseVisualStyleBackColor = true;
			// 
			// cbControlInsIns
			// 
			this.cbControlInsIns.Location = new System.Drawing.Point(8, 43);
			this.cbControlInsIns.Name = "cbControlInsIns";
			this.cbControlInsIns.Size = new System.Drawing.Size(373, 24);
			this.cbControlInsIns.TabIndex = 3;
			this.cbControlInsIns.Text = "Activate on Ctrl+Ins+Ins hotkey";
			this.cbControlInsIns.UseVisualStyleBackColor = true;
			// 
			// cbTranslateOnHotkey
			// 
			this.cbTranslateOnHotkey.Location = new System.Drawing.Point(8, 84);
			this.cbTranslateOnHotkey.Name = "cbTranslateOnHotkey";
			this.cbTranslateOnHotkey.Size = new System.Drawing.Size(373, 24);
			this.cbTranslateOnHotkey.TabIndex = 4;
			this.cbTranslateOnHotkey.Text = "Translate when activated by hotkey";
			this.cbTranslateOnHotkey.UseVisualStyleBackColor = true;
			// 
			// HotkeysOptionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "HotkeysOptionsControl";
			this.pBody.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox cbTranslateOnHotkey;
		private System.Windows.Forms.CheckBox cbControlInsIns;
		private System.Windows.Forms.CheckBox cbControlCC;
	}
}
