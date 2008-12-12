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
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using FreeCL.Forms;

namespace Translate
{
	/// <summary>
	/// Description of HotkeysOptrionsControl.
	/// </summary>
	public partial class HotkeysOptionsControl : FreeCL.Forms.BaseOptionsControl
	{
		public HotkeysOptionsControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
		}
		
		private class HotkeyData
		{
			public HotkeyData(Keys keys, MouseButtons mouseButtons)
			{
				this.keys = keys;
				this.mouseButtons = mouseButtons;
				this.name = HotkeysOptionsControl.GetHotkeyName(keys, mouseButtons);
				this.caption = LangPack.TranslateString(name);
			}

			public HotkeyData(Keys keys, MouseButtons mouseButtons, bool custom)
			{
				this.keys = keys;
				this.mouseButtons = mouseButtons;
				this.name = HotkeysOptionsControl.GetHotkeyName(keys, mouseButtons, custom);
				this.caption = LangPack.TranslateString(name);
			}
		
			Keys keys;
			public Keys Keys
			{
				get { return keys; }
				set { keys = value; }
			}
			
			MouseButtons mouseButtons;
			public MouseButtons MouseButtons
			{
				get { return mouseButtons; }
				set { mouseButtons = value; }
			}
			
			string name;
			public string Name
			{
				get { return name; }
				set { name = value; }
			}
			
			string caption;
			public string Caption
			{
				get { return caption; }
				set { caption = value; }
			}
			
			public override string ToString()
			{
				return caption;
			}
			
			
		}
		
		public static string GetHotkeyName(Keys keys, MouseButtons mouseButtons)
		{
			return GetHotkeyName(keys, mouseButtons, false);
		}
		
		public static string GetHotkeyName(Keys keys, MouseButtons mouseButtons, bool custom)
		{
			string result = "Custom Hotkey";
			if(custom)
				return result;
			if(Keys.None == keys && MouseButtons.None == mouseButtons)
				result = "Hotkey not set"; 
			else if(Keys.Alt == keys && MouseButtons.Right == mouseButtons)
				result = "Alt + Right Mouse Button"; 
			else if(Keys.None == keys && MouseButtons.Middle == mouseButtons)
				result = "Middle Mouse Button"; 
			else if(Keys.Control == keys && MouseButtons.Right == mouseButtons)
				result = "Ctrl + Right Mouse Button"; 
			else if(Keys.Shift == keys && MouseButtons.Right == mouseButtons)
				result = "Shift + Right Mouse Button"; 
			else if(Keys.None == keys && (MouseButtons.Left | MouseButtons.Right) == mouseButtons)
				result = "Left Mouse Button + Right Mouse Button"; 
			return result;
		}
		
		void AddItems()
		{
			cbHotkeys.Items.Clear();
			cbHotkeys.Items.Add(new HotkeyData(Keys.None, MouseButtons.None));
			cbHotkeys.Items.Add(new HotkeyData(Keys.None, MouseButtons.None, true));
			cbHotkeys.Items.Add(new HotkeyData(Keys.Alt, MouseButtons.Right));
			cbHotkeys.Items.Add(new HotkeyData(Keys.None, MouseButtons.Middle));
			cbHotkeys.Items.Add(new HotkeyData(Keys.Control, MouseButtons.Right));
			cbHotkeys.Items.Add(new HotkeyData(Keys.Shift, MouseButtons.Right));
			cbHotkeys.Items.Add(new HotkeyData(Keys.None, MouseButtons.Left | MouseButtons.Right));
		}
		
		void OnLanguageChanged()
		{
			cbControlCC.Text = TranslateString("Activate on Ctrl+C+C hotkey");
			cbControlInsIns.Text = TranslateString("Activate on Ctrl+Ins+Ins hotkey");
			cbTranslateOnHotkey.Text = TranslateString("Translate when activated by hotkey");
			gbAdvanced.Text = TranslateString("Advanced Hotkey");

			int savedIdx = cbHotkeys.SelectedIndex;
			AddItems();
			if(savedIdx != -1)
				cbHotkeys.SelectedIndex = savedIdx;
		}
		
		HookOptions current;
		MouseButtons selectedMouseShortcut = MouseButtons.None;
		Keys selectedKeysShortcut = Keys.None;		
		
		public override void Init()
		{
			current = TranslateOptions.Instance.HookOptions;
			cbControlCC.Checked = current.ControlCC;
			cbControlInsIns.Checked = current.ControlInsIns;
			cbTranslateOnHotkey.Checked = current.TranslateOnHotkey;
			
			if(current.Shortcut == Keys.None && current.MouseShortcut == MouseButtons.None)
				cbHotkeys.SelectedIndex = 0;
			else 
			{
				selectedMouseShortcut = current.MouseShortcut;
				selectedKeysShortcut = current.Shortcut;
				cbHotkeys.SelectedIndex = 1;
				
				foreach(object o in cbHotkeys.Items)
				{
					HotkeyData hd = o as HotkeyData;
					if(current.Shortcut == hd.Keys && current.MouseShortcut == hd.MouseButtons)
					{
						cbHotkeys.SelectedItem = hd;
						break;
					}
				}					
			}
		}
		
		public override void Apply()
		{
			current.ControlCC = cbControlCC.Checked;
			current.ControlInsIns = cbControlInsIns.Checked;
			current.TranslateOnHotkey = cbTranslateOnHotkey.Checked;
			current.Shortcut = selectedKeysShortcut;
			current.MouseShortcut = selectedMouseShortcut;
			
			KeyboardHook.Init();
		}
		
		public override bool IsChanged()
		{
			return 
				current.ControlCC != cbControlCC.Checked ||
				current.ControlInsIns != cbControlInsIns.Checked ||
				current.TranslateOnHotkey != cbTranslateOnHotkey.Checked ||
				current.Shortcut != selectedKeysShortcut ||
				current.MouseShortcut != selectedMouseShortcut;
		}
		
		
		void CbHotkeysSelectedIndexChanged(object sender, EventArgs e)
		{
			HotkeyData hd = cbHotkeys.SelectedItem as HotkeyData;
			if(hd != null)
			{
				if(hd.Name == "Custom Hotkey")
					hotkeyEditor.SetShortcut(selectedKeysShortcut, selectedMouseShortcut, true);
				else
				{
					hotkeyEditor.SetShortcut(hd.Keys, hd.MouseButtons, false);
					selectedKeysShortcut = hd.Keys;
					selectedMouseShortcut = hd.MouseButtons;
				}	
			}

		}
		
		void HotkeyEditorShortcutChanged(object sender, EventArgs e)
		{
			selectedKeysShortcut = hotkeyEditor.Shortcut;
			selectedMouseShortcut = hotkeyEditor.MouseShortcut;
		}
	}
}
