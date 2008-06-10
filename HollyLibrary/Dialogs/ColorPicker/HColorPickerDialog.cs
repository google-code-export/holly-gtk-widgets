// HColorPickerDialog.cs created with MonoDevelop
// User: dantes at 2:03 PM 6/10/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HColorPickerDialog : Gtk.Dialog
	{
		
		public HColorPickerDialog()
		{
			this.Build();
		}
		
        #region color widget wrapper properties
		public String ColorHexa
		{
			get
			{
				return Picker.ColorHexa;
			}
			set
			{
				Picker.ColorHexa = value;
			}
		}
		
		public GraphUtil.CMYK ColorCMYK
		{
			get
			{
				return Picker.ColorCMYK;
			}
			set
			{
				Picker.ColorCMYK = value;
			}
		}
		
		public Gdk.Color ColorGdk
		{
			get
			{
				return Picker.ColorGdk;
			}
			set
			{
				Picker.ColorGdk = value;
			}
		}
		
		public System.Drawing.Color ColorRGB
		{
			get
			{
				return Picker.ColorRGB;
			}
			set
			{
				Picker.ColorRGB = value;
			}
		}
		
		public GraphUtil.HSL ColorHSL
		{
			get
			{
				return Picker.ColorHSL;
			}
			set
			{
				Picker.ColorHSL = value;
			}	
		}
#endregion

	}
}
