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
using System.Text; 
using System.Globalization; 

namespace Translate
{
	/// <summary>
	/// Fixes problems of System.Web.HttpUtility
	/// </summary>
	public static class HttpUtilityEx
	{
	    /// <summary>
	    /// Converts a string that has been HTML-encoded into a decoded string, and sends the decoded string to a TextWriter output stream.
	    /// </summary>
	    public static string HtmlDecode(string s)
	    {
	    	if(string.IsNullOrEmpty(s))
	    		return s;
	    		
	    	//fix \u four-hex-digits problem
	    	int idx = s.IndexOf("\\u");
	    	if(idx >= 0)
	    	{
		    	string char_code;
		    	ushort code;
		    	StringBuilder sb = new StringBuilder(s.Length);
		    	int prev_idx = 0;
		    	while(idx >= 0)
		    	{
		    		sb.Append(s.Substring(prev_idx, idx - prev_idx));
		    		char_code = s.Substring(idx + 2, 4);
		    		if(ushort.TryParse(char_code, NumberStyles.AllowHexSpecifier, null, out code))
		    			sb.Append((char)code);
		    		prev_idx = idx + 6;
		    		idx = s.IndexOf("\\u", prev_idx);
		    	}
		    	if(s.Length - prev_idx > 0)
		    		sb.Append(s.Substring(prev_idx, s.Length - prev_idx));
		    		
		    	s = sb.ToString();	
	    	}
	    	
	    	s = HttpUtility.HtmlDecode(s);
	    	s = s.Replace("#39;", "'");
			s = s.Replace("&apos;", "'");
			s = s.Replace("<br>", "\n");
			s = s.Replace("&nbsp;", " ");
			s = s.Replace("&#41<", ")<");			//sometime google generate
	    	
	    	return s;
	    }
	
	}
}
