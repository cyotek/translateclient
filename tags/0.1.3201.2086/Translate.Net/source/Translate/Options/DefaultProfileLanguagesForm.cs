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

namespace Translate
{
	/// <summary>
	/// Description of DefaultProfileLanguagesForm.
	/// </summary>
	public partial class DefaultProfileLanguagesForm : FreeCL.Forms.BaseForm
	{
		public DefaultProfileLanguagesForm(DefaultTranslateProfile profile)
		{
			InitializeComponent();
			this.profile = profile;
			RegisterLanguageEvent(OnLanguageChanged);
			profile.DisabledLanguagesAlreadySet = true;
		}
		
		void OnLanguageChanged()
		{
			Text = TranslateString("Filter of languages");
			lInfo.Text = TranslateString("Choose languages you want to translate from and to. Disabled languages will be removed from \"Languages\" tab of default profile");
			
			gbFrom.Text = TranslateString("Source languages");
			gbTo.Text = TranslateString("Target languages");
			

			bCancel.Text = LangPack.TranslateString("Cancel");
			bOk.Text = TranslateString("OK");
			
			LoadLanguages();
			
		}		
	
		void LoadLanguages()
		{
			SuspendLayout();
			lbFrom.Items.Clear();
			lbTo.Items.Clear();
			
			LanguageCollection fromLangs = new LanguageCollection();
			LanguageCollection toLangs = new LanguageCollection();
			
			foreach(LanguagePair lp in Manager.LanguagePairServiceItems.Keys)
			{
				if(!fromLangs.Contains(lp.From))
					fromLangs.Add(lp.From);

				if(!toLangs.Contains(lp.To))
					toLangs.Add(lp.To);
			}
			
			fromLangs.Sort();
			
			string val = "";
			int idx;

			val = "+" + TranslateString("Toggle all");
			lbFrom.Items.Add(new LanguageDataContainer(Language.Any, val));
			
			foreach(Language l in fromLangs)
			{
				val = LangPack.TranslateLanguage(l);
				idx = lbFrom.Items.Add(new LanguageDataContainer(l, val));
				lbFrom.SetItemChecked(idx, !profile.DisabledSourceLanguages.Contains(l));
			}
			
			lbFrom.SetItemChecked(0, lbFrom.CheckedItems.Count == fromLangs.Count);

			if(lbFrom.Items.Count > 0)
				lbFrom.SelectedIndex = 0;
				
			toLangs.Sort();

			val = "+" + TranslateString("Toggle all");
			lbTo.Items.Add(new LanguageDataContainer(Language.Any, val));

			foreach(Language l in toLangs)
			{
				val = LangPack.TranslateLanguage(l);
				idx = lbTo.Items.Add(new LanguageDataContainer(l, val));
				lbTo.SetItemChecked(idx, !profile.DisabledTargetLanguages.Contains(l));
			}
			
			lbTo.SetItemChecked(0, lbTo.CheckedItems.Count == toLangs.Count);

			if(lbTo.Items.Count > 0)
				lbTo.SelectedIndex = 0;
				
			ResumeLayout(true);
		}
		
		
		DefaultTranslateProfile profile;
		public DefaultTranslateProfile Profile {
			get { return profile; }
			set { profile = value; }
		}
		
		
		void DefaultProfileLanguagesFormSizeChanged(object sender, EventArgs e)
		{
			gbFrom.Width = (ClientSize.Width)/2;
		}
		
		bool updating = false;
		void LbFromItemCheck(object sender, ItemCheckEventArgs e)
		{
			if(updating) return;
			CheckedListBox lb = sender as CheckedListBox;
			if(e.Index == 0)
			{
				try
				{
					updating = true;
					for(int i = 1; i < lb.Items.Count; i++)
						lb.SetItemCheckState(i, e.NewValue);
				}
				finally
				{
					updating = false;
				}
			}
		}
		
		void BOkClick(object sender, EventArgs e)
		{
			LanguageCollection checkedFromLanguages = new LanguageCollection();
			LanguageCollection checkedToLanguages = new LanguageCollection();
			
			foreach(LanguageDataContainer ldc in lbFrom.CheckedItems)
			{
				if(ldc.Language != Language.Any)
					checkedFromLanguages.Add(ldc.Language);
			}
			
			foreach(LanguageDataContainer ldc in lbTo.CheckedItems)
			{
				if(ldc.Language != Language.Any)
					checkedToLanguages.Add(ldc.Language);
			}
			
			if(checkedFromLanguages.Count == 0)
			{
				MessageBox.Show(FindForm(), 
					TranslateString("Please select at least one source language"), 
						Constants.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				DialogResult = DialogResult.None;
				return;
			}

			if(checkedToLanguages.Count == 0)
			{
				MessageBox.Show(FindForm(), 
					TranslateString("Please select at least one target language"), 
						Constants.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				DialogResult = DialogResult.None;
				return;
			}
			
			profile.DisabledSourceLanguages.Clear();
			foreach(LanguageDataContainer ldc in lbFrom.Items)
			{
				if(ldc.Language != Language.Any && !checkedFromLanguages.Contains(ldc.Language))
				{
					profile.DisabledSourceLanguages.Add(ldc.Language);
				}
			}
			
			

			profile.DisabledTargetLanguages.Clear();
			foreach(LanguageDataContainer ldc in lbTo.Items)
			{
				if(ldc.Language != Language.Any && !checkedToLanguages.Contains(ldc.Language))
					profile.DisabledTargetLanguages.Add(ldc.Language);
			}
			
			
		}
	}
}
