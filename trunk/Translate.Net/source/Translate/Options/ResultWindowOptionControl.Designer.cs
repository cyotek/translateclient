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
 
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
 
namespace Translate
{
	partial class ResultWindowOptionControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>

		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		[SuppressMessage("Microsoft.Usage", "CA2204:LiteralsShouldBeSpelledCorrectly", MessageId="Autorun")]
		private void InitializeComponent()
		{
			this.cbShowStatistics = new System.Windows.Forms.CheckBox();
			this.cbMarkErrors = new System.Windows.Forms.CheckBox();
			this.cbHideWithoutResult = new System.Windows.Forms.CheckBox();
			this.cbShowTranslationDirection = new System.Windows.Forms.CheckBox();
			this.cbShowAccents = new System.Windows.Forms.CheckBox();
			this.cbShowServiceName = new System.Windows.Forms.CheckBox();
			this.pBody.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBody
			// 
			this.pBody.Controls.Add(this.cbShowServiceName);
			this.pBody.Controls.Add(this.cbShowAccents);
			this.pBody.Controls.Add(this.cbShowTranslationDirection);
			this.pBody.Controls.Add(this.cbHideWithoutResult);
			this.pBody.Controls.Add(this.cbMarkErrors);
			this.pBody.Controls.Add(this.cbShowStatistics);
			// 
			// cbShowStatistics
			// 
			this.cbShowStatistics.Location = new System.Drawing.Point(13, 103);
			this.cbShowStatistics.Name = "cbShowStatistics";
			this.cbShowStatistics.Size = new System.Drawing.Size(373, 24);
			this.cbShowStatistics.TabIndex = 1;
			this.cbShowStatistics.Text = "Show query time and other information";
			this.cbShowStatistics.UseVisualStyleBackColor = true;
			// 
			// cbMarkErrors
			// 
			this.cbMarkErrors.Location = new System.Drawing.Point(13, 133);
			this.cbMarkErrors.Name = "cbMarkErrors";
			this.cbMarkErrors.Size = new System.Drawing.Size(373, 24);
			this.cbMarkErrors.TabIndex = 2;
			this.cbMarkErrors.Text = "Mark by red color words";
			this.cbMarkErrors.UseVisualStyleBackColor = true;
			// 
			// cbHideWithoutResult
			// 
			this.cbHideWithoutResult.Location = new System.Drawing.Point(13, 163);
			this.cbHideWithoutResult.Name = "cbHideWithoutResult";
			this.cbHideWithoutResult.Size = new System.Drawing.Size(373, 24);
			this.cbHideWithoutResult.TabIndex = 3;
			this.cbHideWithoutResult.Text = "Don\'t show \"Nothing found\" results";
			this.cbHideWithoutResult.UseVisualStyleBackColor = true;
			// 
			// cbShowTranslationDirection
			// 
			this.cbShowTranslationDirection.Location = new System.Drawing.Point(13, 13);
			this.cbShowTranslationDirection.Name = "cbShowTranslationDirection";
			this.cbShowTranslationDirection.Size = new System.Drawing.Size(373, 24);
			this.cbShowTranslationDirection.TabIndex = 4;
			this.cbShowTranslationDirection.Text = "Show direction of translation";
			this.cbShowTranslationDirection.UseVisualStyleBackColor = true;
			// 
			// cbShowAccents
			// 
			this.cbShowAccents.Location = new System.Drawing.Point(13, 43);
			this.cbShowAccents.Name = "cbShowAccents";
			this.cbShowAccents.Size = new System.Drawing.Size(373, 24);
			this.cbShowAccents.TabIndex = 5;
			this.cbShowAccents.Text = "Show accents";
			this.cbShowAccents.UseVisualStyleBackColor = true;
			// 
			// cbShowServiceName
			// 
			this.cbShowServiceName.Location = new System.Drawing.Point(13, 73);
			this.cbShowServiceName.Name = "cbShowServiceName";
			this.cbShowServiceName.Size = new System.Drawing.Size(373, 24);
			this.cbShowServiceName.TabIndex = 6;
			this.cbShowServiceName.Text = "Show names of services";
			this.cbShowServiceName.UseVisualStyleBackColor = true;
			// 
			// ResultWindowOptionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "ResultWindowOptionControl";
			this.pBody.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox cbShowAccents;
		private System.Windows.Forms.CheckBox cbShowTranslationDirection;
		private System.Windows.Forms.CheckBox cbShowServiceName;
		private System.Windows.Forms.CheckBox cbHideWithoutResult;
		private System.Windows.Forms.CheckBox cbMarkErrors;
		private System.Windows.Forms.CheckBox cbShowStatistics;
	}
}
