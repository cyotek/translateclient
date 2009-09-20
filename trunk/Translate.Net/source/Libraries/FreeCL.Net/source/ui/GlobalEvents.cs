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
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.UI
{
	/// <summary>
	/// Description of GlobalEvents.	
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(FreeCL.UI.GlobalEvents))]
	public class GlobalEvents : System.ComponentModel.Component
	{
		
		public GlobalEvents()
		{
		}

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]		
		static GlobalEvents()
		{
			System.Windows.Forms.Application.Idle += GlobalEventsIdle;
		}
		
		public GlobalEvents(System.ComponentModel.IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}
		

		static bool allowIdleProcessing = true;
		public static bool AllowIdleProcessing {
			get { return allowIdleProcessing; }
			set { allowIdleProcessing = value; }
		}
		
		static bool inIdleLoop = false;
		
		private static event EventHandler InternalIdle;
		
		static void GlobalEventsIdle(object sender, EventArgs e)
		{
			if(!allowIdleProcessing || inIdleLoop)
				return;
				
			if(InternalIdle != null)
			{
				try
				{
					inIdleLoop = true;
					InternalIdle(sender, e);
				}
				finally
				{
					inIdleLoop = false;
				}
			}
		}
		


		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public event EventHandler Idle
		{
			add
			{
				InternalIdle += value;
			}
			remove
			{
				InternalIdle -= value;				 
			}
		}

		public static event EventHandler StaticIdle
		{
			add
			{
				InternalIdle += value;
			}
			remove
			{
				InternalIdle -= value;				 
			}
		}
		
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public event System.Threading.ThreadExceptionEventHandler ThreadException	
		{
			add
			{
				System.Windows.Forms.Application.ThreadException += value;				 
			}
			remove
			{
				System.Windows.Forms.Application.ThreadException -= value;				 
			}
		}
		
	}
}
