// HComboTree.cs created with MonoDevelop
// User: dantes at 6:42 PM 5/26/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HComboTree : Gtk.Bin
	{
		
		TreePopupDialog Popup;
		
		int  dropDownHeight = 300;
		
		public HComboTree()
		{
			this.Build();
			Popup = new TreePopupDialog( this );
			comboBox.PopupButton.Clicked += OnPopupButtonClicked;
		}

		private void OnPopupButtonClicked( object sender, EventArgs args )
		{
			int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			if( DropDownHeight == 0 ) DropDownHeight = 300;
			Popup.ShowMe( new Gdk.Rectangle( x, y, Allocation.Width, DropDownHeight ) );
		}
		
		public string Text
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
		
		public HTreeView Tree
		{
			get
			{
				return Popup.TreeView;
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
