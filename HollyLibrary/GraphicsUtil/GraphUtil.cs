// GraphUtil.cs created with MonoDevelop
// User: dantes at 3:01 PM 5/22/2008
//

using System; 
using System.Drawing; 
using System.Drawing.Drawing2D; 

namespace HollyLibrary
{
	
	
	public class GraphUtil
	{
#region color util
		
		#region Public Methods

		/// <summary> 
		/// Sets the absolute brightness of a colour 
		/// </summary> 
		/// <param name="c">Original colour</param> 
		/// <param name="brightness">The luminance level to impose</param> 
		/// <returns>an adjusted colour</returns> 
		public static  Color SetBrightness(Color c, double brightness) 
		{ 
			HSL hsl = RGB_to_HSL(c); 
			hsl.L=brightness; 
			return HSL_to_RGB(hsl); 
		} 


		/// <summary> 
		/// Modifies an existing brightness level 
		/// </summary> 
		/// <remarks> 
		/// To reduce brightness use a number smaller than 1. To increase brightness use a number larger tnan 1 
		/// </remarks> 
		/// <param name="c">The original colour</param> 
		/// <param name="brightness">The luminance delta</param> 
		/// <returns>An adjusted colour</returns> 
		public static  Color ModifyBrightness(Color c, double brightness) 
		{ 
			HSL hsl = RGB_to_HSL(c); 
			hsl.L*=brightness; 
			return HSL_to_RGB(hsl); 
		} 


		/// <summary> 
		/// Sets the absolute saturation level 
		/// </summary> 
		/// <remarks>Accepted values 0-1</remarks> 
		/// <param name="c">An original colour</param> 
		/// <param name="Saturation">The saturation value to impose</param> 
		/// <returns>An adjusted colour</returns> 
		public static  Color SetSaturation(Color c, double Saturation) 
		{ 
			HSL hsl = RGB_to_HSL(c); 
			hsl.S=Saturation; 
			return HSL_to_RGB(hsl); 
		} 


		/// <summary> 
		/// Modifies an existing Saturation level 
		/// </summary> 
		/// <remarks> 
		/// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tnan 1 
		/// </remarks> 
		/// <param name="c">The original colour</param> 
		/// <param name="Saturation">The saturation delta</param> 
		/// <returns>An adjusted colour</returns> 
		public static  Color ModifySaturation(Color c, double Saturation) 
		{ 
			HSL hsl = RGB_to_HSL(c); 
			hsl.S*=Saturation; 
			return HSL_to_RGB(hsl); 
		} 


		/// <summary> 
		/// Sets the absolute Hue level 
		/// </summary> 
		/// <remarks>Accepted values 0-1</remarks> 
		/// <param name="c">An original colour</param> 
		/// <param name="Hue">The Hue value to impose</param> 
		/// <returns>An adjusted colour</returns> 
		public static  Color SetHue(Color c, double Hue) 
		{ 
			HSL hsl = RGB_to_HSL(c); 
			hsl.H=Hue; 
			return HSL_to_RGB(hsl); 
		} 


		/// <summary> 
		/// Modifies an existing Hue level 
		/// </summary> 
		/// <remarks> 
		/// To reduce Hue use a number smaller than 1. To increase Hue use a number larger tnan 1 
		/// </remarks> 
		/// <param name="c">The original colour</param> 
		/// <param name="Hue">The Hue delta</param> 
		/// <returns>An adjusted colour</returns> 
		public static  Color ModifyHue(Color c, double Hue) 
		{ 
			HSL hsl = RGB_to_HSL(c); 
			hsl.H*=Hue; 
			return HSL_to_RGB(hsl); 
		} 


		/// <summary> 
		/// Converts a colour from HSL to RGB 
		/// </summary> 
		/// <remarks>Adapted from the algoritm in Foley and Van-Dam</remarks> 
		/// <param name="hsl">The HSL value</param> 
		/// <returns>A Color structure containing the equivalent RGB values</returns> 
		public static Color HSL_to_RGB(HSL hsl) 
		{ 
			int Max, Mid, Min;
			double q;

			Max = Round(hsl.L * 255);
			Min = Round((1.0 - hsl.S)*(hsl.L/1.0)*255);
			q   = (double)(Max - Min)/255;

			if ( hsl.H >= 0 && hsl.H <= (double)1/6 )
			{
				Mid = Round(((hsl.H - 0) * q) * 1530 + Min);
				return Color.FromArgb(Max,Mid,Min);
			}
			else if ( hsl.H <= (double)1/3 )
			{
				Mid = Round(-((hsl.H - (double)1/6) * q) * 1530 + Max);
				return Color.FromArgb(Mid,Max,Min);
			}
			else if ( hsl.H <= 0.5 )
			{
				Mid = Round(((hsl.H - (double)1/3) * q) * 1530 + Min);
				return Color.FromArgb(Min,Max,Mid);
			}
			else if ( hsl.H <= (double)2/3 )
			{
				Mid = Round(-((hsl.H - 0.5) * q) * 1530 + Max);
				return Color.FromArgb(Min,Mid,Max);
			}
			else if ( hsl.H <= (double)5/6 )
			{
				Mid = Round(((hsl.H - (double)2/3) * q) * 1530 + Min);
				return Color.FromArgb(Mid,Min,Max);
			}
			else if ( hsl.H <= 1.0 )
			{
				Mid = Round(-((hsl.H - (double)5/6) * q) * 1530 + Max);
				return Color.FromArgb(Max,Min,Mid);
			}
			else	return Color.FromArgb(0,0,0);
		} 
  

		/// <summary> 
		/// Converts RGB to HSL 
		/// </summary> 
		/// <remarks>Takes advantage of whats already built in to .NET by using the Color.GetHue, Color.GetSaturation and Color.GetBrightness methods</remarks> 
		/// <param name="c">A Color to convert</param> 
		/// <returns>An HSL value</returns> 
		public static HSL RGB_to_HSL (Color c) 
		{ 
			HSL hsl =  new HSL(); 
          
			int Max, Min, Diff;

			//	Of our RGB values, assign the highest value to Max, and the Smallest to Min
			if ( c.R > c.G )	{ Max = c.R; Min = c.G; }
			else				{ Max = c.G; Min = c.R; }
			if ( c.B > Max )	  Max = c.B;
			else if ( c.B < Min ) Min = c.B;

			Diff = Max - Min;

			//	Luminance - a.k.a. Brightness - Adobe photoshop uses the logic that the
			//	site VBspeed regards (regarded) as too primitive = superior decides the 
			//	level of brightness.
			hsl.L = (double)Max/255;

			//	Saturation
			if ( Max == 0 ) hsl.S = 0;	//	Protecting from the impossible operation of division by zero.
			else hsl.S = (double)Diff/Max;	//	The logic of Adobe Photoshops is this simple.

			//	Hue		R is situated at the angel of 360 eller noll degrees; 
			//			G vid 120 degrees
			//			B vid 240 degrees
			double q;
			if ( Diff == 0 ) q = 0; // Protecting from the impossible operation of division by zero.
			else q = (double)60/Diff;
			
			if ( Max == c.R )
			{
					if ( c.G < c.B )	hsl.H = (double)(360 + q * (c.G - c.B))/360;
				else				hsl.H = (double)(q * (c.G - c.B))/360;
			}
			else if ( Max == c.G )	hsl.H = (double)(120 + q * (c.B - c.R))/360;
			else if ( Max == c.B )	hsl.H = (double)(240 + q * (c.R - c.G))/360;
			else					hsl.H = 0.0;

			return hsl; 
		} 


		/// <summary>
		/// Converts RGB to CMYK
		/// </summary>
		/// <param name="c">A color to convert.</param>
		/// <returns>A CMYK object</returns>
		public static CMYK RGB_to_CMYK(Color c)
		{
			CMYK _cmyk = new CMYK();
			double low = 1.0;

			_cmyk.C = (double)(255 - c.R)/255;
			if ( low > _cmyk.C )
				low = _cmyk.C;

			_cmyk.M = (double)(255 - c.G)/255;
			if ( low > _cmyk.M )
				low = _cmyk.M;

			_cmyk.Y = (double)(255 - c.B)/255;
			if ( low > _cmyk.Y )
				low = _cmyk.Y;

			if ( low > 0.0 )
			{
				_cmyk.K = low;
			}

			return _cmyk;
		}


		/// <summary>
		/// Converts CMYK to RGB
		/// </summary>
		/// <param name="_cmyk">A color to convert</param>
		/// <returns>A Color object</returns>
		public static Color CMYK_to_RGB(CMYK _cmyk)
		{
			int red, green, blue;

			red =	Round(255 - (255 * _cmyk.C));
			green =	Round(255 - (255 * _cmyk.M));
			blue =	Round(255 - (255 * _cmyk.Y));

			return Color.FromArgb(red, green, blue);
		}


		/// <summary>
		/// Custom rounding function.
		/// </summary>
		/// <param name="val">Value to round</param>
		/// <returns>Rounded value</returns>
		private static int Round(double val)
		{
			int ret_val = (int)val;
			
			int temp = (int)(val * 100);

			if ( (temp % 100) >= 50 )
				ret_val += 1;

			return ret_val;
		}


		#endregion

		#region Public Classes

		public class HSL 
		{ 
			#region Class Variables

			public HSL() 
			{ 
				_h=0; 
				_s=0; 
				_l=0; 
			} 

			double _h; 
			double _s; 
			double _l; 

			#endregion

			#region Public Methods

			public double H 
			{ 
				get{return _h;} 
				set 
				{ 
					_h=value; 
					_h=_h>1 ? 1 : _h<0 ? 0 : _h; 
				} 
			} 


			public double S 
			{ 
				get{return _s;} 
				set 
				{ 
					_s=value; 
					_s=_s>1 ? 1 : _s<0 ? 0 : _s; 
				} 
			} 


			public double L 
			{ 
				get{return _l;} 
				set 
				{ 
					_l=value; 
					_l=_l>1 ? 1 : _l<0 ? 0 : _l; 
				} 
			} 


			#endregion
		} 


		public class CMYK 
		{ 
			#region Class Variables

			public CMYK() 
			{ 
				_c=0; 
				_m=0; 
				_y=0; 
				_k=0; 
			} 


			double _c; 
			double _m; 
			double _y; 
			double _k;

			#endregion

			#region Public Methods

			public double C 
			{ 
				get{return _c;} 
				set 
				{ 
					_c=value; 
					_c=_c>1 ? 1 : _c<0 ? 0 : _c; 
				} 
			} 


			public double M 
			{ 
				get{return _m;} 
				set 
				{ 
					_m=value; 
					_m=_m>1 ? 1 : _m<0 ? 0 : _m; 
				} 
			} 


			public double Y 
			{ 
				get{return _y;} 
				set 
				{ 
					_y=value; 
					_y=_y>1 ? 1 : _y<0 ? 0 : _y; 
				} 
			} 


			public double K 
			{ 
				get{return _k;} 
				set 
				{ 
					_k=value; 
					_k=_k>1 ? 1 : _k<0 ? 0 : _k; 
				} 
			} 


			#endregion
		} 


		#endregion
#endregion
		
		public static Gdk.Color GdkTransparentColor
		{
			get
			{
				return GraphUtil.gdkColorFromWinForms( System.Drawing.Color.Transparent );
			}
		}
		
		public static string Color2Hex(Color rgb)
		{
			string red = Convert.ToString(rgb.R, 16);
			if ( red.Length < 2 ) red = "0" + red;
			string green = Convert.ToString(rgb.G, 16);
			if ( green.Length < 2 ) green = "0" + green;
			string blue = Convert.ToString(rgb.B, 16);
			if ( blue.Length < 2 ) blue = "0" + blue;

			return red.ToUpper() + green.ToUpper() + blue.ToUpper();
		}


		public static Color Hex2Color(string hex_data)
		{
			if ( hex_data.Length != 6 )
				return Color.Black;

			string r_text, g_text, b_text;
			int r, g, b;

			r_text = hex_data.Substring(0, 2);
			g_text = hex_data.Substring(2, 2);
			b_text = hex_data.Substring(4, 2);

			r = int.Parse(r_text, System.Globalization.NumberStyles.HexNumber);
			g = int.Parse(g_text, System.Globalization.NumberStyles.HexNumber);
			b = int.Parse(b_text, System.Globalization.NumberStyles.HexNumber);

			return Color.FromArgb(r, g, b);
		}
		
		public static Gdk.Color gdkColorFromWinForms( System.Drawing.Color color )
		{
			return new Gdk.Color( color.R, color.G, color.B);
		}
		
		public static Gdk.Pixbuf pixbufFromStock( String stock_id, Gtk.IconSize size )
		{
			Gdk.Pixbuf ret  = null;
			try
			{
				Gtk.IconSet iset = Gtk.IconFactory.LookupDefault( stock_id );
				ret              = iset.RenderIcon( Gtk.Widget.DefaultStyle, Gtk.TextDirection.None, Gtk.StateType.Normal, size, null, "" );
			}
			catch(Exception ex)
			{
				Console.WriteLine( ex.Message + ex.StackTrace );
			}
			return ret;
		}
		
		private Graphics mGraphics; 
        public Graphics Graphics 
        { 
            get{ return this.mGraphics; } 
            set{ this.mGraphics = value; } 
        } 


        public GraphUtil(Graphics graphics) 
        { 
            this.Graphics = graphics; 
        } 

		
		public static System.Drawing.Color winFormsColorFromGdk( Gdk.Color color )
		{
			int r = (int)color.Red   / 255;
			int g = (int)color.Green / 255;
			int b = (int)color.Blue  / 255;
			if( r > 255 ) r = 255;
			if( g > 255 ) g = 255;
			if( b > 255 ) b = 255;
			return System.Drawing.Color.FromArgb( r, g, b );
		}
		
		public static Gdk.Pixbuf PixbufFromBitmap( Bitmap bmp )
		{
			//save bitmap to stream
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			bmp.Save( stream, System.Drawing.Imaging.ImageFormat.Png );
			//verry important: put stream on position 0
			stream.Position     = 0;
			//get the pixmap mask
			Gdk.Pixbuf buf      = new Gdk.Pixbuf( stream, bmp.Width, bmp.Height );
			return buf;
		}
		
		public static Bitmap BitmapFromPixbuf( Gdk.Pixbuf pixbuf )
		{
			//save bitmap to stream
			byte[] bytes = pixbuf.SaveToBuffer( "png" );
			System.IO.MemoryStream stream = new System.IO.MemoryStream( bytes );
			//verry important: put stream on position 0
			stream.Position     = 0;
			//get the pixmap mask
			Bitmap bmp = new Bitmap( stream );
			return bmp;
		}

        #region Fills a Rounded Rectangle with integers. 
        public void FillRoundRectangle(System.Drawing.Brush brush, 
          int x, int y,
          int width, int height, int radius) 
        { 

            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y); 
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height); 
            float fradius = Convert.ToSingle(radius); 
            this.FillRoundRectangle(brush, fx, fy, 
              fwidth, fheight, fradius); 

        } 
        #endregion 


        #region Fills a Rounded Rectangle with continuous numbers.
        public void FillRoundRectangle(System.Drawing.Brush brush, 
          float x, float y,
          float width, float height, float radius)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = this.GetRoundedRect(rectangle, radius);
            this.Graphics.FillPath(brush, path);
        } 
        #endregion


        #region Draws a Rounded Rectangle border with integers. 
        public void DrawRoundRectangle(System.Drawing.Pen pen, int x, int y,
          int width, int height, int radius) 
        { 
            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y); 
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height); 
            float fradius = Convert.ToSingle(radius); 
            this.DrawRoundRectangle(pen, fx, fy, fwidth, fheight, fradius); 
        }
        #endregion 


        #region Draws a Rounded Rectangle border with continuous numbers. 
        public void DrawRoundRectangle(System.Drawing.Pen pen, 
          float x, float y,
          float width, float height, float radius) 
        { 
            RectangleF rectangle = new RectangleF(x, y, width, height); 
            GraphicsPath path = this.GetRoundedRect(rectangle, radius); 
            this.Graphics.DrawPath(pen, path); 
        } 
        #endregion 

        #region Get the desired Rounded Rectangle path. 
        public GraphicsPath GetRoundedRect(RectangleF baseRect,float radius) 
        {
            // if corner radius is less than or equal to zero, 

            // return the original rectangle 

            if( radius<=0.0F ) 
            { 
                GraphicsPath mPath = new GraphicsPath(); 
                mPath.AddRectangle(baseRect); 
                mPath.CloseFigure(); 
                return mPath;
            }

            // if the corner radius is greater than or equal to 

            // half the width, or height (whichever is shorter) 

            // then return a capsule instead of a lozenge 

            if( radius>=(Math.Min(baseRect.Width, baseRect.Height))/2.0) 
              return GetCapsule( baseRect ); 

            // create the arc for the rectangle sides and declare 

            // a graphics path object for the drawing 

            float diameter = radius * 2.0F; 
            SizeF sizeF = new SizeF( diameter, diameter );
            RectangleF arc = new RectangleF( baseRect.Location, sizeF ); 
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath(); 

            // top left arc 

            path.AddArc( arc, 180, 90 ); 

            // top right arc 

            arc.X = baseRect.Right-diameter; 
            path.AddArc( arc, 270, 90 ); 

            // bottom right arc 

            arc.Y = baseRect.Bottom-diameter; 
            path.AddArc( arc, 0, 90 ); 

            // bottom left arc

            arc.X = baseRect.Left;     
            path.AddArc( arc, 90, 90 );     

            path.CloseFigure(); 
            return path; 
        } 
        #endregion 

        

#region Gets the desired Capsular path. 
        private GraphicsPath GetCapsule( RectangleF baseRect ) 
        { 
            float diameter; 
            RectangleF arc; 
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath(); 
            try 
            { 
                if( baseRect.Width>baseRect.Height ) 
                { 
                    // return horizontal capsule 

                    diameter = baseRect.Height; 
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF( baseRect.Location, sizeF ); 
                    path.AddArc( arc, 90, 180); 
                    arc.X = baseRect.Right-diameter; 
                    path.AddArc( arc, 270, 180); 
                } 
                else if( baseRect.Width < baseRect.Height ) 
                { 
                    // return vertical capsule 

                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF( baseRect.Location, sizeF ); 
                    path.AddArc( arc, 180, 180 ); 
                    arc.Y = baseRect.Bottom-diameter; 
                    path.AddArc( arc, 0, 180 ); 
                } 
                else
                { 
                    // return circle 

                    path.AddEllipse( baseRect ); 
                }
            } 
            catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
                path.AddEllipse( baseRect ); 
            } 
            finally 
            { 
                path.CloseFigure(); 
            } 
            return path; 
        } 
        #endregion 
    } 
}



