﻿#region License block : MPL 1.1/GPL 2.0/LGPL 2.1
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
using System.Diagnostics.CodeAnalysis;


namespace Translate
{
	/// <summary>
	/// Description of BaseTranslateResult.
	/// </summary>
	public class Result
	{
		public Result(ServiceItem serviceItem, string phrase, LanguagePair languagesPair, string subject)
		{
			this.serviceItem = serviceItem;
			this.languagesPair = languagesPair;
			this.subject = subject;
			this.phrase = phrase;
			childs = new ResultCollection(this);
		}
		
		string phrase;
		public string Phrase {
			get { return phrase; }
			set { phrase = value; }
		}
		
		StringsCollection translations = new StringsCollection();
		public StringsCollection Translations {
			get { return translations; }
		}
		
		string abbreviation;
		public string Abbreviation {
			get { return abbreviation; }
			set { abbreviation = value; }
		}
		
		
		ServiceItem serviceItem;
		public ServiceItem ServiceItem {
			get { return serviceItem; }
			set { serviceItem = value; }
		}
		
		LanguagePair languagesPair;
		public LanguagePair LanguagesPair {
			get { return languagesPair; }
			set { languagesPair = value; }
		}
		
		Exception error;		
		public Exception Error {
			get { return error; }
			set { error = value; }
		}
		
		long queryTicks;
		public long QueryTicks {
			get { return queryTicks; }
			set { queryTicks = value; }
		}
		
		long retryCount;
		public long RetryCount {
			get { return retryCount; }
			set { retryCount = value; }
		}
		
		long bytesSent;
		public long BytesSent {
			get { return bytesSent; }
			set { bytesSent = value; }
		}
		
		long bytesReceived;
		public long BytesReceived {
			get { return bytesReceived; }
			set { bytesReceived = value; }
		}

		string subject;
		public string Subject {
			get { return subject; }
			set { subject = value; }
		}
		
		bool resultNotFound;
		public bool ResultNotFound {
			get { return resultNotFound; }
			set { resultNotFound = value; }
		}
		
		ResultCollection childs;
		public ResultCollection Childs {
			get { return childs; }
		}
		
		Result parent;
		public Result Parent {
			get { return parent; }
			set { parent = value; }
		}
		
		
	}
	
	public class ResultCollection : Collection<Result>
	{
		public ResultCollection()
		{
		}
	
		public ResultCollection(Result parent)
		{
			this.parent = parent;
		}
		
		Result parent;
		
		protected override void InsertItem(int index, Result item)
		{
			if(parent != null)
				item.Parent = parent;
			base.InsertItem(index, item);
		}
		
	}
	
	public class ReadOnlyResultCollection: ReadOnlyCollection<Result>
	{
		public ReadOnlyResultCollection(ResultCollection list):base(list)
		{
		
		}
	}
	
	public class StringsCollection : Collection<string>
	{
	
	}
	
}
