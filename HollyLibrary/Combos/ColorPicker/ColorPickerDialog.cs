// ColorPickerDialog.cs created with MonoDevelop
// User: dantes at 1:33 PM 5/20/2008
//

using System;
using System.Collections.Generic;
using Gdk;

namespace HollyLibrary
{
	
	
	public partial class ColorPickerDialog : Gtk.Window
	{
		List<Color> colors = new List<Color>();
		HColorPicker parent;
		
		public ColorPickerDialog( int x, int y, HColorPicker parent ) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.parent       = parent;
			this.AppPaintable = true;
			Move  ( x - 180 , y   );
			Resize( 120     , 120 );
			this.Build();
			addColors();
			//add buttons

			int cols  = 8;
			uint left = 0;
			uint top  = 0;

			foreach( Color c in colors )
			{
			
				if( left == cols )
				{
					left = 0;
					top++;
				}
				//color button
				ColorButton btn   = new ColorButton( c );
				btn.BorderWidth   = 1;
				btn.HeightRequest = 20;
				btn.WidthRequest  = 20;
				//
				btn.ButtonPressEvent += new Gtk.ButtonPressEventHandler( OnColorChange );
				TblColors.Attach( btn, left, left + 1, top, top + 1 );
				left++;
			}
			ShowAll();
			GrabUtil.GrabWindow(this);
		}
		
		private void OnColorChange( object sender, EventArgs args )
		{
			ColorButton btn = (ColorButton) sender;
			parent.Color    = btn.Style.Background( Gtk.StateType.Normal );
			Close();
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			base.OnExposeEvent (args);
			
			int winWidth, winHeight;
			this.GetSize ( out winWidth, out winHeight );
			this.GdkWindow.DrawRectangle ( this.Style.ForegroundGC ( Gtk.StateType.Insensitive ), false, 0, 0, winWidth - 1, winHeight - 1 );
			
			return false;
		}
		
		private void Close()
		{
			GrabUtil.RemoveGrab(this);
			Destroy();
		}
		
		public static void ShowMe( int x, int y, HColorPicker parent)
		{
			 new ColorPickerDialog( x, y, parent );
		}
		
		protected virtual void OnButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			Close();
		}

		protected virtual void OnBtnMoreColorsClicked (object sender, System.EventArgs e)
		{
			HColorPickerDialog dlg = new HColorPickerDialog();
			if (dlg.Run () == (int) Gtk.ResponseType.Ok)
			{
				parent.Color = dlg.ColorGdk;
			}
			dlg.Hide();
			Close();
		}

		#region addColors method
		private void addColors()
		{
			//add colors to list
			colors.Add( new Color(0  ,0  ,0  ) );
			colors.Add( new Color(128,0  ,0  ) );
			colors.Add( new Color(255,0  ,0  ) );
			colors.Add( new Color(255,0  ,255) );
			colors.Add( new Color(255,153,204) );
			colors.Add( new Color(153,51 ,0  ) );
			colors.Add( new Color(255,102,0  ) );
			colors.Add( new Color(255,153,0  ) );
			colors.Add( new Color(255,204,0  ) );
			colors.Add( new Color(255,204,153) );
			colors.Add( new Color(51 ,51 ,0  ) );
			colors.Add( new Color(128,128,0  ) );
			colors.Add( new Color(153,204,0  ) );
			colors.Add( new Color(255,255,0  ) );
			colors.Add( new Color(255,255,153) );
			colors.Add( new Color(0  ,51 ,0  ) );
			colors.Add( new Color(0  ,128,0  ) );
			colors.Add( new Color(51 ,153,102) );
			colors.Add( new Color(0  ,255,0  ) );
			colors.Add( new Color(204,255,204) );
			
			colors.Add( new Color(0  ,51 ,102) );
			colors.Add( new Color(128,0  ,0  ) );
			colors.Add( new Color(51 ,204,204) );
			colors.Add( new Color(0  ,255,255) );
			colors.Add( new Color(204,255,255) );
			colors.Add( new Color(0  ,0  ,128) );
			colors.Add( new Color(0  ,0  ,225) );
			colors.Add( new Color(51 ,102,255) );
			colors.Add( new Color(0  ,204,255) );
			colors.Add( new Color(153,204,255) );
			colors.Add( new Color(51 ,51 ,153) );
			colors.Add( new Color(102,102,153) );
			colors.Add( new Color(128,0  ,128) );
			colors.Add( new Color(153,51 ,102) );
			colors.Add( new Color(204,153,255) );
			colors.Add( new Color(51 ,51 ,51 ) );
			colors.Add( new Color(128,128,128) );
			colors.Add( new Color(153,153,153) );
			colors.Add( new Color(192,192,192) );
			colors.Add( new Color(255,255,255) );
		}
#endregion
	}
}
