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
using FreeCL.Forms;
using System.Net;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of CustomProfilesControl.
	/// </summary>
	public partial class CustomProfilesControl : FreeCL.Forms.BaseOptionsControl
	{
		public CustomProfilesControl()
		{
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
		}
		
		void OnLanguageChanged()
		{
			aMoveProfileUp.Hint = TranslateString("Move profile up");
			aMoveProfileDown.Hint = TranslateString("Move profile down");
			chName.Text  = TranslateString("Name");
			chDirection.Text  = TranslateString("Translation direction");
			chSubject.Text  = TranslateString("Subject");
			lName.Text  = TranslateString("Name");
			cbIncludeMonolingualDictionaryInTranslation.Text = TranslateString("Include monolingual dictionaries in translation");
			aAddProfile.Hint = TranslateString("Add new user profile");
			aRemoveProfile.Hint = TranslateString("Remove user profile");
			tpDefaultOptions.Text = TranslateString("Options");
			tpOptions.Text = TranslateString("Options");
			tpServices.Text = TranslateString("Services");
			bChangeName.Text = TranslateString("Set profile properties");
			lLangPair.Text  = TranslateString("Translation direction");
			lSubject.Text  = TranslateString("Subject");
			
			aEditServices.Hint = TranslateString("Edit services");
			aRemoveService.Hint = TranslateString("Remove service");
			aMoveServiceUp.Hint = TranslateString("Move service up");
			aMoveServiceDown.Hint = TranslateString("Move service down");
			
			
			tpLanguageSelector.Text  = TranslateString("View");
			cbShowServices.Text  = TranslateString("Show services list");
			cbShowLanguages.Text  = TranslateString("Show languages list");
			cbShowSubjects.Text  = TranslateString("Show subjects list");
			
			
			foreach(ListViewItem lvi in lvProfiles.Items)
			{
				if(lvi.Tag == defaultProfile)
				{
					lvi.Text = TranslateString(TranslateOptions.Instance.DefaultProfile.Name);
				}	
				else
				{
					UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;		
					if(pf != null)
					{
						lvi.SubItems[1].Text = (LangPack.TranslateLanguage(pf.TranslationDirection.From) +
							 " -> " + 
							 LangPack.TranslateLanguage(pf.TranslationDirection.To)
							);
						lvi.SubItems[2].Text = 	LangPack.TranslateString(pf.Subject);
					}	
				}
				
			}
			
			
			chDirection.Width = -2;	
			chSubject.Width = -2;
			
			InitUserProfileControls();
		}

		TranslateProfilesCollection profiles = new TranslateProfilesCollection();
		DefaultTranslateProfile defaultProfile; 
		string currentProfileName;
		
		public override void Init()
		{
			
			defaultProfile = (DefaultTranslateProfile)TranslateOptions.Instance.DefaultProfile.Clone();
			cbIncludeMonolingualDictionaryInTranslation.Checked = defaultProfile.IncludeMonolingualDictionaryInTranslation;			
			
			currentProfileName = TranslateOptions.Instance.CurrentProfile.Name;
			
			profiles.Clear();
			
			foreach(TranslateProfile pf in TranslateOptions.Instance.Profiles)
			{
				if(pf == TranslateOptions.Instance.DefaultProfile)
					profiles.Add(defaultProfile);
				else 
					profiles.Add((TranslateProfile)pf.Clone());
			}
			
			lvProfiles.Items.Clear();
			foreach(TranslateProfile pf in profiles)
			{
				ListViewItem lvi = new ListViewItem();
				if(pf == defaultProfile)
					lvi.Text = TranslateString(pf.Name);
				else
					lvi.Text = pf.Name;
				lvi.Tag = pf;
				
				UserTranslateProfile upf = pf as UserTranslateProfile;
				if(upf != null)
				{
					lvi.SubItems.Add(LangPack.TranslateLanguage(upf.TranslationDirection.From) +
						 " -> " + 
						 LangPack.TranslateLanguage(upf.TranslationDirection.To)
						);
					lvi.SubItems.Add(LangPack.TranslateString(upf.Subject));	
				}	
				else
				{
					lvi.SubItems.Add("");
					lvi.SubItems.Add("");
				}
				lvProfiles.Items.Add(lvi);
			}
			lvProfiles.Focus();
			lvProfiles.Items[0].Selected = true;
			lvProfiles.Items[0].Focused = true;

			InitUserProfileControls();
			
			chDirection.Width = -2;	
			chSubject.Width = -2;
		}
		
		void InitUserProfileControls()
		{
			cbFrom.Items.Clear();
			cbTo.Items.Clear();
			
			for(int i = 0; i < (int)Language.Last; i++)
			{
				LanguageDataContainer ld = new LanguageDataContainer((Language)i, LangPack.TranslateLanguage((Language)i));
				cbFrom.Items.Add(ld);
				cbTo.Items.Add(ld);
			}

			cbSubject.Items.Clear();
			foreach(string subject in Manager.Subjects)
			{
				SubjectContainer sc = new SubjectContainer(subject, LangPack.TranslateString(subject));
				cbSubject.Items.Add(sc);
			}	
			
			SubjectContainer sc1 = new SubjectContainer(SubjectConstants.Any, LangPack.TranslateString(SubjectConstants.Any));
			cbSubject.Items.Add(sc1);
			
			changed = false;
			LvProfilesSelectedIndexChanged(lvProfiles, new EventArgs());
		}

		public override void Apply()
		{
			TranslateOptions.Instance.Profiles.Clear();
			TranslateOptions.Instance.Profiles.AddRange(profiles);
			
			defaultProfile.IncludeMonolingualDictionaryInTranslation = cbIncludeMonolingualDictionaryInTranslation.Checked;
			TranslateOptions.Instance.DefaultProfile = defaultProfile;

			TranslateOptions.Instance.CurrentProfile = defaultProfile;			
			foreach(TranslateProfile pf in TranslateOptions.Instance.Profiles)
			{
				if(pf.Name == currentProfileName)
				{
					TranslateOptions.Instance.CurrentProfile = pf;
				}	
				
				if(pf is UserTranslateProfile)
				{
					pf.Subjects.Clear();
					pf.Subjects.AddRange(pf.GetSupportedSubjects());
					pf.SortData.Clear();
				}
			}
			
			(TranslateMainForm.Instance as TranslateMainForm).UpdateProfiles();
			changed = false;
		}
		
		bool changed;
		public override bool IsChanged()
		{
			return changed;
		}
		
		void LvProfilesSelectedIndexChanged(object sender, EventArgs e)
		{
			aEditServices.Enabled = false;
			if(lvProfiles.SelectedItems.Count == 0)
				return;
			TranslateProfile pf = lvProfiles.SelectedItems[0].Tag as TranslateProfile;
			UserTranslateProfile upf = pf as UserTranslateProfile;
			
			if(pf == defaultProfile)
			{
				if(!tcOptions.TabPages.Contains(tpDefaultOptions))
					tcOptions.TabPages.Add(tpDefaultOptions);
				
				tcOptions.TabPages.Remove(tpOptions);
				tcOptions.TabPages.Remove(tpServices);
				tcOptions.TabPages.Remove(tpLanguageSelector);
			}
			else if(upf != null)
			{
				if(!tcOptions.TabPages.Contains(tpServices))
					tcOptions.TabPages.Add(tpServices);
			
				if(!tcOptions.TabPages.Contains(tpOptions))
					tcOptions.TabPages.Add(tpOptions);

				if(!tcOptions.TabPages.Contains(tpLanguageSelector))
					tcOptions.TabPages.Add(tpLanguageSelector);

				tcOptions.TabPages.Remove(tpDefaultOptions);
				lProfileName.Text = pf.Name;
				lvServices.Services = upf.Services;
			}
			
			aEditServices.Enabled = pf != defaultProfile;
			
			if(upf != null)
			{
				ignoreLanguageChange = true;
				for(int i = 0; i < cbFrom.Items.Count; i++)
				{
					LanguageDataContainer ld = cbFrom.Items[i] as LanguageDataContainer;
					
					if(ld.Language == upf.TranslationDirection.From)
						cbFrom.SelectedItem = ld;
	
					if(ld.Language == upf.TranslationDirection.To)
						cbTo.SelectedItem = ld;
				}
				
				for(int i = 0; i < cbSubject.Items.Count; i++)
				{
					SubjectContainer sc  = cbSubject.Items[i] as SubjectContainer;
					if(upf.Subject == sc.Subject)
					{
						cbSubject.SelectedItem = sc;
						break;
					}
				}
				
				cbShowServices.Checked = upf.ShowServices;
				cbShowSubjects.Checked = upf.ShowSubjects;
				cbShowLanguages.Checked = upf.ShowLanguages;
				
				ignoreLanguageChange = false;
			}
		}

		
		void AAddProfileExecute(object sender, EventArgs e)
		{
			
			UserTranslateProfile pf = new UserTranslateProfile();
			
			SetProfileNameForm nameForm = new SetProfileNameForm(pf, profiles); 
			DialogResult dr = nameForm.ShowDialog(FindForm());
			nameForm.Dispose();
			if(dr == DialogResult.Cancel)
				return;
				
			profiles.Add(pf);
			
			ListViewItem lvi = new ListViewItem();
			lvi.Text = pf.Name;
			lvi.Tag = pf;
			
			lvi.SubItems.Add(LangPack.TranslateLanguage(pf.TranslationDirection.From) +
						 " -> " + 
						 LangPack.TranslateLanguage(pf.TranslationDirection.To)
						);
			lvi.SubItems.Add(LangPack.TranslateString(pf.Subject));	
			
			lvProfiles.Items.Add(lvi);
			lvProfiles.Focus();
			
			foreach(ListViewItem lv in lvProfiles.SelectedItems)
			{
				lv.Selected = false;
				lv.Focused = false;
			}	
				
			lvi.Selected = true;
			lvi.Focused = true;
			
			AEditServicesExecute(this, new EventArgs());
		}
		
		void BChangeNameClick(object sender, EventArgs e)
		{
			ListViewItem lvi = lvProfiles.SelectedItems[0];	
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;		
			
			string oldName = pf.Name;
			SetProfileNameForm nameForm = new SetProfileNameForm(pf as UserTranslateProfile, profiles); 
			
			DialogResult dr = nameForm.ShowDialog(FindForm());
			nameForm.Dispose();
			if(dr == DialogResult.Cancel)
			{
				pf.Name = oldName;
				return;
			}	
				
			lvi.Text = pf.Name;
			lProfileName.Text = pf.Name;
			
			lvi.SubItems[1].Text = (LangPack.TranslateLanguage(pf.TranslationDirection.From) +
					 " -> " + 
					 LangPack.TranslateLanguage(pf.TranslationDirection.To)
					);
			lvi.SubItems[2].Text = LangPack.TranslateString(pf.Subject);		
			
			
			LvProfilesSelectedIndexChanged(lvProfiles, new EventArgs());
		}
		
		void ARemoveProfileExecute(object sender, EventArgs e)
		{
			ListViewItem lvi = lvProfiles.SelectedItems[0];
			if(MessageBox.Show(FindForm(), string.Format(TranslateString("The profile {0} will be deleted.\r\nAre you sure ?"), lvi.Text) , Constants.AppName, MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				TranslateProfile pf = lvi.Tag as TranslateProfile;
				profiles.Remove(pf);
				lvi.Selected = false;
				lvi.Focused = false;
				lvProfiles.Items.Remove(lvi);
				lvProfiles.Items[0].Selected = true;
				lvProfiles.Items[0].Focused = true;
			}
		}
		
		void ARemoveProfileUpdate(object sender, EventArgs e)
		{
			if(lvProfiles.SelectedItems.Count > 0)
			{
				TranslateProfile pf = lvProfiles.SelectedItems[0].Tag as TranslateProfile;
				aRemoveProfile.Enabled = pf != defaultProfile;
			}
			else
				aRemoveProfile.Enabled = false;
		}
		
		void AMoveProfileUpExecute(object sender, EventArgs e)
		{
			ListViewItem lvi = lvProfiles.SelectedItems[0];	
			lvi.Selected = false;
			lvi.Focused = false;
			int idx = lvi.Index;
			lvProfiles.Items.RemoveAt(idx);
			lvProfiles.Items.Insert(idx - 1, lvi);
			lvi.Selected = true;
			lvi.Focused = true;
			TranslateProfile pf = lvi.Tag as TranslateProfile;
			profiles.RemoveAt(idx);
			profiles.Insert(idx - 1, pf);
		}
		
		void AMoveProfileUpUpdate(object sender, EventArgs e)
		{
			aMoveProfileUp.Enabled = lvProfiles.SelectedIndices[0] != 0;

		}
		
		void AMoveProfileDownExecute(object sender, EventArgs e)
		{
			ListViewItem lvi = lvProfiles.SelectedItems[0];	
			lvi.Selected = false;
			lvi.Focused = false;
			int idx = lvi.Index;
			lvProfiles.Items.RemoveAt(idx);
			lvProfiles.Items.Insert(idx + 1, lvi);
			lvi.Selected = true;
			lvi.Focused = true;
			TranslateProfile pf = lvi.Tag as TranslateProfile;
			profiles.RemoveAt(idx);
			profiles.Insert(idx + 1, pf);
		}
		
		void AMoveProfileDownUpdate(object sender, EventArgs e)
		{
			aMoveProfileDown.Enabled = lvProfiles.SelectedIndices[0] != lvProfiles.Items.Count - 1;
		}
		
		
		void CbIncludeMonolingualDictionaryInTranslationCheckedChanged(object sender, EventArgs e)
		{
			changed = true;
		}
		
		bool ignoreLanguageChange;
		void CbFromSelectedIndexChanged(object sender, EventArgs e)
		{
			if(ignoreLanguageChange)
				return;
			if(lvProfiles.SelectedItems.Count != 1)	
				return;
				
			ListViewItem lvi = lvProfiles.SelectedItems[0];		
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;
			
			if(pf == null)
				return;
				
			LanguageDataContainer ld = cbFrom.SelectedItem as LanguageDataContainer;
			pf.TranslationDirection.From = ld.Language;

			lvi.SubItems[1].Text = (LangPack.TranslateLanguage(pf.TranslationDirection.From) +
					 " -> " + 
					 LangPack.TranslateLanguage(pf.TranslationDirection.To)
					);
			
		}
		
		void CbToSelectedIndexChanged(object sender, EventArgs e)
		{
			if(ignoreLanguageChange)
				return;
				
			if(lvProfiles.SelectedItems.Count != 1)	
				return;
				
			ListViewItem lvi = lvProfiles.SelectedItems[0];		
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;
			
			if(pf == null)
				return;

			LanguageDataContainer ld = cbTo.SelectedItem as LanguageDataContainer;
			pf.TranslationDirection.To = ld.Language;
			
			lvi.SubItems[1].Text = (LangPack.TranslateLanguage(pf.TranslationDirection.From) +
					 " -> " + 
					 LangPack.TranslateLanguage(pf.TranslationDirection.To)
					);
			
		}
		
		void CbSubjectSelectedIndexChanged(object sender, EventArgs e)
		{
			if(ignoreLanguageChange)
				return;
				
			if(lvProfiles.SelectedItems.Count != 1)	
				return;
				
			ListViewItem lvi = lvProfiles.SelectedItems[0];		
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;
			
			if(pf == null)
				return;

			SubjectContainer sc = cbSubject.SelectedItem as SubjectContainer;
			pf.Subject = sc.Subject;
			
			lvi.SubItems[2].Text = LangPack.TranslateString(sc.Subject);
		}
		
		
		void AEditServicesExecute(object sender, EventArgs e)
		{
			ListViewItem lvi = lvProfiles.SelectedItems[0];		
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;
		
			CustomProfileServicesForm form = new CustomProfileServicesForm(pf);
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				lvServices.Services = (pf as UserTranslateProfile).Services;
			}
			form.Dispose();
		}
		
		void ARemoveServiceUpdate(object sender, EventArgs e)
		{
			aRemoveService.Enabled = lvServices.Selected != null;	
		}
		
		void ARemoveServiceExecute(object sender, EventArgs e)
		{
			lvServices.RemoveSelected();
		}
		
		void AMoveServiceUpExecute(object sender, EventArgs e)
		{
			lvServices.MoveUp();
		}
		
		void AMoveServiceUpUpdate(object sender, EventArgs e)
		{
			aMoveServiceUp.Enabled = lvServices.CanMoveUp;
		}
		
		void AMoveServiceDownExecute(object sender, EventArgs e)
		{
			lvServices.MoveDown();
		}
		
		void AMoveServiceDownUpdate(object sender, EventArgs e)
		{
			aMoveServiceDown.Enabled = lvServices.CanMoveDown;
		}
		
		
		void CbShowServicesCheckedChanged(object sender, EventArgs e)
		{
			if(ignoreLanguageChange)			
				return;
				
			ListViewItem lvi = lvProfiles.SelectedItems[0];		
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;
				
			pf.ShowServices = cbShowServices.Checked;
			
			changed = true;
		}
		
		void CbShowLanguagesCheckedChanged(object sender, EventArgs e)
		{
			if(ignoreLanguageChange)			
				return;
				
			ListViewItem lvi = lvProfiles.SelectedItems[0];		
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;
				
			pf.ShowLanguages = cbShowLanguages.Checked;
			
			changed = true;
		}
		
		void CbShowSubjectsCheckedChanged(object sender, EventArgs e)
		{
			if(ignoreLanguageChange)			
				return;
				
			ListViewItem lvi = lvProfiles.SelectedItems[0];		
			UserTranslateProfile pf = lvi.Tag as UserTranslateProfile;
				
			pf.ShowSubjects	= cbShowSubjects.Checked;
			
			changed = true;
		}
	}
}
