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
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Drawing;
using Application = FreeCL.Forms.Application;
using System.Security;
using System.Security.Permissions;
using System.ComponentModel;


namespace Translate
{
	/// <summary>
	/// Description of Options.
	/// </summary>
	[Serializable()]
	[XmlSerializerAssemblyAttribute(AssemblyName="Translate.Net.XmlSerializers")]
	public class TranslateOptions : FreeCL.Forms.BaseOptions
	{
		public TranslateOptions()
		{
			//UseSoapSerialization = true;
			defaultProfile = new DefaultTranslateProfile();
			currentProfile = defaultProfile;
			defaultProfile.Subjects.Add("Common");
		}
		
		protected TranslateOptions(SerializationInfo info, StreamingContext context):base(info, context)
		{
			currentProfile = defaultProfile;
		}	
		
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]		
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]		
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		
		
		Size mainFormSize;
		public Size MainFormSize {
			get { return mainFormSize; }
			set { mainFormSize = value; }
		}
		
		Point mainFormLocation;
		public Point MainFormLocation {
			get { return mainFormLocation; }
			set { mainFormLocation = value; }
		}
		
		bool mainFormMaximized;
		public bool MainFormMaximized {
			get { return mainFormMaximized; }
			set { mainFormMaximized = value; }
		}
		
		int languageSelectorWidth;
		public int LanguageSelectorWidth {
			get { return languageSelectorWidth; }
			set { languageSelectorWidth = value; }
		}
		
		int historyHeight;
		public int HistoryHeight {
			get { return historyHeight; }
			set { historyHeight = value; }
		}
		
		int sourceHeight;
		public int SourceHeight {
			get { return sourceHeight; }
			set { sourceHeight = value; }
		}
		
		bool minimizeToTray = true;
		[DefaultValue(true)]
		public bool MinimizeToTray {
			get { return minimizeToTray; }
			set { minimizeToTray = value; }
		}
		
		bool minimizeToTrayOnStartup = true;
		[DefaultValue(true)]
		public bool MinimizeToTrayOnStartup {
			get { return minimizeToTrayOnStartup; }
			set { minimizeToTrayOnStartup = value; }
		}
		
		bool singleInstance = true;
		[DefaultValue(true)]
		public bool SingleInstance {
			get { return singleInstance; }
			set { singleInstance = value; }
		}
		
		ResultWindowOptions resultWindowOptions = new ResultWindowOptions();
		public ResultWindowOptions ResultWindowOptions {
			get { return resultWindowOptions; }
			set { resultWindowOptions = value; }
		}
		
		
		DefaultTranslateProfile defaultProfile;
		
		
		public DefaultTranslateProfile DefaultProfile {
			get { return defaultProfile; }
			set { defaultProfile = value; }
		}
		
		
		TranslateProfile currentProfile;
		[ XmlIgnore()]
		public TranslateProfile CurrentProfile
		{
			get { return currentProfile; }
			set {currentProfile = value;}
		}
		
		string currentProfileName = "Default";
		[DefaultValue("Default")]
		public string CurrentProfileName {
			get { return currentProfileName; }
			set { currentProfileName = value; }
		}
		
		[ XmlIgnore()]
		TranslateProfilesCollection profiles = new TranslateProfilesCollection();
		
		[ XmlIgnore()]
		public TranslateProfilesCollection Profiles {
			get { return profiles; }
			set { profiles = value; }
		}
		
		UserTranslateProfilesCollection userProfiles = new UserTranslateProfilesCollection();
		public UserTranslateProfilesCollection UserProfiles {
			get { return userProfiles; }
			set { userProfiles = value; }
		}
		
		public NetworkSetting GetNetworkSetting(Service service)
		{
			return networkOptions.GetNetworkSetting(service);
		}
		
		NetworkOptions networkOptions = new NetworkOptions();
		public NetworkOptions NetworkOptions {
			get { return networkOptions; }
			set { networkOptions = value; }
		}
		
		UpdateOptions updateOptions = new UpdateOptions();
		public UpdateOptions UpdateOptions {
			get { return updateOptions; }
			set { updateOptions = value; }
		}
		
		FontsOptions fontsOptions = new FontsOptions();
		public FontsOptions FontsOptions {
			get { return fontsOptions; }
			set { fontsOptions = value; }
		}
		
		
		public override void OnLoaded()
		{
			base.OnLoaded();
			networkOptions.Apply();
			fontsOptions.Apply();
			profiles.Add(defaultProfile);
			foreach(UserTranslateProfile pf in userProfiles)
			{
				pf.AfterLoad();
				if(pf.Position < profiles.Count)
					profiles.Insert(pf.Position, pf);	
				else
					profiles.Add(pf);
			}
			
			foreach(TranslateProfile pf in profiles)
			{
				if(currentProfileName == pf.Name)
				{
					currentProfile = pf;
					break;
				}
			}
		}
		
		public override void OnSave()
		{
			base.OnSave();
			defaultProfile.BeforeSave();
			userProfiles.Clear();
			for(int i = 0; i < profiles.Count; i++)
			{
				profiles[i].Position = i;
				UserTranslateProfile pf = profiles[i] as UserTranslateProfile;
				if(pf != null)
				{
					userProfiles.Add(pf);		
				}
			}
			currentProfileName = currentProfile.Name; 
		}
		
		
		public static TranslateOptions Instance
		{
			get
			{
				return (TranslateOptions)Application.BaseOptions;
			}
		}
		
		
		HookOptions hookOptions = new HookOptions();
		public HookOptions HookOptions {
			get { return hookOptions; }
			set { hookOptions = value; }
		}
		
		StatOptions statOptions = new StatOptions();
		public StatOptions StatOptions {
			get { return statOptions; }
			set { statOptions = value; }
		}
		
		GuessingOptions guessingOptions = new GuessingOptions();
		public GuessingOptions GuessingOptions
		{
			get { return guessingOptions; }
			set { guessingOptions = value; }
		}
		
		ProfilesHistory profilesHistory = new ProfilesHistory();
		public ProfilesHistory ProfilesHistory
		{
			get { return profilesHistory; }
			set { profilesHistory = value; }
		}
		
		bool breakTranslationOnDeactivation = false;
		[DefaultValue(false)]
		public bool BreakTranslationOnDeactivation
		{
			get { return breakTranslationOnDeactivation; }
			set { breakTranslationOnDeactivation = value; }
		}
		
		KeyboardLayoutOptions keyboardLayoutsOptions = new KeyboardLayoutOptions();
		public KeyboardLayoutOptions KeyboardLayoutsOptions
		{
			get { return keyboardLayoutsOptions; }
			set { keyboardLayoutsOptions = value; }
		}
		
		bool useEQATECMonitor = true;
		[DefaultValue(true)]
		public bool UseEQATECMonitor
		{
			get { return useEQATECMonitor; }
			set { useEQATECMonitor = value; }
		}

		bool askedEQATECMonitor	= false;
		[DefaultValue(false)]
		public bool AskedEQATECMonitor
		{
			get { return askedEQATECMonitor; }
			set { askedEQATECMonitor = value; }
		}
		
		
	}
}
