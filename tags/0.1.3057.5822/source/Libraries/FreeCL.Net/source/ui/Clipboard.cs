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

namespace FreeCL.UI
{
	/// <summary>
	/// Description of Clipboard.	
	/// </summary>
	public static class Clipboard
	{
		/// <summary>
		/// Does application-wide checking
		/// </summary>
		public static bool CanCopy
		{
			get
			{
				System.Windows.Forms.TextBoxBase active = Application.ActiveControl as System.Windows.Forms.TextBoxBase;	
				if(active != null)
				{
					return active.SelectionLength > 0;
				}
				
				System.Windows.Forms.WebBrowser webbrowser = Application.ActiveControl as System.Windows.Forms.WebBrowser;	
				if(webbrowser != null && webbrowser.Document != null)
				{  
					return true;
				}
				
				return false;
			}
		}
		
		/// <summary>
		/// Does application-wide copy
		/// </summary>
		public static void Copy()
		{
			System.Windows.Forms.TextBoxBase active = Application.ActiveControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Copy();
			}

			System.Windows.Forms.WebBrowser webbrowser = Application.ActiveControl as System.Windows.Forms.WebBrowser;	
			if(webbrowser != null)
			{  
				webbrowser.Document.ExecCommand("Copy", false, null);
			}
		}

		/// <summary>
		/// Does application-wide cut
		/// </summary>
		public static void Cut()
		{
			System.Windows.Forms.TextBoxBase active = Application.ActiveControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Cut();
			}
			
			System.Windows.Forms.WebBrowser webbrowser = Application.ActiveControl as System.Windows.Forms.WebBrowser;	
			if(webbrowser != null)
			{  
				webbrowser.Document.ExecCommand("Copy", false, null);
			}
			
		}

		/// <summary>
		/// Does application-wide checking
		/// </summary>
		public static bool CanPaste
		{
			get
			{
				System.Windows.Forms.TextBoxBase active = Application.ActiveControl as System.Windows.Forms.TextBoxBase;	
				if(active != null)
				{
					//TODO: check rich text, formats, etc
					IDataObject iData = null;
					try
					{
						iData = System.Windows.Forms.Clipboard.GetDataObject();
					}
					catch(System.Runtime.InteropServices.ExternalException)
					{
					
					}

					return iData != null && !active.ReadOnly && iData.GetDataPresent(DataFormats.Text);
				}
				return false;
			}
		}

		/// <summary>
		/// Does application-wide cut
		/// </summary>
		public static void Paste()
		{
			System.Windows.Forms.TextBoxBase active = Application.ActiveControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Paste();
			}
		}
	}
}
