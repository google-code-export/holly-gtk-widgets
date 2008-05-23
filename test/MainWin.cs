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
			String text = "My first line of text \r\n";
			text       += "Second line of text bla bla \r\n";
			text       += "last line of text";
			HollyLibrary.HToolTip.ToolTipInterval = 100;
			HollyLibrary.HToolTip.AddToolTip( button2, "title 2", text, "gtk-yes" );
			HollyLibrary.HToolTip.AddToolTip( button3, "title 3", text + text, System.Drawing.Color.White, System.Drawing.Color.Black );
			HollyLibrary.HToolTip.AddToolTip( button4, "title 4", "buga buga", System.Drawing.Color.Yellow, System.Drawing.Color.Orange, "gtk-no" );
		}

		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		
	}
}
