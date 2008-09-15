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
			this.tsIconSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.miAbout2 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsIconSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.miControlCC = new System.Windows.Forms.ToolStripMenuItem();
			this.miControlInsIns = new System.Windows.Forms.ToolStripMenuItem();
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
			this.miBrowserTranslateSel = new System.Windows.Forms.ToolStripMenuItem();
			this.miSearchInGoogle = new System.Windows.Forms.ToolStripMenuItem();
			this.miBrowserSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.miBrowserCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.miBrowserSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.miBrowserSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.miShowSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitterTranslate = new System.Windows.Forms.Splitter();
			this.tbFrom = new System.Windows.Forms.TextBox();
			this.tsTranslate = new System.Windows.Forms.ToolStrip();
			this.msProfileAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miAddProfile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsSepProfiles4 = new System.Windows.Forms.ToolStripSeparator();
			this.miRemoveProfile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsSepProfiles3 = new System.Windows.Forms.ToolStripSeparator();
			this.miSelectedProfile = new System.Windows.Forms.ToolStripMenuItem();
			this.miProfileView = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowProfileServices = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowProfileLanguages = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowProfileSubjects = new System.Windows.Forms.ToolStripMenuItem();
			this.miIncludeMonolingualDicts2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miSetProfileProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditProfileServices = new System.Windows.Forms.ToolStripMenuItem();
			this.miFilterLanguages = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbTranslate = new System.Windows.Forms.ToolStripButton();
			this.tsbSearchInGoogle = new System.Windows.Forms.ToolStripButton();
			this.tsbStop = new System.Windows.Forms.ToolStripButton();
			this.tsSeparatorTranslate = new System.Windows.Forms.ToolStripSeparator();
			this.miProfiles = new System.Windows.Forms.ToolStripMenuItem();
			this.miAddProfile2 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsSepProfiles2 = new System.Windows.Forms.ToolStripSeparator();
			this.miRemoveProfile2 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsSepProfiles = new System.Windows.Forms.ToolStripSeparator();
			this.miProfileView2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowProfileServices2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowProfileLanguages2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowProfileSubjects2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miIncludeMonolingualDicts = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditProfileProperties2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditProfileServices2 = new System.Windows.Forms.ToolStripMenuItem();
			this.miFilterLanguages2 = new System.Windows.Forms.ToolStripMenuItem();
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
			this.aControlCC = new FreeCL.UI.Actions.Action(this.components);
			this.aControlInsIns = new FreeCL.UI.Actions.Action(this.components);
			this.miHelpSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miWebsite = new System.Windows.Forms.ToolStripMenuItem();
			this.aWebsite = new FreeCL.UI.Actions.Action(this.components);
			this.aFeedback = new FreeCL.UI.Actions.Action(this.components);
			this.miFeedback = new System.Windows.Forms.ToolStripMenuItem();
			this.aAddProfile = new FreeCL.UI.Actions.Action(this.components);
			this.aRemoveProfile = new FreeCL.UI.Actions.Action(this.components);
			this.aSetProfileProperties = new FreeCL.UI.Actions.Action(this.components);
			this.aEditProfileServices = new FreeCL.UI.Actions.Action(this.components);
			this.aShowProfileServices = new FreeCL.UI.Actions.Action(this.components);
			this.aShowProfileLanguages = new FreeCL.UI.Actions.Action(this.components);
			this.aShowProfileSubjects = new FreeCL.UI.Actions.Action(this.components);
			this.aNextProfile = new FreeCL.UI.Actions.Action(this.components);
			this.aPreviousProfile = new FreeCL.UI.Actions.Action(this.components);
			this.miView = new System.Windows.Forms.ToolStripMenuItem();
			this.miResultViewPlace = new System.Windows.Forms.ToolStripMenuItem();
			this.miResultViewPlaceTop = new System.Windows.Forms.ToolStripMenuItem();
			this.miResultViewPlaceBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.miShowAccents = new System.Windows.Forms.ToolStripMenuItem();
			this.miMarkErrors = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miShowTranslateDirection = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowServiceNames = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowStats = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miHideWithoutResult = new System.Windows.Forms.ToolStripMenuItem();
			this.aShowStatistics = new FreeCL.UI.Actions.Action(this.components);
			this.aShowErrors = new FreeCL.UI.Actions.Action(this.components);
			this.aHideWithoutResult = new FreeCL.UI.Actions.Action(this.components);
			this.aShowTranslationDirection = new FreeCL.UI.Actions.Action(this.components);
			this.aShowServiceName = new FreeCL.UI.Actions.Action(this.components);
			this.aShowAccents = new FreeCL.UI.Actions.Action(this.components);
			this.aIncludeMonolingualDicts = new FreeCL.UI.Actions.Action(this.components);
			this.aDonate = new FreeCL.UI.Actions.Action(this.components);
			this.miDonate = new System.Windows.Forms.ToolStripMenuItem();
			this.miOnlineHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.aOnlineHelp = new FreeCL.UI.Actions.Action(this.components);
			this.timerRecheckServices = new System.Windows.Forms.Timer(this.components);
			this.aStopTranslate = new FreeCL.UI.Actions.Action(this.components);
			this.miOnlineHelp2 = new System.Windows.Forms.ToolStripMenuItem();
			this.aFilterLanguages = new FreeCL.UI.Actions.Action(this.components);
			this.aSearchInGoogle = new FreeCL.UI.Actions.Action(this.components);
			this.aPlaceResultViewTop = new FreeCL.UI.Actions.Action(this.components);
			this.aPlaceResultViewBottom = new FreeCL.UI.Actions.Action(this.components);
			this.aPlaceResultViewLeft = new FreeCL.UI.Actions.Action(this.components);
			this.aPlaceResultViewRight = new FreeCL.UI.Actions.Action(this.components);
			this.ptEdit.SuspendLayout();
			this.sbMain.SuspendLayout();
			this.msMain.SuspendLayout();
			this.pToolBars.SuspendLayout();
			this.msIcon.SuspendLayout();
			this.pRight.SuspendLayout();
			this.pMain.SuspendLayout();
			this.msBrowser.SuspendLayout();
			this.tsTranslate.SuspendLayout();
			this.msProfileAdd.SuspendLayout();
			this.SuspendLayout();
			// 
			// miEditSelectAll
			// 
			this.al.SetAction(this.miEditSelectAll, this.aEditSelectAll);
			this.miEditSelectAll.ToolTipText = "Select Аll (Ctrl+A)";
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
			this.miEditDel.Enabled = false;
			this.miEditDel.ToolTipText = "Delete selection (Del)";
			this.miEditDel.Visible = true;
			// 
			// sbSelectAll
			// 
			this.al.SetAction(this.sbSelectAll, this.aEditSelectAll);
			this.sbSelectAll.Enabled = false;
			this.sbSelectAll.ToolTipText = "Select Аll (Ctrl+A)";
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
			this.sbDelete.Enabled = false;
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
			// aEditDelete
			// 
			this.aEditDelete.Enabled = false;
			// 
			// miOptionsToolStripMenuItem
			// 
			this.miOptionsToolStripMenuItem.Visible = true;
			// 
			// miService
			// 
			this.miService.Visible = true;
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
			this.miAbout.Size = new System.Drawing.Size(235, 22);
			this.miAbout.Visible = true;
			// 
			// miHelp
			// 
			this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miCheckUpdates,
									this.miHelpSeparator1,
									this.miHelpSeparator2,
									this.miWebsite,
									this.miFeedback,
									this.miDonate,
									this.miOnlineHelp});
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
									this.miView,
									this.miProfiles,
									this.miAnimatedIcon,
									this.miOnlineHelp2});
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
			this.il.Images.SetKeyName(14, "plus.16x16.png");
			this.il.Images.SetKeyName(15, "minus.16x16.png");
			this.il.Images.SetKeyName(16, "edit.16x16.png");
			this.il.Images.SetKeyName(17, "Help.16x16.png");
			this.il.Images.SetKeyName(18, "stop.16x16.png");
			this.il.Images.SetKeyName(19, "Service.ico");
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
			this.al.Actions.Add(this.aControlCC);
			this.al.Actions.Add(this.aControlInsIns);
			this.al.Actions.Add(this.aWebsite);
			this.al.Actions.Add(this.aFeedback);
			this.al.Actions.Add(this.aAddProfile);
			this.al.Actions.Add(this.aRemoveProfile);
			this.al.Actions.Add(this.aSetProfileProperties);
			this.al.Actions.Add(this.aEditProfileServices);
			this.al.Actions.Add(this.aShowProfileServices);
			this.al.Actions.Add(this.aShowProfileLanguages);
			this.al.Actions.Add(this.aShowProfileSubjects);
			this.al.Actions.Add(this.aNextProfile);
			this.al.Actions.Add(this.aPreviousProfile);
			this.al.Actions.Add(this.aShowStatistics);
			this.al.Actions.Add(this.aShowErrors);
			this.al.Actions.Add(this.aHideWithoutResult);
			this.al.Actions.Add(this.aShowTranslationDirection);
			this.al.Actions.Add(this.aShowServiceName);
			this.al.Actions.Add(this.aShowAccents);
			this.al.Actions.Add(this.aIncludeMonolingualDicts);
			this.al.Actions.Add(this.aDonate);
			this.al.Actions.Add(this.aOnlineHelp);
			this.al.Actions.Add(this.aStopTranslate);
			this.al.Actions.Add(this.aFilterLanguages);
			this.al.Actions.Add(this.aSearchInGoogle);
			this.al.Actions.Add(this.aPlaceResultViewTop);
			this.al.Actions.Add(this.aPlaceResultViewBottom);
			this.al.Actions.Add(this.aPlaceResultViewLeft);
			this.al.Actions.Add(this.aPlaceResultViewRight);
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
									this.tsIconSep2,
									this.miAbout2,
									this.tsIconSep3,
									this.miControlCC,
									this.miControlInsIns,
									this.tsIconSep1,
									this.miIconExit});
			this.msIcon.Name = "msIcon";
			this.msIcon.Size = new System.Drawing.Size(157, 132);
			// 
			// miShowHide
			// 
			this.al.SetAction(this.miShowHide, this.aShowMainForm);
			this.miShowHide.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowHide.Name = "miShowHide";
			this.miShowHide.Size = new System.Drawing.Size(156, 22);
			this.miShowHide.Text = "Show/Hide";
			// 
			// tsIconSep2
			// 
			this.tsIconSep2.Name = "tsIconSep2";
			this.tsIconSep2.Size = new System.Drawing.Size(153, 6);
			// 
			// miAbout2
			// 
			this.miAbout2.Image = ((System.Drawing.Image)(resources.GetObject("miAbout2.Image")));
			this.miAbout2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miAbout2.Name = "miAbout2";
			this.miAbout2.Size = new System.Drawing.Size(156, 22);
			this.miAbout2.Text = "&About ...";
			// 
			// tsIconSep3
			// 
			this.tsIconSep3.Name = "tsIconSep3";
			this.tsIconSep3.Size = new System.Drawing.Size(153, 6);
			// 
			// miControlCC
			// 
			this.al.SetAction(this.miControlCC, this.aControlCC);
			this.miControlCC.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miControlCC.Name = "miControlCC";
			this.miControlCC.Size = new System.Drawing.Size(156, 22);
			this.miControlCC.Text = "aControlCC";
			// 
			// miControlInsIns
			// 
			this.al.SetAction(this.miControlInsIns, this.aControlInsIns);
			this.miControlInsIns.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miControlInsIns.Name = "miControlInsIns";
			this.miControlInsIns.Size = new System.Drawing.Size(156, 22);
			this.miControlInsIns.Text = "aControlInsIns";
			// 
			// tsIconSep1
			// 
			this.tsIconSep1.Name = "tsIconSep1";
			this.tsIconSep1.Size = new System.Drawing.Size(153, 6);
			// 
			// miIconExit
			// 
			this.miIconExit.Image = ((System.Drawing.Image)(resources.GetObject("miIconExit.Image")));
			this.miIconExit.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miIconExit.Name = "miIconExit";
			this.miIconExit.ShortcutKeyDisplayString = "Alt+F4";
			this.miIconExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.miIconExit.Size = new System.Drawing.Size(156, 22);
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
			this.languageSelector.Phrase = null;
			this.languageSelector.Profile = null;
			this.languageSelector.Size = new System.Drawing.Size(200, 344);
			this.languageSelector.TabIndex = 0;
			this.languageSelector.SelectionChanged += new System.EventHandler(this.LanguageSelectorSelectionChanged);
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
			this.resBrowser.Size = new System.Drawing.Size(386, 189);
			this.resBrowser.TabIndex = 3;
			this.resBrowser.StatusTextChanged += new System.EventHandler(this.ResBrowserStatusTextChanged);
			// 
			// msBrowser
			// 
			this.msBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miBrowserTranslateSel,
									this.miSearchInGoogle,
									this.miBrowserSep2,
									this.miBrowserCopy,
									this.miBrowserSelectAll,
									this.miBrowserSep1,
									this.miShowSourceToolStripMenuItem});
			this.msBrowser.Name = "browserMenu";
			this.msBrowser.Size = new System.Drawing.Size(177, 126);
			this.msBrowser.Opening += new System.ComponentModel.CancelEventHandler(this.MsBrowserOpening);
			// 
			// miBrowserTranslateSel
			// 
			this.miBrowserTranslateSel.Name = "miBrowserTranslateSel";
			this.miBrowserTranslateSel.Size = new System.Drawing.Size(176, 22);
			this.miBrowserTranslateSel.Text = "Translate selection";
			// 
			// miSearchInGoogle
			// 
			this.miSearchInGoogle.Image = ((System.Drawing.Image)(resources.GetObject("miSearchInGoogle.Image")));
			this.miSearchInGoogle.Name = "miSearchInGoogle";
			this.miSearchInGoogle.Size = new System.Drawing.Size(176, 22);
			this.miSearchInGoogle.Text = "miSearchInGoogle";
			this.miSearchInGoogle.Click += new System.EventHandler(this.MiSearchInGoogleClick);
			// 
			// miBrowserSep2
			// 
			this.miBrowserSep2.Name = "miBrowserSep2";
			this.miBrowserSep2.Size = new System.Drawing.Size(173, 6);
			// 
			// miBrowserCopy
			// 
			this.miBrowserCopy.Image = ((System.Drawing.Image)(resources.GetObject("miBrowserCopy.Image")));
			this.miBrowserCopy.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miBrowserCopy.Name = "miBrowserCopy";
			this.miBrowserCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.miBrowserCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.miBrowserCopy.Size = new System.Drawing.Size(176, 22);
			this.miBrowserCopy.Text = "&Copy";
			this.miBrowserCopy.ToolTipText = "Copy selection to clipboard";
			// 
			// miBrowserSelectAll
			// 
			this.miBrowserSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("miBrowserSelectAll.Image")));
			this.miBrowserSelectAll.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miBrowserSelectAll.Name = "miBrowserSelectAll";
			this.miBrowserSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
			this.miBrowserSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.miBrowserSelectAll.Size = new System.Drawing.Size(176, 22);
			this.miBrowserSelectAll.Text = "&Select Аll";
			this.miBrowserSelectAll.ToolTipText = "Select Аll";
			// 
			// miBrowserSep1
			// 
			this.miBrowserSep1.Name = "miBrowserSep1";
			this.miBrowserSep1.Size = new System.Drawing.Size(173, 6);
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
			this.splitterTranslate.Location = new System.Drawing.Point(0, 189);
			this.splitterTranslate.Name = "splitterTranslate";
			this.splitterTranslate.Size = new System.Drawing.Size(386, 5);
			this.splitterTranslate.TabIndex = 2;
			this.splitterTranslate.TabStop = false;
			// 
			// tbFrom
			// 
			this.tbFrom.AcceptsReturn = true;
			this.tbFrom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tbFrom.Location = new System.Drawing.Point(0, 194);
			this.tbFrom.MaxLength = 0;
			this.tbFrom.Multiline = true;
			this.tbFrom.Name = "tbFrom";
			this.tbFrom.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbFrom.Size = new System.Drawing.Size(386, 127);
			this.tbFrom.TabIndex = 1;
			this.tbFrom.TextChanged += new System.EventHandler(this.TbFromTextChanged);
			// 
			// tsTranslate
			// 
			this.tsTranslate.AllowItemReorder = true;
			this.tsTranslate.ContextMenuStrip = this.msProfileAdd;
			this.tsTranslate.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tsTranslate.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsTranslate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbTranslate,
									this.tsbSearchInGoogle,
									this.tsbStop,
									this.tsSeparatorTranslate});
			this.tsTranslate.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.tsTranslate.Location = new System.Drawing.Point(0, 321);
			this.tsTranslate.Name = "tsTranslate";
			this.tsTranslate.Size = new System.Drawing.Size(386, 23);
			this.tsTranslate.TabIndex = 0;
			// 
			// msProfileAdd
			// 
			this.msProfileAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miAddProfile,
									this.tsSepProfiles4,
									this.miRemoveProfile,
									this.tsSepProfiles3,
									this.miSelectedProfile,
									this.miProfileView,
									this.miIncludeMonolingualDicts2,
									this.miSetProfileProperties,
									this.miEditProfileServices,
									this.miFilterLanguages});
			this.msProfileAdd.Name = "msProfileAdd";
			this.msProfileAdd.Size = new System.Drawing.Size(200, 192);
			// 
			// miAddProfile
			// 
			this.al.SetAction(this.miAddProfile, this.aAddProfile);
			this.miAddProfile.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miAddProfile.Name = "miAddProfile";
			this.miAddProfile.Size = new System.Drawing.Size(199, 22);
			this.miAddProfile.Text = "Add new user profile";
			this.miAddProfile.ToolTipText = "Add new user profile";
			// 
			// tsSepProfiles4
			// 
			this.tsSepProfiles4.Name = "tsSepProfiles4";
			this.tsSepProfiles4.Size = new System.Drawing.Size(196, 6);
			// 
			// miRemoveProfile
			// 
			this.al.SetAction(this.miRemoveProfile, this.aRemoveProfile);
			this.miRemoveProfile.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miRemoveProfile.Name = "miRemoveProfile";
			this.miRemoveProfile.Size = new System.Drawing.Size(199, 22);
			this.miRemoveProfile.Text = "Remove user profile";
			this.miRemoveProfile.ToolTipText = "Remove user profile";
			// 
			// tsSepProfiles3
			// 
			this.tsSepProfiles3.Name = "tsSepProfiles3";
			this.tsSepProfiles3.Size = new System.Drawing.Size(196, 6);
			// 
			// miSelectedProfile
			// 
			this.miSelectedProfile.Enabled = false;
			this.miSelectedProfile.Name = "miSelectedProfile";
			this.miSelectedProfile.Size = new System.Drawing.Size(199, 22);
			this.miSelectedProfile.Text = "Profile";
			// 
			// miProfileView
			// 
			this.miProfileView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miShowProfileServices,
									this.miShowProfileLanguages,
									this.miShowProfileSubjects});
			this.miProfileView.Name = "miProfileView";
			this.miProfileView.Size = new System.Drawing.Size(199, 22);
			this.miProfileView.Text = "View";
			// 
			// miShowProfileServices
			// 
			this.al.SetAction(this.miShowProfileServices, this.aShowProfileServices);
			this.miShowProfileServices.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowProfileServices.Name = "miShowProfileServices";
			this.miShowProfileServices.Size = new System.Drawing.Size(179, 22);
			this.miShowProfileServices.Text = "Show services list";
			// 
			// miShowProfileLanguages
			// 
			this.al.SetAction(this.miShowProfileLanguages, this.aShowProfileLanguages);
			this.miShowProfileLanguages.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowProfileLanguages.Name = "miShowProfileLanguages";
			this.miShowProfileLanguages.Size = new System.Drawing.Size(179, 22);
			this.miShowProfileLanguages.Text = "Show languages list";
			// 
			// miShowProfileSubjects
			// 
			this.al.SetAction(this.miShowProfileSubjects, this.aShowProfileSubjects);
			this.miShowProfileSubjects.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowProfileSubjects.Name = "miShowProfileSubjects";
			this.miShowProfileSubjects.Size = new System.Drawing.Size(179, 22);
			this.miShowProfileSubjects.Text = "Show subjects list";
			// 
			// miIncludeMonolingualDicts2
			// 
			this.al.SetAction(this.miIncludeMonolingualDicts2, this.aIncludeMonolingualDicts);
			this.miIncludeMonolingualDicts2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miIncludeMonolingualDicts2.Name = "miIncludeMonolingualDicts2";
			this.miIncludeMonolingualDicts2.Size = new System.Drawing.Size(199, 22);
			this.miIncludeMonolingualDicts2.Text = "IncludeMonolingualDicts";
			// 
			// miSetProfileProperties
			// 
			this.al.SetAction(this.miSetProfileProperties, this.aSetProfileProperties);
			this.miSetProfileProperties.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miSetProfileProperties.Name = "miSetProfileProperties";
			this.miSetProfileProperties.Size = new System.Drawing.Size(199, 22);
			this.miSetProfileProperties.Text = "Set profile properies";
			// 
			// miEditProfileServices
			// 
			this.al.SetAction(this.miEditProfileServices, this.aEditProfileServices);
			this.miEditProfileServices.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditProfileServices.Name = "miEditProfileServices";
			this.miEditProfileServices.Size = new System.Drawing.Size(199, 22);
			this.miEditProfileServices.Text = "Edit profile services";
			// 
			// miFilterLanguages
			// 
			this.al.SetAction(this.miFilterLanguages, this.aFilterLanguages);
			this.miFilterLanguages.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miFilterLanguages.Name = "miFilterLanguages";
			this.miFilterLanguages.Size = new System.Drawing.Size(199, 22);
			this.miFilterLanguages.Text = "Filter of languages ...";
			this.miFilterLanguages.ToolTipText = "Allow to choose languages for use";
			// 
			// tsbTranslate
			// 
			this.al.SetAction(this.tsbTranslate, this.aTranslate);
			this.tsbTranslate.AutoToolTip = false;
			this.tsbTranslate.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.tsbTranslate.Name = "tsbTranslate";
			this.tsbTranslate.Size = new System.Drawing.Size(56, 17);
			this.tsbTranslate.Text = "Translate";
			this.tsbTranslate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.tsbTranslate.LocationChanged += new System.EventHandler(this.TsbTranslateLocationChanged);
			// 
			// tsbSearchInGoogle
			// 
			this.al.SetAction(this.tsbSearchInGoogle, this.aSearchInGoogle);
			this.tsbSearchInGoogle.AutoToolTip = false;
			this.tsbSearchInGoogle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSearchInGoogle.Image = ((System.Drawing.Image)(resources.GetObject("tsbSearchInGoogle.Image")));
			this.tsbSearchInGoogle.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.tsbSearchInGoogle.Name = "tsbSearchInGoogle";
			this.tsbSearchInGoogle.Size = new System.Drawing.Size(23, 20);
			this.tsbSearchInGoogle.ToolTipText = "Search in Google";
			// 
			// tsbStop
			// 
			this.al.SetAction(this.tsbStop, this.aStopTranslate);
			this.tsbStop.AutoToolTip = false;
			this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStop.Enabled = false;
			this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
			this.tsbStop.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.tsbStop.Name = "tsbStop";
			this.tsbStop.Size = new System.Drawing.Size(23, 20);
			this.tsbStop.LocationChanged += new System.EventHandler(this.TsbTranslateLocationChanged);
			// 
			// tsSeparatorTranslate
			// 
			this.tsSeparatorTranslate.Name = "tsSeparatorTranslate";
			this.tsSeparatorTranslate.Size = new System.Drawing.Size(6, 23);
			this.tsSeparatorTranslate.LocationChanged += new System.EventHandler(this.TsbTranslateLocationChanged);
			// 
			// miProfiles
			// 
			this.miProfiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miAddProfile2,
									this.tsSepProfiles2,
									this.miRemoveProfile2,
									this.tsSepProfiles,
									this.miProfileView2,
									this.miIncludeMonolingualDicts,
									this.miEditProfileProperties2,
									this.miEditProfileServices2,
									this.miFilterLanguages2});
			this.miProfiles.Name = "miProfiles";
			this.miProfiles.Size = new System.Drawing.Size(54, 20);
			this.miProfiles.Text = "Profiles";
			// 
			// miAddProfile2
			// 
			this.al.SetAction(this.miAddProfile2, this.aAddProfile);
			this.miAddProfile2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miAddProfile2.Name = "miAddProfile2";
			this.miAddProfile2.Size = new System.Drawing.Size(199, 22);
			this.miAddProfile2.Text = "Add new user profile";
			this.miAddProfile2.ToolTipText = "Add new user profile";
			// 
			// tsSepProfiles2
			// 
			this.tsSepProfiles2.Name = "tsSepProfiles2";
			this.tsSepProfiles2.Size = new System.Drawing.Size(196, 6);
			// 
			// miRemoveProfile2
			// 
			this.al.SetAction(this.miRemoveProfile2, this.aRemoveProfile);
			this.miRemoveProfile2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miRemoveProfile2.Name = "miRemoveProfile2";
			this.miRemoveProfile2.Size = new System.Drawing.Size(199, 22);
			this.miRemoveProfile2.Text = "Remove user profile";
			this.miRemoveProfile2.ToolTipText = "Remove user profile";
			// 
			// tsSepProfiles
			// 
			this.tsSepProfiles.Name = "tsSepProfiles";
			this.tsSepProfiles.Size = new System.Drawing.Size(196, 6);
			// 
			// miProfileView2
			// 
			this.miProfileView2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miShowProfileServices2,
									this.miShowProfileLanguages2,
									this.miShowProfileSubjects2});
			this.miProfileView2.Name = "miProfileView2";
			this.miProfileView2.Size = new System.Drawing.Size(199, 22);
			this.miProfileView2.Text = "View";
			// 
			// miShowProfileServices2
			// 
			this.al.SetAction(this.miShowProfileServices2, this.aShowProfileServices);
			this.miShowProfileServices2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowProfileServices2.Name = "miShowProfileServices2";
			this.miShowProfileServices2.Size = new System.Drawing.Size(179, 22);
			this.miShowProfileServices2.Text = "Show services list";
			// 
			// miShowProfileLanguages2
			// 
			this.al.SetAction(this.miShowProfileLanguages2, this.aShowProfileLanguages);
			this.miShowProfileLanguages2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowProfileLanguages2.Name = "miShowProfileLanguages2";
			this.miShowProfileLanguages2.Size = new System.Drawing.Size(179, 22);
			this.miShowProfileLanguages2.Text = "Show languages list";
			// 
			// miShowProfileSubjects2
			// 
			this.al.SetAction(this.miShowProfileSubjects2, this.aShowProfileSubjects);
			this.miShowProfileSubjects2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowProfileSubjects2.Name = "miShowProfileSubjects2";
			this.miShowProfileSubjects2.Size = new System.Drawing.Size(179, 22);
			this.miShowProfileSubjects2.Text = "Show subjects list";
			// 
			// miIncludeMonolingualDicts
			// 
			this.al.SetAction(this.miIncludeMonolingualDicts, this.aIncludeMonolingualDicts);
			this.miIncludeMonolingualDicts.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miIncludeMonolingualDicts.Name = "miIncludeMonolingualDicts";
			this.miIncludeMonolingualDicts.Size = new System.Drawing.Size(199, 22);
			this.miIncludeMonolingualDicts.Text = "IncludeMonolingualDicts";
			// 
			// miEditProfileProperties2
			// 
			this.al.SetAction(this.miEditProfileProperties2, this.aSetProfileProperties);
			this.miEditProfileProperties2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditProfileProperties2.Name = "miEditProfileProperties2";
			this.miEditProfileProperties2.Size = new System.Drawing.Size(199, 22);
			this.miEditProfileProperties2.Text = "Set profile properies";
			// 
			// miEditProfileServices2
			// 
			this.al.SetAction(this.miEditProfileServices2, this.aEditProfileServices);
			this.miEditProfileServices2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miEditProfileServices2.Name = "miEditProfileServices2";
			this.miEditProfileServices2.Size = new System.Drawing.Size(199, 22);
			this.miEditProfileServices2.Text = "Edit profile services";
			// 
			// miFilterLanguages2
			// 
			this.al.SetAction(this.miFilterLanguages2, this.aFilterLanguages);
			this.miFilterLanguages2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miFilterLanguages2.Name = "miFilterLanguages2";
			this.miFilterLanguages2.Size = new System.Drawing.Size(199, 22);
			this.miFilterLanguages2.Text = "Filter of languages ...";
			this.miFilterLanguages2.ToolTipText = "Allow to choose languages for use";
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
			this.aInvertTranslationDirection.Update += new System.EventHandler(this.AInvertTranslationDirectionUpdate);
			// 
			// miAnimatedIcon
			// 
			this.miAnimatedIcon.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.miAnimatedIcon.AutoToolTip = true;
			this.miAnimatedIcon.Image = ((System.Drawing.Image)(resources.GetObject("miAnimatedIcon.Image")));
			this.miAnimatedIcon.Name = "miAnimatedIcon";
			this.miAnimatedIcon.Size = new System.Drawing.Size(28, 20);
			this.miAnimatedIcon.Click += new System.EventHandler(this.AWebsiteExecute);
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
			this.miCheckUpdates.Size = new System.Drawing.Size(235, 22);
			this.miCheckUpdates.Text = "Check Updates ...";
			// 
			// miHelpSeparator1
			// 
			this.miHelpSeparator1.Name = "miHelpSeparator1";
			this.miHelpSeparator1.Size = new System.Drawing.Size(232, 6);
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
			this.lSelectedLangsPair.Size = new System.Drawing.Size(0, 17);
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
			// aControlCC
			// 
			this.aControlCC.Checked = false;
			this.aControlCC.Enabled = true;
			this.aControlCC.Hint = null;
			this.aControlCC.Tag = null;
			this.aControlCC.Text = "aControlCC";
			this.aControlCC.Visible = true;
			this.aControlCC.Execute += new System.EventHandler(this.AControlCCExecute);
			// 
			// aControlInsIns
			// 
			this.aControlInsIns.Checked = false;
			this.aControlInsIns.Enabled = true;
			this.aControlInsIns.Hint = null;
			this.aControlInsIns.Tag = null;
			this.aControlInsIns.Text = "aControlInsIns";
			this.aControlInsIns.Visible = true;
			this.aControlInsIns.Execute += new System.EventHandler(this.AControlInsInsExecute);
			this.aControlInsIns.Update += new System.EventHandler(this.AControlInsInsUpdate);
			// 
			// miHelpSeparator2
			// 
			this.miHelpSeparator2.Name = "miHelpSeparator2";
			this.miHelpSeparator2.Size = new System.Drawing.Size(232, 6);
			// 
			// miWebsite
			// 
			this.al.SetAction(this.miWebsite, this.aWebsite);
			this.miWebsite.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miWebsite.Name = "miWebsite";
			this.miWebsite.Size = new System.Drawing.Size(235, 22);
			this.miWebsite.Text = "Translate.Net Website";
			// 
			// aWebsite
			// 
			this.aWebsite.Checked = false;
			this.aWebsite.Enabled = true;
			this.aWebsite.Hint = null;
			this.aWebsite.ImageIndex = 12;
			this.aWebsite.Tag = null;
			this.aWebsite.Text = "Translate.Net Website";
			this.aWebsite.Visible = true;
			this.aWebsite.Execute += new System.EventHandler(this.AWebsiteExecute);
			// 
			// aFeedback
			// 
			this.aFeedback.Checked = false;
			this.aFeedback.Enabled = true;
			this.aFeedback.Hint = null;
			this.aFeedback.Tag = null;
			this.aFeedback.Text = "Send feedback or bugreport ...";
			this.aFeedback.Visible = true;
			this.aFeedback.Execute += new System.EventHandler(this.AFeedbackExecute);
			// 
			// miFeedback
			// 
			this.al.SetAction(this.miFeedback, this.aFeedback);
			this.miFeedback.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miFeedback.Name = "miFeedback";
			this.miFeedback.Size = new System.Drawing.Size(235, 22);
			this.miFeedback.Text = "Send feedback or bugreport ...";
			// 
			// aAddProfile
			// 
			this.aAddProfile.Checked = false;
			this.aAddProfile.Enabled = true;
			this.aAddProfile.Hint = "Add new user profile";
			this.aAddProfile.ImageIndex = 14;
			this.aAddProfile.Tag = null;
			this.aAddProfile.Text = "Add new user profile";
			this.aAddProfile.Visible = true;
			this.aAddProfile.Execute += new System.EventHandler(this.AAddProfileExecute);
			// 
			// aRemoveProfile
			// 
			this.aRemoveProfile.Checked = false;
			this.aRemoveProfile.Enabled = true;
			this.aRemoveProfile.Hint = "Remove user profile";
			this.aRemoveProfile.ImageIndex = 15;
			this.aRemoveProfile.Tag = null;
			this.aRemoveProfile.Text = "Remove user profile";
			this.aRemoveProfile.Visible = true;
			this.aRemoveProfile.Execute += new System.EventHandler(this.ARemoveProfileExecute);
			this.aRemoveProfile.Update += new System.EventHandler(this.ARemoveProfileUpdate);
			// 
			// aSetProfileProperties
			// 
			this.aSetProfileProperties.Checked = false;
			this.aSetProfileProperties.Enabled = true;
			this.aSetProfileProperties.Hint = null;
			this.aSetProfileProperties.Tag = null;
			this.aSetProfileProperties.Text = "Set profile properies";
			this.aSetProfileProperties.Visible = true;
			this.aSetProfileProperties.Execute += new System.EventHandler(this.ASetProfilePropertiesExecute);
			// 
			// aEditProfileServices
			// 
			this.aEditProfileServices.Checked = false;
			this.aEditProfileServices.Enabled = true;
			this.aEditProfileServices.Hint = null;
			this.aEditProfileServices.ImageIndex = 16;
			this.aEditProfileServices.Tag = null;
			this.aEditProfileServices.Text = "Edit profile services";
			this.aEditProfileServices.Visible = true;
			this.aEditProfileServices.Execute += new System.EventHandler(this.AEditProfileServicesExecute);
			// 
			// aShowProfileServices
			// 
			this.aShowProfileServices.Checked = false;
			this.aShowProfileServices.Enabled = true;
			this.aShowProfileServices.Hint = null;
			this.aShowProfileServices.Tag = null;
			this.aShowProfileServices.Text = "Show services list";
			this.aShowProfileServices.Visible = true;
			this.aShowProfileServices.Execute += new System.EventHandler(this.AShowProfileServicesExecute);
			// 
			// aShowProfileLanguages
			// 
			this.aShowProfileLanguages.Checked = false;
			this.aShowProfileLanguages.Enabled = true;
			this.aShowProfileLanguages.Hint = null;
			this.aShowProfileLanguages.Tag = null;
			this.aShowProfileLanguages.Text = "Show languages list";
			this.aShowProfileLanguages.Visible = true;
			this.aShowProfileLanguages.Execute += new System.EventHandler(this.AShowProfileLanguagesExecute);
			// 
			// aShowProfileSubjects
			// 
			this.aShowProfileSubjects.Checked = false;
			this.aShowProfileSubjects.Enabled = true;
			this.aShowProfileSubjects.Hint = null;
			this.aShowProfileSubjects.Tag = null;
			this.aShowProfileSubjects.Text = "Show subjects list";
			this.aShowProfileSubjects.Visible = true;
			this.aShowProfileSubjects.Execute += new System.EventHandler(this.AShowProfileSubjectsExecute);
			// 
			// aNextProfile
			// 
			this.aNextProfile.Checked = false;
			this.aNextProfile.Enabled = true;
			this.aNextProfile.Hint = null;
			this.aNextProfile.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)));
			this.aNextProfile.Tag = null;
			this.aNextProfile.Text = "Next profile";
			this.aNextProfile.Visible = true;
			this.aNextProfile.Execute += new System.EventHandler(this.ANextProfileExecute);
			this.aNextProfile.Update += new System.EventHandler(this.ANextProfileUpdate);
			// 
			// aPreviousProfile
			// 
			this.aPreviousProfile.Checked = false;
			this.aPreviousProfile.Enabled = true;
			this.aPreviousProfile.Hint = null;
			this.aPreviousProfile.Shortcut = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
									| System.Windows.Forms.Keys.Tab)));
			this.aPreviousProfile.Tag = null;
			this.aPreviousProfile.Text = "Previous profile";
			this.aPreviousProfile.Visible = true;
			this.aPreviousProfile.Execute += new System.EventHandler(this.APreviousProfileExecute);
			// 
			// miView
			// 
			this.miView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miResultViewPlace,
									this.toolStripSeparator3,
									this.miShowAccents,
									this.miMarkErrors,
									this.toolStripSeparator1,
									this.miShowTranslateDirection,
									this.miShowServiceNames,
									this.miShowStats,
									this.toolStripSeparator2,
									this.miHideWithoutResult});
			this.miView.Name = "miView";
			this.miView.Size = new System.Drawing.Size(41, 20);
			this.miView.Text = "View";
			// 
			// miResultViewPlace
			// 
			this.miResultViewPlace.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miResultViewPlaceTop,
									this.miResultViewPlaceBottom,
									this.toolStripSeparator4,
									this.leftToolStripMenuItem,
									this.rightToolStripMenuItem});
			this.miResultViewPlace.Name = "miResultViewPlace";
			this.miResultViewPlace.Size = new System.Drawing.Size(272, 22);
			this.miResultViewPlace.Text = "Result view placement";
			// 
			// miResultViewPlaceTop
			// 
			this.al.SetAction(this.miResultViewPlaceTop, this.aPlaceResultViewTop);
			this.miResultViewPlaceTop.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miResultViewPlaceTop.Name = "miResultViewPlaceTop";
			this.miResultViewPlaceTop.Size = new System.Drawing.Size(119, 22);
			this.miResultViewPlaceTop.Text = "Top";
			this.miResultViewPlaceTop.ToolTipText = "Dock result view at top";
			// 
			// miResultViewPlaceBottom
			// 
			this.al.SetAction(this.miResultViewPlaceBottom, this.aPlaceResultViewBottom);
			this.miResultViewPlaceBottom.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miResultViewPlaceBottom.Name = "miResultViewPlaceBottom";
			this.miResultViewPlaceBottom.Size = new System.Drawing.Size(119, 22);
			this.miResultViewPlaceBottom.Text = "Bottom";
			this.miResultViewPlaceBottom.ToolTipText = "Dock result view at bottom";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(116, 6);
			// 
			// leftToolStripMenuItem
			// 
			this.al.SetAction(this.leftToolStripMenuItem, this.aPlaceResultViewLeft);
			this.leftToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
			this.leftToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.leftToolStripMenuItem.Text = "Left";
			// 
			// rightToolStripMenuItem
			// 
			this.al.SetAction(this.rightToolStripMenuItem, this.aPlaceResultViewRight);
			this.rightToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
			this.rightToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.rightToolStripMenuItem.Text = "Right";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(269, 6);
			// 
			// miShowAccents
			// 
			this.al.SetAction(this.miShowAccents, this.aShowAccents);
			this.miShowAccents.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowAccents.Name = "miShowAccents";
			this.miShowAccents.Size = new System.Drawing.Size(272, 22);
			this.miShowAccents.Text = "Show accents";
			// 
			// miMarkErrors
			// 
			this.al.SetAction(this.miMarkErrors, this.aShowErrors);
			this.miMarkErrors.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miMarkErrors.Name = "miMarkErrors";
			this.miMarkErrors.Size = new System.Drawing.Size(272, 22);
			this.miMarkErrors.Text = "Mark by red color untranslated words";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(269, 6);
			// 
			// miShowTranslateDirection
			// 
			this.al.SetAction(this.miShowTranslateDirection, this.aShowTranslationDirection);
			this.miShowTranslateDirection.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowTranslateDirection.Name = "miShowTranslateDirection";
			this.miShowTranslateDirection.Size = new System.Drawing.Size(272, 22);
			this.miShowTranslateDirection.Text = "Show direction of translation";
			// 
			// miShowServiceNames
			// 
			this.al.SetAction(this.miShowServiceNames, this.aShowServiceName);
			this.miShowServiceNames.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowServiceNames.Name = "miShowServiceNames";
			this.miShowServiceNames.Size = new System.Drawing.Size(272, 22);
			this.miShowServiceNames.Text = "Show names of services";
			// 
			// miShowStats
			// 
			this.al.SetAction(this.miShowStats, this.aShowStatistics);
			this.miShowStats.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miShowStats.Name = "miShowStats";
			this.miShowStats.Size = new System.Drawing.Size(272, 22);
			this.miShowStats.Text = "Show query time and other information";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(269, 6);
			// 
			// miHideWithoutResult
			// 
			this.al.SetAction(this.miHideWithoutResult, this.aHideWithoutResult);
			this.miHideWithoutResult.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miHideWithoutResult.Name = "miHideWithoutResult";
			this.miHideWithoutResult.Size = new System.Drawing.Size(272, 22);
			this.miHideWithoutResult.Text = "Don\'t show \"Nothing found\" results";
			// 
			// aShowStatistics
			// 
			this.aShowStatistics.Checked = false;
			this.aShowStatistics.Enabled = true;
			this.aShowStatistics.Hint = null;
			this.aShowStatistics.Tag = null;
			this.aShowStatistics.Text = "Show query time and other information";
			this.aShowStatistics.Visible = true;
			this.aShowStatistics.Execute += new System.EventHandler(this.AShowStatisticsExecute);
			// 
			// aShowErrors
			// 
			this.aShowErrors.Checked = false;
			this.aShowErrors.Enabled = true;
			this.aShowErrors.Hint = null;
			this.aShowErrors.Tag = null;
			this.aShowErrors.Text = "Mark by red color untranslated words";
			this.aShowErrors.Visible = true;
			this.aShowErrors.Execute += new System.EventHandler(this.AShowErrorsExecute);
			// 
			// aHideWithoutResult
			// 
			this.aHideWithoutResult.Checked = false;
			this.aHideWithoutResult.Enabled = true;
			this.aHideWithoutResult.Hint = null;
			this.aHideWithoutResult.Tag = null;
			this.aHideWithoutResult.Text = "Don\'t show \"Nothing found\" results";
			this.aHideWithoutResult.Visible = true;
			this.aHideWithoutResult.Execute += new System.EventHandler(this.AHideWithoutResultExecute);
			this.aHideWithoutResult.Update += new System.EventHandler(this.AHideWithoutResultUpdate);
			// 
			// aShowTranslationDirection
			// 
			this.aShowTranslationDirection.Checked = false;
			this.aShowTranslationDirection.Enabled = true;
			this.aShowTranslationDirection.Hint = null;
			this.aShowTranslationDirection.Tag = null;
			this.aShowTranslationDirection.Text = "Show direction of translation";
			this.aShowTranslationDirection.Visible = true;
			this.aShowTranslationDirection.Execute += new System.EventHandler(this.AShowTranslationDirectionExecute);
			// 
			// aShowServiceName
			// 
			this.aShowServiceName.Checked = false;
			this.aShowServiceName.Enabled = true;
			this.aShowServiceName.Hint = null;
			this.aShowServiceName.Tag = null;
			this.aShowServiceName.Text = "Show names of services";
			this.aShowServiceName.Visible = true;
			this.aShowServiceName.Execute += new System.EventHandler(this.AShowServiceNameExecute);
			// 
			// aShowAccents
			// 
			this.aShowAccents.Checked = false;
			this.aShowAccents.Enabled = true;
			this.aShowAccents.Hint = null;
			this.aShowAccents.Tag = null;
			this.aShowAccents.Text = "Show accents";
			this.aShowAccents.Visible = true;
			this.aShowAccents.Execute += new System.EventHandler(this.AShowAccentsExecute);
			// 
			// aIncludeMonolingualDicts
			// 
			this.aIncludeMonolingualDicts.Checked = false;
			this.aIncludeMonolingualDicts.Enabled = true;
			this.aIncludeMonolingualDicts.Hint = null;
			this.aIncludeMonolingualDicts.Tag = null;
			this.aIncludeMonolingualDicts.Text = "IncludeMonolingualDicts";
			this.aIncludeMonolingualDicts.Visible = true;
			this.aIncludeMonolingualDicts.Execute += new System.EventHandler(this.AIncludeMonolingualDictsExecute);
			this.aIncludeMonolingualDicts.Update += new System.EventHandler(this.AIncludeMonolingualDictsUpdate);
			// 
			// aDonate
			// 
			this.aDonate.Checked = false;
			this.aDonate.Enabled = true;
			this.aDonate.Hint = null;
			this.aDonate.Tag = null;
			this.aDonate.Text = "Donate ...";
			this.aDonate.Visible = true;
			this.aDonate.Execute += new System.EventHandler(this.ADonateExecute);
			// 
			// miDonate
			// 
			this.al.SetAction(this.miDonate, this.aDonate);
			this.miDonate.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miDonate.Name = "miDonate";
			this.miDonate.Size = new System.Drawing.Size(235, 22);
			this.miDonate.Text = "Donate ...";
			// 
			// miOnlineHelp
			// 
			this.al.SetAction(this.miOnlineHelp, this.aOnlineHelp);
			this.miOnlineHelp.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miOnlineHelp.Name = "miOnlineHelp";
			this.miOnlineHelp.ShortcutKeyDisplayString = "F1";
			this.miOnlineHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.miOnlineHelp.Size = new System.Drawing.Size(235, 22);
			this.miOnlineHelp.Text = "Online Help";
			// 
			// aOnlineHelp
			// 
			this.aOnlineHelp.Checked = false;
			this.aOnlineHelp.Enabled = true;
			this.aOnlineHelp.Hint = null;
			this.aOnlineHelp.ImageIndex = 17;
			this.aOnlineHelp.Shortcut = System.Windows.Forms.Keys.F1;
			this.aOnlineHelp.Tag = null;
			this.aOnlineHelp.Text = "Online Help";
			this.aOnlineHelp.Visible = true;
			this.aOnlineHelp.Execute += new System.EventHandler(this.AOnlineHelpExecute);
			// 
			// timerRecheckServices
			// 
			this.timerRecheckServices.Interval = 700;
			this.timerRecheckServices.Tick += new System.EventHandler(this.TimerRecheckServicesTick);
			// 
			// aStopTranslate
			// 
			this.aStopTranslate.Checked = false;
			this.aStopTranslate.Enabled = false;
			this.aStopTranslate.Hint = null;
			this.aStopTranslate.ImageIndex = 18;
			this.aStopTranslate.Shortcut = System.Windows.Forms.Keys.Escape;
			this.aStopTranslate.Tag = null;
			this.aStopTranslate.Text = "";
			this.aStopTranslate.Visible = true;
			this.aStopTranslate.Execute += new System.EventHandler(this.AStopTranslateExecute);
			// 
			// miOnlineHelp2
			// 
			this.miOnlineHelp2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.miOnlineHelp2.AutoToolTip = true;
			this.miOnlineHelp2.Image = ((System.Drawing.Image)(resources.GetObject("miOnlineHelp2.Image")));
			this.miOnlineHelp2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.miOnlineHelp2.Name = "miOnlineHelp2";
			this.miOnlineHelp2.ShortcutKeyDisplayString = "";
			this.miOnlineHelp2.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.miOnlineHelp2.Size = new System.Drawing.Size(28, 20);
			this.miOnlineHelp2.Click += new System.EventHandler(this.AOnlineHelpExecute);
			// 
			// aFilterLanguages
			// 
			this.aFilterLanguages.Checked = false;
			this.aFilterLanguages.Enabled = true;
			this.aFilterLanguages.Hint = "Allow to choose languages for use";
			this.aFilterLanguages.Tag = null;
			this.aFilterLanguages.Text = "Filter of languages ...";
			this.aFilterLanguages.Visible = true;
			this.aFilterLanguages.Execute += new System.EventHandler(this.AFilterLanguagesExecute);
			// 
			// aSearchInGoogle
			// 
			this.aSearchInGoogle.Checked = false;
			this.aSearchInGoogle.Enabled = true;
			this.aSearchInGoogle.Hint = "Search in Google";
			this.aSearchInGoogle.ImageIndex = 19;
			this.aSearchInGoogle.Shortcut = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
									| System.Windows.Forms.Keys.Return)));
			this.aSearchInGoogle.Tag = null;
			this.aSearchInGoogle.Text = "";
			this.aSearchInGoogle.Visible = true;
			this.aSearchInGoogle.Execute += new System.EventHandler(this.ASearchInGoogleExecute);
			this.aSearchInGoogle.Update += new System.EventHandler(this.ASearchInGoogleUpdate);
			// 
			// aPlaceResultViewTop
			// 
			this.aPlaceResultViewTop.Checked = false;
			this.aPlaceResultViewTop.Enabled = true;
			this.aPlaceResultViewTop.Hint = "Dock result view at top";
			this.aPlaceResultViewTop.Tag = null;
			this.aPlaceResultViewTop.Text = "Top";
			this.aPlaceResultViewTop.Visible = true;
			this.aPlaceResultViewTop.Execute += new System.EventHandler(this.APlaceResultViewTopExecute);
			this.aPlaceResultViewTop.Update += new System.EventHandler(this.APlaceResultViewTopUpdate);
			// 
			// aPlaceResultViewBottom
			// 
			this.aPlaceResultViewBottom.Checked = false;
			this.aPlaceResultViewBottom.Enabled = true;
			this.aPlaceResultViewBottom.Hint = "Dock result view at bottom";
			this.aPlaceResultViewBottom.Tag = null;
			this.aPlaceResultViewBottom.Text = "Bottom";
			this.aPlaceResultViewBottom.Visible = true;
			this.aPlaceResultViewBottom.Execute += new System.EventHandler(this.APlaceResultViewBottomExecute);
			// 
			// aPlaceResultViewLeft
			// 
			this.aPlaceResultViewLeft.Checked = false;
			this.aPlaceResultViewLeft.Enabled = true;
			this.aPlaceResultViewLeft.Hint = null;
			this.aPlaceResultViewLeft.Tag = null;
			this.aPlaceResultViewLeft.Text = "Left";
			this.aPlaceResultViewLeft.Visible = true;
			this.aPlaceResultViewLeft.Execute += new System.EventHandler(this.APlaceResultViewLeftExecute);
			// 
			// aPlaceResultViewRight
			// 
			this.aPlaceResultViewRight.Checked = false;
			this.aPlaceResultViewRight.Enabled = true;
			this.aPlaceResultViewRight.Hint = null;
			this.aPlaceResultViewRight.Tag = null;
			this.aPlaceResultViewRight.Text = "Right";
			this.aPlaceResultViewRight.Visible = true;
			this.aPlaceResultViewRight.Execute += new System.EventHandler(this.APlaceResultViewRightExecute);
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
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 400);
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
			this.msProfileAdd.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private FreeCL.UI.Actions.Action aPlaceResultViewRight;
		private FreeCL.UI.Actions.Action aPlaceResultViewLeft;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem miResultViewPlaceBottom;
		private System.Windows.Forms.ToolStripMenuItem miResultViewPlaceTop;
		private System.Windows.Forms.ToolStripMenuItem miResultViewPlace;
		private FreeCL.UI.Actions.Action aPlaceResultViewBottom;
		private FreeCL.UI.Actions.Action aPlaceResultViewTop;
		private System.Windows.Forms.ToolStripMenuItem miSearchInGoogle;
		private System.Windows.Forms.ToolStripButton tsbSearchInGoogle;
		private FreeCL.UI.Actions.Action aSearchInGoogle;
		private System.Windows.Forms.ToolStripMenuItem miFilterLanguages;
		private System.Windows.Forms.ToolStripMenuItem miFilterLanguages2;
		private FreeCL.UI.Actions.Action aFilterLanguages;
		private System.Windows.Forms.ToolStripMenuItem miBrowserTranslateSel;
		private System.Windows.Forms.ToolStripMenuItem miBrowserSelectAll;
		private System.Windows.Forms.ToolStripSeparator miBrowserSep1;
		private System.Windows.Forms.ToolStripMenuItem miBrowserCopy;
		private System.Windows.Forms.ToolStripSeparator miBrowserSep2;
		private System.Windows.Forms.ToolStripMenuItem miOnlineHelp2;
		private System.Windows.Forms.ToolStripButton tsbStop;
		private FreeCL.UI.Actions.Action aStopTranslate;
		private System.Windows.Forms.Timer timerRecheckServices;
		private System.Windows.Forms.ToolStripMenuItem miOnlineHelp;
		private FreeCL.UI.Actions.Action aOnlineHelp;
		private System.Windows.Forms.ToolStripMenuItem miDonate;
		private FreeCL.UI.Actions.Action aDonate;
		private System.Windows.Forms.ToolStripMenuItem miIncludeMonolingualDicts2;
		private System.Windows.Forms.ToolStripMenuItem miIncludeMonolingualDicts;
		private FreeCL.UI.Actions.Action aIncludeMonolingualDicts;
		private System.Windows.Forms.ToolStripMenuItem miHideWithoutResult;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem miShowStats;
		private System.Windows.Forms.ToolStripMenuItem miShowServiceNames;
		private System.Windows.Forms.ToolStripMenuItem miShowTranslateDirection;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miMarkErrors;
		private System.Windows.Forms.ToolStripMenuItem miShowAccents;
		private FreeCL.UI.Actions.Action aShowAccents;
		private FreeCL.UI.Actions.Action aShowServiceName;
		private FreeCL.UI.Actions.Action aShowTranslationDirection;
		private FreeCL.UI.Actions.Action aHideWithoutResult;
		private FreeCL.UI.Actions.Action aShowErrors;
		private FreeCL.UI.Actions.Action aShowStatistics;
		private System.Windows.Forms.ToolStripMenuItem miView;
		private FreeCL.UI.Actions.Action aPreviousProfile;
		private FreeCL.UI.Actions.Action aNextProfile;
		private System.Windows.Forms.ToolStripMenuItem miSelectedProfile;
		private System.Windows.Forms.ToolStripSeparator tsSepProfiles4;
		private System.Windows.Forms.ToolStripMenuItem miSetProfileProperties;
		private System.Windows.Forms.ToolStripMenuItem miEditProfileServices;
		private System.Windows.Forms.ToolStripMenuItem miShowProfileSubjects;
		private System.Windows.Forms.ToolStripMenuItem miShowProfileLanguages;
		private System.Windows.Forms.ToolStripMenuItem miShowProfileServices;
		private System.Windows.Forms.ToolStripMenuItem miProfileView;
		private System.Windows.Forms.ToolStripSeparator tsSepProfiles3;
		private System.Windows.Forms.ToolStripSeparator tsSepProfiles2;
		private System.Windows.Forms.ToolStripMenuItem miEditProfileProperties2;
		private System.Windows.Forms.ToolStripMenuItem miEditProfileServices2;
		private System.Windows.Forms.ToolStripMenuItem miShowProfileSubjects2;
		private System.Windows.Forms.ToolStripMenuItem miShowProfileLanguages2;
		private System.Windows.Forms.ToolStripMenuItem miShowProfileServices2;
		private System.Windows.Forms.ToolStripMenuItem miProfileView2;
		private System.Windows.Forms.ToolStripSeparator tsSepProfiles;
		private FreeCL.UI.Actions.Action aShowProfileSubjects;
		private FreeCL.UI.Actions.Action aShowProfileLanguages;
		private FreeCL.UI.Actions.Action aShowProfileServices;
		private FreeCL.UI.Actions.Action aEditProfileServices;
		private FreeCL.UI.Actions.Action aSetProfileProperties;
		private System.Windows.Forms.ToolStripMenuItem miRemoveProfile2;
		private System.Windows.Forms.ToolStripMenuItem miAddProfile2;
		private System.Windows.Forms.ToolStripMenuItem miProfiles;
		private System.Windows.Forms.ToolStripMenuItem miRemoveProfile;
		private System.Windows.Forms.ToolStripMenuItem miAddProfile;
		private System.Windows.Forms.ContextMenuStrip msProfileAdd;
		private FreeCL.UI.Actions.Action aAddProfile;
		private FreeCL.UI.Actions.Action aRemoveProfile;
		private System.Windows.Forms.ToolStripSeparator tsSeparatorTranslate;
		private System.Windows.Forms.ToolStripMenuItem miFeedback;
		private FreeCL.UI.Actions.Action aFeedback;
		private FreeCL.UI.Actions.Action aWebsite;
		private System.Windows.Forms.ToolStripMenuItem miWebsite;
		private System.Windows.Forms.ToolStripSeparator miHelpSeparator2;
		private System.Windows.Forms.ToolStripMenuItem miControlInsIns;
		private System.Windows.Forms.ToolStripMenuItem miControlCC;
		private System.Windows.Forms.ToolStripSeparator tsIconSep3;
		private System.Windows.Forms.ToolStripSeparator tsIconSep2;
		private FreeCL.UI.Actions.Action aControlInsIns;
		private FreeCL.UI.Actions.Action aControlCC;
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
		public System.Windows.Forms.TextBox tbFrom;
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
		private System.Windows.Forms.ToolStripMenuItem miAbout2;
		private System.Windows.Forms.ToolStripMenuItem miShowHide;
		private FreeCL.UI.Actions.Action aShowMainForm;
		private System.Windows.Forms.NotifyIcon niMain;
		private System.Windows.Forms.ToolStripMenuItem miIconExit;
		private System.Windows.Forms.ToolStripSeparator tsIconSep1;
		private System.Windows.Forms.ContextMenuStrip msIcon;
	}
}
