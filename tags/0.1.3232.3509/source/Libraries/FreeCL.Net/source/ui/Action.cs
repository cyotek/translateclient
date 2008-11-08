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


}
