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
	partial class HotkeyEditorControl
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.lModifiers = new System.Windows.Forms.Label();
			this.cbAlt = new System.Windows.Forms.CheckBox();
			this.cbShift = new System.Windows.Forms.CheckBox();
			this.cbCtrl = new System.Windows.Forms.CheckBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lMouse = new System.Windows.Forms.Label();
			this.cbX2 = new System.Windows.Forms.CheckBox();
			this.cbX1 = new System.Windows.Forms.CheckBox();
			this.cbRight = new System.Windows.Forms.CheckBox();
			this.cbMiddle = new System.Windows.Forms.CheckBox();
			this.cbLeft = new System.Windows.Forms.CheckBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lKey = new System.Windows.Forms.Label();
			this.cbKey = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbAlt);
			this.panel1.Controls.Add(this.cbShift);
			this.panel1.Controls.Add(this.cbCtrl);
			this.panel1.Controls.Add(this.lModifiers);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(358, 24);
			this.panel1.TabIndex = 10;
			// 
			// lModifiers
			// 
			this.lModifiers.Dock = System.Windows.Forms.DockStyle.Left;
			this.lModifiers.Location = new System.Drawing.Point(0, 0);
			this.lModifiers.Name = "lModifiers";
			this.lModifiers.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.lModifiers.Size = new System.Drawing.Size(100, 24);
			this.lModifiers.TabIndex = 5;
			this.lModifiers.Text = "Modifiers : ";
			this.lModifiers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbAlt
			// 
			this.cbAlt.AutoSize = true;
			this.cbAlt.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbAlt.Location = new System.Drawing.Point(188, 0);
			this.cbAlt.Name = "cbAlt";
			this.cbAlt.Size = new System.Drawing.Size(38, 24);
			this.cbAlt.TabIndex = 8;
			this.cbAlt.Text = "Alt";
			this.cbAlt.UseVisualStyleBackColor = true;
			// 
			// cbShift
			// 
			this.cbShift.AutoSize = true;
			this.cbShift.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbShift.Location = new System.Drawing.Point(141, 0);
			this.cbShift.Name = "cbShift";
			this.cbShift.Size = new System.Drawing.Size(47, 24);
			this.cbShift.TabIndex = 7;
			this.cbShift.Text = "Shift";
			this.cbShift.UseVisualStyleBackColor = true;
			// 
			// cbCtrl
			// 
			this.cbCtrl.AutoSize = true;
			this.cbCtrl.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbCtrl.Location = new System.Drawing.Point(100, 0);
			this.cbCtrl.Name = "cbCtrl";
			this.cbCtrl.Size = new System.Drawing.Size(41, 24);
			this.cbCtrl.TabIndex = 6;
			this.cbCtrl.Text = "Ctrl";
			this.cbCtrl.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cbX2);
			this.panel2.Controls.Add(this.cbX1);
			this.panel2.Controls.Add(this.cbRight);
			this.panel2.Controls.Add(this.cbMiddle);
			this.panel2.Controls.Add(this.cbLeft);
			this.panel2.Controls.Add(this.lMouse);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 24);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(358, 24);
			this.panel2.TabIndex = 12;
			// 
			// lMouse
			// 
			this.lMouse.Dock = System.Windows.Forms.DockStyle.Left;
			this.lMouse.Location = new System.Drawing.Point(0, 0);
			this.lMouse.Name = "lMouse";
			this.lMouse.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.lMouse.Size = new System.Drawing.Size(100, 24);
			this.lMouse.TabIndex = 17;
			this.lMouse.Text = "Mouse : ";
			this.lMouse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbX2
			// 
			this.cbX2.AutoSize = true;
			this.cbX2.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbX2.Location = new System.Drawing.Point(291, 0);
			this.cbX2.Name = "cbX2";
			this.cbX2.Size = new System.Drawing.Size(39, 24);
			this.cbX2.TabIndex = 16;
			this.cbX2.Text = "X2";
			this.cbX2.UseVisualStyleBackColor = true;
			// 
			// cbX1
			// 
			this.cbX1.AutoSize = true;
			this.cbX1.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbX1.Location = new System.Drawing.Point(252, 0);
			this.cbX1.Name = "cbX1";
			this.cbX1.Size = new System.Drawing.Size(39, 24);
			this.cbX1.TabIndex = 15;
			this.cbX1.Text = "X1";
			this.cbX1.UseVisualStyleBackColor = true;
			// 
			// cbRight
			// 
			this.cbRight.AutoSize = true;
			this.cbRight.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbRight.Location = new System.Drawing.Point(201, 0);
			this.cbRight.Name = "cbRight";
			this.cbRight.Size = new System.Drawing.Size(51, 24);
			this.cbRight.TabIndex = 14;
			this.cbRight.Text = "Right";
			this.cbRight.UseVisualStyleBackColor = true;
			// 
			// cbMiddle
			// 
			this.cbMiddle.AutoSize = true;
			this.cbMiddle.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbMiddle.Location = new System.Drawing.Point(144, 0);
			this.cbMiddle.Name = "cbMiddle";
			this.cbMiddle.Size = new System.Drawing.Size(57, 24);
			this.cbMiddle.TabIndex = 13;
			this.cbMiddle.Text = "Middle";
			this.cbMiddle.UseVisualStyleBackColor = true;
			// 
			// cbLeft
			// 
			this.cbLeft.AutoSize = true;
			this.cbLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbLeft.Location = new System.Drawing.Point(100, 0);
			this.cbLeft.Name = "cbLeft";
			this.cbLeft.Size = new System.Drawing.Size(44, 24);
			this.cbLeft.TabIndex = 12;
			this.cbLeft.Text = "Left";
			this.cbLeft.UseVisualStyleBackColor = true;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.cbKey);
			this.panel3.Controls.Add(this.lKey);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 48);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(358, 24);
			this.panel3.TabIndex = 13;
			// 
			// lKey
			// 
			this.lKey.Dock = System.Windows.Forms.DockStyle.Left;
			this.lKey.Location = new System.Drawing.Point(0, 0);
			this.lKey.Name = "lKey";
			this.lKey.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.lKey.Size = new System.Drawing.Size(100, 24);
			this.lKey.TabIndex = 18;
			this.lKey.Text = "Key : ";
			this.lKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbKey
			// 
			this.cbKey.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbKey.FormattingEnabled = true;
			this.cbKey.Location = new System.Drawing.Point(100, 0);
			this.cbKey.Name = "cbKey";
			this.cbKey.Size = new System.Drawing.Size(121, 21);
			this.cbKey.TabIndex = 19;
			// 
			// HotkeyEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "HotkeyEditorControl";
			this.Size = new System.Drawing.Size(358, 76);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lKey;
		private System.Windows.Forms.ComboBox cbKey;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.CheckBox cbLeft;
		private System.Windows.Forms.CheckBox cbMiddle;
		private System.Windows.Forms.CheckBox cbRight;
		private System.Windows.Forms.CheckBox cbX1;
		private System.Windows.Forms.CheckBox cbX2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lMouse;
		private System.Windows.Forms.CheckBox cbAlt;
		private System.Windows.Forms.CheckBox cbShift;
		private System.Windows.Forms.CheckBox cbCtrl;
		private System.Windows.Forms.Label lModifiers;
	}
}
