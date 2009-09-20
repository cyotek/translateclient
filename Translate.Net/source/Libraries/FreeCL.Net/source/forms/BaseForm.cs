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
using FreeCL.UI;
using FreeCL.RTL;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Security.Permissions;

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of BaseForm.	
	/// </summary>
	public class BaseForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public System.Windows.Forms.ImageList il;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected FreeCL.UI.Actions.ActionList al;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public FreeCL.UI.GlobalEvents globalEvents;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId="toolTip")]
		public System.Windows.Forms.ToolTip toolTip;
		
		private System.Drawing.Icon StartIcon_;

		public BaseForm()
		{
			StartIcon_ = Icon; 
			InitializeComponent();
			options = FreeCL.Forms.Application.BaseOptions;
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.globalEvents = new FreeCL.UI.GlobalEvents(this.components);
			this.al = new FreeCL.UI.Actions.ActionList(this.components);
			this.il = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// toolTip
			// 
			this.toolTip.ShowAlways = true;
			// 
			// al
			// 
			this.al.ImageList = this.il;
			this.al.LockAllExecute = false;
			this.al.Tag = null;
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.il.ImageSize = new System.Drawing.Size(16, 16);
			this.il.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// BaseForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(286, 259);
			this.DoubleBuffered = true;
			this.Name = "BaseForm";
			this.Text = "BaseForm";
			this.VisibleChanged += new System.EventHandler(this.BaseMainFormVisibleChanged);
			this.ResumeLayout(false);
		}
		#endregion
		
		void BaseMainFormVisibleChanged(object sender, System.EventArgs e)
		{
			if(Disposing) return;
			
 			if(Icon != StartIcon_)
				return;
				
			System.Windows.Forms.Form activeForm = FreeCL.UI.Application.MainForm;
			if(activeForm == null)
				return;
			
			this.Icon = activeForm.Icon;

		}
		
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
		{
			if(!DesignMode && al.ProcessKey(keyData))
				return true;
			else
				return base.ProcessCmdKey(ref msg, keyData);
		}		 
		
		
		protected override void Dispose( bool disposing )
		{
			if( disposing && !DesignMode)
			{
				try //monobug
				{
					Enabled = false;
				}
				catch{}
				
					
				Memory.Dispose(components);																			
				Memory.DisposeChilds(this);								
				Memory.DisposeAndNull(ref components);				
			}
			base.Dispose( disposing );
		}
		
		BaseOptions options = null;
		public BaseOptions Options
		{
			get{return options;}
		}
		
		public FreeCL.UI.Actions.ActionList	ActionList
		{
			get 
			{
				return al;
			}
		}
		
		int updateLockCount;
		public bool IsUpdateLocked
		{
			get
			{
				return (this.updateLockCount > 0);
			}
		}
		
		static class NativeMethods
		{
			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public  static extern bool LockWindowUpdate(IntPtr hWndLock);
			
			//TODO: implement for unix !
		}
		
		public void LockUpdate(bool lockIt)
		{
			if(lockIt && updateLockCount == 0)
			{
				Cursor = Cursors.WaitCursor;
				SuspendFormLayout();
				Application.DoEvents();
				if(!MonoHelper.IsUnix)
				{
					NativeMethods.LockWindowUpdate(Handle);
				}	
			}	
			else if(!lockIt && updateLockCount == 1)	
			{
				Application.DoEvents();
				ResumeFormLayout();
				if(!MonoHelper.IsUnix)
					NativeMethods.LockWindowUpdate(IntPtr.Zero);
				Cursor = Cursors.Default;
				Refresh();
			}	
			else if(!lockIt && updateLockCount == 0)
				throw new ArgumentException("Unlocks count greater of locks count", "lockIt");
			
			if(lockIt)
				updateLockCount++;
			else
				updateLockCount--;
		}
	
		int layoutSuspendCount;
		
		public bool IsLayoutSuspended
		{
			get
			{
				return (this.layoutSuspendCount > 0);
			}
		}
		
		public void SuspendFormLayout()
		{
			SuspendLayout();
			layoutSuspendCount++;
		}

		public void ResumeFormLayout()
		{
			ResumeLayout();
			layoutSuspendCount--;
		}
		
		public static string TranslateString(string data)
		{
			return LangPack.TranslateString(data);
		}
	
		public static void RegisterLanguageEvent(OnLanguageChangedEventHandler onLanguageChangedEventHandler)
		{
			LangPack.RegisterLanguageEvent(onLanguageChangedEventHandler);
			if(onLanguageChangedEventHandler != null)
				onLanguageChangedEventHandler.Invoke();
		}
	
	}
}
