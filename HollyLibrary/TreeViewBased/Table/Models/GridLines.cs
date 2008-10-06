// GridLines.cs created with MonoDevelop
// User: dantes at 4:55 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Specifies how a Table draws grid lines between its rows and columns
	/// </summary>
	public enum GridLines
	{
		/// <summary>
		/// No grid lines are drawn
		/// </summary>
		None = 0,

		/// <summary>
		/// Grid lines are only drawn between columns
		/// </summary>
		Columns = 1,

		/// <summary>
		/// Grid lines are only drawn between rows
		/// </summary>
		Rows = 2,

		/// <summary>
		/// Grid lines are drawn between rows and columns
		/// </summary>
		Both = 3
	}
}
