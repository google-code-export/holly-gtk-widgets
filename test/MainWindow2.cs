// MainWindow2.cs created with MonoDevelop
// User: dantes at 10:54 AM 9/16/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using HollyLibrary;


namespace test
{
	
	
	public partial class MainWindow2 : Gtk.Window
	{
		
		public MainWindow2() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();

			hlabel1.TextOweritesIcon = false;
			hlabel1.HorizontalLine   = true;
			
			Gtk.IconTheme theme = Gtk.IconTheme.Default;
			Gdk.Pixbuf buf      = theme.LoadIcon("folder",24, Gtk.IconLookupFlags.ForceSvg);
			
			hlabel1.Icon        = buf;
		}

		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
		}
	}
}
