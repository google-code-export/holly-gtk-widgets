// test.cs created with MonoDevelop
// User: dantes at 11:08 AM 9/19/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;

namespace test
{
	
	
	class testuu: Window
	{
	    public HollyLibrary.HIpEntry ipentry;
	
	    public testuu(): base("Main window")
	    {
	        Gtk.VBox vbox = new Gtk.VBox(false, 10);
	        Add(vbox);
	       
	        ipentry = new HollyLibrary.HIpEntry();
	        ipentry.Text = "255.255.255.255";
	        vbox.PackStart(ipentry, false, false, 0);
	       
	        ShowAll();
	    }
	}
}
