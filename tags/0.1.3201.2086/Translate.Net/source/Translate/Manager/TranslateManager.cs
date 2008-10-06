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
using System.Reflection;
using FreeCL.RTL;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	

	/// <summary>
	/// Description of TranslateManager.
	/// </summary>
	public static class TranslateManager
	{
		static void TranslateCompleted(object operationState)
		{
		    TranslateCompletedEventArgs e =
		        operationState as TranslateCompletedEventArgs;
		
		    e.TranslateState.OnTranslateCompleted(e);
		}
		
		internal class ReportProgressState
		{
			public ReportProgressState(
				Result translateResult,
				AsyncTranslateState translateState)
			{
				this.translateResult = translateResult;
				this.translateState = translateState;
			}
			
			public Result translateResult;
			public AsyncTranslateState translateState;
		}
		
		static void ReportProgress(object state)
		{
		    ReportProgressState e =
		        state as ReportProgressState;
		
		    e.translateState.RaiseProgressChanged(e.translateResult);
		}

		public static AsyncTranslateState TranslateAsync(LanguagePair languagePair, string phrase, ReadOnlyServiceSettingCollection translatorsSettings, EventHandler<TranslateProgressChangedEventArgs> progressChangedHandler, EventHandler<TranslateCompletedEventArgs> translateCompletedHandler)
		{
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(DateTime.Now.Ticks);

			AsyncTranslateState state = new AsyncTranslateState(languagePair, translatorsSettings, phrase, asyncOp, progressChangedHandler, translateCompletedHandler);
			
			state.Run();

			return state;	
		}
		
		public static void CancelAsync(AsyncTranslateState translateState)
		{
			if(translateState == null)
				throw new ArgumentNullException("translateState");
		
		    AsyncOperation asyncOp = translateState.AsyncOperation;
		
			translateState.RemoveTimers();
		
		    TranslateCompletedEventArgs e =
		        new TranslateCompletedEventArgs(
		        translateState,
		        null,
		        true,
		        translateState);
		
		    // The asyncOp object is responsible for marshaling 
		    // the call.
		    try 
		    {	//some time operation already completed 
				asyncOp.PostOperationCompleted(TranslateCompleted, e);
		    } 
		    catch (InvalidOperationException) 
		    { 
		    	
		    }
		    translateState.Canceled = true;
		    
		}
		
		    
		internal static void TranslateWorker(
			ServiceItemSetting translatorSetting,
		    AsyncTranslateState translateState)
		{
			if(translateState.Canceled) 
				return;
			
			Result tr = translatorSetting.ServiceItem.Translate(translateState.Phrase, translatorSetting.LanguagePair, translatorSetting.Subject, translatorSetting.NetworkSetting);
			
			ReportProgressState repState = new ReportProgressState(tr,translateState);

			try
			{  //lock errors when 
				translateState.AsyncOperation.Post(ReportProgress, repState);
			}
			catch(InvalidOperationException)
			{
			
			}
		}
	}
	
	public class TranslateProgressChangedEventArgs : ProgressChangedEventArgs
	{
		internal TranslateProgressChangedEventArgs(
			Result translateResult,
			AsyncTranslateState translateState,
			int progressPercentage,
			Object userState
			) : base(progressPercentage, userState)
		{
			this.translateState = translateState;
			this.translateResult = translateResult; 
		}

       	AsyncTranslateState translateState;
		public AsyncTranslateState TranslateState {
			get { return translateState; }
		}
       	
       	Result translateResult;
		public Result TranslateResult {
			get { return translateResult; }
		}
       	
	}
	
	public class TranslateCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal TranslateCompletedEventArgs(
			AsyncTranslateState translateState,
			Exception e,
        	bool canceled,
        	object state) : base(e, canceled, state)
		{
			this.translateState = translateState;
		}
		
       	AsyncTranslateState translateState;
		public AsyncTranslateState TranslateState {
			get { return translateState; }
		}
		
	}
	
	public class AsyncTranslateState
	{
	
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
		internal AsyncTranslateState(
			LanguagePair languagePair,
			ReadOnlyServiceSettingCollection translatorsSettings,
			string phrase, 
			AsyncOperation asyncOp,
			EventHandler<TranslateProgressChangedEventArgs> progressChangedHandler, 
			EventHandler<TranslateCompletedEventArgs> translateCompletedHandler)
		{
			if(translatorsSettings == null)
				throw new ArgumentNullException("translatorsSettings");
			
			this.phrase = phrase;
			this.asyncOperation = asyncOp;
			this.translatorsSettings = translatorsSettings;
			this.languagePair = languagePair;
			count = translatorsSettings.Count;
			
			ProgressChanged += progressChangedHandler;
			TranslateCompleted += translateCompletedHandler;
		}
		
		AsyncOperation asyncOperation;
		public AsyncOperation AsyncOperation {
			get { return asyncOperation; }
		}
		
		LanguagePair languagePair;
		public LanguagePair LanguagePair {
			get { return languagePair; }
			set { languagePair = value; }
		}
		
		bool canceled = false;
		public bool Canceled {
			get { return canceled; }
			set { canceled = value; }
		}
		
		
		string phrase;
		public string Phrase {
			get { return phrase; }
		}
		
		ReadOnlyServiceSettingCollection translatorsSettings;
		public ReadOnlyServiceSettingCollection TranslatorsSettings {
			get { return translatorsSettings; }
		}
		
		ResultCollection results = new ResultCollection();
		public ReadOnlyResultCollection Results {
			get { return new ReadOnlyResultCollection(results); }
		}
		
		Dictionary<ServiceItem, int> useCount = new Dictionary<ServiceItem, int>();
		
		int GetDelayBeforeRun(ServiceItemSetting ts) 
		{
			int cnt = 1;
			if(useCount.TryGetValue(ts.ServiceItem, out cnt))
				cnt++;
				
			useCount[ts.ServiceItem] = cnt;

			int result = ((cnt-1)*1000);

			int koeff = cnt < 10 ? cnt : 10;
			if(cnt < 4)
				koeff = 0;
				
			result *= koeff;
			return result;
		}
		
		void DelayedProcess(object state)
		{
			ServiceItemSetting ts = (ServiceItemSetting)state;
			TranslateManager.TranslateWorker(ts, this);
		}
		
		List<string> timers = new List<string>();
		
		public void RemoveTimers()
		{
			foreach(string name in timers)
			{
				try 
				{
					TaskConveyer.StopTimer(name);
				} catch (Exception) {
					
				}
			}	
		}
		
		delegate void WorkerEventHandler(
			ServiceItemSetting translatorSetting,
		    AsyncTranslateState translateState);
		
		public void Run()
		{
			WorkerEventHandler workerDelegate = new WorkerEventHandler(TranslateManager.TranslateWorker);
			
			int cnt = 0;
			foreach(ServiceItemSetting ts in translatorsSettings)
			{
				cnt++;
				int delay = GetDelayBeforeRun(ts);
				if(delay == 0)
				{
	    			workerDelegate.BeginInvoke(
		        		ts,
		        		this,
		        		null,
		        		null);
	        	}	
	        	else
	        	{
	        		string timerName = ts.ServiceItem.FullName + "_" +  
	        			Environment.TickCount.ToString() + "_" + cnt.ToString();
	        		TaskConveyer.QueueTimer(timerName, DelayedProcess, ts, delay, Timeout.Infinite);
	        		timers.Add(timerName);
	        	}
        	}
		
		}
		
		public event EventHandler<TranslateProgressChangedEventArgs> ProgressChanged;
		
		int count;
		int processed;
		
		[SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]	
		public void RaiseProgressChanged(Result translateResult)
		{
			results.Add(translateResult);
			processed++;
			
			TranslateProgressChangedEventArgs e = new TranslateProgressChangedEventArgs(
				translateResult,
				this,
				(int)(((double)processed)/((double)count)*100),
				this
			);
			
		    if (ProgressChanged != null)
		    {
		        ProgressChanged(this, e);
		    }
		    
		    if(Canceled) return;
		    
		    if(processed == count)
		    {
			    TranslateCompletedEventArgs args =
			        new TranslateCompletedEventArgs(
			        this,
			        null,
			        false,
			        this);
			
				try
				{
			    	asyncOperation.PostOperationCompleted(RaiseTranslateCompleted, args);
			    }
			    catch(System.InvalidOperationException)
			    {
			    	//ignore exception raised sometime from here
			    }
			    Canceled = true;
		    }
		}
	
		public event EventHandler<TranslateCompletedEventArgs> TranslateCompleted;
		
		public void OnTranslateCompleted(
		    TranslateCompletedEventArgs e)
		{
		    if (TranslateCompleted != null)
		    {
		        TranslateCompleted(this, e);
		    }
		}
		
		static void RaiseTranslateCompleted(object operationState)
		{
		    TranslateCompletedEventArgs e =
		        operationState as TranslateCompletedEventArgs;
		
		    e.TranslateState.OnTranslateCompleted(e);
		}
		
		
	}
	
}
