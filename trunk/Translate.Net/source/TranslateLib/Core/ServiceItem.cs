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
using System.Collections.ObjectModel;
using System.Net; 
using System.Runtime.Serialization; 
using System.Diagnostics.CodeAnalysis;


namespace Translate
{
	/// <summary>
	/// Description of ServiceItem.
	/// </summary>
	public abstract class ServiceItem
	{
		int chars_limit = -1;
		public int CharsLimit {
			get { return chars_limit; }
			set { chars_limit = value; }
		}
		
		string name;
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		public string FullName
		{
			get
			{
				if(service != null)
					return service.Name + Name;
				else
					return Name;
			}
		}
		
		int wordsCount = -1;
		public int WordsCount {
			get { return wordsCount; }
			set { wordsCount = value; }
		}
		
		bool splitToSubQueries;
		public bool SplitToSubQueries {
			get { return splitToSubQueries; }
			set { splitToSubQueries = value; }
		}
		
		int maxCountOfSubQueries = 1;
		public int MaxCountOfSubQueries {
			get { return maxCountOfSubQueries; }
			set { maxCountOfSubQueries = value; }
		}
		
		public virtual string[] SplitPhraseToSubqueries(string phrase)
		{
			return new string[]{phrase};
		}
		
		Service service;
		public Service Service {
			get { return service; }
			set { service = value; }
		}
		
		LanguagePairCollection supportedTranslations = new LanguagePairCollection();
		
		public ReadOnlyLanguagePairCollection SupportedTranslations
		{
			get{return new ReadOnlyLanguagePairCollection(supportedTranslations);}
		}
		
		protected internal void AddSupportedTranslation(LanguagePair languagePair)
		{
			supportedTranslations.Add(languagePair);
		}

		protected internal void AddSupportedTranslation(Language from, Language to)
		{
			AddSupportedTranslation(new LanguagePair(from ,to));
		}
		
		protected internal void AddSupportedTranslationToEnglish(Language from)
		{
			AddSupportedTranslation(from, Language.English);
			AddSupportedTranslation(from, Language.English_GB);
			AddSupportedTranslation(from, Language.English_US);
		}

		protected internal void AddSupportedTranslationFromEnglish(Language to)
		{
			AddSupportedTranslation(Language.English, to);
			AddSupportedTranslation(Language.English_GB, to);
			AddSupportedTranslation(Language.English_US, to);
		}
		
		SubjectCollection supportedSubjects = new SubjectCollection();
		public ReadOnlySubjectCollection SupportedSubjects {
			get { return new ReadOnlySubjectCollection(supportedSubjects); }
		}
		
		protected internal void AddSupportedSubject(string subject)
		{
			supportedSubjects.Add(subject);
		}
		
		protected internal Result CreateNewResult(string phrase, LanguagePair languagesPair, string subject)
		{
			Result res = new Result(this, phrase, languagesPair, subject);
			return res;
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String)")]
		public Result Translate(string phrase, LanguagePair languagesPair, string subject, NetworkSetting networkSetting)
		{
			if(!SupportedSubjects.Contains(subject))
				throw new ArgumentException("Subject : " + subject + " not supported");
			
			Result result = ResultsCache.GetCachedResult(this, phrase, languagesPair, subject);
			lock(result)
			{
				if(result.IsHasData())
					return result;
					
				long start = DateTime.Now.Ticks;			
				try
				{
					string error;
					if(CheckPhrase(phrase, out error))
						DoTranslate(phrase, languagesPair, subject, result, networkSetting);
					else
						result.Error = new TranslationException(error);
				}
				catch(System.Exception e)
				{
					result.Error = e;
				}
				result.QueryTicks = DateTime.Now.Ticks - start;
			}
			return result;
		}
		
		protected abstract void DoTranslate(string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting);
	
		public virtual bool CheckPhrase(string phrase, out string error)
		{
			error = "";
			if(string.IsNullOrEmpty(phrase))
			{
				error = "Nothing to translate";
				return false;
			}

			if(chars_limit != -1 && phrase.Length > chars_limit)
			{
				error = "Length too big";
				return false;
			}
			
			return true;
		}
	}
	
	public class ServiceItemsCollection : Collection<ServiceItem>
	{
		public ServiceItemsCollection()
		{
		}
	}

	public class ReadOnlyServiceItemsCollection: ReadOnlyCollection<ServiceItem>
	{
		public ReadOnlyServiceItemsCollection(ServiceItemsCollection list):base(list)
		{
		
		}
	}
	
	[Serializable] 
	public class LanguagePairServiceItemsDictionary	 : Dictionary<LanguagePair, ServiceItemsCollection>
	{
		public LanguagePairServiceItemsDictionary()
		{
		}
		
		protected LanguagePairServiceItemsDictionary(SerializationInfo info, StreamingContext context):base(info, context)
		{
		
		}
	}
	
	
}
