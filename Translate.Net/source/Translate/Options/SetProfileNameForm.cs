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
using FreeCL.UI;
using FreeCL.RTL;


namespace Translate
{
	/// <summary>
	/// Description of SetProfileNameForm.
	/// </summary>
	public partial class SetProfileNameForm :  FreeCL.Forms.BaseForm
	{
		public SetProfileNameForm(UserTranslateProfile profile, TranslateProfilesCollection profiles)
		{
			InitializeComponent();
			RegisterLanguageEvent(OnLanguageChanged);
			
			this.profile = profile;
			this.profiles = profiles;
			
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
			

			foreach(string subject in Manager.Subjects)
			{
				SubjectContainer sc = new SubjectContainer(subject, LangPack.TranslateString(subject));
				cbSubject.Items.Add(sc);
			}	
			
			SubjectContainer sc1 = new SubjectContainer(SubjectConstants.Any, LangPack.TranslateString(SubjectConstants.Any));
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

			if(!string.IsNullOrEmpty(profile.Name))			
			{
				tbName.Text = profile.Name;
			}
			else
			{
				tbName.Text = GetNewProfileName();
			}	
			
			initialized = true;
		}
		
		void OnLanguageChanged()
		{
			bCancel.Text = LangPack.TranslateString("Cancel");
			lName.Text = LangPack.TranslateString("Profile name");
			lLangPair.Text  = TranslateString("Translation direction");
			lSubject.Text  = TranslateString("Subject");
			
			Text = LangPack.TranslateString("Set profile properties");
		}
		
		bool initialized;
		
		void CbFromSelectedIndexChanged(object sender, EventArgs e)
		{
			if(!initialized)
				return;

			bool generatedName = tbName.Text == GetNewProfileName();
				
			LanguageDataContainer ld = cbFrom.SelectedItem as LanguageDataContainer;
			profile.TranslationDirection.From = ld.Language;
	
			ld = cbTo.SelectedItem as LanguageDataContainer;
			profile.TranslationDirection.To = ld.Language;
	
			SubjectContainer sc = cbSubject.SelectedItem as SubjectContainer;
			profile.Subject = sc.Subject;
			
			if(generatedName)
				tbName.Text = GetNewProfileName();
			
		}
		
		UserTranslateProfile profile;
		public UserTranslateProfile Profile {
			get { return profile; }
		}
		
		TranslateProfilesCollection profiles;
		
		public TranslateProfilesCollection Profiles {
			get { return profiles; }
		}
		
		bool IsProfileNameExists(string name)
		{
			return IsProfileNameExists(name, "");
		}
		
		bool IsProfileNameExists(string name, string currName)
		{
			bool exists = false;
			foreach(TranslateProfile pf in profiles)
			{
				if(pf.Name == name && (string.IsNullOrEmpty(currName) || pf.Name != currName))
				{
					exists = true;
					break;
				}
			}
			return exists;
		}
		
		string GetNewProfileName()
		{
			string nameBase = "";
			
			if(profile.TranslationDirection.From != Language.Any)
				nameBase += StringParser.SafeResizeString(LangPack.TranslateLanguage(profile.TranslationDirection.From), 3);
			else	
				nameBase += LangPack.TranslateLanguage(profile.TranslationDirection.From);
				
			nameBase += "->";
			
			if(profile.TranslationDirection.To != Language.Any)
				nameBase += StringParser.SafeResizeString(LangPack.TranslateLanguage(profile.TranslationDirection.To), 3);
			else	
				nameBase += LangPack.TranslateLanguage(profile.TranslationDirection.To);
					
			if(profile.Subject != SubjectConstants.Any && profile.Subject != SubjectConstants.Common)
				nameBase += "->" + StringParser.SafeResizeString(LangPack.TranslateString(profile.Subject), 3);

			if(!IsProfileNameExists(nameBase, profile.Name))
					return nameBase;

			string result;
			for(int i = 1; i < 1000; i++)
			{
				result = nameBase + " " + i.ToString();
				if(!IsProfileNameExists(result, profile.Name))
					return result;
			}
			return "";
		}

		void BOkClick(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(tbName.Text))
			{
				MessageBox.Show(FindForm(), TranslateString("Profile name don't set. Please enter profile name."), Constants.AppName, MessageBoxButtons.OK);
				DialogResult = DialogResult.None;
			}
			else if(IsProfileNameExists(tbName.Text, profile.Name))	
			{
				MessageBox.Show(FindForm(), TranslateString("Name for new profile you enter already used. Please enter unique name."), Constants.AppName, MessageBoxButtons.OK);
				DialogResult = DialogResult.None;
			}
			else
				profile.Name = tbName.Text;
		}
	}
}
