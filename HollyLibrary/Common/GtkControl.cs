// GtkControl.cs created with MonoDevelop
// User: dantes at 11:22 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;
using Gdk;

namespace HollyLibrary
{

	
	/// <summary>
	/// A Gtk.Bin that has all the basic events/ properties of the System.Windows.Forms basic control
	/// </summary>
	public class GtkControl : Gtk.EventBox
	{
		//key
		public event GcKeyUp           KeyUp;
		public event GcKeyDown       KeyDown;
		public event GcKeyPressed KeyPressed;
		//mouse
		public event GcMouseMove               MouseMove;
		public event GcMouseUp                   MouseUp;
		public event GcMouseDown               MouseDown;
		public event GcMouseClick             MouseClick;
		public event GcMouseDoubleClick MouseDoubleClick;
		public event GcMouseWheel             MouseWheel;
		//focus
		public event EventHandler               GotFocus;
		public event EventHandler              LostFocus;
		//
		bool suppress_key_press           = false;
		//
		Gdk.Pixbuf backgroundImage        = null;
		ImageLayout backgroundImageLayout = ImageLayout.None;
		ShadowType border                 = ShadowType.In;
		
		public GtkControl()
		{
			this.Events =  EventMask.ExposureMask    | EventMask.LeaveNotifyMask   | EventMask.AllEventsMask     | 
				           EventMask.ScrollMask      | EventMask.EnterNotifyMask   | EventMask.ExposureMask      |
				       	   EventMask.ButtonPressMask | EventMask.PointerMotionMask | EventMask.ButtonReleaseMask |
				       	   EventMask.PointerMotionHintMask;
			this.WidgetFlags       |= WidgetFlags.CanFocus;
			this.AppPaintable       = true;
			
			
		}
		
#region events
		
		//paint me event
		protected override bool OnExposeEvent (EventExpose evnt)
		{
			//deseneaza poza daca este cazul
			if( BackgroundImage != null && Allocation.Width > 5 && Allocation.Height > 5)
			{
				if( BackgroundImageLayout == ImageLayout.None )
				{
					//draw the pixbuf normally
					GdkWindow.DrawPixbuf( this.Style.BaseGC( State), BackgroundImage, 0, 0, 0, 0, BackgroundImage.Width, BackgroundImage.Height, Gdk.RgbDither.Normal,0,0 );
				}
				else if( backgroundImageLayout == ImageLayout.Stretch  )
				{
					//draw pixbuf stretched
					Gdk.Pixbuf buf = BackgroundImage.ScaleSimple( Allocation.Width, Allocation.Height, Gdk.InterpType.Bilinear );
					GdkWindow.DrawPixbuf( this.Style.WhiteGC, buf, 0, 0, 0, 0, Allocation.Width, Allocation.Height, Gdk.RgbDither.Normal, 0, 0 );
				}
				else if( BackgroundImageLayout == ImageLayout.Center )
				{
					//draw the pixbuf on center
					int x = ( Allocation.Width  / 2 ) -  ( BackgroundImage.Width  / 2 );
					int y = ( Allocation.Height / 2 ) -  ( BackgroundImage.Height / 2 );
					GdkWindow.DrawPixbuf( this.Style.BaseGC( State), BackgroundImage, 0, 0, x, y, BackgroundImage.Width, BackgroundImage.Height, Gdk.RgbDither.Normal,0,0 );
				}
				else if( BackgroundImageLayout == ImageLayout.Tile )
				{
					//draw backgorund image as tiles
					int nr_rows = (int)Math.Ceiling( (double)(Allocation.Height / backgroundImage.Height) ) + 1;
					int nr_cols = (int)Math.Ceiling( (double)(Allocation.Width  / backgroundImage.Width ) ) + 1;
					int x = 0, y = 0;
					for( int i = 0; i < nr_rows; i++ )
					{
						for( int j = 0; j < nr_cols; j++ )
						{
							GdkWindow.DrawPixbuf( this.Style.WhiteGC, BackgroundImage, 0, 0, x, y, BackgroundImage.Width, BackgroundImage.Height, Gdk.RgbDither.Normal, x, y );
							x += BackgroundImage.Width;
						}
						y += BackgroundImage.Height;
						x  = 0;
					}
					
				}
				else if( BackgroundImageLayout == ImageLayout.Zoom )
				{
					//draw the zoomed image
					int sx = Allocation.Width,sy = Allocation.Height;
					
					int sizeX,sizeY;
					
					if(((sx+1.0f)/(sy+1.0f))<((BackgroundImage.Width+1.0f)/(1.0f+BackgroundImage.Height)))
					{							    					
						sizeX=sx;
						sizeY=(int)(BackgroundImage.Height*((float)sx)/BackgroundImage.Width);
					}
					else
					{								
						sizeY=sy;
						sizeX=(int)(BackgroundImage.Width*((float)sy)/BackgroundImage.Height);
					}
					
					Gdk.Pixbuf buf = BackgroundImage.ScaleSimple( sizeX, sizeY, Gdk.InterpType.Bilinear );
					
					int x = ( Allocation.Width  / 2 ) -  ( buf.Width  / 2 );
					int y = ( Allocation.Height / 2 ) -  ( buf.Height / 2 );
					
					GdkWindow.DrawPixbuf( this.Style.WhiteGC, buf, 0, 0, x, y, buf.Width, buf.Height, Gdk.RgbDither.Normal, 0, 0 );

				}
				
			}
			                   
			//draw border
			Gdk.Rectangle r = new Gdk.Rectangle( 0, 0, Allocation.Width, Allocation.Height );
            Style.PaintShadow(Style, GdkWindow, State, Border, r, this, "frame", 0, 0, Allocation.Width, Allocation.Height );
			return base.OnExposeEvent (evnt);
		}
		
		

		
		//LOST FOCUS
		protected override bool OnFocusOutEvent (EventFocus evnt)
		{
			if( LostFocus != null )  LostFocus( this, new EventArgs() );
			return base.OnFocusOutEvent (evnt);
		}

		
		//GOT FOCUS
		protected override void OnFocusGrabbed ()
		{
			if( GotFocus != null ) GotFocus( this, new EventArgs() ); 
			base.OnFocusGrabbed ();
		}

		
		//MOUSE WHEEL
		protected override bool OnScrollEvent (EventScroll evnt)
		{
			//daca este inainte este pozitiva, daca este inapoi este negativa valoarea delta
			int delta = 120;
			if( evnt.Direction == ScrollDirection.Down ) delta = 0 - delta;
			GcMouseEventArgs args = new GcMouseEventArgs( this, 0, delta, (int)evnt.X, (int)evnt.Y, MouseButton.None );
			if( MouseWheel != null ) MouseWheel( this, args );
			return base.OnScrollEvent (evnt);
		}

		
		//MOUSE MOVE event
		protected override bool OnMotionNotifyEvent (EventMotion evnt)
		{
			GcMouseEventArgs args = new GcMouseEventArgs( this, 0, 0, (int)evnt.X, (int)evnt.Y, MouseButton.None );
			if( MouseMove != null ) MouseMove( this, args );
			return base.OnMotionNotifyEvent (evnt);
		}

		
		//MOUSE UP event + MOUSE CLICK + MOUSE DBL CLICK
		protected override bool OnButtonReleaseEvent (EventButton evnt)
		{
			MouseButton btn  = MouseButton.None;
			if( evnt.Button == 1 ) btn = MouseButton.Left;
			if( evnt.Button == 2 ) btn = MouseButton.Middle;
			if( evnt.Button == 3 ) btn = MouseButton.Right;
			GcMouseEventArgs args = new GcMouseEventArgs( this, 0, 0, (int)evnt.X, (int)evnt.Y, btn );
			if( MouseUp != null ) MouseUp( this, args );
			return base.OnButtonReleaseEvent (evnt);
		}
		
		//MOUSE DOWN event
		protected override bool OnButtonPressEvent (EventButton evnt)
		{
			MouseButton btn  = MouseButton.None;
			if( evnt.Button == 1 ) btn = MouseButton.Left;
			if( evnt.Button == 2 ) btn = MouseButton.Middle;
			if( evnt.Button == 3 ) btn = MouseButton.Right;
			GcMouseEventArgs args = new GcMouseEventArgs( this, 0, 0, (int)evnt.X, (int)evnt.Y, btn );
			if( MouseDown != null ) MouseDown( this, args );
			
			//fa si clickurile si dublu-clickurile
			
			if( MouseDoubleClick != null && evnt.Type == EventType.TwoButtonPress )
			{
				args = new GcMouseEventArgs( this, 2, 0, (int)evnt.X, (int)evnt.Y, btn );
				MouseDoubleClick( this, args );
			}
			else if( MouseClick != null && evnt.Type == EventType.ButtonPress) 
			{
				args = new GcMouseEventArgs( this, 1, 0, (int)evnt.X, (int)evnt.Y, btn );
				MouseClick( this, args );
			}
			
			return base.OnButtonPressEvent (evnt);
		}

		//KEY UP event
		protected override bool OnKeyReleaseEvent (EventKey evnt)
		{
			GcKeyEventArgs args   = new GcKeyEventArgs( this, evnt, suppress_key_press );
			if( KeyUp != null )  
			{
				KeyUp( this, args );
				suppress_key_press = args.SuppressKeyPress;
				if( !suppress_key_press )
				{
					//trigger the keypress event
					if( KeyPressed != null ) KeyPressed( this, args );
				}
				suppress_key_press = false;
				return args.Handled;
			}
			return base.OnKeyReleaseEvent (evnt);
		}
		
		//KEY DOWN event
		protected override bool OnKeyPressEvent (EventKey evnt)
		{
			GcKeyEventArgs args   = new GcKeyEventArgs( this, evnt, suppress_key_press );
			if( KeyDown != null )  
			{
				KeyDown( this, args );
				suppress_key_press = args.SuppressKeyPress;
				return args.Handled;
			}
			return base.OnKeyPressEvent (evnt);
		}

#endregion
		
#region properties
		public Gdk.Color BackColor
		{
			get
			{
				return this.Style.Background( this.State );
			}
			set
			{
				this.ModifyBg( this.State, value );
			}
		}
		
		public Gdk.Pixbuf BackgroundImage
		{
			get
			{
				return backgroundImage;
			}
			set
			{
				backgroundImage = value;
				QueueDraw();
			}
		}
		
		public Pango.FontDescription Font
		{
			get
			{
				return this.PangoContext.FontDescription;
			}
		}

		public ShadowType Border {
			get {
				return border;
			}
			set {
				border = value;
			}
		}

		public ImageLayout BackgroundImageLayout {
			get {
				return backgroundImageLayout;
			}
			set {
				backgroundImageLayout = value;
			}
		}

#endregion
		
	}
}
