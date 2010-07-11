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
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace Translate
{
	/// <summary>
	/// Description of JsonParser.
	/// </summary>
	public class JsonParser
	{
		string data;
		int position = 0;
		public JsonParser(string data)
		{
			this.data = data;
		}
		
		public static JsonItem Parse(string data)
		{
			if(string.IsNullOrEmpty(data))
				throw new TranslationException("JSON should contain data");
			
			//previously fixes string 
			data = data.Replace("\\\"", "<quote_str>");
			JsonParser parser = new JsonParser(data);
			return parser.ParseNext();
		}
		
		string GetNextChar()
		{
			string nextChar = data[position].ToString();
			position++;
			return nextChar;
		}
		
		string GetCurrentChar()
		{
			string nextChar = data[position].ToString();
			return nextChar;
		}
		
		static string FixString(string data)
		{
			data = data.Replace("<quote_str>", "\"");
			data = data.Replace("\\\\", "\\");
			data = data.Replace("\\n", "\n");
			data = data.Replace("\\r", "\r");
			data = data.Replace("\\t", "\t");
			data = HttpUtilityEx.HtmlDecode(data);
			return data;
		}
		public JsonItem ParseNext()
		{
			string nextChar = GetNextChar();
			
			if(string.IsNullOrEmpty(data))
				return null;
			
			JsonItem result;
			if(nextChar == "[")
			{ //array
				result = ParseArray();
			}
			else if(nextChar == "{")
			{ //object
				result = ParseObject();
			}
			else if(nextChar == "\"")
			{ //string value 
				int pos = data.IndexOf("\"", position);
				if(pos < 0)				
					throw new TranslationException("JSON parsing error. Not found \" at pos : " + (position).ToString() + 
						" in string : " + data.Substring(position));
				if(pos-position > 1)		
					result = new JsonValue(FixString(data.Substring(position, pos-position)));
				else	
					result = new JsonValue(""); //null val
				position = pos+1;
			}
			else
			{ //simple value 
				string val = nextChar;
				List<string> separators = new List<string>(new string[]{",", "}", "]"});
				while(!separators.Contains(GetCurrentChar()))
				{
					val += GetNextChar();
				}
				result = new JsonValue(val);				
			}
			return result;
		}
		
		JsonArray ParseArray()
		{
			JsonArray result = new JsonArray();
			do
			{
				JsonItem next = ParseNext();
				result.Add(next);
			}
			while(GetNextChar() != "]");
			return result;
		}
		
		JsonObject ParseObject()
		{
			JsonObject result = new JsonObject();
			do
			{
				JsonItem propName = ParseNext();
				if(!(propName is JsonValue) || string.IsNullOrEmpty(((JsonValue)propName).Value) )
					throw new TranslationException("JSON parsing error. Not found property name at pos : " + (position).ToString() + 
						" in string : " + data);
				string propNameStr = ((JsonValue)propName).Value;
				if(result.ContainsKey(propNameStr))
					throw new TranslationException("JSON parsing error. The property " + propNameStr + "already  exists. At pos : " + (position-1).ToString() + 
					" in string : " + data);
				
				
				if(GetNextChar() != ":")
					throw new TranslationException("JSON parsing error. Not found ':' at pos : " + (position-1).ToString() + 
					" in string : " + data);
					
				JsonItem next = ParseNext();
				
				result.Add(propNameStr, next);
			}
			while(GetNextChar() != "}");
			return result;
		}
		
	}

	public interface JsonItem
	{
	}
	
	public class JsonArray : List<JsonItem>, JsonItem
	{
	}
	
	public class JsonObject : Dictionary<string, JsonItem>, JsonItem
	{
	
	}
	
	public class JsonValue : JsonItem
	{
		public JsonValue(string itemValue)
		{
			this.itemValue = itemValue;
		}
		
		string itemValue;
		
		public string Value
		{
			get { return itemValue; }
			set { itemValue = value; }
		}
		
	}
	
}
