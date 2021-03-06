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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Translate
{
	/// <summary>
	/// Description of ResultWindowOptionControl.
	/// </summary>
	public partial class ResultWindowOptionControl : FreeCL.Forms.BaseOptionsControl
	{
		public ResultWindowOptionControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
		}
		
		void OnLanguageChanged()
		{
			cbShowStatistics.Text = TranslateString("Show query time and other information");
			cbMarkErrors.Text = TranslateString("Mark by red color untranslated words");
			cbHideWithoutResult.Text = TranslateString("Don't show \"Nothing found\" results");
			cbShowTranslationDirection.Text = TranslateString("Show direction of translation");
			cbShowServiceName.Text = TranslateString("Show names of services");
			cbShowAccents.Text = TranslateString("Show accents");
			gbPlacement.Text = TranslateString("Result view placement");
			rbTop.Text = TranslateString("Top");
			toolTip.SetToolTip(rbTop, TranslateString("Place result view at top"));
			rbBottom.Text = TranslateString("Bottom");
			toolTip.SetToolTip(rbBottom, TranslateString("Place result view at bottom"));
			rbLeft.Text = TranslateString("Left");
			toolTip.SetToolTip(rbLeft, TranslateString("Place result view at left"));
			rbRight.Text = TranslateString("Right");
			toolTip.SetToolTip(rbRight, TranslateString("Place result view at right"));
		}

		ResultWindowOptions current;
		public override void Init()
		{
			current = TranslateOptions.Instance.ResultWindowOptions;
			cbShowStatistics.Checked = current.ShowQueryStatistics;
			cbMarkErrors.Checked = current.MarkErrorsWithRed;
			cbHideWithoutResult.Checked = current.HideWithoutResult;
			cbShowTranslationDirection.Checked = current.ShowTranslationDirection;
			cbShowServiceName.Checked = current.ShowServiceName;
			cbShowAccents.Checked = current.ShowAccents;
			rbTop.Checked = current.DockAtTop;
			rbBottom.Checked = !rbTop.Checked;
			rbLeft.Checked = current.DockAtLeft;
			rbRight.Checked =  !rbLeft.Checked;
		}
		
		public override void Apply()
		{
			current.ShowQueryStatistics = cbShowStatistics.Checked;
			current.MarkErrorsWithRed = cbMarkErrors.Checked;
			current.HideWithoutResult = cbHideWithoutResult.Checked; 
			current.ShowTranslationDirection = cbShowTranslationDirection.Checked;
			current.ShowServiceName = cbShowServiceName.Checked;
			current.ShowAccents = cbShowAccents.Checked;
			current.DockAtTop = rbTop.Checked;
			current.DockAtLeft = rbLeft.Checked;
			(TranslateMainForm.Instance as TranslateMainForm).PlaceResultViewVertical(current.DockAtTop);
			(TranslateMainForm.Instance as TranslateMainForm).PlaceResultViewHorizontal(current.DockAtLeft);
		}
		
		public override bool IsChanged()
		{
			return cbShowStatistics.Checked != current.ShowQueryStatistics ||
				current.MarkErrorsWithRed != cbMarkErrors.Checked ||
				current.HideWithoutResult != cbHideWithoutResult.Checked ||
				current.ShowTranslationDirection != cbShowTranslationDirection.Checked ||
				current.ShowServiceName != cbShowServiceName.Checked ||
				current.ShowAccents != cbShowAccents.Checked ||
				current.DockAtTop != rbTop.Checked ||
				current.DockAtLeft != rbLeft.Checked;
		}

	}
}
