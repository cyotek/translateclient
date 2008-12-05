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
using System.Windows.Forms;

namespace Translate
{
	/// <summary>
	/// Description of KeyboardLayoutOptions.
	/// </summary>
	[Serializable()]
	public class KeyboardLayoutOptions
	{
		public KeyboardLayoutOptions()
		{
		}
		
		KeyboardLayoutLanguagesList customLayouts = new KeyboardLayoutLanguagesList();
		public KeyboardLayoutLanguagesList CustomLayouts
		{
			get { return customLayouts; }
			set { customLayouts = value; }
		}
		
		bool switchLayoutsBasedOnLanguage = true;
		[DefaultValue(true)]
		public bool SwitchLayoutsBasedOnLanguage
		{
			get { return switchLayoutsBasedOnLanguage; }
			set { switchLayoutsBasedOnLanguage = value; }
		}
		
		public KeyboardLayoutLanguagesList GetAutomaticLayouts()
		{
			KeyboardLayoutLanguagesList result = new KeyboardLayoutLanguagesList();
			
			for(int i = 1; i < (int)Language.Last; i++)
			{
				Language lng = (Language)i;
				foreach(InputLanguage il in InputLanguage.InstalledInputLanguages)
				{
					if(InputLanguageManager.IsLanguageSupported(il, lng))
						result.Add(il.LayoutName, lng);
				}
			}
			
			return result;
		}
	}
	
	[Serializable()]
	public class KeyboardLayoutLanguage: ICloneable
	{
		public KeyboardLayoutLanguage()
		{
		}
		
		public KeyboardLayoutLanguage(string layoutName, Language language)
		{
			this.layoutName = layoutName;
			this.language = language;
		}
		
		public virtual object Clone()
		{
			KeyboardLayoutLanguage result = new KeyboardLayoutLanguage(layoutName, language);
			return result;
		}		
	
		string layoutName;
		public string LayoutName
		{
			get { return layoutName; }
			set { layoutName = value; }
		}
		
		
		Language language;
		public Language Language
		{
			get { return language; }
			set { language = value; }
		}
	}
	

	public class KeyboardLayoutLanguagesList : List<KeyboardLayoutLanguage>
	{
		public KeyboardLayoutLanguagesList()
		{
		
		}
		
		public void Add(string layoutName, Language language)
		{
			base.Add(new KeyboardLayoutLanguage(layoutName, language));
		}
		
	}
}
