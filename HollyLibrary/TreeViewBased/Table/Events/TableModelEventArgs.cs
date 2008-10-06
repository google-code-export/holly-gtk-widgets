// TableModelEventArgs.cs created with MonoDevelop
// User: dantes at 3:52 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the RowAdded and RowRemoved 
	/// events of a TableModel
	/// </summary>
	public delegate void TableModelEventHandler(object sender, TableModelEventArgs e);

	#endregion



	#region TableModelEventArgs

	/// <summary>
	/// Provides data for a TableModel's RowAdded and RowRemoved events
	/// </summary>
	public class TableModelEventArgs : EventArgs
	{
		#region Class Data
		
		/// <summary>
		/// The TableModel that Raised the event
		/// </summary>
		private TableModel source;

		/// <summary>
		/// The affected Row
		/// </summary>
		private Row row;

		/// <summary>
		/// The start index of the affected Row(s)
		/// </summary>
		private int toIndex;

		/// <summary>
		/// The end index of the affected Row(s)
		/// </summary>
		private int fromIndex;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		public TableModelEventArgs(TableModel source) : this(source, null, -1, -1)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		/// <param name="fromIndex">The start index of the affected Row(s)</param>
		/// <param name="toIndex">The end index of the affected Row(s)</param>
		public TableModelEventArgs(TableModel source, int fromIndex, int toIndex) : this(source, null, fromIndex, toIndex)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the TableModelEventArgs class with 
		/// the specified TableModel source, start index, end index and affected Column
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		/// <param name="row">The affected Row</param>
		/// <param name="fromIndex">The start index of the affected Row(s)</param>
		/// <param name="toIndex">The end index of the affected Row(s)</param>
		public TableModelEventArgs(TableModel source, Row row, int fromIndex, int toIndex)
		{
			this.source = source;
			this.row = row;
			this.fromIndex = fromIndex;
			this.toIndex = toIndex;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the TableModel that Raised the event
		/// </summary>
		public TableModel TableModel
		{
			get
			{
				return this.source;
			}
		}


		/// <summary>
		/// Gets the affected Row
		/// </summary>
		public Row Row
		{
			get
			{
				return this.row;
			}
		}


		/// <summary>
		/// Gets the start index of the affected Row(s)
		/// </summary>
		public int RowFromIndex
		{
			get
			{
				return this.fromIndex;
			}
		}


		/// <summary>
		/// Gets the end index of the affected Row(s)
		/// </summary>
		public int RowToIndex
		{
			get
			{
				return this.toIndex;
			}
		}

		#endregion
	}

	#endregion
}
