// GridLineStyle.cs created with MonoDevelop
// User: dantes at 4:56 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary
{
	
	
	/// <summary>
	/// Specifies the style of the lines drawn when a Table draws its grid lines
	/// </summary>
	public enum GridLineStyle
	{
		/// <summary>
		/// Specifies a solid line
		/// </summary>
		Solid = 0,

		/// <summary>
		/// Specifies a line consisting of dashes
		/// </summary>
		Dash = 1,

		/// <summary>
		/// Specifies a line consisting of dots
		/// </summary>
		Dot = 2,

		/// <summary>
		/// Specifies a line consisting of a repeating pattern of dash-dot
		/// </summary>
		DashDot = 3,

		/// <summary>
		/// Specifies a line consisting of a repeating pattern of dash-dot-dot
		/// </summary>
		DashDotDot = 4
	}
}
