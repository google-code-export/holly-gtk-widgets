// ICustomCellSupport.cs created with MonoDevelop
// User: dantes at 3:01 PM 6/3/2008
//

using System;
using Gtk;

namespace HollyLibrary
{
	
	
	public interface ICustomCellTreeView
	{
		
		//on measure item 
		void OnMeasureItem ( int ItemIndex, Widget widget, ref Gdk.Rectangle cell_area, out Gdk.Rectangle result );
		
		//on draw item 
		void OnDrawItem ( int ItemIndex, Gdk.Drawable window, Widget widget, Gdk.Rectangle background_area, Gdk.Rectangle cell_area, Gdk.Rectangle expose_area, CellRendererState flags);
	}
}
