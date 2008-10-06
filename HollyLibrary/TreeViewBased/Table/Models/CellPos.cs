// CellPos.cs created with MonoDevelop
// User: dantes at 3:32 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.ComponentModel;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Represents the position of a Cell in a Table
	/// </summary>
	public struct CellPos
	{
		#region Class Data

		/// <summary>
		/// Repsesents a null CellPos
		/// </summary>
		public static readonly CellPos Empty = new CellPos(-1, -1);
		
		/// <summary>
		/// The Row index of this CellPos
		/// </summary>
		private int row;

		/// <summary>
		/// The Column index of this CellPos
		/// </summary>
		private int column;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellPos class with the specified 
		/// row index and column index
		/// </summary>
		/// <param name="row">The Row index of the CellPos</param>
		/// <param name="column">The Column index of the CellPos</param>
		public CellPos(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Translates this CellPos by the specified amount
		/// </summary>
		/// <param name="rows">The amount to offset the row index</param>
		/// <param name="columns">The amount to offset the column index</param>
		public void Offset(int rows, int columns)
		{
			this.row += rows;
			this.column += columns;
		}


		/// <summary>
		/// Tests whether obj is a CellPos structure with the same values as 
		/// this CellPos structure
		/// </summary>
		/// <param name="obj">The Object to test</param>
		/// <returns>This method returns true if obj is a CellPos structure 
		/// and its Row and Column properties are equal to the corresponding 
		/// properties of this CellPos structure; otherwise, false</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is CellPos))
			{
				return false;
			}

			CellPos cellPos = (CellPos) obj;

			if (cellPos.Row == this.Row)
			{
				return (cellPos.Column == this.Column);
			}

			return false;
		}


		/// <summary>
		/// Returns the hash code for this CellPos structure
		/// </summary>
		/// <returns>An integer that represents the hashcode for this 
		/// CellPos</returns>
		public override int GetHashCode()
		{
			return (this.Row ^ ((this.Column << 13) | (this.Column >> 0x13)));
		}


		/// <summary>
		/// Converts the attributes of this CellPos to a human-readable string
		/// </summary>
		/// <returns>A string that contains the row and column indexes of this 
		/// CellPos structure </returns>
		public override string ToString()
		{
			return "CellPos: (" + this.Row + "," + this.Column + ")";
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the Row index of this CellPos
		/// </summary>
		public int Row
		{
			get
			{
				return this.row;
			}

			set
			{
				this.row = value;
			}
		}


		/// <summary>
		/// Gets or sets the Column index of this CellPos
		/// </summary>
		public int Column
		{
			get
			{
				return this.column;
			}

			set
			{
				this.column = value;
			}
		}


		/// <summary>
		/// Tests whether any numeric properties of this CellPos have 
		/// values of -1
		/// </summary>
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return (this.Row == -1 || this.Column == -1);
			}
		}

		#endregion


		#region Operators

		/// <summary>
		/// Tests whether two CellPos structures have equal Row and Column 
		/// properties
		/// </summary>
		/// <param name="left">The CellPos structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The CellPos structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if the two CellPos structures 
		/// have equal Row and Column properties</returns>
		public static bool operator ==(CellPos left, CellPos right)
		{
			if (left.Row == right.Row)
			{
				return (left.Column == right.Column);
			}

			return false;
		}


		/// <summary>
		/// Tests whether two CellPos structures differ in their Row and 
		/// Column properties
		/// </summary>
		/// <param name="left">The CellPos structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The CellPos structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if any of the Row and Column 
		/// properties of the two CellPos structures are unequal; otherwise 
		/// false</returns>
		public static bool operator !=(CellPos left, CellPos right)
		{
			return !(left == right);
		}

		#endregion
	}
}
