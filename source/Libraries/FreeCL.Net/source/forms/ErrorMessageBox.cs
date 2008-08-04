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

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of ErrorMessageBox.
	/// </summary>
	public partial class ErrorMessageBox : FreeCL.Forms.BaseForm
	{
		public ErrorMessageBox(
			string text,
			string caption,
			MessageBoxButtons buttons,
			Exception exception)
		{
			InitializeComponent();

			lMessage.Text = text;
			Text = caption;
			this.exception = exception;
			bShowExceptionIfno.Enabled =  exception != null;
		}
		
		Exception exception;

		public static DialogResult Show (
			string text,
			string caption,
			Exception exception
		)
		{
			return Show(Application.MainForm, text, caption, exception);
		}

		public static DialogResult Show (
			IWin32Window owner,
			string text,
			string caption,
			Exception exception
		)
		{
			return Show(owner, text, caption, MessageBoxButtons.OK, exception);
		}
		
		public static DialogResult Show (
			IWin32Window owner,
			string text,
			string caption,
			MessageBoxButtons buttons,
			Exception exception
		)
		{
			DialogResult result = DialogResult.Cancel;
		
			try 
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
				
				ErrorMessageBox t = new ErrorMessageBox(
					text,
					caption,
					buttons,
					exception);
				
				if(owner == null)
					result = t.ShowDialog();
				else
					result = t.ShowDialog(owner);
			} 
			finally 
			{
				GlobalEvents.AllowIdleProcessing = true;

			}
			return result;	
			
		}
		
		
		void BShowExceptionIfnoClick(object sender, EventArgs e)
		{
			ExceptionDialog.ShowException(exception,this,true);
		}
	}
}
