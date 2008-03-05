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
using System.Diagnostics.CodeAnalysis;

namespace FreeCL.UI.Actions
{
	/// <summary>
	/// Action.
	/// </summary>
	[
		DesignTimeVisible(false),
		ToolboxItem(false),
		DefaultEvent("Execute"),
		DefaultProperty("Text")
	]
	public class Action : System.ComponentModel.Component
	{
		#region member variables
		private object _tag;
		private string _text;
		private int	_imageIndex = -1;
		private Image	_image;
		private Dictionary<Component, ActionData> _components = new Dictionary<Component, ActionData>();
		internal ActionList _owner;
		private bool _enabled = true;
		private bool _checked;
		private bool _visible = true;
		private Keys _shortcut = Keys.None;
		private string	_hint;
		private ShortcutKeysCollection _shortcuts;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components;
		#endregion
		#region public interface
		public Action(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			container.Add(this);
			InitializeComponent();
			_shortcuts = new ShortcutKeysCollection(this);			
		}

		public Action()
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			InitializeComponent();
			_shortcuts = new ShortcutKeysCollection(this);			
		}
		/// <summary>
		/// The text used in controls associated to this Action.
		/// </summary>
		[
		Category("Misc"), 
		Localizable(true),
		Description("The text used in controls associated to this Action.")
		]
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.Text = _text;
				}
			}
		}
		/// <summary>
		/// Indicates whether the associated components are enabled.
		/// </summary>
		[
		Category("Behavior"), 
		Description("Indicates whether the associated components are enabled.")
		]
		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.Enabled = _enabled;
				}
			}
		}
		/// <summary>
		/// Indicates whether the associated components are checked.
		/// </summary>
		[
		Category("Behavior"), 
		Description("Indicates whether the associated components are checked.")
		]
		public bool Checked
		{
			get
			{
				return _checked;
			}
			set
			{
				_checked = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.Checked = _checked;
				}
			}
		}
		/// <summary>
		/// Indicates the shorcut for this Action.
		/// </summary>
		[
		Category("Misc"), 
		Description("Indicates the shorcut for this Action.To allow Action.Shortcut worked properly you needed assign Action to MenuItem"),
		DefaultValue(Keys.None) 
		]
		public Keys Shortcut
		{
			get
			{
				return _shortcut;
			}
			set
			{
				_shortcut = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.Shortcut = _shortcut;
					ad.ShortcutKeys = _shortcut;
					ad.UpdateShortcutKeyDisplayString();
				}
			}
		}

		/// <summary>
		/// Indicates the shorcuts keys for this Action.
		/// </summary>
		
		[
		Description("Indicates the shorcuts keys for this Action.To allow Action.Shortcuts worked properly you needed assign FreeCL.UI.MainMenu to ActionList"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
		Category("Misc"),
		SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"),
		SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")
		]
		public ShortcutKeysCollection Shortcuts
		{
			get
			{
				return _shortcuts;
			}
			set
			{
				_shortcuts.Clear();
				_shortcuts.AddRange(value);
				foreach(ActionData ad in _components.Values)
				{
					ad.Shortcuts = _shortcuts;
					ad.UpdateShortcutKeyDisplayString();					
				}
				
			}
		}
		
		
		/// <summary>
		/// Indicates whether the associated components are visibled or hidden.
		/// </summary>
		[
		Category("Behavior"), 
		Description("Indicates the shorcut for this Action.")
		]
		public bool Visible
		{
			get
			{
				return _visible;
			}
			set
			{
				_visible = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.Visible = _visible;
				}
			}
		}
		/// <summary>
		/// User defined data associated with this Action.
		/// </summary>
		[
		Category("Data"), 
		Description("User defined data associated with this Action.")
		]
		public object Tag
		{
			get
			{
				return _tag;
			}
			set 
			{
				_tag = value;
			}
		}
		/// <summary>
		/// Indicates the index of the image in the parent ActionList's ImageList this Action will use to obtains its image.
		/// </summary>
		[
		Category("Appearance"),
		Localizable(true),
		Description("Indicates the index of the image in the parent ActionList's ImageList this Action will use to obtains its image."),
		TypeConverter(typeof(System.Windows.Forms.ImageIndexConverter)),
		Editor(typeof(Design.ImageIndexEditor), typeof(UITypeEditor)),
		DefaultValue(-1)
		]
		public int ImageIndex
		{
			get
			{
				return _imageIndex;
			}
			set
			{
				if(value != -1 && Image != null)
					Image = null;
				
				_imageIndex = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.ImageIndex = _imageIndex;
					ad.UpdateImage();
				}
			}
		}
		
		/// <summary>
		/// Indicates the image of this Action.
		/// </summary>
		[
		Category("Appearance"),
		Localizable(true),
		Description("Indicates the image of this Action."),
		DefaultValue(null)
		]
		public Image Image
		{
			get
			{
				return _image;
			}
			set
			{
				if(value != null && ImageIndex != -1)
					ImageIndex = -1;
				
				_image = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.UpdateImage();
				}
			}
		}

		string GeneratedHint()
		{
			if(_owner != null && _owner.ShowShortcutsInHints && _shortcut != Keys.None)
			{
				string displayString =	System.ComponentModel.TypeDescriptor.GetConverter(_shortcut.GetType()).ConvertToString(_shortcut);
				return _hint + " (" + displayString + ")";
			}
			return _hint;
		}
		
		/// <summary>
		/// Indicates the text that appears as a ToolTip for a control.
		/// </summary>
		[
		Category("Misc"),
		Localizable(true),
		Description("Indicates the text that appears as a ToolTip for a control."),
		]
		public string Hint
		{
			get
			{
				return _hint;
			}
			set
			{
				_hint = value;
				foreach(ActionData ad in _components.Values)
				{
					ad.Hint = GeneratedHint();
				}
			}
		}
		/// <summary>
		/// The ActionList to which this action belongs
		/// </summary>
		[Browsable(false)]
		public ActionList Parent
		{
			get
			{
				return _owner;
			}
		}
		/// <summary>
		/// This event is triggered when the path changes
		/// </summary>
		[Description("Triggered when the action is executed")]
		public event EventHandler Execute;

		/// <summary>
		/// Execute action
		/// </summary>
		[Description("Execute action")]
		public void DoExecute()
		{
			OnExecute(_owner, new EventArgs());
		}
		/// <summary>
		/// This event is triggered when the path changes
		/// </summary>
		[Description("Triggered when the application is idle or when the action list updates.")]
		public event EventHandler Update;
		#endregion
		
		#region implementation
		[Browsable(false)]
		internal ImageList ImageList
		{
			set
			{
				foreach(ActionData ad in _components.Values)
				{
					ad.ImageList = value;
					ad.UpdateImage();
				}
			}
		}
		internal void OnExecute(Object sender, EventArgs e)
		{
			if (Execute != null)
			{
				OnUpdate(sender, e);
				if(_enabled && !_owner.LockAllExecute)
					Execute(this, e);
			}
		}
		internal void OnUpdate(Object sender, EventArgs e)
		{
			if (Update != null)
			{
				Update(this, e);
			}
		}
		internal void SetComponent(Component comp, bool add)
		{
			ActionData ad = null;
			_components.TryGetValue(comp, out ad);
			
			if (add)
			{
				if (ad == null)
				{
					ad = new ActionData();
					ad.Attach(this, comp, DesignMode);
					_components[comp] = ad;
				}
			}
			else if (ad != null)
			{
				ad.Detach(DesignMode);
				_components.Remove(comp);
			}
		}
		internal bool HandleComponent(Component comp)
		{
			return (_components.ContainsKey(comp));
		}
		/// <summary>
		/// The ActionList to which this action belongs
		/// </summary>
		[Browsable(false)]
		internal bool ShowTextOnToolBar
		{
			set
			{
				string dtext = (value ? Text : null);

				foreach(ActionData ad in _components.Values)
				{
					ad.ShowTextOnToolBar = dtext;
				}
			}
		}
		
		/// <summary>
		/// The ActionList to which this action belongs
		/// </summary>
		[Browsable(false)]
		internal bool OwnerDrawMenus
		{
			set
			{
				foreach(ActionData ad in _components.Values)
				{
					ad.OwnerDrawMenus = value;
				}
			}
		}
		#endregion
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		internal void FinishInit()
		{
			foreach(ActionData ad in _components.Values)
			{
				ad.FinishInit();
			}
		}
		#endregion
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
				
				foreach(ActionData ad in _components.Values)
				{
					ad.Detach(DesignMode);
				}
				
				if(components != null)
				{
					_components.Clear();
					_components = null;
				}
				
				if(_shortcuts != null)
				{
					_shortcuts.Dispose();
					_shortcuts = null;
				}
				
				
			}
			base.Dispose( disposing );
		}
		
	}

	/// <summary>
	/// Internal data about a control used by an Action
	/// </summary>
	internal class ActionData : IDisposable
	{
		#region member variables
		private		PropertyInfo	_text;
		private		PropertyInfo	_enabled;
		private		PropertyInfo	_checked;
		private		PropertyInfo	_visible;
		private		PropertyInfo	_shortcut;
		private		PropertyInfo	_shortcutkeys;
		private		PropertyInfo	_shortcutkeydisplaystring;
		private		PropertyInfo	_imageIndex;
		private		PropertyInfo	_imageList;
		private		PropertyInfo	_image;
		private		PropertyInfo	_imageTransparentColor;
		private		bool			_click;
		private		bool			_owner_draw_menus;
		private		Component		_component;
		private		Action			_owner;
		private PropertyInfo _shortcuts;		
		private bool _designMode;
		#endregion
		#region "public" interface
		
		
		internal void Attach(Action a, Component o, bool designMode)
		{
			Debug.Assert(o != null && a != null);
			_component = o;
			_owner = a;
			_designMode = designMode;
			Debug.Assert(_owner.Parent != null);
			// Text
			_text = o.GetType().GetProperty("Text");
			if (_text != null && (!_text.CanRead || !_text.CanWrite) && (_text.PropertyType == typeof(string)))
			{
				// we must be able to read and write a boolean property
				_text = null;
			}
			Text = _owner.Text;
			// Enabled
			_enabled = o.GetType().GetProperty("Enabled");
			if (_enabled != null && (!_enabled.CanRead || !_enabled.CanWrite) && (_enabled.PropertyType == typeof(bool)))
			{
				// we must be able to read and write a boolean property
				_enabled = null;
			}
			Enabled = _owner.Enabled;
			// Checked
			// special case of a toolbarButton
			if (_component is ToolBarButton)
			{
				_checked = o.GetType().GetProperty("Pushed");
				Debug.Assert(_checked != null && _checked.CanRead && _checked.CanWrite && (_checked.PropertyType == typeof(bool)));
			}
			else
			{
				_checked = o.GetType().GetProperty("Checked");
				if (_checked != null && (!_checked.CanRead || !_checked.CanWrite) && (_checked.PropertyType == typeof(bool)))
				{
					// we must be able to read and write a boolean property
					_checked = null;
				}
			}
			Checked = _owner.Checked;
			// Visible
			_visible = o.GetType().GetProperty("Visible");
			if (_visible != null && (!_visible.CanRead || !_visible.CanWrite) && (_visible.PropertyType == typeof(bool)))
			{
				// we must be able to read and write a boolean property
				_visible = null;
			}
			Visible = _owner.Visible;
			// Shortcut
			_shortcut = o.GetType().GetProperty("Shortcut");
			if (_shortcut != null && (!_shortcut.CanRead || !_shortcut.CanWrite) && (_shortcut.PropertyType == typeof(Keys)))
			{
				// we must be able to read and write a shortcut property
				_shortcut = null;
			}
			Shortcut = _owner.Shortcut;
			
		
 			_shortcutkeys = o.GetType().GetProperty("ShortcutKeys");
				if (_shortcutkeys != null && (!_shortcutkeys.CanRead || !_shortcutkeys.CanWrite) && (_shortcutkeys.PropertyType == typeof(Keys)))
				{
					// we must be able to read and write a shortcut property
					_shortcutkeys = null;
				}
 			ShortcutKeys = _owner.Shortcut;
 			
 			
			_shortcuts = o.GetType().GetProperty("Shortcuts");
			if (_shortcuts != null && (!_shortcuts.CanRead || !_shortcuts.CanWrite) && (_enabled.PropertyType == typeof(ShortcutKeysCollection)))
			{
				// we must be able to read and write a shortcut property
				_shortcuts = null;
			}
			Shortcuts = _owner.Shortcuts;
			
 			_shortcutkeydisplaystring = o.GetType().GetProperty("ShortcutKeyDisplayString");
 			if (_shortcutkeydisplaystring != null && (!_shortcutkeydisplaystring.CanRead || !_shortcutkeydisplaystring.CanWrite) && (_shortcutkeydisplaystring.PropertyType == typeof(string)))
 			{
					// we must be able to read and write a shortcut property
 				_shortcutkeydisplaystring = null;
 			}
 			UpdateShortcutKeyDisplayString();
			
			
			// ImageList
			// don't handle toolbarButtons here
			if (!(_component is ToolBarButton))
			{
				_imageList = o.GetType().GetProperty("ImageList");
				if (_imageList != null && (!_imageList.CanRead || !_imageList.CanWrite) && (_imageList.PropertyType == typeof(ImageList)))
				{
					// we must be able to read and write an ImageList property
					_imageList = null;
				}
			}
			ImageList = _owner.Parent.ImageList;
			// ImageIndex
			_imageIndex = o.GetType().GetProperty("ImageIndex");
			if (_imageIndex != null && (!_imageIndex.CanRead || !_imageIndex.CanWrite) && (_imageIndex.PropertyType == typeof(int)))
			{
				// we must be able to read and write an integer property
				_imageIndex = null;
			}
			ImageIndex = _owner.ImageIndex;
			
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
			Hint = _owner.Hint;
			// click
			if (!designMode)
			{
				// special case of a toolbarButton
				if (_component is ToolBarButton)
				{
					ToolBar tb = ((ToolBarButton)_component).Parent;
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
						e.AddEventHandler(_component, new EventHandler(_owner.OnExecute));
						_click = true;
					}
					
					if(_component is System.Windows.Forms.MenuItem)
					{
						OwnerDrawMenus = _owner.Parent.OwnerDrawMenus;	
					}
					
				}
				
				
			}
			// Dispose
			Debug.Assert(_owner.Parent != null);
			_component.Disposed += new EventHandler(_owner.Parent.OnComponentDisposed);
		}
		
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		internal void Detach(bool designMode)
		{
			_text = null;
			_enabled = null;
			_checked = null;
			_shortcut = null;
			_shortcuts = null;			
			_shortcutkeys = null;						
			_shortcutkeydisplaystring = null;
			_designMode = designMode;
			_image = null;
			_imageTransparentColor = null;
			
			if (!designMode && _component != null && _click)
			{
				if (_component is ToolBarButton)
				{
					ToolBar tb = ((ToolBarButton)_component).Parent;
					if (tb != null)
					{
						tb.ButtonClick -= new ToolBarButtonClickEventHandler(OnToolbarClick);
					}
				}
				else
				{
					EventInfo e = _component.GetType().GetEvent("Click");
					if(_owner != null && _component != null)
					try
					{
						e.RemoveEventHandler(_component, new EventHandler(_owner.OnExecute));
					}
					catch
					{
						
					}
						
				}
				
				if(_component is System.Windows.Forms.MenuItem && _owner_draw_menus)
				{
 					OwnerDrawMenus = false;	
 				}
				
			}
			Debug.Assert(_owner.Parent != null);
			_component.Disposed -= new EventHandler(_owner.Parent.OnComponentDisposed);
		}
		internal string Text
		{
			set
			{
				if (_text != null)
				{
					if ((_component is ToolBarButton || (_component is SpeedButton && !(_component as SpeedButton).ShowText)) && !_owner.Parent.ShowTextOnToolBar)
					{
						_text.SetValue(_component, null, null);
					}
					else if ((string)_text.GetValue(_component, null) != value)
					{
						_text.SetValue(_component, value, null);
					}
				}
			}
		}
		internal bool Enabled
		{
			set
			{
				if (_enabled != null && ((bool)_enabled.GetValue(_component, null) != value))
				{
					_enabled.SetValue(_component, value, null);
				}
			}
		}
		internal bool Checked
		{
			set
			{
				if (_checked != null && ((bool)_checked.GetValue(_component, null) != value))
				{
					_checked.SetValue(_component, value, null);
				}
			}
		}
		internal bool Visible
		{
			set
			{
				if (_visible != null && ((bool)_visible.GetValue(_component, null) != value))
				{
					_visible.SetValue(_component, value, null);
				}
			}
		}
		internal Keys Shortcut
		{
			set
			{
				if (_shortcut != null && ((Keys)_shortcut.GetValue(_component, null) != value))
				{
					_shortcut.SetValue(_component, value, null);
				}
			}
		}

		internal Keys ShortcutKeys
		{
			set
			{
				if (_shortcutkeys != null && ((Keys)_shortcutkeys.GetValue(_component, null) != value))
				{
					_shortcutkeys.SetValue(_component, value, null);
				}
			}
		}
		
		internal ShortcutKeysCollection Shortcuts
		{
			set
			{
				if (_shortcuts != null && ((ShortcutKeysCollection)_shortcuts.GetValue(_component, null) != value))
				{
					_shortcuts.SetValue(_component, value, null);
				}
			}
		}
		
		
		internal void UpdateShortcutKeyDisplayString()
		{
			if (_shortcutkeydisplaystring != null && _owner != null)
			{

				Keys k = Keys.None;
				
				if(_owner.Shortcut != Keys.None)
					k = _owner.Shortcut;
				else if (_owner.Shortcuts.Count > 0)
					k= _owner.Shortcuts[0].Keys;
				
				if (k != Keys.None )	
				{
					string displayString =	System.ComponentModel.TypeDescriptor.GetConverter(k.GetType()).ConvertToString(k);
					if (((string)_shortcutkeydisplaystring.GetValue(_component, null) != displayString))
					{
						_shortcutkeydisplaystring.SetValue(_component, displayString, null);						
					}
				}
			}
				
		}

		internal void UpdateImage()
		{
			if (_image != null 
					&& _imageTransparentColor != null 
					&& _owner.Parent.ImageList != null
				 )
			{
				if((Color)_imageTransparentColor.GetValue(_component, null) != _owner.Parent.ImageList.TransparentColor)
				{
					_imageTransparentColor.SetValue(_component, _owner.Parent.ImageList.TransparentColor, null);
				}

				if(_owner.ImageIndex >= 0 && _owner.ImageIndex <	_owner.Parent.ImageList.Images.Count
					 )
				{
					if((Image)_image.GetValue(_component, null) != _owner.Parent.ImageList.Images[_owner.ImageIndex])
						_image.SetValue(_component, _owner.Parent.ImageList.Images[_owner.ImageIndex], null);
				}
				else if(_owner.Image != null)
				{
					if((Image)_image.GetValue(_component, null) != _owner.Image)
						_image.SetValue(_component, _owner.Image, null);
				}
				else
					_image.SetValue(_component, null, null);
				
			}
		}
		
		internal ImageList ImageList
		{
			set
			{
				if (_component is ToolBarButton)
				{
					ToolBarButton tb = (ToolBarButton)_component;

					if (tb.Parent != null && tb.Parent.ImageList != value)
					{
						tb.Parent.ImageList = value;
					}
					return;
				}
				if (_imageList != null && ((ImageList)_imageList.GetValue(_component, null) != value))
				{
					_imageList.SetValue(_component, value, null);
				}
			}
		}
		internal int ImageIndex
		{
			set
			{
				if (_imageIndex != null && ((int)_imageIndex.GetValue(_component, null) != value))
				{
					_imageIndex.SetValue(_component, value, null);
				}
			}
		}
		private void OnToolbarClick(Object sender, ToolBarButtonClickEventArgs e)
		{
			if	(e.Button == _component)
			{
				_owner.OnExecute(sender, e);
			}
		}
		public void Dispose()
		{
			Detach(_component.Site.DesignMode);
		}
		internal void FinishInit()
		{
			if (_component is ToolBarButton && !_click)
			{
				ToolBar tb = ((ToolBarButton)_component).Parent;
				if (tb != null)
				{
					tb.ButtonClick += new ToolBarButtonClickEventHandler(OnToolbarClick);
					_click = true;
				}
			}
			
			if(_component is System.Windows.Forms.MenuItem && !_owner_draw_menus)
			{
				OwnerDrawMenus = _owner.Parent.OwnerDrawMenus;	
			}
			
		}
		internal string ShowTextOnToolBar
		{
			set
			{
				if (_component is ToolBarButton)
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
				if(_component is System.Windows.Forms.MenuItem && !_designMode)
				{
					System.Windows.Forms.MenuItem mi = _component as System.Windows.Forms.MenuItem;
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
				else if (_owner.Shortcuts.Count > 0)
					k= _owner.Shortcuts[0].Keys;

				if(k != Keys.None )
					finalText = System.ComponentModel.TypeDescriptor.GetConverter(k.GetType()).ConvertToString(k);
			}
			
			return finalText;
		}
		
			private void OwnerMeasureMenuItem(object sender, MeasureItemEventArgs e)
			{
				System.Windows.Forms.MenuItem mi = _component as System.Windows.Forms.MenuItem;
				FreeCL.UI.MenuItem.OwnerMeasureMenuItem(mi, e, GetShortcutText(mi));
			}
	
			private void OwnerDrawMenuItem(object sender, DrawItemEventArgs e)
			{
				System.Windows.Forms.MenuItem mi = _component as System.Windows.Forms.MenuItem;
				FreeCL.UI.MenuItem.OwnerDrawMenuItem(mi, e, GetShortcutText(mi));
			}
		
		
		internal string Hint
		{
			set
			{
				if (_component is ToolBarButton)
				{
					if (((ToolBarButton)_component).ToolTipText != value)
					{
						((ToolBarButton)_component).ToolTipText = value;
					}
				}
				else if (_component is ToolStripItem)
				{
					if (((ToolStripItem)_component).ToolTipText != value)
					{
						((ToolStripItem)_component).ToolTipText = value;
						((ToolStripItem)_component).AutoToolTip = false;
					}
				}
				else if (_component is Control)
				{
					Debug.Assert(_owner != null &&	_owner.Parent != null && _owner._owner._toolTip != null);
					Control	c = (Control)_component;
					ToolTip t = _owner._owner._toolTip;
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
