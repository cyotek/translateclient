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
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
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
using System.Windows.Forms;
using System.ComponentModel;
using FreeCL.UI;
using FreeCL.RTL;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.Forms
{
	/// <summary>
	/// Description of BaseOptionsForm.	
	/// </summary>
	public class BaseOptionsForm : FreeCL.Forms.BaseForm, IOptionsForm
	{
		private System.ComponentModel.IContainer components;
		private FreeCL.UI.Panel bOptions;
		private FreeCL.UI.Actions.Action aClose;
		
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public System.Windows.Forms.TreeView tvItems;
		private FreeCL.UI.Panel panel1;
		public BaseOptionsForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
			
		}

		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseOptionsForm));
			this.panel1 = new FreeCL.UI.Panel();
			this.bApply = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.bOk = new System.Windows.Forms.Button();
			this.tvItems = new System.Windows.Forms.TreeView();
			this.aClose = new FreeCL.UI.Actions.Action(this.components);
			this.bOptions = new FreeCL.UI.Panel();
			this.pMain = new FreeCL.UI.Panel();
			this.aApply = new FreeCL.UI.Actions.Action(this.components);
			this.panel1.SuspendLayout();
			this.bOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// il
			// 
			this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
			this.il.Images.SetKeyName(0, "");
			// 
			// al
			// 
			this.al.Actions.Add(this.aClose);
			this.al.Actions.Add(this.aApply);
			// 
			// panel1
			// 
			this.panel1.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.panel1.Controls.Add(this.bApply);
			this.panel1.Controls.Add(this.bCancel);
			this.panel1.Controls.Add(this.bOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(297, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(283, 28);
			this.panel1.TabIndex = 3;
			// 
			// bApply
			// 
			this.al.SetAction(this.bApply, this.aApply);
			this.bApply.ImageList = this.il;
			this.bApply.Location = new System.Drawing.Point(189, 4);
			this.bApply.Name = "bApply";
			this.bApply.Size = new System.Drawing.Size(88, 23);
			this.bApply.TabIndex = 2;
			this.bApply.Text = "Apply";
			this.bApply.UseVisualStyleBackColor = true;
			// 
			// bCancel
			// 
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(95, 4);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(88, 23);
			this.bCancel.TabIndex = 1;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			this.bCancel.Click += new System.EventHandler(this.BCancelClick);
			// 
			// bOk
			// 
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOk.Location = new System.Drawing.Point(1, 4);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(88, 23);
			this.bOk.TabIndex = 0;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			this.bOk.Click += new System.EventHandler(this.BOkClick);
			// 
			// tvItems
			// 
			this.tvItems.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvItems.FullRowSelect = true;
			this.tvItems.HideSelection = false;
			this.tvItems.Location = new System.Drawing.Point(0, 0);
			this.tvItems.Name = "tvItems";
			this.tvItems.Size = new System.Drawing.Size(168, 420);
			this.tvItems.TabIndex = 4;
			this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvItemsAfterSelect);
			// 
			// aClose
			// 
			this.aClose.Checked = false;
			this.aClose.Enabled = true;
			this.aClose.Hint = null;
			this.aClose.ImageIndex = 0;
			this.aClose.Tag = null;
			this.aClose.Text = "Close";
			this.aClose.Visible = true;
			this.aClose.Execute += new System.EventHandler(this.ACloseExecute);
			// 
			// bOptions
			// 
			this.bOptions.AutoUpdateDockPadding = false;
			this.bOptions.Controls.Add(this.panel1);
			this.bOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bOptions.Location = new System.Drawing.Point(0, 420);
			this.bOptions.Name = "bOptions";
			this.bOptions.Padding = new System.Windows.Forms.Padding(2);
			this.bOptions.Size = new System.Drawing.Size(582, 32);
			this.bOptions.TabIndex = 3;
			// 
			// pMain
			// 
			this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pMain.Location = new System.Drawing.Point(168, 0);
			this.pMain.Name = "pMain";
			this.pMain.Padding = new System.Windows.Forms.Padding(5);
			this.pMain.Size = new System.Drawing.Size(414, 420);
			this.pMain.TabIndex = 5;
			// 
			// aApply
			// 
			this.aApply.Checked = false;
			this.aApply.Enabled = true;
			this.aApply.Hint = null;
			this.aApply.Tag = null;
			this.aApply.Text = "Apply";
			this.aApply.Visible = true;
			this.aApply.Execute += new System.EventHandler(this.BApplyClick);
			this.aApply.Update += new System.EventHandler(this.AApplyUpdate);
			// 
			// BaseOptionsForm
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(582, 452);
			this.Controls.Add(this.pMain);
			this.Controls.Add(this.tvItems);
			this.Controls.Add(this.bOptions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "BaseOptionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.VisibleChanged += new System.EventHandler(this.BaseOptionsFormVisibleChanged);
			this.panel1.ResumeLayout(false);
			this.bOptions.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private FreeCL.UI.Actions.Action aApply;
		private FreeCL.UI.Panel pMain;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Button bApply;
		#endregion
		
		void OnLanguageChanged()
		{
			bCancel.Text = LangPack.TranslateString("Cancel");
			aApply.Text = LangPack.TranslateString("Apply");
			Text = LangPack.TranslateString("Options");
			
			foreach(TreeNode rtn in tvItems.Nodes)
			{
				string groupName = (string)rtn.Tag;
				rtn.Text = LangPack.TranslateString(groupName);
				foreach(TreeNode tn in rtn.Nodes)
				{
					OptionControlInfo inf = (OptionControlInfo)tn.Tag;
					inf.Instance.lCaption.Text = LangPack.TranslateString(inf.Caption);
					tn.Text = LangPack.TranslateString(inf.Caption);
				}
			}
		}
		
		
		bool controlsAdded;
		void InitControls()
		{
			if(!controlsAdded)
			{
				tvItems.Nodes.Clear();
				foreach(OptionControlInfo inf in OptionsControlsManager.OptionsControls)
				{
					inf.Instance = (BaseOptionsControl)Activator.CreateInstance(inf.ControlType);
					inf.Instance.Parent = pMain;
					inf.Instance.Dock = DockStyle.Fill;
					inf.Instance.Visible = false;
					inf.Instance.lCaption.Text = LangPack.TranslateString(inf.Caption);
					TreeNode rootNode = null;
					foreach(TreeNode tn in tvItems.Nodes)
					{
						if(inf.Group == (string)tn.Tag)		
						{
							rootNode = tn;
							break;
						}
					}
					if(rootNode == null)
					{
						rootNode = new TreeNode(LangPack.TranslateString(inf.Group));
						rootNode.Tag = inf.Group;
						tvItems.Nodes.Insert(inf.GroupOrder, rootNode);
					}
					TreeNode childNode = new TreeNode(LangPack.TranslateString(inf.Caption));
					childNode.Tag = inf;
					rootNode.Nodes.Insert(inf.Order, childNode);
				}
				controlsAdded = true;
				tvItems.ExpandAll();
				tvItems.SelectedNode = tvItems.Nodes[0];
			}
			
			foreach(OptionControlInfo inf in OptionsControlsManager.OptionsControls)
			{
				inf.Instance.Init();
			}
			
			
		}
		
		
		

		UserControl prevControl;		
		void TvItemsAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(e.Node.Tag.GetType() == typeof(string))
			{
				tvItems.SelectedNode = e.Node.Nodes[0];
			}
			else
			{
				if(prevControl != null)
					prevControl.Visible = false;
				OptionControlInfo inf = (OptionControlInfo)e.Node.Tag;
				inf.Instance.Visible = true;
				inf.Instance.Activate();
				prevControl = inf.Instance;
			}
		}
		
		void ACloseExecute(object sender, System.EventArgs e)
		{
		}
		
		void BaseOptionsFormVisibleChanged(object sender, System.EventArgs e)
		{
			if(DesignMode)	
				return;
			tvItems.ExpandAll();			
			if(Visible)
			{
				InitControls();
				if(OnInitBaseOptionsForm != null)
					OnInitBaseOptionsForm(this, new EventArgs());
			}
			else
			{
				if(OnFinalizeBaseOptionsForm != null)
					OnFinalizeBaseOptionsForm(this, new EventArgs());
			
			}
				
		}

		/// <summary>
 		/// Occurs before options shown
 		/// </summary>
 		[Browsable(true)]
		[Description("Occurs before data shown")]
		[Category("Options")]
		public event EventHandler OnInitBaseOptionsForm;

		/// <summary>
 		/// Occurs after options shown
 		/// </summary>
 		[Browsable(true)]
		[Description("Occurs after data shown")]
		[Category("Options")]
		public event EventHandler OnFinalizeBaseOptionsForm;
		
		
		void BOkClick(object sender, EventArgs e)
		{
			foreach(OptionControlInfo inf in OptionsControlsManager.OptionsControls)
			{
				inf.Instance.Apply();
			}
			Application.BaseOptions.Save();
		}
		
		void BCancelClick(object sender, EventArgs e)
		{
			foreach(OptionControlInfo inf in OptionsControlsManager.OptionsControls)
			{
				inf.Instance.Revert();
			}
		}
		
		void BApplyClick(object sender, EventArgs e)
		{
			foreach(OptionControlInfo inf in OptionsControlsManager.OptionsControls)
			{
				inf.Instance.Apply();
			}
		}
		
		void AApplyUpdate(object sender, EventArgs e)
		{
			bool enabled = false;
			foreach(OptionControlInfo inf in OptionsControlsManager.OptionsControls)
			{
				if(inf.Instance != null && inf.Instance.IsChanged())
				{
					enabled = true;	
					break;
				}
			}
			aApply.Enabled = enabled;
			
		}
	}
}
