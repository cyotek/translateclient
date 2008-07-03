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
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FreeCL.RTL;

namespace Translate
{
	/// <summary>
	/// Description of KeyboardHook.
	/// </summary>
	public static class KeyboardHook
	{
		public static void Init()
		{
			if(controlCC || controlInsIns)
				NativeMethods.SetHook();
			else if(!controlCC && !controlInsIns)
				NativeMethods.RemoveHook();
		}
		
		public static event EventHandler Hotkey;
		
		static void HookCalledThreadProc(object state)
		{
			if(Hotkey != null)
				Hotkey(null, new EventArgs());
		}
		
		static bool controlCC = true;
		public static bool ControlCC {
			get { return controlCC; }
			set { controlCC = value; }
		}
		
		static bool controlInsIns;
		public static bool ControlInsIns {
			get { return controlInsIns; }
			set { controlInsIns = value; }
		}
		
		static bool translateOnHotkey;
		public static bool TranslateOnHotkey {
			get { return translateOnHotkey; }
			set { translateOnHotkey = value; }
		}
		
		static class NativeMethods
		{
			private const int WH_KEYBOARD_LL = 13;
			private const int WM_KEYDOWN = 0x0100;
			private static LowLevelKeyboardProc proc = HookCallback;
			private static IntPtr hookID = IntPtr.Zero;
			
			public static void SetHook()
			{
				if(hookID == IntPtr.Zero)
					hookID = SetHook(proc);
			}
			
			public static void RemoveHook()
			{
				if(hookID != IntPtr.Zero)
				{
					UnhookWindowsHookEx(hookID);
					hookID = IntPtr.Zero;
				}
			}
			
			private static IntPtr SetHook(LowLevelKeyboardProc proc)
			{
				using (Process curProcess = Process.GetCurrentProcess())
				using (ProcessModule curModule = curProcess.MainModule)
				{
					return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
						GetModuleHandle(curModule.ModuleName), 0);
				}
			}
			
			static long controlCClickTime;
			static long controlInsClickTime;
			static long ticksInSecond = DateTimeUtils.Second.Ticks;
			
			private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
			{
				if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
				{
					Keys key = (Keys)Marshal.ReadInt32(lParam);
					
					if (Keys.C == key && Keys.Control == Control.ModifierKeys &&
						KeyboardHook.ControlCC
					)
					{
						if(controlCClickTime + ticksInSecond > DateTime.Now.Ticks)
						{
							TaskConveyer.QueueTask("hook", HookCalledThreadProc, null);
							controlCClickTime = 0;
						}
						else
							controlCClickTime = DateTime.Now.Ticks;	
					}
					
					if (Keys.Insert == key && Keys.Control == Control.ModifierKeys &&
						KeyboardHook.ControlInsIns
					)
					{
						if(controlInsClickTime + ticksInSecond > DateTime.Now.Ticks)
						{
							TaskConveyer.QueueTask("hook", HookCalledThreadProc, null);
							controlInsClickTime = 0;
						}
						else
							controlInsClickTime = DateTime.Now.Ticks;	
					}
					
				}
				return CallNextHookEx(hookID, nCode, wParam, lParam);
			}
			
			private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
			
			[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
			
			[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool UnhookWindowsHookEx(IntPtr hhk);
			
			[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
			
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			private static extern IntPtr GetModuleHandle(string lpModuleName);			
		}
	}
}
