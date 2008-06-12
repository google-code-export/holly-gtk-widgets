// MeasureItemEventArgs.cs created with MonoDevelop
// User: dantes at 2:18 PM 5/16/2008
//

using System;

namespace HollyLibrary
{
	
	
	public class MeasureItemEventArgs : EventArgs
	{
		private int index;
		int itemLeft, itemTop, itemHeight, itemWidth;
		private Gtk.TreeIter iter;
		
		public int Index 
		{
			get 
			{
				return index;
			}
		}

		public int ItemLeft {
			get {
				return itemLeft;
			}
			set {
				itemLeft = value;
			}
		}

		public int ItemTop {
			get {
				return itemTop;
			}
			set {
				itemTop = value;
			}
		}

		public int ItemHeight {
			get {
				return itemHeight;
			}
			set {
				itemHeight = value;
			}
		}

		public int ItemWidth {
			get {
				return itemWidth;
			}
			set {
				itemWidth = value;
			}
		}

		public Gtk.TreeIter Iter 
		{
			get 
			{
				return iter;
			}
			internal set 
			{
				iter = value;
			}
		}
		
		public MeasureItemEventArgs( int index, Gtk.TreeIter iter, Gdk.Rectangle rect )
		{
			this.index = index;
			this.ItemHeight = rect.Height;
			this.ItemWidth  = rect.Width;
			this.ItemLeft   = rect.X;
			this.ItemTop    = rect.Y;
			this.iter       = iter;
		}
	}
	
}
