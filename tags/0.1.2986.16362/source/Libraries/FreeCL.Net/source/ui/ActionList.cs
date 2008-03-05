// --------------------------------------------------------------------------
// Description : CDiese Toolkit library
// Author		 : Serge Weinstock
//
//	You are free to use, distribute or modify this code
//	as long as this header is not removed or modified.
// --------------------------------------------------------------------------

// --------------------------------------------------------------------------
// Further changes made by Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com)
// and available under MPL 1.1/GPL 2.0/LGPL 2.1
//
// Changes list:
//  - Shortcuts support  
//  - ToolStrips support
//  - .net 2 porting
// --------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace FreeCL.UI.Actions
{
	/// <summary>
	/// An ActionList is a list of Actions.
	/// To allow Action.Shortcut worked properly you needed assign action to MenuItem
	/// To allow Action.Shortcuts worked properly you needed assign FreeCL.UI.MainMenu to ActionList
	/// </summary>
	[
		ToolboxBitmap(typeof(ActionList)),
		DefaultProperty("Actions"),
		ProvideProperty("Action", typeof(Component))
	]
	public class ActionList : System.ComponentModel.Component, IExtenderProvider
	{
		#region member variables
		private ActionCollection	_actions;
		private ImageList			_imageList;
		private	object				_tag;
		private	bool				_showTextOnToolBar;
		private	bool				_init;
		private	FreeCL.UI.MainMenu _menu;
		private	bool				_ownerDrawMenus = true;
		private	bool				_showShortcutsinHints;
		// As there is now way to ensure that the designer calls the SetAction method after the call to ActionList.AddRange method, 
		// we need to duplicate the list of assocations between actions and component already contained in the ActionCollection member. 
		// It's not really a problem, because it speeds up all operations based on iteration on the components.
		private Dictionary<Component, Action>	_components = new Dictionary<Component, Action>();
		internal System.Windows.Forms.ToolTip _toolTip;
		private System.ComponentModel.IContainer components;
		#endregion
		#region public interface
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="container"></param>
		public ActionList(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			container.Add(this);
			InitializeComponent();
			//
			Init();
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public ActionList()
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			InitializeComponent();
			//
			Init();
		}
		/// <summary>
		/// The number of Actions in this ActionList
		/// </summary>
		[Browsable(false)]
		public int Count
		{
			get
			{
				return _actions.Count;
			}
		}
		/// <summary>
		/// The collection of Actions that makes up this ActionList
		/// </summary>
		[
			DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
			Category("Behavior"), 
			Description("The collection of Actions that makes up this ActionList"),
			SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")
		]
		public ActionCollection Actions
		{
			get
			{
				return _actions;
			}
			/*set
			{
				_actions = value;
			}*/
		}
		/// <summary>
		/// The ImageList from which this ActionList will get all of the action images.
		/// </summary>
		[
		Category("Behavior"), 
		Description("The ImageList from which this ActionList will get all of the action images."),
		DefaultValue((string)null)
		]
		public ImageList ImageList
		{
			get
			{
				return _imageList;
			}
			set
			{
				_imageList = value;
				foreach(Action a in Actions)
				{
					a.ImageList = _imageList;
				}
			}
		}
		/// <summary>
		/// User defined data associated with this ActionList.
		/// </summary>
		[
		Category("Data"), 
		Description("User defined data associated with this ActionList.")
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
		/// Show text on toolbar
		/// </summary>
		[
		Category("Behavior"), 
		Description("Show text on toolbar"),
		DefaultValue(false)
		]
		public bool ShowTextOnToolBar
		{
			get
			{
				return _showTextOnToolBar;
			}
			set 
			{
				_showTextOnToolBar = value;
				foreach(Action a in Actions)
				{
					a.ShowTextOnToolBar = value;
				}
			}
		}

		/// <summary>
		/// Show shortcuts in hints
		/// </summary>
		[
		Category("Behavior"), 
		Description("Show shortcuts in hints."),
		DefaultValue(false)
		]
		public bool ShowShortcutsInHints
		{
			get
			{
				return _showShortcutsinHints;
			}
			set 
			{
				_showShortcutsinHints = value;
				foreach(Action a in Actions)
				{
					a.Hint = a.Hint; //force refresh
				}
			}
		}

		/// <summary>
		/// OwnerDraw menus
		/// </summary>
		[
		Category("Behavior"), 
		Description("OwnerDraw menus."),
		DefaultValue(true)
		]
		public bool OwnerDrawMenus
		{
			get
			{
				return _ownerDrawMenus;
			}
			set 
			{
				_ownerDrawMenus = value;
				foreach(Action a in Actions)
				{
					a.OwnerDrawMenus = value;
				}
				
				if(_menu != null)
				{
				 foreach (System.Windows.Forms.MenuItem item in _menu.MenuItems)	
				 {
					 HookPopupMenuItem(item);
				 }
				}
			}
		}
		
		[
		Category("Behavior"), 
		Description("ToolTip component for actions"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId="ToolTip")
		]
		public ToolTip ToolTip {
			get { return _toolTip; }
		}
		
		
		private void MakeItemOwnerDraw(System.Windows.Forms.MenuItem item)
		{
			if(_ownerDrawMenus && !item.OwnerDraw)
			{
				item.OwnerDraw = true;	
	 			item.DrawItem += new DrawItemEventHandler(OwnerDrawMenuItem);
	 			item.MeasureItem += new MeasureItemEventHandler(OwnerMeasureMenuItem);
			}
			else if(!_ownerDrawMenus && item.OwnerDraw)
			{
				item.OwnerDraw = false;					
			 	item.DrawItem -= new DrawItemEventHandler(OwnerDrawMenuItem);
			 	item.MeasureItem -= new MeasureItemEventHandler(OwnerMeasureMenuItem);
			}
		}
		 
		private void PopupMenuItem(object sender, System.EventArgs e)
		{
			HandleChildMenuItems(sender as System.Windows.Forms.Menu);
		}

		private void HookPopupMenuItem(System.Windows.Forms.MenuItem item)
		{
			item.Popup += new EventHandler(PopupMenuItem);
		}
			
		private void HandleChildMenuItems(System.Windows.Forms.Menu popupMenu)
		{
			foreach (System.Windows.Forms.MenuItem item in popupMenu.MenuItems) 
			{
				MakeItemOwnerDraw(item);
				HookPopupMenuItem(item);
				HandleChildMenuItems(item);
			}
		}

 		private void OwnerMeasureMenuItem(object sender, MeasureItemEventArgs e)
 		{
			System.Windows.Forms.MenuItem mi = sender as System.Windows.Forms.MenuItem;
			FreeCL.UI.MenuItem.OwnerMeasureMenuItem(mi, e);
 		}
	
 		private void OwnerDrawMenuItem(object sender, DrawItemEventArgs e)
 		{
			System.Windows.Forms.MenuItem mi = sender as System.Windows.Forms.MenuItem;
			FreeCL.UI.MenuItem.OwnerDrawMenuItem(mi, e);
 		}
		
		
		/// <summary>
		/// MainMenu associated with this ActionList.
		/// </summary>
		[
		Category("Misc"), 
		Description("MainMenu associated with this ActionList.To allow Action.Shortcuts worked properly you needed assign FreeCL.UI.MainMenu to ActionList"),
		DefaultValue((string) null)
		]
		public FreeCL.UI.MainMenu Menu
		{
			get
			{
				return _menu;
			}
			set 
			{
				if(!DesignMode && _menu != null)
					_menu.OnProcessCmdKey -= new EventHandler<ProcessCmdKeyEventArgs>(this.MenuOnProcessCmdKey);					
				_menu = value;
				if(!DesignMode && _menu != null)
					_menu.OnProcessCmdKey += new EventHandler<ProcessCmdKeyEventArgs>(this.MenuOnProcessCmdKey);					
		
				if(!DesignMode && OwnerDrawMenus && _menu != null)
					foreach(Action a in Actions)
					{
						a.OwnerDrawMenus = OwnerDrawMenus;
					}				
				
				if(_menu != null)
				{
				 foreach (System.Windows.Forms.MenuItem item in _menu.MenuItems)	
				 {
					 HandleChildMenuItems(item);
				 }
				 _menu.OnMergeMenu += new MergeMenuHandler(OnMergeMenu);
				}
				
			}
		}
		
		void OnMergeMenu(FreeCL.UI.MainMenu owner, System.Windows.Forms.Menu src)
		{
			foreach (System.Windows.Forms.MenuItem item in src.MenuItems)	
				HandleChildMenuItems(item);
		}
		
		void MenuOnProcessCmdKey(object sender, FreeCL.UI.ProcessCmdKeyEventArgs e)
		{
			e.Handled = ProcessKey(e.KeyData);
		}

		public bool ProcessKey(Keys keys)
		{
			if(DesignMode)
			  return false;
			  
			foreach(Action a in Actions)
			{
				if((uint)a.Shortcut == (uint)keys)
				{
					a.DoExecute();
					return true;
				}
					
				foreach(ShortcutKeys sk in a.Shortcuts)
				{
					if(sk.Keys == keys)
					{
						a.DoExecute();
						return true;
					}
				}
			}	
			return false;		
		}
		
		bool _LockAllExecute;
		[
		Category("Behavior"), 
		Description("Force locking of executing of all actions"),
		Browsable(false)
		]
		public bool LockAllExecute
		{
			get
			{
				return _LockAllExecute;
			}
			set 
			{
				_LockAllExecute = value;
			}
		}
		
		/// <summary>
		/// Indicates whether or not labels of ToolBar Buttons are displayed or not.
		/// </summary>
		[
		ExtenderProvidedProperty(),
		TypeConverter(typeof(ActionConverter)),
		Description("Indicates whether or not labels of ToolBar Buttons are displayed or not."),
		Category("Behavior")
		]
		public Action GetAction(Component comp)
		{
			Debug.Assert(comp != null);
			Action res = null;
			if (!_components.TryGetValue(comp, out res))
			{
				return Actions.Null;
			}
			return res;
		}
		/// <summary>
		/// The set method of the extended property Action
		/// </summary>
		[ExtenderProvidedProperty()]
		public void SetAction(Component comp, FreeCL.UI.Actions.Action value) 
		{
			Debug.Assert(comp != null && value != null);
			Action res = null;
			if (_components.TryGetValue(comp, out res))
			{
				if (value == res)
				{
					return;
				}
				res.SetComponent(comp, false);
				_components.Remove(comp);
			}

			if (value != Actions.Null)
			{
				value._owner = this;
				value.SetComponent(comp, true);
				_components.Add(comp, value);
			}
		}
		/// <summary>
		/// We need only to serialize Components wich have associated to an Action
		/// </summary>
		public bool ShouldSerializeAction(object component) 
		{
			Debug.Assert(component != null);
			foreach (Action a in Actions)
			{
				if (a.HandleComponent((Component)component))
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Specifies whether this object can provide its extender properties to the specified object.
		/// </summary>
		/// <param name="target">The Object to receive the extender properties</param>
		public bool CanExtend(Object extendee) 
		{
			return (extendee is Component) && 
				!(extendee is ActionList) && 
				!(extendee is Action) &&
				!(extendee is ShortcutKeys);
		}
		#endregion
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);

		}
		#endregion
		

		#region	internal
		/// <summary>
		/// Common code for initialising the instance
		/// </summary>
		private void Init()
		{
			_actions = new ActionCollection(this);
			if (!DesignMode)
			{
				System.Windows.Forms.Application.Idle += new EventHandler(OnIdle);
			}
		}
		/// <summary>
		/// Occurs when the application is idle so that the action list can update the actions in the list.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void OnIdle(Object sender, EventArgs e)
		{
			// the designer don't put necesseraly the initialisation code in the right order
			// Especially, toolbarbuttons can be assigned to their corresponding toolbar after their corresponding action has been assigned
			// So in this case, we are not able to add an event handler to toolbarbutton.Parent.click

			// so if it was not done, we do the job here
			if (DesignMode) return;
	
			if (!_init)
			{
				if(Actions == null) return;
				foreach(Action a in Actions)
				{
					if(a != null)
						a.FinishInit();
				}
				
				if(_menu != null && Actions != null)
				{
					foreach (System.Windows.Forms.MenuItem item in _menu.MenuItems)	
					{
						HookPopupMenuItem(item);
					}
				}
				
				_init = true;
			}

			// the real work begins here
			if(Actions == null) return;
			
			foreach(Action a in Actions)
			{
				if(a != null)
					a.OnUpdate(this, e);
			}
		}
		/// <summary>
		/// Cleans the action list when an associated component is destroyed
		/// </summary>
		internal void OnComponentDisposed(Object sender, EventArgs e)
		{
			Component comp = (Component)sender;
			Debug.Assert(comp != null);
			Action a = null;
			if (_components.TryGetValue(comp, out a))
			{
				a.SetComponent(comp, false);
				_components.Remove(comp);
			}
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (!DesignMode)
				{
					System.Windows.Forms.Application.Idle -= new EventHandler(OnIdle);
					Menu = null;
				}
				if(components != null)
				{
					components.Dispose();
					components = null;
				}
				
				if(_toolTip != null)
				{
					_toolTip.Dispose();
					_toolTip = null;
				}

				if(_actions != null)
				{
					_actions.Dispose();
					_actions = null;
				}
			}
			base.Dispose( disposing );
		}
		#endregion
	}
}
