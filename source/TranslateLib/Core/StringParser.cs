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
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace Translate
{
	/// <summary>
	/// Description of StringParser.
	/// </summary>
	public class StringParser
	{
		static List<char> delimiterCharsList;
		static StringParser()
		{
			char[] delimiterChars = { ' ', ',', '.', ':', ';', '\t', '\n', '!', '?', '(', ')', '[', ']', '{', '}', '*', '/', '@', '#', '$', '%', '^', '&', '+', '=', '\\', '|', '\u00A0' };

			delimiterCharsList = new List<char>(delimiterChars);
			delimiterCharsList.Sort();
		}
		
		public static List<char> DelimiterCharsList {
			get { return delimiterCharsList; }
		}
		
		
		string internalData;
		int pos;
		public StringParser(string startTag, string endTag, string data)
		{
			internalData = Parse(startTag, endTag, data);
		}

		public StringParser(string data)
		{
			internalData = data;
		}
		
		public bool ExistsTag(string tag, string stopTag) 
		{
			if(string.IsNullOrEmpty(internalData.Substring(pos).Trim()))
				return false;
				
			int stopTagIdx = internalData.IndexOf(stopTag, pos);
			int tagIdx = internalData.IndexOf(tag, pos);
			if(stopTagIdx < 0 && tagIdx >= 0)
				return true;
			else if(stopTagIdx >= 0 && tagIdx >= 0)
				return  tagIdx <= stopTagIdx;
			else
				return false;
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]
		public string ReadItem(string startTag, string endTag, string stopTag)
		{
			if(startTag == null)
				throw new ArgumentNullException("startTag");

			if(endTag == null)
				throw new ArgumentNullException("endTag");
				
			if(stopTag == null)
				throw new ArgumentNullException("stopTag");

			int stopTagIdx = internalData.IndexOf(stopTag, pos);

			int startTagLength = startTag.Length;
			int resultIdxStart = internalData.IndexOf(startTag, pos);
			if (resultIdxStart < 0) throw new TranslationException("Can't found start tag :" + startTag +
				" in string : " + internalData.Substring(pos));
			
			if(stopTagIdx >= 0 && stopTagIdx < resultIdxStart)
			{
				return null;
			}
			
			
			resultIdxStart += startTagLength;

			int resultIdxEnd = internalData.IndexOf(endTag, resultIdxStart);
			if (resultIdxEnd < 0) throw new TranslationException("Can't found end tag :" + endTag +
				" in string : " + internalData.Substring(resultIdxStart));
			
			if(stopTagIdx >= 0 && stopTagIdx < resultIdxEnd)
			{
				return null;
			}
			
			String result_string = internalData.Substring(resultIdxStart, resultIdxEnd - resultIdxStart);
			resultIdxEnd += endTag.Length;
			pos = resultIdxEnd;
			return result_string.Trim();
		}

		public string ReadItem(string startTag, string endTag)
		{
			return ReadItem(startTag, endTag, endTag);
		}
		
		public string[] ReadItemsList(string startTag, string endTag)
		{
			return ReadItemsList(startTag, endTag, "238742sdf89723sd9821"); //some unique end tag
		}
		public string[] ReadItemsList(string startTag, string endTag, string stopTag)
		{
			List<string> res = new List<string>();
			string item;
			while(internalData.IndexOf(startTag, pos) >= 0 )
			{
				item = ReadItem(startTag, endTag, stopTag);
				if(string.IsNullOrEmpty(item))
					break;
				res.Add(HttpUtilityEx.HtmlDecode(item));
			}
			return res.ToArray();
		}
		
		public static string[] ParseItemsList(string startTag, string endTag, string data)
		{
			StringParser parser = new StringParser(data);
			return parser.ReadItemsList(startTag, endTag);
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="Translate.TranslationException.#ctor(System.String)")]	
		public static string Parse(string startTag, string endTag, string data)
		{
			if(startTag == null)
				throw new ArgumentNullException("startTag");

			if(endTag == null)
				throw new ArgumentNullException("endTag");

			if(data == null)
				throw new ArgumentNullException("data");
		
			int startTagLength = startTag.Length;
			int resultIdxStart = data.IndexOf(startTag);
			if (resultIdxStart < 0) throw new TranslationException("Can't found start tag :" + startTag + " in string : " +  data);
			resultIdxStart += startTagLength;

			int resultIdxEnd = data.IndexOf(endTag, resultIdxStart);
			if (resultIdxEnd < 0) throw new TranslationException("Can't found end tag :" + endTag + " in string : " +  data);
			
			String result_string = HttpUtilityEx.HtmlDecode(data.Substring(resultIdxStart, resultIdxEnd - resultIdxStart));
			return result_string.Trim();
		}
		
		public static string ExtractLeft(string endTag, string data)
		{
			if(endTag == null)
				throw new ArgumentNullException("endTag");

			if(data == null)
				throw new ArgumentNullException("data");
		
			int resultIdxEnd = data.IndexOf(endTag);
			if (resultIdxEnd < 0) throw new TranslationException("Can't found end tag :" + endTag + " in string : " +  data);
			
			String result_string = HttpUtilityEx.HtmlDecode(data.Substring(0, resultIdxEnd));
			return result_string.Trim();
		}

		public static string ExtractRight(string startTag, string data)
		{
			if(startTag == null)
				throw new ArgumentNullException("startTag");

			if(data == null)
				throw new ArgumentNullException("data");
		
			int startTagLength = startTag.Length;
			int resultIdxStart = data.IndexOf(startTag);
			if (resultIdxStart < 0) throw new TranslationException("Can't found start tag :" + startTag + " in string : " +  data);
			resultIdxStart += startTagLength;

			String result_string = HttpUtilityEx.HtmlDecode(data.Substring(resultIdxStart));
			return result_string.Trim();
		}
		
		public static string RemoveAll(string startTag, string endTag, string data)
		{
			if(startTag == null)
				throw new ArgumentNullException("startTag");

			if(endTag == null)
				throw new ArgumentNullException("endTag");

			if(data == null)
				throw new ArgumentNullException("data");

			StringBuilder result = new StringBuilder();
			
			int pos = 0;
			int endTagLength = endTag.Length;
			int startTagIdx = data.IndexOf(startTag, pos);
			while(startTagIdx >= 0)
			{
				result.Append(data.Substring(pos, startTagIdx - pos));
				pos = data.IndexOf(endTag, startTagIdx);
				if (pos < 0) throw new TranslationException("Can't found end tag :" + endTag + " in string : " +  data);
				pos += endTagLength;
				startTagIdx = data.IndexOf(startTag, pos);
			}
			result.Append(data.Substring(pos));
			return result.ToString();
		}
		
		class TreeParseData
		{
			public bool IsStartTag;
			public int Position;
			
			public TreeParseData(bool isStartTag, int position)
			{
				this.IsStartTag = isStartTag;
				this.Position = position;
			}
		}
		
		public static StringsTree ParseTreeStructure(string startTag, string endTag, string data)
		{
			if(startTag == null)
				throw new ArgumentNullException("startTag");

			if(endTag == null)
				throw new ArgumentNullException("endTag");

			if(data == null)
				throw new ArgumentNullException("data");
				
			int startTagLength = startTag.Length;	
			int endTagLength = endTag.Length;
			List<TreeParseData> parseList = new List<TreeParseData>();
			int idx = 0;
			
			int ident = 0; 
			while(idx < data.Length)
			{
				int resultIdxStart = data.IndexOf(startTag, idx);
				if(resultIdxStart < 0)
					resultIdxStart = int.MaxValue;
					
				int resultIdxEnd = data.IndexOf(endTag, idx);
				if(resultIdxEnd < 0)
					resultIdxEnd = int.MaxValue;
					
				if(resultIdxStart < resultIdxEnd)
				{
					ident++;
					parseList.Add(new TreeParseData(true, resultIdxStart));
					idx = resultIdxStart + startTagLength;
				}
				else if(resultIdxStart > resultIdxEnd)
				{
					ident--;
					if(ident < 0)	
						throw new ArgumentNullException("Wrong data structure", "data");
				
					parseList.Add(new TreeParseData(false, resultIdxEnd));
					idx = resultIdxEnd + endTagLength;
				}
				else
				{	//equal, end found
					break;
				}
			}

			StringsTree current = new StringsTree(null, 0);
			
			for(idx = 0; idx < parseList.Count; idx++)
			{
				if(parseList[idx].IsStartTag)
				{
					StringsTree child = new StringsTree(current, idx);
					current.Childs.Add(child);
					current = child;
				}	
				else
				{   //this is child
					int childStart = parseList[current.Offset].Position;
					int childEnd = parseList[idx].Position;
					if(parseList[current.Offset+1].IsStartTag)
						childEnd = parseList[current.Offset+1].Position;
						
					current.Data = data.Substring(childStart + startTagLength, childEnd - childStart - startTagLength);
					current = current.Parent;
				}	
			}
			return current;
		}
		
		
		public static List<string> SplitToParts(string data)
		{
			List<string> result = new List<string>();
			char[] dataChars = data.ToCharArray();
			
			StringBuilder sb = new StringBuilder();
			foreach(char ch in dataChars)
			{
				if(delimiterCharsList.BinarySearch(ch) >= 0)	
				{
					if(sb.Length >= 0)
					{
						result.Add(sb.ToString());
						sb.Length = 0;
					}
					
					result.Add(ch.ToString());
				}
				else
				{
					sb.Append(ch);
				}
			}
			
			if(sb.Length >= 0)
			{
				result.Add(sb.ToString());
			}
			
			return result;
		}

		public static List<string> SplitToWords(string data)
		{
			List<string> result = new List<string>();
			char[] dataChars = data.ToCharArray();
			
			StringBuilder sb = new StringBuilder();
			foreach(char ch in dataChars)
			{
				if(delimiterCharsList.BinarySearch(ch) >= 0)	
				{
					if(sb.Length >= 0)
					{
						result.Add(sb.ToString());
						sb.Length = 0;
					}
					
					//result.Add(ch.ToString());
				}
				else
				{
					sb.Append(ch);
				}
			}
			
			if(sb.Length >= 0)
			{
				result.Add(sb.ToString());
			}
			
			return result;
		}
		
		public static string SafeResizeString(string data, int length)
		{
			string result = "";
			if(!string.IsNullOrEmpty(data))
			{
				if(data.Length > length)
					result = data.Substring(0, length);
				else
					result = data;
			}		
			return result;
		}
		
	}
	
	public class StringsTree
	{
		StringsTreeList childs = new StringsTreeList();
		public StringsTreeList Childs {
			get { return childs; }
		}
		
		string data;
		public string Data {
			get { return data; }
			set { data = value; }
		}
		
		StringsTree parent;
		public StringsTree Parent {
			get { return parent; }
			set { parent = value; }
		}
		
		public StringsTree(StringsTree parent, int offset)
		{
			this.parent = parent;
			this.offset = offset;
		}
		
		
		int offset;
		public int Offset {
			get { return offset; }
			set { offset = value; }
		}
		
		
	}
	
	public class StringsTreeList : List<StringsTree>
	{
	
	}
}
