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
using Translate.DictD;

namespace Translate
{
	/// <summary>
	/// Description of DictDUtils.
	/// </summary>
	internal static class DictDUtils
	{
		internal static void DoTranslate(IDictDServiceItem dictServiceItem, string phrase, LanguagePair languagesPair, string subject, Result result, NetworkSetting networkSetting)
		{
			ServiceItem si = dictServiceItem as ServiceItem;
			DictionaryClient dc = null;
			
			try 
			{
				dc = DictDClientsPool.GetPooledClient(dictServiceItem.Urls);
				DefinitionCollection definitions = dc.GetDefinitions(phrase, si.Name);
				string translation;
				if(definitions != null && definitions.Count > 0)
				{
					foreach(Definition df in definitions)
					{
						translation = "html!<div style='width:{allowed_width}px;overflow:scroll;overflow-y:hidden;overflow-x:auto;'><pre>" + df.Description + "&nbsp</pre></div>";
						result.Translations.Add(translation);
					}
				}
				else
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
			} 
			catch(DictionaryServerException e)
			{
				if(e.ErrorCode == 552) //No definitions found
				{
					result.ResultNotFound = true;
					throw new TranslationException("Nothing found");
				}
				else
					throw;
			}
			finally
			{
				if(dc != null)
					DictDClientsPool.PushPooledClient(dc);
			}
		}	
	}
}
