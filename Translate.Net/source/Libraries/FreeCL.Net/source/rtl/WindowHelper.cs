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
	/// Description of WindowHelper.
	/// </summary>
	public static class WindowHelper
	{
		static WindowHelper()
		{
		}
		
		static class NativeMethods
		{
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
   
			public static void SetForeground(IntPtr handle)
			{
				IntPtr oldForegroundWnd = GetForegroundWindow();
				uint oldProcessId;
				uint oldThreadId = GetWindowThreadProcessId(oldForegroundWnd, out oldProcessId);
				uint currentThreadId = GetCurrentThreadId();
				
				AttachThreadInput(currentThreadId, oldThreadId, true);
				SetForegroundWindow(handle);
				AttachThreadInput(currentThreadId, oldThreadId, false);
			}
		}
		
		public static void BringToForeground(IntPtr hWnd)
		{
			if(!MonoHelper.IsUnix)
				NativeMethods.SetForeground(hWnd);
			//TODO: implement this for unix
		}

		public static void BringToForeground(Form form)
		{
			if(!MonoHelper.IsUnix)
				NativeMethods.SetForeground(form.Handle);
			//TODO: implement this for unix
		}
		
	}
}
