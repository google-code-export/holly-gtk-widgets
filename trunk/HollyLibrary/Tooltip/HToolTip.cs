// HToolTip.cs created with MonoDevelop
// User: dantes at 2:54 PM 5/22/2008
//

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace HollyLibrary
{
	
	
	public partial class HToolTip : Gtk.Window
	{
		private int tail_height      = 30;
		private int tail_left        = 30;
		private int tail_width       = 60;
		private int round_rect_angle = 15;
		private static HToolTip instance;
		private static Dictionary<Gtk.Widget,String[]> widgets = new Dictionary<Gtk.Widget,String[]>();
			
		public HToolTip() : 
				base(Gtk.WindowType.Popup)
		{
			this.AppPaintable       = true;
			this.TypeHint           = Gdk.WindowTypeHint.Tooltip;
			this.Build();
			this.Hide();
			instance                = this;
			Spacer.HeightRequest    = tail_height;
			SpacerLeft.WidthRequest = round_rect_angle;
			SpacerRight.WidthRequest= round_rect_angle;
		}
		
		public void Close()
		{
			instance.Hide();
		}
		
		
		public static HToolTip Instance
		{
			get
			{
				if ( instance == null ) new HToolTip();
				return instance;
			}
		}
		
		public String ToolTipText
		{
			get
			{
				return LblContent.Text;
			}
			set
			{
				LblContent.Text = value;
			}
		}
		
		public String ToolTipTitle
		{
			get
			{
				return LblTitle.Text;
			}
			set
			{
				LblTitle.Markup = "<b>"+value+"</b>";
			}
		}
		
		public static void AddToolTip( Gtk.Widget widget, String title, String text )
		{
			widgets.Add( widget, new String[] { title, text } );
			widget.DeleteEvent  += on_widget_delete;
			widget.AddEvents( (int)Gdk.EventMask.PointerMotionMask );
			widget.AddEvents( (int)Gdk.EventMask.PointerMotionHintMask );
			widget.AddEvents( (int)Gdk.EventMask.LeaveNotifyMask );
			widget.MotionNotifyEvent += on_widget_motion;
			widget.LeaveNotifyEvent  += on_widget_out;
		}
		
		private static void on_widget_out( object sender, Gtk.LeaveNotifyEventArgs args )
		{
			Instance.Close();
		}
		
		private static void on_widget_motion( object sender, Gtk.MotionNotifyEventArgs args )
		{
			if( !Instance.Visible  )
			{
				Gtk.Widget w         = (Gtk.Widget) sender ;
				String[] ToolTipData = widgets[ w ];
				int x, y;
				Gdk.ModifierType mt;
				w.GdkWindow.GetPointer( out x, out y, out mt );
				int _x, _y;
				w.GdkWindow.GetPosition( out _x, out _y );
				x += _x;
				y += _y;
				Instance.Move( x , y );
				Instance.ToolTipTitle = ToolTipData[0];
				instance.ToolTipText  = ToolTipData[1] ;
				Instance.Resize( 150, 100 );
				Instance.Show();
			}
		}
				
		private static void on_widget_delete( object sender, EventArgs args )
		{
			widgets.Remove( (Gtk.Widget) sender );
		}
		
	
#region make shape method
		private void makeShape()
		{
			Bitmap bmp   = new Bitmap( Allocation.Width, Allocation.Height);
			Graphics g   = Graphics.FromImage( bmp );
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;
			
			GraphUtil g2 = new HollyLibrary.GraphUtil( g );
			Brush b      = new SolidBrush( Color.Red );
			//
			g2.FillRoundRectangle( b, 0, tail_height, bmp.Width, bmp.Height - tail_height, round_rect_angle );
			Point[] points  = new Point[] 
			{ 
				new Point( tail_left  , tail_height ),
				new Point( tail_left  , 0           ),
				new Point( tail_width , tail_height )
			};
			g.FillPolygon( b, points );
			g.Dispose();
			WinUtil.ModifyWindowShape( bmp, this );
		}
#endregion
		
#region expose event
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Graphics g     = Gtk.DotNet.Graphics.FromDrawable( evnt.Window );
			//fill background
			Color c1       = Color.FromArgb( 251, 254, 255 );
			Color c2       = Color.FromArgb( 229, 229, 239 );
			Rectangle rect = new Rectangle( Allocation.X, Allocation.Y, Allocation.Width, Allocation.Height );
			Brush b        = new LinearGradientBrush(rect, c1, c2, 90, true );
			g.FillRectangle( b, rect );
			
			//draw border
			SolidBrush bb   = new SolidBrush( Color.FromArgb( 147, 148, 166 ) );
			GraphUtil g2    = new GraphUtil( g );
			Pen border_pen  = new Pen(bb, 1F );
			g2.DrawRoundRectangle( border_pen,0, tail_height, Allocation.Width - 2, Allocation.Height - tail_height - 1, round_rect_angle );
			
			//draw tail background again to hide the bottom line
			
			Point[] points  = new Point[] 
			{ 
				new Point( tail_left  , tail_height + 1 ),
				new Point( tail_left  , 0           ),
				new Point( tail_width , tail_height + 1 )
			};
			g.FillPolygon( b, points );
			
			//draw tail border
			points  = new Point[] 
			{ 
				new Point( tail_left - 1 , tail_height ),
				new Point( tail_left     , 1           ),
				new Point( tail_width -1 , tail_height )
			};
			g.DrawLine( border_pen, points[0], points[1] );
			g.DrawLine( border_pen, points[1], points[2] );
			return base.OnExposeEvent (evnt);
		}

		

		
#endregion

		protected virtual void OnSizeAllocated (object o, Gtk.SizeAllocatedArgs args)
		{
			makeShape();
		}
	

	
	}
}
