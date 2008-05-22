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
		
		/// <summary>
		/// this function modifies a window shape
		/// </summary>
		/// <param name="bmp">
		/// the bitmap that has the shape of the window. <see cref="System.Drawing.Bitmap"/>
		/// </param>
		/// <param name="window">
		/// the window to apply the shape change <see cref="Gtk.Window"/>
		/// </param>
		/// <returns>
		/// true if it succeded, either return false <see cref="System.Boolean"/>
		/// </returns>
		public static bool ModifyWindowShape( System.Drawing.Bitmap bmp, Gtk.Window window )
		{
			bool ret = false;
			try
			{
				//save bitmap to stream
				System.IO.MemoryStream stream = new System.IO.MemoryStream();
				bmp.Save( stream, System.Drawing.Imaging.ImageFormat.Png );
				//verry important: put stream on position 0
				stream.Position     = 0;
				//get the pixmap mask
				Gdk.Pixbuf buf      = new Gdk.Pixbuf( stream, bmp.Width, bmp.Height );
				Gdk.Pixmap map1, map2;
				buf.RenderPixmapAndMask( out map1, out map2, 255 );
				//shape combine window 
				window.ShapeCombineMask( map2, 0, 0 );
				//dispose
				buf.Dispose();
				map1.Dispose();
				map2.Dispose();
				bmp.Dispose();
				//if evrything is ok return true;
				ret = true;
			}
			catch(Exception ex)
			{
				Console.WriteLine( ex.Message + "\r\n" + ex.StackTrace );
			}
			return ret;
		}
		
	}
}
