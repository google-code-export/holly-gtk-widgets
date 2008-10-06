// TableState.cs created with MonoDevelop
// User: dantes at 9:14 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Specifies the current state of the Table
	/// </summary>
	public enum TableState
	{
		/// <summary>
		/// The Table is in its normal state
		/// </summary>
		Normal = 0,

		/// <summary>
		/// The Table is selecting a Column
		/// </summary>
		ColumnSelecting = 1,

		/// <summary>
		/// The Table is resizing a Column
		/// </summary>
		ColumnResizing = 2,

		/// <summary>
		/// The Table is editing a Cell
		/// </summary>
		Editing = 3, 

		/// <summary>
		/// The Table is sorting a Column
		/// </summary>
		Sorting = 4, 

		/// <summary>
		/// The Table is selecting Cells
		/// </summary>
		Selecting = 5
	}
}
