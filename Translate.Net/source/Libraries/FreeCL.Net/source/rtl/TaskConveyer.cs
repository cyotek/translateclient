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
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FreeCL.RTL
{
	
	/// <summary>
	/// Description of TaskConveyer.
	/// </summary>
	public static class TaskConveyer
	{
		[Conditional("TRACE")]
		public static void Trace(string message)
		{
			System.Diagnostics.Trace.WriteLine(Environment.TickCount.ToString(CultureInfo.InvariantCulture) + ":" +
											System.Threading.Thread.CurrentThread.GetHashCode().ToString(CultureInfo.InvariantCulture) + ":" +
											message
										 );
		}

		class TaskInfo
		{
			public string Name;
			public object State;
			public WaitCallback Callback;
			
			public TaskInfo(string taskName, object taskState, WaitCallback callback)
			{
				Name = taskName;
				State = taskState;
				Callback = callback;
			}

			public void DoCallback()
			{
				if(Callback != null)
					Callback(State);
			}
		}
		
		
		public static bool QueueTask(string taskName, WaitCallback callback, object state)
		{
			TaskInfo ti = new TaskInfo(taskName, state, callback);
			Trace("QueueTask : " + taskName);
			return ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProc), ti);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		static void TaskProc(Object stateInfo) 
		{
			TaskInfo ti = (TaskInfo) stateInfo;
			Trace("TaskStarted : " + ti.Name);
			try
			{
				ti.DoCallback();
			}
			catch(Exception E)
			{
				FreeCL.RTL.Trace.TraceException(E);
			}

			Trace("TaskStopped : " + ti.Name);
		}
		
		class ExtTaskInfo : TaskInfo
		{
			public WaitCallback StartCallback;			
			public WaitCallback EndCallback;						
			public WaitCallback ExceptionCallback;									
			
			public ExtTaskInfo(string taskName, object taskState, WaitCallback callback, WaitCallback callbackStart, WaitCallback callbackEnd, WaitCallback callbackException):
				base(taskName, taskState, callback)
			{
				StartCallback = callbackStart;	 
				EndCallback = callbackEnd;
				ExceptionCallback = callbackException;
			}
			
			public void DoStartCallback()
			{
				if(StartCallback != null)
					StartCallback(State);
			}

			public void DoEndCallback()
			{
				if(EndCallback != null)
					EndCallback(State);
			}

			public void DoExceptionCallback(Exception E)
			{
				if(ExceptionCallback != null)
					ExceptionCallback(E);
			}
			
		}
		
		public static bool QueueExtTask(string taskName, WaitCallback callback, object state, WaitCallback callbackStart, WaitCallback callbackEnd, WaitCallback callbackException)
		{
			ExtTaskInfo ti = new ExtTaskInfo(taskName, state, callback, callbackStart, callbackEnd, callbackException);
			Trace("QueueExtTask : " + taskName);
			return ThreadPool.QueueUserWorkItem(new WaitCallback(ExtTaskProc), ti);
		}
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		static void ExtTaskProc(Object stateInfo) 
		{
			ExtTaskInfo ti = (ExtTaskInfo) stateInfo;
			Trace("ExtTaskStarted : " + ti.Name);
			
			try
			{
				ti.DoStartCallback();
			}
			catch(Exception E)
			{
				FreeCL.RTL.Trace.TraceException(E);
				ti.DoExceptionCallback(E);
			}
			
			
			try
			{
				ti.DoCallback();
			}
			catch(Exception E)
			{
				FreeCL.RTL.Trace.TraceException(E);
				ti.DoExceptionCallback(E);
			}
			finally
			{
				try
				{
					ti.DoEndCallback();									
				}
				catch(Exception E)
				{
					FreeCL.RTL.Trace.TraceException(E);
					ti.DoExceptionCallback(E);
				}
				
				
			}
			Trace("TaskStopped : " + ti.Name);
		}
		

		class TimerInfo
		{
			public string Name;
			public object State;
			public TimerCallback Callback;
			public int Period;			
			
			public TimerInfo(string timerName, object timerState, TimerCallback callback, int period)
			{
				Name = timerName;
				State = timerState;
				Callback = callback;
				Period = period;
			}

			public void DoCallback()
			{
				if(Callback != null)
					Callback(State);
			}
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String)")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
		public static void QueueTimer(string timerName, TimerCallback callback, object state, int dueTime, int period)
		{
			Trace("QueueTimer : " + timerName);						
			Hashtable syncedTimersHash = Hashtable.Synchronized( TimersHash );
			if(syncedTimersHash.Contains(timerName))
			{
				throw new System.ArgumentException("The timer + " + timerName + " already queued");
			}
			TimerInfo ti = new TimerInfo(timerName, state, callback, period);
			Timer timer = new Timer(new TimerCallback(TimerProc), ti, dueTime, period);
			syncedTimersHash[timerName] = timer;
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.ArgumentException.#ctor(System.String)")]
		public static void StopTimer(string timerName)
		{
			Trace("StopTimer : " + timerName);						
			Hashtable syncedTimersHash = Hashtable.Synchronized( TimersHash );
			if(!syncedTimersHash.Contains(timerName))
			{
				throw new System.ArgumentException("The timer + " + timerName + " don't queued");
			}
			Timer timer = (Timer)syncedTimersHash[timerName];
			syncedTimersHash.Remove(timerName);
			timer.Dispose();
		}
		
		static Hashtable TimersHash = new Hashtable();

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		static void TimerProc(Object stateInfo) 
		{
			TimerInfo ti = (TimerInfo) stateInfo;
			int start = Environment.TickCount;
			//Trace("TimerStarted : " + ti.Name);
			try
			{
				ti.DoCallback();
			}
			catch(Exception E)
			{
				FreeCL.RTL.Trace.TraceException(E);
			}
			
			
			//Trace("TimerStopped : " + ti.Name);
			int end = Environment.TickCount;
			int process_time = 0;
			if(end >= start)
				process_time = end - start;
			else
				process_time = Int32.MaxValue - start + end;
					
			if(process_time > ti.Period)
			{
				Trace("TimerOverload : " + ti.Name + " , required time " + ti.Period.ToString(CultureInfo.InvariantCulture) + "msec, executing time " + process_time.ToString(CultureInfo.InvariantCulture) + " msec" );
			}
		}
		
		
		
	}
}
