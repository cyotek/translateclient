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
using System.Net; 
using System.Text; 
using System.IO; 
using System.Web; 
using System.IO.Compression;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;


namespace Translate
{
	/// <summary>
	/// Description of SlovnykNetService.
	/// </summary>
	
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Slovnyk")]
	public class SlovnykNetService : Service
	{
		public SlovnykNetService()
		{
			Url = new Uri("http://www.slovnyk.net");
			Name = "slovnyk_net_th";
			CompanyName = "��� \"�����\" - VTF \"Perun\"";
			Copyright = "Copyright � 2005, VTF Perun, � 2006-2008, Oleg Dmitriev";
			IconUrl = new Uri("http://www.slovnyk.net/favicon.ico");
			FullName = "Large explanatory dictionary of modern Ukrainian";
			MonolingualDictionary tr = new SlovnykNetDictionary();
			tr.Service = this;
			AddMonolingualDictionary(tr);
		}
		
		
	}
	
}
