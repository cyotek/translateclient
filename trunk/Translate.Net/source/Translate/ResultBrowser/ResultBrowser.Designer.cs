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


namespace Translate
{
	partial class ResultBrowser
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
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.wBrowser = new System.Windows.Forms.WebBrowser();
			this.wAdvertBrowser = new System.Windows.Forms.WebBrowser();
			this.gEvents = new FreeCL.UI.GlobalEvents(this.components);
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// wBrowser
			// 
			this.wBrowser.IsWebBrowserContextMenuEnabled = false;
			this.wBrowser.Location = new System.Drawing.Point(0, 0);
			this.wBrowser.Margin = new System.Windows.Forms.Padding(0);
			this.wBrowser.MinimumSize = new System.Drawing.Size(20, 0);
			this.wBrowser.Name = "wBrowser";
			this.wBrowser.ScrollBarsEnabled = false;
			this.wBrowser.Size = new System.Drawing.Size(391, 144);
			this.wBrowser.TabIndex = 1;
			this.wBrowser.WebBrowserShortcutsEnabled = false;
			this.wBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WBrowserNavigating);
			// 
			// wAdvertBrowser
			// 
			this.wAdvertBrowser.Location = new System.Drawing.Point(0, 157);
			this.wAdvertBrowser.Margin = new System.Windows.Forms.Padding(0);
			this.wAdvertBrowser.MinimumSize = new System.Drawing.Size(20, 0);
			this.wAdvertBrowser.Name = "wAdvertBrowser";
			this.wAdvertBrowser.ScrollBarsEnabled = false;
			this.wAdvertBrowser.Size = new System.Drawing.Size(391, 44);
			this.wAdvertBrowser.TabIndex = 2;
			this.wAdvertBrowser.WebBrowserShortcutsEnabled = false;
			this.wAdvertBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WBrowserNavigating);
			this.wAdvertBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WAdvertBrowserDocumentCompleted);
			// 
			// gEvents
			// 
			this.gEvents.Idle += new System.EventHandler(this.GEventsIdle);
			// 
			// vScrollBar
			// 
			this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar.Enabled = false;
			this.vScrollBar.LargeChange = 50;
			this.vScrollBar.Location = new System.Drawing.Point(357, 0);
			this.vScrollBar.Maximum = 50;
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(17, 184);
			this.vScrollBar.SmallChange = 30;
			this.vScrollBar.TabIndex = 3;
			this.vScrollBar.Visible = false;
			this.vScrollBar.ValueChanged += new System.EventHandler(this.VScrollBarValueChanged);
			// 
			// ResultBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.wBrowser);
			this.Controls.Add(this.wAdvertBrowser);
			this.Name = "ResultBrowser";
			this.Size = new System.Drawing.Size(374, 184);
			this.Load += new System.EventHandler(this.ResultBrowserLoad);
			this.Resize += new System.EventHandler(this.ResultBrowserResize);
			this.SizeChanged += new System.EventHandler(this.ResultBrowserSizeChanged);
			this.ClientSizeChanged += new System.EventHandler(this.ResultBrowserClientSizeChanged);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.VScrollBar vScrollBar;
		private FreeCL.UI.GlobalEvents gEvents;
		private System.Windows.Forms.WebBrowser wAdvertBrowser;
		private System.Windows.Forms.WebBrowser wBrowser;
	}
}
