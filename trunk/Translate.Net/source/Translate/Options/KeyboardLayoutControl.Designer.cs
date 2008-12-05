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
	partial class KeyboardLayoutControl
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
			this.cbSwitchLayoutBasedOnLanguage = new System.Windows.Forms.CheckBox();
			this.gbLayouts = new System.Windows.Forms.GroupBox();
			this.dgvLayouts = new System.Windows.Forms.DataGridView();
			this.dsMain = new System.Data.DataSet();
			this.dtLangToLayouts = new System.Data.DataTable();
			this.dcolLanguage = new System.Data.DataColumn();
			this.dcolLayoutName2 = new System.Data.DataColumn();
			this.dcolMode = new System.Data.DataColumn();
			this.dcolLanguageDisplayName = new System.Data.DataColumn();
			this.dcolAutoLayout = new System.Data.DataColumn();
			this.dcolSavedLayout = new System.Data.DataColumn();
			this.dcolChanged = new System.Data.DataColumn();
			this.dtLayouts = new System.Data.DataTable();
			this.dcolLayoutName = new System.Data.DataColumn();
			this.dcolLayoutDisplayName = new System.Data.DataColumn();
			this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LayoutId = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Auto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Changed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.pBody.SuspendLayout();
			this.gbLayouts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvLayouts)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtLangToLayouts)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtLayouts)).BeginInit();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.gbLayouts);
			this.pBody.Controls.Add(this.cbSwitchLayoutBasedOnLanguage);
			this.pBody.Size = new System.Drawing.Size(399, 322);
			// 
			// cbSwitchLayoutBasedOnLanguage
			// 
			this.cbSwitchLayoutBasedOnLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cbSwitchLayoutBasedOnLanguage.Location = new System.Drawing.Point(8, 13);
			this.cbSwitchLayoutBasedOnLanguage.Name = "cbSwitchLayoutBasedOnLanguage";
			this.cbSwitchLayoutBasedOnLanguage.Size = new System.Drawing.Size(378, 24);
			this.cbSwitchLayoutBasedOnLanguage.TabIndex = 4;
			this.cbSwitchLayoutBasedOnLanguage.Text = "Switch keyboard layout based on selected language";
			this.cbSwitchLayoutBasedOnLanguage.UseVisualStyleBackColor = true;
			this.cbSwitchLayoutBasedOnLanguage.CheckedChanged += new System.EventHandler(this.CbSwitchLayoutBasedOnLanguageCheckedChanged);
			// 
			// gbLayouts
			// 
			this.gbLayouts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gbLayouts.Controls.Add(this.dgvLayouts);
			this.gbLayouts.Location = new System.Drawing.Point(8, 43);
			this.gbLayouts.Name = "gbLayouts";
			this.gbLayouts.Size = new System.Drawing.Size(383, 266);
			this.gbLayouts.TabIndex = 6;
			this.gbLayouts.TabStop = false;
			this.gbLayouts.Text = "Selected language to layout name mapping";
			// 
			// dgvLayouts
			// 
			this.dgvLayouts.AllowUserToAddRows = false;
			this.dgvLayouts.AllowUserToDeleteRows = false;
			this.dgvLayouts.AllowUserToResizeRows = false;
			this.dgvLayouts.AutoGenerateColumns = false;
			this.dgvLayouts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			this.dgvLayouts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvLayouts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvLayouts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.languageDataGridViewTextBoxColumn,
									this.LayoutId,
									this.Auto,
									this.Changed});
			this.dgvLayouts.DataMember = "LangToLayouts";
			this.dgvLayouts.DataSource = this.dsMain;
			this.dgvLayouts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvLayouts.Location = new System.Drawing.Point(3, 16);
			this.dgvLayouts.Name = "dgvLayouts";
			this.dgvLayouts.RowHeadersVisible = false;
			this.dgvLayouts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dgvLayouts.RowTemplate.Height = 10;
			this.dgvLayouts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvLayouts.Size = new System.Drawing.Size(377, 247);
			this.dgvLayouts.TabIndex = 6;
			this.dgvLayouts.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLayoutsCellValueChanged);
			this.dgvLayouts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvLayoutsCellFormatting);
			this.dgvLayouts.CurrentCellDirtyStateChanged += new System.EventHandler(this.DgvLayoutsCurrentCellDirtyStateChanged);
			// 
			// dsMain
			// 
			this.dsMain.DataSetName = "NewDataSet";
			this.dsMain.Locale = new System.Globalization.CultureInfo("");
			this.dsMain.Tables.AddRange(new System.Data.DataTable[] {
									this.dtLangToLayouts,
									this.dtLayouts});
			// 
			// dtLangToLayouts
			// 
			this.dtLangToLayouts.Columns.AddRange(new System.Data.DataColumn[] {
									this.dcolLanguage,
									this.dcolLayoutName2,
									this.dcolMode,
									this.dcolLanguageDisplayName,
									this.dcolAutoLayout,
									this.dcolSavedLayout,
									this.dcolChanged});
			this.dtLangToLayouts.Constraints.AddRange(new System.Data.Constraint[] {
									new System.Data.UniqueConstraint("Constraint1", new string[] {
																		"Language"}, true)});
			this.dtLangToLayouts.PrimaryKey = new System.Data.DataColumn[] {
						this.dcolLanguage};
			this.dtLangToLayouts.TableName = "LangToLayouts";
			// 
			// dcolLanguage
			// 
			this.dcolLanguage.AllowDBNull = false;
			this.dcolLanguage.ColumnName = "Language";
			this.dcolLanguage.DataType = typeof(int);
			this.dcolLanguage.ReadOnly = true;
			// 
			// dcolLayoutName2
			// 
			this.dcolLayoutName2.ColumnName = "Layout";
			// 
			// dcolMode
			// 
			this.dcolMode.ColumnName = "Auto";
			this.dcolMode.DataType = typeof(bool);
			this.dcolMode.DefaultValue = true;
			// 
			// dcolLanguageDisplayName
			// 
			this.dcolLanguageDisplayName.ColumnName = "DisplayName";
			// 
			// dcolAutoLayout
			// 
			this.dcolAutoLayout.ColumnName = "AutoLayout";
			// 
			// dcolSavedLayout
			// 
			this.dcolSavedLayout.ColumnName = "SavedLayout";
			// 
			// dcolChanged
			// 
			this.dcolChanged.ColumnName = "Changed";
			this.dcolChanged.DataType = typeof(bool);
			// 
			// dtLayouts
			// 
			this.dtLayouts.Columns.AddRange(new System.Data.DataColumn[] {
									this.dcolLayoutName,
									this.dcolLayoutDisplayName});
			this.dtLayouts.TableName = "Layouts";
			// 
			// dcolLayoutName
			// 
			this.dcolLayoutName.AllowDBNull = false;
			this.dcolLayoutName.ColumnName = "Name";
			// 
			// dcolLayoutDisplayName
			// 
			this.dcolLayoutDisplayName.ColumnName = "DisplayName";
			// 
			// languageDataGridViewTextBoxColumn
			// 
			this.languageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.languageDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
			this.languageDataGridViewTextBoxColumn.HeaderText = "Language";
			this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
			this.languageDataGridViewTextBoxColumn.ReadOnly = true;
			this.languageDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.languageDataGridViewTextBoxColumn.Width = 110;
			// 
			// LayoutId
			// 
			this.LayoutId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.LayoutId.DataPropertyName = "Layout";
			this.LayoutId.DataSource = this.dsMain;
			this.LayoutId.DisplayMember = "Layouts.DisplayName";
			this.LayoutId.DropDownWidth = 300;
			this.LayoutId.HeaderText = "Layout";
			this.LayoutId.MinimumWidth = 150;
			this.LayoutId.Name = "LayoutId";
			this.LayoutId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.LayoutId.ValueMember = "Layouts.Name";
			this.LayoutId.Width = 158;
			// 
			// Auto
			// 
			this.Auto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Auto.DataPropertyName = "Auto";
			this.Auto.HeaderText = "Auto";
			this.Auto.Name = "Auto";
			this.Auto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Auto.Width = 35;
			// 
			// Changed
			// 
			this.Changed.DataPropertyName = "Changed";
			this.Changed.HeaderText = "Changed";
			this.Changed.Name = "Changed";
			this.Changed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Changed.Width = 58;
			// 
			// KeyboardLayoutControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "KeyboardLayoutControl";
			this.Size = new System.Drawing.Size(399, 362);
			this.pBody.ResumeLayout(false);
			this.gbLayouts.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvLayouts)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtLangToLayouts)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtLayouts)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Data.DataColumn dcolChanged;
		private System.Data.DataColumn dcolSavedLayout;
		private System.Data.DataColumn dcolAutoLayout;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Changed;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Auto;
		private System.Data.DataColumn dcolLanguageDisplayName;
		private System.Data.DataColumn dcolMode;
		private System.Data.DataColumn dcolLayoutDisplayName;
		private System.Data.DataColumn dcolLayoutName2;
		private System.Windows.Forms.DataGridViewComboBoxColumn LayoutId;
		private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
		private System.Data.DataColumn dcolLayoutName;
		private System.Data.DataTable dtLayouts;
		private System.Data.DataColumn dcolLanguage;
		private System.Data.DataTable dtLangToLayouts;
		private System.Data.DataSet dsMain;
		private System.Windows.Forms.GroupBox gbLayouts;
		private System.Windows.Forms.DataGridView dgvLayouts;
		private System.Windows.Forms.CheckBox cbSwitchLayoutBasedOnLanguage;
	}
}
