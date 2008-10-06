// CellEventTyoe.cs created with MonoDevelop
// User: dantes at 3:34 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Specifies the type of event generated when the value of a 
	/// Cell's property changes
	/// </summary>
	public enum CellEventType
	{
		/// <summary>
		/// Occurs when the Cell's property change type is unknown
		/// </summary>
		Unknown,

		/// <summary>
		/// Occurs when the value displayed by a Cell has changed
		/// </summary>
		ValueChanged,

		/// <summary>
		/// Occurs when the value of a Cell's Font property changes
		/// </summary>
		FontChanged,

		/// <summary>
		/// Occurs when the value of a Cell's BackColor property changes
		/// </summary>
		BackColorChanged,

		/// <summary>
		/// Occurs when the value of a Cell's ForeColor property changes
		/// </summary>
		ForeColorChanged,

		/// <summary>
		/// Occurs when the value of a Cell's CellStyle property changes
		/// </summary>
		StyleChanged,

		/// <summary>
		/// Occurs when the value of a Cell's Padding property changes
		/// </summary>
		PaddingChanged,

		/// <summary>
		/// Occurs when the value of a Cell's Editable property changes
		/// </summary>
		EditableChanged,

		/// <summary>
		/// Occurs when the value of a Cell's Enabled property changes
		/// </summary>
		EnabledChanged,

		/// <summary>
		/// Occurs when the value of a Cell's ToolTipText property changes
		/// </summary>
		ToolTipTextChanged,

		/// <summary>
		/// Occurs when the value of a Cell's CheckState property changes
		/// </summary>
		CheckStateChanged,

		/// <summary>
		/// Occurs when the value of a Cell's ThreeState property changes
		/// </summary>
		ThreeStateChanged,

		/// <summary>
		/// Occurs when the value of a Cell's Image property changes
		/// </summary>
		ImageChanged,

		/// <summary>
		/// Occurs when the value of a Cell's ImageSizeMode property changes
		/// </summary>
		ImageSizeModeChanged
	}
}
