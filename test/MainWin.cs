// MainWin.cs created with MonoDevelop
// User: dantes at 9:35 PM 5/18/2008
//

using System;

namespace test
{
	
	
	public partial class MainWin : Gtk.Window
	{
		
		public MainWin() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			this.hregexentry1.RegularExpression = "\\d{3}-\\d{2}-\\d{4}";
		}
		
	
		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		
	}
}
