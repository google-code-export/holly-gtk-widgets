// SelectionStyle.cs created with MonoDevelop
// User: dantes at 9:11 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Specifies how selected Cells are drawn by a Table
	/// </summary>
	public enum SelectionStyle
	{
		/// <summary>
		/// The first visible Cell in the selected Cells Row is drawn as selected
		/// </summary>
		ListView = 0,

		/// <summary>
		/// The selected Cells are drawn as selected
		/// </summary>
		Grid = 1
	}
}
