// GrabUtil.cs created with MonoDevelop
// User: dantes at 7:52 PM 4/13/2008
//
/*
 * exemplu:
 * public TestDlg( int x, int y ) : base( Gtk.WindowType.Popup )
		{
			this.ButtonPressEvent += new ButtonPressEventHandler( this.on_button_press );
			Gtk.Button btn = new Gtk.Button("test");
			
			this.Add( btn );
			
			//btn.Show();
			this.Move( x, y );
			this.ShowAll();
			
			HollyLibrary.GrabUtil.GrabWindow( this );
		}
		public void on_button_press( object sender, ButtonPressEventArgs args )
		{
			HideMe();
		}
		
		public static void ShowMe( int x, int y )
		{
			new TestDlg( x, y );
		}
		
		public void HideMe()
        {
               GrabUtil.RemoveGrab(this);
               this.Destroy();
        }
*/

using System;
using Gtk;
namespace HollyLibrary
{
	
	
	public class GrabUtil
	{
		private static uint CURRENT_TIME = 0; 
		
		public static void GrabWindow( Gtk.Window window )
		{
			window.GrabFocus();
			
			Grab.Add(window);

            Gdk.GrabStatus grabbed   = Gdk.Pointer.Grab(window.GdkWindow, true,
                                        Gdk.EventMask.ButtonPressMask
                                        | Gdk.EventMask.ButtonReleaseMask
                                        | Gdk.EventMask.PointerMotionMask, null, null, CURRENT_TIME);

            if (grabbed == Gdk.GrabStatus.Success)
			{
				grabbed = Gdk.Keyboard.Grab(window.GdkWindow, true, CURRENT_TIME);

				if (grabbed != Gdk.GrabStatus.Success) 
				{
					Grab.Remove(window);
					window.Destroy();
				}
			} 
			else 
			{
				Grab.Remove(window);
				window.Destroy();
			}
		}
		
		public static void RemoveGrab( Gtk.Window window )
		{
			Grab.Remove(window);
			Gdk.Pointer.Ungrab (CURRENT_TIME);
			Gdk.Keyboard.Ungrab(CURRENT_TIME);
		}
		
	}
}
