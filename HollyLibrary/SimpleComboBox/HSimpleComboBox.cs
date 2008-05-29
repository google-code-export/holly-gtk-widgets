// HSimpleComboBox.cs created with MonoDevelop
// User: dantes at 2:21 PM 5/18/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HSimpleComboBox : Gtk.Bin
	{
		//popup windor
		ComboListWindow Popup = null;
		//properties
		int dropDownHeight    = 200;
		int dropDownWidth     = 0;
		
		public event EventHandler DropDownOpened;
		public event EventHandler DropDownClosed;
		public event EventHandler TextChanged;

		public HSimpleComboBox()
		{
			this.Popup                              = new ComboListWindow( this );
			this.Build();
			this.IsEditable                         = false;
			this.comboBox.Entry.KeyPressEvent      += new Gtk.KeyPressEventHandler( this.on_entry_key_pressed );
			this.comboBox.Entry.Changed            += new EventHandler( this.OnTextChanged        );
			this.comboBox.PopupButton.Clicked      += new EventHandler( this.on_popup_open        );
			this.Popup.List.SelectedIndexChanged += new EventHandler( this.on_list_item_changed );
			}
		
		public virtual void OnDropDownOpened( object sender, EventArgs args )
		{
			if( DropDownOpened != null ) DropDownOpened( sender, args );
		}
		
		public virtual void  OnDropDownClosed( object sender, EventArgs args )
		{
			if( DropDownClosed != null ) DropDownClosed( sender, args );
		}
		
		public virtual  void OnTextChanged( object sender, EventArgs args )
		{
			if( TextChanged != null ) TextChanged( sender, args );
		}
		
		private void on_entry_key_pressed ( object sender, Gtk.KeyPressEventArgs args )
		{
			if( args.Event.Key != Gdk.Key.Tab && !IsEditable ) ShowPopup();
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
		
		public bool IsEditable
		{
			get
			{
				return comboBox.Entry.IsEditable;
			}
			set
			{
				comboBox.Entry.IsEditable = value;
			}
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
