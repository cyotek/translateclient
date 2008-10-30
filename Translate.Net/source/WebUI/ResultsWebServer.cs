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
using Mono.WebServer;
using System.Net;
using System.Reflection;
using System.IO;

namespace WebUI
{
	/// <summary>
	/// Description of ResultsWebServer.
	/// </summary>
	public static class ResultsWebServer 
	{
		static ResultsWebServer()
		{
			webServerGate = new WebServerGate();
		}
		
		static WebServerGate webServerGate;
		public static WebServerGate WebServerGate {
			get { return webServerGate; }
		}
		
		
		public static void Start()
		{
			XSPWebSource websource=new XSPWebSource(IPAddress.Loopback,0);
			ApplicationServer WebAppServer=new ApplicationServer(websource);

			string basePath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
			string serverPath = basePath + "\\WebUI";
			string serverBinPath = serverPath + "\\bin\\";
			WebAppServer.AddApplication("",-1,"/", serverPath);
			
			try 
			{
				WebAppServer.Start(true);
			} 
			catch (System.Net.Sockets.SocketException e)
			{
				if(e.ErrorCode == 10049)
				{  
					//strange error on bind, probably network still not started
					//try to rerun server
					System.Threading.Thread.Sleep(10000); 	
					WebAppServer.Start(true);
				}
				else
					throw;
			}
			
			
			//copy Mono.WebServer2.dll
			/*try
			{
				if (!Directory.Exists(serverBinPath))
					Directory.CreateDirectory(serverBinPath);

				File.Copy(basePath + "\\Mono.WebServer2.dll", serverBinPath + "Mono.WebServer2.dll", true);
			}

    		catch { ;}			
    		*/
			
			AppDomain ap = WebAppServer.GetApplicationForPath("", WebAppServer.Port, "/", false).AppHost.Domain;
			ap.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
			ap.UnhandledException += OnUnhandledExceptionEvent;
			
			
			ap.SetData("WebUIGate", webServerGate);
			
			port = WebAppServer.Port;
			uri = new Uri("http://127.0.0.1:" + port.ToString() + "/");

		}
			
		public static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args) 
		{
		    return null;
		}
		
		public static event UnhandledExceptionEventHandler UnhandledException;
		
		public static void OnUnhandledExceptionEvent(object sender, UnhandledExceptionEventArgs e)
		{
			if(UnhandledException != null)
				UnhandledException(sender, e);
		}
		
		static int port;
		public static int Port
		{
			get
			{
				return port;
			}
		}
		
		static Uri uri;
		public static Uri Uri {
			get { return uri; }
		}
	
		public static Uri GetIconUrl(string serviceName)
		{
			return new Uri(uri, "GetIcon.aspx?service=" + serviceName);
		}
	
	}
}
