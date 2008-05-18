// HSimpleComboBox.cs created with MonoDevelop
// User: dantes at 2:21 PM 5/18/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HSimpleComboBox : Gtk.Bin
	{
		//popup windor
		ComboListWindow Popup = new ComboListWindow();
		//properties
		object selectedItem   = null;
		int dropDownHeight    = 300, dropDownWidth = 200;

		public HSimpleComboBox()
		{
			this.Build();
			DropDownWidth = this.Allocation.Width;
			this.comboBox.PopupButton.Clicked += new EventHandler( this.on_popup_open );
		}
		
		private void on_popup_open( object sender, EventArgs args )
		{
			int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			//show list popup
			Popup.ShowMe( x, y, DropDownWidth, DropDownHeight, SelectedItem );
		}
		
		//TODO: implement this
		public int SelectedIndex
		{
			get
			{
				//
				return -1;
			}
			set
			{
				//
			}
		}
		//TODO: implement this
		public string SelectedText
		{
			get
			{
				return comboBox.Entry.Text;
			}
			set
			{
				Popup.List.Text = value;
			}
		}
		
		//TODO:  implement this 
		public object SelectedItem 
		{
			get 
			{
				return selectedItem;
			}
			set 
			{
				selectedItem = value;
			}
		}

		public int DropDownWidth 
		{
			get 
			{
				return dropDownWidth;
			}
			set 
			{
				dropDownWidth = value;
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
