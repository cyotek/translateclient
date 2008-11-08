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
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;

namespace FreeCL.UI
{
	/// <summary>
	/// Description of TabControl.
	/// </summary>
	[ListBindable(false), ToolboxItemFilter("System.Windows.Forms"), Designer("Microsoft.VisualStudio.Windows.Forms.MenuDesigner")]
	public class MenuItem : System.Windows.Forms.MenuItem
	{
		public MenuItem()
		{
		}
		
		const int BitmapWidth = 20;
		const int VerticalTextOffset = 0;
		const int HorizontalTextOffset = 6;
		const int SeparatorHeight = 6;
		const int RightOffset = 15;
		
		static private string GetMenuShortcutText(System.Windows.Forms.MenuItem item)
		{
			string finalText = "";
			if (item.ShowShortcut & item.Shortcut != Shortcut.None) 
			{
				Keys k= (Keys)item.Shortcut;
				finalText = System.ComponentModel.TypeDescriptor.GetConverter(k.GetType()).ConvertToString(k);
			}
			return finalText;
		}

		public static void OwnerMeasureMenuItem(System.Windows.Forms.MenuItem item, MeasureItemEventArgs e)
 		{
			OwnerMeasureMenuItem(item, e, GetMenuShortcutText(item));
		}
		
			
 		public static void OwnerMeasureMenuItem(System.Windows.Forms.MenuItem item, MeasureItemEventArgs e, string shortcutText)
 		{
			if(item == null)
				throw new ArgumentNullException("item");
 		
			if(e == null)
				throw new ArgumentNullException("e");
 		
			if ((item.Text == "-")) 
			{
				e.ItemHeight = SeparatorHeight;
				return;
			}
			SizeF stringSize;
			stringSize = e.Graphics.MeasureString(item.Text + shortcutText, SystemInformation.MenuFont);
			e.ItemHeight = SystemInformation.MenuHeight;
			e.ItemWidth = BitmapWidth + HorizontalTextOffset + System.Convert.ToInt32(stringSize.Width) + RightOffset;
		}
	
 		public static void OwnerDrawMenuItem(System.Windows.Forms.MenuItem menuItem, DrawItemEventArgs e)
 		{
			OwnerDrawMenuItem(menuItem, e, GetMenuShortcutText(menuItem)); 			
 		}
 		
 		public static void OwnerDrawMenuItem(System.Windows.Forms.MenuItem menuItem, DrawItemEventArgs e, string shortcutText)
 		{
			if(e == null)
				throw new ArgumentNullException("e");
 		
			Graphics g = e.Graphics;
			DrawItemState itemState = e.State;

			
			Rectangle BitmapBounds = e.Bounds;
			BitmapBounds.Width = BitmapWidth + 2;
			Rectangle ItemBounds = e.Bounds;
			ItemBounds.X = BitmapWidth;
			Rectangle ItemTextBounds = e.Bounds;
			
			ItemTextBounds.X = (BitmapWidth + HorizontalTextOffset);
			ItemTextBounds.Y = (e.Bounds.Y + VerticalTextOffset);
			ItemTextBounds.Width = (e.Bounds.Width);
			ItemTextBounds.Height = (e.Bounds.Height);
			
			DrawBackground(g, itemState, e.Bounds);
			//DrawBitmap(g, item, itemState);
			DrawText(g, menuItem, itemState, shortcutText, e.Bounds, ItemTextBounds);
 		}

		static private void DrawBackground(Graphics g, DrawItemState itemState, Rectangle rectToPaint)
		{
			Brush backBrush;
			bool selected;
			bool disabled;
			
			selected = (itemState & DrawItemState.Selected) == DrawItemState.Selected;
			disabled = (itemState & DrawItemState.Disabled) == DrawItemState.Disabled;
			
			if (selected & !(disabled)) 
				backBrush = new SolidBrush(SystemColors.Highlight);
			else
				backBrush = new SolidBrush(SystemColors.Menu);
			
			g.FillRectangle(backBrush, rectToPaint);
				
			backBrush.Dispose();
		}

		static private void DrawSeparator(Graphics g, Rectangle rectToPaint)
		{
			Pen sepPen = new Pen(SystemColors.GrayText, 1);
			g.DrawLine(sepPen, rectToPaint.X, rectToPaint.Y, rectToPaint.X + rectToPaint.Right, rectToPaint.Y);
			sepPen.Dispose();
		}
		
		static private void DrawText(Graphics g, System.Windows.Forms.MenuItem item, DrawItemState itemState, string shortcutText, Rectangle itemRect, Rectangle rectToPaint)
		{
			if (item.Text == "-") 
			{
				DrawSeparator(g, rectToPaint);
				return;
			}
			
			Brush foreBrush;
			if (item.Enabled) 
				foreBrush = new SolidBrush(SystemColors.MenuText);
			else
				foreBrush = new SolidBrush(SystemColors.GrayText);
			
			Font tmpFont;
			
			if((itemState & DrawItemState.Default) == DrawItemState.Default) 
				tmpFont = new Font(SystemInformation.MenuFont, FontStyle.Bold);
			else 
				tmpFont = SystemInformation.MenuFont;
			
			
																				 
			StringFormat strFormat = new StringFormat();
			strFormat.HotkeyPrefix = HotkeyPrefix.Show;
			strFormat.LineAlignment = StringAlignment.Center;
			
			RectangleF ItemTextBounds = new RectangleF(rectToPaint.X, rectToPaint.Y, rectToPaint.Width, rectToPaint.Height);
			
			g.DrawString(item.Text, tmpFont, foreBrush, ItemTextBounds, strFormat);

			if(shortcutText.Length > 0)
			{
				RectangleF rect = new RectangleF(itemRect.X + BitmapWidth , itemRect.Y, itemRect.Width - BitmapWidth, itemRect.Height);
				rect.Width -=	RightOffset;
				strFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				g.DrawString(shortcutText, tmpFont, foreBrush, rect, strFormat);
			}
			foreBrush.Dispose();
		}
	
	}
}
