// HeaderMouseEventArgs.cs created with MonoDevelop
// User: dantes at 4:45 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the method that will handle the HeaderMouseEnter, HeaderMouseLeave, 
	/// HeaderMouseDown, HeaderMouseUp, HeaderMouseMove, HeaderClick and HeaderDoubleClick 
	/// events of a Table
	/// </summary>
	public delegate void HeaderMouseEventHandler(object sender, HeaderMouseEventArgs e);

	#endregion



	#region HeaderMouseEventArgs
	
	/// <summary>
	/// Provides data for the HeaderMouseEnter, HeaderMouseLeave, HeaderMouseDown, 
	/// HeaderMouseUp, HeaderMouseMove, HeaderClick and HeaderDoubleClick events of a Table
	/// </summary>
	public class HeaderMouseEventArgs : Gtk.MotionNotifyEventArgs
	{
		#region Class Data

		/// <summary>
		/// The Column that raised the event
		/// </summary>
		private Column column;
		
		/// <summary>
		/// The Table the Column belongs to
		/// </summary>
		private HTable table;
		
		/// <summary>
		/// The index of the Column
		/// </summary>
		private int index;
		
		/// <summary>
		/// The column header's bounding rectangle
		/// </summary>
		private Gdk.Rectangle headerRect;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the HeaderMouseEventArgs class with 
		/// the specified source Column, Table, column index and column header bounds
		/// </summary>
		/// <param name="column">The Column that Raised the event</param>
		/// <param name="table">The Table the Column belongs to</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="headerRect">The column header's bounding rectangle</param>
		public HeaderMouseEventArgs(Column column, HTable table, int index, Gdk.Rectangle headerRect) 
		{
			this.column = column;
			this.table = table;
			this.index = index;
			this.headerRect = headerRect;
		} 

		
		/// <summary>
		/// Initializes a new instance of the HeaderMouseEventArgs class with 
		/// the specified source Column, Table, column index, column header bounds 
		/// and MouseEventArgs
		/// </summary>
		/// <param name="column">The Column that Raised the event</param>
		/// <param name="table">The Table the Column belongs to</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="headerRect">The column header's bounding rectangle</param>
		/// <param name="mea">The MouseEventArgs that contains data about the 
		/// mouse event</param>
		public HeaderMouseEventArgs(Column column, HTable table, int index, Gdk.Rectangle headerRect, Gtk.MotionNotifyEventArgs mea) 
		{
			this.column = column;
			this.table = table;
			this.index = index;
			this.headerRect = headerRect;
		} 

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Column that Raised the event
		/// </summary>
		public Column Column
		{
			get
			{
				return this.column;
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
		/// Gets the index of the Column
		/// </summary>
		public int Index
		{
			get
			{
				return this.index;
			}
		}


		/// <summary>
		/// Gets the column header's bounding rectangle
		/// </summary>
		public Gdk.Rectangle HeaderRect
		{
			get
			{
				return this.headerRect;
			}
		}

		#endregion
	}

	#endregion
}
