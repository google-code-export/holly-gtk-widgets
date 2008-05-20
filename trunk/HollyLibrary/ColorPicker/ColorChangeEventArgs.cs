// ColorChangeEventArgs.cs created with MonoDevelop
// User: dantes at 2:34 PM 5/20/2008
//

using System;
using Gdk;

namespace HollyLibrary
{
	
	public delegate void ColorChangeEventHandler(object sender, ColorChangeEventArgs args);
	
	public class ColorChangeEventArgs : EventArgs
	{
		Color color;
		
		public ColorChangeEventArgs( Color color )
		{
			this.Color = color;
		}
		
		public Color Color 
		{
			get 
			{
				return color;
			}
			set 
			{
				color = value;
			}
		}
		
	}
}
