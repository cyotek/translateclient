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
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Translate
{
	/// <summary>
	/// Description of ProfilesHistory.
	/// </summary>
	[Serializable()]
	public class ProfilesHistoryData: ICloneable
	{
		public ProfilesHistoryData()
		{
		}
		
		public ProfilesHistoryData(string name, Language language, Language detectedLanguage)
		{
			this.name = name;
			this.language = language;
			this.detectedLanguage = detectedLanguage;
		}
		
		public virtual object Clone()
		{
			ProfilesHistoryData result = new ProfilesHistoryData(name, language, detectedLanguage);
			return result;
		}
		
		string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		Language language;
		public Language Language
		{
			get { return language; }
			set { language = value; }
		}
		
		Language detectedLanguage = Language.Unknown;
		[DefaultValue(Language.Unknown)]
		public Language DetectedLanguage
		{
			get { return detectedLanguage; }
			set { detectedLanguage = value; }
		}
		
	}
	
	public class ProfilesHistory: List<ProfilesHistoryData>
	{
		public void AddProfile(string profileName, Language language, Language detectedLanguage)
		{
			ProfilesHistoryData data = null;
			int i = 0;
			while(i < base.Count)
			{
				if(base[i].Name == profileName && 
					base[i].Language == language && 
					data == null && 
					base[i].DetectedLanguage == detectedLanguage)
				{
					data = base[i];
					base.RemoveAt(i);
				}	
				else if(base[i].Language == language && base[i].DetectedLanguage == detectedLanguage)
					base.RemoveAt(i);
				else	
					i++;
			}
			
			if(data == null)
				data = new ProfilesHistoryData(profileName, language, detectedLanguage);
			
			base.Insert(0, data);
		}
		
		public void DeleteProfile(string profileName)
		{
			int i = 0;
			while(i < base.Count)
			{
				if(base[i].Name == profileName)
					base.RemoveAt(i);
				else
					i++;
			}
		}
		
		public void RenameProfile(string oldProfileName, string newProfileName)
		{
			foreach(ProfilesHistoryData phd in this)
			{
				if(phd.Name == oldProfileName)
					phd.Name = newProfileName;
			}
		}
		
	}

}
