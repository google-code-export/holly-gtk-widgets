// HRegExEntry.cs created with MonoDevelop
// User: dantes at 1:20 PM 5/20/2008
//

using System;
using System.Text.RegularExpressions;

namespace HollyLibrary
{
	
	
	public partial class HRegExEntry : Gtk.Bin
	{
		private enum ImageType
		{
			Error, Ok
		}
		
		string regularExpression = "";
		string errorMessage      = "Error!";
		string okMessage         = "Ok!";
		
		public HRegExEntry()
		{
			this.Build();
			this.TextBox.FocusInEvent  += new Gtk.FocusInEventHandler ( delegate { this.QueueDraw(); } );
			this.TextBox.FocusOutEvent += new Gtk.FocusOutEventHandler( delegate { this.QueueDraw(); } );
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Gdk.Rectangle rect = new Gdk.Rectangle( Allocation.X, Allocation.Y, Allocation.Width , Allocation.Height );
			
			Gtk.Style.PaintFlatBox( TextBox.Style, this.GdkWindow, TextBox.State, TextBox.ShadowType, this.Allocation, TextBox, "entry_bg", rect.X, rect.Y, rect.Width, rect.Height );
			Gtk.Style.PaintShadow ( TextBox.Style, this.GdkWindow, TextBox.State, TextBox.ShadowType, this.Allocation, TextBox, "entry"   , rect.X, rect.Y, rect.Width, rect.Height );
			
			return base.OnExposeEvent (evnt);
		}
		
		private void SetImage( ImageType type )
		{
			if( type == ImageType.Error )
			{
				HToolTip.SetToolTip( EbErrorImage, "Error", errorMessage, "gtk-dialog-warning" );
				ErrorImage.Stock         = "gtk-dialog-warning";
			}
			else
			{
				HToolTip.SetToolTip( EbErrorImage, "Ok", okMessage, "gtk-yes" );
				ErrorImage.Stock         = "gtk-yes";
			}
		}

		protected virtual void OnTextBoxChanged (object sender, System.EventArgs e)
		{
			if( IsTextValid )
				SetImage( ImageType.Ok    );
			else
				SetImage( ImageType.Error );
		}

#region entry properties
		public Gtk.Entry Entry
		{
			get
			{
				return TextBox;
			}
		}
		
		public string Text
		{
			get
			{
				return TextBox.Text;
			}
			set
			{
				TextBox.Text = value;
			}
		}
#endregion

#region properties
		public bool IsTextValid
		{
			get
			{
				Regex rx;
				try
				{
					rx = new Regex(RegularExpression);
				}
				catch
				{
					return false;
				}
				
				return rx.IsMatch( TextBox.Text );
			}
		}
		
		public string RegularExpression 
		{
			get 
			{
				return regularExpression;
			}
			set 
			{
				regularExpression = value;
			}
		}

		public string OkMessage {
			get {
				return okMessage;
			}
			set {
				okMessage = value;
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
#endregion	

	}
}
