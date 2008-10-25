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
using System.ComponentModel;
using System.Windows.Forms;
using FreeCL.RTL;
using System.Reflection;
using System.Threading;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of Application.	
	/// </summary>
	[DesignTimeVisibleAttribute(false)] 
	public class Application : FreeCL.UI.Application
	{


		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		static Application()
		{
			System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(GlobalEventsThreadException);
		
			try
			{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			}
			catch{}
			LangPack.TranslateString(""); //init language
			SkipSplashForm = CommandLineHelper.IsCommandSwitchSet("skipsplash");
		}


		static void GlobalEventsThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			OnThreadException(e.Exception);
		}
		
		public static void OnThreadException(object exception)
		{
			 System.Exception e = exception as System.Exception;
			 if(e == null) return;
				 
			 if(e.GetType().FullName == "FreeCL.RTL.AbortException") 
				 return;
			 FreeCL.RTL.Trace.TraceException(e);
			 FreeCL.Forms.ExceptionDialog.ShowException(e);
		}

		static string waitMessage;
		public static bool QueueGuiLockingTask(string taskName, WaitCallback callback, object state)
		{
			if(callback == null)
				throw new ArgumentNullException("callback");
		
			if(MainForm == null)
			{ //execute directly
				callback(state);
				return true;
			}

			if(LockedGUIForm == null)
			{
				LockedGUIForm = new FreeCL.Forms.LockedGuiForm();
				LockedGUIForm.Init();
			}
			
			waitMessage = taskName;
			return TaskConveyer.QueueExtTask(
				taskName, 
				callback, 
				state, 
				new WaitCallback(OnStartGuiLockingTask), 
				new WaitCallback(OnEndGuiLockingTask),
				new WaitCallback(OnExceptionInGuiLockingTask)
			 );
		}
		
		public static void StartGuiLockingTask(string waitMess)
		{
			if(LockedGUIForm == null)
			{
				LockedGUIForm = new FreeCL.Forms.LockedGuiForm();
				LockedGUIForm.Init();
			}
			
			waitMessage = waitMess;
			OnStartGuiLockingTask(null);
		}
		
		static int LockCounter;
		static Cursor savedCursor; 
		static void OnStartGuiLockingTask(Object stateInfo)
		{
			
			
			if(LockCounter == 0 && LockedGUIForm != null)
			{
				savedCursor = MainForm.Cursor;
				MainForm.Cursor = Cursors.WaitCursor; 			
				
				
				LockedGUIForm.StartWaiting(waitMessage);
				
			}
			LockCounter++;				
		}
		
		static FreeCL.Forms.LockedGuiForm LockedGUIForm;
		
		static void OnEndGuiLockingTask(Object stateInfo)
		{
			if(LockCounter == 1 && LockedGUIForm != null)
			{
				MainForm.Cursor = savedCursor;				
				LockedGUIForm.StopWaiting();
				LockedGUIForm = null;
			}
			LockCounter--;							
		}
		
		public static void EndGuiLockingTask()
		{
			OnEndGuiLockingTask(null);
		}

		static void OnExceptionInGuiLockingTask(Object stateInfo)
		{
			Exception e = stateInfo as Exception;
			if(e.GetType().FullName == "FreeVCL.Components.AbortException") 
				 return;
		 
			FreeCL.Forms.ExceptionDialog.ShowException(e);			
		}
		
		private static BaseSplashForm SplashForm_;
		public static BaseSplashForm SplashForm
		{
			get{return SplashForm_;}
			set{SplashForm_ = value;}
		}

		private static BaseAboutForm aboutForm;
		public static BaseAboutForm AboutForm
		{
			get
			{
				if(aboutForm == null)
					aboutForm = new BaseAboutForm();
				return aboutForm;
			}
			set{aboutForm = value;}
		}
		
		public static void ShowAboutForm()
		{
			AboutForm.ShowDialog(MainForm);
		}
		
		public static void ShowOptionsForm()
		{
			(OptionsForm as Form).ShowDialog(MainForm);
		}
		
		private static bool SkipSplashForm_;
		
		public static bool SkipSplashForm
		{
			get{return SkipSplashForm_;}
			set{SkipSplashForm_ = value;}
		}
		
		private static IOptionsForm OptionsForm_;		
		
		public static IOptionsForm OptionsForm
		{
			get{return OptionsForm_;}
			set{OptionsForm_ = value;}
		}
		
		private static BaseOptions Options_;
		
		public static BaseOptions BaseOptions
		{
			get{return Options_;}
			set{Options_ = value;}
		}
		
		
		private static bool Initialized_;


		public static void Initialize()
		{
			Initialize(null, null, null); 
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.InvalidOperationException.#ctor(System.String)")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="FreeCL.Forms.OptionsControlsManager.RegisterOptionControl(System.Type,System.String,System.Int32,System.String,System.Int32)")]
		public static void Initialize(BaseSplashForm splashForm, BaseOptions options, BaseOptionsForm optionsForm)
		{
			
			if(Initialized_)		
				throw new System.InvalidOperationException("DBApplication already initialized");

			if(optionsForm != null && OptionsForm_ == null)
				OptionsForm_ = optionsForm;
			
			if(OptionsForm_ == null)
				throw new System.InvalidOperationException("OptionsForm should be setuped by Initialize or OptionsForm property");
				
			OptionsControlsManager.RegisterOptionControl(typeof(UILanguageOptionsControl), "General", 0, "UI Language", 0);
			
			
			if(options == null && Options_ == null)
				Options_ = new BaseOptions();
			else if(Options_ == null)
				Options_ = options;
			
			if(Options_ == null)
				throw new System.InvalidOperationException("Options should be setuped by Initialize or Options property");
			
		
			if(SplashForm_ == null && splashForm == null && !SkipSplashForm_)
				splashForm = new FreeCL.Forms.BaseSplashForm();

			if(splashForm != null)
			{
				splashForm.Show();
				System.Windows.Forms.Application.DoEvents();				
				SplashForm_ = splashForm;
			}
			
			Options_ = BaseOptions.Load(Options_);		 
			
			Initialized_ = true;
			System.Windows.Forms.Application.DoEvents();
		}


		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void ShowSplashForm(BaseSplashForm splashForm)
		{
			try
			{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			}
			catch{}
			
			if(splashForm == null && !SkipSplashForm_)
				splashForm = new FreeCL.Forms.BaseSplashForm();

			if(splashForm != null)
			{
				splashForm.Show();
				System.Windows.Forms.Application.DoEvents();				
				SplashForm_ = splashForm;
			}
		}
		public static new void Run(System.Windows.Forms.Form form)
		{
			if(SplashForm_ != null)
				SplashForm_.Dispose();
			
			FreeCL.UI.Application.Run(form);	
			Options_.Save(); 
		}

		
	
	}
}
