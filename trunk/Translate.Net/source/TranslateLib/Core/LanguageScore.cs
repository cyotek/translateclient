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

namespace Translate
{
	/// <summary>
	/// Description of LanguageScore.
	/// </summary>
	public class LanguageScore
	{
		public LanguageScore()
		{
		}
		
		public LanguageScore(Language language, double score, bool isReliable)
		{
			this.language = language;
			this.score = score;
			this.isReliable = isReliable;
			if(score <= 30 && !isReliable)
				this.confidence = Confidence.Poor;
			else if (score > 50)	
				this.confidence = Confidence.Strong;
			else if(isReliable)
				this.confidence = Confidence.Intermediate;
			else	
				this.confidence = Confidence.Poor;
		}
		
		public LanguageScore(Language language, Confidence confidence, double score)
		{
			this.language = language;
			this.confidence = confidence;
			this.score = score;
		}
		
		public LanguageScore(Language language, Confidence confidence)
		{
			this.language = language;
			this.confidence = confidence;
			switch(confidence)
			{
				case Confidence.Strong:
					this.score = 100;
					break;
				case Confidence.Intermediate:	
					this.score = 50;
					break;
				case Confidence.Poor:	
					this.score = 30;
					break;
			}
		}
		
		
		Language language;
		public Language Language {
			get { return language; }
		}
		
		Confidence confidence = Confidence.Unknown;
		public Confidence Confidence {
			get { return confidence; }
		}
		
		double score;
		public double Score {
			get { return score; }
		}
		
		bool isReliable;
		public bool IsReliable
		{
			get { return isReliable; }
		}
		
	}
	
	public class ScoresCollection : Collection<LanguageScore>
	{
	
	}
	
	public class ReadOnlyScoresCollection : ReadOnlyCollection<LanguageScore>
	{
		public ReadOnlyScoresCollection(ScoresCollection collection):base(collection)
		{
			
		}
	}
}
