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
using System.Diagnostics;
using FreeCL.UI;
using FreeCL.RTL;
using System.Collections;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.Forms
{
	
	/// <summary>
	/// Description of BaseMainForm.	
	/// </summary>
	public class BaseGuiForm : FreeCL.Forms.BaseForm
	{
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public System.Windows.Forms.MenuStrip msMain;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public System.Windows.Forms.ToolStripPanel pToolBars;		

		public BaseGuiForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			pToolBars.Dock = DockStyle.Top;
			
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent() {
			this.msMain = new System.Windows.Forms.MenuStrip();
			this.pToolBars = new System.Windows.Forms.ToolStripPanel();
			this.SuspendLayout();
			// 
			// il
			// 
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			// 
			// msMain
			// 
			this.msMain.AllowMerge = false;
			this.msMain.ImageList = this.il;
			this.msMain.Location = new System.Drawing.Point(0, 0);
			this.msMain.Name = "msMain";
			this.msMain.ShowItemToolTips = true;
			this.msMain.Size = new System.Drawing.Size(550, 24);
			this.msMain.TabIndex = 0;
			this.msMain.Text = "menuStrip1";
			// 
			// pToolbars
			// 
			this.pToolBars.Dock = System.Windows.Forms.DockStyle.Top;
			this.pToolBars.Location = new System.Drawing.Point(0, 24);
			this.pToolBars.Name = "pToolbars";
			this.pToolBars.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.pToolBars.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.pToolBars.Size = new System.Drawing.Size(550, 0);
			// 
			// BaseGuiForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(550, 549);
			this.Controls.Add(this.pToolBars);
			this.Controls.Add(this.msMain);
			this.MainMenuStrip = this.msMain;
			this.Name = "BaseGuiForm";
			this.Text = "BaseGUIForm";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion
		
		
		public ToolStripMenuItem GetTopMenuItem(CommonMenusEnum menu)
		{
			string name = "&" + Enum.GetName(typeof(CommonMenusEnum), menu);
			int index = (int)menu;
			ToolStripMenuItem res = GetTopMenuItem(name, index);
			if(CommonMenusEnum.Window == menu && msMain.MdiWindowListItem != res )
				msMain.MdiWindowListItem = res;
			
			return res;
		}
		
		public ToolStripMenuItem GetTopMenuItem(string name, int index)
		{
			foreach(ToolStripMenuItem mi in msMain.Items)
			{
				if(mi.Text == name)
				{
					return mi;
				}
			}
			ToolStripMenuItem newmi = new ToolStripMenuItem();
			newmi.Text = name;
			newmi.MergeIndex = index;
			newmi.MergeAction = MergeAction.MatchOnly;
		 
			if(index < msMain.Items.Count)
				 msMain.Items.Insert(index, newmi);					
			else
				 msMain.Items.Add(newmi);					
					
			SortItems(msMain.Items);
			
			return newmi;
		}
		
		public ToolStripItem AddMenuItem(CommonMenusEnum menu, int index, FreeCL.UI.Actions.Action action)
		{
			return AddMenuItem(GetTopMenuItem(menu), index, action);
		}

		public ToolStripItem AddMenuItem(CommonMenusEnum menu, CommonItemsEnum index, FreeCL.UI.Actions.Action action)
		{
			return AddMenuItem(menu, (int)index, action);
		}
		
		public ToolStripItem AddMenuItem(ToolStripMenuItem owner, int index, FreeCL.UI.Actions.Action action)
		{
			return AddMenuItem(owner, (int)index, action, true);
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]		
		public ToolStripItem AddMenuItem(ToolStripMenuItem owner, int index, FreeCL.UI.Actions.Action action, bool separator)
		{
			if(action != null)
				return AddMenuItem(owner, (int)index, action, separator, action.Text);
			else	
				return AddMenuItem(owner, (int)index, action, separator, "");
		}

		public ToolStripItem AddMenuItem(CommonMenusEnum menu, CommonItemsEnum index, string caption)
		{
			return AddMenuItem(GetTopMenuItem(menu), (int)index, null, false, caption);
		}

		public ToolStripMenuItem GetOrCreateMenuItem(CommonMenusEnum menu, CommonItemsEnum index, string caption)
		{
			return GetOrCreateMenuItem(GetTopMenuItem(menu), (int)index, caption);
		}
		
		public ToolStripMenuItem GetOrCreateMenuItem(ToolStripMenuItem owner, int index, string caption)
		{
			return GetOrCreateMenuItem(owner, index, null, false, caption);
		}
		
		public ToolStripMenuItem GetOrCreateMenuItem(ToolStripMenuItem owner, int index, FreeCL.UI.Actions.Action action, bool separator, string caption)
		{
			if(owner == null)
				throw new ArgumentNullException("owner");
		
			foreach(ToolStripItem mi in owner.DropDownItems)
			{
				if(mi.Text == caption)
				{
					return (ToolStripMenuItem)mi;
				}
			}
			return (ToolStripMenuItem)this.AddMenuItem(owner, index, action, separator, caption);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ToolStripItem GetMenuItem(ToolStripMenuItem owner, int index, FreeCL.UI.Actions.Action action, bool separator)
		{
			if(owner == null)
				throw new ArgumentNullException("owner");

			if(action == null)
				throw new ArgumentNullException("action");
		
			foreach(ToolStripItem mi in owner.DropDownItems)
			{
				if(mi.MergeIndex == index && ((separator && mi is ToolStripSeparator) || action.Parent.GetAction(mi) == action))
				{
					return mi;
				}
			}
			return null;
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ToolStripItem AddMenuItem(ToolStripMenuItem owner, int index, FreeCL.UI.Actions.Action action, bool separator, string caption)
		{
			if(owner == null)
				throw new ArgumentNullException("owner");
		
			ToolStripItem newmi = new ToolStripMenuItem();
			if(action != null)
			{
				action.Parent.SetAction(newmi, action);
			}
			else if(separator)
				newmi = new ToolStripSeparator();
			else
				newmi.Text = caption;
					
			newmi.MergeIndex = index;
			newmi.MergeAction =	MergeAction.Insert;
					
			if(index < owner.DropDownItems.Count && index >= 0)
				owner.DropDownItems.Insert(index, newmi);					
			else
				owner.DropDownItems.Add(newmi);					
					
			SortItems(owner.DropDownItems);
					
			return newmi;
		}
		
		public void AddMenuSeparator(ToolStripMenuItem owner, int index)
		{
			 AddMenuItem(owner, index, null); 
		}
		
		public void AddMenuSeparator(CommonMenusEnum menu, int index)
		{
			AddMenuItem(GetTopMenuItem(menu), index, null);
		}
		
		public void AddMenuSeparator(CommonMenusEnum menu, CommonItemsEnum index)
		{
			AddMenuSeparator(menu, (int)index);
		}
		
		
		class ToolStripCustomIComparer : IComparer
		{
			public ToolStripCustomIComparer()
			{
				
			}
			int IComparer.Compare(object x, object y)
			{
				ToolStripItem xi = x as ToolStripItem;
				ToolStripItem yi = y as ToolStripItem;
				return xi.MergeIndex -	yi.MergeIndex;
			}
		
		}
		
		
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public void SortItems(ToolStripItemCollection items)
		{
			if(items == null)
				throw new ArgumentNullException("items");
		
			ArrayList list = new ArrayList();
			foreach(object o in items)
				list.Add(o);
			
			list.Sort(new ToolStripCustomIComparer());
				
			items.Clear();
			items.AddRange((ToolStripItem[])list.ToArray(typeof(ToolStripItem)));
		}
		

		class ToolStripBarCustomIComparer : IComparer
		{
			int IComparer.Compare(object x, object y)
			{
				System.Windows.Forms.ToolStrip xi = x as System.Windows.Forms.ToolStrip;
				System.Windows.Forms.ToolStrip yi = y as System.Windows.Forms.ToolStrip;
				if((!(yi.Tag is int)) &&  (!(xi.Tag is int)))
					return 0;
				if(!(xi.Tag is int))
					return -1;
				if(!(yi.Tag is int))
					return 1;
					
				return ((int)yi.Tag) - ((int)xi.Tag);
			}
		}		
		
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		void SortItems(ToolStripPanel panel)
		{
			if(panel == null)
				throw new ArgumentNullException("panel");
		
			ArrayList list = new ArrayList();
			foreach(object o in panel.Controls)
				list.Add(o);
			
			list.Sort(new ToolStripBarCustomIComparer());
				
			panel.Controls.Clear();
			foreach(object o in list)
			{
				panel.Join(o as System.Windows.Forms.ToolStrip);
			}
		}
		
		public System.Windows.Forms.ToolStrip GetToolStrip(CommonToolStripsEnum toolStrip)
		{
			string name = Enum.GetName(typeof(CommonToolStripsEnum), toolStrip);
			int index = (int)toolStrip;
			return GetToolStrip(name, index);
		}
		
		public System.Windows.Forms.ToolStrip GetToolStrip(string name, int index)
		{
			foreach(Control c in pToolBars.Controls)
			{
				System.Windows.Forms.ToolStrip ts = c as System.Windows.Forms.ToolStrip;
				if(ts != null && ts.Text == name)
				{
					return ts;
				}
			}
			System.Windows.Forms.ToolStrip newts = new System.Windows.Forms.ToolStrip();
			newts.Text = name;
			newts.Tag = index;
			newts.AllowMerge = true;
		 
			pToolBars.Join(newts);					
			
			SortItems(pToolBars);
			
			return newts;
		}
		
		public ToolStripItem AddToolStripButton(CommonToolStripsEnum toolStrip, int index, FreeCL.UI.Actions.Action action)
		{
			return AddToolStripButton(GetToolStrip(toolStrip), index, action, typeof(ToolStripButton));
		}

		public ToolStripItem  AddToolStripButton(CommonToolStripsEnum toolStrip, CommonItemsEnum index, FreeCL.UI.Actions.Action action)
		{
			return AddToolStripButton(toolStrip, (int)index, action, typeof(ToolStripButton));
		}

		public ToolStripItem AddToolStripButton(CommonToolStripsEnum toolStrip, int index, FreeCL.UI.Actions.Action action, Type buttonType)
		{
			return AddToolStripButton(GetToolStrip(toolStrip), index, action, buttonType);
		}

		public ToolStripItem  AddToolStripButton(CommonToolStripsEnum toolStrip, CommonItemsEnum index, FreeCL.UI.Actions.Action action, Type buttonType)
		{
			return AddToolStripButton(toolStrip, (int)index, action, buttonType);
		}
		
		public ToolStripItem AddToolStripButton(System.Windows.Forms.ToolStrip owner, int index, FreeCL.UI.Actions.Action action, Type buttonType)
		{
		
			if(owner == null)
				throw new ArgumentNullException("owner");
				
			ToolStripItem netsb = (ToolStripItem)Activator.CreateInstance(buttonType);
			
			if(action != null)
			{
				action.Parent.SetAction(netsb, action);
			}
					
			netsb.DisplayStyle = ToolStripItemDisplayStyle.Image;
			netsb.MergeIndex = index;
			netsb.MergeAction =	MergeAction.Insert;
			
			if(index < owner.Items.Count)
				owner.Items.Insert(index, netsb);					
			else
				owner.Items.Add(netsb);					
					
			SortItems(owner.Items);
			
			return netsb;
		}
		
		public void AddToolStripSeparator(System.Windows.Forms.ToolStrip owner, int index)
		{
			 AddToolStripButton(owner, index, null, typeof(ToolStripSeparator)); 
		}
		
		public void AddToolStripSeparator(CommonToolStripsEnum toolStrip, int index)
		{
			 AddToolStripButton(GetToolStrip(toolStrip), index, null, typeof(ToolStripSeparator));
		}

		public void AddToolStripSeparator(CommonToolStripsEnum toolStrip, CommonItemsEnum index)
		{
			AddToolStripSeparator(toolStrip, (int)index);
		}
		
		
	}
}
