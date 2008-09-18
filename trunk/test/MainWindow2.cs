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


			hlabel1.TextOverwritesIcon = false;
			hlabel1.HorizontalLine     = true;
			hlabel1.TextPosition       = HPosition.Left;
			hlabel1.IconPosition       = HPosition.Left;
			
			//for example, get the theme folder icon
			Gtk.IconTheme theme = Gtk.IconTheme.Default;
			Gdk.Pixbuf buf      = theme.LoadIcon("folder",24, Gtk.IconLookupFlags.ForceSvg);
			//set icon for label
			hlabel1.Icon        = buf;
		}

		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
		}
	}
}
