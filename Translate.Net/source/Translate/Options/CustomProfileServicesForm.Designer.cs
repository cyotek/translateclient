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
	partial class CustomProfileServicesForm
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
			this.pTop = new FreeCL.UI.Panel();
			this.cbSubject = new System.Windows.Forms.ComboBox();
			this.lSubject = new System.Windows.Forms.Label();
			this.cbTo = new System.Windows.Forms.ComboBox();
			this.cbFrom = new System.Windows.Forms.ComboBox();
			this.lLangPair = new System.Windows.Forms.Label();
			this.pLeft = new FreeCL.UI.Panel();
			this.lvSource = new Translate.Options.ServicesListView();
			this.pRight = new FreeCL.UI.Panel();
			this.lvCurrent = new Translate.Options.ServicesListView();
			this.pServiceControl = new System.Windows.Forms.Panel();
			this.sbMoveServiceUp = new FreeCL.UI.SpeedButton();
			this.sbMoveServiceDown = new FreeCL.UI.SpeedButton();
			this.pCenter = new FreeCL.UI.Panel();
			this.pBottom = new FreeCL.UI.Panel();
			this.bCancel = new System.Windows.Forms.Button();
			this.bOk = new System.Windows.Forms.Button();
			this.pTop.SuspendLayout();
			this.pLeft.SuspendLayout();
			this.pRight.SuspendLayout();
			this.pServiceControl.SuspendLayout();
			this.pBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// pTop
			// 
			this.pTop.Controls.Add(this.cbSubject);
			this.pTop.Controls.Add(this.lSubject);
			this.pTop.Controls.Add(this.cbTo);
			this.pTop.Controls.Add(this.cbFrom);
			this.pTop.Controls.Add(this.lLangPair);
			this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pTop.Location = new System.Drawing.Point(0, 0);
			this.pTop.Name = "pTop";
			this.pTop.Size = new System.Drawing.Size(663, 45);
			this.pTop.TabIndex = 0;
			// 
			// cbSubject
			// 
			this.cbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSubject.FormattingEnabled = true;
			this.cbSubject.Location = new System.Drawing.Point(475, 11);
			this.cbSubject.Name = "cbSubject";
			this.cbSubject.Size = new System.Drawing.Size(110, 21);
			this.cbSubject.Sorted = true;
			this.cbSubject.TabIndex = 10;
			this.cbSubject.SelectedIndexChanged += new System.EventHandler(this.CbFromSelectedIndexChanged);
			// 
			// lSubject
			// 
			this.lSubject.Location = new System.Drawing.Point(386, 9);
			this.lSubject.Name = "lSubject";
			this.lSubject.Size = new System.Drawing.Size(83, 23);
			this.lSubject.TabIndex = 9;
			this.lSubject.Text = "Subject";
			this.lSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbTo
			// 
			this.cbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTo.FormattingEnabled = true;
			this.cbTo.Location = new System.Drawing.Point(260, 12);
			this.cbTo.Name = "cbTo";
			this.cbTo.Size = new System.Drawing.Size(110, 21);
			this.cbTo.TabIndex = 8;
			this.cbTo.SelectedIndexChanged += new System.EventHandler(this.CbFromSelectedIndexChanged);
			// 
			// cbFrom
			// 
			this.cbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFrom.FormattingEnabled = true;
			this.cbFrom.Location = new System.Drawing.Point(144, 11);
			this.cbFrom.Name = "cbFrom";
			this.cbFrom.Size = new System.Drawing.Size(110, 21);
			this.cbFrom.TabIndex = 7;
			this.cbFrom.SelectedIndexChanged += new System.EventHandler(this.CbFromSelectedIndexChanged);
			// 
			// lLangPair
			// 
			this.lLangPair.Location = new System.Drawing.Point(3, 9);
			this.lLangPair.Name = "lLangPair";
			this.lLangPair.Size = new System.Drawing.Size(135, 23);
			this.lLangPair.TabIndex = 6;
			this.lLangPair.Text = "Translation direction";
			this.lLangPair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pLeft
			// 
			this.pLeft.Controls.Add(this.lvSource);
			this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pLeft.Location = new System.Drawing.Point(0, 45);
			this.pLeft.Name = "pLeft";
			this.pLeft.Size = new System.Drawing.Size(298, 354);
			this.pLeft.TabIndex = 1;
			// 
			// lvSource
			// 
			this.lvSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSource.Location = new System.Drawing.Point(0, 0);
			this.lvSource.Name = "lvSource";
			this.lvSource.Size = new System.Drawing.Size(298, 354);
			this.lvSource.TabIndex = 0;
			// 
			// pRight
			// 
			this.pRight.Controls.Add(this.lvCurrent);
			this.pRight.Controls.Add(this.pServiceControl);
			this.pRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pRight.Location = new System.Drawing.Point(345, 45);
			this.pRight.Name = "pRight";
			this.pRight.Size = new System.Drawing.Size(318, 354);
			this.pRight.TabIndex = 2;
			// 
			// lvCurrent
			// 
			this.lvCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvCurrent.Location = new System.Drawing.Point(0, 0);
			this.lvCurrent.Name = "lvCurrent";
			this.lvCurrent.Size = new System.Drawing.Size(292, 354);
			this.lvCurrent.TabIndex = 0;
			// 
			// pServiceControl
			// 
			this.pServiceControl.Controls.Add(this.sbMoveServiceUp);
			this.pServiceControl.Controls.Add(this.sbMoveServiceDown);
			this.pServiceControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.pServiceControl.Location = new System.Drawing.Point(292, 0);
			this.pServiceControl.Name = "pServiceControl";
			this.pServiceControl.Size = new System.Drawing.Size(26, 354);
			this.pServiceControl.TabIndex = 8;
			// 
			// sbMoveServiceUp
			// 
			this.sbMoveServiceUp.ImageIndex = 3;
			this.sbMoveServiceUp.Location = new System.Drawing.Point(3, 74);
			this.sbMoveServiceUp.Name = "sbMoveServiceUp";
			this.sbMoveServiceUp.Selectable = false;
			this.sbMoveServiceUp.Size = new System.Drawing.Size(20, 20);
			this.sbMoveServiceUp.TabIndex = 7;
			this.sbMoveServiceUp.UseVisualStyleBackColor = true;
			// 
			// sbMoveServiceDown
			// 
			this.sbMoveServiceDown.ImageIndex = 4;
			this.sbMoveServiceDown.Location = new System.Drawing.Point(3, 100);
			this.sbMoveServiceDown.Name = "sbMoveServiceDown";
			this.sbMoveServiceDown.Selectable = false;
			this.sbMoveServiceDown.Size = new System.Drawing.Size(20, 20);
			this.sbMoveServiceDown.TabIndex = 6;
			this.sbMoveServiceDown.UseVisualStyleBackColor = true;
			// 
			// pCenter
			// 
			this.pCenter.Dock = System.Windows.Forms.DockStyle.Left;
			this.pCenter.Location = new System.Drawing.Point(298, 45);
			this.pCenter.Name = "pCenter";
			this.pCenter.Size = new System.Drawing.Size(47, 354);
			this.pCenter.TabIndex = 3;
			// 
			// pBottom
			// 
			this.pBottom.Controls.Add(this.bCancel);
			this.pBottom.Controls.Add(this.bOk);
			this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBottom.Location = new System.Drawing.Point(0, 399);
			this.pBottom.Name = "pBottom";
			this.pBottom.Size = new System.Drawing.Size(663, 33);
			this.pBottom.TabIndex = 4;
			// 
			// bCancel
			// 
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(563, 7);
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
			this.bOk.Location = new System.Drawing.Point(469, 7);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(88, 23);
			this.bOk.TabIndex = 4;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			// 
			// CustomProfileServicesForm
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(663, 432);
			this.Controls.Add(this.pRight);
			this.Controls.Add(this.pCenter);
			this.Controls.Add(this.pLeft);
			this.Controls.Add(this.pTop);
			this.Controls.Add(this.pBottom);
			this.MinimizeBox = false;
			this.Name = "CustomProfileServicesForm";
			this.ShowInTaskbar = false;
			this.Text = "Edit services";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.SizeChanged += new System.EventHandler(this.CustomProfileServicesFormSizeChanged);
			this.pTop.ResumeLayout(false);
			this.pLeft.ResumeLayout(false);
			this.pRight.ResumeLayout(false);
			this.pServiceControl.ResumeLayout(false);
			this.pBottom.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ComboBox cbSubject;
		private System.Windows.Forms.Label lSubject;
		private System.Windows.Forms.Label lLangPair;
		private System.Windows.Forms.ComboBox cbFrom;
		private System.Windows.Forms.ComboBox cbTo;
		private Translate.Options.ServicesListView lvCurrent;
		private FreeCL.UI.SpeedButton sbMoveServiceDown;
		private FreeCL.UI.SpeedButton sbMoveServiceUp;
		private System.Windows.Forms.Panel pServiceControl;
		private Translate.Options.ServicesListView lvSource;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Button bCancel;
		private FreeCL.UI.Panel pBottom;
		private FreeCL.UI.Panel pCenter;
		private FreeCL.UI.Panel pRight;
		private FreeCL.UI.Panel pLeft;
		private FreeCL.UI.Panel pTop;
	}
}
