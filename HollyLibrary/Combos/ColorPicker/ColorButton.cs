// ColorButton.cs created with MonoDevelop
// User: dantes at 1:44 PM 8/27/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gdk;

namespace HollyLibrary
{
	
	
	public class ColorButton : Gtk.EventBox
	{
		
		public ColorButton( Color c )
		{
			this.ModifyBg( Gtk.StateType.Normal  , c );
			this.ModifyBg( Gtk.StateType.Selected, c );
		}
		
	}
}
