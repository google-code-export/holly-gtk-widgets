// CellRendererCustom.cs created with MonoDevelop
// User: dantes at 12:42 PM 5/16/2008
//

using System;
using Gtk;
using Gdk;

namespace HollyLibrary
{

	public class CellRendererCustom : CellRenderer
	{
		HSimpleList Father;
		private int itemIndex = -1;
		
		public CellRendererCustom( HSimpleList father ) 
		{
			this.Father = father;
		}
		
		public override void GetSize (Widget widget, ref Rectangle cell_area, out int x_offset, out int y_offset, out int width, out int height)
		{
			Rectangle result;
			Father.MeasureItem( ItemIndex, widget, ref cell_area, out result );
			x_offset = result.X;
			y_offset = result.Y;
			width    = result.Width;
			height   = result.Height;
		}

		protected override void Render (Drawable window, Widget widget, Rectangle background_area, Rectangle cell_area, Rectangle expose_area, CellRendererState flags)
		{	
			Father.DrawItem(ItemIndex, window, widget, background_area, cell_area, expose_area, flags );
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
		
	}
}
