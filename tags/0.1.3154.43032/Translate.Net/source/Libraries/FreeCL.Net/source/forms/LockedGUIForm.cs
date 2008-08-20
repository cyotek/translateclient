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
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
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
 

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.Forms
{
	
	public delegate void LockCallback(
		 object state
	);
	
	/// <summary>
	/// Description of LockedGUIForm.
	/// </summary>
	public class LockedGuiForm : FreeCL.Forms.BaseForm
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lCaption;
		private FreeCL.UI.Panel pAll;
		private System.Windows.Forms.Timer tCallback;
		public LockedGuiForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		[SuppressMessage("Microsoft.Mobility", "CA1601:DoNotUseTimersThatPreventPowerStateChanges")]
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.tCallback = new System.Windows.Forms.Timer(this.components);
			this.pAll = new FreeCL.UI.Panel();
			this.lCaption = new System.Windows.Forms.Label();
			this.pAll.SuspendLayout();
			this.SuspendLayout();
			// 
			// tCallback
			// 
			this.tCallback.Interval = 10;
			this.tCallback.Tick += new System.EventHandler(this.TCallbackTick);
			// 
			// pAll
			// 
			this.pAll.BevelInner = FreeCL.UI.BevelStyle.Lowered;
			this.pAll.Controls.Add(this.lCaption);
			this.pAll.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pAll.Location = new System.Drawing.Point(0, 0);
			this.pAll.Name = "pAll";
			this.pAll.Padding = new System.Windows.Forms.Padding(4);
			this.pAll.Size = new System.Drawing.Size(339, 100);
			this.pAll.TabIndex = 0;
			// 
			// lCaption
			// 
			this.lCaption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lCaption.Location = new System.Drawing.Point(4, 4);
			this.lCaption.Name = "lCaption";
			this.lCaption.Size = new System.Drawing.Size(331, 92);
			this.lCaption.TabIndex = 1;
			this.lCaption.Text = "Please wait";
			this.lCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lCaption.UseCompatibleTextRendering = true;
			// 
			// LockedGUIForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(339, 100);
			this.ControlBox = false;
			this.Controls.Add(this.pAll);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LockedGUIForm";
			this.Opacity = 0.9;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "LockedGUIForm";
			this.pAll.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion
		
		public DialogResult ShowDialog(IWin32Window owner, string caption)
		{
			lCaption.Text = caption;
			return base.ShowDialog(owner);
		}
		
		public DialogResult ShowDialog(string caption)
		{
			lCaption.Text = caption;
			return base.ShowDialog(FreeCL.UI.Application.MainForm);
		}

		public void StartShowDialog()
		{
			Trace.WriteLine("StartWaiting");			
			lCaption.Text = waitMessage;
			Cursor.Current = Cursors.WaitCursor;
			Cursor = Cursors.WaitCursor;
			FreeCL.UI.Application.MainForm.Cursor = Cursors.WaitCursor;
			base.ShowDialog(FreeCL.UI.Application.MainForm);
		}
		
		string waitMessage;		
		public void StartWaiting(string message)
		{
			waitMessage = message;
			BeginInvoke(new MethodInvoker(StartShowDialog));	
		}

		public void StopShowDialog()
		{
			Trace.WriteLine("StopWaiting");									
			Cursor.Current = Cursors.Default;
			Cursor = Cursors.Default;
			FreeCL.UI.Application.MainForm.Cursor = Cursors.Default;
			DialogResult = DialogResult.OK;
		}
		
		public void StopWaiting()
		{
			waitMessage = "";		
			BeginInvoke(new MethodInvoker(StopShowDialog));				
		}
		
		public void Init()
		{
			if(!IsHandleCreated)
				base.CreateHandle();
		}
		
		static public void SyncStart(string message, LockCallback callback, object state)
		{
			if(SyncLockedGUIForm == null)
			{
				SyncLockedGUIForm = new FreeCL.Forms.LockedGuiForm();
				SyncLockedGUIForm.Init();
			}
			SyncLockedGUIForm.savedCallBack = callback;
			SyncLockedGUIForm.savedState = state;
			SyncLockedGUIForm.tCallback.Enabled = true;	
			
			SyncLockedGUIForm.StartWaiting(message);
	
			Application.DoEvents();
		}

		static FreeCL.Forms.LockedGuiForm SyncLockedGUIForm;
		LockCallback savedCallBack;
		object savedState;
		
		void TCallbackTick(object sender, System.EventArgs e)
		{
			tCallback.Enabled = false; 
			Application.DoEvents();			
			
			try
			{
				if(SyncLockedGUIForm.savedCallBack != null)
					SyncLockedGUIForm.savedCallBack(savedState);
			}	
			finally
			{
				SyncLockedGUIForm.StopWaiting();
				SyncLockedGUIForm = null;
			}

		}
		
	}
}
