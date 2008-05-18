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
		
		public void ShowMe( int x, int y, int width, int height )
		{
			if( height == 0 ) height = 200;
			this.Move  ( x    , y      );
			this.Resize( width, height );
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

		protected virtual void OnButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			Close();
		}

		protected virtual void OnTvListRowActivated (object o, Gtk.RowActivatedArgs args)
		{
			Close();
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
