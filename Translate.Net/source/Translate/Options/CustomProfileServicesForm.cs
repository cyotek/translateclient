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
		public CustomProfileServicesForm()
		{
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
			
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
			List<string> subjects = new List<string>();
			foreach(ServiceItem si in Manager.ServiceItems)
			{
				foreach(string subject in si.SupportedSubjects)
				{
					if(!subjects.Contains(subject))
					{
						subjects.Add(subject);
						SubjectContainer sc = new SubjectContainer(subject, LangPack.TranslateString(subject));
						cbSubject.Items.Add(sc);
					}	
				}
			}
			
			foreach(object o in cbSubject.Items)
			{
				SubjectContainer sc = o as SubjectContainer;
				if(sc.Subject == SubjectConstants.Common)
				{
					cbSubject.SelectedItem = sc;
					break;
				}	
			}
			
			initialized = true;
			CbFromSelectedIndexChanged(null, new EventArgs());
		}
		
		void OnLanguageChanged()
		{
			Text = TranslateString("Edit services");
			lLangPair.Text  = TranslateString("Translation direction");
		}		
		
		void CustomProfileServicesFormSizeChanged(object sender, EventArgs e)
		{
			pLeft.Width = (ClientSize.Width - pCenter.Width - pServiceControl.Width)/2;
		}
		
		bool initialized;
		void LoadSources()
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
						if(si.SupportedSubjects.Contains(subject))
						{
							ServiceItemData sid = new ServiceItemData(si, kvp.Key, subject);
							services.Add(sid);
						}
					}
				}
			}
			lvSource.Services = services;
		}
		
		
		void CbFromSelectedIndexChanged(object sender, EventArgs e)
		{
			if(!initialized)
				return;
				
			LoadSources();	
		}
	}
}
