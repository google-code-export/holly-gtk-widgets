// HFontPicker.cs created with MonoDevelop
// User: dantes at 8:47 PM 5/12/2008
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace HollyLibrary
{
	public delegate void FontEventHandler(object sender, FontEventArgs args);
	
	public partial class HFontPicker : Gtk.Bin
	{
			
		public event FontEventHandler FontChange;
		Dictionary<String, Font> FontStore = new Dictionary<String,Font>();
		
		public HFontPicker()
		{
			this.Build();
			comboBox.Entry.IsEditable = false;
			//creeaza un hashmap cu lista de fonturi
			FontFamily[] fonts = FontFamily.Families;
			int i = 0;
			foreach( System.Drawing.FontFamily font in fonts )
			{ 
				FontStore[ font.Name ] =  new Font( font, 10F );
				if( i == 0 )
				{
					i++;
					comboBox.Entry.Text = font.Name;
					Pango.FontDescription fontdescr = Pango.FontDescription.FromString( font.Name );
					comboBox.Entry.ModifyFont( fontdescr );
				}
			}
			//
			comboBox.PopupButton.Clicked += new EventHandler( this.on_btn_clicked );
			comboBox.Entry.KeyPressEvent += new Gtk.KeyPressEventHandler( this.on_text_changed );
		}
		
		private void on_text_changed( object Sender, Gtk.KeyPressEventArgs args )
		{
			if( args.Event.Key != Gdk.Key.Tab ) ShowPopup();
		}
		
		private void on_btn_clicked( object Sender, EventArgs args )
		{
			ShowPopup();			
		}
		
		public void FocusOnEntry()
		{
			comboBox.Entry.GrabFocus();
		}
		
		public void ShowPopup()
		{
			int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			
			FontChooserDialog.ShowMe(x,y, Allocation.Width, on_font_change, FontStore, comboBox.Entry.Text, this );
		}
		
		private void on_font_change( object Sender, FontEventArgs args )
		{
			comboBox.Entry.Text = args.Font;
			comboBox.Entry.ModifyFont( Pango.FontDescription.FromString( args.Font ) );
			if( FontChange != null ) FontChange( this, args );
		}
		
		public String Font
		{
			get
			{
				return comboBox.Entry.Style.FontDesc.Family;
			}
			set
			{
				try
				{
					Pango.FontDescription fd = Pango.FontDescription.FromString( value );
					comboBox.Entry.Text      = value;
					comboBox.Entry.ModifyFont( fd );
				}
				catch
				{
					Console.WriteLine("bad font");
				}
			}
		}
		
	}
	
}
