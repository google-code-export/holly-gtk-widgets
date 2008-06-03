// FontChooserDialog.cs created with MonoDevelop
// User: dantes at 9:11 PM 5/9/2008
//

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Gtk;

namespace HollyLibrary
{
	
	public class FontEventArgs : EventArgs
	{
		private String font;
		
		public String Font 
		{
			get 
			{
				return font;
			}
		}
		
		public FontEventArgs( String font )
		{
			this.font = font;
		}
	}
	
	public partial class FontChooserDialog : Gtk.Window
	{
		public delegate void FontEventHandler(object sender, FontEventArgs args);
		public event FontEventHandler OnFontChange;
		private static FontChooserDialog instance;
		Dictionary<String, Font> FontStore;
		private HFontPicker father;
		
		public FontChooserDialog( int x, int y, int width, FontEventHandler onFontChange, Dictionary<String, Font> FontStore, HFontPicker father ) : base(Gtk.WindowType.Popup)
		{
			
			
			this.father       = father;
			this.FontStore    = FontStore;
			this.OnFontChange = onFontChange;
			this.Move( x, y );
			this.Resize(width, 200);

			
			this.Build();
			
			//grab dialog
			GrabUtil.GrabWindow(this);
			instance = this;
			TvFonts.GrabFocus();

			while( Gtk.Application.EventsPending() )
				Gtk.Application.RunIteration();
			
			Thread t = new Thread( new ThreadStart( AddFontsToList ) );
			t.Start();
			
		}
		
		public HSimpleList TreeviewFonts
		{
			get
			{
				
				return TvFonts;
			}
		}
		
		public string SelectedFont
		{
			set
			{
				TvFonts.SelectedItem = value;
			}
		}
		
	
		private void AddFontsToList()
		{
			
			foreach( KeyValuePair<string,Font> pair in FontStore )
			{
				String font_name = pair.Key.ToString();
				Gdk.Threads.Enter();
				TvFonts.Items.Add( font_name );
				Gdk.Threads.Leave();
			}
		}

		public static void ShowMe( int x, int y, int width, FontEventHandler OnFontChange, Dictionary<String, Font> FontStore, String font_name, HFontPicker father)
		{
			if( instance == null )
				new FontChooserDialog( x, y, width, OnFontChange, FontStore, father);
			else
				ShowInstance(x, y, width, OnFontChange, FontStore, font_name, father );
		}
		
		private static void ShowInstance(int x, int y, int width, FontEventHandler OnFontChange, Dictionary<String, Font> FontStore, String SelectedFont, HFontPicker father )
		{
			instance.FontStore = FontStore;
			instance.Move( x,y );
			instance.Resize(width, instance.Allocation.Height);
			instance.OnFontChange  = null;
			instance.OnFontChange += OnFontChange;
			instance.Show();
			instance.QueueResize();
			instance.SizeRequest();
			instance.TreeviewFonts.GrabFocus();
			//selecteaza fontul ales
			instance.SelectedFont = SelectedFont;
			GrabUtil.GrabWindow(instance);			
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
			SelectCurrentFont();
			GrabUtil.RemoveGrab(this);
			father.FocusOnEntry();
			this.Hide();
		}

		private void SelectCurrentFont()
		{
			if( TvFonts.SelectedIndex >= 0 )
			{
				String font                     = TvFonts.Text;
				if( this.OnFontChange != null ) 
					OnFontChange( this, new FontEventArgs( font ) );
			}
		}
		
		protected virtual void OnTvFontsButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			args.RetVal = true;
		}

		protected virtual void OnTvFontsCursorChanged (object sender, System.EventArgs e)
		{
			SelectCurrentFont();
		}

		protected virtual void OnTvFontsRowActivated (object o, Gtk.RowActivatedArgs args)
		{
			Close();
		}

		protected virtual void OnTvFontsSelectedIndexChanged (object sender, System.EventArgs e)
		{
			this.SelectCurrentFont();
		}
		

		protected virtual void OnTvFontsDrawItem (object sender, HollyLibrary.DrawItemEventArgs args)
		{
			Graphics g           = args.Graphics;
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
			String font_name     = TvFonts.Items[ args.ItemIndex ].ToString();
			Font font            = new System.Drawing.Font( font_name, 10F );
			Color color          = System.Drawing.Color.Black;
			SolidBrush b         = new System.Drawing.SolidBrush( color );
			g.DrawString( font_name, font, b, args.CellArea.X, args.CellArea.Y );
		}

		protected virtual void OnTvFontsMeasureItem (object sender, HollyLibrary.MeasureItemEventArgs args)
		{
			args.ItemHeight = 25;
		}


		
	}
}
