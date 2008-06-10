// HColorPickerWidget.cs created with MonoDevelop
// User: dantes at 9:16 PM 6/9/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HColorPickerWidget : Gtk.Bin
	{
		
		public HColorPickerWidget()
		{
			this.Build();
			
			UpdatePreview();
			//check hue by default
			chkH.Active = true;
		}

		protected virtual void OnSliderScroll (object sender, System.EventArgs e)
		{

			ColorWell.RGB = Slider.RGB;
			UpdatePreview();
		}

		protected virtual void OnColorWellScroll (object sender, System.EventArgs e)
		{
			UpdatePreview();
			//hsb
			TxtH.Value = Math.Round(ColorWell.HSL.H * 360);
			TxtS.Value = Math.Round(ColorWell.HSL.S* 100);
			TxtB.Value = Math.Round(ColorWell.HSL.L* 100);
			//rgb
			TxtRed.Value = ColorWell.RGB.R;
			TxtBlue.Value = ColorWell.RGB.B;
			TxtGreen.Value = ColorWell.RGB.G;
			//cmyk
			AdobeColors.CMYK cmyk = AdobeColors.RGB_to_CMYK( ColorWell.RGB );
			TxtC.Value = Math.Round(cmyk.C * 100);
			TxtM.Value = Math.Round(cmyk.M * 100);
			TxtY.Value = Math.Round(cmyk.Y * 100);
			TxtK.Value = Math.Round(cmyk.K * 100);
			//hex
			TxtHexa.Text = GraphUtil.Color2Hex( ColorWell.RGB );
		}
		
		private void UpdatePreview()
		{
			Preview.ModifyBg( Preview.State, GraphUtil.gdkColorFromWinForms( ColorWell.RGB ) );
		}
		
		private void ChangeDrawStyle( eDrawStyle style )
		{
			try
			{
				ColorWell.DrawStyle = style;
				Slider.DrawStyle    = style;
			}
			catch(Exception ex)
			{
				Console.WriteLine( ex.Message );
			}
		}

		protected virtual void OnPreviewExposeEvent (object o, Gtk.ExposeEventArgs args)
		{
			Gtk.Style.PaintBox( Style, args.Event.Window, Preview.State, Gtk.ShadowType.EtchedIn, Preview.Allocation, Preview, "preview", 0, 0, Preview.Allocation.Width, Preview.Allocation.Height );
		}

		protected virtual void OnChkHToggled (object sender, System.EventArgs e)
		{
			
			ChangeDrawStyle( eDrawStyle.Hue );
		}

		protected virtual void OnChkSToggled (object sender, System.EventArgs e)
		{
			ChangeDrawStyle( eDrawStyle.Saturation);
		}

		protected virtual void OnChkBToggled (object sender, System.EventArgs e)
		{
			ChangeDrawStyle( eDrawStyle.Brightness);
		}

		protected virtual void OnChkRToggled (object sender, System.EventArgs e)
		{
			ChangeDrawStyle( eDrawStyle.Red);
		}

		protected virtual void OnChkGToggled (object sender, System.EventArgs e)
		{
			ChangeDrawStyle( eDrawStyle.Green );
		}

		protected virtual void OnCkkBToggled (object sender, System.EventArgs e)
		{
			ChangeDrawStyle(eDrawStyle.Blue);
		}

	
		private void UpdateCMYK()
		{
			ColorRGB = AdobeColors.CMYK_to_RGB( ColorCMYK );
			UpdatePreview();
		}
		
		private void UpdateRGB()
		{
			ColorWell.RGB = ColorRGB;
			Slider.RGB    = ColorRGB;
			UpdatePreview();
		}
		
		private void UpdateHsl()
		{
			ColorWell.HSL = ColorHSL;
			Slider.HSL    = ColorHSL;
			UpdatePreview();
		}
		
		protected virtual void OnTxtHValueChanged (object sender, System.EventArgs e)
		{
			int val = (int)Math.Round(ColorWell.HSL.H * 360 );
			if( val != TxtH.Value ) UpdateHsl();
		}
		protected virtual void OnTxtSValueChanged (object sender, System.EventArgs e)
		{
			int val = (int)Math.Round(ColorWell.HSL.S * 100 );
			if( val != TxtS.Value ) UpdateHsl();
		}

		protected virtual void OnTxtBValueChanged (object sender, System.EventArgs e)
		{
			int val = (int)Math.Round(ColorWell.HSL.L * 100 );
			if( val != TxtB.Value ) UpdateHsl();
		}

		protected virtual void OnTxtRedValueChanged (object sender, System.EventArgs e)
		{
			int val = ColorWell.RGB.R;
			if( val != TxtRed.Value ) UpdateRGB();
		}

		protected virtual void OnTxtGreenValueChanged (object sender, System.EventArgs e)
		{
			int val = ColorWell.RGB.G;
			if( val != TxtGreen.Value ) UpdateRGB();
		}

		protected virtual void OnTxtBlueValueChanged (object sender, System.EventArgs e)
		{
			int val = ColorWell.RGB.B;
			if( val != TxtBlue.Value ) UpdateRGB();
		}

		protected virtual void OnTxtCValueChanged (object sender, System.EventArgs e)
		{
			AdobeColors.CMYK cmyk = AdobeColors.RGB_to_CMYK( ColorWell.RGB );
			int val = (int)Math.Round(cmyk.C * 100 );
			if( val != TxtC.Value ) UpdateCMYK();
		}

		protected virtual void OnTxtMValueChanged (object sender, System.EventArgs e)
		{
			AdobeColors.CMYK cmyk = AdobeColors.RGB_to_CMYK( ColorWell.RGB );
			int val = (int)Math.Round(cmyk.M * 100 );
			if( val != TxtM.Value ) UpdateCMYK();
		}

		protected virtual void OnTxtYValueChanged (object sender, System.EventArgs e)
		{
			AdobeColors.CMYK cmyk = AdobeColors.RGB_to_CMYK( ColorWell.RGB );
			int val = (int)Math.Round(cmyk.Y * 100 );
			if( val != TxtY.Value ) UpdateCMYK();
		}

		protected virtual void OnTxtKValueChanged (object sender, System.EventArgs e)
		{
			AdobeColors.CMYK cmyk = AdobeColors.RGB_to_CMYK( ColorWell.RGB );
			int val = (int)Math.Round(cmyk.K * 100 );
			if( val != TxtK.Value ) UpdateCMYK();
		}

		protected virtual void OnTxtHexaChanged (object sender, System.EventArgs e)
		{
			TxtHexa.Text = TxtHexa.Text.ToUpper();
			if( TxtHexa.IsTextValid )
			{
				try
				{
					this.ColorRGB = GraphUtil.Hex2Color( TxtHexa.Text );
				}
				catch(Exception ex)
				{
					Console.WriteLine( ex.Message );
				}
			}
		}
		
		public AdobeColors.CMYK ColorCMYK
		{
			get
			{
				AdobeColors.CMYK ret = new AdobeColors.CMYK();
				ret.C = (double)TxtC.Value  / 100 ;
				ret.M = (double)TxtM.Value  / 100 ;
				ret.Y = (double)TxtY.Value  / 100 ;
				ret.K = (double)TxtK.Value  / 100 ;
				return ret;
			}
			set
			{
				TxtC.Value = Math.Round( value.C * 100 );
				TxtM.Value = Math.Round( value.M * 100 );
				TxtY.Value = Math.Round( value.Y * 100 );
				TxtK.Value = Math.Round( value.K * 100 );
				UpdateCMYK();
			}
		}
		
		public Gdk.Color ColorGdk
		{
			get
			{
				return GraphUtil.gdkColorFromWinForms( ColorRGB );
			}
			set
			{				
				ColorRGB = System.Drawing.Color.FromArgb( value.Red, value.Green, value.Blue );
			}
		}
		
		public System.Drawing.Color ColorRGB
		{
			get
			{
				
				int r = TxtRed.ValueAsInt;
				int g = TxtGreen.ValueAsInt;
				int b = TxtBlue.ValueAsInt;
				return System.Drawing.Color.FromArgb( r, g, b );
			}
			set
			{
				TxtRed.Value   = value.R;
				TxtGreen.Value = value.G;
				TxtBlue.Value  = value.B;
				UpdateRGB();
			}
		}
		
		public AdobeColors.HSL ColorHSL
		{
			get
			{
				AdobeColors.HSL hsl = new AdobeColors.HSL();
				hsl.H = (double)TxtH.Value  / 360 ;
				hsl.S = (double)TxtS.Value  / 100;
				hsl.L = (double)TxtB.Value  / 100;
				return hsl;
			}
			set
			{
				TxtH.Value = Math.Round( value.H * 360 );
				TxtS.Value = Math.Round( value.S * 100 );
				TxtB.Value = Math.Round( value.L * 100 );
				UpdateHsl();
			}
			
		}

	}
}
