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

using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	partial class CustomProfilesControl
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomProfilesControl));
			this.pProfiles = new System.Windows.Forms.Panel();
			this.lvProfiles = new System.Windows.Forms.ListView();
			this.chName = new System.Windows.Forms.ColumnHeader();
			this.chDirection = new System.Windows.Forms.ColumnHeader();
			this.chSubject = new System.Windows.Forms.ColumnHeader();
			this.pProfilesControl = new System.Windows.Forms.Panel();
			this.speedButton2 = new FreeCL.UI.SpeedButton();
			this.ilMain = new System.Windows.Forms.ImageList(this.components);
			this.sbAddProfile = new FreeCL.UI.SpeedButton();
			this.sbServiceUp = new FreeCL.UI.SpeedButton();
			this.sbServiceDown = new FreeCL.UI.SpeedButton();
			this.tcOptions = new System.Windows.Forms.TabControl();
			this.tpOptions = new System.Windows.Forms.TabPage();
			this.cbSubject = new System.Windows.Forms.ComboBox();
			this.lSubject = new System.Windows.Forms.Label();
			this.cbTo = new System.Windows.Forms.ComboBox();
			this.cbFrom = new System.Windows.Forms.ComboBox();
			this.lLangPair = new System.Windows.Forms.Label();
			this.bChangeName = new System.Windows.Forms.Button();
			this.lProfileName = new System.Windows.Forms.Label();
			this.lName = new System.Windows.Forms.Label();
			this.tpServices = new System.Windows.Forms.TabPage();
			this.lvServices = new Translate.ServicesListView();
			this.pServiceControl = new System.Windows.Forms.Panel();
			this.sbRemoveService = new FreeCL.UI.SpeedButton();
			this.sbEditServices = new FreeCL.UI.SpeedButton();
			this.sbMoveServiceUp = new FreeCL.UI.SpeedButton();
			this.sbMoveServiceDown = new FreeCL.UI.SpeedButton();
			this.tpDefaultOptions = new System.Windows.Forms.TabPage();
			this.cbIncludeMonolingualDictionaryInTranslation = new System.Windows.Forms.CheckBox();
			this.alMain = new FreeCL.UI.Actions.ActionList(this.components);
			this.aRemoveProfile = new FreeCL.UI.Actions.Action(this.components);
			this.aAddProfile = new FreeCL.UI.Actions.Action(this.components);
			this.aMoveProfileUp = new FreeCL.UI.Actions.Action(this.components);
			this.aMoveProfileDown = new FreeCL.UI.Actions.Action(this.components);
			this.aRemoveService = new FreeCL.UI.Actions.Action(this.components);
			this.aEditServices = new FreeCL.UI.Actions.Action(this.components);
			this.aMoveServiceUp = new FreeCL.UI.Actions.Action(this.components);
			this.aMoveServiceDown = new FreeCL.UI.Actions.Action(this.components);
			this.pBody.SuspendLayout();
			this.pProfiles.SuspendLayout();
			this.pProfilesControl.SuspendLayout();
			this.tcOptions.SuspendLayout();
			this.tpOptions.SuspendLayout();
			this.tpServices.SuspendLayout();
			this.pServiceControl.SuspendLayout();
			this.tpDefaultOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.pProfiles);
			this.pBody.Controls.Add(this.tcOptions);
			this.pBody.Size = new System.Drawing.Size(419, 358);
			// 
			// lCaption
			// 
			this.lCaption.Size = new System.Drawing.Size(403, 24);
			// 
			// pProfiles
			// 
			this.pProfiles.Controls.Add(this.lvProfiles);
			this.pProfiles.Controls.Add(this.pProfilesControl);
			this.pProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pProfiles.Location = new System.Drawing.Point(10, 10);
			this.pProfiles.Name = "pProfiles";
			this.pProfiles.Size = new System.Drawing.Size(399, 182);
			this.pProfiles.TabIndex = 0;
			// 
			// lvProfiles
			// 
			this.lvProfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.chName,
									this.chDirection,
									this.chSubject});
			this.lvProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvProfiles.FullRowSelect = true;
			this.lvProfiles.GridLines = true;
			this.lvProfiles.HideSelection = false;
			this.lvProfiles.Location = new System.Drawing.Point(0, 0);
			this.lvProfiles.MultiSelect = false;
			this.lvProfiles.Name = "lvProfiles";
			this.lvProfiles.Size = new System.Drawing.Size(373, 182);
			this.lvProfiles.TabIndex = 0;
			this.lvProfiles.UseCompatibleStateImageBehavior = false;
			this.lvProfiles.View = System.Windows.Forms.View.Details;
			this.lvProfiles.SelectedIndexChanged += new System.EventHandler(this.LvProfilesSelectedIndexChanged);
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 120;
			// 
			// chDirection
			// 
			this.chDirection.Text = "Translation direction";
			// 
			// chSubject
			// 
			this.chSubject.Text = "Subject";
			// 
			// pProfilesControl
			// 
			this.pProfilesControl.Controls.Add(this.speedButton2);
			this.pProfilesControl.Controls.Add(this.sbAddProfile);
			this.pProfilesControl.Controls.Add(this.sbServiceUp);
			this.pProfilesControl.Controls.Add(this.sbServiceDown);
			this.pProfilesControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.pProfilesControl.Location = new System.Drawing.Point(373, 0);
			this.pProfilesControl.Name = "pProfilesControl";
			this.pProfilesControl.Size = new System.Drawing.Size(26, 182);
			this.pProfilesControl.TabIndex = 6;
			// 
			// speedButton2
			// 
			this.alMain.SetAction(this.speedButton2, this.aRemoveProfile);
			this.speedButton2.ImageIndex = 1;
			this.speedButton2.ImageList = this.ilMain;
			this.speedButton2.Location = new System.Drawing.Point(3, 29);
			this.speedButton2.Name = "speedButton2";
			this.speedButton2.Selectable = false;
			this.speedButton2.Size = new System.Drawing.Size(20, 20);
			this.speedButton2.TabIndex = 9;
			this.speedButton2.UseVisualStyleBackColor = true;
			// 
			// ilMain
			// 
			this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
			this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
			this.ilMain.Images.SetKeyName(0, "plus.16x16.png");
			this.ilMain.Images.SetKeyName(1, "minus.16x16.png");
			this.ilMain.Images.SetKeyName(2, "edit.16x16.png");
			this.ilMain.Images.SetKeyName(3, "ArrowUp.16x16.png");
			this.ilMain.Images.SetKeyName(4, "ArrowDown.16x16.png");
			// 
			// sbAddProfile
			// 
			this.alMain.SetAction(this.sbAddProfile, this.aAddProfile);
			this.sbAddProfile.ImageIndex = 0;
			this.sbAddProfile.ImageList = this.ilMain;
			this.sbAddProfile.Location = new System.Drawing.Point(3, 3);
			this.sbAddProfile.Name = "sbAddProfile";
			this.sbAddProfile.Selectable = false;
			this.sbAddProfile.Size = new System.Drawing.Size(20, 20);
			this.sbAddProfile.TabIndex = 8;
			this.sbAddProfile.UseVisualStyleBackColor = true;
			// 
			// sbServiceUp
			// 
			this.alMain.SetAction(this.sbServiceUp, this.aMoveProfileUp);
			this.sbServiceUp.ImageIndex = 3;
			this.sbServiceUp.ImageList = this.ilMain;
			this.sbServiceUp.Location = new System.Drawing.Point(3, 71);
			this.sbServiceUp.Name = "sbServiceUp";
			this.sbServiceUp.Selectable = false;
			this.sbServiceUp.Size = new System.Drawing.Size(20, 20);
			this.sbServiceUp.TabIndex = 7;
			this.sbServiceUp.UseVisualStyleBackColor = true;
			// 
			// sbServiceDown
			// 
			this.alMain.SetAction(this.sbServiceDown, this.aMoveProfileDown);
			this.sbServiceDown.ImageIndex = 4;
			this.sbServiceDown.ImageList = this.ilMain;
			this.sbServiceDown.Location = new System.Drawing.Point(3, 97);
			this.sbServiceDown.Name = "sbServiceDown";
			this.sbServiceDown.Selectable = false;
			this.sbServiceDown.Size = new System.Drawing.Size(20, 20);
			this.sbServiceDown.TabIndex = 6;
			this.sbServiceDown.UseVisualStyleBackColor = true;
			// 
			// tcOptions
			// 
			this.tcOptions.Controls.Add(this.tpServices);
			this.tcOptions.Controls.Add(this.tpOptions);
			this.tcOptions.Controls.Add(this.tpDefaultOptions);
			this.tcOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tcOptions.Location = new System.Drawing.Point(10, 192);
			this.tcOptions.Name = "tcOptions";
			this.tcOptions.SelectedIndex = 0;
			this.tcOptions.Size = new System.Drawing.Size(399, 156);
			this.tcOptions.TabIndex = 1;
			// 
			// tpOptions
			// 
			this.tpOptions.Controls.Add(this.cbSubject);
			this.tpOptions.Controls.Add(this.lSubject);
			this.tpOptions.Controls.Add(this.cbTo);
			this.tpOptions.Controls.Add(this.cbFrom);
			this.tpOptions.Controls.Add(this.lLangPair);
			this.tpOptions.Controls.Add(this.bChangeName);
			this.tpOptions.Controls.Add(this.lProfileName);
			this.tpOptions.Controls.Add(this.lName);
			this.tpOptions.Location = new System.Drawing.Point(4, 22);
			this.tpOptions.Name = "tpOptions";
			this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tpOptions.Size = new System.Drawing.Size(391, 130);
			this.tpOptions.TabIndex = 0;
			this.tpOptions.Text = "Options";
			this.tpOptions.UseVisualStyleBackColor = true;
			// 
			// cbSubject
			// 
			this.cbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSubject.FormattingEnabled = true;
			this.cbSubject.Location = new System.Drawing.Point(159, 70);
			this.cbSubject.Name = "cbSubject";
			this.cbSubject.Size = new System.Drawing.Size(110, 21);
			this.cbSubject.Sorted = true;
			this.cbSubject.TabIndex = 12;
			this.cbSubject.SelectedIndexChanged += new System.EventHandler(this.CbSubjectSelectedIndexChanged);
			// 
			// lSubject
			// 
			this.lSubject.Location = new System.Drawing.Point(7, 68);
			this.lSubject.Name = "lSubject";
			this.lSubject.Size = new System.Drawing.Size(83, 23);
			this.lSubject.TabIndex = 11;
			this.lSubject.Text = "Subject";
			this.lSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbTo
			// 
			this.cbTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTo.FormattingEnabled = true;
			this.cbTo.Location = new System.Drawing.Point(275, 41);
			this.cbTo.Name = "cbTo";
			this.cbTo.Size = new System.Drawing.Size(110, 21);
			this.cbTo.TabIndex = 5;
			this.cbTo.SelectedIndexChanged += new System.EventHandler(this.CbToSelectedIndexChanged);
			// 
			// cbFrom
			// 
			this.cbFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFrom.FormattingEnabled = true;
			this.cbFrom.Location = new System.Drawing.Point(159, 41);
			this.cbFrom.Name = "cbFrom";
			this.cbFrom.Size = new System.Drawing.Size(110, 21);
			this.cbFrom.TabIndex = 4;
			this.cbFrom.SelectedIndexChanged += new System.EventHandler(this.CbFromSelectedIndexChanged);
			// 
			// lLangPair
			// 
			this.lLangPair.Location = new System.Drawing.Point(7, 39);
			this.lLangPair.Name = "lLangPair";
			this.lLangPair.Size = new System.Drawing.Size(135, 23);
			this.lLangPair.TabIndex = 3;
			this.lLangPair.Text = "Translation direction";
			this.lLangPair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// bChangeName
			// 
			this.bChangeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bChangeName.Location = new System.Drawing.Point(217, 6);
			this.bChangeName.Name = "bChangeName";
			this.bChangeName.Size = new System.Drawing.Size(168, 23);
			this.bChangeName.TabIndex = 2;
			this.bChangeName.Text = "Change";
			this.bChangeName.UseVisualStyleBackColor = true;
			this.bChangeName.Click += new System.EventHandler(this.BChangeNameClick);
			// 
			// lProfileName
			// 
			this.lProfileName.Location = new System.Drawing.Point(84, 7);
			this.lProfileName.Name = "lProfileName";
			this.lProfileName.Size = new System.Drawing.Size(127, 23);
			this.lProfileName.TabIndex = 1;
			this.lProfileName.Text = "label1";
			this.lProfileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lName
			// 
			this.lName.Location = new System.Drawing.Point(7, 7);
			this.lName.Name = "lName";
			this.lName.Size = new System.Drawing.Size(71, 23);
			this.lName.TabIndex = 0;
			this.lName.Text = "Name";
			this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tpServices
			// 
			this.tpServices.Controls.Add(this.lvServices);
			this.tpServices.Controls.Add(this.pServiceControl);
			this.tpServices.Location = new System.Drawing.Point(4, 22);
			this.tpServices.Name = "tpServices";
			this.tpServices.Padding = new System.Windows.Forms.Padding(3);
			this.tpServices.Size = new System.Drawing.Size(391, 130);
			this.tpServices.TabIndex = 1;
			this.tpServices.Text = "Services";
			this.tpServices.UseVisualStyleBackColor = true;
			// 
			// lvServices
			// 
			this.lvServices.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvServices.Location = new System.Drawing.Point(3, 3);
			this.lvServices.Name = "lvServices";
			this.lvServices.Size = new System.Drawing.Size(359, 124);
			this.lvServices.Sorted = false;
			this.lvServices.TabIndex = 8;
			// 
			// pServiceControl
			// 
			this.pServiceControl.Controls.Add(this.sbRemoveService);
			this.pServiceControl.Controls.Add(this.sbEditServices);
			this.pServiceControl.Controls.Add(this.sbMoveServiceUp);
			this.pServiceControl.Controls.Add(this.sbMoveServiceDown);
			this.pServiceControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.pServiceControl.Location = new System.Drawing.Point(362, 3);
			this.pServiceControl.Name = "pServiceControl";
			this.pServiceControl.Size = new System.Drawing.Size(26, 124);
			this.pServiceControl.TabIndex = 7;
			// 
			// sbRemoveService
			// 
			this.alMain.SetAction(this.sbRemoveService, this.aRemoveService);
			this.sbRemoveService.ImageIndex = 1;
			this.sbRemoveService.ImageList = this.ilMain;
			this.sbRemoveService.Location = new System.Drawing.Point(3, 29);
			this.sbRemoveService.Name = "sbRemoveService";
			this.sbRemoveService.Selectable = false;
			this.sbRemoveService.Size = new System.Drawing.Size(20, 20);
			this.sbRemoveService.TabIndex = 9;
			this.sbRemoveService.UseVisualStyleBackColor = true;
			// 
			// sbEditServices
			// 
			this.alMain.SetAction(this.sbEditServices, this.aEditServices);
			this.sbEditServices.ImageIndex = 2;
			this.sbEditServices.ImageList = this.ilMain;
			this.sbEditServices.Location = new System.Drawing.Point(3, 3);
			this.sbEditServices.Name = "sbEditServices";
			this.sbEditServices.Selectable = false;
			this.sbEditServices.Size = new System.Drawing.Size(20, 20);
			this.sbEditServices.TabIndex = 8;
			this.sbEditServices.UseVisualStyleBackColor = true;
			// 
			// sbMoveServiceUp
			// 
			this.alMain.SetAction(this.sbMoveServiceUp, this.aMoveServiceUp);
			this.sbMoveServiceUp.ImageIndex = 3;
			this.sbMoveServiceUp.ImageList = this.ilMain;
			this.sbMoveServiceUp.Location = new System.Drawing.Point(3, 74);
			this.sbMoveServiceUp.Name = "sbMoveServiceUp";
			this.sbMoveServiceUp.Selectable = false;
			this.sbMoveServiceUp.Size = new System.Drawing.Size(20, 20);
			this.sbMoveServiceUp.TabIndex = 7;
			this.sbMoveServiceUp.UseVisualStyleBackColor = true;
			// 
			// sbMoveServiceDown
			// 
			this.alMain.SetAction(this.sbMoveServiceDown, this.aMoveServiceDown);
			this.sbMoveServiceDown.ImageIndex = 4;
			this.sbMoveServiceDown.ImageList = this.ilMain;
			this.sbMoveServiceDown.Location = new System.Drawing.Point(3, 100);
			this.sbMoveServiceDown.Name = "sbMoveServiceDown";
			this.sbMoveServiceDown.Selectable = false;
			this.sbMoveServiceDown.Size = new System.Drawing.Size(20, 20);
			this.sbMoveServiceDown.TabIndex = 6;
			this.sbMoveServiceDown.UseVisualStyleBackColor = true;
			// 
			// tpDefaultOptions
			// 
			this.tpDefaultOptions.Controls.Add(this.cbIncludeMonolingualDictionaryInTranslation);
			this.tpDefaultOptions.Location = new System.Drawing.Point(4, 22);
			this.tpDefaultOptions.Name = "tpDefaultOptions";
			this.tpDefaultOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tpDefaultOptions.Size = new System.Drawing.Size(391, 130);
			this.tpDefaultOptions.TabIndex = 2;
			this.tpDefaultOptions.Text = "Options";
			this.tpDefaultOptions.UseVisualStyleBackColor = true;
			// 
			// cbIncludeMonolingualDictionaryInTranslation
			// 
			this.cbIncludeMonolingualDictionaryInTranslation.Location = new System.Drawing.Point(6, 6);
			this.cbIncludeMonolingualDictionaryInTranslation.Name = "cbIncludeMonolingualDictionaryInTranslation";
			this.cbIncludeMonolingualDictionaryInTranslation.Size = new System.Drawing.Size(353, 24);
			this.cbIncludeMonolingualDictionaryInTranslation.TabIndex = 3;
			this.cbIncludeMonolingualDictionaryInTranslation.Text = "Include monolingual dictionaries in translation";
			this.cbIncludeMonolingualDictionaryInTranslation.UseVisualStyleBackColor = true;
			this.cbIncludeMonolingualDictionaryInTranslation.CheckedChanged += new System.EventHandler(this.CbIncludeMonolingualDictionaryInTranslationCheckedChanged);
			// 
			// alMain
			// 
			this.alMain.Actions.Add(this.aAddProfile);
			this.alMain.Actions.Add(this.aRemoveProfile);
			this.alMain.Actions.Add(this.aMoveProfileUp);
			this.alMain.Actions.Add(this.aMoveProfileDown);
			this.alMain.Actions.Add(this.aEditServices);
			this.alMain.Actions.Add(this.aRemoveService);
			this.alMain.Actions.Add(this.aMoveServiceUp);
			this.alMain.Actions.Add(this.aMoveServiceDown);
			this.alMain.ImageList = this.ilMain;
			this.alMain.LockAllExecute = false;
			this.alMain.Tag = null;
			// 
			// aRemoveProfile
			// 
			this.aRemoveProfile.Checked = false;
			this.aRemoveProfile.Enabled = true;
			this.aRemoveProfile.Hint = "Remove user profile";
			this.aRemoveProfile.ImageIndex = 1;
			this.aRemoveProfile.Tag = null;
			this.aRemoveProfile.Text = "action1";
			this.aRemoveProfile.Visible = true;
			this.aRemoveProfile.Execute += new System.EventHandler(this.ARemoveProfileExecute);
			this.aRemoveProfile.Update += new System.EventHandler(this.ARemoveProfileUpdate);
			// 
			// aAddProfile
			// 
			this.aAddProfile.Checked = false;
			this.aAddProfile.Enabled = true;
			this.aAddProfile.Hint = "Add new user profile ";
			this.aAddProfile.ImageIndex = 0;
			this.aAddProfile.Tag = null;
			this.aAddProfile.Text = "Add new prodile";
			this.aAddProfile.Visible = true;
			this.aAddProfile.Execute += new System.EventHandler(this.AAddProfileExecute);
			// 
			// aMoveProfileUp
			// 
			this.aMoveProfileUp.Checked = false;
			this.aMoveProfileUp.Enabled = true;
			this.aMoveProfileUp.Hint = "Move profile up";
			this.aMoveProfileUp.ImageIndex = 3;
			this.aMoveProfileUp.Tag = null;
			this.aMoveProfileUp.Text = "action1";
			this.aMoveProfileUp.Visible = true;
			this.aMoveProfileUp.Execute += new System.EventHandler(this.AMoveProfileUpExecute);
			this.aMoveProfileUp.Update += new System.EventHandler(this.AMoveProfileUpUpdate);
			// 
			// aMoveProfileDown
			// 
			this.aMoveProfileDown.Checked = false;
			this.aMoveProfileDown.Enabled = true;
			this.aMoveProfileDown.Hint = "Move profile down";
			this.aMoveProfileDown.ImageIndex = 4;
			this.aMoveProfileDown.Tag = null;
			this.aMoveProfileDown.Text = "action1";
			this.aMoveProfileDown.Visible = true;
			this.aMoveProfileDown.Execute += new System.EventHandler(this.AMoveProfileDownExecute);
			this.aMoveProfileDown.Update += new System.EventHandler(this.AMoveProfileDownUpdate);
			// 
			// aRemoveService
			// 
			this.aRemoveService.Checked = false;
			this.aRemoveService.Enabled = true;
			this.aRemoveService.Hint = "Remove service";
			this.aRemoveService.ImageIndex = 1;
			this.aRemoveService.Tag = null;
			this.aRemoveService.Text = "Remove service";
			this.aRemoveService.Visible = true;
			this.aRemoveService.Execute += new System.EventHandler(this.ARemoveServiceExecute);
			this.aRemoveService.Update += new System.EventHandler(this.ARemoveServiceUpdate);
			// 
			// aEditServices
			// 
			this.aEditServices.Checked = false;
			this.aEditServices.Enabled = true;
			this.aEditServices.Hint = "Edit services";
			this.aEditServices.ImageIndex = 2;
			this.aEditServices.Tag = null;
			this.aEditServices.Text = "Edit services";
			this.aEditServices.Visible = true;
			this.aEditServices.Execute += new System.EventHandler(this.AEditServicesExecute);
			// 
			// aMoveServiceUp
			// 
			this.aMoveServiceUp.Checked = false;
			this.aMoveServiceUp.Enabled = true;
			this.aMoveServiceUp.Hint = "Move service up";
			this.aMoveServiceUp.ImageIndex = 3;
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
			this.aMoveServiceDown.ImageIndex = 4;
			this.aMoveServiceDown.Tag = null;
			this.aMoveServiceDown.Text = "action1";
			this.aMoveServiceDown.Visible = true;
			this.aMoveServiceDown.Execute += new System.EventHandler(this.AMoveServiceDownExecute);
			this.aMoveServiceDown.Update += new System.EventHandler(this.AMoveServiceDownUpdate);
			// 
			// CustomProfilesControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "CustomProfilesControl";
			this.Size = new System.Drawing.Size(419, 398);
			this.pBody.ResumeLayout(false);
			this.pProfiles.ResumeLayout(false);
			this.pProfilesControl.ResumeLayout(false);
			this.tcOptions.ResumeLayout(false);
			this.tpOptions.ResumeLayout(false);
			this.tpServices.ResumeLayout(false);
			this.pServiceControl.ResumeLayout(false);
			this.tpDefaultOptions.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lSubject;
		private System.Windows.Forms.ComboBox cbSubject;
		private System.Windows.Forms.ColumnHeader chSubject;
		private FreeCL.UI.SpeedButton sbRemoveService;
		private FreeCL.UI.SpeedButton sbEditServices;
		private FreeCL.UI.SpeedButton sbMoveServiceUp;
		private FreeCL.UI.SpeedButton sbMoveServiceDown;
		private FreeCL.UI.Actions.Action aMoveServiceDown;
		private FreeCL.UI.Actions.Action aMoveServiceUp;
		private FreeCL.UI.Actions.Action aRemoveService;
		private FreeCL.UI.Actions.Action aEditServices;
		private System.Windows.Forms.Panel pServiceControl;
		private Translate.ServicesListView lvServices;
		private System.Windows.Forms.ColumnHeader chDirection;
		private System.Windows.Forms.ComboBox cbFrom;
		private System.Windows.Forms.ComboBox cbTo;
		private System.Windows.Forms.Label lLangPair;
		private System.Windows.Forms.Label lProfileName;
		private System.Windows.Forms.Button bChangeName;
		private System.Windows.Forms.Label lName;
		private System.Windows.Forms.CheckBox cbIncludeMonolingualDictionaryInTranslation;
		private System.Windows.Forms.ColumnHeader chName;
		private FreeCL.UI.Actions.Action aMoveProfileDown;
		private FreeCL.UI.Actions.Action aMoveProfileUp;
		private FreeCL.UI.Actions.Action aRemoveProfile;
		private FreeCL.UI.SpeedButton sbAddProfile;
		private System.Windows.Forms.ImageList ilMain;
		private FreeCL.UI.Actions.Action aAddProfile;
		private FreeCL.UI.Actions.ActionList alMain;
		private FreeCL.UI.SpeedButton speedButton2;
		private System.Windows.Forms.TabPage tpDefaultOptions;
		private System.Windows.Forms.TabPage tpServices;
		private System.Windows.Forms.TabPage tpOptions;
		private System.Windows.Forms.Panel pProfiles;
		private System.Windows.Forms.TabControl tcOptions;
		private System.Windows.Forms.ListView lvProfiles;
		private FreeCL.UI.SpeedButton sbServiceDown;
		private FreeCL.UI.SpeedButton sbServiceUp;
		private System.Windows.Forms.Panel pProfilesControl;
	}
}
