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
 * Portions created by the Initial Developer are Copyright (C) 2007-2008
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
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	/// <summary>
	/// Description of DefaultProfile.
	/// </summary>
	/// 
	[Serializable()]
	public class DefaultTranslateProfile : TranslateProfile, ICloneable
	{
		public DefaultTranslateProfile()
		{
			Name = "Default";
		}
		
		public override object Clone()
		{
			DefaultTranslateProfile result = new DefaultTranslateProfile();
			result.Name = Name;
			result.Position = Position;
			result.IncludeMonolingualDictionaryInTranslation = IncludeMonolingualDictionaryInTranslation;			
			result.Subjects.AddRange(Subjects);
			result.History.AddRange(History);
			result.SelectedLanguagePair = SelectedLanguagePair;
			foreach(ServiceItemsSortDataCollection d in SortData)
				result.SortData.Add(d);
			foreach(ServiceItemData d in DisabledServiceItems)
				result.DisabledServiceItems.Add(d); 
			result.DisabledSourceLanguages.AddRange(DisabledSourceLanguages); 	
			result.DisabledTargetLanguages.AddRange(DisabledTargetLanguages);
			result.DisabledLanguagesAlreadySet = DisabledLanguagesAlreadySet;
			return result;	
		}
		
		bool includeMonolingualDictionaryInTranslation = true;
		public bool IncludeMonolingualDictionaryInTranslation {
			get { return includeMonolingualDictionaryInTranslation; }
			set { includeMonolingualDictionaryInTranslation = value; }
		}
		
		LanguageCollection disabledSourceLanguages = new LanguageCollection();
		public LanguageCollection DisabledSourceLanguages {
			get { return disabledSourceLanguages; }
			set { disabledSourceLanguages = value; }
		}
		
		LanguageCollection disabledTargetLanguages = new LanguageCollection();
		public LanguageCollection DisabledTargetLanguages {
			get { return disabledTargetLanguages; }
			set { disabledTargetLanguages = value; }
		}
		
		bool disabledLanguagesAlreadySet = false;
		public bool DisabledLanguagesAlreadySet {
			get { return disabledLanguagesAlreadySet; }
			set { disabledLanguagesAlreadySet = value; }
		}
				
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public override SubjectCollection GetSupportedSubjects()
		{
			if(!disabledLanguagesAlreadySet)
				return Manager.Subjects;
			else
			{
				SubjectCollection subjects = new SubjectCollection();
				foreach(ServiceItem item in Manager.ServiceItems)
				{
					foreach(LanguagePair lp in item.SupportedTranslations)
					{
						if(disabledSourceLanguages.Contains(lp.From))
							continue;
		
						if(disabledTargetLanguages.Contains(lp.To))
							continue;
					
						foreach(string subject in item.SupportedSubjects)
						{
							if(!subjects.Contains(subject))
								subjects.Add(subject);
						}
						
						break;
					}
				}
				return subjects;
			}
		}

		public override ReadOnlyLanguagePairCollection GetLanguagePairs()
		{
			LanguagePairCollection result = new LanguagePairCollection();
			
			foreach(ServiceItem item in Manager.ServiceItems)
			{
				foreach(string subject in item.SupportedSubjects)
				{
					if(Subjects.Contains(subject))
					{
						foreach(LanguagePair lp in item.SupportedTranslations)
						{
							if(!result.Contains(lp))
							{
								if(disabledSourceLanguages.Contains(lp.From))
									continue;
	
								if(disabledTargetLanguages.Contains(lp.To))
									continue;
							
								result.Add(lp);
							}	
						}
					}
				}
			}
			
			return new ReadOnlyLanguagePairCollection(result);
		}
		
		public override ReadOnlyServiceSettingCollection GetServiceSettings(string phrase, LanguagePair languagePair)
		{
			ServiceSettingCollection result = new ServiceSettingCollection();
			
			foreach (KeyValuePair<LanguagePair, ServiceItemsCollection> kvp in Manager.LanguagePairServiceItems)
			{
				if(disabledSourceLanguages.Contains(kvp.Key.From))
					continue;
	
				if(disabledTargetLanguages.Contains(kvp.Key.To))
					continue;
			
				foreach(ServiceItem si in kvp.Value)
				{
					if( 
						(kvp.Key.From == languagePair.From || languagePair.From == Language.Any) &&
						(kvp.Key.To == languagePair.To || languagePair.To == Language.Any ||
						(IncludeMonolingualDictionaryInTranslation && si is MonolingualDictionary)
						)
					  )
					{				
						
						foreach(string subject in si.SupportedSubjects)
						{
							if(Subjects.Contains(subject))
							{
								ServiceItemSetting tsetting = new ServiceItemSetting(kvp.Key, subject, si, TranslateOptions.Instance.GetNetworkSetting(si.Service));
								result.Add(tsetting);
							}
						}
					}
				}
			}
			return new ReadOnlyServiceSettingCollection(result);
		}
	}
}
