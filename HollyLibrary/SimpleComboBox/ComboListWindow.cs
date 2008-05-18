// ComboListWindow.cs created with MonoDevelop
// User: dantes at 2:22 PM 5/18/2008
//

using System;

namespace HollyLibrary
{

	public partial class ComboListWindow : Gtk.Window
	{
		
		public ComboListWindow() : 
				base(Gtk.WindowType.Popup)
		{
			this.Build();
			this.Visible        = false;
		}
		
		public void ShowMe( int x, int y, int width, int height, Object SelectedItem )
		{
			this.Move  ( x    , y      );
			this.Resize( width, height );
			this.TvList.SelectedItem = SelectedItem;
			this.ShowAll();
			//grab focus
			GrabUtil.GrabWindow( this );
		}
		
		private void Close()
		{
			//remove focus
			GrabUtil.RemoveGrab( this );
			this.Hide();
		}
		
		public HSimpleList List
		{
			get
			{
				return TvList;
			}
		}
		
		
	}
}
