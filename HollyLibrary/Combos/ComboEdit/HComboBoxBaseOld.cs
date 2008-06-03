// HComboBox.cs created with MonoDevelop
// User: dantes at 3:40 PM 5/10/2008
//

//features intended to have:
// - simple to use as the winforms combobox control
// - look exactly the same as the gtk.combobox
// - autocompletion

using System;
using System.Reflection;
using Gtk;

namespace HollyLibrary
{
	
	public class HComboBoxBase : Gtk.HBox
	{
		private Entry entry              = new Entry();
		private Button popupButton       = new Button();	
		
		public HComboBoxBase() 
		{	
			this.AppPaintable   = true;
			Entry.HasFrame      = false;
			popupButton.WidthRequest = 30;

			this.PackStart ( new Label(" "), false, false, 0 );
			
			popupButton.Add( new Arrow( ArrowType.Down, ShadowType.None ) );

			HBox hb_entry        = new HBox();
			hb_entry.BorderWidth = 3;
			hb_entry.PackStart( Entry, true, true, 0 );
			this.PackStart( hb_entry, true, true, 0 );
			
			this.PackStart( popupButton, false, false, 0 );
			this.entry.FocusInEvent  += new FocusInEventHandler ( this.on_entry_focus_in   );
			this.entry.FocusOutEvent += new FocusOutEventHandler( this.on_entry_focus_out  );
		}
		
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Gdk.Rectangle rect = new Gdk.Rectangle( Allocation.X, Allocation.Y, Allocation.Width , Allocation.Height );
			
			Gtk.Style.PaintFlatBox( Entry.Style, this.GdkWindow, Entry.State, Entry.ShadowType, this.Allocation, Entry, "entry_bg", rect.X, rect.Y, rect.Width, rect.Height );
			Gtk.Style.PaintShadow ( Entry.Style, this.GdkWindow, Entry.State, Entry.ShadowType, this.Allocation, Entry, "entry"   , rect.X, rect.Y, rect.Width, rect.Height );
			
			return base.OnExposeEvent (evnt);
		}
		
		private void on_entry_focus_out( object sender, Gtk.FocusOutEventArgs args )
		{
			QueueDraw();
		}
		
		private void on_entry_focus_in( object sender, Gtk.FocusInEventArgs args )
		{
			QueueDraw();
		}
		
		public Entry Entry 
		{
			get 
			{
				return entry;
			}
		}

		public Button PopupButton 
		{
			get 
			{
				return popupButton;
			}
		}
	}
}
