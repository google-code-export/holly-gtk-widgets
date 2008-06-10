// ColorPanel.cs created with MonoDevelop
// User: dantes at 8:40 PM 6/9/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HollyLibrary
{
	public enum eDrawStyle
	{
		Hue,
		Saturation,
		Brightness,
		Red,
		Green,
		Blue
	}
	
	public class ColorPanel : Gtk.DrawingArea
	{
		#region Class Variables

		

		private int		m_iMarker_X = 0;
		private int		m_iMarker_Y = 0;
		private bool	m_bDragging = false;

		//	These variables keep track of how to fill in the content inside the box;
		private eDrawStyle		m_eDrawStyle = eDrawStyle.Hue;
		private GraphUtil.HSL	m_hsl;
		private Color			m_rgb;

		#endregion

		#region Constructors / Destructors

		public ColorPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//	Initialize Colors
			m_hsl   = new GraphUtil.HSL();
			m_hsl.H = 1.0;
			m_hsl.S = 1.0;
			m_hsl.L = 1.0;
			m_rgb   = GraphUtil.HSL_to_RGB(m_hsl);
			m_eDrawStyle = eDrawStyle.Hue;
		}




		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ctrl2DColorBox
			// 
			this.Name = "ctrl2DColorBox";
			this.SetSizeRequest(260, 260);
			this.AddEvents( (int) Gdk.EventMask.AllEventsMask );
		
		}
		#endregion

		#region Control Events
		

		protected override bool OnButtonPressEvent (Gdk.EventButton e)
		{
			if ( e.Button != 1 )	//	Only respond to left mouse button events
				return false;

			m_bDragging = true;		//	Begin dragging which notifies MouseMove function that it needs to update the marker

			int x = (int)e.X - 2, y = (int)e.Y - 2;
			if ( x < 0 ) x = 0;
			if ( x > this.Allocation.Width - 4 ) x = this.Allocation.Width - 4;	//	Calculate marker position
			if ( y < 0 ) y = 0;
			if ( y > this.Allocation.Height - 4 ) y = this.Allocation.Height - 4;

			if ( x == m_iMarker_X && y == m_iMarker_Y )		//	If the marker hasn't moved, no need to redraw it.
				return false;										//	or send a scroll notification

			m_iMarker_X = x;
			m_iMarker_Y = y;
			this.QueueDraw();       //	Redraw the marker
			ResetHSLRGB();			//	Reset the color

			if ( Scroll != null )	//	Notify anyone who cares that the controls marker (selected color) has changed
				Scroll(this, new EventArgs());
			return base.OnButtonPressEvent (e);
		}




		protected override bool OnMotionNotifyEvent (Gdk.EventMotion e)
		{
			if ( !m_bDragging )		//	Only respond when the mouse is dragging the marker.
				return false;

			int x = (int)e.X - 2, y = (int)e.Y - 2;
			if ( x < 0 ) x = 0;
			if ( x > this.Allocation.Width - 4 ) x = this.Allocation.Width - 4;	//	Calculate marker position
			if ( y < 0 ) y = 0;
			if ( y > this.Allocation.Height - 4 ) y = this.Allocation.Height - 4;

			if ( x == m_iMarker_X && y == m_iMarker_Y )		//	If the marker hasn't moved, no need to redraw it.
				return false;										//	or send a scroll notification

			m_iMarker_X = x;
			m_iMarker_Y = y;
			this.QueueDraw();       //	Redraw the marker
			ResetHSLRGB();			//	Reset the color

			if ( Scroll != null )	//	Notify anyone who cares that the controls marker (selected color) has changed
				Scroll(this, new EventArgs());
			return base.OnMotionNotifyEvent (e);
		}
	

		protected override bool OnButtonReleaseEvent (Gdk.EventButton e)
		{
			if ( e.Button != 1 )	//	Only respond to left mouse button events
				return false;

			if ( !m_bDragging )
				return false;

			m_bDragging = false;

			int x = (int)e.X - 2, y = (int)e.Y - 2;
			if ( x < 0 ) x = 0;
			if ( x > this.Allocation.Width - 4 ) x = this.Allocation.Width - 4;	//	Calculate marker position
			if ( y < 0 ) y = 0;
			if ( y > this.Allocation.Height - 4 ) y = this.Allocation.Height - 4;

			if ( x == m_iMarker_X && y == m_iMarker_Y )		//	If the marker hasn't moved, no need to redraw it.
				return false;										//	or send a scroll notification

			m_iMarker_X = x;
			m_iMarker_Y = y;
			this.QueueDraw();       //	Redraw the marker
			
			ResetHSLRGB();			//	Reset the color

			if ( Scroll != null )	//	Notify anyone who cares that the controls marker (selected color) has changed
				Scroll(this, new EventArgs());
			return base.OnButtonReleaseEvent (e);
		}


		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Redraw_Control( evnt.Window );
			return base.OnExposeEvent (evnt);
		}



		#endregion

		#region Events

		public event EventHandler Scroll;

		#endregion

		#region Public Methods

		/// <summary>
		/// The drawstyle of the contol (Hue, Saturation, Brightness, Red, Green or Blue)
		/// </summary>
		public eDrawStyle DrawStyle
		{
			get
			{
				return m_eDrawStyle;
			}
			set
			{
				m_eDrawStyle = value;

				//	Redraw the control based on the new eDrawStyle
				Reset_Marker(this.GdkWindow,true);
				this.QueueDraw();
			}
		}


		/// <summary>
		/// The HSL color of the control, changing the HSL will automatically change the RGB color for the control.
		/// </summary>
		public GraphUtil.HSL HSL
		{
			get
			{
				return m_hsl;
			}
			set
			{
				m_hsl = value;
				m_rgb = GraphUtil.HSL_to_RGB(m_hsl);

				//	Redraw the control based on the new color.
				Reset_Marker(this.GdkWindow, true);
				this.QueueDraw();
			}
		}


		/// <summary>
		/// The RGB color of the control, changing the RGB will automatically change the HSL color for the control.
		/// </summary>
		public Color RGB
		{
			get
			{
				return m_rgb;
			}
			set
			{
				m_rgb = value;
				m_hsl = GraphUtil.RGB_to_HSL(m_rgb);

				//	Redraw the control based on the new color.
				Reset_Marker(this.GdkWindow, true);
				this.QueueDraw();
			}
		}


		#endregion

		#region Private Methods

		/// <summary>
		/// Redraws only the content over the marker
		/// </summary>
		private void ClearMarker( Gdk.Window win )
		{
			//	Determine the area that needs to be redrawn
			Graphics g = Gtk.DotNet.Graphics.FromDrawable( win );
			int start_x, start_y, end_x, end_y;
			int red = 0; int green = 0; int blue = 0;
			GraphUtil.HSL hsl_start = new GraphUtil.HSL();
			GraphUtil.HSL hsl_end   = new GraphUtil.HSL();

			//	Find the markers corners
			start_x = m_iMarker_X - 5;
			start_y = m_iMarker_Y - 5;
			end_x = m_iMarker_X + 5;
			end_y = m_iMarker_Y + 5;
			//	Adjust the area if part of it hangs outside the content area
			if ( start_x < 0 ) start_x = 0;
			if ( start_y < 0 ) start_y = 0;
			if ( end_x > this.Allocation.Width - 4 ) end_x = this.Allocation.Width - 4;
			if ( end_y > this.Allocation.Height - 4 ) end_y = this.Allocation.Height - 4;

			//	Redraw the content based on the current draw style:
			//	The code get's a little messy from here
			switch (m_eDrawStyle)
			{
					//		  S=0,S=1,S=2,S=3.....S=100
					//	L=100
					//	L=99
					//	L=98		Drawstyle
					//	L=97		   Hue
					//	...
					//	L=0
				case eDrawStyle.Hue :	

					hsl_start.H = m_hsl.H;	hsl_end.H = m_hsl.H;	//	Hue is constant
					hsl_start.S = (double)start_x/(this.Allocation.Width - 4);	//	Because we're drawing horizontal lines, s will not change
					hsl_end.S = (double)end_x/(this.Allocation.Width - 4);		//	from line to line

					for ( int i = start_y; i <= end_y; i++ )		//	For each horizontal line:
					{
						hsl_start.L = 1.0 - (double)i/(this.Allocation.Height - 4);	//	Brightness (L) WILL change for each horizontal
						hsl_end.L = hsl_start.L;							//	line drawn
				
						LinearGradientBrush br = new LinearGradientBrush(new Rectangle(start_x + 1,i + 2, end_x - start_x + 1, 1), GraphUtil.HSL_to_RGB(hsl_start), GraphUtil.HSL_to_RGB(hsl_end), 0, false); 
						g.FillRectangle(br,new Rectangle(start_x + 2,i + 2, end_x - start_x + 1 , 1)); 
					}
					
					break;
					//		  H=0,H=1,H=2,H=3.....H=360
					//	L=100
					//	L=99
					//	L=98		Drawstyle
					//	L=97		Saturation
					//	...
					//	L=0
				case eDrawStyle.Saturation :

					hsl_start.S = m_hsl.S;	hsl_end.S = m_hsl.S;			//	Saturation is constant
					hsl_start.L = 1.0 - (double)start_y/(this.Allocation.Height - 4);	//	Because we're drawing vertical lines, L will 
					hsl_end.L = 1.0 - (double)end_y/(this.Allocation.Height - 4);		//	not change from line to line

					for ( int i = start_x; i <= end_x; i++ )				//	For each vertical line:
					{
						hsl_start.H = (double)i/(this.Allocation.Width - 4);			//	Hue (H) WILL change for each vertical
						hsl_end.H = hsl_start.H;							//	line drawn
				
						LinearGradientBrush br = new LinearGradientBrush(new Rectangle(i + 2,start_y + 1, 1, end_y - start_y + 2), GraphUtil.HSL_to_RGB(hsl_start), GraphUtil.HSL_to_RGB(hsl_end), 90, false); 
						g.FillRectangle(br,new Rectangle(i + 2, start_y + 2, 1, end_y - start_y + 1)); 
					}
					break;
					//		  H=0,H=1,H=2,H=3.....H=360
					//	S=100
					//	S=99
					//	S=98		Drawstyle
					//	S=97		Brightness
					//	...
					//	S=0
				case eDrawStyle.Brightness :
					
					hsl_start.L = m_hsl.L;	hsl_end.L = m_hsl.L;			//	Luminance is constant
					hsl_start.S = 1.0 - (double)start_y/(this.Allocation.Height - 4);	//	Because we're drawing vertical lines, S will 
					hsl_end.S = 1.0 - (double)end_y/(this.Allocation.Height - 4);		//	not change from line to line

					for ( int i = start_x; i <= end_x; i++ )				//	For each vertical line:
					{
						hsl_start.H = (double)i/(this.Allocation.Width - 4);			//	Hue (H) WILL change for each vertical
						hsl_end.H = hsl_start.H;							//	line drawn
				
						LinearGradientBrush br = new LinearGradientBrush(new Rectangle(i + 2,start_y + 1, 1, end_y - start_y + 2), GraphUtil.HSL_to_RGB(hsl_start), GraphUtil.HSL_to_RGB(hsl_end), 90, false); 
						g.FillRectangle(br,new Rectangle(i + 2, start_y + 2, 1, end_y - start_y + 1)); 
					}

					break;
					//		  B=0,B=1,B=2,B=3.....B=100
					//	G=100
					//	G=99
					//	G=98		Drawstyle
					//	G=97		   Red
					//	...
					//	G=0
				case eDrawStyle.Red :
					
					red = m_rgb.R;													//	Red is constant
					int start_b = Round(255 * (double)start_x/(this.Allocation.Width - 4));	//	Because we're drawing horizontal lines, B
					int end_b = Round(255 * (double)end_x/(this.Allocation.Width - 4));		//	will not change from line to line

					for ( int i = start_y; i <= end_y; i++ )						//	For each horizontal line:
					{
						green = Round(255 - (255 * (double)i/(this.Allocation.Height - 4)));	//	green WILL change for each horizontal line drawn

						LinearGradientBrush br = new LinearGradientBrush(new Rectangle(start_x + 1,i + 2, end_x - start_x + 1, 1), Color.FromArgb(red, green, start_b), Color.FromArgb(red, green, end_b), 0, false); 
						g.FillRectangle(br,new Rectangle(start_x + 2,i + 2, end_x - start_x + 1 , 1));  
					}

					break;
					//		  B=0,B=1,B=2,B=3.....B=100
					//	R=100
					//	R=99
					//	R=98		Drawstyle
					//	R=97		  Green
					//	...
					//	R=0
				case eDrawStyle.Green :
					
					green = m_rgb.G;;												//	Green is constant
					int start_b2 = Round(255 * (double)start_x/(this.Allocation.Width - 4));	//	Because we're drawing horizontal lines, B
					int end_b2 = Round(255 * (double)end_x/(this.Allocation.Width - 4));		//	will not change from line to line

					for ( int i = start_y; i <= end_y; i++ )						//	For each horizontal line:
					{
						red = Round(255 - (255 * (double)i/(this.Allocation.Height - 4)));		//	red WILL change for each horizontal line drawn

						LinearGradientBrush br = new LinearGradientBrush(new Rectangle(start_x + 1,i + 2, end_x - start_x + 1, 1), Color.FromArgb(red, green, start_b2), Color.FromArgb(red, green, end_b2), 0, false); 
						g.FillRectangle(br,new Rectangle(start_x + 2,i + 2, end_x - start_x + 1 , 1)); 
					}

					break;
					//		  R=0,R=1,R=2,R=3.....R=100
					//	G=100
					//	G=99
					//	G=98		Drawstyle
					//	G=97		   Blue
					//	...
					//	G=0
				case eDrawStyle.Blue :
					
					blue = m_rgb.B;;												//	Blue is constant
					int start_r = Round(255 * (double)start_x/(this.Allocation.Width - 4));	//	Because we're drawing horizontal lines, R
					int end_r = Round(255 * (double)end_x/(this.Allocation.Width - 4));		//	will not change from line to line

					for ( int i = start_y; i <= end_y; i++ )						//	For each horizontal line:
					{
						green = Round(255 - (255 * (double)i/(this.Allocation.Height - 4)));	//	green WILL change for each horizontal line drawn

						LinearGradientBrush br = new LinearGradientBrush(new Rectangle(start_x + 1,i + 2, end_x - start_x + 1, 1), Color.FromArgb(start_r, green, blue), Color.FromArgb(end_r, green, blue), 0, false); 
						g.FillRectangle(br,new Rectangle(start_x + 2,i + 2, end_x - start_x + 1 , 1)); 
					}

					break;
			}
			g.Dispose();
		}


		/// <summary>
		/// Draws the marker (circle) inside the box
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="Unconditional"></param>
		private void DrawMarker( Gdk.Window win, int x, int y, bool Unconditional)			//	   *****
		{																	//	  *  |  *
			if ( x < 0 ) x = 0;												//	 *   |   *
			if ( x > this.Allocation.Width - 4 ) x = this.Allocation.Width - 4;					//	*    |    *
			if ( y < 0 ) y = 0;												//	*    |    *
			if ( y > this.Allocation.Height - 4 ) y = this.Allocation.Height - 4;					//	*----X----*
			//	*    |    *
			if ( m_iMarker_Y == y && m_iMarker_X == x && !Unconditional )	//	*    |    *
				return;														//	 *   |   *
			//	  *  |  *
			ClearMarker(win);													//	   *****

			m_iMarker_X = x;
			m_iMarker_Y = y;

			//Graphics g = Gtk.DotNet.Graphics.FromDrawable( win );

			//Pen pen;
			GraphUtil.HSL _hsl = GetColor(x,y);	//	The selected color determines the color of the marker drawn over
			//	it (black or white)
			Color color = Color.White;
			if ( _hsl.L < (double)200/255 )
				color = Color.White;									//	White marker if selected color is dark
			else if ( _hsl.H < (double)26/360 || _hsl.H > (double)200/360 )
				if ( _hsl.S > (double)70/255 )
					color = Color.White;
				else
					color = Color.Black;								//	Else use a black marker for lighter colors
			else
				color = Color.Black;

			Gdk.GC gc = new Gdk.GC( win );
			gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( color );
			win.DrawArc( gc, false ,x - 3, y - 3, 10, 10, 0 * 64, 360 * 64 );
			
			

			//DrawBorder(win);		//	Force the border to be redrawn, just in case the marker has been drawn over it.
		
		}


		/// <summary>
		/// Draws the border around the control.
		/// </summary>
		private void DrawBorder(Gdk.Window g)
		{
			//	To make the control look like Adobe Photoshop's the border around the control will be a gray line
			//	on the top and left side, a white line on the bottom and right side, and a black rectangle (line) 
			//	inside the gray/white rectangle

			Gdk.GC gc = new Gdk.GC(g);
			gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( Color.FromArgb(172,168,153) );//	The same gray color used by Photoshop
			g.DrawLine(gc, this.Allocation.Width - 2, 0, 0, 0);	//	Draw top line
			g.DrawLine(gc, 0, 0, 0, this.Allocation.Height - 2);	//	Draw left hand line

			gc.RgbFgColor = GraphUtil.gdkColorFromWinForms(Color.White);
			g.DrawLine(gc, this.Allocation.Width - 1, 0, this.Allocation.Width - 1,this.Allocation.Height - 1);	//	Draw right hand line
			g.DrawLine(gc, this.Allocation.Width - 1,this.Allocation.Height - 1, 0,this.Allocation.Height - 1);	//	Draw bottome line

			gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( Color.Black );
			g.DrawRectangle( gc, false, 1, 1, this.Allocation.Width - 3, this.Allocation.Height - 3);	//	Draw inner black rectangle
			
		}


		/// <summary>
		/// Draws the content of the control filling in all color values with the provided Hue value.
		/// </summary>
		private void Draw_Style_Hue(Gdk.Window win)
		{
			Graphics g = Gtk.DotNet.Graphics.FromDrawable(win);

			GraphUtil.HSL hsl_start = new GraphUtil.HSL();
			GraphUtil.HSL hsl_end = new GraphUtil.HSL();
			hsl_start.H = m_hsl.H;
			hsl_end.H = m_hsl.H;
			hsl_start.S = 0.0;
			hsl_end.S = 1.0;

			for ( int i = 0; i < this.Allocation.Height - 4; i++ )				//	For each horizontal line in the control:
			{
				hsl_start.L = 1.0 - (double)i/(this.Allocation.Height - 4);	//	Calculate luminance at this line (Hue and Saturation are constant)
				hsl_end.L = hsl_start.L;
				
				LinearGradientBrush br = new LinearGradientBrush(new Rectangle(2,2, this.Allocation.Width - 4, 1), GraphUtil.HSL_to_RGB(hsl_start), GraphUtil.HSL_to_RGB(hsl_end), 0, false); 
				g.FillRectangle(br,new Rectangle(2,i + 2, this.Allocation.Width - 4, 1)); 
			}
			g.Dispose();
		}


		/// <summary>
		/// Draws the content of the control filling in all color values with the provided Saturation value.
		/// </summary>
		private void Draw_Style_Saturation(Gdk.Window win)
		{
			Graphics g = Gtk.DotNet.Graphics.FromDrawable(win);

			GraphUtil.HSL hsl_start = new GraphUtil.HSL();
			GraphUtil.HSL hsl_end = new GraphUtil.HSL();
			hsl_start.S = m_hsl.S;
			hsl_end.S = m_hsl.S;
			hsl_start.L = 1.0;
			hsl_end.L = 0.0;

			int width = this.Allocation.Width;
			for ( int i = 0; i <  width - 4; i++ )		//	For each vertical line in the control:
			{
				hsl_start.H = (double)i/(width - 4);	//	Calculate Hue at this line (Saturation and Luminance are constant)
				hsl_end.H   = hsl_start.H;
				
				LinearGradientBrush br = new LinearGradientBrush(new Rectangle(2,2, 1, this.Allocation.Height - 4), GraphUtil.HSL_to_RGB(hsl_start), GraphUtil.HSL_to_RGB(hsl_end), 90, false); 
				g.FillRectangle(br,new Rectangle(i + 2, 2, 1, this.Allocation.Height - 4)); 
			}
			g.Dispose();
		}


		/// <summary>
		/// Draws the content of the control filling in all color values with the provided Luminance or Brightness value.
		/// </summary>
		private void Draw_Style_Luminance(Gdk.Window win)
		{
			Graphics g = Gtk.DotNet.Graphics.FromDrawable(win);

			GraphUtil.HSL hsl_start = new GraphUtil.HSL();
			GraphUtil.HSL hsl_end = new GraphUtil.HSL();
			hsl_start.L = m_hsl.L;
			hsl_end.L = m_hsl.L;
			hsl_start.S = 1.0;
			hsl_end.S = 0.0;

			for ( int i = 0; i < this.Allocation.Width - 4; i++ )		//	For each vertical line in the control:
			{
				hsl_start.H = (double)i/(this.Allocation.Width - 4);	//	Calculate Hue at this line (Saturation and Luminance are constant)
				hsl_end.H = hsl_start.H;
				
				LinearGradientBrush br = new LinearGradientBrush(new Rectangle(2,2, 1, this.Allocation.Height - 4), GraphUtil.HSL_to_RGB(hsl_start), GraphUtil.HSL_to_RGB(hsl_end), 90, false); 
				g.FillRectangle(br,new Rectangle(i + 2, 2, 1, this.Allocation.Height - 4)); 
			}
			g.Dispose();
		}


		/// <summary>
		/// Draws the content of the control filling in all color values with the provided Red value.
		/// </summary>
		private void Draw_Style_Red( Gdk.Window win )
		{
			Graphics g = Gtk.DotNet.Graphics.FromDrawable(win);

			int red = m_rgb.R;;

			for ( int i = 0; i < this.Allocation.Height - 4; i++ )				//	For each horizontal line in the control:
			{
				//	Calculate Green at this line (Red and Blue are constant)
				int green = Round(255 - (255 * (double)i/(this.Allocation.Height - 4)));

				LinearGradientBrush br = new LinearGradientBrush(new Rectangle(2,2, this.Allocation.Width - 4, 1), Color.FromArgb(red, green, 0), Color.FromArgb(red, green, 255), 0, false); 
				g.FillRectangle(br,new Rectangle(2,i + 2, this.Allocation.Width - 4, 1)); 
			}
			g.Dispose();
		}


		/// <summary>
		/// Draws the content of the control filling in all color values with the provided Green value.
		/// </summary>
		private void Draw_Style_Green( Gdk.Window win )
		{
			Graphics g = Gtk.DotNet.Graphics.FromDrawable(win);

			int green = m_rgb.G;;

			for ( int i = 0; i < this.Allocation.Height - 4; i++ )	//	For each horizontal line in the control:
			{
				//	Calculate Red at this line (Green and Blue are constant)
				int red = Round(255 - (255 * (double)i/(this.Allocation.Height - 4)));

				LinearGradientBrush br = new LinearGradientBrush(new Rectangle(2,2, this.Allocation.Width - 4, 1), Color.FromArgb(red, green, 0), Color.FromArgb(red, green, 255), 0, false); 
				g.FillRectangle(br,new Rectangle(2,i + 2, this.Allocation.Width - 4, 1)); 
			}
			g.Dispose();
		}


		/// <summary>
		/// Draws the content of the control filling in all color values with the provided Blue value.
		/// </summary>
		private void Draw_Style_Blue( Gdk.Window win )
		{
			Graphics g = Gtk.DotNet.Graphics.FromDrawable(win);

			int blue = m_rgb.B;;

			for ( int i = 0; i < this.Allocation.Height - 4; i++ )	//	For each horizontal line in the control:
			{
				//	Calculate Green at this line (Red and Blue are constant)
				int green = Round(255 - (255 * (double)i/(this.Allocation.Height - 4)));

				LinearGradientBrush br = new LinearGradientBrush(new Rectangle(2,2, this.Allocation.Width - 4, 1), Color.FromArgb(0, green, blue), Color.FromArgb(255, green, blue), 0, false); 
				g.FillRectangle(br,new Rectangle(2,i + 2, this.Allocation.Width - 4, 1)); 
			}
			g.Dispose();
		}


		/// <summary>
		/// Calls all the functions neccessary to redraw the entire control.
		/// </summary>
		private void Redraw_Control(Gdk.Window win)
		{
			DrawBorder(win);

			switch (m_eDrawStyle)
			{
				case eDrawStyle.Hue :
					Draw_Style_Hue(win);
					break;
				case eDrawStyle.Saturation :
					Draw_Style_Saturation(win);
					break;
				case eDrawStyle.Brightness :
					Draw_Style_Luminance(win);
					break;
				case eDrawStyle.Red :
					Draw_Style_Red(win);
					break;
				case eDrawStyle.Green :
					Draw_Style_Green(win);
					break;
				case eDrawStyle.Blue :
					Draw_Style_Blue(win);
					break;
			} 

			DrawMarker(win,m_iMarker_X, m_iMarker_Y, true);
		}


		/// <summary>
		/// Resets the marker position of the slider to match the controls color.  Gives the option of redrawing the slider.
		/// </summary>
		/// <param name="Redraw">Set to true if you want the function to redraw the slider after determining the best position</param>
		private void Reset_Marker(Gdk.Window win, bool Redraw)
		{
			switch (m_eDrawStyle)
			{
				case eDrawStyle.Hue :
					m_iMarker_X = Round((this.Allocation.Width - 4) * m_hsl.S);
					m_iMarker_Y = Round((this.Allocation.Height - 4) * (1.0 - m_hsl.L));
					break;
				case eDrawStyle.Saturation :
					m_iMarker_X = Round((this.Allocation.Width - 4) * m_hsl.H);
					m_iMarker_Y = Round((this.Allocation.Height - 4) * (1.0 - m_hsl.L));
					break;
				case eDrawStyle.Brightness :
					m_iMarker_X = Round((this.Allocation.Width - 4) * m_hsl.H);
					m_iMarker_Y = Round((this.Allocation.Height - 4) * (1.0 - m_hsl.S));
					break;
				case eDrawStyle.Red :
					m_iMarker_X = Round((this.Allocation.Width - 4) * (double)m_rgb.B/255);
					m_iMarker_Y = Round((this.Allocation.Height - 4) * (1.0 - (double)m_rgb.G/255));
					break;
				case eDrawStyle.Green :
					m_iMarker_X = Round((this.Allocation.Width - 4) * (double)m_rgb.B/255);
					m_iMarker_Y = Round((this.Allocation.Height - 4) * (1.0 - (double)m_rgb.R/255));
					break;
				case eDrawStyle.Blue :
					m_iMarker_X = Round((this.Allocation.Width - 4) * (double)m_rgb.R/255);
					m_iMarker_Y = Round((this.Allocation.Height - 4) * (1.0 - (double)m_rgb.G/255));
					break;
			}

			if ( Redraw )
				DrawMarker(win,m_iMarker_X, m_iMarker_Y, true);
		}


		/// <summary>
		/// Resets the controls color (both HSL and RGB variables) based on the current marker position
		/// </summary>
		private void ResetHSLRGB( )
		{
			int red, green, blue;

			switch (m_eDrawStyle)
			{
				case eDrawStyle.Hue :
					m_hsl.S = (double)m_iMarker_X/(this.Allocation.Width - 4);
					m_hsl.L = 1.0 - (double)m_iMarker_Y/(this.Allocation.Height - 4);
					m_rgb = GraphUtil.HSL_to_RGB(m_hsl);
					break;
				case eDrawStyle.Saturation :
					m_hsl.H = (double)m_iMarker_X/(this.Allocation.Width - 4);
					m_hsl.L = 1.0 - (double)m_iMarker_Y/(this.Allocation.Height - 4);
					m_rgb = GraphUtil.HSL_to_RGB(m_hsl);
					break;
				case eDrawStyle.Brightness :
					m_hsl.H = (double)m_iMarker_X/(this.Allocation.Width - 4);
					m_hsl.S = 1.0 - (double)m_iMarker_Y/(this.Allocation.Height - 4);
					m_rgb = GraphUtil.HSL_to_RGB(m_hsl);
					break;
				case eDrawStyle.Red :
					blue = Round(255 * (double)m_iMarker_X/(this.Allocation.Width - 4));
					green = Round(255 * (1.0 - (double)m_iMarker_Y/(this.Allocation.Height - 4)));
					m_rgb = Color.FromArgb(m_rgb.R, green, blue);
					m_hsl = GraphUtil.RGB_to_HSL(m_rgb);
					break;
				case eDrawStyle.Green :
					blue = Round(255 * (double)m_iMarker_X/(this.Allocation.Width - 4));
					red = Round(255 * (1.0 - (double)m_iMarker_Y/(this.Allocation.Height - 4)));
					m_rgb = Color.FromArgb(red, m_rgb.G, blue);
					m_hsl = GraphUtil.RGB_to_HSL(m_rgb);
					break;
				case eDrawStyle.Blue :
					red = Round(255 * (double)m_iMarker_X/(this.Allocation.Width - 4));
					green = Round(255 * (1.0 - (double)m_iMarker_Y/(this.Allocation.Height - 4)));
					m_rgb = Color.FromArgb(red, green, m_rgb.B);
					m_hsl = GraphUtil.RGB_to_HSL(m_rgb);
					break;
			}
		}


		/// <summary>
		/// Kindof self explanitory, I really need to look up the .NET function that does this.
		/// </summary>
		/// <param name="val">double value to be rounded to an integer</param>
		/// <returns></returns>
		private int Round(double val)
		{
			int ret_val = (int)val;
			
			int temp = (int)(val * 100);

			if ( (temp % 100) >= 50 )
				ret_val += 1;

			return ret_val;
			
		}


		/// <summary>
		/// Returns the graphed color at the x,y position on the control
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private GraphUtil.HSL GetColor(int x, int y)
		{

			GraphUtil.HSL _hsl = new GraphUtil.HSL();

			switch (m_eDrawStyle)
			{
				case eDrawStyle.Hue :
					_hsl.H = m_hsl.H;
					_hsl.S = (double)x/(this.Allocation.Width - 4);
					_hsl.L = 1.0 - (double)y/(this.Allocation.Height - 4);
					break;
				case eDrawStyle.Saturation :
					_hsl.S = m_hsl.S;
					_hsl.H = (double)x/(this.Allocation.Width - 4);
					_hsl.L = 1.0 - (double)y/(this.Allocation.Height - 4);
					break;
				case eDrawStyle.Brightness :
					_hsl.L = m_hsl.L;
					_hsl.H = (double)x/(this.Allocation.Width - 4);
					_hsl.S = 1.0 - (double)y/(this.Allocation.Height - 4);
					break;
				case eDrawStyle.Red :
					_hsl = GraphUtil.RGB_to_HSL(Color.FromArgb(m_rgb.R, Round(255 * (1.0 - (double)y/(this.Allocation.Height - 4))), Round(255 * (double)x/(this.Allocation.Width - 4))));
					break;
				case eDrawStyle.Green :
					_hsl = GraphUtil.RGB_to_HSL(Color.FromArgb(Round(255 * (1.0 - (double)y/(this.Allocation.Height - 4))), m_rgb.G, Round(255 * (double)x/(this.Allocation.Width - 4))));
					break;
				case eDrawStyle.Blue :
					_hsl = GraphUtil.RGB_to_HSL(Color.FromArgb(Round(255 * (double)x/(this.Allocation.Width - 4)), Round(255 * (1.0 - (double)y/(this.Allocation.Height - 4))), m_rgb.B));
					break;
			}

			return _hsl;
		}


		#endregion
	}
}
