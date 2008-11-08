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
		Afrikaans,
		Albanian,
		Amharic,
		Autodetect,
		Arabic,
		Armenian,
		Azerbaijani,
		Basque,
		Belarusian,
		Bengali,
		Bihari,
		Bosnian,
		Breton,
		Bulgarian,
		Burmese,
		Catalan,
		Cherokee,
		Chinese,
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]
		Chinese_CN, //Chinese_Simplified,
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]
		Chinese_TW, //Chinese_Traditional,
		Croatian,
		Czech,
		Danish,
		Dhivehi,
		Dutch,
		English,
		
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]
		English_GB,

		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]		
		English_US,
		Esperanto,
		Estonian,
		Icelandic,
		Faroese,
		Filipino,
		Finnish,
		French,
		Frisian,
		Galician,
		Georgian,
		German,
		Greek,
		Guarani,
		Gujarati,
		Hebrew,
		Hindi,
		Hungarian,
		Indonesian,
		Inuktitut,
		Interlingua,
		Irish,
		Italian,
		Japanese,
		Japanese_Romaji,
		Kazakh,
		Khmer,
		Korean,
		Kurdish,
		Kyrgyz,
		Laothian,
		Latin,
		Latvian,
		Lithuanian,
		Macedonian,
		Malay,
		Malayalam,
		Maltese,
		Marathi,
		Mongolian,
		Nepali,
		Norwegian,
		Norwegian_Bokmal,
		Norwegian_Nynorsk,
		Oriya,
		Papiamento,
		Pashto,
		Persian,
		Polish,
		Portuguese,
		
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId="Member")]		
		Portuguese_BR,
		Punjabi,
		Romanian,
		Russian,
		Sanskrit,
		Serbian,
		Sindhi,
		Sinhalese,
		Slovak,
		Slovenian,
		Spanish,
		Spanish_LA,
		Swahili,
		Swedish,
		Tajik,
		Tagalog,
		Tamil,
		Telugu,
		Thai,
		Tibetan,
		Turkish,
		Ukrainian,
		Urdu,
		Uzbek,
		Uighur,
		Vietnamese,
		Welsh,
		Last,
		Unknown
	}
	
	public class LanguagePair : System.IEquatable<LanguagePair>, IComparable<LanguagePair>
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
		
		public bool Equals(LanguagePair obj)
		{
			if(Object.ReferenceEquals(obj,null))
				return false;
			if(Object.ReferenceEquals(obj,this))
				return true;
			return this.from == obj.from && this.to == obj.to;	
		}
		
		public override bool Equals(Object obj)
		{
			LanguagePair arg = obj as LanguagePair;
			if(!Object.ReferenceEquals(obj,null))
				return this.Equals(arg);
			return false;	
		}

		public override int GetHashCode() 
		{
   			return (int)from * ((int)Language.Unknown + 1) + (int)to;
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
			return left.from == right.from && left.to == right.to;	
		}

		public static bool operator !=(LanguagePair left, LanguagePair right)
		{
			return !(left == right);
		}
		
		public int CompareTo(LanguagePair other)
		{
			int result = from - other.from;
			if(result == 0)
				result = to - other.to;
			return result;	
		}

	}
	
	public class LanguageCollection : List<Language>
	{
		public LanguageCollection(LanguageCollection list):base(list)
		{
		
		}
		
		public LanguageCollection():base()
		{
		
		}
		
	}
	
	public class ReadOnlyLanguageCollection : ReadOnlyCollection<Language>
	{
		public ReadOnlyLanguageCollection(LanguageCollection list):base(list)
		{
		
		}
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
		public static bool IntelligentCompare(Language language1, Language language2)
		{
			if(language1 == Language.Any || language2 == Language.Any)
				return true;
				
			language1 = ParentLanguage(language1);
			language2 = ParentLanguage(language2);
			return language1 == language2;
		}
		
		public static Language ParentLanguage(Language language)
		{
			Language lang = language;	 
			
			if(lang == Language.English_US || lang == Language.English_GB)
				lang = Language.English;
				
			if(lang == Language.Chinese_CN || lang == Language.Chinese_TW)
				lang = Language.Chinese;

			if(lang == Language.Portuguese_BR)
				lang = Language.Portuguese;

			if(lang == Language.Tagalog)
				lang = Language.Filipino;
				
			return lang;	
		}
		
		public static bool IsLanguageSupported(CultureInfo culture, Language language)
		{
			// TODO: support of Chinese etc
		
			if(culture == null)
				throw new ArgumentNullException("culture");
				
			Language lang = ParentLanguage(language);	 
			
			string name = Enum.GetName(typeof(Language), lang);
			
			if(culture.EnglishName.IndexOf(name) >= 0)
				return true;
				
			CultureInfo parent = culture.Parent;
			return parent.EnglishName.IndexOf(name) >= 0;
		}
	}
}
