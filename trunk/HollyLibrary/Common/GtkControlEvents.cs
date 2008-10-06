// GtkControlEvents.cs created with MonoDevelop
// User: dantes at 11:13 PM 10/6/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary
{
	
	//key
	public delegate void GcKeyUp      ( object sender, GcKeyEventArgs args  );
	public delegate void GcKeyDown    ( object sender, GcKeyEventArgs args  );
	public delegate void GcKeyPressed ( object sender, GcKeyEventArgs args  );
	//mouse
	public delegate void GcMouseUp    ( object sender, GcMouseEventArgs args  );
	public delegate void GcMouseDown  ( object sender, GcMouseEventArgs args  );
	public delegate void GcMouseMove  ( object sender, GcMouseEventArgs args  );
	public delegate void GcMouseClick ( object sender, GcMouseEventArgs args  );
	public delegate void GcMouseWheel ( object sender, GcMouseEventArgs args  );
	public delegate void GcMouseDoubleClick( object sender, GcMouseEventArgs args  );
}
