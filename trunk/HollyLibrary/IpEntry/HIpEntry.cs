// HIpEntry.cs created with MonoDevelop
// User: dantes at 1:53 PM 5/24/2008
//

using System;
using System.Text.RegularExpressions;

namespace HollyLibrary
{
	
	
	public partial class HIpEntry : Gtk.Bin
	{
		private enum ImageType
		{
			Error, Ok
		}
		
		private string ValidationString = "\\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\b";
		string errorMessage    = "Invalid Ip Address!";
		Gtk.Entry CurrentEntry = null;
		Gtk.Entry[] entries;
		
		public HIpEntry()
		{
			this.Build();
			entries = new Gtk.Entry[] 
			{
				entry1, entry2, entry3, entry4
			};
			foreach( Gtk.Entry e in entries )
			{
				e.FocusInEvent  += delegate(object sender, Gtk.FocusInEventArgs args )
				{
					CurrentEntry = (Gtk.Entry) sender;
					this.QueueDraw(); 
				};
				e.FocusOutEvent += delegate(object sender, Gtk.FocusOutEventArgs args )
				{ 
					CurrentEntry = (Gtk.Entry) sender;
					this.QueueDraw(); 
				};
				e.KeyReleaseEvent += OnEntryKeyRelease;
			}
		}
		
		private void OnEntryKeyRelease( object sender, Gtk.KeyReleaseEventArgs args )
		{
			Gtk.Entry entry = (Gtk.Entry) sender;
			if( char.IsDigit( (char)args.Event.KeyValue ) )
			{
				if( entry.Position == 3 )
					MoveToNextEntry();
			}
			//validate text
			Validate();
		}
		
		private void MoveToNextEntry()
		{
			int current_entry_index = 0;
			foreach( Gtk.Entry e in entries )
			{
				if( CurrentEntry == e )
					break;
				else
					current_entry_index++;
			}
			int next_index  = current_entry_index + 1;
			if( next_index == entries.Length ) next_index = 0;
			entries[ next_index ].GrabFocus();
		}
				
		
		private void Validate()
		{
			if ( Regex.IsMatch( Text, ValidationString ) )
				SetImage( ImageType.Ok );
			else
				SetImage( ImageType.Error );
		}
		
		private void SetImage( ImageType type )
		{
			if( type == ImageType.Error )
			{
				ErrorImage.Show();
				HToolTip.SetToolTip( EbIcon, "Error", ErrorMessage, "gtk-dialog-warning" );
				ErrorImage.Stock         = "gtk-dialog-warning";
			}
			else
			{
				ErrorImage.Hide();
			}
		}
		
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Gdk.Rectangle rect = new Gdk.Rectangle( Allocation.X, Allocation.Y, Allocation.Width , Allocation.Height );
			if( CurrentEntry == null ) CurrentEntry = entries[0];
			Gtk.Entry TextBox  = CurrentEntry;
			Gtk.Style.PaintFlatBox( TextBox.Style, this.GdkWindow, TextBox.State, TextBox.ShadowType, this.Allocation, TextBox, "entry_bg", rect.X, rect.Y, rect.Width, rect.Height );
			Gtk.Style.PaintShadow ( TextBox.Style, this.GdkWindow, TextBox.State, TextBox.ShadowType, this.Allocation, TextBox, "entry"   , rect.X, rect.Y, rect.Width, rect.Height );
			
			return base.OnExposeEvent (evnt);
		}
		
		public String Text
		{
			get
			{
				return entry1.Text + "." + entry2.Text + "." + entry3.Text + "." + entry4.Text;
			}
			set
			{
				String[] parts = value.Split('.');
				if( parts.Length == 4 )
				{
					for( int i = 0; i<4; i++)
					{
						entries[i].Text = parts[i];
					}
				}
			}
		}

		public string ErrorMessage {
			get {
				return errorMessage;
			}
			set {
				errorMessage = value;
			}
		}
		
		
	}
}
