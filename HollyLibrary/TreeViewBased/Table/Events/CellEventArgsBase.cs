// CellEventArgsBase.cs created with MonoDevelop
// User: dantes at 3:34 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Base class for classes containing Cell event data
	/// </summary>
	public class CellEventArgsBase : EventArgs
	{
		#region Class Data
		
		/// <summary>
		/// The Cell that Raised the event
		/// </summary>
		private Cell source;

		/// <summary>
		/// The Column index of the Cell
		/// </summary>
		private int column;

		/// <summary>
		/// The Row index of the Cell
		/// </summary>
		private int row;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source and event type
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		public CellEventArgsBase(Cell source) : this(source, -1, -1)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="row">The Row index of the Cell</param>
		public CellEventArgsBase(Cell source, int column, int row) : base()
		{
			this.source = source;
			this.column = column;
			this.row = row;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Returns the Cell that Raised the event
		/// </summary>
		public Cell Cell
		{
			get
			{
				return this.source;
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
		/// 
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(int column)
		{
			this.column = column;
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
		/// 
		/// </summary>
		/// <param name="row"></param>
		internal void SetRow(int row)
		{
			this.row = row;
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
}
