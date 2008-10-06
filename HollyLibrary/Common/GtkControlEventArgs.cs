// GtkControlEventArgs.cs created with MonoDevelop
// User: dantes at 6:36 PM 10/6/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;
using Gdk;

namespace HollyLibrary
{
	
	/*
	   key up
		key down
		key pressed
				
		mouse up
		mouse down
		mouse click
		mouse double click
		mouse move
		
				
		mouse wheel
				
		paint ( with graphics object )
				
		got Focus
		lost Focus
	*/
	
	public enum ImageLayout
	{
		None, Tile, Center, Stretch, Zoom
	}
	
	public enum MouseButton
	{
		Left, Right, Middle, None
	}
	
	public class GcMouseEventArgs : EventArgs
	{
		MouseButton button = MouseButton.None;
		int clicks         = 0;
		int delta          = 0;
		int x = 0, y = 0;
		Gtk.Widget widget  = null;
		
		public GcMouseEventArgs( Widget sender, int clicks, int delta, int x, int y, MouseButton button )
		{
			this.widget = sender;
			this.clicks = clicks;
			this.delta  = delta;
			this.x      = x;
			this.y      = y;
			this.button = button;
		}
		
#region properties
		public int Y {
			get {
				return y;
			}
		}
		
		public int X {
			get {
				return x;
			}
		}
		
		public Point Location {
			get {
				return new Gdk.Point( this.x, this.y );
			}
		}
		
		public int Delta {
			get {
				return delta;
			}
		}
		
		public int Clicks {
			get {
				return clicks;
			}
		}
		
		public MouseButton Button {
			get {
				return button;
			}
		}

		public Widget Widget 
		{
			get {
				return widget;
			}
		}
		
#endregion
		
	}
	
	public class GcKeyEventArgs : EventArgs
	{
		private bool alt;
		private bool shift;
		private bool control;
		
		private bool suppressKeyPress;
		
		private Widget widget;
		private bool handled = true;
		private Gdk.Key keyCode;
		
		private int keyValue;
		
		public GcKeyEventArgs( Gtk.Widget sender, Gdk.EventKey args, bool key_pressed_suppressed )
		{
			
			control  = ( (args.State & ModifierType.ControlMask) == ModifierType.ControlMask );
			shift    = ( (args.State & ModifierType.ShiftMask  ) == ModifierType.ShiftMask   );
			alt      = ( (args.State & ModifierType.Mod1Mask   ) == ModifierType.Mod1Mask    );
			widget   = sender;
			keyCode  = args.Key;
			keyValue = (int)args.KeyValue;
			suppressKeyPress = key_pressed_suppressed;
		}
			
#region properties
		
		    
		public bool Alt
		{
			get
			{
				return alt;
			}
		}

		public Widget Widget {
			get {
				return widget;
			}
		}

		public bool SuppressKeyPress {
			get {
				return suppressKeyPress;
			}
			set
			{
				suppressKeyPress = value;
			}
		}

		public bool Shift {
			get {
				return shift;
			}
		}

		public int KeyValue {
			get {
				return keyValue;
			}
		}


		public Gdk.Key KeyCode {
			get {
				return keyCode;
			}
		}

		public bool Handled {
			get {
				return handled;
			}
			set
			{
				handled = value;
			}
		}

		public bool Control {
			get {
				return control;
			}
		}
	
		
#endregion
		
	}
	
	
	
		
		
	

	
}
