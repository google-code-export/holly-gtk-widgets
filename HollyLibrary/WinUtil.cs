// WinUtil.cs created with MonoDevelop
// User: dantes at 3:21 PM 5/6/2008
//

using System;

namespace HollyLibrary
{
	
	
	public class WinUtil
	{
		/// <summary>
		/// returns the top gtk.window of a gtk.widget 
		/// </summary>
		/// <param name="widget">
		/// the widget <see cref="Gtk.Widget"/>
		/// </param>
		/// <returns>
		/// the gtk.window <see cref="Gtk.Window"/>
		/// </returns>
		public static Gtk.Window getTopWindow( Gtk.Widget widget )  
		{
			Gtk.Widget w = widget.Parent;
			while (w != null && !(w is Gtk.Window))
				w = w.Parent;
			return (Gtk.Window) w;			
		}
		
	}
}
