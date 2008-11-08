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
using System.Collections.Generic;
using System.Globalization;

namespace FreeCL.RTL
{
	/// <summary>
	/// Description of CommandLineHelper.
	/// </summary>
	public static class CommandLineHelper
	{
		static Dictionary<string, string> switches; 
		static List<string> arguments; 
		
		static CommandLineHelper()
		{
			switches = new Dictionary<string, string>();
			arguments = new List<string>();
			//parse all
			
			string[] args = Environment.GetCommandLineArgs ();
			foreach(string arg in args)
			{
				if(arg.StartsWith("/") || arg.StartsWith("-"))
				{
					string argVal = arg.Substring(1);
					argVal = argVal.ToLowerInvariant();
					switches[argVal] = null;
				}
				arguments.Add(arg.ToLowerInvariant());
			}			
		}
		
		public static bool IsCommandSwitchSet(string switchName)
		{
			return switches.ContainsKey(switchName.ToLowerInvariant());
		}

		public static string GetCommandSwitchValue(string switchName)
		{
			string result = "";
			if(!switches.ContainsKey(switchName.ToLowerInvariant()))
				return result;
				
			for(int i = 0; i < arguments.Count; i++)
			{
				string arg = arguments[i];
				if(arg.StartsWith("/") || arg.StartsWith("-"))
				{
					string argVal = arg.Substring(1);
					if(string.Compare(argVal, switchName, true, CultureInfo.InvariantCulture) ==0 )
					{
						if(i < arguments.Count - 1)
						{
							string val = arguments[i + 1];
							if(!val.StartsWith("/") && !val.StartsWith("-"))
							{
								result = val;
							}
						}
						break;						
					}
				}
			}
			return result;
		}
		
	}
}
