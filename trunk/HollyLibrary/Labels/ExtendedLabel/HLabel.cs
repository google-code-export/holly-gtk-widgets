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
		bool textOweritesIcon  = false;
		
		public HLabel()
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
			if( !TextOweritesIcon )
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
			
			//draw text
			if( this.Text.Length > 0 )
			{
				float x      = rect.Left, y = rect.Top;
				//calculate text size
				float fnt_sz = this.PangoContext.FontDescription.Size / 1000;
				
				Font fnt   = new Font       ( this.PangoContext.FontDescription.Family , fnt_sz );
				SizeF ts   = g.MeasureString( this.Text, fnt );
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
				
				g.DrawString( this.Text, fnt, Brushes.Black, x, y );
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
				this.QueueDraw();
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
				this.QueueDraw();
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
				this.QueueDraw();
			}
		}

		public bool TextOweritesIcon 
		{
			get 
			{
				return textOweritesIcon;
			}
			set 
			{
				textOweritesIcon = value;
			}
		}

		protected override void OnSizeRequested (ref Requisition requisition)
		{
			//set minimum size
			float min_width  = 5;
			float min_height = 5;
			
			try
			{
				Graphics g   = Gtk.DotNet.Graphics.FromDrawable( this.GdkWindow );
				float fnt_sz = this.PangoContext.FontDescription.Size / 1000;
				Font fnt     = new Font       ( this.PangoContext.FontDescription.Family , fnt_sz );
				SizeF ts     = g.MeasureString( this.Text, fnt );
				
				if( Icon != null  )
				{
					min_width  = Icon.Width + ts.Width ;
					min_height = Math.Max( Icon.Height, ts.Height );
				}
				else
				{
					min_width  = ts.Width ;
					min_height = ts.Height;
				}
			}
			catch( Exception ex)
			{
				Console.WriteLine( ex.Message + ex.StackTrace );
			}
			requisition.Width  = (int)min_width;
			requisition.Height = (int)min_height;
			Console.WriteLine( requisition );
			base.OnSizeRequested (ref requisition);
		}


	}
}

