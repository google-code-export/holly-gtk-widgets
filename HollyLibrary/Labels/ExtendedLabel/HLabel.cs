// HLabel.cs created with MonoDevelop
// User: dantes at 2:20 PM 9/15/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Drawing;
using Gtk;

namespace HollyLibrary
{
	
	public enum HPosition
	{
		Top, Bottom, Left, Right, TopLeft, TopRight, BottomLeft, BottomRight, Center
	}
	
	public class HLabel : Label
	{
		Gdk.Pixbuf icon;
		HPosition iconPosition = HPosition.Left;
		HPosition textPosition = HPosition.Left;
		bool textOverwritesIcon  = false;
		bool horizontalLine    = false;
		int lineTextPadding    = 6;
		
		
		public HLabel() 
		{
		}
		
		public HLabel( String str ) : base( str )
		{
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Graphics g  = Gtk.DotNet.Graphics.FromDrawable( evnt.Window );
			Bitmap img  = null;
			int wwidth  = this.Allocation.Width;
			int wheight = this.Allocation.Height;
			if( this.Icon != null ) img = GraphUtil.BitmapFromPixbuf( this.Icon   );
			//draw icon
			if( this.Icon != null )
			{
				int x = 0, y = 0;
				
				
				//calculate x and y for icon
				if( IconPosition == HPosition.Bottom )
				{
					x = ( wwidth / 2 ) - ( img.Width / 2 );
					y = wheight - img.Height;
				}
				if( IconPosition == HPosition.BottomLeft )
				{
					x = 0;
					y = wheight - img.Height;
				}
				if( IconPosition == HPosition.BottomRight )
				{
					x = wwidth  - img.Width ;
					y = wheight - img.Height;
				}
				if( IconPosition == HPosition.Center )
				{
					x = ( wwidth  / 2 ) - ( img.Width  / 2 );
					y = ( wheight / 2 ) - ( img.Height / 2 );
				}
				if( IconPosition == HPosition.Left)
				{
					x = 0;
					y = ( wheight / 2 ) - ( img.Height / 2 );
				}
				if( IconPosition == HPosition.Right )
				{
					x = wwidth   -  img.Width;
					y = ( wheight / 2 ) - ( img.Height / 2 );
				}
				if( IconPosition == HPosition.Top )
				{
					x = ( wwidth  / 2 ) - ( img.Width  / 2 );
					y = 0;
				}
				if( IconPosition == HPosition.TopLeft )
				{
					x = 0;
					y = 0;
				}
				if( IconPosition == HPosition.TopRight )
				{
					x = wwidth   - img.Width;
					y = 0;
				}
				
				g.DrawImageUnscaled( img, x, y, img.Width, img.Height );
			}
			//
			//calculate text rectangle
			Rectangle rect = new Rectangle( 0, 0, wwidth, wheight );
			int imgwidth   = 0, imgheight = 0;
			if( Icon != null ) 
			{
				imgwidth  = img.Width;
				imgheight = img.Height;
			}
			if( !TextOverwritesIcon )
			{
				if( IconPosition == HPosition.Bottom      ) rect = new Rectangle( 0       , 0        , wwidth            , wheight - imgheight  );
				if( IconPosition == HPosition.BottomLeft  ) rect = new Rectangle( imgwidth, 0        , wwidth            , wheight - imgheight  );
				if( IconPosition == HPosition.BottomRight ) rect = new Rectangle( 0       , 0        , wwidth - imgwidth , wheight - imgheight  );
				if( IconPosition == HPosition.Left        ) rect = new Rectangle( imgwidth, 0        , wwidth            , wheight - imgheight  );
				
				if( IconPosition == HPosition.Right       ) rect = new Rectangle( 0       , 0        , wwidth - imgwidth , wheight              );
				if( IconPosition == HPosition.Top         ) rect = new Rectangle( 0       , imgheight, wwidth            , wheight  - imgheight );
				if( IconPosition == HPosition.TopLeft     ) rect = new Rectangle( imgwidth, imgheight, wwidth - imgwidth , wheight  - imgheight );
				if( IconPosition == HPosition.TopRight    ) rect = new Rectangle( 0       , imgheight, wwidth - imgwidth , wheight  - imgheight );
			}
			
			
			//calculate text size
			float fnt_sz = this.PangoContext.FontDescription.Size / 1000;
			
			Font fnt   = new Font       ( this.PangoContext.FontDescription.Family , fnt_sz );
			SizeF ts   = g.MeasureString( this.Text, fnt );
			//draw text
			if( this.Text.Length > 0 )
			{
				float x      = rect.Left, y = rect.Top;
				
				//calculate x and y for text
				if( TextPosition == HPosition.Bottom )
				{
					x = ( wwidth / 2 ) - ( ts.Width / 2 );
					y = wheight        - ts.Height;
				}
				if( TextPosition == HPosition.BottomLeft )
				{
					x = rect.Left;
					y = wheight - ts.Height;
				}
				if( TextPosition == HPosition.BottomRight )
				{
					x = wwidth  - ts.Width ;
					y = wheight - ts.Height;
				}
				if( TextPosition == HPosition.Center )
				{
					x = ( wwidth  / 2 ) - ( ts.Width  / 2 );
					y = ( wheight / 2 ) - ( ts.Height / 2 );
				}
				if( TextPosition == HPosition.Left)
				{
					x = rect.Left;
					y = ( wheight / 2 ) - ( ts.Height / 2 );
				}
				if( TextPosition == HPosition.Right )
				{
					x = wwidth   -  ts.Width;
					y = ( wheight / 2 ) - ( ts.Height / 2 );
				}
				if( TextPosition == HPosition.Top )
				{
					x = ( wwidth  / 2 ) - ( ts.Width  / 2 );
					y = rect.Top;
				}
				if( TextPosition == HPosition.TopLeft )
				{
					x = rect.Left;
					y = rect.Top;
				}
				if( TextPosition == HPosition.TopRight )
				{
					x = wwidth   - ts.Width;
					y = rect.Top;
				}
				//draw text
				Gdk.Color c  = this.Style.Text( this.State );
				SolidBrush b = new SolidBrush( GraphUtil.winFormsColorFromGdk( c ) );
				g.DrawString( this.Text, fnt, b, x, y );
			}
			//draw line if horizonatlLine is true
			if( this.HorizontalLine )
			{
				int x1  = rect.Left  + (int)ts.Width + lineTextPadding;
				int y   = Allocation.Height / 2;
				int x2  = Allocation.Width - lineTextPadding;
				if( x2 > x1 )
					Gtk.Style.PaintHline( this.Style, GdkWindow, this.State, this.Allocation, this, "hline", x1, x2, y );
			}
			return true;
		}
		
		public Gdk.Pixbuf Icon 
		{
			get 
			{
				return icon;
			}
			set 
			{
				icon = value;
				this.QueueResize();
			}
		}

		public HPosition TextPosition 
		{
			get 
			{
				return textPosition;
			}
			set 
			{
				textPosition = value;
				this.QueueResize();
			}
		}

		public HPosition IconPosition 
		{
			get 
			{
				return iconPosition;
			}
			set 
			{
				iconPosition = value;
				this.QueueResize();
			}
		}

		public bool TextOverwritesIcon 
		{
			get 
			{
				return textOverwritesIcon;
			}
			set 
			{
				textOverwritesIcon = value;
			}
		}

		public bool HorizontalLine 
		{
			get 
			{
				return horizontalLine;
			}
			set 
			{
				horizontalLine = value;
			}
		}

		
		
		private Size MeasureString( Pango.FontDescription font, string s) 
		{
			Pango.Layout ly = this.Layout;
			ly.SetText(s);
			
			ly.FontDescription = font;
			int width, height;
			ly.GetSize( out width, out height );
			return new Size( (int)(width/1024.0f), (int)(height/1024.0f) );		
		}

		private Size getMininumSize()
		{
			int width = 5, height = 5;

			Size ts     = MeasureString( this.PangoContext.FontDescription, this.Text );
			Console.WriteLine( ts );
			if( Icon != null  )
			{
				width  = Icon.Width + ts.Width ;
				height = Math.Max( Icon.Height, ts.Height );
			}
			else
			{
				width  = ts.Width ;
				height = ts.Height;
			}
			
			return new Size( width, height );
		}


		
		protected override void OnSizeRequested (ref Requisition requisition)
		{
			//set minimum size
			Size size          = getMininumSize();
			requisition.Width  = size.Width;
			requisition.Height = size.Height;
			//
			base.OnSizeRequested (ref requisition);
		}


	}
}

