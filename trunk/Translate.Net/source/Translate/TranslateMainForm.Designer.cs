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
 
using System.Diagnostics.CodeAnalysis;
 
namespace Translate
{
	partial class TranslateMainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TranslateMainForm));
			this.msIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miShowHide = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miAbout2 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsIconSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.miIconExit = new System.Windows.Forms.ToolStripMenuItem();
			this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
			this.aShowMainForm = new FreeCL.UI.Actions.Action(this.components);
			this.pLeft = new FreeCL.UI.Panel();
			this.pRight = new FreeCL.UI.Panel();
			this.languageSelector = new Translate.LanguageSelector();
			this.pTop = new FreeCL.UI.Panel();
			this.pBottom = new FreeCL.UI.Panel();
			this.pMain = new FreeCL.UI.Panel();
			this.resBrowser = new Translate.ResultBrowser();
			this.msBrowser = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miShowSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitterTranslate = new System.Windows.Forms.Splitter();
			this.tbFrom = new System.Windows.Forms.TextBox();
			this.tsTranslate = new System.Windows.Forms.ToolStrip();
			this.tsbTranslate = new System.Windows.Forms.ToolStripButton();
			this.splitterLeft = new System.Windows.Forms.Splitter();
			this.splitterRight = new System.Windows.Forms.Splitter();
			this.splitterTop = new System.Windows.Forms.Splitter();
			this.splitterBottom = new System.Windows.Forms.Splitter();
			this.aTranslate = new FreeCL.UI.Actions.Action(this.components);
			this.miTranslate = new System.Windows.Forms.ToolStripMenuItem();
			this.aInvertTranslationDirection = new FreeCL.UI.Actions.Action(this.components);
			this.miAnimatedIcon = new System.Windows.Forms.ToolStripMenuItem();
			this.lStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.aCheckUpdates = new FreeCL.UI.Actions.Action(this.components);
			this.miCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
			this.miHelpSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.timerUpdater = new System.Windows.Forms.Timer(this.components);
			this.aShowHtmlSource = new FreeCL.UI.Actions.Action(this.components);
			this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
			this.lSelectedLangsPair = new System.Windows.Forms.ToolStripStatusLabel();
			this.lInputLang = new System.Windows.Forms.ToolStripStatusLabel();
			this.aScrollResultDown = new FreeCL.UI.Actions.Action(this.components);
			this.aScrollResultUp = new FreeCL.UI.Actions.Action(this.components);
			this.aScrollResultPageDown = new FreeCL.UI.Actions.Action(this.components);
			this.aScrollResultPageUp = new FreeCL.UI.Actions.Action(this.components);
			this.ptEdit.SuspendLayout();
			this.sbMain.SuspendLayout();
			this.msMain.SuspendLayout();
			this.pToolBars.SuspendLayout();
			this.msIcon.SuspendLayout();
			this.pRight.SuspendLayout();
			this.pMain.SuspendLayout();
			this.msBrowser.SuspendLayout();
			this.tsTranslate.SuspendLayout();
			this.SuspendLayout();
			// 
			// miEditSelectAll
			// 
			this.al.SetAction(this.miEditSelectAll, this.aEditSelectAll);
			this.miEditSelectAll.ToolTipText = "Select all (Ctrl+A)";
			this.miEditSelectAll.Visible = true;
			// 
			// miExit
			// 
			this.al.SetAction(this.miExit, this.aExit);
			this.miExit.Size = new System.Drawing.Size(188, 22);
			this.miExit.ToolTipText = "Exit from application (Alt+F4)";
			this.miExit.Visible = true;
			// 
			// sbCut
			// 
			this.al.SetAction(this.sbCut, this.aEditCut);
			this.sbCut.ToolTipText = "Cut selection to clipboard (Ctrl+X)";
			this.sbCut.Visible = true;
			// 
			// miEditCopy
			// 
			this.al.SetAction(this.miEditCopy, this.aEditCopy);
			this.miEditCopy.ToolTipText = "Copy selection to clipboard (Ctrl+C)";
			this.miEditCopy.Visible = true;
			// 
			// sbPaste
			// 
			this.al.SetAction(this.sbPaste, this.aEditPaste);
			this.sbPaste.ToolTipText = "Paste from clipboard (Ctrl+V)";
			this.sbPaste.Visible = true;
			// 
			// miEditDel
			// 
			this.al.SetAction(this.miEditDel, this.aEditDelete);
			this.miEditDel.ToolTipText = "Delete selection (Del)";
			this.miEditDel.Visible = true;
			// 
			// sbSelectAll
			// 
			this.al.SetAction(this.sbSelectAll, this.aEditSelectAll);
			this.sbSelectAll.Enabled = false;
			this.sbSelectAll.ToolTipText = "Select all (Ctrl+A)";
			this.sbSelectAll.Visible = true;
			// 
			// miEditRedo
			// 
			this.al.SetAction(this.miEditRedo, this.aEditRedo);
			this.miEditRedo.ToolTipText = "Redo last operation (Ctrl+Y)";
			this.miEditRedo.Visible = true;
			// 
			// miEditUndo
			// 
			this.al.SetAction(this.miEditUndo, this.aEditUndo);
			this.miEditUndo.ToolTipText = "Undo last operation (Ctrl+Z)";
			this.miEditUndo.Visible = true;
			// 
			// aExit
			// 
			this.aExit.Execute += new System.EventHandler(this.AExitExecute);
			// 
			// miFile
			// 
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miTranslate});
			this.miFile.Size = new System.Drawing.Size(64, 20);
			this.miFile.Text = "&Translate";
			this.miFile.Visible = true;
			// 
			// sbCopy
			// 
			this.al.SetAction(this.sbCopy, this.aEditCopy);
			this.sbCopy.ToolTipText = "Copy selection to clipboard (Ctrl+C)";
			this.sbCopy.Visible = true;
			// 
			// sbDelete
			// 
			this.al.SetAction(this.sbDelete, this.aEditDelete);
			this.sbDelete.ToolTipText = "Delete selection (Del)";
			this.sbDelete.Visible = true;
			// 
			// miEditPaste
			// 
			this.al.SetAction(this.miEditPaste, this.aEditPaste);
			this.miEditPaste.ToolTipText = "Paste from clipboard (Ctrl+V)";
			this.miEditPaste.Visible = true;
			// 
			// sbRedo
			// 
			this.al.SetAction(this.sbRedo, this.aEditRedo);
			this.sbRedo.ToolTipText = "Redo last operation (Ctrl+Y)";
			this.sbRedo.Visible = true;
			// 
			// miEdit
			// 
			this.miEdit.Visible = true;
			// 
			// miEditCut
			// 
			this.al.SetAction(this.miEditCut, this.aEditCut);
			this.miEditCut.ToolTipText = "Cut selection to clipboard (Ctrl+X)";
			this.miEditCut.Visible = true;
			// 
			// aAbout
			// 
			this.aAbout.Image = ((System.Drawing.Image)(resources.GetObject("aAbout.Image")));
			// 
			// ptEdit
			// 
			this.ptEdit.Text = "";
			// 
			// tsEditSep1
			// 
			this.tsEditSep1.Visible = true;
			// 
			// tsEditSep3
			// 
			this.tsEditSep3.Visible = true;
			// 
			// tsEditSep4
			// 
			this.tsEditSep4.Visible = true;
			// 
			// sbUndo
			// 
			this.al.SetAction(this.sbUndo, this.aEditUndo);
			this.sbUndo.ToolTipText = "Undo last operation (Ctrl+Z)";
			this.sbUndo.Visible = true;
			// 
			// tsEditSep2
			// 
			this.tsEditSep2.Visible = true;
			// 
			// helpToolStripButton
			// 
			this.helpToolStripButton.Visible = true;
			// 
			// printToolStripButton
			// 
			this.printToolStripButton.Enabled = false;
			this.printToolStripButton.Visible = true;
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.Enabled = false;
			this.saveToolStripButton.Visible = true;
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.Enabled = false;
			this.openToolStripButton.Visible = true;
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.Enabled = false;
			this.newToolStripButton.Visible = true;
			// 
			// miAbout
			// 
			this.al.SetAction(this.miAbout, this.aAbout);
			this.miAbout.Image = ((System.Drawing.Image)(resources.GetObject("miAbout.Image")));
			this.miAbout.Size = new System.Drawing.Size(172, 22);
			this.miAbout.Visible = true;
			// 
			// miHelp
			// 
			this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miCheckUpdates,
									this.miHelpSeparator1});
			this.miHelp.Visible = true;
			// 
			// miEditSep2
			// 
			this.miEditSep2.Visible = true;
			// 
			// miEditSep11
			// 
			this.miEditSep11.Visible = true;
			// 
			// miFileSep1
			// 
			this.miFileSep1.Size = new System.Drawing.Size(185, 6);
			this.miFileSep1.Visible = true;
			// 
			// sbMain
			// 
			this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.lStatus,
									this.pbMain,
									this.lSelectedLangsPair,
									this.lInputLang});
			this.sbMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.sbMain.Location = new System.Drawing.Point(0, 442);
			this.sbMain.Size = new System.Drawing.Size(615, 22);
			this.sbMain.Text = "";
			this.sbMain.Resize += new System.EventHandler(this.SbMainResize);
			// 
			// msMain
			// 
			this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miAnimatedIcon});
			this.msMain.Size = new System.Drawing.Size(615, 24);
			this.msMain.Text = "";
			// 
			// pToolBars
			// 
			this.pToolBars.Size = new System.Drawing.Size(615, 25);
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
			this.il.Images.SetKeyName(6, "");
			this.il.Images.SetKeyName(7, "");
			this.il.Images.SetKeyName(8, "");
			this.il.Images.SetKeyName(9, "");
			this.il.Images.SetKeyName(10, "");
			this.il.Images.SetKeyName(11, "");
			this.il.Images.SetKeyName(12, "translate.net.16x16.ico");
			this.il.Images.SetKeyName(13, "reverse.16x16.png");
			// 
			// al
			// 
			this.al.Actions.Add(this.aShowMainForm);
			this.al.Actions.Add(this.aTranslate);
			this.al.Actions.Add(this.aInvertTranslationDirection);
			this.al.Actions.Add(this.aCheckUpdates);
			this.al.Actions.Add(this.aShowHtmlSource);
			this.al.Actions.Add(this.aScrollResultDown);
			this.al.Actions.Add(this.aScrollResultUp);
			this.al.Actions.Add(this.aScrollResultPageDown);
			this.al.Actions.Add(this.aScrollResultPageUp);
			this.al.ShowShortcutsInHints = true;
			// 
			// globalEvents
			// 
			this.globalEvents.Idle += new System.EventHandler(this.GlobalEventsIdle);
			// 
			// msIcon
			// 
			this.msIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miShowHide,
									this.toolStripSeparator1,
									this.miAbout2,
									this.tsIconSep1,
									this.miIconExit});
			this.msIcon.Name = "msIcon";
			this.msIcon.Size = new System.Drawing.Size(144, 82);
			// 
			// miShowHide
			// 
			this.al.SetAction(this.miShowHide, this.aShowMainForm);
			this.miShowHide.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowHide.Name = "miShowHide";
			this.miShowHide.Size = new System.Drawing.Size(143, 22);
			this.miShowHide.Text = "Show/Hide";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
			// 
			// miAbout2
			// 
			this.al.SetAction(this.miAbout2, this.aAbout);
			this.miAbout2.Image = ((System.Drawing.Image)(resources.GetObject("miAbout2.Image")));
			this.miAbout2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miAbout2.Name = "miAbout2";
			this.miAbout2.Size = new System.Drawing.Size(143, 22);
			this.miAbout2.Text = "&About ...";
			// 
			// tsIconSep1
			// 
			this.tsIconSep1.Name = "tsIconSep1";
			this.tsIconSep1.Size = new System.Drawing.Size(140, 6);
			// 
			// miIconExit
			// 
			this.al.SetAction(this.miIconExit, this.aExit);
			this.miIconExit.Image = ((System.Drawing.Image)(resources.GetObject("miIconExit.Image")));
			this.miIconExit.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miIconExit.Name = "miIconExit";
			this.miIconExit.ShortcutKeyDisplayString = "Alt+F4";
			this.miIconExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.miIconExit.Size = new System.Drawing.Size(143, 22);
			this.miIconExit.Text = "E&xit";
			this.miIconExit.ToolTipText = "Exit from application";
			// 
			// niMain
			// 
			this.niMain.ContextMenuStrip = this.msIcon;
			this.niMain.Icon = ((System.Drawing.Icon)(resources.GetObject("niMain.Icon")));
			this.niMain.Text = "Translate.Net";
			this.niMain.Visible = true;
			this.niMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NiMainMouseClick);
			// 
			// aShowMainForm
			// 
			this.aShowMainForm.Checked = false;
			this.aShowMainForm.Enabled = true;
			this.aShowMainForm.Hint = null;
			this.aShowMainForm.Tag = null;
			this.aShowMainForm.Text = "Show/Hide";
			this.aShowMainForm.Visible = true;
			this.aShowMainForm.Execute += new System.EventHandler(this.AShowMainFormExecute);
			// 
			// pLeft
			// 
			this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pLeft.Location = new System.Drawing.Point(0, 49);
			this.pLeft.Name = "pLeft";
			this.pLeft.Size = new System.Drawing.Size(21, 393);
			this.pLeft.TabIndex = 5;
			// 
			// pRight
			// 
			this.pRight.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.pRight.Controls.Add(this.languageSelector);
			this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pRight.Location = new System.Drawing.Point(415, 72);
			this.pRight.Name = "pRight";
			this.pRight.Size = new System.Drawing.Size(200, 344);
			this.pRight.TabIndex = 6;
			// 
			// languageSelector
			// 
			this.languageSelector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.languageSelector.Location = new System.Drawing.Point(0, 0);
			this.languageSelector.MinimumSize = new System.Drawing.Size(150, 0);
			this.languageSelector.Name = "languageSelector";
			this.languageSelector.Padding = new System.Windows.Forms.Padding(2);
			this.languageSelector.Size = new System.Drawing.Size(200, 344);
			this.languageSelector.TabIndex = 0;
			this.languageSelector.SelectionChanged += new System.EventHandler(this.LanguageSelectorSelectionChanged);
			this.languageSelector.SubjectsChanged += new System.EventHandler(this.LanguageSelectorSubjectsChanged);
			// 
			// pTop
			// 
			this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pTop.Location = new System.Drawing.Point(21, 49);
			this.pTop.Name = "pTop";
			this.pTop.Size = new System.Drawing.Size(594, 20);
			this.pTop.TabIndex = 7;
			// 
			// pBottom
			// 
			this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBottom.Location = new System.Drawing.Point(21, 419);
			this.pBottom.Name = "pBottom";
			this.pBottom.Size = new System.Drawing.Size(594, 23);
			this.pBottom.TabIndex = 8;
			// 
			// pMain
			// 
			this.pMain.BevelOuter = FreeCL.UI.BevelStyle.None;
			this.pMain.Controls.Add(this.resBrowser);
			this.pMain.Controls.Add(this.splitterTranslate);
			this.pMain.Controls.Add(this.tbFrom);
			this.pMain.Controls.Add(this.tsTranslate);
			this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pMain.Location = new System.Drawing.Point(24, 72);
			this.pMain.Name = "pMain";
			this.pMain.Size = new System.Drawing.Size(386, 344);
			this.pMain.TabIndex = 9;
			// 
			// resBrowser
			// 
			this.resBrowser.ContextMenuStrip = this.msBrowser;
			this.resBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resBrowser.Location = new System.Drawing.Point(0, 0);
			this.resBrowser.Name = "resBrowser";
			this.resBrowser.Size = new System.Drawing.Size(386, 187);
			this.resBrowser.TabIndex = 3;
			this.resBrowser.StatusTextChanged += new System.EventHandler(this.ResBrowserStatusTextChanged);
			// 
			// msBrowser
			// 
			this.msBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miShowSourceToolStripMenuItem});
			this.msBrowser.Name = "browserMenu";
			this.msBrowser.Size = new System.Drawing.Size(177, 26);
			// 
			// miShowSourceToolStripMenuItem
			// 
			this.al.SetAction(this.miShowSourceToolStripMenuItem, this.aShowHtmlSource);
			this.miShowSourceToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowSourceToolStripMenuItem.Name = "miShowSourceToolStripMenuItem";
			this.miShowSourceToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.miShowSourceToolStripMenuItem.Text = "Show HTML Source";
			// 
			// splitterTranslate
			// 
			this.splitterTranslate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitterTranslate.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterTranslate.Location = new System.Drawing.Point(0, 187);
			this.splitterTranslate.Name = "splitterTranslate";
			this.splitterTranslate.Size = new System.Drawing.Size(386, 5);
			this.splitterTranslate.TabIndex = 2;
			this.splitterTranslate.TabStop = false;
			// 
			// tbFrom
			// 
			this.tbFrom.AcceptsReturn = true;
			this.tbFrom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tbFrom.Location = new System.Drawing.Point(0, 192);
			this.tbFrom.MaxLength = 0;
			this.tbFrom.Multiline = true;
			this.tbFrom.Name = "tbFrom";
			this.tbFrom.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbFrom.Size = new System.Drawing.Size(386, 127);
			this.tbFrom.TabIndex = 1;
			// 
			// tsTranslate
			// 
			this.tsTranslate.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tsTranslate.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsTranslate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbTranslate});
			this.tsTranslate.Location = new System.Drawing.Point(0, 319);
			this.tsTranslate.Name = "tsTranslate";
			this.tsTranslate.Size = new System.Drawing.Size(386, 25);
			this.tsTranslate.TabIndex = 0;
			// 
			// tsbTranslate
			// 
			this.al.SetAction(this.tsbTranslate, this.aTranslate);
			this.tsbTranslate.AutoToolTip = false;
			this.tsbTranslate.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.tsbTranslate.Name = "tsbTranslate";
			this.tsbTranslate.Size = new System.Drawing.Size(56, 22);
			this.tsbTranslate.Text = "Translate";
			// 
			// splitterLeft
			// 
			this.splitterLeft.Location = new System.Drawing.Point(21, 69);
			this.splitterLeft.Name = "splitterLeft";
			this.splitterLeft.Size = new System.Drawing.Size(3, 350);
			this.splitterLeft.TabIndex = 10;
			this.splitterLeft.TabStop = false;
			// 
			// splitterRight
			// 
			this.splitterRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitterRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitterRight.Location = new System.Drawing.Point(410, 72);
			this.splitterRight.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.splitterRight.MinExtra = 60;
			this.splitterRight.MinSize = 60;
			this.splitterRight.Name = "splitterRight";
			this.splitterRight.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
			this.splitterRight.Size = new System.Drawing.Size(5, 344);
			this.splitterRight.TabIndex = 11;
			this.splitterRight.TabStop = false;
			// 
			// splitterTop
			// 
			this.splitterTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterTop.Location = new System.Drawing.Point(24, 69);
			this.splitterTop.Name = "splitterTop";
			this.splitterTop.Size = new System.Drawing.Size(591, 3);
			this.splitterTop.TabIndex = 12;
			this.splitterTop.TabStop = false;
			// 
			// splitterBottom
			// 
			this.splitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterBottom.Location = new System.Drawing.Point(24, 416);
			this.splitterBottom.Name = "splitterBottom";
			this.splitterBottom.Size = new System.Drawing.Size(591, 3);
			this.splitterBottom.TabIndex = 13;
			this.splitterBottom.TabStop = false;
			// 
			// aTranslate
			// 
			this.aTranslate.Checked = false;
			this.aTranslate.Enabled = true;
			this.aTranslate.Hint = null;
			this.aTranslate.ImageIndex = 12;
			this.aTranslate.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
			this.aTranslate.Tag = null;
			this.aTranslate.Text = "Translate";
			this.aTranslate.Visible = true;
			this.aTranslate.Execute += new System.EventHandler(this.ATranslateExecute);
			this.aTranslate.Update += new System.EventHandler(this.ATranslateUpdate);
			// 
			// miTranslate
			// 
			this.al.SetAction(this.miTranslate, this.aTranslate);
			this.miTranslate.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miTranslate.Name = "miTranslate";
			this.miTranslate.ShortcutKeyDisplayString = "Ctrl+Enter";
			this.miTranslate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
			this.miTranslate.Size = new System.Drawing.Size(188, 22);
			this.miTranslate.Text = "Translate";
			// 
			// aInvertTranslationDirection
			// 
			this.aInvertTranslationDirection.Checked = false;
			this.aInvertTranslationDirection.Enabled = true;
			this.aInvertTranslationDirection.Hint = "Reverse translation direction";
			this.aInvertTranslationDirection.ImageIndex = 13;
			this.aInvertTranslationDirection.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.aInvertTranslationDirection.Tag = null;
			this.aInvertTranslationDirection.Text = "Invert";
			this.aInvertTranslationDirection.Visible = true;
			this.aInvertTranslationDirection.Execute += new System.EventHandler(this.AInvertTranslationDirectionExecute);
			// 
			// miAnimatedIcon
			// 
			this.miAnimatedIcon.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.miAnimatedIcon.Image = ((System.Drawing.Image)(resources.GetObject("miAnimatedIcon.Image")));
			this.miAnimatedIcon.Name = "miAnimatedIcon";
			this.miAnimatedIcon.Size = new System.Drawing.Size(28, 20);
			// 
			// lStatus
			// 
			this.lStatus.AutoSize = false;
			this.lStatus.Name = "lStatus";
			this.lStatus.Size = new System.Drawing.Size(400, 17);
			this.lStatus.Spring = true;
			this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// aCheckUpdates
			// 
			this.aCheckUpdates.Checked = false;
			this.aCheckUpdates.Enabled = true;
			this.aCheckUpdates.Hint = null;
			this.aCheckUpdates.Tag = null;
			this.aCheckUpdates.Text = "Check Updates ...";
			this.aCheckUpdates.Visible = true;
			this.aCheckUpdates.Execute += new System.EventHandler(this.ACheckUpdatesExecute);
			this.aCheckUpdates.Update += new System.EventHandler(this.ACheckUpdatesUpdate);
			// 
			// miCheckUpdates
			// 
			this.al.SetAction(this.miCheckUpdates, this.aCheckUpdates);
			this.miCheckUpdates.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miCheckUpdates.Name = "miCheckUpdates";
			this.miCheckUpdates.Size = new System.Drawing.Size(172, 22);
			this.miCheckUpdates.Text = "Check Updates ...";
			// 
			// miHelpSeparator1
			// 
			this.miHelpSeparator1.Name = "miHelpSeparator1";
			this.miHelpSeparator1.Size = new System.Drawing.Size(169, 6);
			// 
			// timerUpdater
			// 
			this.timerUpdater.Enabled = true;
			this.timerUpdater.Interval = 3600000;
			this.timerUpdater.Tick += new System.EventHandler(this.TimerUpdaterTick);
			// 
			// aShowHtmlSource
			// 
			this.aShowHtmlSource.Checked = false;
			this.aShowHtmlSource.Enabled = true;
			this.aShowHtmlSource.Hint = null;
			this.aShowHtmlSource.Tag = null;
			this.aShowHtmlSource.Text = "Show HTML Source";
			this.aShowHtmlSource.Visible = true;
			this.aShowHtmlSource.Execute += new System.EventHandler(this.AShowHtmlSourceExecute);
			// 
			// pbMain
			// 
			this.pbMain.Name = "pbMain";
			this.pbMain.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.pbMain.Size = new System.Drawing.Size(110, 16);
			this.pbMain.Step = 1;
			this.pbMain.Visible = false;
			this.pbMain.VisibleChanged += new System.EventHandler(this.PbMainVisibleChanged);
			// 
			// lSelectedLangsPair
			// 
			this.lSelectedLangsPair.Name = "lSelectedLangsPair";
			this.lSelectedLangsPair.Size = new System.Drawing.Size(55, 17);
			// 
			// lInputLang
			// 
			this.lInputLang.Name = "lInputLang";
			this.lInputLang.Size = new System.Drawing.Size(20, 17);
			this.lInputLang.Text = "EN";
			// 
			// aScrollResultDown
			// 
			this.aScrollResultDown.Checked = false;
			this.aScrollResultDown.Enabled = true;
			this.aScrollResultDown.Hint = null;
			this.aScrollResultDown.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
			this.aScrollResultDown.Tag = null;
			this.aScrollResultDown.Text = "ScrollResultDown";
			this.aScrollResultDown.Visible = true;
			this.aScrollResultDown.Execute += new System.EventHandler(this.AScrollResultDownExecute);
			// 
			// aScrollResultUp
			// 
			this.aScrollResultUp.Checked = false;
			this.aScrollResultUp.Enabled = true;
			this.aScrollResultUp.Hint = null;
			this.aScrollResultUp.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
			this.aScrollResultUp.Tag = null;
			this.aScrollResultUp.Text = "ScrollResultUp";
			this.aScrollResultUp.Visible = true;
			this.aScrollResultUp.Execute += new System.EventHandler(this.AScrollResultUpExecute);
			// 
			// aScrollResultPageDown
			// 
			this.aScrollResultPageDown.Checked = false;
			this.aScrollResultPageDown.Enabled = true;
			this.aScrollResultPageDown.Hint = "Ctrl+PageDown";
			this.aScrollResultPageDown.Tag = null;
			this.aScrollResultPageDown.Text = "ScrollResultPageDown";
			this.aScrollResultPageDown.Visible = true;
			this.aScrollResultPageDown.Execute += new System.EventHandler(this.AScrollResultPageDownExecute);
			// 
			// aScrollResultPageUp
			// 
			this.aScrollResultPageUp.Checked = false;
			this.aScrollResultPageUp.Enabled = true;
			this.aScrollResultPageUp.Hint = "Ctrl+PageUp";
			this.aScrollResultPageUp.Tag = null;
			this.aScrollResultPageUp.Text = "ScrollResultPageUp";
			this.aScrollResultPageUp.Visible = true;
			this.aScrollResultPageUp.Execute += new System.EventHandler(this.AScrollResultPageUpExecute);
			// 
			// TranslateMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(615, 464);
			this.Controls.Add(this.pMain);
			this.Controls.Add(this.splitterRight);
			this.Controls.Add(this.pRight);
			this.Controls.Add(this.splitterBottom);
			this.Controls.Add(this.splitterTop);
			this.Controls.Add(this.splitterLeft);
			this.Controls.Add(this.pBottom);
			this.Controls.Add(this.pTop);
			this.Controls.Add(this.pLeft);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 34);
			this.Name = "TranslateMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "";
			this.Load += new System.EventHandler(this.TranslateMainFormLoad);
			this.Shown += new System.EventHandler(this.TranslateMainFormShown);
			this.Activated += new System.EventHandler(this.TranslateMainFormActivated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TranslateMainFormFormClosing);
			this.Resize += new System.EventHandler(this.TranslateMainFormResize);
			this.Controls.SetChildIndex(this.sbMain, 0);
			this.Controls.SetChildIndex(this.msMain, 0);
			this.Controls.SetChildIndex(this.pToolBars, 0);
			this.Controls.SetChildIndex(this.pLeft, 0);
			this.Controls.SetChildIndex(this.pTop, 0);
			this.Controls.SetChildIndex(this.pBottom, 0);
			this.Controls.SetChildIndex(this.splitterLeft, 0);
			this.Controls.SetChildIndex(this.splitterTop, 0);
			this.Controls.SetChildIndex(this.splitterBottom, 0);
			this.Controls.SetChildIndex(this.pRight, 0);
			this.Controls.SetChildIndex(this.splitterRight, 0);
			this.Controls.SetChildIndex(this.pMain, 0);
			this.ptEdit.ResumeLayout(false);
			this.ptEdit.PerformLayout();
			this.sbMain.ResumeLayout(false);
			this.sbMain.PerformLayout();
			this.msMain.ResumeLayout(false);
			this.msMain.PerformLayout();
			this.pToolBars.ResumeLayout(false);
			this.pToolBars.PerformLayout();
			this.msIcon.ResumeLayout(false);
			this.pRight.ResumeLayout(false);
			this.pMain.ResumeLayout(false);
			this.pMain.PerformLayout();
			this.msBrowser.ResumeLayout(false);
			this.tsTranslate.ResumeLayout(false);
			this.tsTranslate.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private FreeCL.UI.Actions.Action aScrollResultPageUp;
		private FreeCL.UI.Actions.Action aScrollResultPageDown;
		private FreeCL.UI.Actions.Action aScrollResultUp;
		private FreeCL.UI.Actions.Action aScrollResultDown;
		private System.Windows.Forms.ToolStripStatusLabel lInputLang;
		private System.Windows.Forms.ToolStripStatusLabel lSelectedLangsPair;
		private System.Windows.Forms.ToolStripProgressBar pbMain;
		private System.Windows.Forms.ToolStripMenuItem miShowSourceToolStripMenuItem;
		private FreeCL.UI.Actions.Action aShowHtmlSource;
		private System.Windows.Forms.ContextMenuStrip msBrowser;
		private System.Windows.Forms.Timer timerUpdater;
		private System.Windows.Forms.ToolStripSeparator miHelpSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miCheckUpdates;
		private FreeCL.UI.Actions.Action aCheckUpdates;
		private System.Windows.Forms.ToolStripStatusLabel lStatus;
		private System.Windows.Forms.ToolStripMenuItem miAnimatedIcon;
		private FreeCL.UI.Actions.Action aInvertTranslationDirection;
		private System.Windows.Forms.ToolStripMenuItem miTranslate;
		private System.Windows.Forms.ToolStripButton tsbTranslate;
		private FreeCL.UI.Actions.Action aTranslate;
		private System.Windows.Forms.ToolStrip tsTranslate;
		private System.Windows.Forms.TextBox tbFrom;
		private System.Windows.Forms.Splitter splitterTranslate;
		private Translate.ResultBrowser resBrowser;
		private Translate.LanguageSelector languageSelector;
		private System.Windows.Forms.Splitter splitterRight;
		private System.Windows.Forms.Splitter splitterBottom;
		private System.Windows.Forms.Splitter splitterTop;
		private System.Windows.Forms.Splitter splitterLeft;
		private FreeCL.UI.Panel pMain;
		private FreeCL.UI.Panel pBottom;
		private FreeCL.UI.Panel pTop;
		private FreeCL.UI.Panel pRight;
		private FreeCL.UI.Panel pLeft;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miAbout2;
		private System.Windows.Forms.ToolStripMenuItem miShowHide;
		private FreeCL.UI.Actions.Action aShowMainForm;
		private System.Windows.Forms.NotifyIcon niMain;
		private System.Windows.Forms.ToolStripMenuItem miIconExit;
		private System.Windows.Forms.ToolStripSeparator tsIconSep1;
		private System.Windows.Forms.ContextMenuStrip msIcon;
	}
}
