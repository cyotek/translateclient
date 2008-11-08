// --------------------------------------------------------------------------
// Description : CDiese Toolkit library
// Author		 : Serge Weinstock
//
//	You are free to use, distribute or modify this code
//	as long as this header is not removed or modified.
// --------------------------------------------------------------------------

// --------------------------------------------------------------------------
// Further changes made by Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com) 2005-2008
// and available under MPL 1.1/GPL 2.0/LGPL 2.1
//
// Changes list:
//  - Shortcuts support  
//  - ToolStrips support
//  - .net 2 porting
// --------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using FreeCL.UI;
using FreeCL.RTL;
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.UI.Actions
{
	/// <summary>
	/// Internal data about a control used by an Action
	/// </summary>
	internal class ActionData : IDisposable
	{
		
		PropertyWrapper<string> textWrapper = new PropertyWrapper<string>();
		PropertyWrapper<bool> enabledWrapper = new PropertyWrapper<bool>();
		PropertyWrapper<bool> checkedWrapper = new PropertyWrapper<bool>();
		PropertyWrapper<bool> visibleWrapper = new PropertyWrapper<bool>();
		
		private		PropertyInfo	_shortcut;
		private		PropertyInfo	_shortcutkeys;
		private		PropertyInfo	_shortcutkeydisplaystring;
		private		PropertyInfo	_imageIndex;
		private		PropertyInfo	_imageList;
		private		PropertyInfo	_image;
		private		PropertyInfo	_imageTransparentColor;
		private		bool			_click;
		private		bool			_owner_draw_menus;
		private		Component		component;
		private		Action			action;
		private PropertyInfo _shortcuts;		
		private bool _designMode;
		#region "public" interface
		internal void Attach(Action a, Component o, bool designMode)
		{
			Debug.Assert(o != null && a != null);
			component = o;
			action = a;
			_designMode = designMode;
			Debug.Assert(action.Parent != null);
			// Text
			textWrapper.Attach(component, "Text");
			Text = action.Text;

			// Enabled
			enabledWrapper.Attach(component, "Enabled");
			Enabled = action.Enabled;
			
			// Checked
			// special case of a toolbarButton
			if (component is ToolBarButton)
			{
				checkedWrapper.Attach(component, "Pushed");
			}
			else
			{
				checkedWrapper.Attach(component, "Checked");
			}
			Checked = action.Checked;
			// Visible
			visibleWrapper.Attach(component, "Visible");
			Visible = action.Visible;
			
			// Shortcut
			_shortcut = o.GetType().GetProperty("Shortcut");
			if (_shortcut != null && (!_shortcut.CanRead || !_shortcut.CanWrite) && (_shortcut.PropertyType == typeof(Keys)))
			{
				// we must be able to read and write a shortcut property
				_shortcut = null;
			}
			Shortcut = action.Shortcut;
			
		
	 			_shortcutkeys = o.GetType().GetProperty("ShortcutKeys");
				if (_shortcutkeys != null && (!_shortcutkeys.CanRead || !_shortcutkeys.CanWrite) && (_shortcutkeys.PropertyType == typeof(Keys)))
				{
					// we must be able to read and write a shortcut property
					_shortcutkeys = null;
				}
	 			ShortcutKeys = action.Shortcut;
	 			
	 			
			_shortcuts = o.GetType().GetProperty("Shortcuts");
			if (_shortcuts != null && (!_shortcuts.CanRead || !_shortcuts.CanWrite) && (_shortcuts.PropertyType == typeof(ShortcutKeysCollection)))
			{
				// we must be able to read and write a shortcut property
				_shortcuts = null;
			}
			Shortcuts = action.Shortcuts;
			
	 			_shortcutkeydisplaystring = o.GetType().GetProperty("ShortcutKeyDisplayString");
	 			if (_shortcutkeydisplaystring != null && (!_shortcutkeydisplaystring.CanRead || !_shortcutkeydisplaystring.CanWrite) && (_shortcutkeydisplaystring.PropertyType == typeof(string)))
	 			{
					// we must be able to read and write a shortcut property
	 				_shortcutkeydisplaystring = null;
	 			}
	 			UpdateShortcutKeyDisplayString();
			
			
			// ImageList
			// don't handle toolbarButtons here
			if (!(component is ToolBarButton))
			{
				_imageList = o.GetType().GetProperty("ImageList");
				if (_imageList != null && (!_imageList.CanRead || !_imageList.CanWrite) && (_imageList.PropertyType == typeof(ImageList)))
				{
					// we must be able to read and write an ImageList property
					_imageList = null;
				}
			}
			ImageList = action.Parent.ImageList;
			// ImageIndex
			_imageIndex = o.GetType().GetProperty("ImageIndex");
			if (_imageIndex != null && (!_imageIndex.CanRead || !_imageIndex.CanWrite) && (_imageIndex.PropertyType == typeof(int)))
			{
				// we must be able to read and write an integer property
				_imageIndex = null;
			}
			ImageIndex = action.ImageIndex;
			
			//image
			if (_imageIndex == null || o is ToolStripItem)
			{
				_image = o.GetType().GetProperty("Image");
				_imageTransparentColor = o.GetType().GetProperty("ImageTransparentColor");
				if (_image != null 
						&& (!_image.CanRead || !_image.CanWrite) 
						&& (_image.PropertyType == typeof(Image))
						&& _imageTransparentColor != null 
						&& (!_imageTransparentColor.CanRead || !_imageTransparentColor.CanWrite) 
						&& (_imageTransparentColor.PropertyType == typeof(Color))						
					 )
				{
					_image = null;
					_imageTransparentColor = null;
				}
				UpdateImage();
			}
	
			// Hint
			Hint = action.Hint;
			// click
			if (!designMode)
			{
				// special case of a toolbarButton
				if (component is ToolBarButton)
				{
					ToolBar tb = ((ToolBarButton)component).Parent;
					if (tb != null)
					{
						tb.ButtonClick += new ToolBarButtonClickEventHandler(OnToolbarClick);
						_click = true;
					}
				}
				else
				{
					EventInfo e = o.GetType().GetEvent("Click");
					if (e != null && e.EventHandlerType == typeof(EventHandler))
					{
						e.AddEventHandler(component, new EventHandler(action.OnExecute));
						_click = true;
					}
					
					if(component is System.Windows.Forms.MenuItem)
					{
						OwnerDrawMenus = action.Parent.OwnerDrawMenus;	
					}
					
				}
				
				
			}
			// Dispose
			Debug.Assert(action.Parent != null);
			component.Disposed += new EventHandler(action.Parent.OnComponentDisposed);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		internal void Detach(bool designMode)
		{
			textWrapper.Detach();
			enabledWrapper.Detach();
			checkedWrapper.Detach();
			visibleWrapper.Detach();
			_shortcut = null;
			_shortcuts = null;			
			_shortcutkeys = null;						
			_shortcutkeydisplaystring = null;
			_designMode = designMode;
			_image = null;
			_imageTransparentColor = null;
			
			if (!designMode && component != null && _click)
			{
				if (component is ToolBarButton)
				{
					ToolBar tb = ((ToolBarButton)component).Parent;
					if (tb != null)
					{
						tb.ButtonClick -= new ToolBarButtonClickEventHandler(OnToolbarClick);
					}
				}
				else
				{
					EventInfo e = component.GetType().GetEvent("Click");
					if(action != null && component != null)
					try
					{
						e.RemoveEventHandler(component, new EventHandler(action.OnExecute));
					}
					catch
					{
						
					}
						
				}
				
				if(component is System.Windows.Forms.MenuItem && _owner_draw_menus)
				{
	 					OwnerDrawMenus = false;	
	 				}
				
			}
			Debug.Assert(action.Parent != null);
			component.Disposed -= new EventHandler(action.Parent.OnComponentDisposed);
		}
		

		internal string Text
		{
			set
			{
				if (textWrapper.Attached)
				{
					string currValue = textWrapper.Value;
					SpeedButton speedButton = component as SpeedButton;
					if ((component is ToolBarButton || (speedButton != null && !speedButton.ShowText)) 
						&& !action.Parent.ShowTextOnToolBar
						)
					{
						textWrapper.Value = null;
					}
					else if (currValue != value)
					{
						textWrapper.Value = value;
					}
				}
			}
		}
		internal bool Enabled
		{
			set
			{
				if (enabledWrapper.Attached && enabledWrapper.Value != value)
				{
					enabledWrapper.Value = value;
				}
			}
		}
		internal bool Checked
		{
			set
			{
				if (checkedWrapper.Attached && checkedWrapper.Value != value)
				{
					checkedWrapper.Value = value;
				}
			}
		}
		internal bool Visible
		{
			set
			{
				if (visibleWrapper.Attached && visibleWrapper.Value != value)
				{
					visibleWrapper.Value = value;
				}
			}
		}
		internal Keys Shortcut
		{
			set
			{
				if (_shortcut != null && ((Keys)_shortcut.GetValue(component, null) != value))
				{
					_shortcut.SetValue(component, value, null);
				}
			}
		}
	
		internal Keys ShortcutKeys
		{
			set
			{
				if (_shortcutkeys != null && ((Keys)_shortcutkeys.GetValue(component, null) != value))
				{
					_shortcutkeys.SetValue(component, value, null);
				}
			}
		}
		
		internal ShortcutKeysCollection Shortcuts
		{
			set
			{
				if (_shortcuts != null && ((ShortcutKeysCollection)_shortcuts.GetValue(component, null) != value))
				{
					_shortcuts.SetValue(component, value, null);
				}
			}
		}
		
		
		internal void UpdateShortcutKeyDisplayString()
		{
			if (_shortcutkeydisplaystring != null && action != null)
			{
	
				Keys k = Keys.None;
				
				if(action.Shortcut != Keys.None)
					k = action.Shortcut;
				else if (action.Shortcuts.Count > 0)
					k= action.Shortcuts[0].Keys;
				
				if (k != Keys.None )	
				{
					string displayString =	System.ComponentModel.TypeDescriptor.GetConverter(k.GetType()).ConvertToString(k);
					if (((string)_shortcutkeydisplaystring.GetValue(component, null) != displayString))
					{
						_shortcutkeydisplaystring.SetValue(component, displayString, null);						
					}
				}
			}
				
		}
	
		internal void UpdateImage()
		{
			if (_image != null 
					&& _imageTransparentColor != null 
					&& action.Parent.ImageList != null
				 )
			{
				if((Color)_imageTransparentColor.GetValue(component, null) != action.Parent.ImageList.TransparentColor)
				{
					_imageTransparentColor.SetValue(component, action.Parent.ImageList.TransparentColor, null);
				}
	
				if(action.ImageIndex >= 0 && action.ImageIndex <	action.Parent.ImageList.Images.Count
					 )
				{
					if((Image)_image.GetValue(component, null) != action.Parent.ImageList.Images[action.ImageIndex])
						_image.SetValue(component, action.Parent.ImageList.Images[action.ImageIndex], null);
				}
				else if(action.Image != null)
				{
					if((Image)_image.GetValue(component, null) != action.Image)
						_image.SetValue(component, action.Image, null);
				}
				else
					_image.SetValue(component, null, null);
				
			}
		}
		
		internal ImageList ImageList
		{
			set
			{
				if (component is ToolBarButton)
				{
					ToolBarButton tb = (ToolBarButton)component;
	
					if (tb.Parent != null && tb.Parent.ImageList != value)
					{
						tb.Parent.ImageList = value;
					}
					return;
				}
				if (_imageList != null && ((ImageList)_imageList.GetValue(component, null) != value))
				{
					_imageList.SetValue(component, value, null);
				}
			}
		}
		internal int ImageIndex
		{
			set
			{
				if (_imageIndex != null && ((int)_imageIndex.GetValue(component, null) != value))
				{
					_imageIndex.SetValue(component, value, null);
				}
			}
		}
		private void OnToolbarClick(Object sender, ToolBarButtonClickEventArgs e)
		{
			if	(e.Button == component)
			{
				action.OnExecute(sender, e);
			}
		}
		public void Dispose()
		{
			Detach(component.Site.DesignMode);
			textWrapper.Detach();
			textWrapper = null;
			enabledWrapper.Detach();
			enabledWrapper = null;
			checkedWrapper.Detach();
			checkedWrapper = null;
			visibleWrapper.Detach();
			visibleWrapper = null;
			
		}
		internal void FinishInit()
		{
			if (component is ToolBarButton && !_click)
			{
				ToolBar tb = ((ToolBarButton)component).Parent;
				if (tb != null)
				{
					tb.ButtonClick += new ToolBarButtonClickEventHandler(OnToolbarClick);
					_click = true;
				}
			}
			
			if(component is System.Windows.Forms.MenuItem && !_owner_draw_menus)
			{
				OwnerDrawMenus = action.Parent.OwnerDrawMenus;	
			}
			
		}
		internal string ShowTextOnToolBar
		{
			set
			{
				if (component is ToolBarButton)
				{
					Text = value;
				}
			}
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		internal bool OwnerDrawMenus
		{
			set
			{
				if(component is System.Windows.Forms.MenuItem && !_designMode)
				{
					System.Windows.Forms.MenuItem mi = component as System.Windows.Forms.MenuItem;
					try
					{
						if(_owner_draw_menus && !value)
						{
							mi.OwnerDraw = false;					
							mi.DrawItem -= new DrawItemEventHandler(OwnerDrawMenuItem);
							mi.MeasureItem -= new MeasureItemEventHandler(OwnerMeasureMenuItem);
						}
						else if(!_owner_draw_menus && value)
						{
							mi.OwnerDraw = true;	
							mi.DrawItem += new DrawItemEventHandler(OwnerDrawMenuItem);
							mi.MeasureItem += new MeasureItemEventHandler(OwnerMeasureMenuItem);
						}
					}
					catch
					{
						
					}
					_owner_draw_menus = value;
				}
			}
		}
		
		private string GetShortcutText(System.Windows.Forms.MenuItem item)
		{
			string finalText = "";
			if(item.ShowShortcut)
			{
				Keys k = Keys.None;
				if (item.Shortcut !=	System.Windows.Forms.Shortcut.None) 
					k= (Keys)item.Shortcut;
				else if (action.Shortcuts.Count > 0)
					k= action.Shortcuts[0].Keys;
	
				if(k != Keys.None )
					finalText = System.ComponentModel.TypeDescriptor.GetConverter(k.GetType()).ConvertToString(k);
			}
			
			return finalText;
		}
		
			private void OwnerMeasureMenuItem(object sender, MeasureItemEventArgs e)
			{
				System.Windows.Forms.MenuItem mi = component as System.Windows.Forms.MenuItem;
				FreeCL.UI.MenuItem.OwnerMeasureMenuItem(mi, e, GetShortcutText(mi));
			}
	
			private void OwnerDrawMenuItem(object sender, DrawItemEventArgs e)
			{
				System.Windows.Forms.MenuItem mi = component as System.Windows.Forms.MenuItem;
				FreeCL.UI.MenuItem.OwnerDrawMenuItem(mi, e, GetShortcutText(mi));
			}
		
		
		internal string Hint
		{
			set
			{
				if (component is ToolBarButton)
				{
					if (((ToolBarButton)component).ToolTipText != value)
					{
						((ToolBarButton)component).ToolTipText = value;
					}
				}
				else if (component is ToolStripItem)
				{
					if (((ToolStripItem)component).ToolTipText != value)
					{
						((ToolStripItem)component).ToolTipText = value;
						((ToolStripItem)component).AutoToolTip = false;
					}
				}
				else if (component is Control)
				{
					Debug.Assert(action != null &&	action.Parent != null && action._owner._toolTip != null);
					Control	c = (Control)component;
					ToolTip t = action._owner._toolTip;
					if (t.GetToolTip(c) != value)
					{
						t.SetToolTip(c, value);
					}
				}
			}
		}
		#endregion
		
	}
}
