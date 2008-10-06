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
using System.Diagnostics;
using FreeCL.UI;
using FreeCL.RTL;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.Forms
{
	using Clipboard = FreeCL.UI.Clipboard;
	
	/// <summary>
	/// Description of MainForm.	
	/// </summary>
	public class MainForm : FreeCL.Forms.BaseMainForm
	{
		private System.ComponentModel.IContainer components;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEditSelectAll;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miExit;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton sbCut;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aEditCut;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEditCopy;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton sbPaste;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEditDel;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aEditCopy;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton sbSelectAll;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aEditRedo;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEditRedo;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEditUndo;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aExit;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miFile;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton sbCopy;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton sbDelete;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEditPaste;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton sbRedo;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEdit;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aEditPaste;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aEditUndo;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miEditCut;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aEditDelete;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aEditSelectAll;
		
		

		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			RegisterLanguageEvent(OnLanguageChanged);
		}
		
		void OnLanguageChanged()
		{
			aEditDelete.Hint = LangPack.TranslateString("Delete selection");
			aEditDelete.Text = LangPack.TranslateString("&Delete");
			aEditUndo.Hint = LangPack.TranslateString("Undo last operation");
			aEditUndo.Text = LangPack.TranslateString("&Undo");
			
			aEditPaste.Hint = LangPack.TranslateString("Paste from clipboard");
			aEditPaste.Text = LangPack.TranslateString("&Paste");
			
			miFile.Text = LangPack.TranslateString("&File");
			
			aExit.Hint = LangPack.TranslateString("Exit from application");
			aExit.Text = LangPack.TranslateString("E&xit");
			
			aEditRedo.Hint = LangPack.TranslateString("Redo last operation");
			aEditRedo.Text = LangPack.TranslateString("&Redo");
			
			aEditCopy.Hint = LangPack.TranslateString("Copy selection to clipboard");
			aEditCopy.Text = LangPack.TranslateString("&Copy");
			
			aEditCut.Hint = LangPack.TranslateString("Cut selection to clipboard");			
			aEditCut.Text = LangPack.TranslateString("Cu&t");

			
			
			aEditSelectAll.Hint = LangPack.TranslateString("Select Àll");
			aEditSelectAll.Text = LangPack.TranslateString("&Select Àll");
			
			miEdit.Text = LangPack.TranslateString("&Edit");

			miHelp.Text = LangPack.TranslateString("&Help");
			aAbout.Text = LangPack.TranslateString("&About ...");
			
			miService.Text = LangPack.TranslateString("&Tools");
			aOptions.Text = LangPack.TranslateString("O&ptions...");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.aEditSelectAll = new FreeCL.UI.Actions.Action(this.components);
			this.aEditDelete = new FreeCL.UI.Actions.Action(this.components);
			this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
			this.aEditUndo = new FreeCL.UI.Actions.Action(this.components);
			this.aEditPaste = new FreeCL.UI.Actions.Action(this.components);
			this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditSep11 = new System.Windows.Forms.ToolStripSeparator();
			this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditDel = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.miEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.miFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miFileSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.aExit = new FreeCL.UI.Actions.Action(this.components);
			this.aEditRedo = new FreeCL.UI.Actions.Action(this.components);
			this.aEditCopy = new FreeCL.UI.Actions.Action(this.components);
			this.aEditCut = new FreeCL.UI.Actions.Action(this.components);
			this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.ptEdit = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.tsEditSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.sbSelectAll = new System.Windows.Forms.ToolStripButton();
			this.tsEditSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.sbCut = new System.Windows.Forms.ToolStripButton();
			this.sbCopy = new System.Windows.Forms.ToolStripButton();
			this.sbPaste = new System.Windows.Forms.ToolStripButton();
			this.sbDelete = new System.Windows.Forms.ToolStripButton();
			this.tsEditSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.sbUndo = new System.Windows.Forms.ToolStripButton();
			this.sbRedo = new System.Windows.Forms.ToolStripButton();
			this.tsEditSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.aAbout = new FreeCL.UI.Actions.Action(this.components);
			this.miService = new System.Windows.Forms.ToolStripMenuItem();
			this.miOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aOptions = new FreeCL.UI.Actions.Action(this.components);
			this.msMain.SuspendLayout();
			this.pToolBars.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.al)).BeginInit();
			this.ptEdit.SuspendLayout();
			this.SuspendLayout();
			
			this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			// 
			// sbMain
			// 
			this.sbMain.Size = new System.Drawing.Size(484, 22);
			// 
			// msMain
			// 
			this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miFile,
									this.miEdit,
									this.miService,
									this.miHelp});
			this.msMain.Size = new System.Drawing.Size(484, 24);
			// 
			// pToolBars
			// 
			this.pToolBars.Controls.Add(this.ptEdit);
			this.pToolBars.Size = new System.Drawing.Size(484, 25);
			// 
			// il
			// 
			this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
			this.il.Images.SetKeyName(0, "");
			this.il.Images.SetKeyName(1, "");
			this.il.Images.SetKeyName(2, "");
			this.il.Images.SetKeyName(3, "");
			this.il.Images.SetKeyName(4, "");
			this.il.Images.SetKeyName(5, "");
			this.il.Images.SetKeyName(6, "Icons.16x16.SelectAlllIcon.png");
			this.il.Images.SetKeyName(7, "");
			this.il.Images.SetKeyName(8, "");
			this.il.Images.SetKeyName(9, "");
			this.il.Images.SetKeyName(10, "");
			this.il.Images.SetKeyName(11, "");
			// 
			// al
			// 
			this.al.Actions.Add(this.aExit);
			this.al.Actions.Add(this.aEditUndo);
			this.al.Actions.Add(this.aEditRedo);
			this.al.Actions.Add(this.aEditCut);
			this.al.Actions.Add(this.aEditCopy);
			this.al.Actions.Add(this.aEditPaste);
			this.al.Actions.Add(this.aEditDelete);
			this.al.Actions.Add(this.aEditSelectAll);
			this.al.Actions.Add(this.aAbout);
			this.al.Actions.Add(this.aOptions);
			// 
			// aEditSelectAll
			// 
			this.aEditSelectAll.Checked = false;
			this.aEditSelectAll.Enabled = true;
			this.aEditSelectAll.Hint = "Select all";
			this.aEditSelectAll.ImageIndex = 6;
			this.aEditSelectAll.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.aEditSelectAll.Tag = null;
			this.aEditSelectAll.Text = "&Select All";
			this.aEditSelectAll.Visible = true;
			this.aEditSelectAll.Execute += new System.EventHandler(this.AEditSelectAllExecute);
			this.aEditSelectAll.Update += new System.EventHandler(this.AEditSelectAllUpdate);
			// 
			// aEditDelete
			// 
			this.aEditDelete.Checked = false;
			this.aEditDelete.Enabled = true;
			this.aEditDelete.Hint = "Delete selection";
			this.aEditDelete.ImageIndex = 2;
			this.aEditDelete.Shortcut = System.Windows.Forms.Keys.Delete;
			this.aEditDelete.Tag = null;
			this.aEditDelete.Text = "&Delete";
			this.aEditDelete.Visible = true;
			this.aEditDelete.Execute += new System.EventHandler(this.AEditDeleteExecute);
			this.aEditDelete.Update += new System.EventHandler(this.AEditDeleteUpdate);
			// 
			// miEditCut
			// 
			this.al.SetAction(this.miEditCut, this.aEditCut);
			this.miEditCut.Image = ((System.Drawing.Image)(resources.GetObject("miEditCut.Image")));
			this.miEditCut.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditCut.MergeIndex = 3;
			this.miEditCut.Name = "miEditCut";
			this.miEditCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.miEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.miEditCut.Size = new System.Drawing.Size(167, 22);
			this.miEditCut.Text = "Cu&t";
			this.miEditCut.ToolTipText = "Cut selection to clipboard";
			// 
			// aEditUndo
			// 
			this.aEditUndo.Checked = false;
			this.aEditUndo.Enabled = true;
			this.aEditUndo.Hint = "Undo last operation";
			this.aEditUndo.ImageIndex = 5;
			this.aEditUndo.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.aEditUndo.Tag = null;
			this.aEditUndo.Text = "&Undo";
			this.aEditUndo.Visible = true;
			this.aEditUndo.Execute += new System.EventHandler(this.AEditUndoExecute);
			this.aEditUndo.Update += new System.EventHandler(this.AEditUndoUpdate);
			// 
			// aEditPaste
			// 
			this.aEditPaste.Checked = false;
			this.aEditPaste.Enabled = true;
			this.aEditPaste.Hint = "Paste from clipboard";
			this.aEditPaste.ImageIndex = 3;
			this.aEditPaste.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.aEditPaste.Tag = null;
			this.aEditPaste.Text = "&Paste";
			this.aEditPaste.Visible = true;
			this.aEditPaste.Execute += new System.EventHandler(this.AEditPasteExecute);
			this.aEditPaste.Update += new System.EventHandler(this.AEditPasteUpdate);
			// 
			// miEdit
			// 
			this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miEditUndo,
									this.miEditRedo,
									this.miEditSep11,
									this.miEditCut,
									this.miEditCopy,
									this.miEditPaste,
									this.miEditDel,
									this.miEditSep2,
									this.miEditSelectAll});
			this.miEdit.MergeIndex = 1;
			this.miEdit.Name = "miEdit";
			this.miEdit.Size = new System.Drawing.Size(37, 20);
			this.miEdit.Text = "&Edit";
			// 
			// miEditUndo
			// 
			this.al.SetAction(this.miEditUndo, this.aEditUndo);
			this.miEditUndo.Image = ((System.Drawing.Image)(resources.GetObject("miEditUndo.Image")));
			this.miEditUndo.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditUndo.MergeIndex = 0;
			this.miEditUndo.Name = "miEditUndo";
			this.miEditUndo.ShortcutKeyDisplayString = "Ctrl+Z";
			this.miEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.miEditUndo.Size = new System.Drawing.Size(167, 22);
			this.miEditUndo.Text = "&Undo";
			this.miEditUndo.ToolTipText = "Undo last operation";
			// 
			// miEditRedo
			// 
			this.al.SetAction(this.miEditRedo, this.aEditRedo);
			this.miEditRedo.Image = ((System.Drawing.Image)(resources.GetObject("miEditRedo.Image")));
			this.miEditRedo.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditRedo.MergeIndex = 1;
			this.miEditRedo.Name = "miEditRedo";
			this.miEditRedo.ShortcutKeyDisplayString = "Ctrl+Y";
			this.miEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.miEditRedo.Size = new System.Drawing.Size(167, 22);
			this.miEditRedo.Text = "&Redo";
			this.miEditRedo.ToolTipText = "Redo last operation";
			// 
			// miEditSep11
			// 
			this.miEditSep11.Name = "miEditSep11";
			this.miEditSep11.Size = new System.Drawing.Size(164, 6);
			// 
			// miEditCopy
			// 
			this.al.SetAction(this.miEditCopy, this.aEditCopy);
			this.miEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("miEditCopy.Image")));
			this.miEditCopy.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditCopy.MergeIndex = 4;
			this.miEditCopy.Name = "miEditCopy";
			this.miEditCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.miEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.miEditCopy.Size = new System.Drawing.Size(167, 22);
			this.miEditCopy.Text = "&Copy";
			this.miEditCopy.ToolTipText = "Copy selection to clipboard";
			// 
			// miEditPaste
			// 
			this.al.SetAction(this.miEditPaste, this.aEditPaste);
			this.miEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("miEditPaste.Image")));
			this.miEditPaste.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditPaste.MergeIndex = 5;
			this.miEditPaste.Name = "miEditPaste";
			this.miEditPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.miEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.miEditPaste.Size = new System.Drawing.Size(167, 22);
			this.miEditPaste.Text = "&Paste";
			this.miEditPaste.ToolTipText = "Paste from clipboard";
			// 
			// miEditDel
			// 
			this.al.SetAction(this.miEditDel, this.aEditDelete);
			this.miEditDel.Image = ((System.Drawing.Image)(resources.GetObject("miEditDel.Image")));
			this.miEditDel.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditDel.MergeIndex = 6;
			this.miEditDel.Name = "miEditDel";
			this.miEditDel.ShortcutKeyDisplayString = "Del";
			this.miEditDel.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.miEditDel.Size = new System.Drawing.Size(167, 22);
			this.miEditDel.Text = "&Delete";
			this.miEditDel.ToolTipText = "Delete selection";
			// 
			// miEditSep2
			// 
			this.miEditSep2.Name = "miEditSep2";
			this.miEditSep2.Size = new System.Drawing.Size(164, 6);
			// 
			// miEditSelectAll
			// 
			this.al.SetAction(this.miEditSelectAll, this.aEditSelectAll);
			this.miEditSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("miEditSelectAll.Image")));
			this.miEditSelectAll.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditSelectAll.MergeIndex = 8;
			this.miEditSelectAll.Name = "miEditSelectAll";
			this.miEditSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
			this.miEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.miEditSelectAll.Size = new System.Drawing.Size(167, 22);
			this.miEditSelectAll.Text = "&Select All";
			this.miEditSelectAll.ToolTipText = "Select all";
			// 
			// miFile
			// 
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miFileSep1,
									this.miExit});
			this.miFile.MergeIndex = 0;
			this.miFile.Name = "miFile";
			this.miFile.Size = new System.Drawing.Size(35, 20);
			this.miFile.Text = "&File";
			// 
			// miFileSep1
			// 
			this.miFileSep1.Name = "miFileSep1";
			this.miFileSep1.Size = new System.Drawing.Size(149, 6);
			// 
			// miExit
			// 
			this.al.SetAction(this.miExit, this.aExit);
			this.miExit.Image = ((System.Drawing.Image)(resources.GetObject("miExit.Image")));
			this.miExit.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miExit.MergeIndex = 1;
			this.miExit.Name = "miExit";
			this.miExit.ShortcutKeyDisplayString = "Alt+F4";
			this.miExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.miExit.Size = new System.Drawing.Size(152, 22);
			this.miExit.Text = "E&xit";
			this.miExit.ToolTipText = "Exit from application";
			// 
			// aExit
			// 
			this.aExit.Checked = false;
			this.aExit.Enabled = true;
			this.aExit.Hint = "Exit from application";
			this.aExit.ImageIndex = 2;
			this.aExit.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.aExit.Tag = null;
			this.aExit.Text = "E&xit";
			this.aExit.Visible = true;
			this.aExit.Execute += new System.EventHandler(this.AExitExecute);
			// 
			// aEditRedo
			// 
			this.aEditRedo.Checked = false;
			this.aEditRedo.Enabled = true;
			this.aEditRedo.Hint = "Redo last operation";
			this.aEditRedo.ImageIndex = 4;
			this.aEditRedo.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.aEditRedo.Tag = null;
			this.aEditRedo.Text = "&Redo";
			this.aEditRedo.Visible = true;
			this.aEditRedo.Execute += new System.EventHandler(this.AEditRedoExecute);
			this.aEditRedo.Update += new System.EventHandler(this.AEditRedoUpdate);
			// 
			// aEditCopy
			// 
			this.aEditCopy.Checked = false;
			this.aEditCopy.Enabled = true;
			this.aEditCopy.Hint = "Copy selection to clipboard";
			this.aEditCopy.ImageIndex = 0;
			this.aEditCopy.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.aEditCopy.Tag = null;
			this.aEditCopy.Text = "&Copy";
			this.aEditCopy.Visible = true;
			this.aEditCopy.Execute += new System.EventHandler(this.AEditCopyExecute);
			this.aEditCopy.Update += new System.EventHandler(this.AEditCopyUpdate);
			// 
			// aEditCut
			// 
			this.aEditCut.Checked = false;
			this.aEditCut.Enabled = true;
			this.aEditCut.Hint = "Cut selection to clipboard";
			this.aEditCut.ImageIndex = 1;
			this.aEditCut.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.aEditCut.Tag = null;
			this.aEditCut.Text = "Cu&t";
			this.aEditCut.Visible = true;
			this.aEditCut.Execute += new System.EventHandler(this.AEditCutExecute);
			this.aEditCut.Update += new System.EventHandler(this.AEditCutUpdate);
			// 
			// miHelp
			// 
			this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miAbout});
			this.miHelp.MergeIndex = 1000;
			this.miHelp.Name = "miHelp";
			this.miHelp.Size = new System.Drawing.Size(40, 20);
			this.miHelp.Text = "Help";
			// 
			// miAbout
			// 
			this.al.SetAction(this.miAbout, this.aAbout);
			this.miAbout.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miAbout.MergeIndex = 1000;
			this.miAbout.Name = "miAbout";
			this.miAbout.Size = new System.Drawing.Size(114, 22);
			this.miAbout.Text = "About";
			// 
			// ptEdit
			// 
			this.ptEdit.Dock = System.Windows.Forms.DockStyle.None;
			this.ptEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.newToolStripButton,
									this.openToolStripButton,
									this.saveToolStripButton,
									this.printToolStripButton,
									this.tsEditSep1,
									this.sbSelectAll,
									this.tsEditSep2,
									this.sbCut,
									this.sbCopy,
									this.sbPaste,
									this.sbDelete,
									this.tsEditSep3,
									this.sbUndo,
									this.sbRedo,
									this.tsEditSep4,
									this.helpToolStripButton});
			this.ptEdit.Location = new System.Drawing.Point(3, 0);
			this.ptEdit.Name = "ptEdit";
			this.ptEdit.Size = new System.Drawing.Size(312, 25);
			this.ptEdit.TabIndex = 7;
			this.ptEdit.Text = "toolStrip1";
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "&New";
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Open";
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Save";
			// 
			// printToolStripButton
			// 
			this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
			this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printToolStripButton.Name = "printToolStripButton";
			this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.printToolStripButton.Text = "&Print";
			// 
			// tsEditSep1
			// 
			this.tsEditSep1.Name = "tsEditSep1";
			this.tsEditSep1.Size = new System.Drawing.Size(6, 25);
			// 
			// sbSelectAll
			// 
			this.al.SetAction(this.sbSelectAll, this.aEditSelectAll);
			this.sbSelectAll.AutoToolTip = false;
			this.sbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sbSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("sbSelectAll.Image")));
			this.sbSelectAll.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.sbSelectAll.Name = "sbSelectAll";
			this.sbSelectAll.Size = new System.Drawing.Size(23, 22);
			this.sbSelectAll.Text = "&Select All";
			this.sbSelectAll.ToolTipText = "Select all";
			// 
			// tsEditSep2
			// 
			this.tsEditSep2.Name = "tsEditSep2";
			this.tsEditSep2.Size = new System.Drawing.Size(6, 25);
			// 
			// sbCut
			// 
			this.al.SetAction(this.sbCut, this.aEditCut);
			this.sbCut.AutoToolTip = false;
			this.sbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sbCut.Image = ((System.Drawing.Image)(resources.GetObject("sbCut.Image")));
			this.sbCut.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.sbCut.Name = "sbCut";
			this.sbCut.Size = new System.Drawing.Size(23, 22);
			this.sbCut.Text = "Cu&t";
			this.sbCut.ToolTipText = "Cut selection to clipboard";
			// 
			// sbCopy
			// 
			this.al.SetAction(this.sbCopy, this.aEditCopy);
			this.sbCopy.AutoToolTip = false;
			this.sbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sbCopy.Image = ((System.Drawing.Image)(resources.GetObject("sbCopy.Image")));
			this.sbCopy.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.sbCopy.Name = "sbCopy";
			this.sbCopy.Size = new System.Drawing.Size(23, 22);
			this.sbCopy.Text = "&Copy";
			this.sbCopy.ToolTipText = "Copy selection to clipboard";
			// 
			// sbPaste
			// 
			this.al.SetAction(this.sbPaste, this.aEditPaste);
			this.sbPaste.AutoToolTip = false;
			this.sbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sbPaste.Image = ((System.Drawing.Image)(resources.GetObject("sbPaste.Image")));
			this.sbPaste.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.sbPaste.Name = "sbPaste";
			this.sbPaste.Size = new System.Drawing.Size(23, 22);
			this.sbPaste.Text = "&Paste";
			this.sbPaste.ToolTipText = "Paste from clipboard";
			// 
			// sbDelete
			// 
			this.al.SetAction(this.sbDelete, this.aEditDelete);
			this.sbDelete.AutoToolTip = false;
			this.sbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sbDelete.Image = ((System.Drawing.Image)(resources.GetObject("sbDelete.Image")));
			this.sbDelete.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.sbDelete.Name = "sbDelete";
			this.sbDelete.Size = new System.Drawing.Size(23, 22);
			this.sbDelete.Text = "&Delete";
			this.sbDelete.ToolTipText = "Delete selection";
			// 
			// tsEditSep3
			// 
			this.tsEditSep3.Name = "tsEditSep3";
			this.tsEditSep3.Size = new System.Drawing.Size(6, 25);
			// 
			// sbUndo
			// 
			this.al.SetAction(this.sbUndo, this.aEditUndo);
			this.sbUndo.AutoToolTip = false;
			this.sbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sbUndo.Image = ((System.Drawing.Image)(resources.GetObject("sbUndo.Image")));
			this.sbUndo.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.sbUndo.Name = "sbUndo";
			this.sbUndo.Size = new System.Drawing.Size(23, 22);
			this.sbUndo.Text = "&Undo";
			this.sbUndo.ToolTipText = "Undo last operation";
			// 
			// sbRedo
			// 
			this.al.SetAction(this.sbRedo, this.aEditRedo);
			this.sbRedo.AutoToolTip = false;
			this.sbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sbRedo.Image = ((System.Drawing.Image)(resources.GetObject("sbRedo.Image")));
			this.sbRedo.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.sbRedo.Name = "sbRedo";
			this.sbRedo.Size = new System.Drawing.Size(23, 22);
			this.sbRedo.Text = "&Redo";
			this.sbRedo.ToolTipText = "Redo last operation";
			// 
			// tsEditSep4
			// 
			this.tsEditSep4.Name = "tsEditSep4";
			this.tsEditSep4.Size = new System.Drawing.Size(6, 25);
			// 
			// helpToolStripButton
			// 
			this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
			this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.helpToolStripButton.Name = "helpToolStripButton";
			this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.helpToolStripButton.Text = "He&lp";
			// 
			// aAbout
			// 
			this.aAbout.Checked = false;
			this.aAbout.Enabled = true;
			this.aAbout.Hint = null;
			this.aAbout.Tag = null;
			this.aAbout.Text = "About";
			this.aAbout.Visible = true;
			this.aAbout.Execute += new System.EventHandler(this.AAboutExecute);
			// 
			// miService
			// 
			this.miService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miOptionsToolStripMenuItem});
			this.miService.Name = "miService";
			this.miService.Size = new System.Drawing.Size(44, 20);
			this.miService.Text = "&Tools";
			// 
			// miOptionsToolStripMenuItem
			// 
			this.al.SetAction(this.miOptionsToolStripMenuItem, this.aOptions);
			this.miOptionsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miOptionsToolStripMenuItem.Name = "miOptionsToolStripMenuItem";
			this.miOptionsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.miOptionsToolStripMenuItem.Text = "O&ptions ...";
			// 
			// aOptions
			// 
			this.aOptions.Checked = false;
			this.aOptions.Enabled = true;
			this.aOptions.Hint = null;
			this.aOptions.Tag = null;
			this.aOptions.Text = "O&ptions ...";
			this.aOptions.Visible = true;
			this.aOptions.Execute += new System.EventHandler(this.AOptionsExecute);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(484, 549);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.msMain.ResumeLayout(false);
			this.msMain.PerformLayout();
			this.pToolBars.ResumeLayout(false);
			this.pToolBars.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.al)).EndInit();
			this.ptEdit.ResumeLayout(false);
			this.ptEdit.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		public System.Windows.Forms.ToolStripMenuItem miOptionsToolStripMenuItem;
		private FreeCL.UI.Actions.Action aOptions;
		public System.Windows.Forms.ToolStripMenuItem miService;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public FreeCL.UI.Actions.Action aAbout;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStrip ptEdit;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripSeparator tsEditSep1;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripSeparator tsEditSep3;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripSeparator tsEditSep4;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton sbUndo;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripSeparator tsEditSep2;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton helpToolStripButton;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton printToolStripButton;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton saveToolStripButton;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton openToolStripButton;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripButton newToolStripButton;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miAbout;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripMenuItem miHelp;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripSeparator miEditSep2;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripSeparator miEditSep11;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		public System.Windows.Forms.ToolStripSeparator miFileSep1;
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]	
		#endregion
		void AExitExecute(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		void AEditCopyUpdate(object sender, System.EventArgs e)
		{
			aEditCopy.Enabled = EditingManager.CanCopy;
		}
		
		void AEditCopyExecute(object sender, System.EventArgs e)
		{
			EditingManager.Copy();	
		}
		
		void AEditCutUpdate(object sender, System.EventArgs e)
		{
			aEditCut.Enabled = EditingManager.CanCut;			
		}
		
		void AEditCutExecute(object sender, System.EventArgs e)
		{
			EditingManager.Cut();	 
		}
		
		void AEditPasteUpdate(object sender, System.EventArgs e)
		{
			aEditPaste.Enabled = EditingManager.CanPaste;
		}
		
		void AEditPasteExecute(object sender, System.EventArgs e)
		{
			EditingManager.Paste();	 
		}
		
		void AEditSelectAllUpdate(object sender, System.EventArgs e)
		{
			aEditSelectAll.Enabled = EditingManager.CanSelectAll ;
			//TODO: support for combobox			
		}
		
		void AEditSelectAllExecute(object sender, System.EventArgs e)
		{
			EditingManager.SelectAll();
		}
		
		void AEditDeleteUpdate(object sender, System.EventArgs e)
		{
			aEditDelete.Enabled = EditingManager.CanDelete;
			//TODO: support for combobox
		}
		
		void AEditDeleteExecute(object sender, System.EventArgs e)
		{
			EditingManager.Delete();
			//TODO: support for combobox
		}
		
		void AEditUndoUpdate(object sender, System.EventArgs e)
		{
			aEditUndo.Enabled = EditingManager.CanUndo;
		}
		
		void AEditUndoExecute(object sender, System.EventArgs e)
		{
			 EditingManager.Undo();
		}
		
		void AEditRedoUpdate(object sender, System.EventArgs e)
		{
			aEditRedo.Enabled = EditingManager.CanRedo;
			
		}
		
		void AEditRedoExecute(object sender, System.EventArgs e)
		{
			EditingManager.Redo();
		}
		
		void AAboutExecute(object sender, EventArgs e)
		{
			Application.ShowAboutForm();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			aAbout.Image = Icon.ToBitmap();
		}
		
		
		void AOptionsExecute(object sender, EventArgs e)
		{
			Application.ShowOptionsForm();
		}
	}
}
