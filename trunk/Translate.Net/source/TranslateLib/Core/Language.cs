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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Translate
{
	/// <summary>
	/// Description of Language.
	/// </summary>
	public enum Language
	{
		Any,
		Arabic,
		Belarusian,
		Bulgarian,
		Chinese,
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]
		Chinese_CN, //Chinese_Simplified,
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]
		Chinese_TW, //Chinese_Traditional,
		Croatian,
		Czech,
		Danish,
		Dutch,
		English,
		
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]
		English_GB,

		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]		
		English_US,
		Esperanto,
		Estonian,
		Icelandic,
		Finnish,
		French,
		German,
		Greek,
		Hungarian,
		Italian,
		Japanese,
		Korean,
		Latin,
		Latvian,
		Lithuanian,
		Macedonian,
		Norwegian,
		Polish,
		Portuguese,
		Romanian,
		Russian,
		Serbian,
		Slovak,
		Slovenian,
		Spanish,
		Swedish,
		Ukrainian,
		Last
	}
	
	public class LanguagePair
	{
		public LanguagePair()
		{
			this.From = Language.Any;
			this.To = Language.Any;
		}
	
		public LanguagePair(Language from, Language to)
		{
			this.From = from;
			this.To = to;
		}
		
		Language from;
		public Language From {
			get { return from; }
			set { from = value; }
		}
		
		Language to;
		public Language To {
			get { return to; }
			set { to = value; }
		}
		
		
		public override bool Equals(Object obj)
		{
			LanguagePair arg = obj as LanguagePair;
			if(arg == null) return false;
			return From == arg.From && To == arg.To;
		}

		public override int GetHashCode() 
		{
   			return From.GetHashCode() * 100 + To.GetHashCode();
		}	
			
		public override string ToString()
		{
			return Language.GetName(typeof(Language), From) + 
				" -> " + 
				Language.GetName(typeof(Language), To);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
		public static bool operator ==(LanguagePair left, LanguagePair right)
		{
			bool anull, bnull;
			anull = Object.ReferenceEquals(left,null); 
			bnull = Object.ReferenceEquals(right,null);
			if (anull && bnull) return true;
			if (anull || bnull) return false;
			return left.Equals(right); 
		}

		public static bool operator !=(LanguagePair left, LanguagePair right)
		{
			return !(left == right);
		}
		
	}
	
	public class LanguageCollection : List<Language>
	{
	
	}
	
	public class LanguagePairCollection: List<LanguagePair>
	{
	
	}
	

	public class ReadOnlyLanguagePairCollection: ReadOnlyCollection<LanguagePair>
	{
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]	
		public ReadOnlyLanguagePairCollection(List<LanguagePair> list):base(list)
		{
		
		}
	}
	
	public static class LanguageHelper
	{
		public static bool IsLanguageSupported(CultureInfo culture, Language language)
		{
			// TODO: support of Chinese etc
		
			if(culture == null)
				throw new ArgumentNullException("culture");
				
				
			string name = Enum.GetName(typeof(Language), language);
			if(culture.EnglishName.IndexOf(name) >= 0)
				return true;
				
			CultureInfo parent = culture.Parent;
			return parent.EnglishName.IndexOf(name) >= 0;
		}
	}
}
