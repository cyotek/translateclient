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
 
 using System.Diagnostics.CodeAnalysis;
 
namespace Translate
{
	partial class LanguageSelector
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
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageSelector));
			this.pBottom = new FreeCL.UI.Panel();
			this.lbHistory = new System.Windows.Forms.ListBox();
			this.splitterBottom = new System.Windows.Forms.Splitter();
			this.tcMain = new FreeCL.UI.TabControl();
			this.tpLangs = new System.Windows.Forms.TabPage();
			this.pMain = new FreeCL.UI.Panel();
			this.pTo = new FreeCL.UI.Panel();
			this.lbTo = new System.Windows.Forms.ListBox();
			this.pFrom = new FreeCL.UI.Panel();
			this.lbFrom = new System.Windows.Forms.ListBox();
			this.pColumns = new FreeCL.UI.Panel();
			this.lTo = new System.Windows.Forms.Label();
			this.sbInvert = new FreeCL.UI.SpeedButton();
			this.lFrom = new System.Windows.Forms.Label();
			this.tpSubject = new System.Windows.Forms.TabPage();
			this.lbSubjects = new System.Windows.Forms.CheckedListBox();
			this.pBottom.SuspendLayout();
			this.tcMain.SuspendLayout();
			this.tpLangs.SuspendLayout();
			this.pMain.SuspendLayout();
			this.pTo.SuspendLayout();
			this.pFrom.SuspendLayout();
			this.pColumns.SuspendLayout();
			this.tpSubject.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBottom
			// 
			this.pBottom.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.pBottom.Controls.Add(this.lbHistory);
			this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBottom.Location = new System.Drawing.Point(2, 276);
			this.pBottom.Name = "pBottom";
			this.pBottom.Size = new System.Drawing.Size(198, 61);
			this.pBottom.TabIndex = 3;
			// 
			// lbHistory
			// 
			this.lbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbHistory.FormattingEnabled = true;
			this.lbHistory.Location = new System.Drawing.Point(0, 0);
			this.lbHistory.Name = "lbHistory";
			this.lbHistory.Size = new System.Drawing.Size(198, 56);
			this.lbHistory.TabIndex = 4;
			this.lbHistory.SelectedIndexChanged += new System.EventHandler(this.LbHistorySelectedIndexChanged);
			// 
			// splitterBottom
			// 
			this.splitterBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterBottom.Location = new System.Drawing.Point(2, 271);
			this.splitterBottom.Name = "splitterBottom";
			this.splitterBottom.Size = new System.Drawing.Size(198, 5);
			this.splitterBottom.TabIndex = 4;
			this.splitterBottom.TabStop = false;
			// 
			// tcMain
			// 
			this.tcMain.Controls.Add(this.tpLangs);
			this.tcMain.Controls.Add(this.tpSubject);
			this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcMain.Location = new System.Drawing.Point(2, 2);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(198, 269);
			this.tcMain.TabIndex = 7;
			// 
			// tpLangs
			// 
			this.tpLangs.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tpLangs.Controls.Add(this.pMain);
			this.tpLangs.Controls.Add(this.pColumns);
			this.tpLangs.Location = new System.Drawing.Point(4, 22);
			this.tpLangs.Name = "tpLangs";
			this.tpLangs.Padding = new System.Windows.Forms.Padding(3);
			this.tpLangs.Size = new System.Drawing.Size(190, 243);
			this.tpLangs.TabIndex = 0;
			this.tpLangs.Text = "Languages";
			// 
			// pMain
			// 
			this.pMain.BevelInner = FreeCL.UI.BevelStyle.Lowered;
			this.pMain.Controls.Add(this.pTo);
			this.pMain.Controls.Add(this.pFrom);
			this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pMain.Location = new System.Drawing.Point(3, 59);
			this.pMain.Name = "pMain";
			this.pMain.Padding = new System.Windows.Forms.Padding(2);
			this.pMain.Size = new System.Drawing.Size(184, 181);
			this.pMain.TabIndex = 8;
			this.pMain.SizeChanged += new System.EventHandler(this.PMainResize);
			// 
			// pTo
			// 
			this.pTo.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.pTo.Controls.Add(this.lbTo);
			this.pTo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pTo.Location = new System.Drawing.Point(92, 2);
			this.pTo.Name = "pTo";
			this.pTo.Size = new System.Drawing.Size(90, 177);
			this.pTo.TabIndex = 5;
			// 
			// lbTo
			// 
			this.lbTo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbTo.FormattingEnabled = true;
			this.lbTo.HorizontalScrollbar = true;
			this.lbTo.Location = new System.Drawing.Point(0, 0);
			this.lbTo.Name = "lbTo";
			this.lbTo.Size = new System.Drawing.Size(90, 173);
			this.lbTo.Sorted = true;
			this.lbTo.TabIndex = 2;
			this.lbTo.SelectedIndexChanged += new System.EventHandler(this.LbToSelectedIndexChanged);
			// 
			// pFrom
			// 
			this.pFrom.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.pFrom.Controls.Add(this.lbFrom);
			this.pFrom.Dock = System.Windows.Forms.DockStyle.Left;
			this.pFrom.Location = new System.Drawing.Point(2, 2);
			this.pFrom.Name = "pFrom";
			this.pFrom.Size = new System.Drawing.Size(90, 177);
			this.pFrom.TabIndex = 4;
			// 
			// lbFrom
			// 
			this.lbFrom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbFrom.FormattingEnabled = true;
			this.lbFrom.HorizontalScrollbar = true;
			this.lbFrom.Location = new System.Drawing.Point(0, 0);
			this.lbFrom.Name = "lbFrom";
			this.lbFrom.Size = new System.Drawing.Size(90, 173);
			this.lbFrom.Sorted = true;
			this.lbFrom.TabIndex = 1;
			this.lbFrom.SelectedIndexChanged += new System.EventHandler(this.LbFromSelectedIndexChanged);
			// 
			// pColumns
			// 
			this.pColumns.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.pColumns.Controls.Add(this.lTo);
			this.pColumns.Controls.Add(this.sbInvert);
			this.pColumns.Controls.Add(this.lFrom);
			this.pColumns.Dock = System.Windows.Forms.DockStyle.Top;
			this.pColumns.Location = new System.Drawing.Point(3, 3);
			this.pColumns.Name = "pColumns";
			this.pColumns.Size = new System.Drawing.Size(184, 56);
			this.pColumns.TabIndex = 7;
			this.pColumns.SizeChanged += new System.EventHandler(this.PColumnsResize);
			// 
			// lTo
			// 
			this.lTo.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.lTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lTo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lTo.Location = new System.Drawing.Point(106, 0);
			this.lTo.Name = "lTo";
			this.lTo.Size = new System.Drawing.Size(78, 56);
			this.lTo.TabIndex = 2;
			this.lTo.Text = "label1";
			this.lTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// sbInvert
			// 
			this.sbInvert.Dock = System.Windows.Forms.DockStyle.Left;
			this.sbInvert.Image = ((System.Drawing.Image)(resources.GetObject("sbInvert.Image")));
			this.sbInvert.Location = new System.Drawing.Point(82, 0);
			this.sbInvert.Name = "sbInvert";
			this.sbInvert.Selectable = false;
			this.sbInvert.Size = new System.Drawing.Size(24, 56);
			this.sbInvert.TabIndex = 3;
			this.sbInvert.TabStop = false;
			this.sbInvert.UseVisualStyleBackColor = true;
			// 
			// lFrom
			// 
			this.lFrom.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.lFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lFrom.Dock = System.Windows.Forms.DockStyle.Left;
			this.lFrom.Location = new System.Drawing.Point(0, 0);
			this.lFrom.Name = "lFrom";
			this.lFrom.Size = new System.Drawing.Size(82, 56);
			this.lFrom.TabIndex = 1;
			this.lFrom.Text = "label1";
			this.lFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tpSubject
			// 
			this.tpSubject.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tpSubject.Controls.Add(this.lbSubjects);
			this.tpSubject.Location = new System.Drawing.Point(4, 22);
			this.tpSubject.Name = "tpSubject";
			this.tpSubject.Padding = new System.Windows.Forms.Padding(3);
			this.tpSubject.Size = new System.Drawing.Size(190, 243);
			this.tpSubject.TabIndex = 1;
			this.tpSubject.Text = "Subjects";
			// 
			// lbSubjects
			// 
			this.lbSubjects.CheckOnClick = true;
			this.lbSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbSubjects.FormattingEnabled = true;
			this.lbSubjects.Location = new System.Drawing.Point(3, 3);
			this.lbSubjects.Name = "lbSubjects";
			this.lbSubjects.Size = new System.Drawing.Size(184, 229);
			this.lbSubjects.Sorted = true;
			this.lbSubjects.TabIndex = 0;
			this.lbSubjects.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LbSubjectsItemCheck);
			// 
			// LanguageSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tcMain);
			this.Controls.Add(this.splitterBottom);
			this.Controls.Add(this.pBottom);
			this.Name = "LanguageSelector";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.Size = new System.Drawing.Size(202, 339);
			this.pBottom.ResumeLayout(false);
			this.tcMain.ResumeLayout(false);
			this.tpLangs.ResumeLayout(false);
			this.pMain.ResumeLayout(false);
			this.pTo.ResumeLayout(false);
			this.pFrom.ResumeLayout(false);
			this.pColumns.ResumeLayout(false);
			this.tpSubject.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckedListBox lbSubjects;
		private System.Windows.Forms.TabPage tpSubject;
		private System.Windows.Forms.TabPage tpLangs;
		private FreeCL.UI.TabControl tcMain;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public FreeCL.UI.SpeedButton sbInvert;
		private FreeCL.UI.Panel pColumns;
		private System.Windows.Forms.Label lFrom;
		private System.Windows.Forms.ListBox lbFrom;
		private System.Windows.Forms.Label lTo;
		private System.Windows.Forms.ListBox lbTo;
		private System.Windows.Forms.ListBox lbHistory;
		private System.Windows.Forms.Splitter splitterBottom;
		private FreeCL.UI.Panel pFrom;
		private FreeCL.UI.Panel pTo;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public FreeCL.UI.Panel pBottom;
		private FreeCL.UI.Panel pMain;
	}
}
