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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;


namespace Translate
{
	/// <summary>
	/// Description of LanguageDetectorResult.
	/// </summary>
	public class GuessResult : IServiceItemResult
	{
		public GuessResult()
		{
		}
		
		public GuessResult(LanguageGuesser detectorItem, string phrase)
		{
			this.DetectorItem = detectorItem;
			this.phrase = phrase;
		}
		
		string phrase;
		public string Phrase {
			get { return phrase; }
			set { phrase = value; }
		}
		
		[NonSerialized]
		LanguageGuesser detectorItem;
		[ XmlIgnore()]
		public LanguageGuesser DetectorItem {
			get { return detectorItem; }
			set 
			{ 
				detectorItem = value; 
				if(detectorItem != null)
					Name = detectorItem.FullName;
			}
		}

		string name;
		public string Name {
			get { return name; }
			set { name = value; }
		}
	
		ScoresCollection scores = new ScoresCollection();
		public ReadOnlyScoresCollection Scores {
			get { return new  ReadOnlyScoresCollection(scores); }
		}
		
		public Language Language {
			get 
			{ 
				if(scores.Count != 0)
					return scores[0].Language;
				else
					return Language.Unknown;
			}
		}
		
		public Confidence Confidence {
			get 
			{ 
				if(scores.Count != 0)
					return scores[0].Confidence;
				else
					return Confidence.Unknown;
			}
		}

		public double Score {
			get 
			{ 
				if(scores.Count != 0)
					return scores[0].Score;
				else
					return 0;
			}
		}

		public bool IsReliable {
			get 
			{ 
				if(scores.Count != 0)
					return scores[0].IsReliable;
				else
					return false;
			}
		}
		
		public void AddScore(LanguageScore score)
		{
			scores.Add(score);
		}
		
		public void AddScore(Language language, Confidence confidence, double score)
		{
			AddScore(new LanguageScore(language, confidence, score));
		}
		
		public void AddScore(Language language, Confidence confidence)
		{
			AddScore(new LanguageScore(language, confidence));
		}
		
		public void AddScore(Language language, double score, bool isReliable)
		{
			AddScore(new LanguageScore(language, score, isReliable));
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

		bool resultNotFound;
		public bool ResultNotFound {
			get { return resultNotFound; }
			set { resultNotFound = value; }
		}

		public bool IsHasData()
		{
			return (scores.Count != 0) && !resultNotFound && error == null;
		}

		DateTime lastUsed = DateTime.Now;
		
		public DateTime LastUsed {
			get { return lastUsed; }
			set { lastUsed = value; }
		}
		
	}
	
	public class GuessResultsHashtable : Dictionary<int, GuessResult>
	{
	
	}

	public class GuessResultsCollection : List<GuessResult>
	{
	
	}
	
}
