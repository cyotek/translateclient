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
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using FreeCL.UI;
using FreeCL.RTL;
using System.Security;

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of ExceptionDialog.	
	/// </summary>
	public class ExceptionDialog : FreeCL.Forms.BaseForm
	{
		private System.Windows.Forms.Label lClass;
		private System.Windows.Forms.Button bContinue;
		private System.Windows.Forms.Button bCopy;
		private System.Windows.Forms.Button bTerminate;
		private System.Windows.Forms.Label lMessage;
		private System.Windows.Forms.TextBox lInfo;
		private System.Windows.Forms.Button bSend;
		private System.Windows.Forms.PictureBox picApp;
		
		private System.Exception Current_;
		public ExceptionDialog(System.Exception exception, bool infoOnly)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Current_ = exception;
			
			InitInfo();
			
			if(infoOnly)
			{
				bTerminate.Enabled = false;
				bTerminate.Visible = false;
				bContinue.Enabled = false;
				bContinue.Visible = false;
				bClose.Visible = true;
				bClose.Enabled = true;
				bClose.Location = bTerminate.Location;
				bCopy.Location = bContinue.Location;
			}
			
			OnLanguageChanged();
		}
		
		void OnLanguageChanged()
		{
			bSend.Text = TranslateString("Send by mail");
			bContinue.Text = TranslateString("Continue");
			bCopy.Text = TranslateString("Copy");
			bClose.Text = TranslateString("Close");
			bTerminate.Text = TranslateString("Terminate");
		}
		
		
		private static string SingleExceptionText(System.Exception e)
		{
			StringBuilder exceptionText = new StringBuilder();			
			
			exceptionText.Append("Unhandled exception : " +e.GetType().FullName + "\r\n");
			exceptionText.Append("Message : " + e.Message + "\r\n");
			ExternalException externalException = e as ExternalException;
			if(externalException != null)
			{
				exceptionText.Append("ErrorCode : " + externalException.ErrorCode + "\r\n");
			}
			
			FileNotFoundException fileNotFoundException = e as FileNotFoundException;
			if(fileNotFoundException != null)
			{
				exceptionText.Append("Fusion Log \r\n" + fileNotFoundException.FusionLog + "\r\n");			
			}
			
			SecurityException securityException = e as SecurityException;
			if(securityException != null)
			{
				exceptionText.AppendLine("Action : " + securityException.Action);
				exceptionText.AppendLine("PermissionType    : " + securityException.PermissionType);
				try
				{
					exceptionText.AppendLine("Demanded    : " + securityException.Demanded);
				}catch{}
				try
				{
					exceptionText.AppendLine("FirstPermissionThatFailed    : " + securityException.FirstPermissionThatFailed);
				}catch{}
				try
				{
					exceptionText.AppendLine("GrantedSet    : " + securityException.GrantedSet);
				}catch{}
				try
				{
					exceptionText.AppendLine("RefusedSet    : " + securityException.RefusedSet);
				}catch{}
				try
				{
					exceptionText.AppendLine("Url    : " + securityException.Url);
				}catch{}
				try
				{
					exceptionText.AppendLine("Zone    : " + securityException.Zone);
				}catch{}
			}
			
			exceptionText.Append("Stack Trace \r\n" + e.StackTrace + "\r\n");			
			exceptionText.Append("Source : " + e.Source + "\r\n");						
			exceptionText.Append("TargetSite : " + e.TargetSite + "\r\n");									

			if(e.InnerException != null)			
			{
				 exceptionText.Append("Inner Exception\r\n");
				 exceptionText.Append(SingleExceptionText(e.InnerException));				 
			}
			
			ReflectionTypeLoadException reflectionTypeLoadException = e as ReflectionTypeLoadException;
			if(reflectionTypeLoadException != null)
			{
				 exceptionText.Append("Loader Exceptions\r\n");				 
				 foreach(Exception tlle in reflectionTypeLoadException.LoaderExceptions)
				 {
					 exceptionText.Append("Loader Exception:\r\n");				 
					 exceptionText.Append(SingleExceptionText(tlle));
				 }
			}
			
			
			return exceptionText.ToString();			
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static string ExceptionText(System.Exception exception)
		{
			StringBuilder exceptionText = new StringBuilder();			

			exceptionText.Append(SingleExceptionText(exception));

			StackTrace st = new StackTrace(true);
			exceptionText.Append("Current Stack" + st.ToString());
			
			exceptionText.Append("\r\nAssemblies:");
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) 
			{
				exceptionText.Append("\r\nName:" + asm.FullName);
				try
				{
					exceptionText.Append("\r\n\tPath:" + asm.Location);
				}
				catch
				{
					
				}
				
			}

			return exceptionText.ToString();
			
		}
		
		public static void ShowException(System.Exception exception)
		{
			ShowException(exception, null, false);
		}
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon)")]
		public static void ShowException(System.Exception exception, IWin32Window owner, bool infoOnly)
		{
			GlobalEvents.AllowIdleProcessing = false;
			try
			{
				if(Application.SplashForm != null && Application.SplashForm.Visible)
					Application.SplashForm.Hide();
			}
			catch
			{
			
			}
			
			DialogResult result = DialogResult.Cancel;
			try
			{
					ExceptionDialog t = new ExceptionDialog(exception, infoOnly);
					if(owner == null)
						result = t.ShowDialog();
					else
						result = t.ShowDialog(owner);
			}
			catch
			{
					try
					{
						MessageBox.Show("Fatal Error", "Fatal Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
					}
					finally
					{
						System.Environment.FailFast(ApplicationInfo.ProductName + " :Fatal Error");
					}
			}

			// Exits the program when the user clicks Abort.
			if (result == DialogResult.Abort) 
				System.Environment.FailFast("\n" + ApplicationInfo.ProductName + 
					" - " + TranslateString("Terminated by user on error") + " : \n" +  ExceptionText(exception));
			else
				GlobalEvents.AllowIdleProcessing = true;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void InitInfo()
		{
			this.Text = TranslateString("Unhandled exception in") + " " + ApplicationInfo.ProductName;
			picApp.Image = FreeCL.UI.ShellFileInfo.LargeIcon(System.Windows.Forms.Application.ExecutablePath).ToBitmap();
			lClass.Text = Current_.GetType().FullName;
			lMessage.Text = Current_.Message;
			lInfo.Text = ExceptionText(Current_);
		}
		
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent() {
			this.picApp = new System.Windows.Forms.PictureBox();
			this.bSend = new System.Windows.Forms.Button();
			this.lInfo = new System.Windows.Forms.TextBox();
			this.lMessage = new System.Windows.Forms.Label();
			this.bTerminate = new System.Windows.Forms.Button();
			this.bCopy = new System.Windows.Forms.Button();
			this.bContinue = new System.Windows.Forms.Button();
			this.lClass = new System.Windows.Forms.Label();
			this.bClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picApp)).BeginInit();
			this.SuspendLayout();
			// 
			// picApp
			// 
			this.picApp.Location = new System.Drawing.Point(8, 6);
			this.picApp.Name = "picApp";
			this.picApp.Size = new System.Drawing.Size(48, 33);
			this.picApp.TabIndex = 1;
			this.picApp.TabStop = false;
			// 
			// bSend
			// 
			this.bSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bSend.Location = new System.Drawing.Point(76, 371);
			this.bSend.Name = "bSend";
			this.bSend.Size = new System.Drawing.Size(116, 23);
			this.bSend.TabIndex = 9;
			this.bSend.Text = "Send by mail";
			this.bSend.UseCompatibleTextRendering = true;
			this.bSend.Click += new System.EventHandler(this.BSendClick);
			// 
			// lInfo
			// 
			this.lInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lInfo.BackColor = System.Drawing.SystemColors.Window;
			this.lInfo.Location = new System.Drawing.Point(80, 84);
			this.lInfo.MaxLength = 0;
			this.lInfo.Multiline = true;
			this.lInfo.Name = "lInfo";
			this.lInfo.ReadOnly = true;
			this.lInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.lInfo.Size = new System.Drawing.Size(468, 275);
			this.lInfo.TabIndex = 5;
			this.lInfo.Text = "Info";
			this.lInfo.WordWrap = false;
			// 
			// lMessage
			// 
			this.lMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lMessage.Location = new System.Drawing.Point(80, 6);
			this.lMessage.Name = "lMessage";
			this.lMessage.Size = new System.Drawing.Size(468, 49);
			this.lMessage.TabIndex = 2;
			this.lMessage.Text = "Message";
			this.lMessage.UseCompatibleTextRendering = true;
			// 
			// bTerminate
			// 
			this.bTerminate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bTerminate.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.bTerminate.Location = new System.Drawing.Point(458, 371);
			this.bTerminate.Name = "bTerminate";
			this.bTerminate.Size = new System.Drawing.Size(88, 23);
			this.bTerminate.TabIndex = 7;
			this.bTerminate.Text = "Terminate";
			this.bTerminate.UseCompatibleTextRendering = true;
			// 
			// bCopy
			// 
			this.bCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCopy.Location = new System.Drawing.Point(284, 371);
			this.bCopy.Name = "bCopy";
			this.bCopy.Size = new System.Drawing.Size(88, 23);
			this.bCopy.TabIndex = 8;
			this.bCopy.Text = "Copy";
			this.bCopy.UseCompatibleTextRendering = true;
			this.bCopy.Click += new System.EventHandler(this.BCopyClick);
			// 
			// bContinue
			// 
			this.bContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bContinue.Location = new System.Drawing.Point(198, 371);
			this.bContinue.Name = "bContinue";
			this.bContinue.Size = new System.Drawing.Size(80, 23);
			this.bContinue.TabIndex = 6;
			this.bContinue.Text = "Continue";
			this.bContinue.UseCompatibleTextRendering = true;
			// 
			// lClass
			// 
			this.lClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lClass.Location = new System.Drawing.Point(80, 55);
			this.lClass.Name = "lClass";
			this.lClass.Size = new System.Drawing.Size(468, 26);
			this.lClass.TabIndex = 4;
			this.lClass.Text = "Class";
			this.lClass.UseCompatibleTextRendering = true;
			// 
			// bClose
			// 
			this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bClose.Enabled = false;
			this.bClose.Location = new System.Drawing.Point(378, 371);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(88, 23);
			this.bClose.TabIndex = 10;
			this.bClose.Text = "Close";
			this.bClose.UseCompatibleTextRendering = true;
			this.bClose.Visible = false;
			// 
			// ExceptionDialog
			// 
			this.AcceptButton = this.bContinue;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(564, 400);
			this.Controls.Add(this.bClose);
			this.Controls.Add(this.bSend);
			this.Controls.Add(this.bCopy);
			this.Controls.Add(this.bTerminate);
			this.Controls.Add(this.bContinue);
			this.Controls.Add(this.lInfo);
			this.Controls.Add(this.lClass);
			this.Controls.Add(this.lMessage);
			this.Controls.Add(this.picApp);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(572, 434);
			this.Name = "ExceptionDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ExceptionDialog";
			this.VisibleChanged += new System.EventHandler(this.ExceptionDialogVisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button bClose;
		#endregion
		void BCopyClick(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Clipboard.SetDataObject(lInfo.Text);
		}

		
		public static void ExecCommand(string processCommand)
		{
			System.Diagnostics.ProcessStartInfo p = new System.Diagnostics.ProcessStartInfo(processCommand);
			p.UseShellExecute = true;
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			process.StartInfo = p;
			process.Start();
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon)")]
		void BSendClick(object sender, System.EventArgs e)
		{
			MailTo.Send(ApplicationInfo.SupportEmail, 
				"Exception error for: " + ApplicationInfo.ProductName + " " + ApplicationInfo.ProductVersion,
				lInfo.Text);
		}
		
		void ExceptionDialogVisibleChanged(object sender, System.EventArgs e)
		{
			lInfo.Select(0,0);		
		}
		
	}
}
