// CellFocusEventArgs.cs created with MonoDevelop
// User: dantes at 3:59 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gdk;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the CellGotFocus and CellLostFocus 
	/// events of a Table
	/// </summary>
	public delegate void CellFocusEventHandler(object sender, CellFocusEventArgs e);

	#endregion



	#region CellFocusEventArgs
	
	/// <summary>
	/// Provides data for the CellGotFocus and CellLostFocus events of a Table
	/// </summary>
	public class CellFocusEventArgs : CellEventArgsBase
	{
		#region Class Data

		/// <summary>
		/// The Table the Cell belongs to
		/// </summary>
		private HTable table;
		
		/// <summary>
		/// The Cells bounding rectangle
		/// </summary>
		private Rectangle cellRect;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellFocusEventArgs class with 
		/// the specified source Cell, table, row index, column index and 
		/// cell bounds
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
		public CellFocusEventArgs(Cell source, HTable table, int row, int column, Rectangle cellRect) : base(source, column, row)
		{
			this.table = table;
			this.cellRect = cellRect;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Table the Cell belongs to
		/// </summary>
		public HTable Table
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
		/// Gets the Cell's bounding rectangle
		/// </summary>
		public Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}

		#endregion
	}

	#endregion
}
