// HColorPicker.cs created with MonoDevelop
// User: dantes at 1:30 PM 5/20/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HColorPicker : Gtk.Bin
	{
		//color change event
		public event ColorChangeEventHandler ColorChange;
		//color
		Gdk.Color color = new Gdk.Color( 0, 0, 0 );
		
		public HColorPicker()
		{
			this.Build();
			
			comboBox.PopupButton.Clicked += new EventHandler( OnPopupButtonClicked );
			comboBox.Entry.IsEditable     = false;
		}
		
		private void OnPopupButtonClicked( object sender, EventArgs args )
		{
			int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left + this.Allocation.Width;
			y += this.Allocation.Top  + this.Allocation.Height;
			ColorPickerDialog.ShowMe( x, y, this );
		}
	
		
		public Gdk.Color Color 
		{
			get 
			{
				return color;
			}
			set 
			{
				color = value;
				comboBox.Entry.ModifyBase( Gtk.StateType.Normal, color );
				comboBox.QueueDraw();
				if( ColorChange != null )  
					ColorChange( this, new ColorChangeEventArgs( color ) );
			}
		}
		
	}
}
