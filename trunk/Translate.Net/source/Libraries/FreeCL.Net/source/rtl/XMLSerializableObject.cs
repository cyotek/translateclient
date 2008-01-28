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
using System.IO;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization; 
using System.Runtime.Serialization.Formatters.Soap; 
using System.Security;
using System.Security.Permissions;
using System.Diagnostics.CodeAnalysis;


namespace FreeCL.RTL
{
	/// <summary>
	/// Description of XMLSerializableObject.
	/// </summary>
	[Serializable()]
	public class XmlSerializableObject : ISerializable
	{
		[NonSerialized]
		bool useSoapSerialization;
		
		protected bool UseSoapSerialization {
			get { return useSoapSerialization; }
			set { useSoapSerialization = value; }
		}
		
		
		public XmlSerializableObject()
		{
		}
		
		protected XmlSerializableObject(SerializationInfo info, StreamingContext context)		
		{
			
		}
		
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]		
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]		
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			
		}

		public void Save(System.IO.Stream stream)
		{
			if(stream == null)
				throw new ArgumentNullException("stream");
		
			OnSave();

			if(UseSoapSerialization)
			{
				SoapFormatter Serializer = new SoapFormatter();
	
				Serializer.Serialize(stream, this);
			}
			else
			{
				XmlSerializer Serializer = new XmlSerializer(this.GetType());
	
				Serializer.Serialize(stream, this);
			}
			stream.Flush();
			stream.Close();
			OnSaved();
		}
		
		public void Save(string fileName)
		{
			FileStream writer = new FileStream(fileName, FileMode.Create);			
			Save(writer);
			writer.Dispose();
		}

		public virtual void OnSave()
		{
			
		}
		
		public virtual void OnSaved()
		{
			
		}
		
		public virtual void OnLoaded()
		{
			
		}

		public static XmlSerializableObject Load(System.IO.Stream stream, Type objectType, bool useSoapSerialization)
		{
			if(stream == null)
				throw new ArgumentNullException("stream");
				
			object o;

			if(useSoapSerialization)				
			{
				SoapFormatter Serializer = new SoapFormatter();
					
				o = Serializer.Deserialize(stream);
			}
			else
			{
				XmlSerializer Serializer = new XmlSerializer(objectType);
				 
				o = Serializer.Deserialize(stream);
			}
			stream.Flush();
			stream.Close();
			XmlSerializableObject result = ((XmlSerializableObject)o);
			result.OnLoaded();
			return	result;
		}
		

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String,System.String)")]		
		public static XmlSerializableObject Load(string fileName, Type objectType, bool useSoapSerialization)
		{
			if( System.IO.File.Exists(fileName))
			{
				
				FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				
				XmlSerializableObject res = Load(fileStream, objectType, useSoapSerialization);
				fileStream.Dispose();
				return res;
			}
			throw new System.ArgumentException("File " + fileName + " not found", "fileName");
		}
		
	}
}
