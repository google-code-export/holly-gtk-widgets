// CellRendererCustom.cs created with MonoDevelop
// User: dantes at 12:42 PM 5/16/2008
//

using System;
using Gtk;
using Gdk;

namespace HollyLibrary
{

	public class CellRendererCustom : CellRendererText
	{
		ICustomCellTreeView Father;
		private int itemIndex = -1;
		private TreeIter iter;
		
		public CellRendererCustom( ICustomCellTreeView father ) 
		{
			this.Father = father;
			this.Mode   = CellRendererMode.Editable;
		}
		
		public override void GetSize (Widget widget, ref Rectangle cell_area, out int x_offset, out int y_offset, out int width, out int height)
		{
			Rectangle result;
			Father.OnMeasureItem( ItemIndex, Iter, widget, ref cell_area, out result );
			x_offset = result.X;
			y_offset = result.Y;
			width    = result.Width;
			height   = result.Height;
		}

		protected override void Render (Drawable window, Widget widget, Rectangle background_area, Rectangle cell_area, Rectangle expose_area, CellRendererState flags)
		{	
			Father.OnDrawItem(ItemIndex, Iter, window, widget, background_area, cell_area, expose_area, flags );
		}
		
		public int ItemIndex 
		{
			get 
			{
				return itemIndex;
			}
			set 
			{
				itemIndex = value;
			}
		}

		public TreeIter Iter {
			get {
				return iter;
			}
			set {
				iter = value;
			}
		}
		
	}
}
