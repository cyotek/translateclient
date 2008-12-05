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
using System.Data;

namespace Translate
{
	/// <summary>
	/// Description of KeyboardLayoutControl.
	/// </summary>
	public partial class KeyboardLayoutControl : FreeCL.Forms.BaseOptionsControl
	{
		public KeyboardLayoutControl()
		{
			InitializeComponent();
			RegisterLanguageEvent(OnLanguageChanged);
		}
		
		internal class LayoutData : IComparable<LayoutData>
		{
			public LayoutData(string name, string displayName)
			{
				this.name = name;
				this.displayName = displayName;
			}
			
			string name;
			public string Name
			{
				get { return name; }
				set { name = value; }
			}
			
			
			string displayName;
			public string DisplayName
			{
				get { return displayName; }
				set { displayName = value; }
			}
			
			public int CompareTo(LayoutData other)
			{
				if(name == dontchangeStr && other.name == name)
					return 0;
				if(name == dontchangeStr)
					return -1;
				if(other.name == dontchangeStr)
					return 1;
				return string.Compare(displayName, other.displayName);
			}
			
		}
		List<LayoutData> layouts = new List<LayoutData>();
		Dictionary<string, LayoutData> layoutsKeys = new Dictionary<string, LayoutData>();
		
		
		const string dontchangeStr = "Don't change"; 
		string dontchangeStrTranslated;

		void OnLanguageChanged()
		{
			//Keyboard layouts
			cbSwitchLayoutBasedOnLanguage.Text = TranslateString("Switch keyboard layout based on selected language");
			languageDataGridViewTextBoxColumn.HeaderText = TranslateString("Language");
			languageDataGridViewTextBoxColumn.ToolTipText = TranslateString("Selected language");
			
			LayoutId.HeaderText = TranslateString("Change keyboard layout to");
			LayoutId.ToolTipText = TranslateString("When source language changes switch keyboard layout to");
			
			Changed.HeaderText = TranslateString("Changed");
			Changed.ToolTipText = TranslateString("Modified by user");
			
			Auto.HeaderText = TranslateString("Auto");
			Auto.ToolTipText = TranslateString("Assigned automatically");
			
			gbLayouts.Text = TranslateString("Selected language to keyboard layout mapping");
			
			dontchangeStrTranslated = TranslateString(dontchangeStr);

			LoadLayouts();
			LoadLangToLayout();
		}
		
		void LoadLangToLayout()
		{
			DataTable dtLangs = dsMain.Tables["LangToLayouts"];
			if(dtLangs.Rows.Count == 0)
			{
				for(int i = 1; i < (int)Language.Last; i++)
				{
					Language lng = (Language)i;
					if(lng == Language.Autodetect)
						continue;
						
					DataRow row = dtLangs.NewRow();
					row["Language"] = lng;
					row["DisplayName"] = LangPack.TranslateLanguage(lng);
					row["Layout"] = dontchangeStr;
					dtLangs.Rows.Add(row);
				}		
			}
			else
			{
				for(int i = 1; i < (int)Language.Last; i++)
				{
					Language lng = (Language)i;
					if(lng == Language.Autodetect)
						continue;
						
					DataRow row = dtLangs.Rows.Find(lng);
					row["DisplayName"] = LangPack.TranslateLanguage(lng);
				}		
			}
			
			string tooltip;
			
			foreach(DataGridViewRow gvRow in dgvLayouts.Rows)
			{
				DataRowView rowView = gvRow.DataBoundItem as DataRowView;
				DataRow row = rowView.Row;
				
				if(row.IsNull("SavedLayout"))
					break;
				row["Changed"] = String.Compare((string)row["Layout"], (string)row["SavedLayout"], false, CultureInfo.InvariantCulture) != 0;	
				row["Auto"] = String.Compare((string)row["Layout"], (string)row["AutoLayout"], false, CultureInfo.InvariantCulture) == 0;	
				gvRow.Cells[2].ReadOnly = (bool)row["Auto"];
				gvRow.Cells[3].ReadOnly = !(bool)row["Changed"];
				
				tooltip = GenerateTooltip(row);
				for(int i = 0; i < 4; i++)
					gvRow.Cells[i].ToolTipText = tooltip;
			}
			
		}
		
		void LoadLayouts()
		{
			
			LayoutData ld;
			if(layouts.Count == 0)
			{
				ld = new LayoutData(dontchangeStr, dontchangeStrTranslated);
				layoutsKeys.Add(ld.Name, ld);
				layouts.Add(ld);	
				foreach(InputLanguage il in InputLanguage.InstalledInputLanguages)
				{
					ld = new LayoutData(il.LayoutName + il.Culture.EnglishName, TranslateString(il.Culture.Parent.EnglishName) +  " (" + il.LayoutName + ") - " + il.Culture.EnglishName);
					layoutsKeys.Add(ld.Name, ld);
					layouts.Add(ld);	
				}
			}
			else
			{
				ld = layoutsKeys[dontchangeStr];
				ld.DisplayName = dontchangeStrTranslated;
				foreach(InputLanguage il in InputLanguage.InstalledInputLanguages)
				{
					if(!layoutsKeys.TryGetValue(il.LayoutName + il.Culture.EnglishName, out ld))
					{
						ld = new LayoutData(il.LayoutName + il.Culture.EnglishName, TranslateString(il.Culture.Parent.EnglishName) +  " (" + il.LayoutName + ") - " + il.Culture.EnglishName);
						layoutsKeys.Add(ld.Name, ld);
					}
					else
						ld.DisplayName = TranslateString(il.Culture.Parent.EnglishName) +  " (" + il.LayoutName + ") - " + il.Culture.EnglishName;
				}
			}
			layouts.Sort();
			DataGridViewComboBoxColumn columnCombo =  dgvLayouts.Columns["LayoutId"] as DataGridViewComboBoxColumn;
			columnCombo.DataSource = layouts;
			columnCombo.DisplayMember = "DisplayName";
			columnCombo.ValueMember = "Name";
		}
		
		void InitLangsToLayout()
		{
			DataTable dtLangs = dsMain.Tables["LangToLayouts"];
			bool autoSupported;
			for(int i = 1; i < (int)Language.Last; i++)
			{
				Language lng = (Language)i;
				if(lng == Language.Autodetect)
					continue;

				DataRow row = dtLangs.Rows.Find(lng);	
				row["Auto"] = true;	
				row["Changed"] = false;
					
				autoSupported = false;
				foreach(InputLanguage il in InputLanguage.InstalledInputLanguages)
				{
					if(InputLanguageManager.IsLanguageSupported(il, lng))
					{
						autoSupported = true;
						string layoutName = il.LayoutName + il.Culture.EnglishName;
						row["SavedLayout"] = layoutName;
						row["Layout"] = layoutName;
						row["AutoLayout"] = layoutName;
						row["Auto"] = true;
						break;
					}	
				}
				if(!autoSupported)
				{
					row["SavedLayout"] = dontchangeStr;
					row["Layout"] = dontchangeStr;
					row["AutoLayout"] = dontchangeStr;
				}
			}		
			
			foreach(KeyboardLayoutLanguage kll in options.CustomLayouts)
			{
				if(layoutsKeys.ContainsKey(kll.LayoutName))
				{
					DataRow row = dtLangs.Rows.Find(kll.Language);	
					row["SavedLayout"] = kll.LayoutName;
					row["Layout"] = kll.LayoutName;
					row["Auto"] = false;	
				}
			}
			//DataView dv = dtLangs.DefaultView;
			//dv.Sort = "Layout DESC";
			//dgvLayouts.DataSource = dv;
			DataView dv = dtLangs.DefaultView;
			dv.Sort = "DisplayName";
			dgvLayouts.DataSource = dv;
			dgvLayouts.Invalidate();
			string tooltip;
			
			foreach(DataGridViewRow gvRow in dgvLayouts.Rows)
			{
				DataRowView rowView = gvRow.DataBoundItem as DataRowView;
				DataRow row = rowView.Row;
				row["Changed"] = String.Compare((string)row["Layout"], (string)row["SavedLayout"], false, CultureInfo.InvariantCulture) != 0;	
				row["Auto"] = String.Compare((string)row["Layout"], (string)row["AutoLayout"], false, CultureInfo.InvariantCulture) == 0;	
				gvRow.Cells[2].ReadOnly = (bool)row["Auto"];
				gvRow.Cells[3].ReadOnly = !(bool)row["Changed"];
				
				tooltip = GenerateTooltip(row);
				for(int i = 0; i < 4; i++)
					gvRow.Cells[i].ToolTipText = tooltip;
			}
		}
		
		string GenerateTooltip(DataRow row)
		{
			string tooltip;
			LayoutData ld;
			
			ld = layoutsKeys[(string)row["Layout"]];
			tooltip = TranslateString("Selected\t :") + " " + ld.DisplayName;

				
			ld = layoutsKeys[(string)row["AutoLayout"]];
			tooltip += Environment.NewLine +
				TranslateString("Auto\t\t :") + " " +
				ld.DisplayName;
				
			ld = layoutsKeys[(string)row["SavedLayout"]];
			tooltip += Environment.NewLine + 
				TranslateString("Saved\t\t :")  +  " " +
				ld.DisplayName;
			return tooltip;	
		}
		
		KeyboardLayoutOptions options;
		public override void Init()
		{
			options = TranslateOptions.Instance.KeyboardLayoutsOptions;
			InitLangsToLayout();
			cbSwitchLayoutBasedOnLanguage.Checked = options.SwitchLayoutsBasedOnLanguage;
		}		
		
		
		public override void Apply()
		{
			options.SwitchLayoutsBasedOnLanguage = cbSwitchLayoutBasedOnLanguage.Checked;
			options.CustomLayouts.Clear();
			
			DataTable dtLangs = dsMain.Tables["LangToLayouts"];
			foreach(DataRow row in dtLangs.Rows)
			{
				if(!(bool)row["Auto"])
				{
					options.CustomLayouts.Add((string)row["Layout"], (Language)row["Language"]);
				}
			}
			InitLangsToLayout();
		}
		
		public override bool IsChanged()
		{
			bool res = options.SwitchLayoutsBasedOnLanguage != cbSwitchLayoutBasedOnLanguage.Checked;
			
			if(!res && cbSwitchLayoutBasedOnLanguage.Checked)
			{	
				DataTable dtLangs = dsMain.Tables["LangToLayouts"];
				foreach(DataRow row in dtLangs.Rows)
				{
					if((bool)row["Changed"])
					{
						res = true;
						break;
					}
				}
			}
			
			return res;
			
		}

		void CbSwitchLayoutBasedOnLanguageCheckedChanged(object sender, System.EventArgs e)
		{
			dgvLayouts.Enabled = cbSwitchLayoutBasedOnLanguage.Checked;
			dgvLayouts.DefaultCellStyle.BackColor = !dgvLayouts.Enabled ? SystemColors.Control : SystemColors.Window;
		}
		
		
		
		void DgvLayoutsCellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if(e.RowIndex < 0)
				return;
			DataRowView rowView = dgvLayouts.Rows[e.RowIndex].DataBoundItem as DataRowView;
			DataRow row = rowView.Row;
			
			if (e.ColumnIndex == 2)
			{
				if((bool)row["Auto"])
					row["Layout"] = row["AutoLayout"];
				row["Changed"] = String.Compare((string)row["Layout"], (string)row["SavedLayout"], false, CultureInfo.InvariantCulture) != 0;	
			}
			else if (e.ColumnIndex == 3)
			{
				if(!(bool)row["Changed"])
					row["Layout"] = row["AutoLayout"];
				row["Auto"] = String.Compare((string)row["Layout"], (string)row["AutoLayout"], false, CultureInfo.InvariantCulture) == 0;	
			}
			else
			{
				row["Changed"] = String.Compare((string)row["Layout"], (string)row["SavedLayout"], false, CultureInfo.InvariantCulture) != 0;	
				row["Auto"] = String.Compare((string)row["Layout"], (string)row["AutoLayout"], false, CultureInfo.InvariantCulture) == 0;	
			}
			
			
			
			dgvLayouts.Rows[e.RowIndex].Cells[2].ReadOnly = (bool)row["Auto"];
			dgvLayouts.Rows[e.RowIndex].Cells[3].ReadOnly = !(bool)row["Changed"];
			
			string tooltip = GenerateTooltip(row);
			for(int i = 0; i < 4; i++)
				dgvLayouts.Rows[e.RowIndex].Cells[i].ToolTipText = tooltip;
			
			
			dgvLayouts.Invalidate();
		}
		
		void DgvLayoutsCurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			if (dgvLayouts.IsCurrentCellDirty)
			{
				dgvLayouts.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}
		
		void DgvLayoutsCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if(e.ColumnIndex == 2)
			{
				e.CellStyle.BackColor = (bool)e.Value ? SystemColors.Control : SystemColors.Window;
			}
			else if(e.ColumnIndex == 3)
			{
				e.CellStyle.BackColor = !(bool)e.Value ? SystemColors.Control : SystemColors.Window;
			}
		}
	}
}
