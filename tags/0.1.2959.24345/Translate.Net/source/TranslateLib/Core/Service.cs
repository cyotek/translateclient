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
using System.Collections.Generic;
using System.Runtime.Serialization; 
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Resources;
using System.Reflection;

namespace Translate
{
	/// <summary>
	/// Description of BaseTranslator.
	/// </summary>
	public abstract class Service
	{
		Uri url;
		public Uri Url {
			get { return url; }
			set { url = value; }
		}
		
		string name;
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		string companyName;
		public string CompanyName {
			get { return companyName; }
			set { companyName = value; }
		}
		
		string copyright;
		public string Copyright {
			get { return copyright; }
			set { copyright = value; }
		}
		
		
		Uri iconUrl;
		public Uri IconUrl {
			get { return iconUrl; }
			set { iconUrl = value; }
		}
		
		string iconResourceName;
		public string IconResourceName {
			get { 
					if(!string.IsNullOrEmpty(iconResourceName))
						return iconResourceName; 

					iconResourceName = this.GetType().FullName;
					if(!iconResourceName.EndsWith("Service"))
					{
						iconResourceName = null;
						return null;
					}
						
					iconResourceName = iconResourceName.Substring(0, iconResourceName.IndexOf("Service"));
					iconResourceName += ".Service.ico";
					return iconResourceName;
				}
			set { iconResourceName = value; }
		}
		
		
		System.Drawing.Icon icon;
		public System.Drawing.Icon Icon {
			get 
			{ 
				if(icon != null)
					return icon; 
					
				
				try
				{
					Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(IconResourceName);
					icon = new System.Drawing.Icon(resourceStream);
					resourceStream.Close();
					resourceStream.Dispose();
				}
				catch
				{
					icon = null;
				}
				return icon;
			}
		}
		
		
		TranslatorsCollection translators = new TranslatorsCollection();
		public ReadOnlyTranslatorsCollection Translators {
			get { return new ReadOnlyTranslatorsCollection(translators); }
		}
		
		protected void AddTranslator(Translator translator)
		{
			if(translator == null)
				throw new ArgumentNullException("translator");
			
			translator.Service = this;
			translators.Add(translator);
		}
		
		BilingualDictionariesCollection bilingualDictionaries = new BilingualDictionariesCollection();
		public ReadOnlyBilingualDictionariesCollection BilingualDictionaries {
			get { return new ReadOnlyBilingualDictionariesCollection(bilingualDictionaries); }
		}
		
		protected void AddBilingualDictionary(BilingualDictionary bilingualDictionary)
		{
			if(bilingualDictionary == null)
				throw new ArgumentNullException("bilingualDictionary");
			
			bilingualDictionary.Service = this;
			bilingualDictionaries.Add(bilingualDictionary);
		}

		MonolingualDictionariesCollection monolingualDictionaries = new MonolingualDictionariesCollection();
		public ReadOnlyMonolingualDictionariesCollection MonolingualDictionaries {
			get { return new ReadOnlyMonolingualDictionariesCollection(monolingualDictionaries); }
		}
		
		protected void AddMonolingualDictionary(MonolingualDictionary monolingualDictionary)
		{
			if(monolingualDictionary == null)
				throw new ArgumentNullException("monolingualDictionary");
			
			monolingualDictionary.Service = this;
			monolingualDictionaries.Add(monolingualDictionary);
		}
		
	}
	
	public class ServicesCollection : List<Service>
	{
		public ServicesCollection()
		{
		}
	}
	

	
	public class ReadOnlyServicesCollection: ReadOnlyCollection<Service>
	{
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]		
		public ReadOnlyServicesCollection(List<Service> list):base(list)
		{
		
		}
	}
	
	
	
}
