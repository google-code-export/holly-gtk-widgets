// ComboListWindow.cs created with MonoDevelop
// User: dantes at 2:22 PM 5/18/2008
//

using System;

namespace HollyLibrary
{

	public partial class ComboListWindow : Gtk.Window
	{
		HSimpleComboBox combo_parent;
		
		public ComboListWindow( HSimpleComboBox combo_parent ) : 
				base(Gtk.WindowType.Popup)
		{
			this.combo_parent   = combo_parent;
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
			this.TvList.GrabFocus();
			//invoke DropDownOpened
			combo_parent.OnDropDownOpened( this, new EventArgs() );
		}
		
		private void Close()
		{
			//remove focus
			GrabUtil.RemoveGrab( this );
			this.Hide();
			combo_parent.OnDropDownClosed( this, new EventArgs() );
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
