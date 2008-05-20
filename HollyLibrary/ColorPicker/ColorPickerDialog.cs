// ColorPickerDialog.cs created with MonoDevelop
// User: dantes at 1:33 PM 5/20/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class ColorPickerDialog : Gtk.Window
	{
		
		public ColorPickerDialog() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
