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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of LanguageSelector.
	/// </summary>
	public partial class LanguageSelector : UserControl
	{
	
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.Control.set_Text(System.String)")]
		public LanguageSelector()
		{
			InitializeComponent();
			
			FreeCL.RTL.LangPack.RegisterLanguageEvent(OnLanguageChanged);
			lTo.Text = "";
			lFrom.Text = "";
			tcMain.SelectedTab = tpLangs;
			OnLanguageChanged();
			
			foreach(Service s in Manager.Services)
			{
				ilServices.Images.Add(s.Name, s.Icon);
				WebUI.ResultsWebServer.WebServerGate.AddServiceIcon(s.Name, s.Icon);
			}
			
			lvServicesEnabled.Items.Clear();
			lvServicesDisabled.Items.Clear();
			lvServicesDisabledByUser.Items.Clear();
		}
		
		void OnLanguageChanged()
		{
			ignoreServicesLoading = true;
			try
			{
				if(languages != null)
				{
					LoadLanguages();
					LoadSubjects();
					LoadHistory();
				}
				tpLangs.Text = LangPack.TranslateString("Languages");
				tpSubject.Text = LangPack.TranslateString("Subjects");
				tpServices.Text = LangPack.TranslateString("Services"); 
				lEnabled.Text = LangPack.TranslateString("Enabled"); 
				lDisabled.Text = LangPack.TranslateString("Error"); 
				lDisabledByUser.Text = LangPack.TranslateString("Disabled"); 
				ttMain.SetToolTip(sbServiceUp, LangPack.TranslateString("Move service up"));
				ttMain.SetToolTip(sbServiceDown, LangPack.TranslateString("Move service down"));
				LvServicesResize(this, new EventArgs());
			}
			finally
			{
				ignoreServicesLoading = false;
				LoadServices(false);
			}
		}
		
		LanguagePair selection;
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LanguagePair Selection {
			get { return selection; }
			
			[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentOutOfRangeException.#ctor(System.String,System.String)")]
			[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.LanguageSelector+LanguageContainer.#ctor(Translate.Language,System.String)")]
			set { 
					if(value == null)
						return;
					
					if((selection == null && !profileChanging) || selection == value)
						return;
						
					//selection = value; 
					ListViewItem lvi = FindItem(fromLVItems, value.From);
					if(lvi != null && lbFrom.Handle != IntPtr.Zero)
					{
						lvi.Selected = true;
						lvi.Focused = true;
						lvi.EnsureVisible();
						LbFromSelectedIndexChanged(null, null);
					}	
					else
						throw new ArgumentOutOfRangeException("value", "Can't select LanguagePair : " + value.ToString());
					
					
					lvi = FindItem(toLVItems, value.To);
					if(lvi != null && lbTo.Handle != IntPtr.Zero)
					{
						lvi.Selected = true;
						lvi.Focused = true;
						lvi.EnsureVisible();
					}	
					else
						throw new ArgumentOutOfRangeException("value", "Can't select LanguagePair : " + value.ToString());
				}
		}
		
		public string SelectionName
		{
			get
			{
				if(lbFrom.SelectedIndices.Count == 0 || lbTo.SelectedIndices.Count == 0)
					return "";
				
				LanguageContainerPair currSel = new LanguageContainerPair((LanguageDataContainer)lbFrom.SelectedItems[0].Tag, (LanguageDataContainer)lbTo.SelectedItems[0].Tag);
				return currSel.ToString();
			}
		}
		
		void LockUpdate(bool lockIt)
		{
			TranslateMainForm.Instance.LockUpdate(lockIt);
			if(lockIt)
				SuspendLayout();
			else
				ResumeLayout();
		}
		bool profileChanging;
		TranslateProfile profile;
		public TranslateProfile Profile {
			get { return profile; }
			set 
			{ 
				try 
				{
					LockUpdate(true);
					profile = value;
					selection = null;
					lvServicesEnabled.Items.Clear();
					lvServicesDisabled.Items.Clear();
					lvServicesDisabledByUser.Items.Clear();
					serviceStatus.Status = null;
	
					
					if(profile != null)
					{
						pBottom.Visible = true;
						pBottom.Enabled = true;
							
						splitterBottom.Enabled = true;
						splitterBottom.Visible = true;
						
						tcMain.Visible = true;
						tcMain.Enabled = true;
							
						tcMain.SuspendLayout();	
						tcMain.TabPages.Clear();
						tcMain.TabPages.Add(tpServices);
						tcMain.TabPages.Add(tpLangs);
						tcMain.TabPages.Add(tpSubject);
						tcMain.SelectedTab = tpLangs;
					
						UserTranslateProfile upf = profile as UserTranslateProfile;
						if(upf != null)
						{
							if(!upf.ShowLanguages)
							{
								tcMain.TabPages.Remove(tpLangs);
	
								splitterBottom.Enabled = false;
								splitterBottom.Visible = false;
								
								pBottom.Visible = false;
								pBottom.Enabled = false;
							}
						
							if(!upf.ShowServices)
							{
								tcMain.TabPages.Remove(tpServices);
							}
							
							if(!upf.ShowSubjects)
								tcMain.TabPages.Remove(tpSubject);
						}
						tcMain.ResumeLayout();
						
						lvServicesEnabled.ListViewItemSorter = null;
						lvServicesDisabled.ListViewItemSorter = null;
						profileChanging = profile.History.Count > 0;
						try 
						{
							SetSubjects(profile.GetSupportedSubjects(), profile.Subjects);
							Languages = profile.GetLanguagePairs();
							History = profile.History;
						} 
						finally 
						{
							profileChanging = false;
						}
						
						CalcServicesSizes();
					}
				} 
				finally
				{
					LockUpdate(false);
				}
			}
		}
		
		
		ReadOnlyLanguagePairCollection languages;
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public ReadOnlyLanguagePairCollection Languages {
			get { return languages; }
			set { 
					languages = value; 
					LoadLanguages();
				}
		}

		
		class LanguageContainerPair
		{
			public LanguageContainerPair(LanguageDataContainer from, LanguageDataContainer to)
			{
				From = from;
				To = to;
			}
			
			public LanguageDataContainer From;
			public LanguageDataContainer To;
			
			public override bool Equals(Object obj)
			{
				if(From == null) return false;
				if(To == null) return false;

				LanguageContainerPair arg = obj as LanguageContainerPair;
				if(arg == null) return false;
				if(arg.From == null) return false;
				if(arg.To == null) return false;
				
				return From.Equals(arg.From) && To.Equals(arg.To);
			}
			
			public override string ToString()
			{
				return From.Text + " -> " + To.Text;
			}
			
			public override int GetHashCode() 
			{
      			return From.GetHashCode() * 100 + To.GetHashCode();
   			}	
			
			public static bool operator ==(LanguageContainerPair a, LanguageContainerPair b)
			{
				bool anull, bnull;
				anull = Object.ReferenceEquals(a,null); 
				bnull = Object.ReferenceEquals(b,null);
				if (anull && bnull) return true;
				if (anull || bnull) return false;
				return a.Equals(b);
			}
	
			public static bool operator !=(LanguageContainerPair a, LanguageContainerPair b)
			{
				return !(a == b);
			}
			
			
		}
		
		Dictionary<Language, ListViewItem> fromLVItems = new Dictionary<Language, ListViewItem>();
		Dictionary<Language, ListViewItem> toLVItems = new Dictionary<Language, ListViewItem>();
		
		ListViewItem FindItem(Dictionary<Language, ListViewItem> itemsDictionary, Language language)
		{
			ListViewItem result = null;
			itemsDictionary.TryGetValue(language, out result);
			return result;
		}
		
		
		void LoadLanguages()
		{
			LockUpdate(true);
			lbFrom.BeginUpdate();
			lbFrom.Sorting = SortOrder.None;
			
			lbTo.BeginUpdate();
			lbFrom.Items.Clear();
			lbTo.Items.Clear();
			fromLVItems.Clear();
			toLVItems.Clear();
			
			LanguageCollection fromLangs = new LanguageCollection();
			
			foreach(LanguagePair lp in languages)
			{
				if(!fromLangs.Contains(lp.From))
					fromLangs.Add(lp.From);
					
			}
			
			fromLangs.Sort();
			
			string val = "";
			
			ListViewItem lvi;
			
			if(fromLangs.Count > 1)
			{
				val = "+" + LangPack.TranslateLanguage(Language.Any);
				LanguageDataContainer ldc = new LanguageDataContainer(Language.Any, val);
				lvi = new ListViewItem(val);
				lvi.Tag = ldc;
				lvi.ToolTipText = val.Substring(1);
				lbFrom.Items.Add(lvi);
				fromLVItems.Add(Language.Any, lvi);
			}
			
			foreach(Language l in fromLangs)
			{
				val = LangPack.TranslateLanguage(l);
				if(l == Language.Autodetect)
				{
					val = "÷" + val;
					lvi = new ListViewItem(val);
					lvi.ToolTipText = val.Substring(1);
				}	
				else
				{
					lvi = new ListViewItem(val);
					lvi.ToolTipText = val;
				}	
					
				LanguageDataContainer ldc = new LanguageDataContainer(l, val);
				lvi.Tag = ldc;
				lbFrom.Items.Add(lvi);
				fromLVItems.Add(l, lvi);
			}

			lbFrom.Sorting = SortOrder.Ascending;
			lbTo.EndUpdate();
			lbFrom.EndUpdate();

			try //avoiding urepetative bug 
			{
				if(lbFrom.Items.Count > 0 && !profileChanging)
				{
					lbFrom.Items[0].Selected = true;
					lbFrom.Items[0].Focused = true;
					lbFrom.Items[0].EnsureVisible();
					LbFromSelectedIndexChanged(null, null);
				}	
			}
			catch
			{
				try
				{
					if(lbFrom.Items.Count > 0 && !profileChanging)
					{
						lbFrom.Items[0].Selected = true;
						lbFrom.Items[0].Focused = true;
						lbFrom.Items[0].EnsureVisible();
						LbFromSelectedIndexChanged(null, null);
					}	
				}
				catch
				{
				
				}
			}
			
			LockUpdate(false);
		}
		

		void LbFromSelectedIndexChanged(object sender, EventArgs e)
		{
			if(lbFrom.SelectedItems.Count == 0)
				return;
		
			LockUpdate(true);
				
			Language fromLanguage = ((LanguageDataContainer)lbFrom.SelectedItems[0].Tag).Language;	
			LanguageDataContainer toLanguage = null;
			if(lbTo.SelectedItems.Count != 0)
				toLanguage = ((LanguageDataContainer)lbTo.SelectedItems[0].Tag);
			
			lbTo.BeginUpdate();
			lbTo.Sorting = SortOrder.None;

			lbTo.Items.Clear();
			toLVItems.Clear();
			
			LanguageCollection toLangs = new LanguageCollection();
			
			foreach(LanguagePair lp in languages)
			{
				if((lp.From == fromLanguage || fromLanguage == Language.Any) && !toLangs.Contains(lp.To))
					toLangs.Add(lp.To);
			}
			
			toLangs.Sort();
			
			string val = "";
			
			ListViewItem lvi;
			
			if(toLangs.Count > 1)
			{
				val = "+" + LangPack.TranslateLanguage(Language.Any);
				LanguageDataContainer ldc = new LanguageDataContainer(Language.Any, val);
				lvi = new ListViewItem(val);
				lvi.Tag = ldc;
				lvi.ToolTipText = val.Substring(1);
				lbTo.Items.Add(lvi);
				toLVItems.Add(Language.Any, lvi);
			}
			
			foreach(Language l in toLangs)
			{
				val = LangPack.TranslateLanguage(l);
				LanguageDataContainer ldc = new LanguageDataContainer(l, val);
				lvi = new ListViewItem(val);
				lvi.Tag = ldc;
				lvi.ToolTipText = val;
				lbTo.Items.Add(lvi);
				toLVItems.Add(l, lvi);
			}
			
			lbTo.Sorting = SortOrder.Ascending;
			lbTo.EndUpdate();

			string caption = LangPack.TranslateLanguage(fromLanguage);
			lFrom.Text = caption;
			
			if(toLanguage != null)
			{
				lvi = FindItem(toLVItems, toLanguage.Language);
				if(lvi != null && lbTo.Handle != IntPtr.Zero)
				{
					try
					{
						lvi.Selected = true;
						lvi.Focused = true;
						lvi.EnsureVisible();
						LbToSelectedIndexChanged(null, null);
					}
					catch
					{
						try
						{
							lvi.Selected = true;
							lvi.Focused = true;
							lvi.EnsureVisible();
							LbToSelectedIndexChanged(null, null);
						}
						catch
						{
						
						}
					}
				}
				else
				{
					try //avoiding urepetative bug 
					{
						if(lbTo.Items.Count > 0 && !profileChanging)
						{
							lbTo.Items[0].Selected = true;
							lbTo.Items[0].Focused = true;
							lbTo.Items[0].EnsureVisible(); 
							LbToSelectedIndexChanged(null, null);
						}	
					}
					catch
					{
						try
						{
							if(lbTo.Items.Count > 0 && !profileChanging)
							{
								lbTo.Items[0].Selected = true;
								lbTo.Items[0].Focused = true;
								lbTo.Items[0].EnsureVisible(); 
								LbToSelectedIndexChanged(null, null);
								
							}	
						}
						catch
						{
						
						}
					}
				}
			}

			LockUpdate(false);
		}
		
		
		void PMainResize(object sender, EventArgs e)
		{
			pFrom.Width = pMain.ClientSize.Width/2 - 2;
		}
		
		public event EventHandler SelectionChanged;
		
		void LbToSelectedIndexChanged(object sender, EventArgs e)
		{
			if(lbFrom.SelectedIndices.Count == 0 || lbTo.SelectedIndices.Count == 0)
				return;

			Language fromLanguage = ((LanguageDataContainer)lbFrom.SelectedItems[0].Tag).Language;
			Language toLanguage = ((LanguageDataContainer)lbTo.SelectedItems[0].Tag).Language;
			selection = new LanguagePair(fromLanguage, toLanguage);
			lvServicesEnabled.ListViewItemSorter = null;
			lvServicesDisabled.ListViewItemSorter = null;
			
			
			string caption = LangPack.TranslateLanguage(toLanguage);
			lTo.Text = caption;
		
			if(SelectionChanged != null)
			{
				SelectionChanged(this, new EventArgs());
			}
			
			
			foreach(LanguageContainerPair lp in lbHistory.Items)
			{
				if(lp.From.Language == selection.From && lp.To.Language == selection.To)
				{
					lbHistory.SelectedItem = lp;
					break;
				}
			}
			LoadServices(false);
			profile.SelectedLanguagePair = selection;
			if(selection.From != Language.Any && selection.To != Language.Any)
			{
				lvServicesEnabled.ListViewItemSorter = profile.ServiceSettingComparer;
				lvServicesDisabled.ListViewItemSorter = profile.ServiceSettingComparer;
			}	
		}
		
		void PColumnsResize(object sender, EventArgs e)
		{
			lFrom.Width = (pColumns.ClientSize.Width - sbInvert.Width)/2 - 3;
		}
		
		public void Invert()
		{
			if(lbFrom.SelectedIndices.Count == 0 || lbTo.SelectedIndices.Count == 0)
				return;
		
			Language fromLanguage = ((LanguageDataContainer)lbFrom.SelectedItems[0].Tag).Language;
			Language toLanguage = ((LanguageDataContainer)lbTo.SelectedItems[0].Tag).Language;
			
			ListViewItem lvi = FindItem(fromLVItems, toLanguage);
			if(lvi != null && lbFrom.Handle != IntPtr.Zero)
			{
				lvi.Selected = true;
				lvi.Focused = true;
				lvi.EnsureVisible();
				LbFromSelectedIndexChanged(null, null);
			}	
			
			lvi = FindItem(toLVItems, fromLanguage);
			if(lvi != null && lbTo.Handle != IntPtr.Zero)
			{
				lvi.Selected = true;
				lvi.Focused = true;
				lvi.EnsureVisible();
				LbToSelectedIndexChanged(null, null);
			}	
		}
		
		LanguagePairCollection history = new LanguagePairCollection();
		
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public LanguagePairCollection History {
			get { return history; }
			set { 
					history = value; 
					LoadHistory();
				}
		}
		
		public void LoadHistory()
		{
			//int idx = lbHistory.SelectedIndex;
			lbHistory.BeginUpdate();
			lbHistory.Items.Clear();
			foreach(LanguagePair lp in history)
			{
				LanguageContainerPair lpc = new LanguageContainerPair(
					new LanguageDataContainer(lp.From, LangPack.TranslateLanguage(lp.From)),
					new LanguageDataContainer(lp.To, LangPack.TranslateLanguage(lp.To))
					);
				lbHistory.Items.Add(lpc);
			}
			lbHistory.EndUpdate();
			if(history.Count > 0)
				lbHistory.SelectedIndex = 0;
		}
		
		public void AddSelectionToHistory()
		{
			if(lbFrom.SelectedIndices.Count == 0 || lbTo.SelectedIndices.Count == 0)
				return;
				
			LanguageContainerPair currSel = new LanguageContainerPair((LanguageDataContainer)lbFrom.SelectedItems[0].Tag, (LanguageDataContainer)lbTo.SelectedItems[0].Tag);	
			int idx = lbHistory.Items.IndexOf(currSel);
			if(idx != -1)
			{
				lbHistory.Items.RemoveAt(idx);
			}
			lbHistory.Items.Insert(0, currSel);
			lbHistory.SelectedIndex = 0;
			
			LanguagePair lp = new LanguagePair(currSel.From.Language, currSel.To.Language);
			idx = history.IndexOf(lp);
			if(idx != -1)
			{
				history.RemoveAt(idx);
			}
			history.Insert(0, lp);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		void LbHistorySelectedIndexChanged(object sender, EventArgs e)
		{
			if(lbHistory.SelectedIndex == -1)
				return;
			
			LanguageContainerPair currSel;
			if(lbFrom.SelectedIndices.Count == 0 || lbTo.SelectedIndices.Count == 0)
				currSel = new LanguageContainerPair(null, null);
			else	
				currSel = new LanguageContainerPair((LanguageDataContainer)lbFrom.SelectedItems[0].Tag, (LanguageDataContainer)lbTo.SelectedItems[0].Tag);
			
			LanguageContainerPair currHistorySel = (LanguageContainerPair)lbHistory.SelectedItem;
			if(currSel != currHistorySel)
			{
				try
				{
					Selection = new LanguagePair(currHistorySel.From.Language, currHistorySel.To.Language);
				}
				catch
				{
					lbHistory.Items.RemoveAt(lbHistory.SelectedIndex);
					history.Remove(new LanguagePair(currHistorySel.From.Language, currHistorySel.To.Language));
					try
					{
						Selection =  new LanguagePair(Language.Any, Language.Any);
					}
					catch{}
				}	
			}
		}
		
		
		SubjectCollection supportedSubjects;
		SubjectCollection subjects;
		bool loadingSubjects;
		void LoadSubjects()
		{
			lbSubjects.BeginUpdate();
			loadingSubjects = true;
			lbSubjects.Items.Clear();
			string val;
			
			val = "+ " + LangPack.TranslateString("Toggle all");
			lbSubjects.Items.Add(new SubjectContainer("Toggle all", val));
			
			foreach(string s in supportedSubjects)
			{
				val = LangPack.TranslateString(s);
				if(s == "Common")
					val = "+" + val;
				lbSubjects.Items.Add(new SubjectContainer(s, val), subjects.Contains(s));
			}		
			lbSubjects.EndUpdate();
			lbSubjects.SetItemChecked(0, lbSubjects.CheckedItems.Count == supportedSubjects.Count);
			loadingSubjects = false;
			serviceItemsSettings = null; //reset
			LoadServices(false);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		void SetSubjects(SubjectCollection supportedSubjects, SubjectCollection subjects)
		{
			this.supportedSubjects = supportedSubjects;
			this.subjects = subjects;
			LoadSubjects();
		}
		
		public event EventHandler SubjectsChanged;
		
		void LbSubjectsItemCheck(object sender, ItemCheckEventArgs e)
		{
			if(loadingSubjects) return;
			
			if(e.Index == 0)
			{
				try 
				{
					ignoreServicesLoading = true;
					loadingSubjects = true;
					LockUpdate(true);
					if(e.NewValue == CheckState.Checked)
					{  //all
						subjects.Clear();
						subjects.AddRange(supportedSubjects);
						for(int i = 1; i < lbSubjects.Items.Count; i++)
							lbSubjects.SetItemCheckState(i, e.NewValue);
					}
					else if(e.NewValue == CheckState.Unchecked)
					{
						subjects.Clear();
						subjects.Add((lbSubjects.Items[1] as SubjectContainer).Subject);
					
						lbSubjects.SetItemCheckState(1, CheckState.Checked);
	
						for(int i = 2; i < lbSubjects.Items.Count; i++)
							lbSubjects.SetItemCheckState(i, e.NewValue);
					}
					
					Languages = profile.GetLanguagePairs();
					LanguagePairCollection to_delete = new LanguagePairCollection();
					foreach(LanguagePair lp in history)
					{
						if(!Languages.Contains(lp))
						{
							to_delete.Add(lp);
						}
					}
					
					foreach(LanguagePair lp in to_delete)
					{
						history.Remove(lp);
					}
					LoadHistory();

				} 
				finally
				{
					LockUpdate(false);
					ignoreServicesLoading = false;
					loadingSubjects = false;
					serviceItemsSettings = null; //reset	
					LoadServices(false);
				}
				
				if(SubjectsChanged != null)
					SubjectsChanged(this, new EventArgs()); 
				
				return;
			}
			

			SubjectContainer sc = (SubjectContainer)lbSubjects.Items[e.Index];	
			if(e.NewValue == CheckState.Checked)
				subjects.Add(sc.Subject);
			else if(e.NewValue == CheckState.Unchecked)
			{
				if(subjects.Count == 1)
				{
					int new_idx = e.Index;
					new_idx ++;
					if(new_idx >= lbSubjects.Items.Count)
					{
						new_idx = 1;
					}
					lbSubjects.SetItemChecked(new_idx, true);
				}
				subjects.Remove(sc.Subject);
			}
				
			try
			{
				ignoreServicesLoading = true;
				Languages = profile.GetLanguagePairs();
				LanguagePairCollection to_delete = new LanguagePairCollection();
				foreach(LanguagePair lp in history)
				{
					if(!Languages.Contains(lp))
					{
						to_delete.Add(lp);
					}
				}
				
				foreach(LanguagePair lp in to_delete)
				{
					history.Remove(lp);
				}
				LoadHistory();
			}
			finally
			{
				ignoreServicesLoading = false;
				serviceItemsSettings = null; //reset	
				LoadServices(false);
			}
			
			if(SubjectsChanged != null)
				SubjectsChanged(this, new EventArgs()); 
		}
		
		
		void LanguageSelectorLoad(object sender, EventArgs e)
		{
			CalcServicesSizes();
		}
		
		void LvServicesResize(object sender, EventArgs e)
		{
			lvServicesEnabled.Columns[0].Width = lvServicesEnabled.Width;
			lvServicesDisabled.Columns[0].Width = lvServicesEnabled.Width;
			lvServicesDisabledByUser.Columns[0].Width = lvServicesEnabled.Width;
		}

		void CalcListViewSize(ListView lv, Label label)
		{
			if(lv.Items.Count > 0)
			{
				if(!lv.Enabled)
				{
					lv.Visible = true;
					lv.Enabled = true;
					
				}
				lv.Height = (lv.Items[0].Bounds.Height)*lv.Items.Count + 5;
			}
			else
			{
				if(lv.Enabled)
				{
					lv.Visible = false;
					lv.Enabled = false;
				}
			}
		}
		
		void CalcServicesSizes()
		{
			CalcListViewSize(lvServicesEnabled, lEnabled);
			CalcListViewSize(lvServicesDisabled, lDisabled);
			CalcListViewSize(lvServicesDisabledByUser, lDisabledByUser);
		}
		
		
		ReadOnlyServiceSettingCollection serviceItemsSettings;
		List<ServiceSettingsContainer> serviceItemsContainers = new List<ServiceSettingsContainer>();
		bool ignoreServicesLoading;
		Dictionary<ServiceSettingsContainer, ListViewItem> enabledLVItems = new Dictionary<ServiceSettingsContainer, ListViewItem>();
		Dictionary<ServiceSettingsContainer, ListViewItem> disabledLVItems = new Dictionary<ServiceSettingsContainer, ListViewItem>();
		Dictionary<ServiceSettingsContainer, ListViewItem> disabledByUserLVItems = new Dictionary<ServiceSettingsContainer, ListViewItem>();
		void LoadServices(bool phraseChanged)
		{
			if(selection == null || ignoreServicesLoading)
				return;
			if(serviceItemsSettings == null || !phraseChanged)
			{
				serviceItemsSettings = profile.GetServiceSettings(phrase, selection);
				lvServicesEnabled.Items.Clear();
				lvServicesDisabled.Items.Clear();
				lvServicesDisabledByUser.Items.Clear();
				serviceItemsContainers.Clear();
				enabledLVItems.Clear();
				disabledLVItems.Clear();
				disabledByUserLVItems.Clear();
				bool showLanguage = selection.From == Language.Any || selection.To == Language.Any;
				foreach(ServiceItemSetting ss in serviceItemsSettings)
				{
					ServiceSettingsContainer sc = new ServiceSettingsContainer(ss, showLanguage);
					sc.DisabledByUser = !profile.IsServiceEnabled(ss.ServiceItem.FullName, ss.LanguagePair, ss.Subject);
					serviceItemsContainers.Add(sc);
				}
			}

			try 
			{
				tpServices.SuspendLayout();
				lvServicesEnabled.SuspendLayout();
				lvServicesEnabled.BeginUpdate();
				lvServicesDisabled.SuspendLayout();
				lvServicesDisabled.BeginUpdate();
				lvServicesDisabledByUser.SuspendLayout();
				lvServicesDisabledByUser.BeginUpdate();
				foreach(ServiceSettingsContainer sc in serviceItemsContainers)
				{
					AddListViewItem(sc);
				}
				
				CalcServicesSizes();
				try 
				{ //try to avoid unrepeatable bug
					if(lvServicesEnabled.Items.Count > 0)
					{
						lvServicesEnabled.Items[0].Focused = true;
						lvServicesEnabled.Items[0].Selected = true;
					}
					else if(lvServicesDisabled.Items.Count > 0)
					{
						lvServicesDisabled.Items[0].Focused = true;
						lvServicesDisabled.Items[0].Selected = true;
					}
					else if(lvServicesDisabledByUser.Items.Count > 0)
					{
						lvServicesDisabledByUser.Items[0].Focused = true;
						lvServicesDisabledByUser.Items[0].Selected = true;
					}
	
				} 
				catch
				{
					try 
					{
						if(lvServicesEnabled.Items.Count > 0)
						{
							lvServicesEnabled.Items[0].Focused = true;
							lvServicesEnabled.Items[0].Selected = true;
						}
						else if(lvServicesDisabled.Items.Count > 0)
						{
							lvServicesDisabled.Items[0].Focused = true;
							lvServicesDisabled.Items[0].Selected = true;
						}
						else if(lvServicesDisabledByUser.Items.Count > 0)
						{
							lvServicesDisabledByUser.Items[0].Focused = true;
							lvServicesDisabledByUser.Items[0].Selected = true;
						}
		
					} 
					catch
					{
						
					}
					
				}
			} 
			finally
			{
				lvServicesEnabled.EndUpdate();
				lvServicesEnabled.ResumeLayout();
				lvServicesDisabled.EndUpdate();
				lvServicesDisabled.ResumeLayout();
				lvServicesDisabledByUser.EndUpdate();
				lvServicesDisabledByUser.ResumeLayout();
				tpServices.ResumeLayout();
			}
		}
		
		public ReadOnlyServiceSettingCollection GetServiceSettings()
		{
			ServiceSettingCollection result = new ServiceSettingCollection();
			
			foreach(ListViewItem lvi in lvServicesEnabled.Items)
			{
				ServiceSettingsContainer sc = lvi.Tag as ServiceSettingsContainer;
				ServiceItemSetting tsetting = new ServiceItemSetting(sc.Setting.LanguagePair, sc.Setting.Subject, sc.Setting.ServiceItem , TranslateOptions.Instance.GetNetworkSetting(sc.Setting.ServiceItem.Service));
				result.Add(tsetting);
			}
			return new ReadOnlyServiceSettingCollection(result);
		}
		
		
		ListViewItem AddListViewItem(ServiceSettingsContainer sc)
		{
			if(!sc.DisabledByUser)
				sc.Check(phrase);
				
			ListViewItem lvi;
			if(sc.DisabledByUser)
			{
				lvi = FindItem(enabledLVItems, sc);
				if(lvi != null)
				{
					lvServicesEnabled.Items.Remove(lvi);
					enabledLVItems.Remove(sc);
				}	

				lvi = FindItem(disabledLVItems, sc);
				if(lvi != null)
				{
					lvServicesDisabled.Items.Remove(lvi);
					disabledLVItems.Remove(sc);
				}	
				
				lvi = FindItem(disabledByUserLVItems, sc);
				if(lvi == null)
				{
					lvi = new ListViewItem(sc.Name, sc.Setting.ServiceItem.Service.Name);
					lvi.Tag = sc;
					lvi.ToolTipText = ServiceSettingsContainer.GetServiceItemType(sc.Setting.ServiceItem);
					lvServicesDisabledByUser.Items.Add(lvi);
					disabledByUserLVItems.Add(sc, lvi);
				}
			}
			else if(sc.Enabled)
			{
				lvi = FindItem(disabledLVItems, sc);
				if(lvi != null)
				{
					lvServicesDisabled.Items.Remove(lvi);
					disabledLVItems.Remove(sc);
				}	
					
				lvi = FindItem(disabledByUserLVItems, sc);
				if(lvi != null)
				{
					lvServicesDisabledByUser.Items.Remove(lvi);
					disabledByUserLVItems.Remove(sc);
				}	
				
				lvi = FindItem(enabledLVItems, sc);
				if(lvi == null)
				{
					lvi = new ListViewItem(sc.Name, sc.Setting.ServiceItem.Service.Name);
					lvi.Tag = sc;
					lvi.ToolTipText = ServiceSettingsContainer.GetServiceItemType(sc.Setting.ServiceItem);					
					lvServicesEnabled.Items.Add(lvi);
					enabledLVItems.Add(sc, lvi);
				}
			}
			else
			{
				lvi = FindItem(enabledLVItems, sc);
				if(lvi != null)
				{
					lvServicesEnabled.Items.Remove(lvi);
					enabledLVItems.Remove(sc);
				}	
					
				lvi = FindItem(disabledByUserLVItems, sc);
				if(lvi != null)
				{
					lvServicesDisabledByUser.Items.Remove(lvi);
					disabledByUserLVItems.Remove(sc);
				}	
				
				lvi = FindItem(disabledLVItems, sc);
				if(lvi == null)
				{
					lvi = new ListViewItem(sc.Name, sc.Setting.ServiceItem.Service.Name);
					lvi.Tag = sc;
					lvi.ToolTipText = ServiceSettingsContainer.GetServiceItemType(sc.Setting.ServiceItem);
					lvServicesDisabled.Items.Add(lvi);
					disabledLVItems.Add(sc, lvi);
				}
				else if(lvi.Selected)
				{ //reload error
					serviceStatus.Status = sc;
				}
			}
			return lvi;
		}

		
		ListViewItem FindItem(Dictionary<ServiceSettingsContainer, ListViewItem> itemsDictionary, ServiceSettingsContainer sc)
		{
			ListViewItem result;
			itemsDictionary.TryGetValue(sc, out result);
			return result;
		}
		
		string phrase;		
		public string Phrase {
			get { return phrase; }
			set 
			{ 
				phrase = value; 
				LoadServices(true);
			}
		}
		
		void LvServicesEnabledEnter(object sender, EventArgs e)
		{
			LvServicesEnabledSelectedIndexChanged(sender, e);
		}
		
		bool skipselectingservices;
		void LvServicesEnabledSelectedIndexChanged(object sender, EventArgs e)
		{
			LockUpdate(true);
			try 
			{
				if(skipselectingservices)
				{
					pEnabled.Enabled = false;
					pEnabled.Visible = false;
					return;
				}	
				ListView lv = sender as ListView;
				if(lv.SelectedItems.Count == 0)
				{
					return;
				}	
				
				ListViewItem lvi = lv.SelectedItems[0];
				ServiceSettingsContainer sc = lvi.Tag as ServiceSettingsContainer;
				serviceStatus.ShowLanguage = selection.From == Language.Any || selection.To == Language.Any;
				serviceStatus.Status = sc;
				
				if((lvServicesEnabled == lv || lv == lvServicesDisabled) && 
					lv.Items.Count > 1 &&
					(selection.From != Language.Any && selection.To != Language.Any))
				{
					pEnabled.Enabled = true;
					pEnabled.Visible = true;
				
					sbServiceUp.Enabled = lv.SelectedIndices[0] != 0;
					sbServiceDown.Enabled = lv.SelectedIndices[0] != lv.Items.Count - 1;
				}
				else
				{
					pEnabled.Enabled = false;
					pEnabled.Visible = false;
				}	
			} 
			finally
			{
				LockUpdate(false);
			}
		}
		
		void ServiceStatusResize(object sender, EventArgs e)
		{
			pServiceData.Height = serviceStatus.Height + 8;
		}
		
		void ServiceStatusButtonClick(object sender, EventArgs e)
		{
			skipselectingservices = true;
			try
			{
				ServiceSettingsContainer sc = serviceStatus.Status;
				ListViewItem lvi = FindItem(disabledByUserLVItems, sc);
				if(lvi != null)
				{
					lvServicesDisabledByUser.Items.Remove(lvi);
					disabledByUserLVItems.Remove(sc);
				}	
				else
				{
					lvi = FindItem(disabledLVItems, sc);
					if(lvi != null)
					{
						lvServicesDisabled.Items.Remove(lvi);
						disabledLVItems.Remove(sc);
					}	
					else
					{
						lvi = FindItem(enabledLVItems, sc);
						if(lvi != null)
						{
							lvServicesEnabled.Items.Remove(lvi);
							enabledLVItems.Remove(sc);
						}	
					}
				}
							
				
				sc.DisabledByUser = !sc.DisabledByUser;
				profile.EnableService(sc.Setting.ServiceItem.FullName, sc.Setting.LanguagePair, sc.Setting.Subject, !sc.DisabledByUser);
				
				lvi = AddListViewItem(sc);
				CalcServicesSizes();
				lvi.EnsureVisible();
				lvi.ListView.Focus();
				lvi.Selected = true;
				serviceStatus.Status = sc;
				pServices.ScrollControlIntoView(lvi.ListView);
			}
			finally
			{
				skipselectingservices = false;
			}
		}
		
		void SbServiceUpClick(object sender, EventArgs e)
		{
			ServiceSettingsContainer sc = serviceStatus.Status;
			ListViewItem lvi = FindItem(disabledLVItems, sc);
			if(lvi != null && lvi.Index != 0)
			{
				ListViewItem lvi_prev = lvServicesDisabled.Items[lvi.Index - 1];
				ServiceSettingsContainer sc_prev = lvi_prev.Tag as ServiceSettingsContainer;
				profile.MoveBefore(sc_prev.Setting, sc.Setting);
			}	
			else
			{
				lvi = FindItem(enabledLVItems, sc);
				if(lvi != null && lvi.Index != 0)
				{
					ListViewItem lvi_prev = lvServicesEnabled.Items[lvi.Index - 1];
					ServiceSettingsContainer sc_prev = lvi_prev.Tag as ServiceSettingsContainer;
					profile.MoveBefore(sc_prev.Setting, sc.Setting);
				}	
			}
			lvServicesEnabled.Sort();
			lvServicesDisabled.Sort();	
			if(lvi != null)
				LvServicesEnabledSelectedIndexChanged(lvi.ListView, new EventArgs());
		}
		
		void SbServiceDownClick(object sender, EventArgs e)
		{
			ServiceSettingsContainer sc = serviceStatus.Status;
			ListViewItem lvi = FindItem(disabledLVItems, sc);
			if(lvi != null && lvi.Index != lvServicesDisabled.Items.Count - 1)
			{
				ListViewItem lvi_prev = lvServicesDisabled.Items[lvi.Index + 1];
				ServiceSettingsContainer sc_prev = lvi_prev.Tag as ServiceSettingsContainer;
				profile.MoveAfter(sc_prev.Setting, sc.Setting);
			}	
			else
			{
				lvi = FindItem(enabledLVItems, sc);
				if(lvi != null && lvi.Index != lvServicesEnabled.Items.Count - 1)
				{
					ListViewItem lvi_prev = lvServicesEnabled.Items[lvi.Index + 1];
					ServiceSettingsContainer sc_prev = lvi_prev.Tag as ServiceSettingsContainer;
					profile.MoveAfter(sc_prev.Setting, sc.Setting);
				}	
			}
			lvServicesEnabled.Sort();
			lvServicesDisabled.Sort();	
			if(lvi != null)
				LvServicesEnabledSelectedIndexChanged(lvi.ListView, new EventArgs());
		}
		
		void LbFromResize(object sender, EventArgs e)
		{
			lbFrom.Columns[0].Width = lbFrom.Width;
		}
		
		void LbToResize(object sender, EventArgs e)
		{
			lbTo.Columns[0].Width = lbTo.Width;
		}
	}
	
	
}
