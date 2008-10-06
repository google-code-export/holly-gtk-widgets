// ICellRenderer.cs created with MonoDevelop
// User: dantes at 3:28 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Exposes common methods provided by Cell renderers
	/// </summary>
	public interface ICellRenderer : IRenderer
	{
		/// <summary>
		/// Raises the PaintCell event
		/// </summary>
		/// <param name="e">A PaintCellEventArgs that contains the event data</param>
		void OnPaintCell(PaintCellEventArgs e);


		/// <summary>
		/// Raises the GotFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		void OnGotFocus(CellFocusEventArgs e);


		/// <summary>
		/// Raises the LostFocus event
		/// </summary>
		/// <param name="e">A CellFocusEventArgs that contains the event data</param>
		void OnLostFocus(CellFocusEventArgs e);


		/// <summary>
		/// Raises the KeyDown event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		void OnKeyDown(CellKeyEventArgs e);


		/// <summary>
		/// Raises the KeyUp event
		/// </summary>
		/// <param name="e">A CellKeyEventArgs that contains the event data</param>
		void OnKeyUp(CellKeyEventArgs e);


		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseEnter(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseLeave(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseUp event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseUp(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseDown event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseDown(CellMouseEventArgs e);


		/// <summary>
		/// Raises the MouseMove event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnMouseMove(CellMouseEventArgs e);


		/// <summary>
		/// Raises the Click event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnClick(CellMouseEventArgs e);


		/// <summary>
		/// Raises the DoubleClick event
		/// </summary>
		/// <param name="e">A CellMouseEventArgs that contains the event data</param>
		void OnDoubleClick(CellMouseEventArgs e);
	}
}
