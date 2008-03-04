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
using System.IO;
using System.Text;
using ulif;
using System.Net; 

namespace Translate
{
	/// <summary>
	/// Description of UlifHelper.
	/// </summary>
	internal static class UlifHelper
	{
		static bool isVersionSupported;
		static bool isVersionChecked;
		static object lock_obj = new Object();
		
		static void CheckVersion(ulif.dictlib service)
		{
			lock(lock_obj)
			{
				if(!isVersionChecked)
				{
					string[] res;
					if(service.checkver("1.0.3.28", out res))
						isVersionSupported = true;
					isVersionChecked = true;
				}
				if(!isVersionSupported)
					throw new TranslationException("Service version not supported");
			}
		}

		static ulif.dictlib GetService(NetworkSetting networkSetting)
		{
			ulif.dictlib service = new ulif.dictlib();
			service.Proxy = networkSetting.Proxy;
			service.Timeout = networkSetting.Timeout;
			service.EnableDecompression = true;
			service.Credentials = CredentialCache.DefaultCredentials;
			service.UserAgent = "Mozilla/5.0, Translate.Net";
			return service;
		}
		
		public static string GetSynonymsPage(string word, NetworkSetting networkSetting)
		{
			
			ulif.dictlib service = GetService(networkSetting);
			CheckVersion(service);
			bool found;
			int word_idx = service.SearchWord(word, dic.SYN_DIC, true, out found);
			if(!found)
				return "";
			int word_uid = service.ReestrGetID(word_idx, dic.SYN_DIC, true);
			//byte[] buffer = service.DictPrepare(word_uid, "", "style2_2.css", dic.SYN_DIC, true);
			//return Encoding.Unicode.GetString(buffer);
			return service.DictPrepare2(word_uid, "", "style2_2.css", dic.SYN_DIC, true);
		}
	}
}
