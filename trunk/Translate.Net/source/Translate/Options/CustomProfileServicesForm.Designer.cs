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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomProfileServicesForm));
			this.pTop = new System.Windows.Forms.Panel();
			this.pLeft = new System.Windows.Forms.Panel();
			this.pRight = new System.Windows.Forms.Panel();
			this.pServiceControl = new System.Windows.Forms.Panel();
			this.speedButton4 = new FreeCL.UI.SpeedButton();
			this.speedButton3 = new FreeCL.UI.SpeedButton();
			this.sbMoveServiceUp = new FreeCL.UI.SpeedButton();
			this.sbMoveServiceDown = new FreeCL.UI.SpeedButton();
			this.pCenter = new System.Windows.Forms.Panel();
			this.speedButton1 = new FreeCL.UI.SpeedButton();
			this.speedButton2 = new FreeCL.UI.SpeedButton();
			this.pBottom = new System.Windows.Forms.Panel();
			this.bCancel = new System.Windows.Forms.Button();
			this.bOk = new System.Windows.Forms.Button();
			this.aAddSelected = new FreeCL.UI.Actions.Action(this.components);
			this.aAddAll = new FreeCL.UI.Actions.Action(this.components);
			this.aRemoveService = new FreeCL.UI.Actions.Action(this.components);
			this.aMoveServiceUp = new FreeCL.UI.Actions.Action(this.components);
			this.aMoveServiceDown = new FreeCL.UI.Actions.Action(this.components);
			this.aClearAll = new FreeCL.UI.Actions.Action(this.components);
			this.gbFilter = new System.Windows.Forms.GroupBox();
			this.cbSubject = new System.Windows.Forms.ComboBox();
			this.lSubject = new System.Windows.Forms.Label();
			this.cbTo = new System.Windows.Forms.ComboBox();
			this.cbFrom = new System.Windows.Forms.ComboBox();
			this.lLangPair = new System.Windows.Forms.Label();
			this.gbSource = new System.Windows.Forms.GroupBox();
			this.lvSource = new Translate.ServicesListView();
			this.serviceStatusSource = new Translate.ServiceStatusControl();
			this.gbCurrent = new System.Windows.Forms.GroupBox();
			this.lvCurrent = new Translate.ServicesListView();
			this.serviceStatusCurrent = new Translate.ServiceStatusControl();
			this.pTop.SuspendLayout();
			this.pLeft.SuspendLayout();
			this.pRight.SuspendLayout();
			this.pServiceControl.SuspendLayout();
			this.pCenter.SuspendLayout();
			this.pBottom.SuspendLayout();
			this.gbFilter.SuspendLayout();
			this.gbSource.SuspendLayout();
			this.gbCurrent.SuspendLayout();
			this.SuspendLayout();
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
			this.il.Images.SetKeyName(0, "ArrowRight.png");
			this.il.Images.SetKeyName(1, "DobuleArrowRight.png");
			this.il.Images.SetKeyName(2, "ArrowUp.16x16.png");
			this.il.Images.SetKeyName(3, "ArrowDown.16x16.png");
			this.il.Images.SetKeyName(4, "ArrowLeft.png");
			this.il.Images.SetKeyName(5, "DobuleArrowLeft.png");
			// 
			// al
			// 
			this.al.Actions.Add(this.aAddSelected);
			this.al.Actions.Add(this.aAddAll);
			this.al.Actions.Add(this.aRemoveService);
			this.al.Actions.Add(this.aMoveServiceUp);
			this.al.Actions.Add(this.aMoveServiceDown);
			this.al.Actions.Add(this.aClearAll);
			// 
			// pTop
			// 
			this.pTop.Controls.Add(this.gbFilter);
			this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pTop.Location = new System.Drawing.Point(0, 0);
			this.pTop.Name = "pTop";
			this.pTop.Size = new System.Drawing.Size(663, 53);
			this.pTop.TabIndex = 0;
			// 
			// pLeft
			// 
			this.pLeft.Controls.Add(this.gbSource);
			this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pLeft.Location = new System.Drawing.Point(0, 53);
			this.pLeft.Name = "pLeft";
			this.pLeft.Size = new System.Drawing.Size(298, 346);
			this.pLeft.TabIndex = 1;
			// 
			// pRight
			// 
			this.pRight.Controls.Add(this.gbCurrent);
			this.pRight.Controls.Add(this.pServiceControl);
			this.pRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pRight.Location = new System.Drawing.Point(324, 53);
			this.pRight.Name = "pRight";
			this.pRight.Size = new System.Drawing.Size(339, 346);
			this.pRight.TabIndex = 2;
			// 
			// pServiceControl
			// 
			this.pServiceControl.Controls.Add(this.speedButton4);
			this.pServiceControl.Controls.Add(this.speedButton3);
			this.pServiceControl.Controls.Add(this.sbMoveServiceUp);
			this.pServiceControl.Controls.Add(this.sbMoveServiceDown);
			this.pServiceControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.pServiceControl.Location = new System.Drawing.Point(313, 0);
			this.pServiceControl.Name = "pServiceControl";
			this.pServiceControl.Size = new System.Drawing.Size(26, 346);
			this.pServiceControl.TabIndex = 8;
			// 
			// speedButton4
			// 
			this.al.SetAction(this.speedButton4, this.aClearAll);
			this.speedButton4.ImageIndex = 5;
			this.speedButton4.ImageList = this.il;
			this.speedButton4.Location = new System.Drawing.Point(3, 167);
			this.speedButton4.Name = "speedButton4";
			this.speedButton4.Selectable = false;
			this.speedButton4.Size = new System.Drawing.Size(20, 20);
			this.speedButton4.TabIndex = 9;
			this.speedButton4.UseVisualStyleBackColor = true;
			// 
			// speedButton3
			// 
			this.al.SetAction(this.speedButton3, this.aRemoveService);
			this.speedButton3.ImageIndex = 4;
			this.speedButton3.ImageList = this.il;
			this.speedButton3.Location = new System.Drawing.Point(3, 141);
			this.speedButton3.Name = "speedButton3";
			this.speedButton3.Selectable = false;
			this.speedButton3.Size = new System.Drawing.Size(20, 20);
			this.speedButton3.TabIndex = 8;
			this.speedButton3.UseVisualStyleBackColor = true;
			// 
			// sbMoveServiceUp
			// 
			this.al.SetAction(this.sbMoveServiceUp, this.aMoveServiceUp);
			this.sbMoveServiceUp.ImageIndex = 2;
			this.sbMoveServiceUp.ImageList = this.il;
			this.sbMoveServiceUp.Location = new System.Drawing.Point(3, 203);
			this.sbMoveServiceUp.Name = "sbMoveServiceUp";
			this.sbMoveServiceUp.Selectable = false;
			this.sbMoveServiceUp.Size = new System.Drawing.Size(20, 20);
			this.sbMoveServiceUp.TabIndex = 7;
			this.sbMoveServiceUp.UseVisualStyleBackColor = true;
			// 
			// sbMoveServiceDown
			// 
			this.al.SetAction(this.sbMoveServiceDown, this.aMoveServiceDown);
			this.sbMoveServiceDown.ImageIndex = 3;
			this.sbMoveServiceDown.ImageList = this.il;
			this.sbMoveServiceDown.Location = new System.Drawing.Point(3, 229);
			this.sbMoveServiceDown.Name = "sbMoveServiceDown";
			this.sbMoveServiceDown.Selectable = false;
			this.sbMoveServiceDown.Size = new System.Drawing.Size(20, 20);
			this.sbMoveServiceDown.TabIndex = 6;
			this.sbMoveServiceDown.UseVisualStyleBackColor = true;
			// 
			// pCenter
			// 
			this.pCenter.Controls.Add(this.speedButton1);
			this.pCenter.Controls.Add(this.speedButton2);
			this.pCenter.Dock = System.Windows.Forms.DockStyle.Left;
			this.pCenter.Location = new System.Drawing.Point(298, 53);
			this.pCenter.Name = "pCenter";
			this.pCenter.Size = new System.Drawing.Size(26, 346);
			this.pCenter.TabIndex = 3;
			// 
			// speedButton1
			// 
			this.al.SetAction(this.speedButton1, this.aAddSelected);
			this.speedButton1.ImageIndex = 0;
			this.speedButton1.ImageList = this.il;
			this.speedButton1.Location = new System.Drawing.Point(3, 141);
			this.speedButton1.Name = "speedButton1";
			this.speedButton1.Selectable = false;
			this.speedButton1.Size = new System.Drawing.Size(20, 20);
			this.speedButton1.TabIndex = 9;
			this.speedButton1.UseVisualStyleBackColor = true;
			// 
			// speedButton2
			// 
			this.al.SetAction(this.speedButton2, this.aAddAll);
			this.speedButton2.ImageIndex = 1;
			this.speedButton2.ImageList = this.il;
			this.speedButton2.Location = new System.Drawing.Point(3, 167);
			this.speedButton2.Name = "speedButton2";
			this.speedButton2.Selectable = false;
			this.speedButton2.Size = new System.Drawing.Size(20, 20);
			this.speedButton2.TabIndex = 8;
			this.speedButton2.UseVisualStyleBackColor = true;
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
			this.bOk.Click += new System.EventHandler(this.BOkClick);
			// 
			// aAddSelected
			// 
			this.aAddSelected.Checked = false;
			this.aAddSelected.Enabled = true;
			this.aAddSelected.Hint = "Add selected service";
			this.aAddSelected.ImageIndex = 0;
			this.aAddSelected.Tag = null;
			this.aAddSelected.Text = "";
			this.aAddSelected.Visible = true;
			this.aAddSelected.Execute += new System.EventHandler(this.AAddSelectedExecute);
			this.aAddSelected.Update += new System.EventHandler(this.AAddSelectedUpdate);
			// 
			// aAddAll
			// 
			this.aAddAll.Checked = false;
			this.aAddAll.Enabled = true;
			this.aAddAll.Hint = "Add all services";
			this.aAddAll.ImageIndex = 1;
			this.aAddAll.Tag = null;
			this.aAddAll.Text = "";
			this.aAddAll.Visible = true;
			this.aAddAll.Execute += new System.EventHandler(this.AAddAllExecute);
			// 
			// aRemoveService
			// 
			this.aRemoveService.Checked = false;
			this.aRemoveService.Enabled = true;
			this.aRemoveService.Hint = "Remove Service";
			this.aRemoveService.ImageIndex = 4;
			this.aRemoveService.Tag = null;
			this.aRemoveService.Text = "";
			this.aRemoveService.Visible = true;
			this.aRemoveService.Execute += new System.EventHandler(this.ARemoveServiceExecute);
			this.aRemoveService.Update += new System.EventHandler(this.ARemoveServiceUpdate);
			// 
			// aMoveServiceUp
			// 
			this.aMoveServiceUp.Checked = false;
			this.aMoveServiceUp.Enabled = true;
			this.aMoveServiceUp.Hint = "Move service up";
			this.aMoveServiceUp.ImageIndex = 2;
			this.aMoveServiceUp.Tag = null;
			this.aMoveServiceUp.Text = "action1";
			this.aMoveServiceUp.Visible = true;
			this.aMoveServiceUp.Execute += new System.EventHandler(this.AMoveServiceUpExecute);
			this.aMoveServiceUp.Update += new System.EventHandler(this.AMoveServiceUpUpdate);
			// 
			// aMoveServiceDown
			// 
			this.aMoveServiceDown.Checked = false;
			this.aMoveServiceDown.Enabled = true;
			this.aMoveServiceDown.Hint = "Move service down";
			this.aMoveServiceDown.ImageIndex = 3;
			this.aMoveServiceDown.Tag = null;
			this.aMoveServiceDown.Text = "action1";
			this.aMoveServiceDown.Visible = true;
			this.aMoveServiceDown.Execute += new System.EventHandler(this.AMoveServiceDownExecute);
			this.aMoveServiceDown.Update += new System.EventHandler(this.AMoveServiceDownUpdate);
			// 
			// aClearAll
			// 
			this.aClearAll.Checked = false;
			this.aClearAll.Enabled = true;
			this.aClearAll.Hint = "Remove all services";
			this.aClearAll.ImageIndex = 5;
			this.aClearAll.Tag = null;
			this.aClearAll.Text = "action1";
			this.aClearAll.Visible = true;
			this.aClearAll.Execute += new System.EventHandler(this.AClearAllExecute);
			this.aClearAll.Update += new System.EventHandler(this.AClearAllUpdate);
			// 
			// gbFilter
			// 
			this.gbFilter.Controls.Add(this.cbSubject);
			this.gbFilter.Controls.Add(this.lSubject);
			this.gbFilter.Controls.Add(this.cbTo);
			this.gbFilter.Controls.Add(this.cbFrom);
			this.gbFilter.Controls.Add(this.lLangPair);
			this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbFilter.Location = new System.Drawing.Point(0, 0);
			this.gbFilter.Name = "gbFilter";
			this.gbFilter.Size = new System.Drawing.Size(663, 53);
			this.gbFilter.TabIndex = 11;
			this.gbFilter.TabStop = false;
			this.gbFilter.Text = "Services filter";
			// 
			// cbSubject
			// 
			this.cbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSubject.FormattingEnabled = true;
			this.cbSubject.Location = new System.Drawing.Point(475, 21);
			this.cbSubject.Name = "cbSubject";
			this.cbSubject.Size = new System.Drawing.Size(110, 21);
			this.cbSubject.Sorted = true;
			this.cbSubject.TabIndex = 15;
			this.cbSubject.SelectedIndexChanged += new System.EventHandler(this.CbFromSelectedIndexChanged);
			// 
			// lSubject
			// 
			this.lSubject.Location = new System.Drawing.Point(386, 19);
			this.lSubject.Name = "lSubject";
			this.lSubject.Size = new System.Drawing.Size(83, 23);
			this.lSubject.TabIndex = 14;
			this.lSubject.Text = "Subject";
			this.lSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbTo
			// 
			this.cbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTo.FormattingEnabled = true;
			this.cbTo.Location = new System.Drawing.Point(260, 21);
			this.cbTo.Name = "cbTo";
			this.cbTo.Size = new System.Drawing.Size(110, 21);
			this.cbTo.TabIndex = 13;
			this.cbTo.SelectedIndexChanged += new System.EventHandler(this.CbFromSelectedIndexChanged);
			// 
			// cbFrom
			// 
			this.cbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFrom.FormattingEnabled = true;
			this.cbFrom.Location = new System.Drawing.Point(144, 21);
			this.cbFrom.Name = "cbFrom";
			this.cbFrom.Size = new System.Drawing.Size(110, 21);
			this.cbFrom.TabIndex = 12;
			this.cbFrom.SelectedIndexChanged += new System.EventHandler(this.CbFromSelectedIndexChanged);
			// 
			// lLangPair
			// 
			this.lLangPair.Location = new System.Drawing.Point(6, 19);
			this.lLangPair.Name = "lLangPair";
			this.lLangPair.Size = new System.Drawing.Size(135, 23);
			this.lLangPair.TabIndex = 11;
			this.lLangPair.Text = "Translation direction";
			this.lLangPair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gbSource
			// 
			this.gbSource.Controls.Add(this.lvSource);
			this.gbSource.Controls.Add(this.serviceStatusSource);
			this.gbSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbSource.Location = new System.Drawing.Point(0, 0);
			this.gbSource.Name = "gbSource";
			this.gbSource.Size = new System.Drawing.Size(298, 346);
			this.gbSource.TabIndex = 2;
			this.gbSource.TabStop = false;
			this.gbSource.Text = "Exists services";
			// 
			// lvSource
			// 
			this.lvSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSource.Location = new System.Drawing.Point(3, 142);
			this.lvSource.Name = "lvSource";
			this.lvSource.Size = new System.Drawing.Size(292, 201);
			this.lvSource.Sorted = true;
			this.lvSource.TabIndex = 2;
			this.lvSource.ServiceItemChangedEvent += new System.EventHandler<Translate.ServiceItemChangedEventArgs>(this.LvSourceServiceItemChangedEvent);
			// 
			// serviceStatusSource
			// 
			this.serviceStatusSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.serviceStatusSource.Location = new System.Drawing.Point(3, 16);
			this.serviceStatusSource.Name = "serviceStatusSource";
			this.serviceStatusSource.ShortView = true;
			this.serviceStatusSource.ShowLanguage = false;
			this.serviceStatusSource.Size = new System.Drawing.Size(292, 126);
			this.serviceStatusSource.TabIndex = 3;
			// 
			// gbCurrent
			// 
			this.gbCurrent.Controls.Add(this.lvCurrent);
			this.gbCurrent.Controls.Add(this.serviceStatusCurrent);
			this.gbCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbCurrent.Location = new System.Drawing.Point(0, 0);
			this.gbCurrent.Name = "gbCurrent";
			this.gbCurrent.Size = new System.Drawing.Size(313, 346);
			this.gbCurrent.TabIndex = 10;
			this.gbCurrent.TabStop = false;
			this.gbCurrent.Text = "Selected services";
			// 
			// lvCurrent
			// 
			this.lvCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvCurrent.Location = new System.Drawing.Point(3, 142);
			this.lvCurrent.Name = "lvCurrent";
			this.lvCurrent.Size = new System.Drawing.Size(307, 201);
			this.lvCurrent.Sorted = false;
			this.lvCurrent.TabIndex = 10;
			this.lvCurrent.ServiceItemChangedEvent += new System.EventHandler<Translate.ServiceItemChangedEventArgs>(this.LvCurrentServiceItemChangedEvent);
			// 
			// serviceStatusCurrent
			// 
			this.serviceStatusCurrent.Dock = System.Windows.Forms.DockStyle.Top;
			this.serviceStatusCurrent.Location = new System.Drawing.Point(3, 16);
			this.serviceStatusCurrent.Name = "serviceStatusCurrent";
			this.serviceStatusCurrent.ShortView = true;
			this.serviceStatusCurrent.ShowLanguage = false;
			this.serviceStatusCurrent.Size = new System.Drawing.Size(307, 126);
			this.serviceStatusCurrent.TabIndex = 11;
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
			this.pCenter.ResumeLayout(false);
			this.pBottom.ResumeLayout(false);
			this.gbFilter.ResumeLayout(false);
			this.gbSource.ResumeLayout(false);
			this.gbCurrent.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.GroupBox gbCurrent;
		private System.Windows.Forms.GroupBox gbSource;
		private System.Windows.Forms.GroupBox gbFilter;
		private FreeCL.UI.SpeedButton speedButton4;
		private FreeCL.UI.Actions.Action aClearAll;
		private FreeCL.UI.Actions.Action aMoveServiceDown;
		private FreeCL.UI.Actions.Action aMoveServiceUp;
		private FreeCL.UI.SpeedButton speedButton3;
		private FreeCL.UI.Actions.Action aRemoveService;
		private FreeCL.UI.Actions.Action aAddAll;
		private FreeCL.UI.Actions.Action aAddSelected;
		private FreeCL.UI.SpeedButton speedButton2;
		private FreeCL.UI.SpeedButton speedButton1;
		private Translate.ServiceStatusControl serviceStatusCurrent;
		private Translate.ServiceStatusControl serviceStatusSource;
		private System.Windows.Forms.ComboBox cbSubject;
		private System.Windows.Forms.Label lSubject;
		private System.Windows.Forms.Label lLangPair;
		private System.Windows.Forms.ComboBox cbFrom;
		private System.Windows.Forms.ComboBox cbTo;
		private Translate.ServicesListView lvCurrent;
		private FreeCL.UI.SpeedButton sbMoveServiceDown;
		private FreeCL.UI.SpeedButton sbMoveServiceUp;
		private System.Windows.Forms.Panel pServiceControl;
		private Translate.ServicesListView lvSource;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Panel pBottom;
		private System.Windows.Forms.Panel pCenter;
		private System.Windows.Forms.Panel pRight;
		private System.Windows.Forms.Panel pLeft;
		private System.Windows.Forms.Panel pTop;
	}
}
