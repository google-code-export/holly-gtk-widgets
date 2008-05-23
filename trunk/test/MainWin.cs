// MainWin.cs created with MonoDevelop
// User: dantes at 9:35 PM 5/18/2008
//

using System;
using System.Drawing;

namespace test
{
	
	
	public partial class MainWin : Gtk.Window
	{
		
		public MainWin() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Resize( 640, 480 );
			this.Build();
			
			this.hregexentry1.RegularExpression = "\\d{3}-\\d{2}-\\d{4}";
			String text = "testoi asdlf jasd kfjasd fjad;s jfad;lsjfa kdsfal sfa jfad ;sfad sa; jfal jf\r\n asdjf l;akdjsf kdf ljadsf; kjasd; jfaf";
			
			HollyLibrary.HToolTip.AddToolTip( button2, "titlu 1", text );
			HollyLibrary.HToolTip.AddToolTip( button3, "titlu 2", text + text );
			HollyLibrary.HToolTip.AddToolTip( button4, "titlu 3", "buga buga" );
		}

		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		
	}
}
