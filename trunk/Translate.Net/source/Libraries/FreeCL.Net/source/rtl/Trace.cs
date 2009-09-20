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
using System.Reflection;
using System.Security.Permissions;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.RTL
{
	/// <summary>
	/// Description of Trace.
	/// </summary>
	public static class Trace
	{
		static Trace()
		{
			if(MonoHelper.IsUnix)
				System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(Console.Out));
		}
		
		static bool traceEnabled = true;
		public static bool TraceEnabled {
			get { return traceEnabled; }
			set { traceEnabled = value; }
		}
		
		
		public static void WriteLine(string line)
		{
			if(traceEnabled)
				System.Diagnostics.Trace.WriteLine(line);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void TraceException(System.Exception exception)
		{
			if(exception == null)
			{
				WriteLine("Exception is null");
				return;
			}
			
			WriteLine("Unhandled exception : " +exception.GetType().FullName + "\n" +
											 "Message : " + exception.Message + "\n" +
											 "Stack Trace \n" + exception.StackTrace + "\n" +
											 "Source : " + exception.Source + "\n" +											 
											 "TargetSite : " + exception.TargetSite + "\n"										 											 
											 );
			 if(exception.InnerException != null)			
			 {
				 WriteLine("Inner Exception");
				 TraceException(exception.InnerException);				 
			 }
			 
			 System.Reflection.ReflectionTypeLoadException tle = exception as System.Reflection.ReflectionTypeLoadException;
			 System.Security.SecurityException se = exception as System.Security.SecurityException;
			 if(tle != null)
			 {
				 WriteLine("Loader Exceptions");				 
				 
				 foreach(Exception tlle in tle.LoaderExceptions)
				 {
					 WriteLine("Loader Exception:");				 
					 TraceException(tlle);				 
				 }
			 }
			 else if(se != null)
			 {
				
				WriteLine("Security Exception");
				WriteLine("Action : " + se.Action);
				
				WriteLine("PermissionType    : " + se.PermissionType);
			 }
			
			
			StackTrace st = new StackTrace(true);
			WriteLine("Current Stack" + st.ToString());
			
			WriteLine("\r\nAssemblies:");
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) 
			{
				WriteLine("Name : " + asm.FullName);
				try
				{
					WriteLine("\tPath:" + asm.Location);
				}
				catch
				{
					
				}
			}
			
			
		}
		
	}
}
