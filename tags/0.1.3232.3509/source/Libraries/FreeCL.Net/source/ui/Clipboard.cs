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
using FreeCL.RTL;
using System.Windows.Forms;
using System.Reflection;

namespace FreeCL.UI
{
	/// <summary>
	/// Description of Clipboard.	
	/// </summary>
	public static class Clipboard
	{
	
		public delegate W FunctionWithReturn<W>();
		public delegate void FunctionWithoutReturn();
		/// <summary>
		/// Does application-wide checking
		/// </summary>
		/// 
		public static bool CanCopy
		{
			get
			{
				
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}

				System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
				if(active != null)
				{
					return active.SelectionLength > 0;
				}
				
				System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
				if(webbrowser != null && webbrowser.Document != null)
				{  
					return true;
				}

				
				PropertyInfo property = activeControl.GetType().GetProperty("SelectionLength", typeof(int));
				if(property != null)
				{
					MethodInfo method = property.GetGetMethod();
					if(method != null)
					{
						FunctionWithReturn<int> getMethod = (FunctionWithReturn<int>) Delegate.CreateDelegate
            				(typeof(FunctionWithReturn<int>), activeControl, method);
            				
            			return getMethod() > 0;	
					}
				}
				
				return false;
			}
		}
		
		/// <summary>
		/// Does application-wide copy
		/// </summary>
		public static void Copy()
		{
			Control activeControl = Application.ActiveControl;
			if(activeControl == null)
			{
				return;
			}
		
			System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Copy();
				return;
			}

			System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
			if(webbrowser != null)
			{  
				webbrowser.Document.ExecCommand("Copy", false, null);
				return;
			}
			
			MethodInfo method = activeControl.GetType().GetMethod("Copy");
			if(method != null)
			{
				FunctionWithoutReturn getMethod = (FunctionWithoutReturn) Delegate.CreateDelegate
            		(typeof(FunctionWithoutReturn), activeControl, method);
            				
            	getMethod();	
			}
			
		}

		/// <summary>
		/// Does application-wide cut
		/// </summary>
		public static void Cut()
		{
			Control activeControl = Application.ActiveControl;
			if(activeControl == null)
			{
				return;
			}
		
			System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Cut();
				return;
			}
			
			System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
			if(webbrowser != null)
			{  
				webbrowser.Document.ExecCommand("Copy", false, null);
				return;
			}
			
			MethodInfo method = activeControl.GetType().GetMethod("Cut");
			if(method != null)
			{
				FunctionWithoutReturn getMethod = (FunctionWithoutReturn) Delegate.CreateDelegate
            		(typeof(FunctionWithoutReturn), activeControl, method);
            				
            	getMethod();	
			}
			
		}

		/// <summary>
		/// Does application-wide checking
		/// </summary>
		public static bool CanPaste
		{
			get
			{
			
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}
			
				System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
				if(active != null)
				{
					IDataObject iData = null;
					try
					{
						iData = System.Windows.Forms.Clipboard.GetDataObject();
					}
					catch(System.Runtime.InteropServices.ExternalException)
					{
					
					}
					
					if(iData == null)
						return false;
				
					System.Windows.Forms.RichTextBox richText = activeControl as System.Windows.Forms.RichTextBox;
					if(richText != null)
					{
						if(richText.ReadOnly)
							return false;
							
						string[] formats = iData.GetFormats(true);
						bool canPaste = false;
						foreach(string format in formats)
						{
							canPaste = richText.CanPaste(DataFormats.GetFormat(format));
							if(canPaste)
								break;
						}
						return canPaste;
					}	
					else
					{
						return !active.ReadOnly && iData.GetDataPresent(DataFormats.Text);
					}
				}
				
				MethodInfo method = activeControl.GetType().GetMethod("CanPaste");
				if(method != null)
				{
					FunctionWithReturn<bool> getMethod = (FunctionWithReturn<bool>) Delegate.CreateDelegate
            				(typeof(FunctionWithReturn<bool>), activeControl, method);
            				
           			return getMethod();	
				}
				
				
				return false;
			}
		}

		/// <summary>
		/// Does application-wide cut
		/// </summary>
		public static void Paste()
		{
			Control activeControl = Application.ActiveControl;
			if(activeControl == null)
			{
				return;
			}
		
			System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Paste();
				return;
			}
			
			MethodInfo method = activeControl.GetType().GetMethod("Paste");
			if(method != null)
			{
				FunctionWithoutReturn getMethod = (FunctionWithoutReturn) Delegate.CreateDelegate
            		(typeof(FunctionWithoutReturn), activeControl, method);
            				
            	getMethod();	
			}
			
		}
	}
}
