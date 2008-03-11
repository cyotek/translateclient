// --------------------------------------------------------------------------
// Description : CDiese Toolkit library
// Author		 : Serge Weinstock
//
//	You are free to use, distribute or modify this code
//	as long as this header is not removed or modified.
// --------------------------------------------------------------------------

// --------------------------------------------------------------------------
// Further changes made by Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com)
// and available under MPL 1.1/GPL 2.0/LGPL 2.1
//
// Changes list:
//  - .net 2 porting
// --------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;


namespace FreeCL.UI.Actions
{
	/// <summary>
	/// A collection that stores Action Actions.
	/// </summary>
	[Editor(typeof(ActionCollectionEditor), typeof(UITypeEditor))]
	public class ActionCollection : List<Action>, IDisposable
	{
		private ActionList	_owner;
		private Action		_null = new Action();

		/// <summary>
		///	Initializes a new instance of ActionCollection.
		/// </summary>
		public ActionCollection(ActionList owner) 
		{
			Debug.Assert(owner != null);
			_owner = owner;
			_null._owner = _owner;
		}
		/// <summary>
		/// Initialises a new instance of ActionCollection based on another ActionCollection.
		/// </summary>
		/// <param name='value'>An ActionCollection from which the contents are copied</param>
		
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		public ActionCollection(ActionCollection value) 
		{
			this.AddRange(value);
		}
				
		/// <summary>
		/// Initialises a new instance of ActionCollection containing any array of Actions.
		/// </summary>
		/// <param name='value'>An array of Actions with which to intialize the collection</param>
		public ActionCollection(Action[] value) 
		{
			this.AddRange(value);
		}
		
		/// <summary>
		/// Replace inherited add
		/// </summary>
		public new void Add (Action item)
		{
			if(item == null)
				throw new ArgumentNullException("item");
		
			item._owner = _owner;
			base.Add(item);
		}
		
				
		/// <summary>
		/// Returns the ActionList which owns this ActionCollection
		/// </summary>
		public ActionList Parent
		{
			get
			{
				return _owner;
			}
		}

		/// <summary>
		/// Returns a reference to the "null" action of this collection (needed in design mode)
		/// </summary>
		internal Action Null
		{
			get
			{
				return _null;
			}
		}
		
		~ActionCollection()
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
