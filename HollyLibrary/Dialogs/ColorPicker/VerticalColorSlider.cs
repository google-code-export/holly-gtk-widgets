using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Gtk;

namespace HollyLibrary
{

	/// <summary>
	/// A vertical slider control that shows a range for a color property (a.k.a. Hue, Saturation, Brightness,
	/// Red, Green, Blue) and sends an event when the slider is changed.
	/// </summary>
	public class VerticalColorSlider : DrawingArea
	{
		#region Class Variables


		//	Slider properties
		private int			m_iMarker_Start_Y = 0;
		private bool		m_bDragging = false;

		//	These variables keep track of how to fill in the content inside the box;
		private eDrawStyle		m_eDrawStyle = eDrawStyle.Hue;
		private AdobeColors.HSL	m_hsl;
		private Color			m_rgb;


		#endregion

		#region Constructors / Destructors

		public VerticalColorSlider()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//	Initialize Colors
			m_hsl = new AdobeColors.HSL();
			m_hsl.H = 1.0;
			m_hsl.S = 1.0;
			m_hsl.L = 1.0;
			m_rgb = AdobeColors.HSL_to_RGB(m_hsl);
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
			// ctrl1DColorBar
			// 
			this.Name = "ctrl1DColorBar";
			SetSizeRequest( 40, 264 );
			
			this.AddEvents( (int) Gdk.EventMask.AllEventsMask );
		}
		#endregion

		#region Control Events



		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			
			if ( evnt.Button != 1 )	//	Only respond to left mouse button events
				return false;

			m_bDragging = true;		//	Begin dragging which notifies MouseMove function that it needs to update the marker

			int y;
			y = (int)evnt.Y;
			y -= 4;											//	Calculate slider position
			if ( y < 0 ) y = 0;
			if ( y > this.Allocation.Height - 9 ) y = this.Allocation.Height - 9;

			m_iMarker_Start_Y = y;
			this.QueueDraw();
			ResetHSLRGB();			//	Reset the color

			if ( Scroll != null )	//	Notify anyone who cares that the controls slider(color) has changed
				Scroll(this, new EventArgs());
			
			return base.OnButtonPressEvent (evnt);
		}

		protected override bool OnMotionNotifyEvent (Gdk.EventMotion evnt)
		{
			if ( !m_bDragging )		//	Only respond when the mouse is dragging the marker.
				return false;

			int y;
			y = (int)evnt.Y;
			y -= 4; 										//	Calculate slider position
			if ( y < 0 ) y = 0;
			if ( y > this.Allocation.Height - 9 ) y = this.Allocation.Height - 9;

			m_iMarker_Start_Y = y;
			this.QueueDraw();
			ResetHSLRGB();			//	Reset the color

			if ( Scroll != null )	//	Notify anyone who cares that the controls slider(color) has changed
				Scroll(this, new EventArgs());
			return base.OnMotionNotifyEvent (evnt);
		}

		protected override bool OnButtonReleaseEvent (Gdk.EventButton evnt)
		{
			//Console.WriteLine( evnt.Button );
			if ( evnt.Button != 1 )	//	Only respond to left mouse button events
				return false;

			m_bDragging = false;

			int y;
			y = (int)evnt.Y;
			y -= 4; 										//	Calculate slider position
			if ( y < 0 ) y = 0;
			if ( y > this.Allocation.Height - 9 ) y = this.Allocation.Height - 9;
			m_iMarker_Start_Y = y;
			this.QueueDraw();	//	Redraw the slider
			ResetHSLRGB();			//	Reset the color

			if ( Scroll != null )	//	Notify anyone who cares that the controls slider(color) has changed
				Scroll(this, new EventArgs());
			return base.OnButtonReleaseEvent (evnt);
		}


		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Redraw_Control(evnt.Window);
			
			return true;
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
				Reset_Slider(true);
				this.QueueDraw();
			}
		}


		/// <summary>
		/// The HSL color of the control, changing the HSL will automatically change the RGB color for the control.
		/// </summary>
		public AdobeColors.HSL HSL
		{
			get
			{
				return m_hsl;
			}
			set
			{
				m_hsl = value;
				m_rgb = AdobeColors.HSL_to_RGB(m_hsl);

				//	Redraw the control based on the new color.
				Reset_Slider(true);
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
				m_hsl = AdobeColors.RGB_to_HSL(m_rgb);

				//	Redraw the control based on the new color.
				Reset_Slider(true);
				this.QueueDraw();
			}
		}


		#endregion

		#region Private Methods

		/// <summary>
		/// Redraws the background over the slider area on both sides of the control
		/// </summary>
		private void ClearSlider(Gdk.Window g)
		{
			//Brush brush = System.Drawing.SystemBrushes.Control;
			Gdk.Color color = this.Style.Background( this.State );
			Gdk.GC gc       = new Gdk.GC( g );
			gc.RgbBgColor   = color;
			gc.RgbFgColor   = color;
			g.DrawRectangle( gc, true, 0, 0, 8, this.Allocation.Height);				//	clear left hand slider
			g.DrawRectangle( gc, true, this.Allocation.Width - 8, 0, 8, this.Allocation.Height);	//	clear right hand slider
		}


		/// <summary>
		/// Draws the slider arrows on both sides of the control.
		/// </summary>
		/// <param name="position">position value of the slider, lowest being at the bottom.  The range
		/// is between 0 and the controls height-9.  The values will be adjusted if too large/small</param>
		/// <param name="Unconditional">If Unconditional is true, the slider is drawn, otherwise some logic 
		/// is performed to determine is drawing is really neccessary.</param>
		private void DrawSlider(Gdk.Window g,int position, bool Unconditional)
		{
			if ( position < 0 ) position = 0;
			if ( position > this.Allocation.Height - 9 ) position = this.Allocation.Height - 9;

			if ( m_iMarker_Start_Y == position && !Unconditional )	//	If the marker position hasn't changed
				return;												//	since the last time it was drawn and we don't HAVE to redraw
			//	then exit procedure

			m_iMarker_Start_Y = position;	//	Update the controls marker position

			this.ClearSlider(g);		//	Remove old slider

			Gdk.GC gcfill  = new Gdk.GC( g );
			//	Same gray color Photoshop uses
			gcfill.RgbFgColor = GraphUtil.gdkColorFromWinForms(Color.FromArgb(116,114,106));
			
			
			Gdk.GC gcborder  = new Gdk.GC( g );
			gcfill.RgbFgColor = GraphUtil.gdkColorFromWinForms( Color.White );
			//	Same gray color Photoshop uses
			//Console.WriteLine( "position="+position );
			
			Gdk.Point[] arrow = new Gdk.Point[7];				//	 GGG
			arrow[0] = new Gdk.Point(1,position);			//	G   G
			arrow[1] = new Gdk.Point(3,position);			//	G    G
			arrow[2] = new Gdk.Point(7,position + 4);		//	G     G
			arrow[3] = new Gdk.Point(3,position + 8);		//	G      G
			arrow[4] = new Gdk.Point(1,position + 8);		//	G     G
			arrow[5] = new Gdk.Point(0,position + 7);		//	G    G
			arrow[6] = new Gdk.Point(0,position + 1);		//	G   G
			//	 GGG
			g.DrawPolygon( gcfill  , true , arrow );//	Fill left arrow with white
			g.DrawPolygon( gcborder, false, arrow );//	Draw left arrow border with gray

			//	    GGG
			arrow[0] = new Gdk.Point(this.Allocation.Width - 2,position);		//	   G   G
			arrow[1] = new Gdk.Point(this.Allocation.Width - 4,position);		//	  G    G
			arrow[2] = new Gdk.Point(this.Allocation.Width - 8,position + 4);	//	 G     G
			arrow[3] = new Gdk.Point(this.Allocation.Width - 4,position + 8);	//	G      G
			arrow[4] = new Gdk.Point(this.Allocation.Width - 2,position + 8);	//	 G     G
			arrow[5] = new Gdk.Point(this.Allocation.Width - 1,position + 7);	//	  G    G
			arrow[6] = new Gdk.Point(this.Allocation.Width - 1,position + 1);	//	   G   G
			//	    GGG

			g.DrawPolygon(gcfill  , true , arrow );	//	Fill right arrow with white
			g.DrawPolygon(gcborder, false, arrow );	//	Draw right arrow border with gray

		}


		/// <summary>
		/// Draws the border around the control, in this case the border around the content area between
		/// the slider arrows.
		/// </summary>
		private void DrawBorder(Gdk.Window g)
		{
			//	To make the control look like Adobe Photoshop's the border around the control will be a gray line
			//	on the top and left side, a white line on the bottom and right side, and a black rectangle (line) 
			//	inside the gray/white rectangle
			Gdk.GC gc = new Gdk.GC( g );
			gc.RgbFgColor = GraphUtil.gdkColorFromWinForms(Color.FromArgb(172,168,153));
			//The same gray color used by Photoshop
			
			g.DrawLine(gc, this.Allocation.Width - 10, 2, 9, 2);	//	Draw top line
			g.DrawLine(gc, 9, 2, 9, this.Allocation.Height - 4);	//	Draw left hand line

			gc            = new Gdk.GC( g );
			gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( Color.White );
			
			
			g.DrawLine(gc, this.Allocation.Width - 9, 2, this.Allocation.Width - 9,this.Allocation.Height - 3);	//	Draw right hand line
			g.DrawLine(gc, this.Allocation.Width - 9,this.Allocation.Height - 3, 9,this.Allocation.Height - 3);	//	Draw bottome line

			gc            = new Gdk.GC( g );
			gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( Color.Black );
			g.DrawRectangle(gc, false, 10, 3, this.Allocation.Width - 20, this.Allocation.Height - 7);	//	Draw inner black rectangle
		}


	


		#region Draw_Style_X - Content drawing functions

		//	The following functions do the real work of the control, drawing the primary content (the area between the slider)
		//	

		/// <summary>
		/// Fills in the content of the control showing all values of Hue (from 0 to 360)
		/// </summary>
		private void Draw_Style_Hue(Gdk.Window g)
		{
			
				AdobeColors.HSL _hsl = new AdobeColors.HSL();
				_hsl.S = 1.0;	//	S and L will both be at 100% for this DrawStyle
				_hsl.L = 1.0;
	
				for ( int i = 0; i < this.Allocation.Height - 8; i++ )	//	i represents the current line of pixels we want to draw horizontally
				{
					_hsl.H = 1.0 - (double)i/(this.Allocation.Height - 8);			//	H (hue) is based on the current vertical position
					Color c = AdobeColors.HSL_to_RGB(_hsl);
				
				    Gdk.GC gc     = new Gdk.GC( g );
				    gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( c );
	
					g.DrawLine( gc, 11, i + 4, this.Allocation.Width - 11, i +4 );//	Draw the line and loop back for next line
				}
			
		}
	

		/// <summary>
		/// Fills in the content of the control showing all values of Saturation (0 to 100%) for the given
		/// Hue and Luminance.
		/// </summary>
		private void Draw_Style_Saturation(Gdk.Window g)
		{
			AdobeColors.HSL _hsl = new AdobeColors.HSL();
			_hsl.H = m_hsl.H;	//	Use the H and L values of the current color (m_hsl)
			_hsl.L = m_hsl.L;

			for ( int i = 0; i < this.Allocation.Height - 8; i++ ) //	i represents the current line of pixels we want to draw horizontally
			{
				_hsl.S = 1.0 - (double)i/(this.Allocation.Height - 8);			//	S (Saturation) is based on the current vertical position
				
				Color c = AdobeColors.HSL_to_RGB(_hsl);
				
				Gdk.GC gc     = new Gdk.GC( g );
				gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( c );
				g.DrawLine( gc, 11, i + 4, this.Allocation.Width - 11, i + 4 );	//	Draw the line and loop back for next line
			}
		}


		/// <summary>
		/// Fills in the content of the control showing all values of Luminance (0 to 100%) for the given
		/// Hue and Saturation.
		/// </summary>
		private void Draw_Style_Luminance(Gdk.Window g)
		{
			AdobeColors.HSL _hsl = new AdobeColors.HSL();
			_hsl.H = m_hsl.H;	//	Use the H and S values of the current color (m_hsl)
			_hsl.S = m_hsl.S;

			for ( int i = 0; i < this.Allocation.Height - 8; i++ ) //	i represents the current line of pixels we want to draw horizontally
			{
				_hsl.L = 1.0 - (double)i/(this.Allocation.Height - 8);			//	L (Luminance) is based on the current vertical position
				//	Get the Color for this line
				Color c = AdobeColors.HSL_to_RGB(_hsl);
				
				Gdk.GC gc     = new Gdk.GC( g );
				gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( c );
				g.DrawLine( gc, 11, i + 4, this.Allocation.Width - 11, i + 4 );//	Draw the line and loop back for next line
			}
		}


		/// <summary>
		/// Fills in the content of the control showing all values of Red (0 to 255) for the given
		/// Green and Blue.
		/// </summary>
		private void Draw_Style_Red(Gdk.Window g)
		{
			for ( int i = 0; i < this.Allocation.Height - 8; i++ ) //	i represents the current line of pixels we want to draw horizontally
			{
				int red = 255 - Round(255 * (double)i/(this.Allocation.Height - 8));	//	red is based on the current vertical position
				//	Get the Color for this line
				
				Color c = Color.FromArgb(red, m_rgb.G, m_rgb.B);
				
				Gdk.GC gc     = new Gdk.GC( g );
				gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( c );
				g.DrawLine( gc, 11, i + 4, this.Allocation.Width - 11, i + 4 );			//	Draw the line and loop back for next line
			}
		}


		/// <summary>
		/// Fills in the content of the control showing all values of Green (0 to 255) for the given
		/// Red and Blue.
		/// </summary>
		private void Draw_Style_Green(Gdk.Window g)
		{
			for ( int i = 0; i < this.Allocation.Height - 8; i++ ) //	i represents the current line of pixels we want to draw horizontally
			{
				int green = 255 - Round(255 * (double)i/(this.Allocation.Height - 8));	//	green is based on the current vertical position
				//	Get the Color for this line

				Color c = Color.FromArgb(m_rgb.R, green, m_rgb.B);
				
				Gdk.GC gc     = new Gdk.GC( g );
				gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( c );
				g.DrawLine( gc, 11, i + 4, this.Allocation.Width - 11, i + 4 );		
			    //	Draw the line and loop back for next line
			}
		}


		/// <summary>
		/// Fills in the content of the control showing all values of Blue (0 to 255) for the given
		/// Red and Green.
		/// </summary>
		private void Draw_Style_Blue(Gdk.Window g)
		{
			for ( int i = 0; i < this.Allocation.Height - 8; i++ ) //	i represents the current line of pixels we want to draw horizontally
			{
				int blue = 255 - Round(255 * (double)i/(this.Allocation.Height - 8));	//	green is based on the current vertical position
				

				Color c       = Color.FromArgb(m_rgb.R, m_rgb.G, blue);
				
				Gdk.GC gc     = new Gdk.GC( g );
				gc.RgbFgColor = GraphUtil.gdkColorFromWinForms( c );
				g.DrawLine( gc, 11, i + 4, this.Allocation.Width - 11, i + 4 );		
			    //	Draw the line and loop back for next line
			}
		}


		#endregion

		/// <summary>
		/// Calls all the functions neccessary to redraw the entire control.
		/// </summary>
		private void Redraw_Control( Gdk.Window g)
		{
			DrawSlider(g,m_iMarker_Start_Y, true);
			DrawBorder(g);
			
			switch (m_eDrawStyle)
			{
				case eDrawStyle.Hue :
					Draw_Style_Hue(g);
					break;
				case eDrawStyle.Saturation :
					Draw_Style_Saturation(g);
					break;
				case eDrawStyle.Brightness :
					Draw_Style_Luminance(g);
					break;
				case eDrawStyle.Red :
					Draw_Style_Red(g);
					break;
				case eDrawStyle.Green :
					Draw_Style_Green(g);
					break;
				case eDrawStyle.Blue :
					Draw_Style_Blue(g);
					break;
			}
		}


		/// <summary>
		/// Resets the vertical position of the slider to match the controls color.  Gives the option of redrawing the slider.
		/// </summary>
		/// <param name="Redraw">Set to true if you want the function to redraw the slider after determining the best position</param>
		private void Reset_Slider( bool Redraw)
		{
			//	The position of the marker (slider) changes based on the current drawstyle:
			switch (m_eDrawStyle)
			{
				case eDrawStyle.Hue :
					m_iMarker_Start_Y = (this.Allocation.Height - 8) - Round( (this.Allocation.Height - 8) * m_hsl.H );
					break;
				case eDrawStyle.Saturation :
					m_iMarker_Start_Y = (this.Allocation.Height - 8) - Round( (this.Allocation.Height - 8) * m_hsl.S );
					break;
				case eDrawStyle.Brightness :
					m_iMarker_Start_Y = (this.Allocation.Height - 8) - Round( (this.Allocation.Height - 8) * m_hsl.L );
					break;
				case eDrawStyle.Red :
					m_iMarker_Start_Y = (this.Allocation.Height - 8) - Round( (this.Allocation.Height - 8) * (double)m_rgb.R/255 );
					break;
				case eDrawStyle.Green :
					m_iMarker_Start_Y = (this.Allocation.Height - 8) - Round( (this.Allocation.Height - 8) * (double)m_rgb.G/255 );
					break;
				case eDrawStyle.Blue :
					m_iMarker_Start_Y = (this.Allocation.Height - 8) - Round( (this.Allocation.Height - 8) * (double)m_rgb.B/255 );
					break;
			}

			if ( Redraw )
				this.QueueDraw();
		}


		/// <summary>
		/// Resets the controls color (both HSL and RGB variables) based on the current slider position
		/// </summary>
		private void ResetHSLRGB()
		{
			switch (m_eDrawStyle)
			{
				case eDrawStyle.Hue :
					m_hsl.H = 1.0 - (double)m_iMarker_Start_Y/(this.Allocation.Height - 9);
					m_rgb = AdobeColors.HSL_to_RGB(m_hsl);
					break;
				case eDrawStyle.Saturation :
					m_hsl.S = 1.0 - (double)m_iMarker_Start_Y/(this.Allocation.Height - 9);
					m_rgb = AdobeColors.HSL_to_RGB(m_hsl);
					break;
				case eDrawStyle.Brightness :
					m_hsl.L = 1.0 - (double)m_iMarker_Start_Y/(this.Allocation.Height - 9);
					m_rgb = AdobeColors.HSL_to_RGB(m_hsl);
					break;
				case eDrawStyle.Red :
					m_rgb = Color.FromArgb(255 - Round( 255 * (double)m_iMarker_Start_Y/(this.Allocation.Height - 9) ), m_rgb.G, m_rgb.B);
					m_hsl = AdobeColors.RGB_to_HSL(m_rgb);
					break;
				case eDrawStyle.Green :
					m_rgb = Color.FromArgb(m_rgb.R, 255 - Round( 255 * (double)m_iMarker_Start_Y/(this.Allocation.Height - 9) ), m_rgb.B);
					m_hsl = AdobeColors.RGB_to_HSL(m_rgb);
					break;
				case eDrawStyle.Blue :
					m_rgb = Color.FromArgb(m_rgb.R, m_rgb.G, 255 - Round( 255 * (double)m_iMarker_Start_Y/(this.Allocation.Height - 9) ));
					m_hsl = AdobeColors.RGB_to_HSL(m_rgb);
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


		#endregion
	}
}