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
using System.Drawing;
using System.Windows.Forms;

namespace Translate
{
	/// <summary>
	/// Description of SetupAnalyticsForm.
	/// </summary>
	public partial class SetupAnalyticsForm : FreeCL.Forms.BaseForm
	{
		public SetupAnalyticsForm()
		{
			InitializeComponent();
			RegisterLanguageEvent(OnLanguageChanged);
		}
		
		void OnLanguageChanged()
		{
			Text = TranslateString("Usage information collecting");
			bYes.Text = TranslateString("Yes");
			bNo.Text = LangPack.TranslateString("Cancel");
			lTop.Text = TranslateString("Do you want to allow collecting of basic information about application usage ?");
			gbWill.Text = TranslateString("Tool will");
			gbWillNot.Text = TranslateString("Tool will not");
			lWill.Text = TranslateString("Collect anonymous information about tool usage : tool running time, tool version, OS version.");
			lWillNot.Text = TranslateString("Collect your name, address, or any other personally identifiable information. The information collected is anonymous.");
			llAlreadyCollected.Text = TranslateString("You can check already collected data here");
			lDisable.Text = TranslateString("You can disable data collecting at any time in \"Options\" dialog");
		}		
		
		
		void BOkClick(object sender, EventArgs e)
		{
			TranslateOptions.Instance.UseEQATECMonitor = true;
		}
		
		void BNoClick(object sender, EventArgs e)
		{
			TranslateOptions.Instance.UseEQATECMonitor = false;
		}
		
		void LlAlreadyCollectedLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			HtmlHelper.OpenUrl(new Uri("http://analytics.eqatec.com/?publicid=OhIqyKbbUsbqLcjo"));
		}
	}
}
