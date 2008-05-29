// HDateEdit.cs created with MonoDevelop
// User: dantes at 2:33 PM 4/14/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Globalization;
using Gtk;

namespace HollyLibrary
{
	
	public partial class HDateEdit : Gtk.Bin
	{
		//events
		public event EventHandler DateChanged;
		//properties
		DateTimeFormatTypeEnum dateTimeFormatType = DateTimeFormatTypeEnum.FullDateTime;
		private String customFormat               = "";
		DateTime currentDate                      = DateTime.Now;
		Gdk.Color errorColor                      = new Gdk.Color( 255, 0, 0 );
		Gdk.Color NormalColor                     = new Gdk.Color( 255, 255, 255 );
		
		public HDateEdit()
		{
			this.Build();

			CurrentDate = DateTime.Now;
			NormalColor = comboBox.Entry.Style.Text( Gtk.StateType.Normal );
			comboBox.Entry.Changed += new EventHandler( OnTxtDateChanged );
			comboBox.PopupButton.Clicked += new EventHandler( OnBtnShowCalendarClicked );
		}
		
		protected virtual void OnBtnShowCalendarClicked (object sender, System.EventArgs e)
		{
			int x, y;
			this.ParentWindow.GetPosition( out x, out y );	
			x += this.Allocation.Left;
			y += this.Allocation.Top + this.Allocation.Height;
			DateEditDialog.ShowMe( x, y, currentDate, OnPopupDateChanged );
		}
		
		private void OnPopupDateChanged( object sender, DateEventArgs args)
		{
			CurrentDate = args.Date;
		}

		public bool IsDateValid()
		{
			bool is_date_correct = true;
			try
			{
				IFormatProvider culture = CultureInfo.CurrentCulture;
				if( DateTimeFormatType == DateTimeFormatTypeEnum.Custom )
					CurrentDate = DateTime.ParseExact( comboBox.Entry.Text, CustomFormat , culture );
				else
					CurrentDate = DateTime.Parse( comboBox.Entry.Text, culture );
			}
			catch
			{
				is_date_correct = false;
			}
			return is_date_correct;
		}
		
		protected virtual void OnTxtDateChanged (object sender, System.EventArgs e)
		{
			if( this.IsDateValid() )
			{
				comboBox.Entry.ModifyText( Gtk.StateType.Normal, NormalColor );
			}
			else
			{
				comboBox.Entry.ModifyText( Gtk.StateType.Normal, ErrorColor );
			}
			if( DateChanged != null ) DateChanged ( this, e );
		}
	
		public String Text
		{
			get
			{
				return comboBox.Entry.Text;
			}
		}
		
		public DateTime CurrentDate
		{
			get
			{
				return currentDate;
			}
			set
			{
				currentDate  = value;
				if( DateTimeFormatType == DateTimeFormatTypeEnum.FullDateTime )
				{
					comboBox.Entry.Text = value.ToString();
				}
				else if( DateTimeFormatType == DateTimeFormatTypeEnum.Custom )
				{
					comboBox.Entry.Text = value.ToString( CustomFormat );
				}
				else if( DateTimeFormatType == DateTimeFormatTypeEnum.ShortDate )
				{
					comboBox.Entry.Text = value.ToShortDateString();
				}
				else if( DateTimeFormatType == DateTimeFormatTypeEnum.ShortTime )
				{
					comboBox.Entry.Text = value.ToShortTimeString();
				}
				else if( DateTimeFormatType == DateTimeFormatTypeEnum.LongTime )
				{
					comboBox.Entry.Text = value.ToLongTimeString();
				}
				else if( DateTimeFormatType == DateTimeFormatTypeEnum.LongDate )
				{
					comboBox.Entry.Text = value.ToLongDateString();
				}
			}
		}

		public DateTimeFormatTypeEnum DateTimeFormatType 
		{
			get
			{
				return dateTimeFormatType;
			}
			set
			{
				dateTimeFormatType = value;
			}
		}

		public string CustomFormat 
		{
			get 
			{
				return customFormat;
			}
			set
			{
				customFormat = value;
			}
		}

		public Gdk.Color ErrorColor 
		{
			get 
			{
				return errorColor;
			}
			set 
			{
				errorColor = value;
			}
		}
		
	}
}

