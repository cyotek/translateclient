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
using System.Diagnostics.CodeAnalysis;


[module: SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Scope="namespace", Target="Translate.r2uOrgUa")]
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope="namespace", Target="Translate.r2uOrgUa")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope="namespace", Target="Translate.r2uOrgUa")]

namespace Translate.r2uOrgUa
{
	
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
	public class R2uOrgUaService : Service
	{
		public R2uOrgUaService()
		{
			Url = new Uri("http://r2u.org.ua");
			Name = "r2u_org_ua_dic";
			CompanyName = "Andriy Rysin";
			Copyright = "Andriy Rysin, KIS, http://r2u.org.ua/krym/sources.php";
			IconUrl = new Uri("http://r2u.org.ua/img/r2u.png");
			
			BilingualDictionary tr = new R2uOrgUaDictionary();
			tr.Service = this;
			AddBilingualDictionary(tr);
			
		}
	}
	
}
