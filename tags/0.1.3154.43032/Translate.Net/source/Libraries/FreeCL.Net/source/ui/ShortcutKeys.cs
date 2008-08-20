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
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using FreeCL.UI;
using System.ComponentModel.Design;
using System.Globalization;
using FreeCL.UI.Actions;
using System.Diagnostics.CodeAnalysis;



namespace FreeCL.UI.Actions
{
	/// <summary>
	/// Description of ShortcutKeys.
	/// </summary>
	[
		DesignTimeVisible(false),
		ToolboxItem(false),
	]
	public class ShortcutKeys : System.ComponentModel.Component
	{
		private Keys _keys = new Keys();
		internal Action _owner;		

		
		#region public interface
		public ShortcutKeys(System.ComponentModel.IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException("container");
		
			container.Add(this);
		}

		public ShortcutKeys()
		{
		}

		public ShortcutKeys(Keys keys)
		{
			_keys = keys;
		}
		
		/// <summary>
		/// Keys used
		/// </summary>
		[
		Category("Behavior"), 
		Description("Keys Used."),
		]
		public Keys Keys
		{
			get
			{
				return _keys;
			}
			set
			{
				_keys = value;
			}
		}
		
		/// <summary>
		/// The ActionList to which this action belongs
		/// </summary>
		[Browsable(false)]
		public Action Parent
		{
			get
			{
				return _owner;
			}
		}
		
		
		#endregion
		
	}
	
	
	/// <summary>
	/// <para>TypeConverter for an extender provider which provides an Action property which is one of the Action of an ActionCollection</para>
	/// <para>As I've found no way to get access to the associated collection, an Action provides information
	/// about the associated collection through the "Parent" property. As the Action item is initialised in the GetAction method of the extender provider,
	/// it's possible to get a reference to the ActionCollection (which is only needed for filling the list of 
	/// possible values) during the call to ConvertTo.</para>
	/// </summary>
	public class ShortcutKeysConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of one type to the type of this converter.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
		/// <param name="sourceType">A Type that represents the type you want to convert from.</param>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
		{
			if (sourceType == typeof(string)) 
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}
		
		/// <summary>
		/// Converts the given value to the type of this converter.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
		/// <param name="culture">The CultureInfo to use as the current culture.</param>
		/// <param name="value">The Object to convert.</param>
		/// <returns>An Object that represents the converted value.</returns>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String)")]
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
		{
			if(context == null)
				throw new ArgumentNullException("context");
		
			string stringValue = value as string;
			if (stringValue != null) 
			{
				try
				{
					Debug.Assert(_action != null && _action.Shortcuts != null);
					if (string.IsNullOrEmpty(stringValue))
					{
						return _action.Shortcuts.Null;
					}

					IReferenceService rs = (IReferenceService)context.GetService(typeof(IReferenceService));
					Debug.Assert(rs != null);
					if(rs == null)
						return _action.Shortcuts.Null;
					
					return rs.GetReference(stringValue);
				}
				catch
				{
					throw new ArgumentException("Can not convert '" + stringValue + "' to type Object");
				}
			}
			return base.ConvertFrom(context, culture, value);
		}
		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
		/// <param name="culture">A CultureInfo object. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
		/// <param name="value">The Object to convert.</param>
		/// <param name="destinationType">The Type to convert the value parameter to.</param>
		/// <returns>An Object that represents the converted value.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
		{	
		
			if(context == null)
				throw new ArgumentNullException("context");
		
			if (destinationType == typeof(string) && context != null) 
			{
				IReferenceService rs = (IReferenceService)context.GetService(typeof(IReferenceService));
				//Debug.Assert(rs != null);
				ShortcutKeys a = (ShortcutKeys)value;
				if (a != null)
				{
					// here's the hack for getting a reference to the associated collection
					_action = a.Parent;

					//Debug.Assert(_actionList != null && _actionList.Actions != null);
					if (a == _action.Shortcuts.Null)
					{
						return "";
					}
					return rs.GetName(a);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
		/// <summary>
		/// Returns whether this object supports a standard set of values that can be picked from a list.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) 
		{
			return true;
		}
		/// <summary>
		/// Returns whether the collection of standard values returned from GetStandardValues is an exclusive list.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) 
		{
			return true;
		}
		/// <summary>
		/// Returns a collection of standard values for the data type this type converter is designed for.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) 
		{
			//System.Windows.Forms.MessageBox.Show("GetStandardValues");
			Debug.Assert(_action != null && _action.Shortcuts != null);
			ArrayList res = new ArrayList();
			res.Add(_action.Shortcuts.Null);
			foreach (Object o in _action.Shortcuts)
			{
				res.Add(o);
			}
			return	new StandardValuesCollection(res);
		}

		private Action	_action;
	}
	
}
