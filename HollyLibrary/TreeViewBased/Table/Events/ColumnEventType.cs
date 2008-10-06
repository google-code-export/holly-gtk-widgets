// ColumnEventType.cs created with MonoDevelop
// User: dantes at 3:47 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary
{
	
	
	/// <summary>
	/// Specifies the type of event generated when the value of a 
	/// Column's property changes
	/// </summary>
	public enum ColumnEventType
	{
		/// <summary>
		/// Occurs when the Column's property change type is unknown
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Occurs when the value of a Column's Text property changes
		/// </summary>
		TextChanged = 1,

		/// <summary>
		/// Occurs when the value of a Column's Alignment property changes
		/// </summary>
		AlignmentChanged = 2,

		/// <summary>
		/// Occurs when the value of a Column's HeaderAlignment property changes
		/// </summary>
		HeaderAlignmentChanged = 3,

		/// <summary>
		/// Occurs when the value of a Column's Width property changes
		/// </summary>
		WidthChanged = 4,

		/// <summary>
		/// Occurs when the value of a Column's Visible property changes
		/// </summary>
		VisibleChanged = 5,

		/// <summary>
		/// Occurs when the value of a Column's Image property changes
		/// </summary>
		ImageChanged = 6,

		/// <summary>
		/// Occurs when the value of a Column's Format property changes
		/// </summary>
		FormatChanged = 7,

		/// <summary>
		/// Occurs when the value of a Column's ColumnState property changes
		/// </summary>
		StateChanged = 8,

		/// <summary>
		/// Occurs when the value of a Column's Renderer property changes
		/// </summary>
		RendererChanged = 9,

		/// <summary>
		/// Occurs when the value of a Column's Editor property changes
		/// </summary>
		EditorChanged = 10, 

		/// <summary>
		/// Occurs when the value of a Column's Comparer property changes
		/// </summary>
		ComparerChanged = 11, 

		/// <summary>
		/// Occurs when the value of a Column's Enabled property changes
		/// </summary>
		EnabledChanged = 12,

		/// <summary>
		/// Occurs when the value of a Column's Editable property changes
		/// </summary>
		EditableChanged = 13,

		/// <summary>
		/// Occurs when the value of a Column's Selectable property changes
		/// </summary>
		SelectableChanged = 14,

		/// <summary>
		/// Occurs when the value of a Column's Sortable property changes
		/// </summary>
		SortableChanged = 15,

		/// <summary>
		/// Occurs when the value of a Column's SortOrder property changes
		/// </summary>
		SortOrderChanged = 16,

		/// <summary>
		/// Occurs when the value of a Column's ToolTipText property changes
		/// </summary>
		ToolTipTextChanged = 17,

		/// <summary>
		/// Occurs when a Column is being sorted
		/// </summary>
		Sorting = 18
	}
}
