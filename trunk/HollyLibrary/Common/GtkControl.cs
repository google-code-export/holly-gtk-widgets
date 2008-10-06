// GtkControl.cs created with MonoDevelop
// User: dantes at 11:22 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary
{
	
	/// <summary>
	/// A Gtk.Bin that has all the basic events/ properties of the System.Windows.Forms basic control
	/// </summary>
	public class GtkControl : Gtk.Bin
	{
		/*
		EVENTS:
			key up
			key down
			key pressed
			mouse up
			mouse down
			mouse click
			mouse double click
			mouse wheel
			paint ( with graphics object )
			got Focus
			lost Focus
		PROPERTIES:
			back color
			background image
			bounds
			client size
			font ( Pango.FontDescription )
		METHODS:
			invalidate() ( rect, bool ) ( rect )
		*/
		public GtkControl()
		{
		}
	}
}
