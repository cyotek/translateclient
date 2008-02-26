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
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	/// <summary>
	/// Description of Manager.
	/// </summary>
	public static class Manager
	{
	
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static Manager()
		{
			LoadServices(typeof(Language).Assembly);
		}
		
		
		static ServicesCollection services = new ServicesCollection();
		public static ReadOnlyServicesCollection Services {
			get { return new ReadOnlyServicesCollection(services); }
		}
		
		
		static ServiceItemsCollection serviceItems = new ServiceItemsCollection();
		public static ReadOnlyServiceItemsCollection ServiceItems {
			get { return new ReadOnlyServiceItemsCollection(serviceItems); }
		}

		static LanguagePairServiceItemsDictionary langpair_serviceItems = new LanguagePairServiceItemsDictionary();
		public static LanguagePairServiceItemsDictionary LanguagePairServiceItems {
			get { return langpair_serviceItems; }
		}
		
		static TranslatorsCollection translators = new TranslatorsCollection();
		public static ReadOnlyTranslatorsCollection Translators {
			get { return new ReadOnlyTranslatorsCollection(translators); }
		}

		static LanguagePairTranslatorsDictionary langpair_translators = new LanguagePairTranslatorsDictionary();
		public static LanguagePairTranslatorsDictionary LanguagePairTranslators {
			get { return langpair_translators; }
		}
		
		static BilingualDictionariesCollection bilingualDictionaries = new BilingualDictionariesCollection();
		public static BilingualDictionariesCollection BilingualDictionaries {
			get { return bilingualDictionaries; }
		}
		
		static LanguagePairBilingualDictionariesDictionary langpair_bilingualDictionaries = new LanguagePairBilingualDictionariesDictionary();	
		public static LanguagePairBilingualDictionariesDictionary LanguagePairBilingualDictionaries {
			get { return langpair_bilingualDictionaries; }
		}
		
		
		static MonolingualDictionariesCollection monolingualDictionaries = new MonolingualDictionariesCollection();
		public static MonolingualDictionariesCollection MonolingualDictionaries {
			get { return monolingualDictionaries; }
		}
		
		static LanguagePairMonolingualDictionariesDictionary langpair_monolingualDictionaries = new LanguagePairMonolingualDictionariesDictionary();	
		public static LanguagePairMonolingualDictionariesDictionary LanguagePairMonolingualDictionaries {
			get { return langpair_monolingualDictionaries; }
		}
		
		static void Add(Service service)
		{
			services.Add(service);
			foreach(Translator translator in service.Translators)
			{
				serviceItems.Add(translator);
				translators.Add(translator);
				foreach(LanguagePair langPair in translator.SupportedTranslations)
				{
					TranslatorsCollection translators_list;
					if(!langpair_translators.TryGetValue(langPair, out translators_list))
					{
						translators_list = new TranslatorsCollection();
						langpair_translators.Add(langPair, translators_list);
					}
					translators_list.Add(translator);					
					
					ServiceItemsCollection serviceItems_list;
					if(!langpair_serviceItems.TryGetValue(langPair, out serviceItems_list))
					{
						serviceItems_list = new ServiceItemsCollection();
						langpair_serviceItems.Add(langPair, serviceItems_list);
					}
					serviceItems_list.Add(translator);
				}
			}
			
			foreach(BilingualDictionary dictionary in service.BilingualDictionaries)
			{
				serviceItems.Add(dictionary);
				bilingualDictionaries.Add(dictionary);
				foreach(LanguagePair langPair in dictionary.SupportedTranslations)
				{
					BilingualDictionariesCollection dictionaries_list;
					if(!langpair_bilingualDictionaries.TryGetValue(langPair, out dictionaries_list))
					{
						dictionaries_list = new BilingualDictionariesCollection();
						langpair_bilingualDictionaries.Add(langPair, dictionaries_list);
					}
					
					dictionaries_list.Add(dictionary);
					
					ServiceItemsCollection serviceItems_list;
					if(!langpair_serviceItems.TryGetValue(langPair, out serviceItems_list))
					{
						serviceItems_list = new ServiceItemsCollection();
						langpair_serviceItems.Add(langPair, serviceItems_list);
					}
					serviceItems_list.Add(dictionary);
					
				}
			}

			foreach(MonolingualDictionary dictionary in service.MonolingualDictionaries)
			{
				serviceItems.Add(dictionary);
				monolingualDictionaries.Add(dictionary);
				foreach(LanguagePair langPair in dictionary.SupportedTranslations)
				{
					MonolingualDictionariesCollection dictionaries_list;
					if(!langpair_monolingualDictionaries.TryGetValue(langPair, out dictionaries_list))
					{
						dictionaries_list = new MonolingualDictionariesCollection();
						langpair_monolingualDictionaries.Add(langPair, dictionaries_list);
					}
					
					dictionaries_list.Add(dictionary);
					
					ServiceItemsCollection serviceItems_list;
					if(!langpair_serviceItems.TryGetValue(langPair, out serviceItems_list))
					{
						serviceItems_list = new ServiceItemsCollection();
						langpair_serviceItems.Add(langPair, serviceItems_list);
					}
					serviceItems_list.Add(dictionary);
					
				}
			}
			
		}
		
		static void LoadServices(Assembly assembly)
		{
			
			Type[] Types = assembly.GetTypes();

			foreach (Type oType in Types)
			{
				if(oType.IsSubclassOf(typeof(Translate.Service)) )
				{
					Service service = (Service)Activator.CreateInstance(oType);
					Add(service);
				}
			}
		}
		
	}
}
