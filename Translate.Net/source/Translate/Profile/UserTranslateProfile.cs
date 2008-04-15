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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;


namespace Translate
{
	/// <summary>
	/// Description of UserTranslateProfile.
	/// </summary>
	public class UserTranslateProfile : TranslateProfile, ICloneable
	{
		public UserTranslateProfile()
		{
		}
		
		public override object Clone()
		{
			UserTranslateProfile result = new UserTranslateProfile();
			result.Name = Name;
			result.Position = Position;
			result.Subjects.AddRange(Subjects);
			result.History.AddRange(History);
			result.SelectedLanguagePair = SelectedLanguagePair;
			foreach(ServiceItemsSortDataCollection d in SortData)
				result.SortData.Add(d);
			result.TranslationDirection = TranslationDirection;	
			result.Subject = Subject;
			
			foreach(ServiceItemData sid in services)
				result.Services.Add(sid);
				
			foreach(ServiceItemData d in DisabledServiceItems)
				result.DisabledServiceItems.Add(d); 
				
			result.ShowLanguages = ShowLanguages;
			result.ShowServices = ShowServices;
			result.ShowSubjects = ShowSubjects;
			
			return result;	
		}
		
		ServiceItemsDataCollection services = new ServiceItemsDataCollection();
		public ServiceItemsDataCollection Services {
			get { return services; }
			set { services = value; }
		}
		
		LanguagePair translationDirection = new LanguagePair();
		public LanguagePair TranslationDirection {
			get { return translationDirection; }
			set { translationDirection = value; }
		}

		string subject = SubjectConstants.Any;
		[System.ComponentModel.DefaultValueAttribute(SubjectConstants.Any)]
		public string Subject {
			get { return subject; }
			set { subject = value; }
		}
		
		bool showSubjects;
		public bool ShowSubjects {
			get { return showSubjects; }
			set { showSubjects = value; }
		}
		
		bool showLanguages;
		public bool ShowLanguages {
			get { return showLanguages; }
			set { showLanguages = value; }
		}

		bool showServices;
		public bool ShowServices {
			get { return showServices; }
			set { showServices = value; }
		}
		
		
		public void AfterLoad()
		{
			ServiceItemsDataCollection sids_to_delete = new ServiceItemsDataCollection();
			foreach(ServiceItemData sid in services)
			{
				try
				{
					sid.AttachToServiceItem();
				}
				catch(Exception e)
				{
					MessageBox.Show("Service not found with error : " + 
							e.Message + System.Environment.NewLine +
							"Service will be deleted from profile.",
						"Error on loading services in profile : " + Name,  
						MessageBoxButtons.OK, 
						MessageBoxIcon.Error);
					sids_to_delete.Add(sid);	
				}
			}
			
			foreach(ServiceItemData sid in sids_to_delete)
				services.Remove(sid);
		}
		
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public override SubjectCollection GetSupportedSubjects()
		{
			SubjectCollection subjects = new SubjectCollection();
			foreach(ServiceItemData sid in services)
			{
				if(!subjects.Contains(sid.Subject))
					subjects.Add(sid.Subject);
			}
			return subjects;
		}

		public override ReadOnlyLanguagePairCollection GetLanguagePairs()
		{
			LanguagePairCollection result = new LanguagePairCollection();
			
			foreach(ServiceItemData sid in services)
			{
				if(Subjects.Contains(sid.Subject) && !result.Contains(sid.LanguagePair))
				{
					result.Add(sid.LanguagePair);
				}
			}
			
			return new ReadOnlyLanguagePairCollection(result);
		}
		
		public override ReadOnlyServiceSettingCollection GetServiceSettings(string phrase, LanguagePair languagePair)
		{
			ServiceSettingCollection result = new ServiceSettingCollection();
			
			foreach(ServiceItemData sid in services)
			{
				if( 
					(sid.LanguagePair.From == languagePair.From || languagePair.From == Language.Any) &&
					(sid.LanguagePair.To == languagePair.To || languagePair.To == Language.Any)
					  )
				{				
						
					if(Subjects.Contains(sid.Subject))
					{
						ServiceItemSetting tsetting = new ServiceItemSetting(sid.LanguagePair, sid.Subject, sid.ServiceItem, TranslateOptions.Instance.GetNetworkSetting(sid.ServiceItem.Service));
						result.Add(tsetting);
					}
				}
			}
			return new ReadOnlyServiceSettingCollection(result);
		}
		
	}
	
	public class UserTranslateProfilesCollection :  List<UserTranslateProfile>
	{
	
	}
	
}
