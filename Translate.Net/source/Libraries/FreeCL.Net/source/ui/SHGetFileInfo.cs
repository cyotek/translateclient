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
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
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
using System.Runtime.InteropServices;
using FreeCL.RTL;


namespace FreeCL.UI
{
	
	
	/// <summary>
	/// Description of SHGetFileInfo.	
	/// </summary>
	public static class ShellFileInfo
	{
		private const uint SHGFI_ICON = 0x100;
		private const uint SHGFI_LARGEICON = 0x0; // 'Large icon
		private const uint SHGFI_SMALLICON = 0x1; // 'Small icon
		private const uint SHGFI_DISPLAYNAME	= 0x200;		 // get display name		
		
		static class NativeMethods
		{
			[DllImport("shell32.dll", SetLastError=true)]
			public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
			
			[DllImport("user32.dll")]
			public static extern int DestroyIcon(IntPtr Icon);
		}

		[StructLayout(LayoutKind.Sequential)]
		struct SHFILEINFO 
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};
		
		public static System.Drawing.Icon SmallIcon(string fileName)
		{
			if(MonoHelper.IsUnix)
				return System.Drawing.Icon.ExtractAssociatedIcon(fileName);
				
			SHFILEINFO shinfo = new SHFILEINFO();
			if(NativeMethods.SHGetFileInfo(fileName, 0, ref shinfo,(uint)Marshal.SizeOf(shinfo),SHGFI_ICON | SHGFI_SMALLICON) == IntPtr.Zero)
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error()); 
			System.Drawing.Icon tmp = System.Drawing.Icon.FromHandle(shinfo.hIcon);
			
			try
			{			
				System.Drawing.Icon res = (System.Drawing.Icon)tmp.Clone();
				tmp.Dispose();
				NativeMethods.DestroyIcon(shinfo.hIcon);
				return res;
			}
			catch
			{}
			return null;
		}

		public static System.Drawing.Icon LargeIcon(string fileName)
		{
			if(MonoHelper.IsUnix)
				return System.Drawing.Icon.ExtractAssociatedIcon(fileName);
			
			SHFILEINFO shinfo = new SHFILEINFO();
			if(NativeMethods.SHGetFileInfo(fileName, 0, ref shinfo,(uint)Marshal.SizeOf(shinfo),SHGFI_ICON |SHGFI_LARGEICON) == IntPtr.Zero)
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error()); 
			System.Drawing.Icon tmp = System.Drawing.Icon.FromHandle(shinfo.hIcon);
			try
			{			
				System.Drawing.Icon res = (System.Drawing.Icon)tmp.Clone();
				tmp.Dispose();
				NativeMethods.DestroyIcon(shinfo.hIcon);
				return res;
			}
			catch
			{}
			return null;
		}
		
		public static string DisplayName(string fileName)
		{
			if(MonoHelper.IsUnix)
				return null;
			
			SHFILEINFO shinfo = new SHFILEINFO();
			NativeMethods.SHGetFileInfo(fileName, 0, ref shinfo,(uint)Marshal.SizeOf(shinfo),SHGFI_DISPLAYNAME);
			return shinfo.szDisplayName;
		}
		
		
	}
}
