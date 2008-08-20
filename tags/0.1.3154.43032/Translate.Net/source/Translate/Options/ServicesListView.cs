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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Translate
{
	/// <summary>
	/// Description of ServicesListView.
	/// </summary>
	public partial class ServicesListView : FreeCL.Forms.BaseUserControl
	{
		public ServicesListView()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
			
			foreach(Service s in Manager.Services)
			{
				ilServices.Images.Add(s.Name, s.Icon);
			}
		}
		
		void OnLanguageChanged()
		{
			chName.Text  = TranslateString("Name");
			chType.Text  = TranslateString("Type");                          
			chLanguagePair.Text  =  TranslateString("Translation direction");
			chSubject.Text  = TranslateString("Subject");
		}
		
		ServiceItemsDataCollection services = new ServiceItemsDataCollection();
		[Browsable(false)]
		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
		public ServiceItemsDataCollection Services {
			get { return services; }
			set 
			{ 
				services = value; 
				LoadServices();
			}
		}
		
		public bool Sorted
		{
			get{ return lvMain.Sorting == SortOrder.Ascending;}
			set{lvMain.Sorting = value ? SortOrder.Ascending : SortOrder.None;}
		}
		
		class ServiceItemDataContainer
		{
		
			public ServiceItemDataContainer(ServiceItemData serviceItemData)
			{
				this.serviceItemData = serviceItemData;
				GenerateData();
			}
			
			ServiceItemData serviceItemData;
			public ServiceItemData ServiceItemData {
				get { return serviceItemData; }
				set { serviceItemData = value; }
			}
			
			string type;
			public string Type {
				get { return type; }
				set { type = value; }
			}
			
			string name;
			public string Name {
				get { return name; }
				set { name = value; }
			}
			
			string languagePair;
			public string LanguagePair {
				get { return languagePair; }
				set { languagePair = value; }
			}
			
			string subject;
			public string Subject {
				get { return subject; }
				set { subject = value; }
			}
			
			
			void GenerateData()
			{
				name = LangPack.TranslateString(serviceItemData.ServiceItem.Service.FullName);
				type = ServiceSettingsContainer.GetServiceItemType(serviceItemData.ServiceItem);
				languagePair = LangPack.TranslateLanguage(serviceItemData.LanguagePair.From) +
						"->" + 
						LangPack.TranslateLanguage(serviceItemData.LanguagePair.To);
						
				subject = LangPack.TranslateString(serviceItemData.Subject)	;
			}
			
		}
		
		void LoadServices()
		{
			SuspendLayout();
			try
			{
				lvMain.Items.Clear();
				if(services == null)
				{
					return;
				}	
				foreach(ServiceItemData sd in services)
				{
					ServiceItemDataContainer sid = new ServiceItemDataContainer(sd);
					ListViewItem lvi = new ListViewItem();
					lvi.Text = sid.Name;
					lvi.Tag = sid;
					lvi.ImageKey = sd.ServiceItem.Service.Name;
					lvi.SubItems.Add(sid.Type);
					lvi.SubItems.Add(sid.LanguagePair);
					lvi.SubItems.Add(sid.Subject);
					lvi.ToolTipText = sid.Name;
					lvMain.Items.Add(lvi);
				}
				lvMain.Focus();
				if(lvMain.Items.Count > 0)
				{
					lvMain.Items[0].Selected = true;
					lvMain.Items[0].Focused = true;
					
					chName.Width = 200;
					chType.Width = -1;
					chLanguagePair.Width = 150;						
					chSubject.Width = -2;
					
				}
				else
				{
					if(ServiceItemChangedEvent != null)
						ServiceItemChangedEvent(this, new ServiceItemChangedEventArgs(null));
						
					chName.Width = 200;
					chType.Width = -2;
					chLanguagePair.Width = 150;						
					chSubject.Width = -2;
						
				}
			}
			finally
			{
				ResumeLayout();
			}	

			
		}
		
		public ServiceItemData Selected
		{
			get
			{
				if(lvMain.SelectedItems.Count == 0)
					return null;
				ServiceItemDataContainer sidc = lvMain.SelectedItems[0].Tag as ServiceItemDataContainer;
				
				if(sidc == null)
					return null;
					
				return sidc.ServiceItemData;	
			}
			
		}
		
		public event EventHandler<ServiceItemChangedEventArgs> ServiceItemChangedEvent;

		void LvMainSelectedIndexChanged(object sender, EventArgs e)
		{
			if(ServiceItemChangedEvent != null)
			{
				if(lvMain.SelectedItems.Count == 0)
					tDeselectAll.Enabled = true;
				else	
				{
					ServiceItemDataContainer sidc = lvMain.SelectedItems[0].Tag as ServiceItemDataContainer;
				
					if(sidc == null)
						tDeselectAll.Enabled = true;
					else	
					{
						tDeselectAll.Enabled = false;
						ServiceItemChangedEvent(this, new ServiceItemChangedEventArgs(sidc.ServiceItemData));
					}	
				}
			}
		}
		
		public void RemoveSelected()
		{
			if(lvMain.SelectedItems.Count == 0)
				return;
				
			ServiceItemDataContainer sidc = lvMain.SelectedItems[0].Tag as ServiceItemDataContainer;
			
			if(sidc == null)
				return;
				
			ListViewItem lvi = lvMain.SelectedItems[0];
			lvi.Selected = false;
			lvi.Focused = false;
			
			services.Remove(sidc.ServiceItemData);
			lvMain.Items.Remove(lvi);
			
			if(lvMain.Items.Count > 0)
			{
				try
				{ //try to avoid unrepeatable bug
					lvMain.Items[0].Selected = true;
					lvMain.Items[0].Focused = true;
				}
				catch
				{
					try
					{
						lvMain.Items[0].Selected = true;
						lvMain.Items[0].Focused = true;
					}
					catch
					{
					
					}
				}
			}
			else
			{
				if(ServiceItemChangedEvent != null)			
					ServiceItemChangedEvent(this, new ServiceItemChangedEventArgs(null));
			}	
		}
		
		public void RemoveAll()
		{
			services.Clear();
			lvMain.Items.Clear();
			if(ServiceItemChangedEvent != null)
				ServiceItemChangedEvent(this, new ServiceItemChangedEventArgs(null));
		}
		
		public bool CanMoveUp
		{
			get
			{
				return lvMain.SelectedItems.Count > 0 && lvMain.SelectedIndices[0] != 0;
			}
		}
		
		public void MoveUp()
		{
			if(!CanMoveUp)
				return;
				
			ListViewItem lvi = lvMain.SelectedItems[0];	
			lvi.Selected = false;
			lvi.Focused = false;
			int idx = lvi.Index;
			lvMain.Items.RemoveAt(idx);
			lvMain.Items.Insert(idx - 1, lvi);
			lvi.Selected = true;
			lvi.Focused = true;
			ServiceItemDataContainer sidc = lvi.Tag as ServiceItemDataContainer;
			services.RemoveAt(idx);
			services.Insert(idx - 1, sidc.ServiceItemData);
		}
		
		public bool CanMoveDown
		{
			get
			{
				return lvMain.SelectedItems.Count > 0 && lvMain.SelectedIndices[0] != lvMain.Items.Count - 1;
			}
		}
		
		public void MoveDown()
		{
			if(!CanMoveDown)
				return;
				
			ListViewItem lvi = lvMain.SelectedItems[0];	
			lvi.Selected = false;
			lvi.Focused = false;
			int idx = lvi.Index;
			lvMain.Items.RemoveAt(idx);
			lvMain.Items.Insert(idx + 1, lvi);
			lvi.Selected = true;
			lvi.Focused = true;
			ServiceItemDataContainer sidc = lvi.Tag as ServiceItemDataContainer;
			services.RemoveAt(idx);
			services.Insert(idx + 1, sidc.ServiceItemData);
		}
		
		void TDeselectAllTick(object sender, EventArgs e)
		{
			if(ServiceItemChangedEvent != null)
				ServiceItemChangedEvent(this, new ServiceItemChangedEventArgs(null));
			tDeselectAll.Enabled = false;
		}
		
		public new event EventHandler<EventArgs> DoubleClick;
		void LvMainMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if(DoubleClick != null)
				DoubleClick(this, e);
		}
	}
	
	public class ServiceItemChangedEventArgs : EventArgs
	{
		public ServiceItemChangedEventArgs(ServiceItemData serviceItemData)
		{
			this.serviceItemData = serviceItemData;
		}
		
		ServiceItemData serviceItemData;
		public ServiceItemData ServiceItemData {
			get { return serviceItemData; }
			set { serviceItemData = value; }
		}
		
	}

}
