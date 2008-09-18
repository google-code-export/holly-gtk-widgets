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

			himagecheckbox1.CheckedImage   = GraphUtil.pixbufFromStock("gtk-yes", Gtk.IconSize.Button );
			himagecheckbox1.UncheckedImage = GraphUtil.pixbufFromStock("gtk-no" , Gtk.IconSize.Button );
			
		}

		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
		}
	}
}
