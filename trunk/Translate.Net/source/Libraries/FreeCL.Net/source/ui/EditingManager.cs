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
 * Portions created by the Initial Developer are Copyright (C) 2006-2008
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
using System.Reflection;

namespace FreeCL.UI
{

	public class EditingManagerEventArgs : EventArgs
	{
		bool handled;
		public bool Handled {
			get {
				return handled;
			}
			set {
				handled = value;
			}
		}
	}
	
	public class EditingManagerAskEventArgs : EventArgs
	{
		bool allowed;
		public bool Allowed {
			get {
				return allowed;
			}
			set {
				allowed = value;
			}
		}
	}
	
	/// <summary>
	/// The class to manage global editing actions (like often in menu edit)
	/// </summary>
	public static class EditingManager
	{
		static bool Execute(EventHandler<EditingManagerAskEventArgs> d)
		{
			if(d == null) return false;
			EditingManagerAskEventArgs e = new EditingManagerAskEventArgs();
			Delegate[] delegates = d.GetInvocationList();
			foreach(EventHandler<EditingManagerAskEventArgs> dg in delegates)
			{
				dg(null, e);
				if(e.Allowed)
					return true;
			}
			return false;
		}

		static bool Execute(EventHandler<EditingManagerEventArgs> d)
		{
			if(d == null) return false;
			EditingManagerEventArgs e = new EditingManagerEventArgs();
			Delegate[] delegates = d.GetInvocationList();
			foreach(EventHandler<EditingManagerEventArgs> dg in delegates)
			{
				dg(null, e);
				if(e.Handled)
					return true;
			}
			return false;
		}
		
		public static event EventHandler<EditingManagerAskEventArgs> OnCanUndo;
		
		public static bool CanUndo
		{
			get
			{
				bool res = Execute(OnCanUndo);
				if(!res)
				{
					Control activeControl = Application.ActiveControl;
					if(activeControl == null)
					{
						return false;
					}
				
					System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
					if(active != null)
					{
						res = active.CanUndo;
						return res;
					}
					
					if(!res)
					{
						PropertyInfo property = activeControl.GetType().GetProperty("CanUndo", typeof(bool));
						if(property != null)
						{
							MethodInfo method = property.GetGetMethod();
							if(method != null)
							{
								Clipboard.FunctionWithReturn<bool> getMethod = (Clipboard.FunctionWithReturn<bool>) Delegate.CreateDelegate
		            				(typeof(Clipboard.FunctionWithReturn<bool>), activeControl, method);
		            				
		            			res = getMethod();	
							}
						}
					}
				}
				return res;
			}
		}
		
		public static event EventHandler<EditingManagerEventArgs> OnUndo;		
		
		public static bool Undo()
		{
			bool res = Execute(OnUndo);
			if(!res)
			{
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}
			
				System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
				res = active != null;			
				if(res)
					active.Undo();
				else
				{
					MethodInfo method = activeControl.GetType().GetMethod("Undo");
					if(method != null)
					{
						Clipboard.FunctionWithoutReturn getMethod = (Clipboard.FunctionWithoutReturn) Delegate.CreateDelegate
		            		(typeof(Clipboard.FunctionWithoutReturn), activeControl, method);
		            				
		            	getMethod();	
						res = true;				
					}
				}
			}
			return res;
		}
		
		public static event EventHandler<EditingManagerAskEventArgs> OnCanRedo;
		
		public static bool CanRedo
		{
			get
			{
				bool res = Execute(OnCanRedo);
				if(!res)
				{
				}
				if(!res)
				{
					Control activeControl = Application.ActiveControl;
					if(activeControl == null)
					{
						return false;
					}
				
					System.Windows.Forms.RichTextBox active = activeControl as System.Windows.Forms.RichTextBox;	
					if(active != null)
					{
						res = active.CanRedo;
						return res;
					}
					
					if(!res)
					{
						PropertyInfo property = activeControl.GetType().GetProperty("CanRedo", typeof(bool));
						if(property != null)
						{
							MethodInfo method = property.GetGetMethod();
							if(method != null)
							{
								Clipboard.FunctionWithReturn<bool> getMethod = (Clipboard.FunctionWithReturn<bool>) Delegate.CreateDelegate
		            				(typeof(Clipboard.FunctionWithReturn<bool>), activeControl, method);
		            				
		            			res = getMethod();	
							}
						}
					}
				}
				return res;
			}
		}
		
		public static event EventHandler<EditingManagerEventArgs> OnRedo;		
		
		public static bool Redo()
		{
			bool res = Execute(OnRedo);
			if(!res)
			{
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}
			
				System.Windows.Forms.RichTextBox active = activeControl as System.Windows.Forms.RichTextBox;	
				res = active != null;			
				if(res)
					active.Redo();
				else
				{
					MethodInfo method = activeControl.GetType().GetMethod("Redo");
					if(method != null)
					{
						Clipboard.FunctionWithoutReturn getMethod = (Clipboard.FunctionWithoutReturn) Delegate.CreateDelegate
		            		(typeof(Clipboard.FunctionWithoutReturn), activeControl, method);
		            				
		            	getMethod();	
						res = true;				
					}
				}
				
			}
			return res;
		}		
		
		public static event EventHandler<EditingManagerAskEventArgs> OnCanCut;
		
		public static bool CanCut
		{
			get
			{
				return Execute(OnCanCut) || Clipboard.CanCopy;
			}
		}
		
		public static event EventHandler<EditingManagerEventArgs> OnCut;		
		
		public static bool Cut()
		{
			bool res = Execute(OnCut);
			if(!res)
			{
				Clipboard.Cut();
			}
			return res;
		}		
		
		public static event EventHandler<EditingManagerAskEventArgs> OnCanCopy;
		
		public static bool CanCopy
		{
			get
			{
				return Execute(OnCanCopy) || Clipboard.CanCopy;
			}
		}
		
		public static event EventHandler<EditingManagerEventArgs> OnCopy;		
		
		public static bool Copy()
		{
			bool res = Execute(OnCopy);
			if(!res)
			{
				Clipboard.Copy();
			}
			return res;
		}		

		public static event EventHandler<EditingManagerAskEventArgs> OnCanPaste;
		
		public static bool CanPaste
		{
			get
			{
				return Execute(OnCanPaste) || Clipboard.CanPaste;
			}
		}
		
		public static event EventHandler<EditingManagerEventArgs> OnPaste;		
		
		public static bool Paste()
		{
			bool res = Execute(OnPaste);
			if(!res)
			{
				Clipboard.Paste();
			}
			return res;
		}		
		
		public static event EventHandler<EditingManagerAskEventArgs> OnCanDelete;
		
		public static bool CanDelete
		{
			get
			{
				bool res = Execute(OnCanDelete);
				if(!res)
				{
					Control activeControl = Application.ActiveControl;
					if(activeControl == null)
					{
						return false;
					}
				
					System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
					res = active != null && !active.ReadOnly;
					if(res)
					{
						res = active.SelectionLength != 0 || (active.TextLength != 0 && active.SelectionStart < active.TextLength);
						return res;
					}

					
					if(!res)
					{
						PropertyInfo property = activeControl.GetType().GetProperty("CanDelete", typeof(bool));
						if(property != null)
						{
							MethodInfo method = property.GetGetMethod();
							if(method != null)
							{
								Clipboard.FunctionWithReturn<bool> getMethod = (Clipboard.FunctionWithReturn<bool>) Delegate.CreateDelegate
		            				(typeof(Clipboard.FunctionWithReturn<bool>), activeControl, method);
		            				
		            			res = getMethod();	
							}
						}

						if(!res)
						{
							property = activeControl.GetType().GetProperty("TextLength", typeof(int));
							if(property != null)
							{
								MethodInfo method = property.GetGetMethod();
								if(method != null)
								{
									Clipboard.FunctionWithReturn<int> getMethod = (Clipboard.FunctionWithReturn<int>) Delegate.CreateDelegate
			            				(typeof(Clipboard.FunctionWithReturn<int>), activeControl, method);
			            				
			            			res = getMethod() > 0;
								}
							}
						}
					}
				}
				return res;
			}
		}
		
		public static event EventHandler<EditingManagerEventArgs> OnDelete;		
		
		public static bool Delete()
		{
			bool res = Execute(OnDelete);
			if(!res)
			{
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}
			
				System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;				
				res = active != null;						
				if(res)
				{
					if(active.SelectionLength != 0)
	 					active.SelectedText = ""; 
	 				else if(active.Text[active.SelectionStart] != '\r')
	 				{
	 					active.SelectionLength = 1;
	 					active.SelectedText = ""; 
	 				}
	 				else
	 				{
	 					active.SelectionLength = 2;
	 					active.SelectedText = ""; 
	 				}
				}
				else
				{
					MethodInfo method = activeControl.GetType().GetMethod("Delete");
					if(method != null)
					{
						Clipboard.FunctionWithoutReturn getMethod = (Clipboard.FunctionWithoutReturn) Delegate.CreateDelegate
		            		(typeof(Clipboard.FunctionWithoutReturn), activeControl, method);
		            				
		            	getMethod();	
						res = true;				
					}
				}
			}
			return res;
		}		
		
		public static event EventHandler<EditingManagerAskEventArgs> OnCanSelectAll;
		
		public static bool CanSelectAll
		{
			get
			{
				bool res = Execute(OnCanSelectAll);

				if(!res)
				{
					Control activeControl = Application.ActiveControl;
					if(activeControl == null)
					{
						return false;
					}
					
					if(!res)
					{
						System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
						if(active != null)
						{
							res = active.SelectionLength != active.TextLength;
							return res;
						}

					}
					
					if(!res)
					{
						System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
						if(webbrowser != null)
						{  
							res = true;
							return res;
						}
					}
					
					if(!res)
					{
						int selectionLength = 0;
						int controlTextLength = 0;
						int propCount = 0;
						PropertyInfo property = activeControl.GetType().GetProperty("SelectionLength", typeof(int));
						if(property != null)
						{
							MethodInfo method = property.GetGetMethod();
							if(method != null)
							{
								Clipboard.FunctionWithReturn<int> getMethod = (Clipboard.FunctionWithReturn<int>) Delegate.CreateDelegate
		            				(typeof(Clipboard.FunctionWithReturn<int>), activeControl, method);
		            				
		            			selectionLength = getMethod();	
		            			propCount++;
							}
						}
						
						property = activeControl.GetType().GetProperty("TextLength", typeof(int));
						if(property != null)
						{
							MethodInfo method = property.GetGetMethod();
							if(method != null)
							{
								Clipboard.FunctionWithReturn<int> getMethod = (Clipboard.FunctionWithReturn<int>) Delegate.CreateDelegate
		            				(typeof(Clipboard.FunctionWithReturn<int>), activeControl, method);
		            				
		            			controlTextLength = getMethod();
		            			propCount++;
							}
						}
						
						res = propCount > 1 && selectionLength != controlTextLength;
					}
					
				}
				return res;
			}
		}
		
		public static event EventHandler<EditingManagerEventArgs> OnSelectAll;		
		
		public static bool SelectAll()
		{
			bool res = Execute(OnSelectAll);
			if(!res)
			{
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}
			
				System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;				
				res = active != null;			
				if(res)
				{
	 				active.SelectAll(); 
				}

				if(!res)				
				{
					System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
					if(webbrowser != null)
					{  
						FreeCL.RTL.WebBrowserHelper.GetDocument(webbrowser).ExecCommand("SelectAll", false, null);
						res = true;
					}
				}
				
				if(!res)
				{
					MethodInfo method = activeControl.GetType().GetMethod("SelectAll");
					if(method != null)
					{
						Clipboard.FunctionWithoutReturn getMethod = (Clipboard.FunctionWithoutReturn) Delegate.CreateDelegate
		            		(typeof(Clipboard.FunctionWithoutReturn), activeControl, method);
		            				
		            	getMethod();	
						res = true;				
					}
				}
			}
			return res;
		}		
		
		
	}
}
