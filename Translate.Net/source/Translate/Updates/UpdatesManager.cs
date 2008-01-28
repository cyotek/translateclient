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
using System.Net; 
using System.IO; 
using System.Web; 
using System.IO.Compression;
using System.Text; 
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using FreeCL.RTL;
using System.Net.Cache;
using Microsoft.Win32;
using System.Security.Principal;
using System.Threading;

namespace Translate
{
	public enum UpdateState
	{
		None,
		CheckVersion,
		UpdateDownloading,
		UpdateDownloaded,
		Ending,
		Cancel,
		Error
	}
	/// <summary>
	/// Description of UpdatesManager.
	/// </summary>
	public static class UpdatesManager
	{
		static UpdatesManager()
		{
		}
		
		static System.Threading.Mutex mutex;
		static bool mutexLocked;
		public static void Init()
		{
			mutex = new System.Threading.Mutex(false, "SAUTRANSLATENET");
			
			//remove all update files
			string pathToAppData = FreeCL.Forms.Application.DataFolder + @"\Update";
			if(!Directory.Exists(pathToAppData))
				return;
			
			string[] files = Directory.GetFiles(pathToAppData);
			foreach(string file in files)
			{
				File.Delete(Path.Combine(pathToAppData, file));
			}
		}
		

		public static bool NeedCheck
		{
			get
			{
				return TranslateOptions.Instance.UpdateOptions.EnableAutomaticUpdates && DateTime.Now > TranslateOptions.Instance.UpdateOptions.NextCheck && state == UpdateState.None;
			}
		}
	
		public static void CheckUpdates()
		{
			if(state != UpdateState.None)
				return;
				
			if(!mutex.WaitOne(0, true))
				return;
			mutexLocked = true;	
			
			UpdaterForm form = new UpdaterForm();
			form.Show();
		}
		
		static UpdateState state;
		public static UpdateState State {
			get { return state; }
			set { state = value; }
		}

		public static void Stop()		
		{
			if(mutexLocked)
			{
				mutex.ReleaseMutex();
				mutexLocked = false;
			}
			
			if(state == UpdateState.None)
				return;
			else if(state == UpdateState.Error || state == UpdateState.Ending)
			{
				state = UpdateState.None;
			}
			else
			{
				state = UpdateState.Cancel;
			}
		}
		
		
		static int versionUrlToCheck;
		
		public static void DoProcess()
		{
			if(state != UpdateState.None)
				return;
				
			state = UpdateState.CheckVersion;
			
			WebClient client = new WebClient();
			NetworkSetting networkSetting =  TranslateOptions.Instance.NetworkOptions.GetNetworkSetting(null);
			client.Proxy = networkSetting.Proxy;
			client.UseDefaultCredentials = true;
			//client.CachePolicy = new  RequestCachePolicy( RequestCacheLevel.Reload); //for test
			client.DownloadProgressChanged += DownloadProgressChanged;
			client.DownloadStringCompleted += DownloadVersionsCompleted;
			client.DownloadStringAsync(new Uri(Constants.VersionsTxtUrls[versionUrlToCheck]));
		}
		
		static bool TryToRerunVersionsCheck(WebClient client)
		{
			versionUrlToCheck++;
			if(versionUrlToCheck < Constants.VersionsTxtUrls.Length)
			{
				client.DownloadStringAsync(new Uri(Constants.VersionsTxtUrls[versionUrlToCheck]));
				return true;
			}
			
			return false;
		
		}
		
		static string message;
		public static string Message {
			get { return message; }
		}
		
		static string fileName;
		public static string FileName {
			get { return fileName; }
			set { fileName = value; }
		}
		
		
		static void SetErrorState(string Error)
		{
			message = Error;
			state = UpdateState.Error;
			versionUrlToCheck = 0;
			TranslateOptions.Instance.UpdateOptions.NextCheck = new DateTime(DateTime.Now.Ticks + DateTimeUtils.Hours(2).Ticks); //next 2 hours
		}

		static void SetEndingState(string Message)
		{
			message = Message;
			state = UpdateState.Ending;
			versionUrlToCheck = 0;
			TranslateOptions.Instance.UpdateOptions.NextCheck = new DateTime(DateTime.Now.Ticks + DateTimeUtils.Days(1).Ticks); //next day
		}

		static void Cancelled()
		{
			message = LangPack.TranslateString("Canceled");
			state = UpdateState.None;
			versionUrlToCheck = 0;
			TranslateOptions.Instance.UpdateOptions.NextCheck = new DateTime(DateTime.Now.Ticks + DateTimeUtils.Hours(2).Ticks); //next 2 hours
		}
		
		static void DownloadVersionsCompleted (Object sender, DownloadStringCompletedEventArgs e)
		{
			if(state == UpdateState.Cancel)
			{
				Cancelled();
				return;
			}
			
			if(e.Cancelled || e.Error != null)
			{
				string url = Constants.VersionsTxtUrls[versionUrlToCheck];
				if(TryToRerunVersionsCheck(sender as WebClient))
					return;
					
				if(e.Error != null)
					SetErrorState(string.Format(LangPack.TranslateString("Error on downloading {0}"), url) + "\r\n" + e.Error.Message);
				else 
					SetErrorState(LangPack.TranslateString("Canceled"));
				return;
			}
			
			
			//versions.txt received, next step
			ParseVersions(sender as WebClient, e.Result);
		}
		
		static void ParseVersions(WebClient client, string versions)
		{
			StringReader strReader = new StringReader(versions);
			string versionOnSite = strReader.ReadLine();
			if(!versionOnSite.StartsWith("Version="))
			{
				SetErrorState(string.Format(LangPack.TranslateString("{0} don't contained {1} tag:\r\n{2}"), "versions.txt", "Version", versions));
				return;
			}
			versionOnSite = versionOnSite.Substring("Version=".Length);
			System.Diagnostics.FileVersionInfo vi = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);
			TranslateOptions.Instance.UpdateOptions.PreviousVersion = vi.ProductVersion;
			if(versionOnSite == vi.ProductVersion)
			{
				SetEndingState(LangPack.TranslateString("Program is up to date"));
				return;
			}
				
			string[] versionOnSiteArray = versionOnSite.Split('.');
			string[] versionCurrentArray = vi.ProductVersion.Split('.');
			
			if(versionOnSiteArray.Length != versionCurrentArray.Length)
			{
				SetErrorState(string.Format(LangPack.TranslateString("{0} has wrong formatted {1} tag:\r\n{2}"), "versions.txt", "Version", versions));
				return;
			}
			
			bool needUpdate = true;
			
			for(int i = 0; i < versionOnSiteArray.Length; i++)
			{
				int onSitePart;
				if(!int.TryParse(versionOnSiteArray[i], out onSitePart))
				{
					SetErrorState(string.Format(LangPack.TranslateString("{0} has wrong formatted {1} tag:\r\n{2}"), "versions.txt", "Version", versions));
					return;
				}
				
				int onCurrentPart;
				if(!int.TryParse(versionCurrentArray[i], out onCurrentPart))
				{
					SetErrorState(string.Format(LangPack.TranslateString("{0} has wrong formatted {1} tag:\r\n{2}"), "versions.txt", "Version", versions));
					return;
				}
				
				if(onSitePart < onCurrentPart)
				{
					needUpdate = false;
					break;
				}
				else if(onSitePart > onCurrentPart)
				{
					break;
				}
			}
			
			if(!needUpdate)
			{
				SetEndingState(LangPack.TranslateString("Program is up to date"));
				return;
			}
			
			
			string urls = strReader.ReadLine();
			if(!urls.StartsWith("UrlList=\""))
			{
				SetErrorState(string.Format(LangPack.TranslateString("{0} don't contained {1} tag:\r\n{2}"), "versions.txt", "UrlList", versions));
				return;
			}
				
			urls = urls.Substring("UrlList=\"".Length);
			List<string> urlsList = new List<string>();
			while(urls.IndexOf("\",\"") > 0) 
			{
				string url = urls.Substring(0, urls.IndexOf("\",\""));
				urlsList.Add(url);
				urls = urls.Substring(urls.IndexOf("\",\"") + 4);
			}
			
			if(urls.IndexOf("\"") > 0)
			{
				string url = urls.Substring(0, urls.IndexOf("\""));
				urlsList.Add(url);
			}
			
			if(urlsList.Count == 0)
			{
				SetErrorState(string.Format(LangPack.TranslateString("{0} has wrong formatted {1} tag:\r\n{2}"), "versions.txt", "UrlList", versions));
				return;
			}
			
			if(urlsList.Count == 1)
				urlToDownload = urlsList[0];
			else
			{
				Random randObj = new Random( DateTime.Now.Millisecond);
				urlToDownload = urlsList[randObj.Next(urlsList.Count)];
			}
			
			if(state == UpdateState.Cancel)
			{
				Cancelled();
				return;
			}
			
			fileName = Path.GetFileName(urlToDownload);
			state = UpdateState.UpdateDownloading;
			
			Uri uri = new Uri(urlToDownload);
			client.DownloadDataCompleted += DownloadDataCompleted;
			client.DownloadDataAsync(uri);
			startDownload = DateTime.Now;
		}
		
		static string urlToDownload;
		static DateTime startDownload;
		static long bytesReceived;
		public static long KbReceived {
			get { return bytesReceived/1024; }
		}
		
		static long totalBytesToReceive;
		public static long TotalKbToReceive {
			get { return totalBytesToReceive/1024; }
		}
		
		static long progressPercentage;
		public static long ProgressPercentage {
			get { return progressPercentage; }
		}
		
		public static int DownloadSpeedKbs
		{
			get
			{
				try
				{
					long duration = DateTime.Now.Ticks - startDownload.Ticks;
					double speed = bytesReceived / ((double)duration/10000000) / 1024;
					return (int)speed;
				}
				catch
				{
					return 0;
				}
			}
		}
		
		
		static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			if(state == UpdateState.Cancel)
			{
				(sender as WebClient).CancelAsync();
				return;
			}
		
			bytesReceived = e.BytesReceived;
			totalBytesToReceive = e.TotalBytesToReceive;
			progressPercentage = e.ProgressPercentage;
		}
		
		static string updateFileName;
		static void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			if(state == UpdateState.Cancel)
			{
				Cancelled();
				return;
			}
		
			if(e.Cancelled || e.Error != null)
			{
				if(e.Error != null)
					SetErrorState(string.Format(LangPack.TranslateString("Error on downloading {0}"), urlToDownload) + "\r\n" + e.Error.Message);
				else 
					SetErrorState(LangPack.TranslateString("Canceled"));
				return;
			}
			
			string pathToAppData = FreeCL.Forms.Application.DataFolder + @"\Update";
			if(!Directory.Exists(pathToAppData))
				Directory.CreateDirectory(pathToAppData);
			updateFileName = pathToAppData + @"\" + fileName;
			if(File.Exists(updateFileName))
			{
				try
				{
					File.Delete(updateFileName);
				}
				catch
				{
					SetErrorState(string.Format(LangPack.TranslateString("File {0} already exists"), updateFileName));				
					return;
				}
			}
				
			FileStream fs = new FileStream(updateFileName, FileMode.Create, FileAccess.Write, FileShare.None);
			fs.Write(e.Result, 0, e.Result.Length);
			fs.Dispose();
			MarkFileToDeleteOnReboot(updateFileName);
			state = UpdateState.UpdateDownloaded;
		}
		
		[System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
		private static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, int dwFlags);
		const int MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004;
		
		static void MarkFileToDeleteOnReboot(string fileName)
		{
			MoveFileEx(fileName, null, MOVEFILE_DELAY_UNTIL_REBOOT);
		}	
		
		
		static bool canRunUpdate = InitCanRunUpdate();
		public static bool CanRunUpdate {
			get { return canRunUpdate; }
		}
		
		static bool InitCanRunUpdate()
		{
			AppDomain domain = System.Threading.Thread.GetDomain();
			domain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
			WindowsPrincipal principal = (WindowsPrincipal)System.Threading.Thread.CurrentPrincipal;
			bool result = principal.IsInRole(WindowsBuiltInRole.Administrator);		
			
			if(!result)
			{
				result = principal.IsInRole(WindowsBuiltInRole.PowerUser);
			}
			
			if(!result && Environment.OSVersion.Version.Major >= 6) //vista
			{
				const string keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
				const string valueName = "EnableLUA";

				try
				{
					using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, false))
					{
						if (key != null)
						{
							RegistryValueKind valueKind = key.GetValueKind(valueName);
						
							if (valueKind == RegistryValueKind.DWord)
							{
								int value = unchecked((int)key.GetValue(valueName));
								result = (value == 1);
							}
						}
					}
				}
				catch
				{
				}
			}
			
			return result;
		}
		
		public static void RunUpdate(IntPtr parentHandle)
		{
			Process myProcess = new Process();
			
			// Get the path that stores user documents.
			myProcess.StartInfo.FileName = updateFileName; 
			myProcess.StartInfo.Arguments = "/SILENT";
			myProcess.StartInfo.ErrorDialog = true;
			//myProcess.StartInfo.ErrorDialogParentHandle = parentHandle;
			myProcess.Start();
			System.Windows.Forms.Application.Exit(); 
		}
		
		public static void SaveUpdate(string newPath)
		{
			File.Move(updateFileName, newPath);
			SetEndingState(string.Format(LangPack.TranslateString("Installer saved to \r\n{0}"), newPath));
		}
		
		public static void DeleteUpdate()
		{
			File.Delete(updateFileName);
			SetEndingState(LangPack.TranslateString("Update will be run later"));
		}
		
	}
}
