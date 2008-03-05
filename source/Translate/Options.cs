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


namespace Translate
{
	/// <summary>
	/// Description of Options.
	/// </summary>
	[Serializable()]
	public class TranslateOptions : FreeCL.Forms.BaseOptions
	{
		public TranslateOptions()
		{
			//UseSoapSerialization = true;
			defaultProfile.Subjects.Add("Common");
		}
		
		protected TranslateOptions(SerializationInfo info, StreamingContext context):base(info, context)
		{
			//UseSoapSerialization = true;
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
		public bool MinimizeToTray {
			get { return minimizeToTray; }
			set { minimizeToTray = value; }
		}
		
		bool minimizeToTrayOnStartup = true;
		public bool MinimizeToTrayOnStartup {
			get { return minimizeToTrayOnStartup; }
			set { minimizeToTrayOnStartup = value; }
		}
		
		
		ResultWindowOptions resultWindowOptions = new ResultWindowOptions();
		public ResultWindowOptions ResultWindowOptions {
			get { return resultWindowOptions; }
			set { resultWindowOptions = value; }
		}
		
		
		DefaultTranslateProfile defaultProfile = new DefaultTranslateProfile();
		
		
		public DefaultTranslateProfile DefaultProfile {
			get { return defaultProfile; }
			set { defaultProfile = value; }
		}
		
		
		[ XmlIgnore()]
		public TranslateProfile CurrentProfile
		{
			get { return defaultProfile; }
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
		
		
		public override void OnLoaded()
		{
			base.OnLoaded();
			networkOptions.Apply();
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
		
		
	}
}
