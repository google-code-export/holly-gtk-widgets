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



