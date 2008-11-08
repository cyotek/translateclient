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
using System.Threading;

namespace Translate
{
	/// <summary>
	/// Description of BaseServiceItem.
	/// </summary>
	public abstract class BaseServiceItem
	{
		int charsLimit = -1;
		public int CharsLimit {
			get { return charsLimit; }
			set { charsLimit = value; }
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

		Service service;
		public Service Service {
			get { return service; }
			set { service = value; }
		}

		public virtual bool CheckPhrase(string phrase, out string error)
		{
			error = "";
			if(string.IsNullOrEmpty(phrase))
			{
				error = "Nothing to translate";
				return false;
			}

			if(charsLimit != -1 && phrase.Length > charsLimit)
			{
				error = "Length too big";
				return false;
			}

			return true;
		}
		
		protected static void RegisterState(TranslationState state)
		{
			Thread.SetData(
	            Thread.GetNamedDataSlot("TranslationState"), 
            	state);
		}
		
		protected static void UnregisterState()
		{
			RegisterState(null);
		}
		
		public static bool IsTerminated
		{
			get
			{
				object o = Thread.GetData(Thread.GetNamedDataSlot("TranslationState"));
				if(o == null) 
					return false;
					
				TranslationState state = o as TranslationState;	
				if(state == null) 
					return false;

				return state.IsTerminated;				
			}
		}
		
		public static void CheckIsTerminated()
		{
			if(IsTerminated)
				throw new TranslationTerminatedException();
		}
	}
	
}
