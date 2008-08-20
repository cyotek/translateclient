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
using System.Windows.Forms;
using System.Diagnostics;
using FreeCL.UI;
using FreeCL.RTL;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace FreeCL.Forms
{
	using Clipboard = FreeCL.UI.Clipboard;
	
	/// <summary>
	/// Description of BaseMainForm.	
	/// </summary>
	public class BaseMainForm : FreeCL.Forms.BaseGuiForm
	{
	
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public System.Windows.Forms.StatusStrip sbMain;

		public BaseMainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			pToolBars.Dock = DockStyle.Top;
			
			instance = this;			
			
			 
		}
		
		static BaseMainForm instance;
		public static BaseMainForm Instance {
			get { return instance; }
		}
		

		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent() {
			this.sbMain = new System.Windows.Forms.StatusStrip();
			this.SuspendLayout();
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			// 
			// sbMain
			// 
			this.sbMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.sbMain.Location = new System.Drawing.Point(0, 527);
			this.sbMain.Name = "sbMain";
			this.sbMain.Size = new System.Drawing.Size(550, 22);
			this.sbMain.TabIndex = 3;
			this.sbMain.Text = "statusStrip1";
			// 
			// BaseMainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(550, 549);
			this.Controls.Add(this.sbMain);
			this.Name = "BaseMainForm";
			this.Text = "BaseMainForm";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
		private void SyncLockCallback(object state)
		{
			LockCallbackData lcd = state as LockCallbackData;
			lcd.Callback(lcd.State);
		}
		
		private class LockCallbackData
		{
			public LockCallback Callback;
			public object State;
			
			public LockCallbackData(LockCallback callBack, object state)
			{
				Callback = callBack;
				State = state;
			}
		}
		
		public void SyncLock(string message, LockCallback callback, object state)
		{
			if(callback == null)
				throw new ArgumentNullException("callback");
		
			locked = true;
			if(FreeCL.UI.Application.MainForm != null)
			{
				FreeCL.Forms.LockedGuiForm.SyncStart(
					message, 
					new FreeCL.Forms.LockCallback(SyncLockCallback),
					new LockCallbackData(callback, state));
			}
			else
			{
				callback(state);
			}
			locked = false;
		}
		
		bool locked;
		public bool Locked
		{
			get{return locked;}
		}
		
		
		public void BringToForeground()
		{
			WindowHelper.BringToForeground(Handle);
		}
		
	}
}
