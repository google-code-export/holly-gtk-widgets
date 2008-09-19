// HImageCheckBox.cs created with MonoDevelop
// User: dantes at 11:57 AM 6/13/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;
using Gdk;

namespace HollyLibrary
{
	
	public partial class HImageCheckBox : Gtk.Bin
	{
		
		
		public event EventHandler CheckedStateChanged;
		Pixbuf checkedImage;
		Pixbuf uncheckedImage;
		bool _checked = false;
		bool focused  = false;
		
		public HImageCheckBox()
		{
			this.Build();
			this.AppPaintable      = true;
			on_init();
		}
		
		
		public HImageCheckBox( String text )
		{
			this.Build();
			this.Text = text;
			this.AppPaintable      = true;
			on_init();
		}
		
		private void on_init()
		{
			//add focus listener for image widget
			//add event masks
			ImgCheck.AddEvents( (int)Gdk.EventMask.AllEventsMask   );
			ImgCheck.AddEvents( (int)Gdk.EventMask.FocusChangeMask );
			ImgCheck.AddEvents( (int)Gdk.EventMask.KeyPressMask    );
			ImgCheck.WidgetFlags       |= WidgetFlags.CanFocus;
			
			//add events
			ImgCheck.FocusGrabbed      += new EventHandler           ( this.OnImgGrabFocus );
			ImgCheck.FocusOutEvent     += new FocusOutEventHandler   ( this.OnImgFocusOut );
			ECheckbox.ButtonPressEvent   += new ButtonPressEventHandler  ( OnButtonPress );
			ECheckbox.ButtonReleaseEvent += new ButtonReleaseEventHandler( OnButtonRelease );
			ImgCheck.KeyPressEvent     += new KeyPressEventHandler   ( OnImgKeyPress );
		}
		
		
		
		private void OnImgGrabFocus( object sender, EventArgs args )
		{
			LblText.TextInSelectionRectangle = true;
		}
		
		private void OnImgFocusOut( object sender, FocusOutEventArgs args )
		{
			LblText.TextInSelectionRectangle = false;
		}
		
		private void OnImgKeyPress( object sender, Gtk.KeyPressEventArgs e )
		{
			if( e.Event.Key.Equals( Gdk.Key.Return ) || e.Event.Key.Equals( Gdk.Key.space ) )
				Checked = !Checked;
		}

		private void OnButtonPress( object sender, EventArgs args )
		{
			Checked = !Checked;
			LblText.TextInSelectionRectangle = true;
		}
		
		private void OnButtonRelease( object sender, EventArgs args )
		{
			LblText.TextInSelectionRectangle = false;
		}
		
		private void OnCheckedStateChanged()
		{
			if( Checked )
				ImgCheck.Pixbuf = CheckedImage;
			else
				ImgCheck.Pixbuf = UncheckedImage;
			//
			if( CheckedStateChanged != null )
					CheckedStateChanged( this, new EventArgs() );
		}
		

#region properties
		public bool Checked 
		{
			get 
			{
				return _checked;
			}
			set 
			{
				_checked = value;
				OnCheckedStateChanged();
			}
		}

		public Pixbuf CheckedImage 
		{
			get 
			{
				return checkedImage;
			}
			set 
			{
				checkedImage    = value;
				OnCheckedStateChanged();
			}
		}

		public Pixbuf UncheckedImage 
		{
			get 
			{
				return uncheckedImage;
			}
			set 
			{
				uncheckedImage = value;
				OnCheckedStateChanged();
			}
		}
		
		public String Text
		{
			get
			{
				return LblText.Text;
			}
			set
			{
				LblText.Text = value;
			}
		}
		
		public HPosition TextPosition
		{
			get
			{
				return LblText.TextPosition;
			}
			set
			{
				LblText.TextPosition = value;
			}
		}
#endregion
		
	}
}
