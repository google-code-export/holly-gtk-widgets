// HComboFolder.cs created with MonoDevelop
// User: dantes at 11:23 AM 5/28/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HComboFolder : Gtk.Bin
	{
		int dropDownHeight        = 300;
		FolderChooserDialog popup;
		
		
		
		public HComboFolder()
		{
			this.Build();
			popup       = new FolderChooserDialog( this );
			comboBox.PopupButton.Clicked += OnButtonClicked;
			String path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop );
			this.SelectedPath = path;
		}
		
		private void OnButtonClicked( object sender, EventArgs args )
		{
						int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			if( DropDownHeight == 0 ) DropDownHeight = 300;
			popup.ShowMe( new Gdk.Rectangle( x, y, Allocation.Width, DropDownHeight), comboBox.Entry.Text );
		}
		
		public string SelectedPath
		{
			get
			{
				return comboBox.Entry.Text;
			}
			set
			{
				comboBox.Entry.Text = value;
			}
		}
		
		public int DropDownHeight 
		{
			get 
			{
				return dropDownHeight;
			}
			set 
			{
				dropDownHeight = value;
			}
		}
	}
}
