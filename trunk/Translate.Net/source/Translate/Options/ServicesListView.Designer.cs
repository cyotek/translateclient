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

namespace Translate.Options
{
	partial class ServicesListView
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
			this.lvMain = new System.Windows.Forms.ListView();
			this.chName = new System.Windows.Forms.ColumnHeader();
			this.chType = new System.Windows.Forms.ColumnHeader();
			this.chLanguagePair = new System.Windows.Forms.ColumnHeader();
			this.chSubject = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lvMain
			// 
			this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.chName,
									this.chType,
									this.chLanguagePair,
									this.chSubject});
			this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMain.FullRowSelect = true;
			this.lvMain.GridLines = true;
			this.lvMain.HideSelection = false;
			this.lvMain.Location = new System.Drawing.Point(0, 0);
			this.lvMain.MultiSelect = false;
			this.lvMain.Name = "lvMain";
			this.lvMain.ShowItemToolTips = true;
			this.lvMain.Size = new System.Drawing.Size(150, 150);
			this.lvMain.TabIndex = 0;
			this.lvMain.UseCompatibleStateImageBehavior = false;
			this.lvMain.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "Name";
			// 
			// chType
			// 
			this.chType.Text = "Type";
			// 
			// chLanguagePair
			// 
			this.chLanguagePair.Text = "Languages";
			// 
			// chSubject
			// 
			this.chSubject.Text = "Subject";
			// 
			// ServicesListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lvMain);
			this.Name = "ServicesListView";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ColumnHeader chSubject;
		private System.Windows.Forms.ColumnHeader chLanguagePair;
		private System.Windows.Forms.ColumnHeader chType;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ListView lvMain;
	}
}
