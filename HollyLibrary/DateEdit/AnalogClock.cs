// AnalogClock.cs created with MonoDevelop
// User: dantes at 6:50 PM 5/6/2008
//

using System;
using Gtk;
using Gdk;
using GLib;

namespace HollyLibrary
{
	
	public class AnalogClock : Gtk.DrawingArea
	{
		const float PI = 3.141592654F;
		float fRadius;
		float fCenterX, fCenterY;
		
		float fHourLength, fMinLength, fSecLength;
		double fHourTickness = 3, fMinTickness = 2, fSecTickness = 0.9;
		bool bDraw5MinuteTicks = true;
		bool bDraw1MinuteTicks = true;
		float fTicksThickness  = 1;
		
		Cairo.Color hrColor    = CairoUtil.ColorFromHexa("#3d3b33", 0.5);
		Cairo.Color minColor   = CairoUtil.ColorFromHexa("#3d3b33");
		Cairo.Color secColor   = CairoUtil.ColorFromHexa("#8f8a74");
		Cairo.Color ticksColor = CairoUtil.ColorFromHexa("#8f8a74");
			
		DateTime datetime;
		bool is_timer_started = false;
		
		
		
		public AnalogClock()
		{
			datetime       = DateTime.Now;
			QueueResize();
			GLib.Timeout.Add( 1000, this.on_timer_tick );
		}

		public void Start()
		{
			this.is_timer_started = true;
		}
		
		public void Stop()
		{
			this.is_timer_started = false;
		}
		
		public DateTime Datetime 
		{
			get
			{
				return datetime;
			}
			set
			{
				datetime = value;
				this.QueueDraw();
			}
		}
		
		private bool on_timer_tick()
		{
			if( is_timer_started )
			{
				this.datetime = DateTime.Now;
				this.QueueDraw();
			}
			return true;
		}

		private void DrawCenterFilledCircle( Cairo.PointD center, double radius, Cairo.Context e )
		{
			e.Arc( center.X, center.Y, radius, 0, 360 );
			Cairo.Gradient pat = new Cairo.LinearGradient ( 100, 200, 200, 100);
	        pat.AddColorStop (0, CairoUtil.ColorFromRgb( 240, 235, 229, 0.3) );
	        pat.AddColorStop (1, CairoUtil.ColorFromRgb( 0, 0, 0, 0.2));
			e.LineWidth = 0.1 ;
			e.Pattern = pat;
			e.FillPreserve();
			e.Stroke();
		}
		
		private void DrawCenterCircle( Cairo.PointD center, double radius, int width, Cairo.Color color, Cairo.Context e )
		{
			e.Arc( center.X, center.Y, radius, 0, 360 );
			e.LineWidth = width;
			e.Color     = color;
			e.Stroke();
		}
		
		private void DrawLine(double fThickness, float fLength, Cairo.Color color,
		                      float fRadians, Cairo.Context e)
		{
			Cairo.PointD p1, p2;
			p1 = new Cairo.PointD( fCenterX - (double)(fLength/9*System.Math.Sin(fRadians)), fCenterY + (double)(fLength/9*System.Math.Cos(fRadians)) );
			p2 = new Cairo.PointD( fCenterX + (double)(fLength*System.Math.Sin(fRadians) )  , fCenterY - (double)(fLength*System.Math.Cos(fRadians)) );
			e.MoveTo(p1);
			e.LineTo(p2);
			e.ClosePath();
			e.LineCap   = Cairo.LineCap.Round;
			e.LineJoin  = Cairo.LineJoin.Round;
			e.Color     = color;
			e.LineWidth = fThickness;
			e.Stroke();
		}
		
		protected override bool OnExposeEvent (EventExpose evnt)
		{
			base.OnExposeEvent (evnt);
			Cairo.Context e = CairoHelper.Create( evnt.Window);
			//draw background
			e.MoveTo(0,0);
			e.LineTo( this.Allocation.Width, 0 );
			e.LineTo( this.Allocation.Width, this.Allocation.Height );
			e.LineTo( 0, this.Allocation.Height );
			e.LineTo( 0, 0);
			e.ClosePath();
			e.Color = CairoUtil.ColorFromHexa("#FFFFFF");
			e.FillPreserve();
			e.Color = secColor;
			e.Stroke();
			//
			DateTime dateTime = this.datetime;
			float fRadHr      = (dateTime.Hour%12+dateTime.Minute/60F) *30*PI/180;
			float fRadMin     = (dateTime.Minute)*6*PI / 180;
			float fRadSec     = (dateTime.Second)*6*PI / 180;

			
			Cairo.PointD center = new Cairo.PointD( fCenterX, fCenterY );
			
			DrawCenterCircle( center, ( fRadius / 2 ) + 10, 6, CairoUtil.ColorFromHexa("#d5cab6", 0.8 ), e );
			DrawCenterCircle( center, ( fRadius / 2 ) + 15, 1, CairoUtil.ColorFromHexa("#8f857b"), e );
			DrawCenterCircle( center, ( fRadius / 2 ) + 16, 2, CairoUtil.ColorFromHexa("#d5cab6"), e );
			DrawCenterCircle( center, ( fRadius / 2 ) + 17, 1, CairoUtil.ColorFromHexa("#8f857b"), e );
			
			
			DrawLine( this.fHourTickness, this.fHourLength, hrColor , fRadHr , e );
			DrawLine( this.fMinTickness , this.fMinLength , minColor, fRadMin, e );
			DrawLine( this.fSecTickness , this.fSecLength , secColor, fRadSec, e );
			
			for( int i = 0; i < 60; i++ )
			{
				if ( this.bDraw5MinuteTicks==true && i%5==0 )
				// Draw 5 minute ticks
				{
					e.LineWidth = fTicksThickness;
					e.Color     = ticksColor;
					Cairo.PointD p1 = new Cairo.PointD( 
					                  fCenterX + (double)( this.fRadius/1.50F*System.Math.Sin(i*6*PI/180) ) , 
					                  fCenterY - (double)( this.fRadius/1.50F*System.Math.Cos(i*6*PI/180) ) );
					Cairo.PointD p2 = new Cairo.PointD( 
					                  fCenterX + (double)( this.fRadius/1.65F*System.Math.Sin(i*6*PI/180) ),
					                  fCenterY - (double)( this.fRadius/1.65F*System.Math.Cos(i*6*PI/180) ) );
					
					e.MoveTo(p1);
					e.LineTo(p2);
					e.ClosePath();
					e.Stroke();
				}
				else if ( this.bDraw1MinuteTicks==true ) // draw 1 minute ticks
				{
					
					e.LineWidth = fTicksThickness;
					e.Color     = ticksColor;
					Cairo.PointD p1 = new Cairo.PointD
					(
					    fCenterX + (double) ( this.fRadius/1.50F*System.Math.Sin(i*6*PI/180) ),
					    fCenterY - (double) ( this.fRadius/1.50F*System.Math.Cos(i*6*PI/180) )
					);
					Cairo.PointD p2 = new Cairo.PointD
					(
						 fCenterX + (double) ( this.fRadius/1.55F*System.Math.Sin(i*6*PI/180) ),
						 fCenterY - (double) ( this.fRadius/1.55F*System.Math.Cos(i*6*PI/180) ) 		 
					);
					e.MoveTo( p1 );
					e.LineTo( p2 );
					e.ClosePath();
					e.Stroke();
					
				}
			}
			DrawCenterFilledCircle( center, ( fRadius / 2 ) + 17 , e );
			DrawCenterFilledCircle( center, 8 , e );
			
			((IDisposable) e.Target).Dispose ();                                      
			((IDisposable) e).Dispose ();

			//
			return true;			
		}


		protected override void OnSizeAllocated (Rectangle allocation)
		{
			base.OnSizeAllocated (allocation);
			fRadius                  = allocation.Height / 2;
			this.fCenterX            = allocation.Width  / 2;
			this.fCenterY            = allocation.Height / 2;
			this.fHourLength         = allocation.Height / 3 / 1.65F;
			this.fMinLength          = allocation.Height / 3 / 1.20F;
			this.fSecLength          = allocation.Height / 3 / 1.15F;
		}

		
	}
}
