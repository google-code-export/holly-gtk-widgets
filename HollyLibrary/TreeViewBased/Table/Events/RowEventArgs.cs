// RowEventArgs.cs created with MonoDevelop
// User: dantes at 3:47 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the PropertyChanged, CellAdded and 
	/// CellRemoved events of a Row
	/// </summary>
	public delegate void RowEventHandler(object sender, RowEventArgs e);

	#endregion



	#region RowEventArgs
	
	/// <summary>
	/// Provides data for a Row's PropertyChanged, CellAdded 
	/// and CellRemoved events
	/// </summary>
	public class RowEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The Row that Raised the event
		/// </summary>
		private Row source;

		/// <summary>
		/// The index of the Row
		/// </summary>
		private int rowIndex;

		/// <summary>
		/// The affected Cell
		/// </summary>
		private Cell cell;

		/// <summary>
		/// The start index of the affected Cell(s)
		/// </summary>
		private int cellToIndex;

		/// <summary>
		/// The end index of the affected Cell(s)
		/// </summary>
		private int cellFromIndex;

		/// <summary>
		/// The type of event
		/// </summary>
		private RowEventType eventType;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the RowEventArgs class with 
		/// the specified Row source, row index, start index, end index 
		/// and affected Cell
		/// </summary>
		/// <param name="source">The Row that originated the event</param>
		/// <param name="eventType">The type of event</param>
		public RowEventArgs(Row source, RowEventType eventType) : this(source, -1, null, -1, -1, eventType)
		{
			
		}


		/// <summary>
		/// Initializes a new instance of the RowEventArgs class with 
		/// the specified Row source, row index, start index, end index 
		/// and affected Cell
		/// </summary>
		/// <param name="source">The Row that originated the event</param>
		/// <param name="cell">The affected Cell</param>
		/// <param name="cellFromIndex">The start index of the affected Cell(s)</param>
		/// <param name="cellToIndex">The end index of the affected Cell(s)</param>
		public RowEventArgs(Row source, Cell cell, int cellFromIndex, int cellToIndex) : this(source, -1, cell, cellFromIndex, cellToIndex, RowEventType.Unknown)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the RowEventArgs class with 
		/// the specified Row source, row index, start index, end index 
		/// and affected Cell
		/// </summary>
		/// <param name="source">The Row that originated the event</param>
		/// <param name="rowIndex">The index of the Row</param>
		/// <param name="cell">The affected Cell</param>
		/// <param name="cellFromIndex">The start index of the affected Cell(s)</param>
		/// <param name="cellToIndex">The end index of the affected Cell(s)</param>
		/// <param name="eventType">The type of event</param>
		public RowEventArgs(Row source, int rowIndex, Cell cell, int cellFromIndex, int cellToIndex, RowEventType eventType) : base()
		{
			this.source = source;
			this.rowIndex = rowIndex;
			this.cell = cell;
			this.cellFromIndex = cellFromIndex;
			this.cellToIndex = cellToIndex;
			this.eventType = eventType;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Row that Raised the event
		/// </summary>
		public Row Row
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
		/// Gets the index of the Row
		/// </summary>
		public int Index
		{
			get
			{
				return this.rowIndex;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="rowIndex"></param>
		internal void SetRowIndex(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}


		/// <summary>
		/// Gets the affected Cell
		/// </summary>
		public Cell Cell
		{
			get
			{
				return this.cell;
			}
		}


		/// <summary>
		/// Gets the start index of the affected Cell(s)
		/// </summary>
		public int CellFromIndex
		{
			get
			{
				return this.cellFromIndex;
			}
		}


		/// <summary>
		/// Gets the end index of the affected Cell(s)
		/// </summary>
		public int CellToIndex
		{
			get
			{
				return this.cellToIndex;
			}
		}


		/// <summary>
		/// Gets the type of event
		/// </summary>
		public RowEventType EventType
		{
			get
			{
				return this.eventType;
			}
		}

		#endregion
	}

	#endregion
}
