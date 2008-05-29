// HComboFolder.cs created with MonoDevelop
// User: dantes at 11:23 AM 5/28/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HComboFolder : Gtk.Bin
	{
		//events
		public event EventHandler PathChanged;
		//vars
		int dropDownHeight        = 300;
		//popup dialog with folder chooser tree
		FolderChooserDialog popup;
		
		public HComboFolder()
		{
			this.Build();
			//create popup
			popup       = new FolderChooserDialog( this );
			//
			comboBox.PopupButton.Clicked    += OnButtonClicked;
			//put desktop as default folder
			Environment.SpecialFolder folder = Environment.SpecialFolder.Desktop ;
			this.SelectedPath = Environment.GetFolderPath( folder );
		}
		
		private void OnButtonClicked( object sender, EventArgs args )
		{
			int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			//don't allow a 0 height
			if( DropDownHeight == 0 ) DropDownHeight = 300;
			//show popup
			Gdk.Rectangle region = new Gdk.Rectangle( x, y, Allocation.Width, DropDownHeight);
			popup.ShowMe( region, comboBox.Entry.Text );
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
				if( PathChanged != null ) PathChanged( this, new EventArgs() );
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
