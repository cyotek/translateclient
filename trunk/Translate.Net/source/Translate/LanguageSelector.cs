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
			OnLanguageChanged();
		}
		
		void OnLanguageChanged()
		{
			if(languages != null)
			{
				LoadLanguages();
				LoadSubjects();
				LoadHistory();
			}
			tpLangs.Text = LangPack.TranslateString("Languages");
			tpSubject.Text = LangPack.TranslateString("Subjects");
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
					
					if(selection == null || selection == value)
						return;
						
					//selection = value; 
					LanguageContainer from = new LanguageContainer(value.From, "");
					LanguageContainer to = new LanguageContainer(value.To, "");
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
				
				LanguageContainerPair currSel = new LanguageContainerPair((LanguageContainer)lbFrom.SelectedItem, (LanguageContainer)lbTo.SelectedItem);	
				return currSel.ToString();
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

		class LanguageContainer
		{
			public LanguageContainer(Language language, string text)
			{
				Language = language;
				Text = text;
			}
			
			public Language Language;
			public string Text;
			
			public override string ToString()
			{
				return Text;
			}
			
			public override bool Equals(Object obj)
			{
				if(obj == null) return false;
				LanguageContainer arg = obj as LanguageContainer;
				if(arg == null) return false;
				return Language ==  arg.Language;
			}				
			
			public override int GetHashCode() 
			{
      			return Language.GetHashCode();
   			}	
   			
			public static bool operator ==(LanguageContainer a, LanguageContainer b)
			{
				bool anull, bnull;
				anull = Object.ReferenceEquals(a,null); 
				bnull = Object.ReferenceEquals(b,null);
				if (anull && bnull) return true;
				if (anull || bnull) return false;
				return a.Equals(b);
			}
	
			public static bool operator !=(LanguageContainer a, LanguageContainer b)
			{
				return !(a == b);
			}
   			
			
		}
		
		class LanguageContainerPair
		{
			public LanguageContainerPair(LanguageContainer from, LanguageContainer to)
			{
				From = from;
				To = to;
			}
			
			public LanguageContainer From;
			public LanguageContainer To;
			
			public override bool Equals(Object obj)
			{
				LanguageContainerPair arg = obj as LanguageContainerPair;
				if(arg == null) return false;
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
			SuspendLayout();
			lbFrom.Items.Clear();
			
			LanguageCollection fromLangs = new LanguageCollection();
			
			foreach(LanguagePair lp in languages)
			{
				if(!fromLangs.Contains(lp.From))
					fromLangs.Add(lp.From);
					
			}
			
			fromLangs.Sort();
			
			string val = "";
			
			if(fromLangs.Count > 1)
			{
				val = Enum.GetName(typeof(Language), Language.Any);
				val = "+" + LangPack.TranslateString(val);
				lbFrom.Items.Add(new LanguageContainer(Language.Any, val));
			}
			
			foreach(Language l in fromLangs)
			{
				val = Enum.GetName(typeof(Language), l);
				val = LangPack.TranslateString(val);
				lbFrom.Items.Add(new LanguageContainer(l, val));
			}

			if(lbFrom.Items.Count > 0)
				lbFrom.SelectedIndex = 0;
			ResumeLayout(true);
		}
		

		void LbFromSelectedIndexChanged(object sender, EventArgs e)
		{
			SuspendLayout();
			if(lbFrom.SelectedIndex == -1)
				return;
				
			Language fromLanguage = ((LanguageContainer)lbFrom.SelectedItem).Language;	
			lbTo.Items.Clear();
			
			LanguageCollection toLangs = new LanguageCollection();
			
			foreach(LanguagePair lp in languages)
			{
				if((lp.From == fromLanguage || fromLanguage == Language.Any) && !toLangs.Contains(lp.To))
					toLangs.Add(lp.To);
			}
			
			toLangs.Sort();
			
			string val = "";
			
			if(toLangs.Count > 1)
			{
				val = Enum.GetName(typeof(Language), Language.Any);
				val = "+" + LangPack.TranslateString(val);
				lbTo.Items.Add(new LanguageContainer(Language.Any, val));
			}
			
			foreach(Language l in toLangs)
			{
				val = Enum.GetName(typeof(Language), l);
				val = LangPack.TranslateString(val);
				lbTo.Items.Add(new LanguageContainer(l, val));
			}

			string caption = LangPack.TranslateString(Enum.GetName(typeof(Language), fromLanguage));
			lFrom.Text = caption;
			
			lbTo.SelectedIndex = 0;
			ResumeLayout(true);
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

			Language fromLanguage = ((LanguageContainer)lbFrom.SelectedItem).Language;	
			Language toLanguage = ((LanguageContainer)lbTo.SelectedItem).Language;				
			selection = new LanguagePair(fromLanguage, toLanguage);
			
			
			string caption = LangPack.TranslateString(Enum.GetName(typeof(Language), toLanguage));
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
		}
		
		void PColumnsResize(object sender, EventArgs e)
		{
			lFrom.Width = (pColumns.ClientSize.Width - sbInvert.Width)/2 - 3;
		}
		
		public void Invert()
		{
			LanguageContainer fromLanguage = (LanguageContainer)lbFrom.SelectedItem;
			
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
			lbHistory.Items.Clear();
			foreach(LanguagePair lp in history)
			{
				LanguageContainerPair lpc = new LanguageContainerPair(
					new LanguageContainer(lp.From, LangPack.TranslateString(Enum.GetName(typeof(Language), lp.From))),
					new LanguageContainer(lp.To, LangPack.TranslateString(Enum.GetName(typeof(Language), lp.To)))
					);
				lbHistory.Items.Add(lpc);
			}
			
			if(history.Count > 0)
				lbHistory.SelectedIndex = 0;
		}
		
		public void AddSelectionToHistory()
		{
			if(lbFrom.SelectedIndex == -1 || lbTo.SelectedIndex == -1)
				return;
				
			LanguageContainerPair currSel = new LanguageContainerPair((LanguageContainer)lbFrom.SelectedItem, (LanguageContainer)lbTo.SelectedItem);	
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
		
		void LbHistorySelectedIndexChanged(object sender, EventArgs e)
		{
			if(lbHistory.SelectedIndex == -1)
				return;
				
			LanguageContainerPair currSel = new LanguageContainerPair((LanguageContainer)lbFrom.SelectedItem, (LanguageContainer)lbTo.SelectedItem);
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
					Selection =  new LanguagePair(Language.Any, Language.Any);
				}
			}
		}
		
		class SubjectContainer
		{
			public SubjectContainer(string subject, string text)
			{
				this.Subject = subject;
				this.Text = text;
			}
			
			public string Subject;
			public string Text;
			
			public override string ToString()
			{
				return Text;
			}
			
		}
		
		SubjectCollection supportedSubjects;
		SubjectCollection subjects;
		bool loadingSubjects;
		void LoadSubjects()
		{
			loadingSubjects = true;
			lbSubjects.Items.Clear();
			string val;
			foreach(string s in supportedSubjects)
			{
				val = LangPack.TranslateString(s);
				if(s == "Common")
					val = "+" + val;
				lbSubjects.Items.Add(new SubjectContainer(s, val), subjects.Contains(s));
			}		
			loadingSubjects = false;
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public void SetSubjects(SubjectCollection supportedSubjects, SubjectCollection subjects)
		{
			this.supportedSubjects = supportedSubjects;
			this.subjects = subjects;
			LoadSubjects();
		}
		
		public event EventHandler SubjectsChanged;
		
		void LbSubjectsItemCheck(object sender, ItemCheckEventArgs e)
		{
			if(loadingSubjects) return;

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
						new_idx = 0;
					}
					lbSubjects.SetItemChecked(new_idx, true);
				}
				subjects.Remove(sc.Subject);
			}
				
			if(SubjectsChanged != null)
				SubjectsChanged(this, new EventArgs()); 
		}
	}
	
	
}
