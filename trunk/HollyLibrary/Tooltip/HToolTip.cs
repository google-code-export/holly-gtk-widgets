// HToolTip.cs created with MonoDevelop
// User: dantes at 2:54 PM 5/22/2008
//

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace HollyLibrary
{
	
	class ToolTipData
	{
		String title, text;
		Color color1 = Color.FromArgb( 251, 254, 255 );
		Color color2 = Color.FromArgb( 229, 229, 239 );
		String stockIcon = "gtk-dialog-info";
		
#region properties
		public string Title {
			get {
				return title;
			}
			set {
				title = value;
			}
		}

		public string Text {
			get {
				return text;
			}
			set {
				text = value;
			}
		}

		public Color Color1 {
			get {
				return color1;
			}
			set {
				color1 = value;
			}
		}

		public Color Color2 {
			get {
				return color2;
			}
			set {
				color2 = value;
			}
		}

		public string StockIcon {
			get {
				return stockIcon;
			}
			set {
				stockIcon = value;
			}
		}

		
#endregion
		
#region constructors
		public ToolTipData( String title, String text, Color color1, Color color2, String stock )
		{
			this.Title  = title;
			this.Text   = text;
			this.Color1 = color1;
			this.Color2 = color2;
			this.StockIcon = stock;
		}
		
		public ToolTipData( String title, String text, Color color1, Color color2 )
		{
			this.Title  = title;
			this.Text   = text;
			this.Color1 = color1;
			this.Color2 = color2;
		}
		
		public ToolTipData( String title, String text)
		{
			this.Title  = title;
			this.Text   = text;
		}
		
		public ToolTipData( String title, String text, String stock )
		{
			this.Title  = title;
			this.Text   = text;
			this.StockIcon = stock;
		}
#endregion
		
	}
	
	public partial class HToolTip : Gtk.Window
	{
		Color color1     = Color.FromArgb( 251, 254, 255 );
		Color color2     = Color.FromArgb( 229, 229, 239 );
		//
		private static int toolTipInterval      = 1500;
		private static bool is_interval_started = false;
		private static Gtk.Widget CurrentWidget = null;
		//
		private int tail_height      = 30;
		private int tail_left        = 30;
		private int tail_width       = 60;
		private int round_rect_angle = 15;
		private static HToolTip instance;
		private static Dictionary<Gtk.Widget,ToolTipData> widgets = new Dictionary<Gtk.Widget,ToolTipData>();
			
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
			instance.Visible = false;
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
		
#region properties
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

		public static int ToolTipInterval 
		{
			get 
			{
				return toolTipInterval;
			}
			set
			{
				toolTipInterval = value;
			}
		}

		public Color Color1 {
			get {
				return color1;
			}
			set {
				color1 = value;
			}
		}

		public Color Color2 {
			get {
				return color2;
			}
			set {
				color2 = value;
			}
		}

		public string StockIcon {
			get {
				return ImgIcon.Stock;
			}
			set {
				ImgIcon.Stock = value;
			}
		}
#endregion

		public static void AddToolTip( Gtk.Widget widget, String title, String text, String StockIcon )
		{
			ToolTipData data = new ToolTipData( title, text, StockIcon );
			AddWidget( widget, data );
		}
		
		public static void AddToolTip( Gtk.Widget widget, String title, String text, Color color1, Color color2)
		{
			ToolTipData data = new ToolTipData( title, text, color1, color2  );
			AddWidget( widget, data );
		}
		
		public static void AddToolTip( Gtk.Widget widget, String title, String text )
		{
			ToolTipData data = new ToolTipData( title, text );
			AddWidget( widget, data );
		}
		
		public static void AddToolTip( Gtk.Widget widget, String title, String text, Color color1, Color color2, String StockIcon )
		{
			ToolTipData data = new ToolTipData( title, text, color1, color2, StockIcon );
			AddWidget( widget, data );
		}
		
		private static void AddWidget( Gtk.Widget widget, ToolTipData data )
		{
			if( !widgets.ContainsKey( widget ) )
			{
				widgets.Add( widget, data );
				widget.DeleteEvent  += on_widget_delete;
				widget.AddEvents( (int)Gdk.EventMask.PointerMotionMask );
				widget.AddEvents( (int)Gdk.EventMask.PointerMotionHintMask );
				widget.AddEvents( (int)Gdk.EventMask.LeaveNotifyMask );
				widget.MotionNotifyEvent += on_widget_motion;
				widget.LeaveNotifyEvent  += on_widget_out;
			}
			else
			{
				widgets[widget] = data;
			}
		}
		
		private static void on_widget_out( object sender, Gtk.LeaveNotifyEventArgs args )
		{
			is_interval_started = false;
			Instance.Close();
		}
		
		private static void on_widget_motion( object sender, Gtk.MotionNotifyEventArgs args )
		{
			CurrentWidget = (Gtk.Widget) sender;
			is_interval_started = true;
			GLib.Timeout.Add( (uint)ToolTipInterval, new GLib.TimeoutHandler( ShowMe ) );
		}
		
		private static bool ShowMe()
		{
			if( !Instance.Visible  && is_interval_started )
			{
				Gtk.Widget w         = CurrentWidget;
				ToolTipData data     = widgets[ w ];
				int x, y;
				Gdk.ModifierType mt;
				w.GdkWindow.GetPointer( out x, out y, out mt );
				int _x, _y;
				w.GdkWindow.GetPosition( out _x, out _y );
				x += _x;
				y += _y;
				Instance.Move( x , y );
				Instance.ToolTipTitle = data.Title;
				Instance.ToolTipText  = data.Text ;
				Instance.Color1       = data.Color1;
				Instance.Color2       = data.Color2;
				Instance.StockIcon    = data.StockIcon;
				Instance.Resize( 150, 100 );
				Instance.Show();
			}
			return false;
		}
				
		private static void on_widget_delete( object sender, EventArgs args )
		{
			widgets.Remove( (Gtk.Widget) sender );
		}
		
		protected virtual void OnSizeAllocated (object o, Gtk.SizeAllocatedArgs args)
		{
			makeShape();
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
			Color c1       = this.Color1;
			Color c2       = this.Color2;
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

		
	

	
	}
}
