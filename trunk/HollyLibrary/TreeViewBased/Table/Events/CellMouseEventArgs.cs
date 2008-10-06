// CellMouseEventArgs.cs created with MonoDevelop
// User: dantes at 4:03 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gtk;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the CellMouseEnter, CellMouseLeave, 
	/// CellMouseDown, CellMouseUp, CellMouseMove and CellMouseHover events of a Table
	/// </summary>
	public delegate void CellMouseEventHandler(object sender, CellMouseEventArgs e);

	#endregion



	#region CellMouseEventArgs
	
	/// <summary>
	/// Provides data for the CellMouseEnter, CellMouseLeave, CellMouseDown, 
	/// CellMouseUp and CellMouseMove events of a Table
	/// </summary>
	public class CellMouseEventArgs : MotionNotifyEventArgs
	{
		#region Class Data

		
		/// <summary>
		/// The Cell that raised the event
		/// </summary>
		private Cell cell;
		
		/// <summary>
		/// The Table the Cell belongs to
		/// </summary>
		private HTable table;
		
		/// <summary>
		/// The Row index of the Cell
		/// </summary>
		private int row;
		
		/// <summary>
		/// The Column index of the Cell
		/// </summary>
		private int column;
		
		/// <summary>
		/// The Cells bounding rectangle
		/// </summary>
		private Gdk.Rectangle cellRect;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellMouseEventArgs class with 
		/// the specified source Cell, table, row index, column index and 
		/// cell bounds
		/// </summary>
		/// <param name="cell">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="cellPos"></param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
		public CellMouseEventArgs(Cell cell, HTable table, CellPos cellPos, Gdk.Rectangle cellRect) 
		{
			this.cell = cell;
			this.table = table;
			this.row = cellPos.Row;
			this.column = cellPos.Column;
			this.cellRect = cellRect;
		}  
		
		
		/// <summary>
		/// Initializes a new instance of the CellMouseEventArgs class with 
		/// the specified source Cell, table, row index, column index and 
		/// cell bounds
		/// </summary>
		/// <param name="cell">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
		public CellMouseEventArgs(Cell cell, HTable table, int row, int column, Gdk.Rectangle cellRect) 
		{
			this.cell = cell;
			this.table = table;
			this.row = row;
			this.column = column;
			this.cellRect = cellRect;
		}  

		
		/// <summary>
		/// Initializes a new instance of the CellMouseEventArgs class with 
		/// the specified source Cell, table, row index, column index, cell 
		/// bounds and MouseEventArgs
		/// </summary>
		/// <param name="cell">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
		/// <param name="mea">The MouseEventArgs that contains data about the 
		/// mouse event</param>
		public CellMouseEventArgs(Cell cell, HTable table, int row, int column, Gdk.Rectangle cellRect, MotionNotifyEventArgs mea) 
		{
			this.cell = cell;
			this.table = table;
			this.row = row;
			this.column = column;
			this.cellRect = cellRect;
		}  
		
		
		/// <summary>
		/// Initializes a new instance of the CellMouseEventArgs class with 
		/// the specified source Cell, table, row index, column index and 
		/// cell bounds
		/// </summary>
		/// <param name="cell">The Cell that Raised the event</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="cellPos"></param>
		/// <param name="cellRect">The Cell's bounding rectangle</param>
		/// <param name="mea"></param>
		public CellMouseEventArgs(Cell cell, HTable table, CellPos cellPos, Gdk.Rectangle cellRect, Gtk.MotionNotifyEventArgs mea) 
		{
			this.cell = cell;
			this.table = table;
			this.row = cellPos.Row;
			this.column = cellPos.Column;
			this.cellRect = cellRect;
		}  

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Cell that Raised the event
		/// </summary>
		public Cell Cell
		{
			get
			{
				return this.cell;
			}
		}


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
		/// Gets the Row index of the Cell
		/// </summary>
		public int Row
		{
			get
			{
				return this.row;
			}
		}


		/// <summary>
		/// Gets the Column index of the Cell
		/// </summary>
		public int Column
		{
			get
			{
				return this.column;
			}
		}


		/// <summary>
		/// Gets the Cells bounding rectangle
		/// </summary>
		public Gdk.Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}


		/// <summary>
		/// Gets the position of the Cell
		/// </summary>
		public CellPos CellPos
		{
			get
			{
				return new CellPos(this.Row, this.Column);
			}
		}

		#endregion
	}

	#endregion
}
