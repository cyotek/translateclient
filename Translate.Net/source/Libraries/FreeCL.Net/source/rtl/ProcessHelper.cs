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

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Windows.Forms;


namespace FreeCL.RTL
{
	/// <summary>
	/// Description of ProcessHelper.
	/// </summary>
	public static class ProcessHelper
	{
		static Process currentProcess;
		public static Process CurrentProcess {
			get { return currentProcess; }
		}
		
		
		static ProcessHelper()
		{
			currentProcess = Process.GetCurrentProcess(); 
		}
		
		public static Process GetOtherInstance(Process process)
		{
			Process[] processes = null;
			try
			{
				processes = Process.GetProcessesByName(process.ProcessName);
			}
			catch(SystemException)
			{
				if(MonoHelper.IsMono) //in mono may raise exception "Process is out"
					return null;
				else 
					throw;
			}
			
			foreach(Process p in processes)
			{
				if(p.Id != process.Id && (MonoHelper.IsUnix || p.SessionId == process.SessionId))	
					return p;
			}
			return null;
		}
		
		public static bool IsOtherInstanceAlreadyStarted()
		{
			return GetOtherInstance(currentProcess) != null;
		}

		internal static class NativeMethods
		{
			[DllImport("user32.dll")]
			public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

			[DllImport("user32.dll")]
			public static extern bool IsWindowVisible(IntPtr hWnd);
			
			[DllImport("user32.dll")]
			public static extern bool SetForegroundWindow(IntPtr hWnd);
			
			[DllImport("user32.dll")]
			public static extern bool EnumWindows(EnumWindowsProcDelegate lpEnumFunc,
			Int32 lParam);
			
			[DllImport("user32.dll")]
			public static extern int GetWindowThreadProcessId(IntPtr hWnd,
			ref Int32 lpdwProcessId);
			
			[DllImport("user32.dll")]
			public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString,
			Int32 nMaxCount);
			
			[DllImport("user32.dll", SetLastError=true)]
			public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
			
			public const int SW_SHOW = 5;
			public const int SW_RESTORE = 9;
			public const int WS_MINIMIZE = 0x02000000;
			public const int GWL_STYLE  = -16;
		}
		
		public delegate bool EnumWindowsProcDelegate(IntPtr hWnd, Int32 lParam);
	
		static private bool EnumWindowsProc(IntPtr hWnd, Int32 lParam)
		{
			int wndProcessId = 0;
			NativeMethods.GetWindowThreadProcessId(hWnd, ref wndProcessId);
			
			if(wndProcessId != lParam)
				return true; //continue
		
			StringBuilder caption = new StringBuilder(1024);
			NativeMethods.GetWindowText(hWnd, caption, 1024);
		
			if ((caption.ToString().IndexOf(partOfWinTitleSaved, StringComparison.OrdinalIgnoreCase) != -1))
			{
				// If hidden - make visible
				if(!NativeMethods.IsWindowVisible(hWnd))
					NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_SHOW);
					
				//if minimized - restore	
				if((NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE) & NativeMethods.WS_MINIMIZE) > 0)
					NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_RESTORE);
				
				//NativeMethods.SetForegroundWindow(hWnd);
				WindowHelper.BringToForeground(hWnd);
			}
			return true; 
		}
		
		static string partOfWinTitleSaved;

		public static bool ActivateOtherInstance(string partOfWinTitle)
		{
			partOfWinTitleSaved = partOfWinTitle;
			Process p = GetOtherInstance(currentProcess);
			if(p == null)
				return false;
				
			if(!MonoHelper.IsUnix)
			{
            	NativeMethods.EnumWindows(new EnumWindowsProcDelegate(EnumWindowsProc), p.Id);
				return true;
			}
			else
			{
				//TODO: implement this for unix
				return false;
			}
			
		}
	}
}
