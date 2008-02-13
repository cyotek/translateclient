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
using System.ComponentModel;
using System.Windows.Forms;

namespace FreeCL.UI
{
	/// <summary>
	/// Description of SpeedButton.	
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(FreeCL.UI.SpeedButton))]
	public class SpeedButton : System.Windows.Forms.Button
	{
		public SpeedButton()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		}
		
		/// <summary>
		/// Allow or disable ability to focus control
		/// </summary>
		[
		Category("Behavior"), 
		Description("Allow or disable ability to focus control"),
		DefaultValue(true)
		]
		public bool Selectable
		{
			get{return this.GetStyle(ControlStyles.Selectable);}
			set{this.SetStyle(ControlStyles.Selectable, value);}
			
		}

		bool showText;
		/// <summary>
		/// Set ability to show text on button
		/// </summary>
		[
		Category("Appearance"), 
		Description("Set ability to show text on button"),
		DefaultValue(false)
		]
		public bool ShowText
		{
			get{return showText;}
			set{showText = value;}
			
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if(FlatStyle == FlatStyle.Popup && !mouse_over)
				e.Graphics.DrawRectangle(new System.Drawing.Pen(BackColor, 2), ClientRectangle);

			
			if(FlatStyle == FlatStyle.Popup && mouse_down)
			{
				e.Graphics.DrawRectangle(new System.Drawing.Pen(BackColor, 2), ClientRectangle);				
				System.Drawing.Rectangle rc = ClientRectangle;							
				rc.Inflate(-1, -1);
				FreeCL.RTL.DrawHelper.Frame3D(e.Graphics, rc, 
																System.Drawing.SystemColors.ControlDark,
																System.Drawing.SystemColors.ControlLightLight, 
																1);
			}

		}

		bool mouse_over;		
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e); 		
			mouse_over = true;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);			 		
			mouse_over = false;			
			mouse_down = false;
		}
		
		bool mouse_down;		
		protected override void OnMouseDown( MouseEventArgs e)
		{
			base.OnMouseDown(e); 
			if (!Enabled) return;
			mouse_down = true;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);			 
			mouse_down = false;			
			Invalidate();
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// SpeedButton
			// 
			this.Name = "SpeedButton";
		}
		#endregion
	}
}
