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
 * Portions created by the Initial Developer are Copyright (C) 2006-2008
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
using System.Drawing;
using System.Windows.Forms;

namespace FreeCL.UI
{
	/// <summary>
	/// Description of MDIChildsButtonsBar.
	/// </summary>
	public partial class ToolStrip
	{
		public ToolStrip()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		}

		Orientation orientation = Orientation.Horizontal;
		
		[
		Category("Appearance"), 
		Description("Set orientation of buttons align"),
		DefaultValue(Orientation.Horizontal)
		]
		public Orientation BarOrientation {
			get 
			{
				return orientation;
			}
			set 
			{
				orientation = value;
				foreach(Control ctrl in Controls)
					ctrl.Dock = orientation == Orientation.Horizontal ? DockStyle.Left : DockStyle.Top;		

			}
		}
		
		bool sameSize;
		[
		Category("Appearance"), 
		Description("Set same size for controls"),
		DefaultValue(false)		
		]
		public bool SameSize {
			get 
			{
				return sameSize;
			}
			set 
			{
				sameSize = value;
				UpdateButtonsSize();				
			}
		}
		
		protected override void OnItemAdded (
			ToolStripItemEventArgs e
		)
		{
			e.Item.TextChanged += new EventHandler(this.button_TextChanged);
			e.Item.AutoSize = false;
			
			base.OnItemAdded(e);
			UpdateButtonsSize();
		}
		
		protected override void OnItemRemoved (
			ToolStripItemEventArgs e
		)
		{
			base.OnItemRemoved(e);
			UpdateButtonsSize();
			e.Item.TextChanged -= new EventHandler(this.button_TextChanged);			

		}
		
		public System.Windows.Forms.ToolStripButton AddButton(string caption)
		{
			System.Windows.Forms.ToolStripButton button = new System.Windows.Forms.ToolStripButton();			
			Items.Add(button);
			button.DisplayStyle = ToolStripItemDisplayStyle.Text;			
			button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft ;			
			button.Text = caption;
			UpdateButtonsSize();
			return button;
		}

		public void RemoveButton(System.Windows.Forms.ToolStripButton button)
		{
			Items.Remove(button);			
			UpdateButtonsSize();			
		}
		
		private void button_TextChanged(object sender, EventArgs e)
		{
			UpdateButtonsSize();	
		}
		
		protected override void OnLayout (
			LayoutEventArgs levent
		)
		{
			base.OnLayout(levent);
			UpdateButtonsSize();						
		}
		 
		
		
		public void UpdateButtonsSize()
		{
			//Cначала ищем максимально длинный Caption
			 int MaxLength = 0;
			 int tmp_Length = 0;
			 int Space = 2; //Пропуск м-у кнопками
			 Graphics g = CreateGraphics();
			 foreach(ToolStripItem ctrl in Items)
			 {
				 if(orientation == Orientation.Horizontal)
				 {
					tmp_Length = (int)g.MeasureString(ctrl.Text, ctrl.Font).Width;
					if(ctrl.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText && ctrl.Image != null)
						tmp_Length += 18;	
				 }
				 else
					tmp_Length = (int)g.MeasureString(ctrl.Text, ctrl.Font).Height + 10;
				 //Trace.WriteLine("tmp"	+ tmp_Length);
						
					if(MaxLength < tmp_Length)
					{ //Если длина Caption > макс. длины, то изменяем
						 MaxLength = tmp_Length;
					}
			 }
			 g.Dispose();

			 if(orientation == Orientation.Horizontal)
				 MaxLength+=18; //Добавляем с запасом
			 else
				 MaxLength+=4; //Добавляем с запасом
			 
			 MaxLength+=Space; //+Пропуск
			 //Расчитываем сколько потребуется места для всех кнопок при макс длине
			 int	CalcedWidthOfAllButtons = MaxLength * Items.Count;
			 //Trace.WriteLine("maz"	+ MaxLength + "all" + CalcedWidthOfAllButtons);

			 int NewButtonsWidth;
			 //Если места хватает, то так и делаем
			 if(orientation == Orientation.Horizontal)
			 {
				 if(CalcedWidthOfAllButtons <= (ClientSize.Width) && !sameSize)
				 {
						NewButtonsWidth = MaxLength;
				 }
				 else
				 { //Прийдется укорачивать
						NewButtonsWidth = (int)(((float)(MaxLength*ClientSize.Width))/((float)CalcedWidthOfAllButtons)-1);
				 }
			 }
			 else
			 {
				 if(CalcedWidthOfAllButtons <= (ClientSize.Height) && !sameSize)
				 {
						NewButtonsWidth = MaxLength;
				 }
				 else
				 { //Прийдется укорачивать
						NewButtonsWidth = (int)(((float)(MaxLength*ClientSize.Height))/((float)CalcedWidthOfAllButtons)-1);
				 }
			 }

				//Trace.WriteLine("Count" + Controls.Count.ToString() + " width" + NewButtonsWidth.ToString());
			
			 //Обрабатываем
			 Point pos = new Point(0, 0);
			 foreach(ToolStripItem ctrl in Items)
			 {
				 //Trace.WriteLine("Beforr"	+ ctrl.Width);
				 if(orientation == Orientation.Horizontal)
				 {
					ctrl.Width = NewButtonsWidth-Space -10 ; 
					SetItemLocation(ctrl, pos);
					pos.X += ctrl.Width;
				 }
				 else
				 {
					ctrl.Height = NewButtonsWidth-Space;	
					ctrl.Width = ClientSize.Width - 10;
				 }
				 //Trace.WriteLine("After"	+ ctrl.Width);				 
			 }		
		}
		
	}
}
