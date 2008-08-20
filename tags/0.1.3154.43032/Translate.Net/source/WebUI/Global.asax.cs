
using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace WebUI
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : HttpApplication
	{
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		#region global
		/// <summary>
		/// Required designer variable.
		/// </summary>
		//private System.ComponentModel.IContainer components = null;
		
		static WebServerGate webServerGate;
		public static WebServerGate WebServerGate {
			get { return webServerGate; }
		}

		public Global()
		{
			InitializeComponent();
			if(webServerGate == null)
				webServerGate = AppDomain.CurrentDomain.GetData ("WebUIGate") as WebServerGate;
		}

		#endregion
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

		protected void Application_Start(Object sender, EventArgs e)
		{

		}

		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		// This method determines whether request came from the same IP address as the server
		public static bool IsLocalRequest(HttpRequest request)
		{
			//allow only loopback interface
		    return 
		    	"127.0.0.1" == request.ServerVariables["REMOTE_ADDR"] &&
		    		request.ServerVariables["LOCAL_ADDR"] == request.ServerVariables["REMOTE_ADDR"];
		}
		
		// this is one of existing Global.asax methods
		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
		    HttpApplication app = sender as HttpApplication;
		    if(!IsLocalRequest(app.Context.Request))
		        throw new HttpException(403, "Remote access is prohibited");
		}		

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
