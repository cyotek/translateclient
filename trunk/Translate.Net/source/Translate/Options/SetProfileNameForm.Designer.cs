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
	partial class SetProfileNameForm
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
			this.bCancel = new System.Windows.Forms.Button();
			this.bOk = new System.Windows.Forms.Button();
			this.lName = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// bCancel
			// 
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(279, 37);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(88, 23);
			this.bCancel.TabIndex = 3;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			// 
			// bOk
			// 
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOk.Location = new System.Drawing.Point(185, 37);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(88, 23);
			this.bOk.TabIndex = 2;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			// 
			// lName
			// 
			this.lName.Location = new System.Drawing.Point(12, 9);
			this.lName.Name = "lName";
			this.lName.Size = new System.Drawing.Size(100, 23);
			this.lName.TabIndex = 4;
			this.lName.Text = "Profile name : ";
			this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(105, 11);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(262, 20);
			this.tbName.TabIndex = 5;
			// 
			// SetProfileNameForm
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(379, 69);
			this.ControlBox = false;
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.lName);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOk);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetProfileNameForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Change profile name";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		public System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Label lName;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Button bCancel;
	}
}
