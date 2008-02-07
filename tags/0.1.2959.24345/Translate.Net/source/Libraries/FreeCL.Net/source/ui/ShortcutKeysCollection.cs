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
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
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
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization; 

namespace FreeCL.UI.Actions
{

	internal class ShortcutKeysCollectionEditor : CollectionEditor
	{
		
		/// <summary>
		/// Constructor
		/// </summary>
		public ShortcutKeysCollectionEditor() 
			: base(typeof(ShortcutKeysCollection))
		{
			
		}
		
		/// <summary>
		/// Gets an array of objects containing the specified collection.
		/// </summary>
		/// <param name="editValue">The collection to edit</param>
		/// <returns>An array containing the collection objects.</returns>
		protected override object[] GetItems(object editValue)
		{
			ShortcutKeysCollection	coll = editValue as ShortcutKeysCollection;
			Debug.Assert(editValue != null && coll != null);
			
			ShortcutKeys [] res = new ShortcutKeys[coll.Count];
			if (coll.Count > 0)
			{
				coll.CopyTo(res, 0);
			}
			return res;
		}
		/// <summary>
		/// Sets the specified array as the items of the collection.
		/// </summary>
		/// <param name="editValue">The collection to edit.</param>
		/// <param name="value">An array of objects to set as the collection items.</param>
		/// <returns>The newly created collection object or, otherwise, the collection indicated by the editValue parameter.</returns>
		protected override object SetItems(
			object editValue,
			object[] value
			)
		{
			ShortcutKeysCollection	coll = editValue as ShortcutKeysCollection;
			Debug.Assert(editValue != null && coll != null);
			
			
			coll.Clear();
			foreach(object o in value)
			{
				coll.Add((ShortcutKeys)o);
			}
			return coll;
		}
	}
	
	/// <summary>
	///	 A collection that stores <see cref='ShortcutKeys'/> objects.
	/// </summary>
	[Serializable()]
	[Editor(typeof(ShortcutKeysCollectionEditor), typeof(UITypeEditor))]
	public class ShortcutKeysCollection : List<ShortcutKeys>, IDisposable
	{
		
		[NonSerialized()]
		private Action	_owner;
		[NonSerialized()]
		private ShortcutKeys		_null = new ShortcutKeys();
		
		
		/// <summary>
		///	Initializes a new instance of ActionCollection.
		/// </summary>
		public ShortcutKeysCollection(Action owner) 
		{
			Debug.Assert(owner != null);
			_owner = owner;
			_null._owner = _owner;
		}
		
		/// <summary>
		/// Returns the ActionList which owns this ActionCollection
		/// </summary>
		public Action Parent
		{
			get
			{
				return _owner;
			}
		}

		/// <summary>
		/// Returns a reference to the "null" action of this collection (needed in design mode)
		/// </summary>
		internal ShortcutKeys Null
		{
			get
			{
				return _null;
			}
		}
		
		
		/// <summary>
		///	 Initializes a new instance of <see cref='ShortcutKeysCollection'/> based on another <see cref='ShortcutKeysCollection'/>.
		/// </summary>
		/// <param name='val'>
		///	 A <see cref='ShortcutKeysCollection'/> from which the contents are copied
		/// </param>
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public ShortcutKeysCollection(ShortcutKeysCollection collection)
		{
			this.AddRange(collection);
		}
		
		/// <summary>
		///	 Initializes a new instance of <see cref='ShortcutKeysCollection'/> containing any array of <see cref='ShortcutKeys'/> objects.
		/// </summary>
		/// <param name='val'>
		///			 A array of <see cref='ShortcutKeys'/> objects with which to intialize the collection
		/// </param>
		public ShortcutKeysCollection(ShortcutKeys[] collection)
		{
			this.AddRange(collection);
		}
		
		
		/// <summary>
		///	 Adds a <see cref='ShortcutKeys'/> with the specified value to the 
		///	 <see cref='ShortcutKeysCollection'/>.
		/// </summary>
		/// <param name='val'>The <see cref='ShortcutKeys'/> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <seealso cref='ShortcutKeysCollection.AddRange'/>
		public new void Add(ShortcutKeys keys)
		{
			if(keys == null)
				throw new ArgumentNullException("keys");
		
			keys._owner = _owner;
			base.Add(keys);
		}
		
		~ShortcutKeysCollection()
	  	{
      		Dispose(false);
	  	}

    	public void Dispose()
    	{
      		Dispose(true);
      		GC.SuppressFinalize(this);
    	}
    
    	protected virtual void Dispose(bool disposing)
    	{
		
			if(disposing)
			{
				if(_null != null)
				{
					_null.Dispose();
					_null = null;
				}
			}		
		}
		
	}
}
