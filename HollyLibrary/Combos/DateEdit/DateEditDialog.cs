// DateEditDialog.cs created with MonoDevelop
// User: dantes at 9:05 PM 4/19/2008
//

using System;
using System.Timers;
using Gdk;

namespace HollyLibrary
{
	
	public class DateEventArgs : EventArgs
	{
		private DateTime date;
		
		public DateTime Date 
		{
			get 
			{
				return date;
			}
		}
		
		public DateEventArgs( DateTime date )
		{
			this.date = date;
		}
	}
	
	
	public partial class DateEditDialog : Gtk.Window
	{
		public delegate void DateEventHandler(object sender, DateEventArgs args);

		public event DateEventHandler OnChange = null;
		
		public DateEditDialog( int x, int y, DateTime defDate, DateEventHandler handler ) : base(Gtk.WindowType.Popup)
		{	
			this.Move( x, y );
			this.Build();
			this.OnChange  = handler;
			GrabUtil.GrabWindow(this);

			TxtHour.Value  = defDate.Hour;
			TxtMin.Value   = defDate.Minute;
			TxtSec.Value   = defDate.Second;
			CCalendar.Date = defDate;
			
			RefreshClock();
		}
		
		public static void ShowMe( int x, int y, DateTime defDate, DateEventHandler handler )
		{
			new DateEditDialog( x, y, defDate, handler );
		}

		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			base.OnExposeEvent (args);
			
			int winWidth, winHeight;
			this.GetSize ( out winWidth, out winHeight );
			this.GdkWindow.DrawRectangle ( this.Style.ForegroundGC ( Gtk.StateType.Insensitive ), false, 0, 0, winWidth - 1, winHeight - 1 );
			
			return false;
		}

		protected virtual void OnButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			Close();
		}
		
		private void Close()
		{
			GrabUtil.RemoveGrab( this );
			this.Destroy();
		}

		protected virtual void OnCalendar3ButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			args.RetVal = true;
		}
		
		private void RefreshClock()
		{
			int hour              = (int)TxtHour.Value;
			int min               = (int)TxtMin.Value;
			int sec               = (int)TxtSec.Value;
			DateTime dt           = new DateTime( 2000,1,1, hour, min, sec );
			Clock.Datetime        = dt;
			if( OnChange != null ) OnChange( this, new DateEventArgs( CurrentDate ) );
		}

		protected virtual void OnTxtHourValueChanged (object sender, System.EventArgs e)
		{
			Clock.Stop();
			if( TxtHour.Value == 24 ) TxtHour.Value = 0;
			RefreshClock();
		}

		protected virtual void OnTxtMinValueChanged (object sender, System.EventArgs e)
		{
			Clock.Stop();
			if( TxtMin.Value == 60 ) TxtMin.Value = 0;
			RefreshClock();
		}

		protected virtual void OnTxtSecValueChanged (object sender, System.EventArgs e)
		{
			Clock.Stop();
			if( TxtSec.Value == 60 ) TxtSec.Value = 0;
			RefreshClock();
		}

		protected virtual void OnTxtHourButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			args.RetVal = true;
		}

		protected virtual void OnTxtMinButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			args.RetVal = true;
		}

		protected virtual void OnTxtSecButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			args.RetVal = true;
		}

		protected virtual void OnCalendar4ButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			args.RetVal = true;
		}


		public DateTime CurrentDate
		{
			get
			{
				DateTime d = CCalendar.Date;
				return new DateTime( d.Year, d.Month, d.Day, (int)TxtHour.Value, (int)TxtMin.Value, (int)TxtSec.Value );
			}
		}
		
		protected virtual void OnBtnClearClicked (object sender, System.EventArgs e)
		{
			CCalendar.Date = DateTime.Now;
			TxtHour.Value  = DateTime.Now.Hour;
			TxtMin.Value   = DateTime.Now.Minute;
			TxtSec.Value   = DateTime.Now.Second;
			RefreshClock();
			Clock.Start();
		}

		protected virtual void OnCCalendarDaySelected (object sender, System.EventArgs e)
		{
			if( OnChange != null ) OnChange( this, new DateEventArgs( CurrentDate ) );
		}

		protected virtual void OnCCalendarDaySelectedDoubleClick (object sender, System.EventArgs e)
		{
			if( OnChange != null ) OnChange( this, new DateEventArgs( CurrentDate ) );
			Close();
		}

		
		
	}
}
