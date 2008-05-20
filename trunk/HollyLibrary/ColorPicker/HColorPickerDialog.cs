// HColorPickerDialog.cs created with MonoDevelop
// User: dantes at 1:32 PM 5/20/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class HColorPickerDialog : Gtk.Window
	{
		
		public HColorPickerDialog() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
