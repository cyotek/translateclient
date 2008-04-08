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
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of CustomProfileServicesForm.
	/// </summary>
	public partial class CustomProfileServicesForm : FreeCL.Forms.BaseForm
	{
		public CustomProfileServicesForm(UserTranslateProfile profile)
		{
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);

			this.profile = profile;
			ServiceItemsDataCollection services = new ServiceItemsDataCollection();
			foreach(ServiceItemData sid in profile.Services)
				services.Add(sid);	
			lvCurrent.Services = services;	
			
			cbFrom.Items.Clear();
			cbTo.Items.Clear();
			
			for(int i = 0; i < (int)Language.Last; i++)
			{
				LanguageDataContainer ld = new LanguageDataContainer((Language)i, LangPack.TranslateLanguage((Language)i));
				cbFrom.Items.Add(ld);
				cbTo.Items.Add(ld);
			}
			
			cbFrom.SelectedIndex = 0;
			cbTo.SelectedIndex = 0;
			
			//cbSubject
			foreach(string subject in Manager.Subjects)
			{
				SubjectContainer sc = new SubjectContainer(subject, LangPack.TranslateString(subject));
				cbSubject.Items.Add(sc);
			}	
			
			SubjectContainer sc1 = new SubjectContainer(SubjectConstants.Any, "+" + LangPack.TranslateString(SubjectConstants.Any));
			cbSubject.Items.Add(sc1);
			
			cbSubject.SelectedIndex = 0;
			
			for(int i = 0; i < cbFrom.Items.Count; i++)
			{
				LanguageDataContainer ld = cbFrom.Items[i] as LanguageDataContainer;
					
				if(ld.Language == profile.TranslationDirection.From)
					cbFrom.SelectedItem = ld;
	
				if(ld.Language == profile.TranslationDirection.To)
					cbTo.SelectedItem = ld;
			}
				
			for(int i = 0; i < cbSubject.Items.Count; i++)
			{
				SubjectContainer sc  = cbSubject.Items[i] as SubjectContainer;
				if(profile.Subject == sc.Subject)
				{
					cbSubject.SelectedItem = sc;
					break;
				}
			}
			
			
			initialized = true;
			CbFromSelectedIndexChanged(null, new EventArgs());
			
			serviceStatusSource.Visible = false;
			serviceStatusCurrent.Visible = false;
			
		}
		
		void OnLanguageChanged()
		{
			Text = TranslateString("Edit services");
			lLangPair.Text  = TranslateString("Translation direction");
			lSubject.Text  = TranslateString("Subject");
			
			aRemoveService.Hint = TranslateString("Remove service");
			aMoveServiceUp.Hint = TranslateString("Move service up");
			aMoveServiceDown.Hint = TranslateString("Move service down");
			aAddSelected.Hint = TranslateString("Add selected service");
			aAddAll.Hint = TranslateString("Add all services");
			aClearAll.Hint = TranslateString("Remove all services");
			
			gbFilter.Text  = TranslateString("Services filter");
			gbSource.Text  = TranslateString("Available services");
			gbCurrent.Text  = TranslateString("Selected services");
			
		}		
		
		void CustomProfileServicesFormSizeChanged(object sender, EventArgs e)
		{
			pLeft.Width = (ClientSize.Width - pCenter.Width - pServiceControl.Width)/2;
		}
		
		bool initialized;
		void LoadSources()
		{
			UseWaitCursor = true;
			Application.DoEvents();
			LockUpdate(true);
			try
			{
				lvSource.Services = null;
				
				ServiceItemsDataCollection services = new ServiceItemsDataCollection();
				
				Language from = (cbFrom.SelectedItem as LanguageDataContainer).Language;
				Language to = (cbTo.SelectedItem as LanguageDataContainer).Language;
				LanguagePair languagePair = new LanguagePair(from, to);
				string subject = (cbSubject.SelectedItem as SubjectContainer).Subject;
				
				
				foreach (KeyValuePair<LanguagePair, ServiceItemsCollection> kvp in Manager.LanguagePairServiceItems)
				{
					if( 
						(kvp.Key.From == languagePair.From || languagePair.From == Language.Any) &&
						(kvp.Key.To == languagePair.To || languagePair.To == Language.Any)
					  )
					{				
						foreach(ServiceItem si in kvp.Value)
						{
							if(subject != SubjectConstants.Any)
							{
								if(si.SupportedSubjects.Contains(subject))
								{
									ServiceItemData sid = new ServiceItemData(si, kvp.Key, subject);
									if(!profile.Services.Contains(sid))
										services.Add(sid);
								}
							}
							else
							{
								foreach(string siSubject in si.SupportedSubjects)
								{
									ServiceItemData sid = new ServiceItemData(si, kvp.Key, siSubject);
									if(!profile.Services.Contains(sid))
										services.Add(sid);
								}
							}
						}
					}
				}
				lvSource.Services = services;
			}
			finally
			{
				UseWaitCursor = false;
				LockUpdate(false);
			}	
		}
		
		void CbFromSelectedIndexChanged(object sender, EventArgs e)
		{
			if(!initialized)
				return;
			
			LoadSources();	
		}
		
		void ShowStatus(ServiceItemData serviceItemData, ServiceStatusControl statusControl)		
		{
			if(serviceItemData == null)
			{
				statusControl.Visible = false;
			}
			else
			{
				statusControl.Visible = true;
				statusControl.ShowLanguage = true;
				ServiceSettingsContainer sc = new ServiceSettingsContainer(
					new ServiceSetting(serviceItemData.LanguagePair, 
										serviceItemData.Subject, serviceItemData.ServiceItem,
										null),
					true);
				statusControl.Status = sc;
			}
		}
		void LvSourceServiceItemChangedEvent(object sender, ServiceItemChangedEventArgs e)
		{
			ShowStatus(e.ServiceItemData, serviceStatusSource);			
		}
		
		void LvCurrentServiceItemChangedEvent(object sender, ServiceItemChangedEventArgs e)
		{
			ShowStatus(e.ServiceItemData, serviceStatusCurrent);
		}
		
		UserTranslateProfile profile;
		public UserTranslateProfile Profile {
			get { return profile; }
			set { 
					profile = value; 
					//lvCurrent.Services = profile.Services;
				}
		}
		
		void AAddSelectedExecute(object sender, EventArgs e)
		{
			ServiceItemsDataCollection services = lvCurrent.Services;
			services.Add(lvSource.Selected);
			lvCurrent.Services = services;
			lvSource.RemoveSelected();
		}
		
		void AAddAllExecute(object sender, EventArgs e)
		{
			ServiceItemsDataCollection services = lvCurrent.Services;
			foreach(ServiceItemData sid in lvSource.Services)
					services.Add(sid);
			lvCurrent.Services = services;
			lvSource.RemoveAll();
		}
		
		void AAddSelectedUpdate(object sender, EventArgs e)
		{
			aAddAll.Enabled = lvSource.Services.Count > 0;
			aAddSelected.Enabled = aAddAll.Enabled;
		}

		void ARemoveServiceUpdate(object sender, EventArgs e)
		{
			aRemoveService.Enabled = lvCurrent.Selected != null;	
		}
		
		void ARemoveServiceExecute(object sender, EventArgs e)
		{
			Language from = (cbFrom.SelectedItem as LanguageDataContainer).Language;
			Language to = (cbTo.SelectedItem as LanguageDataContainer).Language;
			LanguagePair languagePair = new LanguagePair(from, to);
			string subject = (cbSubject.SelectedItem as SubjectContainer).Subject;
		
			if(lvCurrent.Selected.LanguagePair == languagePair &&
				(lvCurrent.Selected.Subject == subject || subject == SubjectConstants.Any)
			)
			{
				ServiceItemsDataCollection services = lvSource.Services;
				services.Add(lvCurrent.Selected);
				lvSource.Services = services;
			}
			
			lvCurrent.RemoveSelected();
		}
		
		void AClearAllUpdate(object sender, EventArgs e)
		{
			aClearAll.Enabled = lvCurrent.Selected != null;	
		}
		
		void AClearAllExecute(object sender, EventArgs e)
		{
			Language from = (cbFrom.SelectedItem as LanguageDataContainer).Language;
			Language to = (cbTo.SelectedItem as LanguageDataContainer).Language;
			LanguagePair languagePair = new LanguagePair(from, to);
			string subject = (cbSubject.SelectedItem as SubjectContainer).Subject;
		
			ServiceItemsDataCollection services = lvSource.Services;
			foreach(ServiceItemData sid in lvCurrent.Services)
			{
				if(sid.LanguagePair == languagePair &&
					(sid.Subject == subject || subject == SubjectConstants.Any)
				)
					services.Add(sid);
			}
			lvSource.Services = services;
		
			lvCurrent.RemoveAll();
		}
		
		void AMoveServiceUpExecute(object sender, EventArgs e)
		{
			lvCurrent.MoveUp();
		}
		
		void AMoveServiceUpUpdate(object sender, EventArgs e)
		{
			aMoveServiceUp.Enabled = lvCurrent.CanMoveUp;
		}
		
		void AMoveServiceDownExecute(object sender, EventArgs e)
		{
			lvCurrent.MoveDown();
		}
		
		void AMoveServiceDownUpdate(object sender, EventArgs e)
		{
			aMoveServiceDown.Enabled = lvCurrent.CanMoveDown;
		}
		
		void BOkClick(object sender, EventArgs e)
		{
			ServiceItemsDataCollection services = new ServiceItemsDataCollection();
			foreach(ServiceItemData sid in lvCurrent.Services)
				services.Add(sid);	
			profile.Services = services;	
		}
		
	}
}
