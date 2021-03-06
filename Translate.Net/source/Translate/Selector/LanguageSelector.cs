﻿#region License block : MPL 1.1/GPL 2.0/LGPL 2.1
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
using System.Reflection;
using System.Resources;
using FreeCL.RTL;


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
				Icon icon = s.Icon;
				if(icon == null)
				{
					MessageBox.Show(FindForm(), string.Format(FreeCL.RTL.LangPack.TranslateString("The icon for service \"{0}\" not found."), s.Name) , Constants.AppName);
					ResourceManager resources = new ResourceManager("Translate.Common.Icons", Assembly.GetExecutingAssembly());
					icon = (System.Drawing.Icon)(resources.GetObject("StaticIcon"));
				}
				ilServices.Images.Add(s.Name, icon);
				WebUI.ResultsWebServer.WebServerGate.AddServiceIcon(s.Name, icon);
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
				miMoveServiceUp.Text = LangPack.TranslateString("Move service up");
				miMoveServiceUp.ToolTipText = miMoveServiceUp.Text;
				ttMain.SetToolTip(sbServiceUp, miMoveServiceUp.Text);
				
				miMoveServiceDown.Text = LangPack.TranslateString("Move service down");
				miMoveServiceDown.ToolTipText = miMoveServiceDown.Text;
				ttMain.SetToolTip(sbServiceDown, miMoveServiceDown.Text);
				//TODO:ttMain.SetToolTip(sbEnableService, LangPack.TranslateString(""));
				
				
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
					LanguageDataContainer from = new LanguageDataContainer(value.From, "");
					LanguageDataContainer to = new LanguageDataContainer(value.To, "");
					int idx = lbFrom.Items.IndexOf(from);
					if(idx != -1)
						lbFrom.SelectedIndex = idx;
					else
						throw new ArgumentOutOfRangeException("value", "Can't select LanguagePair : " + value.ToString());

					idx = lbTo.Items.IndexOf(to);
					if(idx != -1)
						lbTo.SelectedIndex = idx;
					else
						throw new ArgumentOutOfRangeException("value", "Can't select LanguagePair : " + value.ToString());
					
				}
		}
		
		public string SelectionName
		{
			get
			{
				if(lbFrom.SelectedIndex == -1 || lbTo.SelectedIndex == -1)
					return "";
				
				LanguageContainerPair currSel = new LanguageContainerPair((LanguageDataContainer)lbFrom.SelectedItem, (LanguageDataContainer)lbTo.SelectedItem);	
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
						tcMain.SelectedTab = null;
						tcMain.TabPages.Clear();
						tcMain.TabPages.Add(tpServices);
						tcMain.TabPages.Add(tpLangs);
						pFrom.Width = pMain.ClientSize.Width/2 - 2;
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
							SetLanguages(profile.GetLanguagePairs());
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
		
		
		LanguagePairCollection languages;
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public LanguagePairCollection Languages {
			get { return languages; }
		}
		
		void SetLanguages(ReadOnlyLanguagePairCollection collection)
		{
			languages = new LanguagePairCollection();
			languages.AddRange(collection);
			languages.Sort();
			LoadLanguages();
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
		
		
		void LoadLanguages()
		{
			LockUpdate(true);
			lbFrom.BeginUpdate();
			lbFrom.Items.Clear();
			lbTo.Items.Clear();
			
			LanguageCollection fromLangs = new LanguageCollection();
			int index;
			foreach(LanguagePair lp in languages)
			{
				index = fromLangs.BinarySearch(lp.From);
				if(index < 0)
					fromLangs.Insert(~index, lp.From);
			}
			
			
			string val = "";
			
			if(fromLangs.Count > 1)
			{
				val = "+" + LangPack.TranslateLanguage(Language.Any);
				lbFrom.Items.Add(new LanguageDataContainer(Language.Any, val));
			}
			
			foreach(Language l in fromLangs)
			{
				val = LangPack.TranslateLanguage(l);
				if(l == Language.Autodetect)
					val = "÷" + val;
				lbFrom.Items.Add(new LanguageDataContainer(l, val));
			}

			lbFrom.EndUpdate();

			if(lbFrom.Items.Count > 0 && !profileChanging)
				lbFrom.SelectedIndex = 0;
			LockUpdate(false);
		}
		

		void LbFromSelectedIndexChanged(object sender, EventArgs e)
		{
			LockUpdate(true);
			if(lbFrom.SelectedIndex == -1)
				return;
				
			Language fromLanguage = ((LanguageDataContainer)lbFrom.SelectedItem).Language;	
			LanguageDataContainer toLanguage = null;
			if(lbTo.SelectedItem != null)
				toLanguage = ((LanguageDataContainer)lbTo.SelectedItem);
			
			lbTo.BeginUpdate();

			lbTo.Items.Clear();
			
			LanguageCollection toLangs = new LanguageCollection();
			int index;
			foreach(LanguagePair lp in languages)
			{
				if((lp.From == fromLanguage || fromLanguage == Language.Any))
				{
					index = toLangs.BinarySearch(lp.To);
					if(index < 0)
						toLangs.Insert(~index, lp.To);
				}	
			}
			
			string val = "";
			
			if(toLangs.Count > 1)
			{
				val = "+" + LangPack.TranslateLanguage(Language.Any);
				lbTo.Items.Add(new LanguageDataContainer(Language.Any, val));
			}
			
			foreach(Language l in toLangs)
			{
				val = LangPack.TranslateLanguage(l);
				lbTo.Items.Add(new LanguageDataContainer(l, val));
			}
			lbTo.EndUpdate();

			string caption = LangPack.TranslateLanguage(fromLanguage);
			lFrom.Text = caption;
			
			int idx = -1;
			if(toLanguage != null)
				idx = lbTo.Items.IndexOf(toLanguage);

			if(idx == -1)
				idx = 0;
			lbTo.SelectedIndex = idx;
			LockUpdate(false);
		}
		
		
		void PMainResize(object sender, EventArgs e)
		{
			pFrom.Width = pMain.ClientSize.Width/2 - 2;
		}
		
		public event EventHandler SelectionChanged;
		
		void LbToSelectedIndexChanged(object sender, EventArgs e)
		{
			if(lbFrom.SelectedIndex == -1 || lbTo.SelectedIndex == -1)
				return;

			Language fromLanguage = ((LanguageDataContainer)lbFrom.SelectedItem).Language;	
			Language toLanguage = ((LanguageDataContainer)lbTo.SelectedItem).Language;				
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
			LanguageDataContainer fromLanguage = (LanguageDataContainer)lbFrom.SelectedItem;
			if(fromLanguage == null)
				return;
			
			if(lbTo.SelectedItem == null)
				return;
				
			int idx = lbFrom.Items.IndexOf(lbTo.SelectedItem);
			if(idx == -1) return;
			lbFrom.SelectedIndex = idx;
			
			idx = lbTo.Items.IndexOf(fromLanguage);
			if(idx == -1) return;
			lbTo.SelectedIndex = idx;			
			
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
			if(lbFrom.SelectedIndex == -1 || lbTo.SelectedIndex == -1)
				return;
				
			LanguageContainerPair currSel = new LanguageContainerPair((LanguageDataContainer)lbFrom.SelectedItem, (LanguageDataContainer)lbTo.SelectedItem);	
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
				
			LanguageContainerPair currSel = new LanguageContainerPair((LanguageDataContainer)lbFrom.SelectedItem, (LanguageDataContainer)lbTo.SelectedItem);
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
					
					SetLanguages(profile.GetLanguagePairs());
					
					LanguagePairCollection to_delete = new LanguagePairCollection();
					int index;
					foreach(LanguagePair lp in history)
					{
						index = Languages.BinarySearch(lp);
						if(index < 0)
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
				SetLanguages(profile.GetLanguagePairs());
				LanguagePairCollection to_delete = new LanguagePairCollection();
				int index;
				foreach(LanguagePair lp in history)
				{
					index = Languages.BinarySearch(lp);
					if(index < 0)
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
			int maxWidth = lvServicesEnabled.Width;
			if(lvServicesDisabled.Width > maxWidth)
				maxWidth = lvServicesDisabled.Width;
			if(lvServicesDisabledByUser.Width > maxWidth)
				maxWidth = lvServicesDisabledByUser.Width;
				
			lvServicesEnabled.Columns[0].Width = maxWidth;
			lvServicesDisabled.Columns[0].Width = maxWidth;
			lvServicesDisabledByUser.Columns[0].Width = maxWidth;
		}

		void CalcListViewSize(ListView lv, Label label)
		{
			if(lv.Items.Count > 0)
			{
				lv.Visible = false;
				lv.Enabled = false;
				if(!MonoHelper.IsUnix)
				{
					lv.Height = (lv.Items[0].Bounds.Height)*lv.Items.Count + 5;
				}
				else
				{ //monobug - height of item is 0 and height can't be more 32000
					int itemHeight = lv.Items[0].Bounds.Height;
					if(itemHeight == 0)
						itemHeight = 16;	
					int newHeight = itemHeight*lv.Items.Count + 5;
					if(newHeight > 16000)
						newHeight = 16000;
					lv.Height = newHeight;
				}
				lv.Enabled = true;
				lv.Visible = true;
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
				lock(serviceItemsContainers)
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
				
				lvServicesEnabled.FocusedItem = null;
				lvServicesDisabled.FocusedItem = null;
				lvServicesDisabledByUser.FocusedItem = null;
				
				lock(serviceItemsContainers)
				{
					PrepareAddingServicesBatch();
					foreach(ServiceSettingsContainer sc in serviceItemsContainers)
					{
						AddListViewItem(sc, true);
					}
					ApplyAddingServicesBatch();
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
				Refresh();
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
			return AddListViewItem(sc, false);
		}

		List<ListViewItem> batchEnabledLVItems = new List<ListViewItem>();
		List<ListViewItem> batchDisabledLVItems = new List<ListViewItem>();
		List<ListViewItem> batchDisabledByUserLVItems = new List<ListViewItem>();

		void PrepareAddingServicesBatch()
		{
			batchEnabledLVItems.Clear();
			batchDisabledLVItems.Clear();
			batchDisabledByUserLVItems.Clear();
		}
		
		void ApplyAddingServicesBatch()
		{
			if(batchEnabledLVItems.Count > 0)
				lvServicesEnabled.Items.AddRange(batchEnabledLVItems.ToArray());
			if(batchDisabledLVItems.Count > 0)
				lvServicesDisabled.Items.AddRange(batchDisabledLVItems.ToArray());
			if(batchDisabledByUserLVItems.Count > 0)
				lvServicesDisabledByUser.Items.AddRange(batchDisabledByUserLVItems.ToArray());
			PrepareAddingServicesBatch();
		}
		
		ListViewItem AddListViewItem(ServiceSettingsContainer sc, bool batchMode)
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
					lvi.ToolTipText = sc.GetServiceTooltipText();
					if(batchMode)
						batchDisabledByUserLVItems.Add(lvi);
					else
						lvServicesDisabledByUser.Items.Add(lvi);
					disabledByUserLVItems.Add(sc, lvi);
				}
			}
			else if(sc.Enabled)
			{
				lvi = FindItem(disabledLVItems, sc);
				if(lvi != null)
				{
					lvi.Focused = false;
					lvi.Selected = false;
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
					lvi.ToolTipText = sc.GetServiceTooltipText();
					if(batchMode)
						batchEnabledLVItems.Add(lvi);
					else
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
					lvi.ToolTipText = sc.GetServiceTooltipText();
					if(batchMode)
						batchDisabledLVItems.Add(lvi);
					else
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
				
				if(sc.DisabledByUser)
				{
					sbEnableService.Text = LangPack.TranslateString("Enable");
				}
				else
				{
					sbEnableService.Text = LangPack.TranslateString("Disable");
				}
				miEnableService.Text = sbEnableService.Text; 
				//ttMain.SetToolTip(sbEnableService, sbEnableService.Text);
				
				if((lvServicesEnabled == lv || lv == lvServicesDisabled) && 
					lv.Items.Count > 1 &&
					(selection.From != Language.Any && selection.To != Language.Any))
				{
					sbServiceUp.Enabled = lv.SelectedIndices[0] != 0;
					sbServiceDown.Enabled = lv.SelectedIndices[0] != lv.Items.Count - 1;
				}
				else
				{
					sbServiceUp.Enabled = false;
					sbServiceDown.Enabled = false;
				}	
				
				miMoveServiceDown.Enabled = sbServiceDown.Enabled;
				miMoveServiceUp.Enabled = sbServiceUp.Enabled;
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
			ListViewItem lvi;
			try
			{
				ServiceSettingsContainer sc = serviceStatus.Status;
				lvi = FindItem(disabledByUserLVItems, sc);
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
				Rectangle pos = lvi.Bounds;
				if(pServices.VerticalScroll.Value + pos.Top > pServices.VerticalScroll.Maximum)
					pServices.VerticalScroll.Value  = pServices.VerticalScroll.Maximum;
				else
					pServices.VerticalScroll.Value  += pos.Top;
			}
			finally
			{
				skipselectingservices = false;
			}
			LvServicesEnabledSelectedIndexChanged(lvi.ListView, e);
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
		
		void LbFromMouseMove(object sender, MouseEventArgs e)
		{
			ListBox lb = sender as ListBox;
			if(lb == null)
				return;
				
			Point p = lb.PointToClient(Cursor.Position);
			int idx = lb.IndexFromPoint(p);
			if(idx >= 0)
			{
				string text = lb.Items[idx].ToString();
				if(ttMain.GetToolTip(lb) != text)
				{
					int width = 0;
					using(Graphics g = base.CreateGraphics())
					{
						width = (int)g.MeasureString(text, lb.Font, 0, StringFormat.GenericTypographic).Width;
						g.Dispose();
						width += SystemInformation.VerticalScrollBarWidth;
					}
	
					if(width > lb.ClientRectangle.Width)
						ttMain.SetToolTip(lb, text);
					else
						ttMain.SetToolTip(lb, null);
					
				}
			}
			else
			{
				ttMain.SetToolTip(lb, null);
			}
			
		}
		
		void LbFromMouseLeave(object sender, EventArgs e)
		{
			ListBox lb = sender as ListBox;
			if(lb == null)
				return;
		
			ttMain.SetToolTip(lb, null);
		}
	}
	
	
}
