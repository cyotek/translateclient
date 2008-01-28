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
	partial class UpdaterForm
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
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		[SuppressMessage("Microsoft.Mobility", "CA1601:DoNotUseTimersThatPreventPowerStateChanges")]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.bCancel = new System.Windows.Forms.Button();
			this.bMinimize = new System.Windows.Forms.Button();
			this.lStatus = new System.Windows.Forms.TextBox();
			this.pbMain = new System.Windows.Forms.ProgressBar();
			this.timerUpdate = new System.Windows.Forms.Timer(this.components);
			this.timerClose = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// bCancel
			// 
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(339, 89);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(103, 23);
			this.bCancel.TabIndex = 0;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			this.bCancel.Click += new System.EventHandler(this.BCancelClick);
			// 
			// bMinimize
			// 
			this.bMinimize.Location = new System.Drawing.Point(230, 89);
			this.bMinimize.Name = "bMinimize";
			this.bMinimize.Size = new System.Drawing.Size(103, 23);
			this.bMinimize.TabIndex = 1;
			this.bMinimize.Text = "Minimize";
			this.bMinimize.UseVisualStyleBackColor = true;
			this.bMinimize.Click += new System.EventHandler(this.BMinimizeClick);
			// 
			// lStatus
			// 
			this.lStatus.BackColor = System.Drawing.SystemColors.Window;
			this.lStatus.Location = new System.Drawing.Point(12, 9);
			this.lStatus.Multiline = true;
			this.lStatus.Name = "lStatus";
			this.lStatus.ReadOnly = true;
			this.lStatus.Size = new System.Drawing.Size(425, 54);
			this.lStatus.TabIndex = 2;
			// 
			// pbMain
			// 
			this.pbMain.Location = new System.Drawing.Point(12, 66);
			this.pbMain.Name = "pbMain";
			this.pbMain.Size = new System.Drawing.Size(425, 17);
			this.pbMain.Step = 1;
			this.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.pbMain.TabIndex = 3;
			// 
			// timerUpdate
			// 
			this.timerUpdate.Enabled = true;
			this.timerUpdate.Interval = 200;
			this.timerUpdate.Tick += new System.EventHandler(this.TimerUpdateTick);
			// 
			// timerClose
			// 
			this.timerClose.Interval = 1000;
			this.timerClose.Tick += new System.EventHandler(this.TimerCloseTick);
			// 
			// UpdaterForm
			// 
			this.AcceptButton = this.bMinimize;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(449, 117);
			this.Controls.Add(this.pbMain);
			this.Controls.Add(this.lStatus);
			this.Controls.Add(this.bMinimize);
			this.Controls.Add(this.bCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "UpdaterForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Update Translate.Net";
			this.VisibleChanged += new System.EventHandler(this.UpdaterFormVisibleChanged);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdaterFormFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Timer timerClose;
		private System.Windows.Forms.Timer timerUpdate;
		private System.Windows.Forms.ProgressBar pbMain;
		private System.Windows.Forms.TextBox lStatus;
		private System.Windows.Forms.Button bMinimize;
		private System.Windows.Forms.Button bCancel;
	}
}
