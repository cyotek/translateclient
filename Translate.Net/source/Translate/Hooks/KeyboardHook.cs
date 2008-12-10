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
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of KeyboardHook.
	/// </summary>
	public static class KeyboardHook
	{
		public static void Init()
		{
			if(controlCC || controlInsIns || shortcut != Keys.None)
				NativeMethods.SetHook();
			else if(!controlCC && !controlInsIns && shortcut == Keys.None)
				NativeMethods.RemoveHook();
				
			if(mouseShortcut != MouseButtons.None)
				NativeMethods.SetMouseHook();
			else if(mouseShortcut == MouseButtons.None)
				NativeMethods.RemoveMouseHook();
		}
		
		public static event EventHandler Hotkey;
		
		static void HookCalledThreadProc(object state)
		{
			if(Hotkey != null)
				Hotkey(null, new EventArgs());
		}

		public static event EventHandler AdvancedHotkey;
		static void HookCalledThreadProcAdv(object state)
		{
			if(AdvancedHotkey != null)
				AdvancedHotkey(null, new EventArgs());
		}

		static Keys shortcut = Keys.Alt;		
		public static Keys Shortcut
		{
			get { return shortcut; }
			set { shortcut = value; }
		}
		
		static MouseButtons mouseShortcut = MouseButtons.Right;
		public static MouseButtons MouseShortcut
		{
			get { return mouseShortcut; }
			set { mouseShortcut = value; }
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
		
		static bool translateOnHotkey = true;
		public static bool TranslateOnHotkey {
			get { return translateOnHotkey; }
			set { translateOnHotkey = value; }
		}
		
		public static void SendCtrlC()
		{
			NativeMethods.SendCtrlC();
		}
		
		public static void DisableMouseKeys()
		{
			NativeMethods.DisableMouseKeys();
		}
		static class NativeMethods
		{
			private const int WH_KEYBOARD_LL = 13;
			private const int WH_MOUSE_LL = 14;
			private const int WM_KEYDOWN = 0x0100;
			private const int WM_KEYUP   = 0x0101;
			private static LowLevelKeyboardProc proc = HookCallback;
			private static LowLevelKeyboardProc mouseproc = MouseHookCallback;
			private static IntPtr hookID = IntPtr.Zero;
			private static IntPtr mouseHookID = IntPtr.Zero;
			
			public static void SetHook()
			{
				if(hookID == IntPtr.Zero)
					hookID = SetHook(proc);
			}

			public static void SetMouseHook()
			{
				if(mouseHookID == IntPtr.Zero)
					mouseHookID = SetMouseHook(mouseproc);
			}
			
			public static void RemoveHook()
			{
				if(hookID != IntPtr.Zero)
				{
					UnhookWindowsHookEx(hookID);
					hookID = IntPtr.Zero;
				}
			}

			public static void RemoveMouseHook()
			{
				if(mouseHookID != IntPtr.Zero)
				{
					UnhookWindowsHookEx(mouseHookID);
					mouseHookID = IntPtr.Zero;
					mouseButtons = MouseButtons.None;
					mouseButtonsToSkipUp = MouseButtons.None;
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

			private static IntPtr SetMouseHook(LowLevelKeyboardProc proc)
			{
				using (Process curProcess = Process.GetCurrentProcess())
				using (ProcessModule curModule = curProcess.MainModule)
				{
					return SetWindowsHookEx(WH_MOUSE_LL, proc,
						GetModuleHandle(curModule.ModuleName), 0);
				}
			}
			
			static long controlCClickTime;
			static long controlInsClickTime;
			static long ticksInSecond = DateTimeUtils.Second.Ticks;
			
			static Keys modifierKeys;
			static Dictionary<Keys, bool> keys = new Dictionary<Keys, bool>();
			
			private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
			{
				if(ignoreHookProcessing)
					return CallNextHookEx(hookID, nCode, wParam, lParam);
			
				if (nCode >= 0)
				{
					Keys key;
					if(wParam == (IntPtr)WM_KEYDOWN)
					{
						modifierKeys = Control.ModifierKeys;
						key = (Keys)Marshal.ReadInt32(lParam);
						keys[key] = true;
						
						if (Keys.C == key && Keys.Control == modifierKeys &&
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
						
						if (Keys.Insert == key && Keys.Control == modifierKeys &&
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
						
						CheckShourcut();
					}
					else if(wParam == (IntPtr)WM_KEYUP)
					{
						key = (Keys)Marshal.ReadInt32(lParam);
						keys[key] = false;
					}

					//System.Diagnostics.Trace.Write("Keys : " + key.ToString());
				}
				return CallNextHookEx(hookID, nCode, wParam, lParam);
			}

			private const uint WM_LBUTTONDOWN = 0x201;
			private const uint WM_LBUTTONUP = 0x202;
			private const uint WM_NCLBUTTONDOWN      = 0x00A1;
			private const uint WM_NCLBUTTONUP        = 0x00A2;
			
			private const uint WM_RBUTTONDOWN = 0x204;
			private const uint WM_RBUTTONUP = 0x205;
			private const uint WM_NCRBUTTONDOWN      = 0x00A4;
			private const uint WM_NCRBUTTONUP        = 0x00A5;
			
			private const uint WM_MBUTTONDOWN = 0x207;
			private const uint WM_MBUTTONUP = 0x208;
			private const uint WM_NCMBUTTONDOWN      = 0x00A7;
			private const uint WM_NCMBUTTONUP        = 0x00A8;
			
			private const uint WM_XBUTTONDOWN = 0x020B;
			private const uint WM_XBUTTONUP   = 0x020C;
			private const uint WM_NCXBUTTONDOWN      = 0x00AB;
			private const uint WM_NCXBUTTONUP        = 0x00AC;
			
 			
			static MouseButtons mouseButtons = MouseButtons.None;
			static MouseButtons mouseButtonsToSkipUp = MouseButtons.None;
			
			private static bool CheckShourcut()
			{
				if(shortcut == Keys.None && mouseShortcut == MouseButtons.None)
					return false;
					
				bool mouseSet = mouseButtons == mouseShortcut; 
				bool modifiersSet = (modifierKeys & Keys.Modifiers) == (shortcut & Keys.Modifiers);
				
				bool keySet = true;
				Keys key = (shortcut & Keys.KeyCode);
				if(key != Keys.None)
				{
					bool keydata = false;
					keys.TryGetValue(key, out keydata);
					keySet = keydata;
				}
					
				bool result = false;
				//System.Diagnostics.Trace.Write("Mouse : " + mouseButtons.ToString());
				//System.Diagnostics.Trace.Write("hook catched : " + mouseSet + modifiersSet + keySet);
				if (mouseSet && modifiersSet && keySet)
				{
					TaskConveyer.QueueTask("hook", HookCalledThreadProcAdv, null);
					result = true;
					mouseButtonsToSkipUp = mouseButtons;
				}
				return result;
			}
			
			[StructLayout(LayoutKind.Sequential)]
		    private class MSLLHOOKSTRUCT
		    {
		        public POINT pt;
		        public int mouseData;
		        public int flags;
		        public int time;
		        public IntPtr dwExtraInfo;
		    }			
		    
			[StructLayout(LayoutKind.Sequential)]
		    private struct POINT
		    {
		        public int x;
		        public int y;
		    }
		    
		    static bool SkipMouseButtonUp = false;
		    private static bool SetButton(MouseButtons button, IntPtr wParam, uint downWM, uint upWM)
		    {
		    	bool result = false;
		    	if(wParam == (IntPtr)downWM)
		    	{
		    		mouseButtons |= button;
		    		result = true;
		    	}	
		    	else if(wParam == (IntPtr)upWM)
		    	{
		    		mouseButtons &= ~button;
		    		if((mouseButtonsToSkipUp & button) > 0)
		    		{
		    			SkipMouseButtonUp = true;
		    			mouseButtonsToSkipUp &= ~button;
		    		}
		    	}	
		    	return result;	

		    }
		    
			private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
			{
				if(ignoreHookProcessing)
					return CallNextHookEx(hookID, nCode, wParam, lParam);
			
				if (nCode >= 0)
				{
				  	bool changed = false;
					switch((uint)wParam)
					{
						case WM_LBUTTONDOWN:
						case WM_LBUTTONUP:
							changed = SetButton(MouseButtons.Left, wParam, WM_LBUTTONDOWN, WM_LBUTTONUP);
							break;
						case WM_NCLBUTTONDOWN:
						case WM_NCLBUTTONUP:
							changed = SetButton(MouseButtons.Left, wParam, WM_NCLBUTTONDOWN, WM_NCLBUTTONUP);
							break;
						case WM_RBUTTONDOWN:
						case WM_RBUTTONUP:
							changed = SetButton(MouseButtons.Right, wParam, WM_RBUTTONDOWN, WM_RBUTTONUP);
							break;
						case WM_NCRBUTTONDOWN:
						case WM_NCRBUTTONUP:
							changed = SetButton(MouseButtons.Right, wParam, WM_NCRBUTTONDOWN, WM_NCRBUTTONUP);
							break;
						case WM_MBUTTONDOWN:
						case WM_MBUTTONUP:
							changed = SetButton(MouseButtons.Middle, wParam, WM_MBUTTONDOWN, WM_MBUTTONUP);
							break;
						case WM_NCMBUTTONDOWN:
						case WM_NCMBUTTONUP:
							changed = SetButton(MouseButtons.Middle, wParam, WM_NCMBUTTONDOWN, WM_NCMBUTTONUP);
							break;
						case WM_XBUTTONDOWN:
						case WM_XBUTTONUP:
							MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
							if(((hookStruct.mouseData >> 16) & 0xFFFF) == 1)
								changed = SetButton(MouseButtons.XButton1, wParam, WM_XBUTTONDOWN, WM_XBUTTONUP);
							else
								changed = SetButton(MouseButtons.XButton2, wParam, WM_XBUTTONDOWN, WM_XBUTTONUP);
							break;
						case WM_NCXBUTTONDOWN:
						case WM_NCXBUTTONUP:
							hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
							if(((hookStruct.mouseData >> 16) & 0xFFFF) == 1)
								changed = SetButton(MouseButtons.XButton1, wParam, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP);
							else
								changed = SetButton(MouseButtons.XButton2, wParam, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP);
							break;
						default:
							changed = false;
							break;
					}
					
					if(changed && mouseButtons != MouseButtons.None)
					{
						modifierKeys = Control.ModifierKeys;
						if(CheckShourcut())
							return (IntPtr)1;
					}
					
					if(SkipMouseButtonUp)
					{
						SkipMouseButtonUp = false;
						return (IntPtr)1;
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
			
			[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
			private static extern short GetAsyncKeyState(int keyCode);
			
			[DllImport("user32.dll")]
			private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,  int dwExtraInfo);
			
			[DllImport("user32.dll")]
			private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
			
			[Flags]
			public enum MouseEventFlags
			{
				LEFTDOWN   = 0x00000002,
				LEFTUP     = 0x00000004,
				MIDDLEDOWN = 0x00000020,
				MIDDLEUP   = 0x00000040,
				MOVE       = 0x00000001,
				ABSOLUTE   = 0x00008000,
				RIGHTDOWN  = 0x00000008,
				RIGHTUP    = 0x00000010,
				XUP 	   = 0x0100
			}			
			
			const uint XBUTTON1        = 0x0001;
			const uint XBUTTON2        = 0x0002;
			
			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			static extern bool SetForegroundWindow(IntPtr hWnd);		
			
			[DllImport("user32.dll")]
			static extern IntPtr GetForegroundWindow();			
			
			[DllImport("kernel32.dll")]
			static extern uint GetCurrentThreadId();

			[DllImport("user32.dll", SetLastError=true)]
			static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
			
			[DllImport("user32.dll")]
			static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
			
			static void ResetKey(Keys key, string keyName)
			{
				modifierKeys = Control.ModifierKeys;
				while((modifierKeys & key) > 0)
				{
					System.Windows.Forms.SendKeys.Send(keyName);
					System.Windows.Forms.Application.DoEvents();
					System.Threading.Thread.Sleep(10);
					modifierKeys = Control.ModifierKeys;
				}
			}
   
   			static bool ignoreHookProcessing = false;
   			
   			/// <summary>
   			/// Sending Ctrl+C with avoiding keybd_event or SendInput
   			/// </summary>
			public static void SendCtrlC()
			{
				IntPtr oldForegroundWnd = GetForegroundWindow();
				uint oldProcessId;
				uint oldThreadId = GetWindowThreadProcessId(oldForegroundWnd, out oldProcessId);
				uint currentThreadId = GetCurrentThreadId();
			
				try
				{
					ignoreHookProcessing = true;
					
					//attach to foreground thread
					AttachThreadInput(currentThreadId, oldThreadId, true);
					
					//we needed switch off shift\alt\ctrl
					ResetKey(Keys.Alt, "%");
					ResetKey(Keys.Control, "^");
					ResetKey(Keys.Shift, "+");

					//if left mouse pressed - this is drag-n-drop. we needed to disable ir					
					if((mouseButtons & MouseButtons.Left) > 0)
					{
						mouse_event((uint)MouseEventFlags.LEFTUP,0,0,0,UIntPtr.Zero);
					}	
					

					//send Ctrl+Ins
					System.Windows.Forms.SendKeys.SendWait("^{INS}");
				}
				finally
				{
					//detach
					AttachThreadInput(currentThreadId, oldThreadId, false);
					ignoreHookProcessing = false;
				}	
			}
			
			public static void SendCtrlCOld()
			{
				const int KEYEVENTF_EXTENDEDKEY = 0x1;
				const int KEYEVENTF_KEYUP       = 0x2;
				
				
				//disable alt and shift if needed 
				modifierKeys = Control.ModifierKeys;
				if((modifierKeys & Keys.Alt) > 0)
				{
					keybd_event((byte)Keys.Menu,0xb8,KEYEVENTF_KEYUP,0);
					keybd_event((byte)Keys.LMenu,0,KEYEVENTF_KEYUP,0);
					keybd_event((byte)Keys.RMenu,0,KEYEVENTF_KEYUP,0);
				}

				if((modifierKeys & Keys.Shift) > 0)
					keybd_event( (byte)Keys.ShiftKey, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0 );
					
				//if((modifierKeys & Keys.Control) > 0)
				//	keybd_event( (byte)Keys.ControlKey, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0 );					

				if((modifierKeys & Keys.Control) == 0)
					keybd_event( (byte)Keys.ControlKey, 0x45, KEYEVENTF_EXTENDEDKEY, 0 );
					
				keybd_event( (byte)Keys.C, 0x45, KEYEVENTF_EXTENDEDKEY, 0 );
				
				keybd_event( (byte)Keys.C, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0 );			
				keybd_event( (byte)Keys.ControlKey, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0 );			
				
			}
			
			public static void DisableMouseKeys()
			{
				//disable mouse keys
				if((mouseButtons & MouseButtons.Left) > 0)
					mouse_event((uint)MouseEventFlags.LEFTUP,0,0,0,UIntPtr.Zero);

				if((mouseButtons & MouseButtons.Middle) > 0)
					mouse_event((uint)MouseEventFlags.MIDDLEUP,0,0,0,UIntPtr.Zero);

				if((mouseButtons & MouseButtons.Right) > 0)
					mouse_event((uint)MouseEventFlags.RIGHTUP,0,0,0,UIntPtr.Zero);

				if((mouseButtons & MouseButtons.XButton1) > 0)
					mouse_event((uint)MouseEventFlags.XUP,0,0,XBUTTON1,UIntPtr.Zero);

				if((mouseButtons & MouseButtons.XButton2) > 0)
					mouse_event((uint)MouseEventFlags.XUP,0,0,XBUTTON2,UIntPtr.Zero);
			}
		}
	}
}
