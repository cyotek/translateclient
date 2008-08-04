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
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.UI
{
		public class ProcessCmdKeyEventArgs : EventArgs
		{
		
			[SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId="0#")]
			public ProcessCmdKeyEventArgs(ref System.Windows.Forms.Message msg, Keys keyData)
			{
				this.keyData = keyData;
				this.message = msg;
			}

			Keys keyData;
			public Keys KeyData {
				get { return keyData; }
				set { keyData = value; }
			}
			
			System.Windows.Forms.Message message;
			public Message Message {
				get { return message; }
				set { message = value; }
			}
			
			bool handled;
			public bool Handled {
				get { return handled; }
				set { handled = value; }
			}
			
		};
		
	
		public delegate void MergeMenuHandler(FreeCL.UI.MainMenu owner, System.Windows.Forms.Menu source);				
	
	/// <summary>
	/// Description of MainMenu.
	/// </summary>
	[ToolboxItemFilter("System.Windows.Forms.MainMenu")]
	public class MainMenu : System.Windows.Forms.MainMenu
	{
		public MainMenu()
		{
			
		}
		
		public event EventHandler<ProcessCmdKeyEventArgs> OnProcessCmdKey;
		
		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
		{
			if(OnProcessCmdKey != null)
			{
				ProcessCmdKeyEventArgs e = new ProcessCmdKeyEventArgs(ref msg, keyData);
				OnProcessCmdKey(this, e);
				msg = e.Message; 
				if(e.Handled)
					return true;
			}
			
			return base.ProcessCmdKey(ref msg, keyData);
		}		 
		

		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
		public event MergeMenuHandler OnMergeMenu;
		public override void MergeMenu(System.Windows.Forms.Menu menuSrc)
		{
			if(OnMergeMenu != null)
				OnMergeMenu(this, menuSrc);
			base.MergeMenu(menuSrc);
		}
		

	}
}
