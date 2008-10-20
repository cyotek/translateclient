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
using System.Reflection;
using FreeCL.RTL;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Translate
{
	/// <summary>
	/// Description of Guesser.
	/// </summary>
	public static class Guesser
	{
		static Guesser()
		{
		}
		
		static bool enabled = true;
		public static bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
		
		public static AsyncGuessState GuessAsync(string phrase, NetworkSetting networkSetting, EventHandler<GuessCompletedEventArgs> guessCompletedHandler)
		{
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(DateTime.Now.Ticks);

			AsyncGuessState state = new AsyncGuessState(phrase, networkSetting, asyncOp, guessCompletedHandler);
			
			WorkerEventHandler workerDelegate = new WorkerEventHandler(GuessWorker);
   			workerDelegate.BeginInvoke(
		        		state,
		        		null,
		        		null);

			return state;	
		}
		
		
		public static void CancelAsync(AsyncGuessState guessState)
		{
			if(guessState == null)
				throw new ArgumentNullException("guessState");
		
		    AsyncOperation asyncOp = guessState.AsyncOperation;
		
		    GuessCompletedEventArgs e =
		        new GuessCompletedEventArgs(
		        guessState,
		        null,
		        true,
		        guessState);
		
		    try 
		    {
				asyncOp.PostOperationCompleted(GuessCompleted, e);
		    } 
		    catch (InvalidOperationException) 
		    { 
		    	
		    }
		    guessState.Canceled = true;
		}
		
		delegate void WorkerEventHandler(AsyncGuessState guessState);
		    
		static GoogleLanguageGuesser googleGuesser = new GoogleLanguageGuesser();
			
		internal static void GuessWorker(
		    AsyncGuessState guessState)
		{
			if(guessState.Canceled) 
				return;
			

			guessState.Result = googleGuesser.Guess(guessState.Phrase, guessState.NetworkSetting);
			
		    GuessCompletedEventArgs e =
		        new GuessCompletedEventArgs(
		        guessState,
		        null,
		        false,
		        guessState);
		
		    try 
		    {	
				guessState.AsyncOperation.PostOperationCompleted(GuessCompleted, e);
		    } 
		    catch (InvalidOperationException) 
		    { 
		    	
		    }
		    guessState.Canceled = true;
		}
		
		static void GuessCompleted(object operationState)
		{
		    GuessCompletedEventArgs e =
		        operationState as GuessCompletedEventArgs;
		
		    e.GuessState.OnGuessCompleted(e);
		}
		
		
	}
	
	public class AsyncGuessState
	{
		public AsyncGuessState(
			string phrase, 
			NetworkSetting networkSetting,
			AsyncOperation asyncOperation, 
			EventHandler<GuessCompletedEventArgs> guessCompletedHandler)
		{
			this.asyncOperation = asyncOperation;
			this.phrase = phrase;
			this.networkSetting = networkSetting;
			GuessCompleted += guessCompletedHandler;
		}
		
		AsyncOperation asyncOperation;
		public AsyncOperation AsyncOperation {
			get { return asyncOperation; }
		}
		
		string phrase;
		public string Phrase {
			get { return phrase; }
		}

		NetworkSetting networkSetting;
		public NetworkSetting NetworkSetting
		{
			get { return networkSetting; }
		}
		
		
		public event EventHandler<GuessCompletedEventArgs> GuessCompleted;
		
		bool canceled = false;
		public bool Canceled {
			get { return canceled; }
			set { canceled = value; }
		}
		
		GuessResult result;
		public GuessResult Result
		{
			get { return result; }
			set { result = value; }
		}
		
		public void OnGuessCompleted(
		    GuessCompletedEventArgs e)
		{
		    if (GuessCompleted != null)
		    {
		        GuessCompleted(this, e);
		    }
		}
		
	}
	
	public class GuessCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GuessCompletedEventArgs(
			AsyncGuessState guessState,
			Exception e,
        	bool canceled,
        	object state) : base(e, canceled, state)
		{
			this.guessState = guessState;
			this.result = guessState.Result;
		}
		
       	AsyncGuessState guessState;
		public AsyncGuessState GuessState {
			get { return guessState; }
		}
		
		GuessResult result;
		public GuessResult Result
		{
			get { return result; }
		}
		
		
	}
	
}
