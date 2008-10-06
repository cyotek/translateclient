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
using System.Drawing;
using FreeCL.RTL;

namespace FreeCL.UI
{
	[Serializable]
	public enum BevelStyle
	{
			None, 
			Lowered, 
			Raised, 
			Space	 
	}
	
	/// <summary>
	/// Description of Panel.	
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(FreeCL.UI.Panel))]
	[DesignerAttribute(typeof(System.Windows.Forms.Design.ScrollableControlDesigner), typeof(System.ComponentModel.Design.IDesigner))]
	public class Panel : System.Windows.Forms.Panel
	{
		private System.ComponentModel.Container components = null;
		
		public Panel()
		{
			InitializeComponent();
			ResizeRedraw = true;
		}
		
		
		private BevelStyle BevelInner_ = BevelStyle.None;
		
		/// <summary>
		/// Inner Bevel Style
		/// </summary>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Inner Bevel Style")]
		[Category("Bevel")]
		[DefaultValue(BevelStyle.None)]
		public BevelStyle BevelInner	
		{
			get{return BevelInner_;}
			set
			{
				BevelInner_ = value;
				UpdateDockPadding();
			}
		}
		
		BevelStyle BevelOuter_ = BevelStyle.Raised;
		/// <summary>
		/// Outer Bevel Style
		/// </summary>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Outer Bevel Style")]
		[Category("Bevel")]
		[DefaultValue(BevelStyle.Raised)]		
		public BevelStyle BevelOuter
		{
			get{return BevelOuter_;}
			set
			{
				BevelOuter_ = value;
				UpdateDockPadding();
			}
		}

		int BevelWidth_ = 1;
		/// <summary>
		/// Outer Bevel Style
		/// </summary>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Bevel Width")]
		[Category("Bevel")]
		[DefaultValue(1)]				
		public int BevelWidth
		{
			get{return BevelWidth_;}
			set
			{
				BevelWidth_ = value;
				UpdateDockPadding();
			}
		}

		private bool AutoUpdateDockPadding_ = true;
		/// <summary>
		/// Allow to update padding based on bevels style
		/// </summary>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Allow to update padding based on bevels style")]
		[Category("Bevel")]
		[DefaultValue(true)]				
		public bool AutoUpdateDockPadding
		{
			get{return AutoUpdateDockPadding_;}
			set
			{
				AutoUpdateDockPadding_ = value;
				UpdateDockPadding();
			}
		}
		
		private void UpdateDockPadding()
		{
			if(AutoUpdateDockPadding_)
			{
				int inflate_size = 0;
				inflate_size += (BevelOuter_ != BevelStyle.None) ? BevelWidth_ : 0;
				inflate_size += (BevelInner_ !=BevelStyle.None) ? BevelWidth_ : 0;			
				this.DockPadding.All = inflate_size;
			}
			base.Refresh();
		}
		
		
		
		protected override void OnPaint(PaintEventArgs e) 
		{
			base.OnPaint(e);
			
			Rectangle rc = ClientRectangle;
			rc.Inflate(-1, -1);
	
			if(BevelOuter_ != BevelStyle.None)
			{
				FreeCL.RTL.DrawHelper.Frame3D(e.Graphics, rc, BevelOuter_ == BevelStyle.Lowered	? SystemColors.ControlDark : SystemColors.ControlLightLight , 
																BevelOuter_ == BevelStyle.Lowered	? SystemColors.ControlLightLight : SystemColors.ControlDark , 
																BevelWidth_);
				rc.Inflate(-BevelWidth_, -BevelWidth_);
			}
	
			if(BevelInner_ != BevelStyle.None)
			{
				FreeCL.RTL.DrawHelper.Frame3D(e.Graphics, rc, BevelInner_ == BevelStyle.Lowered	? SystemColors.ControlDark : SystemColors.ControlLightLight , 
																BevelInner_ == BevelStyle.Lowered	? SystemColors.ControlLightLight : SystemColors.ControlDark, 
																BevelWidth_);
			}
		}
		
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();			
		}
		#endregion
		
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				//Memory.Dispose(Controls);				
				Memory.Dispose(components);																			
				Memory.DisposeChilds(this);				
				Memory.DisposeAndNull(ref components);
			}
			base.Dispose( disposing );
		}
		
	}
}
