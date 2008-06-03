// HDateEditDlg.cs created with MonoDevelop
// User: dantes at 2:59 PM 4/14/2008


using System;
using Gtk;
using Gdk;

namespace HollyLibrary
{
	
	
	public partial class HDateEditDlg : Gtk.Window
	{
		
		public HDateEditDlg( Widget parent, DateTime default_value, int x, int y ) : 
				base(Gtk.WindowType.Popup)
		{	
			this.Parent = parent;
			this.Build();
			this.Move( x, y );
	
		}
			
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			base.OnExposeEvent (args);
			
			int winWidth, winHeight;
			this.GetSize (out winWidth, out winHeight);
			this.GdkWindow.DrawRectangle (this.Style.ForegroundGC (Gtk.StateType.Insensitive), false, 0, 0, winWidth-1, winHeight-1);
			return false;
		}
		
		public static DateTime ShowMe( Widget parent, DateTime default_value, int x, int y )
		{
			DateTime ret     = default_value;
			HDateEditDlg dlg = new HDateEditDlg( parent, default_value, x, y );
			dlg.ShowAll();
			return ret;
		}

		protected virtual void OnFocusOutEvent (object o, Gtk.FocusOutEventArgs args)
		{
			Console.WriteLine("Buga");
		}

		protected virtual void OnLeaveNotifyEvent (object o, Gtk.LeaveNotifyEventArgs args)
		{
			Console.WriteLine( args.Event.State );
		}

		protected virtual void OnProximityOutEvent (object o, Gtk.ProximityOutEventArgs args)
		{
			Console.WriteLine( "asfasdf" );
		}

		protected virtual void OnMotionNotifyEvent (object o, Gtk.MotionNotifyEventArgs args)
		{
			//Console.WriteLine( "motion" );
		}

		protected virtual void OnStateChanged (object o, Gtk.StateChangedArgs args)
		{
			Console.WriteLine( "state" );
		}

		protected virtual void OnButton1FocusOutEvent (object o, Gtk.FocusOutEventArgs args)
		{
			Console.WriteLine("blahblah!");
		}

		protected virtual void OnEventbox1FocusOutEvent (object o, Gtk.FocusOutEventArgs args)
		{
			Console.WriteLine("asdfasf");
		}
	


	}
}
