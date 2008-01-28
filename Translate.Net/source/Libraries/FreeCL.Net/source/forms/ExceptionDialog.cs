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
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using FreeCL.UI;

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
		public ExceptionDialog(System.Exception exception)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Current_ = exception;
			
			InitInfo();
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
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon)")]
		public static void ShowException(System.Exception exception)
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
					ExceptionDialog t = new ExceptionDialog(exception);
					result = t.ShowDialog();
			}
			catch
			{
					try
					{
						MessageBox.Show("Fatal Error", "Fatal Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
					}
					finally
					{
						System.Environment.Exit(0);
					}
			}

			// Exits the program when the user clicks Abort.
			if (result == DialogResult.Abort) 
				System.Environment.Exit(0);
			else
				GlobalEvents.AllowIdleProcessing = true;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		void InitInfo()
		{
			System.Diagnostics.FileVersionInfo vi = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);
			this.Text = "Unhandled exception in " + vi.ProductName;			
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
			((System.ComponentModel.ISupportInitialize)(this.picApp)).BeginInit();
			this.SuspendLayout();
			// 
			// picApp
			// 
			this.picApp.Location = new System.Drawing.Point(8, 6);
			this.picApp.Name = "picApp";
			this.picApp.Size = new System.Drawing.Size(48, 36);
			this.picApp.TabIndex = 1;
			this.picApp.TabStop = false;
			// 
			// bSend
			// 
			this.bSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bSend.Enabled = false;
			this.bSend.Location = new System.Drawing.Point(76, 368);
			this.bSend.Name = "bSend";
			this.bSend.Size = new System.Drawing.Size(88, 21);
			this.bSend.TabIndex = 9;
			this.bSend.Text = "Send by mail";
			this.bSend.UseCompatibleTextRendering = true;
			this.bSend.Visible = false;
			this.bSend.Click += new System.EventHandler(this.BSendClick);
			// 
			// lInfo
			// 
			this.lInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lInfo.BackColor = System.Drawing.SystemColors.Control;
			this.lInfo.Location = new System.Drawing.Point(80, 90);
			this.lInfo.Multiline = true;
			this.lInfo.Name = "lInfo";
			this.lInfo.ReadOnly = true;
			this.lInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.lInfo.Size = new System.Drawing.Size(468, 265);
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
			this.lMessage.Size = new System.Drawing.Size(468, 53);
			this.lMessage.TabIndex = 2;
			this.lMessage.Text = "Message";
			this.lMessage.UseCompatibleTextRendering = true;
			// 
			// bTerminate
			// 
			this.bTerminate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bTerminate.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.bTerminate.Location = new System.Drawing.Point(458, 368);
			this.bTerminate.Name = "bTerminate";
			this.bTerminate.Size = new System.Drawing.Size(88, 21);
			this.bTerminate.TabIndex = 7;
			this.bTerminate.Text = "Terminate";
			this.bTerminate.UseCompatibleTextRendering = true;
			// 
			// bCopy
			// 
			this.bCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCopy.Location = new System.Drawing.Point(262, 368);
			this.bCopy.Name = "bCopy";
			this.bCopy.Size = new System.Drawing.Size(88, 21);
			this.bCopy.TabIndex = 8;
			this.bCopy.Text = "Copy";
			this.bCopy.UseCompatibleTextRendering = true;
			this.bCopy.Click += new System.EventHandler(this.BCopyClick);
			// 
			// bContinue
			// 
			this.bContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bContinue.Location = new System.Drawing.Point(172, 368);
			this.bContinue.Name = "bContinue";
			this.bContinue.Size = new System.Drawing.Size(80, 21);
			this.bContinue.TabIndex = 6;
			this.bContinue.Text = "Continue";
			this.bContinue.UseCompatibleTextRendering = true;
			// 
			// lClass
			// 
			this.lClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lClass.Location = new System.Drawing.Point(80, 59);
			this.lClass.Name = "lClass";
			this.lClass.Size = new System.Drawing.Size(468, 28);
			this.lClass.TabIndex = 4;
			this.lClass.Text = "Class";
			this.lClass.UseCompatibleTextRendering = true;
			// 
			// ExceptionDialog
			// 
			this.AcceptButton = this.bContinue;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(564, 400);
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
			this.Name = "ExceptionDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ExceptionDialog";
			this.VisibleChanged += new System.EventHandler(this.ExceptionDialogVisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
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
			string mess = "";
			try
			{
				mess = "mailto:support@support?body=" + lInfo.Text;
				mess = mess.Replace("\r\n", "%0d%0a");
				mess = mess.Replace(" ", "%20");
				
				ExecCommand(mess);
			}
			catch
			{
				MessageBox.Show("Send Error.\n"+ mess, "Failed to execute mailto protocol.", MessageBoxButtons.OK, MessageBoxIcon.Stop);				
			}
			
		}
		
		void ExceptionDialogVisibleChanged(object sender, System.EventArgs e)
		{
			lInfo.Select(0,0);		
		}
		
	}
}
