// PaintCellEventArgs.cs created with MonoDevelop
// User: dantes at 3:56 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Gdk;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the PaintCell events of a Table
	/// </summary>
	public delegate void PaintCellEventHandler(object sender, PaintCellEventArgs e);

	#endregion



	#region PaintCellEventArgs
	
	/// <summary>
	/// Provides data for the PaintCell event
	/// </summary>
	public class PaintCellEventArgs : Gtk.ExposeEventArgs
	{
		#region Class Data

		/// <summary>
		/// The Cell to be painted
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
		/// Specifies whether the Cell is selected
		/// </summary>
		private bool selected;
		
		/// <summary>
		/// Specifies whether the Cell has focus
		/// </summary>
		private bool focused;

		/// <summary>
		/// Specifies whether the Cell's Column is sorted
		/// </summary>
		private bool sorted;

		/// <summary>
		/// Specifies whether the Cell is editable
		/// </summary>
		private bool editable;

		/// <summary>
		/// Specifies whether the Cell is enabled
		/// </summary>
		private bool enabled;
		
		/// <summary>
		/// The rectangle in which to paint the Cell
		/// </summary>
		private Rectangle cellRect;

		/// <summary>
		/// Indicates whether the user has done the paining for us
		/// </summary>
		private bool handled;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the PaintCellEventArgs class with 
		/// the specified graphics and clipping rectangle
		/// </summary>
		/// <param name="g">The Graphics used to paint the Cell</param>
		/// <param name="cellRect">The Rectangle that represents the rectangle 
		/// in which to paint</param>
		public PaintCellEventArgs(Gtk.ExposeEventArgs g, Rectangle cellRect) : this(g, null, null, -1, -1, false, false, false, false, true, cellRect)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the PaintCellEventArgs class with 
		/// the specified graphics, table, row index, column index, selected value,  
		/// focused value, mouse value and clipping rectangle
		/// </summary>
		/// <param name="g">The Graphics used to paint the Cell</param>
		/// <param name="cell">The Cell to be painted</param>
		/// <param name="table">The Table the Cell belongs to</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="selected">Specifies whether the Cell is selected</param>
		/// <param name="focused">Specifies whether the Cell has focus</param>
		/// <param name="sorted">Specifies whether the Cell's Column is sorted</param>
		/// <param name="editable">Specifies whether the Cell is able to be edited</param>
		/// <param name="enabled">Specifies whether the Cell is enabled</param>
		/// <param name="cellRect">The rectangle in which to paint the Cell</param>
		public PaintCellEventArgs(Gtk.ExposeEventArgs g, Cell cell, HTable table, int row, int column, bool selected, bool focused, bool sorted, bool editable, bool enabled, Gdk.Rectangle cellRect) 
		{
			this.cell = cell;
			this.table = table;
			this.row = row;
			this.column = column;
			this.selected = selected;
			this.focused = focused;
			this.sorted = sorted;
			this.editable = editable;
			this.enabled = enabled;
			this.cellRect = cellRect;
			this.handled = false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Cell to be painted
		/// </summary>
		public Cell Cell
		{
			get
			{
				return this.cell;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="cell"></param>
		internal void SetCell(Cell cell)
		{
			this.cell = cell;
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
		/// 
		/// </summary>
		/// <param name="table"></param>
		internal void SetTable(HTable table)
		{
			this.table = table;
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
		/// Gets whether the Cell is selected
		/// </summary>
		public bool Selected
		{
			get
			{
				return this.selected;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="selected"></param>
		internal void SetSelected(bool selected)
		{
			this.selected = selected;
		}


		/// <summary>
		/// Gets whether the Cell has focus
		/// </summary>
		public bool Focused
		{
			get
			{
				return this.focused;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="focused"></param>
		internal void SetFocused(bool focused)
		{
			this.focused = focused;
		}


		/// <summary>
		/// Gets whether the Cell's Column is sorted
		/// </summary>
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sorted"></param>
		internal void SetSorted(bool sorted)
		{
			this.sorted = sorted;
		}


		/// <summary>
		/// Gets whether the Cell is able to be edited
		/// </summary>
		public bool Editable
		{
			get
			{
				return this.editable;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="editable"></param>
		internal void SetEditable(bool editable)
		{
			this.editable = editable;
		}


		/// <summary>
		/// Gets whether the Cell is enabled
		/// </summary>
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="enabled"></param>
		internal void SetEnabled(bool enabled)
		{
			this.enabled = enabled;
		}


		/// <summary>
		/// Gets the Cells bounding rectangle
		/// </summary>
		public Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellRect"></param>
		internal void SetCellRect(Rectangle cellRect)
		{
			this.cellRect = cellRect;
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


		/// <summary>
		/// Gets or sets a value indicating whether the BeforePaintCell 
		/// event was handled
		/// </summary>
		public bool Handled
		{
			get
			{
				return this.handled;
			}

			set
			{
				this.handled = value;
			}
		}

		#endregion
	}

	#endregion
}
