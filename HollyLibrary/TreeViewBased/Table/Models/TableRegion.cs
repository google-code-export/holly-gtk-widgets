// TableRegion.cs created with MonoDevelop
// User: dantes at 9:11 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Specifies the part of the Table the user has clicked
	/// </summary>
	public enum TableRegion
	{
		/// <summary>
		/// A cell in the Table
		/// </summary>
		Cells = 1,

		/// <summary>
		/// A column header in the Table
		/// </summary>
		ColumnHeader = 2,

		/// <summary>
		/// The non-client area of a Table, such as the border
		/// </summary>
		NonClientArea = 3,

		/// <summary>
		/// The click occured outside ot the Table
		/// </summary>
		NoWhere = 4
	}
}
