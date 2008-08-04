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
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Forms;

namespace Translate
{
	/// <summary>
	/// Description of NetworkOptions.
	/// </summary>
	[Serializable()]
	public class NetworkOptions
	{
		public NetworkOptions()
		{
			ServicePointManager.DefaultConnectionLimit = 20;
			ServicePointManager.Expect100Continue = false;
			ServicePointManager.UseNagleAlgorithm = false;
		}
		
		
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public int DefaultConnectionLimit {
			get { return ServicePointManager.DefaultConnectionLimit; }
			set { ServicePointManager.DefaultConnectionLimit = value; }
		}
		
		
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public bool Expect100Continue
		{
			get {return ServicePointManager.Expect100Continue;}
			set {ServicePointManager.Expect100Continue = value; }
		}
		
		
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public bool UseNagleAlgorithm {
			get { return ServicePointManager.UseNagleAlgorithm; }
			set { ServicePointManager.UseNagleAlgorithm = value; }
		}
		
		
		ProxyMode proxyMode = ProxyMode.System;
		public ProxyMode ProxyMode {
			get { return proxyMode; }
			set { proxyMode = value; }
		}
		
		string proxyHost;
		public string ProxyHost {
			get { return proxyHost; }
			set { proxyHost = value; }
		}
		
		int proxyPort;
		public int ProxyPort {
			get { return proxyPort; }
			set { proxyPort = value; }
		}
		
		bool proxyAuthentication;
		public bool ProxyAuthentication {
			get { return proxyAuthentication; }
			set { proxyAuthentication = value; }
		}
		
		string proxyUser;
		public string ProxyUser {
			get { return proxyUser; }
			set { proxyUser = value; }
		}
		
		[XmlIgnore]
		public string ProxyDecryptedPassword 
		{
			get 
			{ 
				if(proxyPassword != "")
				{
					return FreeCL.RTL.CryptoTools.DecryptStringWith3DES(proxyPassword, "NetworkOptions", "SavedPassword"); 
				}
				else 
					return proxyPassword; 
			}
			set 
			{ 
				if(string.IsNullOrEmpty(value))
					proxyPassword = ""; 
				else
					proxyPassword = FreeCL.RTL.CryptoTools.EncryptStringWith3DES(value, "NetworkOptions", "SavedPassword"); 
			}
		}
		
		string proxyPassword = "";
		public string ProxyPassword {
			get { return proxyPassword; }
			set { proxyPassword = value; }
		}
		
		bool proxyNtlmAuthentication;
		public bool ProxyNTLMAuthentication {
			get { return proxyNtlmAuthentication; }
			set { proxyNtlmAuthentication = value; }
		}
		
		string proxyNtlmDomain;
		public string ProxyNTLMDomain {
			get { return proxyNtlmDomain; }
			set { proxyNtlmDomain = value; }
		}
		
		public bool SetProxy(ProxyMode proxyMode, string proxyHost, int proxyPort, 
			bool proxyAuthentication, string proxyUser, string proxyPassword,
			bool ntlmAuthentication, string ntlmDomain)
		{
			this.proxyMode = proxyMode;
			this.proxyHost = proxyHost;
			this.proxyPort = proxyPort;
			this.proxyAuthentication = proxyAuthentication;
			this.proxyUser = proxyUser;
			this.ProxyDecryptedPassword = proxyPassword;
			this.proxyNtlmAuthentication = ntlmAuthentication;
			this.proxyNtlmDomain = ntlmDomain;
			
			return Apply();
		}
		
		public int Timeout {
			get { return networkSetting.Timeout; }
			set	{ networkSetting.Timeout = value; }
		}
		
		[NonSerialized]
		NetworkSetting networkSetting = new NetworkSetting();
		
		//reserved		
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId="service")]
		public NetworkSetting GetNetworkSetting(Service service)
		{
			return networkSetting;
		}
		
		public bool Apply()
		{
			if(proxyMode == ProxyMode.System)
			{
				networkSetting.Proxy = WebRequest.DefaultWebProxy;
			}
			else if(proxyMode == ProxyMode.None)
			{
				networkSetting.Proxy = null;
			}
			else
			{
				try 
				{
					WebProxy proxyObject = new WebProxy("http://" + proxyHost + ":" + proxyPort.ToString(CultureInfo.InvariantCulture) +  "/", true);
					if(proxyAuthentication)
					{
						if(!proxyNtlmAuthentication)
							proxyObject.Credentials = new NetworkCredential(proxyUser, ProxyDecryptedPassword);
						else
							proxyObject.Credentials = new NetworkCredential(proxyUser, ProxyDecryptedPassword, proxyNtlmDomain);
					}
					networkSetting.Proxy = proxyObject;
				} 
				catch (System.UriFormatException) 
				{
					proxyMode = ProxyMode.System;
					string errmessage = LangPack.TranslateString("Format of HTTP-Proxy ({0}) is wrong. Proxy settings will be reset to default.");
					errmessage = string.Format(errmessage, proxyHost);
					MessageBox.Show(errmessage, Constants.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				
			}
			return true;
		}
	}
	
	public enum ProxyMode
	{
		System,
		Custom,
		None
	}
}
