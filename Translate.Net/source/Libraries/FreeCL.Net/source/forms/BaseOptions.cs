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
using System.Xml.Serialization;
using FreeCL.RTL;
using System.IO;
using System.ComponentModel;
using System.Collections;
using FreeCL.UI;
using System.Runtime.Serialization; 
using System.IO.IsolatedStorage;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of BaseOptions.	
	/// </summary>
	[Serializable()]
	public class BaseOptions : FreeCL.RTL.XmlSerializableObject
	{
		public BaseOptions()
		{
			LoadFileName();
		}
		
		protected BaseOptions(SerializationInfo info, StreamingContext context):base(info, context)		
		{
			if(info == null)
				throw new ArgumentNullException("info");
		
			LoadFileName();
			ArrayList tmp = new ArrayList();
			heapNames = (ArrayList)info.GetValue("HeapNames", tmp.GetType());
			heapValues = (ArrayList)info.GetValue("HeapValues", tmp.GetType());			
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]		
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]		
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if(info == null)
				throw new ArgumentNullException("info");
		
			info.AddValue("HeapNames", HeapNames, HeapNames.GetType());
			info.AddValue("HeapValues", HeapValues, HeapValues.GetType());
			base.GetObjectData(info, context);
		}
		
		void LoadFileName()
		{
			OptionsFileName_ =	Application.DataFolder + this.GetType().Name +	".xml";			
		}
			
		
		[NonSerialized]
		private string OptionsFileName_;
		
		[XmlIgnore]		
		public string OptionsFileName
		{
			set{OptionsFileName_ = value;}
			get{return OptionsFileName_;}
		}
		
		public override void OnLoaded()
		{
			base.OnLoaded();
			LoadFileName();
			if(string.IsNullOrEmpty(language))
			{
				language = LangPack.CurrentLanguage;
			}
			else
				LangPack.Load(language);
		}

		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static BaseOptions Load(BaseOptions source)
		{
			if(source == null)
				throw new ArgumentNullException("source");
		
			try
			{
				if( System.IO.File.Exists(source.OptionsFileName_))
				{
					FileStream FStream = new FileStream(source.OptionsFileName_, FileMode.Open, FileAccess.Read, FileShare.Read);			 
					if(FStream.Length > 0)
				 		return (BaseOptions)XmlSerializableObject.Load(FStream, source.GetType(), source.UseSoapSerialization);				
				 	else	
				 		return source;
			 	}	 
			 	else 
					return source;				
			}
			catch(System.Exception e)
			{
				FreeCL.Forms.Application.OnThreadException(e);
				return source;				
			}
		}

		public void Save()
		{
			base.Save(OptionsFileName_);
		}

		[NonSerialized]
		private ArrayList heapNames = new ArrayList();
		[NonSerialized]
		private ArrayList heapValues = new ArrayList();		
		
		//[XmlArray]
		[XmlElement(Type = typeof(String))]
		public ArrayList HeapNames
		{
			get{return heapNames;}
		}

		//[XmlArray]
		[XmlElement(Type = typeof(String))]
		public ArrayList HeapValues
		{
			get{return heapValues;}
		}
		
		public void SetValue(string name, string value)
		{
			int idx = heapNames.IndexOf(name);
			if(idx < 0)
			{
				heapNames.Add(name);
				heapValues.Add(value);
			}
			else
			{
				heapValues[idx] = value;
			}
		}


		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]
		public string GetValue(string name)
		{
			int idx = heapNames.IndexOf(name);
			if(idx < 0)
			{
				throw new System.ArgumentException("The value for tag " + name + " not found", "name");
			}
			else
			{
				return (string)heapValues[idx];
			}
		}
		
		public string GetValue(string name, string defaultValue)
		{
			int idx = heapNames.IndexOf(name);
			if(idx < 0)
			{
				SetValue(name, defaultValue);
				return defaultValue;
			}
			else
			{
				return (string)heapValues[idx];
			}
		}

		public void SetValue(string name, object value)
		{
			if(value == null)
				throw new ArgumentNullException("value");
		
			SetValue(name, value.ToString());
		}
		
		public int GetIntValue(string name)
		{
			string val = GetValue(name);
			return Int32.Parse(val, CultureInfo.InvariantCulture);
		}
		
		public int GetValue(string name, int defaultValue)
		{
			string val = GetValue(name, defaultValue.ToString(CultureInfo.InvariantCulture));
			return Int32.Parse(val, CultureInfo.InvariantCulture);
		}

		public bool GetValue(string name, bool defaultValue)
		{
			string val = GetValue(name, defaultValue.ToString(CultureInfo.InvariantCulture));
			return Boolean.Parse(val);
		}
		
		[NonSerialized]
		string language;
		
		public string Language {
			get { return language; }
			set { language = value; }
		}
		
		
	}
}
