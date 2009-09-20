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
using System.IO.IsolatedStorage;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace FreeCL.UI
{
	/// <summary>
	/// Description of Application.	
	/// </summary>
	[DesignTimeVisibleAttribute(false)] 
	public class Application : System.ComponentModel.Component
	{
		private System.ComponentModel.IContainer components;		

		/// <summary>
		/// Return Active form independent of Mdi\Sdi stuff 
		/// </summary>
		[Browsable(false)]
		public static System.Windows.Forms.Form ActiveForm
		{
			get
			{
				System.Windows.Forms.Form activeForm = null;
				try //monobug
				{
					activeForm = System.Windows.Forms.Form.ActiveForm;
				}
				catch{}
					
				if(activeForm == null)
					return null;
			
				//check mdi
				if(activeForm.IsMdiContainer) 
					activeForm = activeForm.ActiveMdiChild;
			
				return activeForm;
			}
		}

		/// <summary>
		/// Return Active control for application
		/// </summary>
		[Browsable(false)]
		public static System.Windows.Forms.Control ActiveControl
		{
			get
			{
		
				System.Windows.Forms.IContainerControl active = FreeCL.UI.Application.ActiveForm;
				
				if(active == null)
					return null;
			
				while(active.ActiveControl != null && active.ActiveControl is IContainerControl)
					active = active.ActiveControl as IContainerControl;	
				
				if(active.ActiveControl != null)
					return active.ActiveControl;
				else
					return active as System.Windows.Forms.Control;
			}
			set
			{
				Trace.WriteLine("Activate" + value);
				if(value != null) 
				{
					System.Windows.Forms.Form form = value.FindForm();			
					
					if(form == null)
						return;
					
					if(form.MdiParent != null)
					{
						if(form.MdiParent.ActiveMdiChild != form)
							form.Activate();						
					}
					else
						form.Activate();						

					value.Select();
				}
			}
		}
		
		
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId="System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon,System.Windows.Forms.MessageBoxDefaultButton)")]
		public static void ShowException(Exception exception)
		{
			if(exception == null)
				return;
				
			FreeCL.RTL.Trace.TraceException(exception);
			System.Windows.Forms.MessageBox.Show(exception.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
		}
		
		/// <summary>
		/// Seek type in all assemblies of domain
		/// </summary>
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		static public Type GetType(string name)
		{
			bool throwOnError = false;
			bool ignoreCase = false;
			if (name == null || name.Length == 0) {
				return null;
			}
			Assembly lastAssembly = null;
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) 
			{
				Type t = asm.GetType(name, throwOnError);
				if (t != null) 
				{
					lastAssembly = asm;
				}
			}
			
			if (lastAssembly != null) 
			{
				return lastAssembly.GetType(name, throwOnError, ignoreCase);
			}
			
			Type type = Type.GetType(name, throwOnError, ignoreCase);
			
			// type lookup for typename, assembly, xyz style lookups
			if (type == null) 
			{
				int idx = name.IndexOf(",");
				if (idx > 0) 
				{
					string[] splitName = name.Split(',');
					string typeName		 = splitName[0];
					string assemblyName = splitName[1].Substring(1);
					Assembly assembly = null;
					try 
					{
						assembly = Assembly.Load(assemblyName);
					} 
					catch
					{
					}
					
					if (assembly != null) 
					{
						type = assembly.GetType(typeName, throwOnError, ignoreCase);
					} 
					else
					{
						type = Type.GetType(typeName, throwOnError, ignoreCase);
					}
				}
			}
			
			return type;
		}
		
		
		public Application(System.ComponentModel.IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException("container");
		
			container.Add(this);
			InitializeComponent();
		}
		

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]		
		private static System.Windows.Forms.ApplicationContext Context_; 
		
		public static System.Windows.Forms.Form MainForm
		{
			get
			{
				return Context_ != null ? Context_.MainForm : null;
			}
		}
		public static void Run(System.Windows.Forms.Form form)
		{
			Context_ = new ApplicationContext(form);
			System.Windows.Forms.Application.Run(Context_);	
		}
		

		public Application()
		{
			InitializeComponent();
		}
		
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]		
		static Application()
		{
			string configFileName =	CommandLineHelper.GetCommandSwitchValue("config");
			
			if(string.IsNullOrEmpty(configFileName))
			{
				dataFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				dataFolder += Path.DirectorySeparatorChar;
				string companyName = ApplicationInfo.CompanyName;
				if(companyName.IndexOf(",") != -1)
					companyName = companyName.Substring(0, companyName.IndexOf(","));
				dataFolder += companyName;
				dataFolder += Path.DirectorySeparatorChar;			
				dataFolder += ApplicationInfo.ProductName;
				System.IO.Directory.CreateDirectory(dataFolder);
				dataFolder += Path.DirectorySeparatorChar;			
			}
			else
			{
				dataFolder = System.IO.Path.GetDirectoryName(configFileName);
				try 
				{
					System.IO.Directory.CreateDirectory(dataFolder);
					dataFolder += Path.DirectorySeparatorChar;			
				} catch {}
			}
			
			
		}
		
		private static string dataFolder;
		
		public static string DataFolder
		{
			get{return dataFolder;}
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
		}
		#endregion
		
		
		protected override void Dispose( bool disposing )
		{
			try
			{
				if( disposing )
				{
					Memory.DisposeAndNull(ref components);
					
					if(components != null)
					{
						components.Dispose();
						components = null;
					}
					
				}
			}
			finally
			{
				base.Dispose( disposing );			
			}

		}
		
		
		public static void DoEvents()
		{
			System.Windows.Forms.Application.DoEvents();
		}
		
		
	}
}
