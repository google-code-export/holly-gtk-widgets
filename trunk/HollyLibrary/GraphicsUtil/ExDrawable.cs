// ExDrawable.cs created with MonoDevelop
// User: dantes at 12:27 AM 10/7/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gdk;

namespace HollyLibrary
{
	
	/// <summary>
	/// An extended drawable, more flexibile then the simple Gdk one
	/// </summary>
	public class ExDrawable : Drawable
	{
			
		public void DrawArc( Gdk.GC gc, bool filled, Rectangle rect, int angle1, int angle2 )
		{
			this.DrawArc( gc, filled, rect.X, rect.Y, rect.Width, rect.Height, angle1, angle2 );
		}
		
		public void DrawArc( Gdk.GC gc, bool filled, Point p, Size s, int angle1, int angle2 )
		{
			Rectangle rect = new Rectangle( p, s );
			this.DrawArc( gc, filled, rect.X, rect.Y, rect.Width, rect.Height, angle1, angle2 );
		}	
		
		public void DrawDrawable( Gdk.GC gc, Drawable src, Point src_location, Point dest_location, Size sz )
		{
			this.DrawDrawable( gc, src, src_location.X, src_location.Y, dest_location.X, dest_location.Y, sz.Width, sz.Height );
		}
		
		public void DrawText( Gdk.GC gc, Gtk.Widget widget, String text, int x, int y )
		{
			Pango.Layout l = new Pango.Layout( widget.PangoContext );
			l.SetText( text );
			this.DrawLayout( gc, x, y, l );
		}
		
		public static ExDrawable fromDrawable( Drawable d )
		{
			return (ExDrawable) d;
		}
		
	}
}
