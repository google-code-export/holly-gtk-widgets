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
		int dropDownHeight    = 200;
		int dropDownWidth     = 0;

		public HSimpleComboBox()
		{
			this.Build();
			this.comboBox.Entry.IsEditable          = false;
			this.comboBox.Entry.KeyReleaseEvent    += new Gtk.KeyReleaseEventHandler( this.on_entry_key_pressed );
			this.comboBox.PopupButton.Clicked      += new EventHandler( this.on_popup_open        );
			this.Popup.List.OnSelectedIndexChanged += new EventHandler( this.on_list_item_changed );
		}
		
		private void on_entry_key_pressed ( object sender, Gtk.KeyReleaseEventArgs args )
		{
			if( args.Event.Key != Gdk.Key.Tab ) ShowPopup();
		}
		
		private void on_list_item_changed( object sender, EventArgs args )
		{
			comboBox.Entry.Text = this.Popup.List.Text;
		}

		protected override void OnSizeAllocated (Gdk.Rectangle allocation)
		{
			base.OnSizeAllocated (allocation);
			this.dropDownWidth = allocation.Width;
		}
		
		private void on_popup_open( object sender, EventArgs args )
		{
			ShowPopup();
		}
		
		public void ShowPopup()
		{
			int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			//show list popup
			Popup.ShowMe( x, y, dropDownWidth, DropDownHeight );
		}
		
		public string Text
		{
			get
			{
				return comboBox.Entry.Text;
			}
		}
		
		public HSimpleList List
		{
			get
			{
				return Popup.List;
			}
		}

		public int DropDownWidth 
		{
			get 
			{
				return dropDownWidth;
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
