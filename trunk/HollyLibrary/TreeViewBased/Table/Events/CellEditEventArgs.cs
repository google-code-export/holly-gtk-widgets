// CellEditEventArgs.cs created with MonoDevelop
// User: dantes at 4:46 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the BeginEdit, StopEdit and 
	/// CancelEdit events of a Table
	/// </summary>
	public delegate void CellEditEventHandler(object sender, CellEditEventArgs e);

	#endregion

	
	
	#region CellEditEventArgs
	
	/// <summary>
	/// Provides data for the BeginEdit, StopEdit and CancelEdit events of a Table
	/// </summary>
	public class CellEditEventArgs : CellEventArgsBase
	{
		#region Class Data

		/// <summary>
		/// The CellEditor used to edit the Cell
		/// </summary>
		private ICellEditor editor;
		
		/// <summary>
		/// The Table the Cell belongs to
		/// </summary>
		private HTable table;

		/// <summary>
		/// The Cells bounding Rectangle
		/// </summary>
		private Gdk.Rectangle cellRect;

		/// <summary>
		/// Specifies whether the event should be cancelled
		/// </summary>
		private bool cancel;

		/// <summary>
		/// Indicates whether the event was handled
		/// </summary>
		private bool handled;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="editor">The CellEditor used to edit the Cell</param>
		/// <param name="table">The Table that the Cell belongs to</param>
		public CellEditEventArgs(Cell source, ICellEditor editor, HTable table) : this( source, editor, table, -1, -1, Gdk.Rectangle.Zero )
		{
			
		}


		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index and row index
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="editor">The CellEditor used to edit the Cell</param>
		/// <param name="table">The Table that the Cell belongs to</param>
		/// <param name="row">The Column index of the Cell</param>
		/// <param name="column">The Row index of the Cell</param>
		/// <param name="cellRect"></param>
		public CellEditEventArgs(Cell source, ICellEditor editor, HTable table, int row, int column, Gdk.Rectangle cellRect) : base(source, column, row)
		{
			this.editor = editor;
			this.table = table;
			this.cellRect = cellRect;

			this.cancel = false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the CellEditor used to edit the Cell
		/// </summary>
		public ICellEditor Editor
		{
			get
			{
				return this.editor;
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
		/// Gets the Cells bounding Rectangle
		/// </summary>
		public Gdk.Rectangle CellRect
		{
			get
			{
				return this.cellRect;
			}
		}


		/// <summary>
		/// Gets or sets whether the event should be cancelled
		/// </summary>
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}

			set
			{
				this.cancel = value;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether the event was handled
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
