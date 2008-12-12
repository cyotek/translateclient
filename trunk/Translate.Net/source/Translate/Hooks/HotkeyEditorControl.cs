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

namespace Translate
{
	/// <summary>
	/// Description of HotkeyEditorControl.
	/// </summary>
	public partial class HotkeyEditorControl : FreeCL.Forms.BaseUserControl
	{
		public HotkeyEditorControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			RegisterLanguageEvent(OnLanguageChanged);
			
			skipChangeEvent = true;
			foreach(Keys k in Enum.GetValues(typeof(Keys)))
			{
				if((int)k < 256 &&
					(int)k > 6 && k != Keys.IMEAceept)
				{	
					if(!cbKey.Items.Contains(k))
						cbKey.Items.Add(k);
				}		
			}
			cbKey.Items.Add(Keys.None);
			skipChangeEvent = false;
		}
		
		void OnLanguageChanged()
		{
			lModifiers.Text = TranslateString("Modifiers :");
			lMouse.Text = TranslateString("Mouse buttons :");
			lKey.Text = TranslateString("Key :");
			cbLeft.Text = TranslateString("Left{0}").Replace("{0}", "");
			cbMiddle.Text = TranslateString("Middle{0}").Replace("{0}", "");
			cbRight.Text = TranslateString("Right{0}").Replace("{0}", "");
		}

		
		Keys shortcut;
		public Keys Shortcut
		{
			get { return shortcut; }
		}
		
		MouseButtons mouseShortcut;
		public MouseButtons MouseShortcut
		{
			get { return mouseShortcut; }
		}
		
		void SetMouse(CheckBox cb, MouseButtons button, MouseButtons buttons, bool editable)
		{
			cb.Checked = (buttons & button) > 0;
			cb.Enabled = editable;
		}
		
		void SetModifiers(CheckBox cb, Keys key, Keys keys, bool editable)
		{
			cb.Checked = ((keys & Keys.Modifiers) & key) > 0;
			cb.Enabled = editable;
		}
		
		bool skipChangeEvent = false;
		public void SetShortcut(Keys shortcut, MouseButtons mouseButtons, bool editable)
		{
			this.shortcut = shortcut;
			this.mouseShortcut = mouseButtons;
			skipChangeEvent = true;
			SetMouse(cbLeft, MouseButtons.Left, mouseButtons, editable);
			SetMouse(cbMiddle, MouseButtons.Middle, mouseButtons, editable);
			SetMouse(cbRight, MouseButtons.Right, mouseButtons, editable);
			SetMouse(cbX1, MouseButtons.XButton1, mouseButtons, editable);
			SetMouse(cbX2, MouseButtons.XButton2, mouseButtons, editable);

			SetModifiers(cbCtrl, Keys.Control, shortcut, editable);	
			SetModifiers(cbAlt, Keys.Alt, shortcut, editable);	
			SetModifiers(cbShift, Keys.Shift, shortcut, editable);	
			
			cbKey.SelectedItem = shortcut & Keys.KeyCode;
			cbKey.Enabled = editable;
			skipChangeEvent = false;
		}
		
		void SetMouse(CheckBox cb, MouseButtons button)
		{
			if(cb.Checked)
				mouseShortcut |= button;
			else
				mouseShortcut &= ~button;
		}
		
		void SetModifiers(CheckBox cb, Keys key)
		{
			if(cb.Checked)
				shortcut |= key;
			else
				shortcut &= ~key;
		}
		
		public event EventHandler ShortcutChanged;

		void CbCtrlCheckedChanged(object sender, EventArgs e)
		{
			if(skipChangeEvent)
				return;
			SetMouse(cbLeft, MouseButtons.Left);
			SetMouse(cbMiddle, MouseButtons.Middle);
			SetMouse(cbRight, MouseButtons.Right);
			SetMouse(cbX1, MouseButtons.XButton1);
			SetMouse(cbX2, MouseButtons.XButton2);

			SetModifiers(cbCtrl, Keys.Control);
			SetModifiers(cbAlt, Keys.Alt);
			SetModifiers(cbShift, Keys.Shift);
			
			Keys modifiers = shortcut & Keys.Modifiers;
			shortcut = (((Keys)cbKey.SelectedItem) & Keys.KeyCode) | modifiers;
			
			if(ShortcutChanged != null)
				ShortcutChanged(this, new EventArgs());
		}
	}
}
