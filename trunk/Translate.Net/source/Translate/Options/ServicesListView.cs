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

namespace Translate.Options
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
			lvMain.Items.Clear();
			if(services == null)
				return;
			foreach(ServiceItemData sd in services)
			{
				ServiceItemDataContainer sid = new ServiceItemDataContainer(sd);
				ListViewItem lvi = new ListViewItem();
				lvi.Text = sid.Name;
				lvi.Tag = sid;
				lvi.SubItems.Add(sid.Type);
				lvi.SubItems.Add(sid.LanguagePair);
				lvi.SubItems.Add(sid.Subject);
				lvi.ToolTipText = sid.Name;
				lvMain.Items.Add(lvi);
			}
			lvMain.Focus();
			lvMain.Items[0].Selected = true;
			lvMain.Items[0].Focused = true;
			
			//LvProfilesSelectedIndexChanged(lvMain, new EventArgs());
			chName.Width = 150;
			chSubject.Width = -1;
			chType.Width = -1;
			chLanguagePair.Width = -1;
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
	}
}
