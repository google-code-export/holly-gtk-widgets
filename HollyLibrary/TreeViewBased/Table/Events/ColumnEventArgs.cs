// ColumnEventArgs.cs created with MonoDevelop
// User: dantes at 3:46 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the PropertyChanged event of a Column, 
	/// or a Table's BeginSort and EndSort events
	/// </summary>
	public delegate void ColumnEventHandler(object sender, ColumnEventArgs e);

	#endregion
	
	
	
	#region ColumnEventArgs

	/// <summary>
	/// Provides data for a Column's PropertyChanged event, or a Table's 
	/// BeginSort and EndSort events
	/// </summary>
	public class ColumnEventArgs
	{
		#region Class Data

		/// <summary>
		/// The Column that Raised the event
		/// </summary>
		private Column source;

		/// <summary>
		/// The index of the Column in the ColumnModel
		/// </summary>
		private int index;

		/// <summary>
		/// The old value of the property that changed
		/// </summary>
		private object oldValue;

		/// <summary>
		/// The type of event
		/// </summary>
		private ColumnEventType eventType;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the ColumnEventArgs class with 
		/// the specified Column source, column index and event type
		/// </summary>
		/// <param name="source">The Column that Raised the event</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the changed property</param>
		public ColumnEventArgs(Column source, ColumnEventType eventType, object oldValue) : this(source, -1, eventType, oldValue)
		{

		}

		
		/// <summary>
		/// Initializes a new instance of the ColumnEventArgs class with 
		/// the specified Column source, column index and event type
		/// </summary>
		/// <param name="source">The Column that Raised the event</param>
		/// <param name="index">The index of the Column</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the changed property</param>
		public ColumnEventArgs(Column source, int index, ColumnEventType eventType, object oldValue) : base()
		{
			this.source = source;
			this.index = index;
			this.eventType = eventType;
			this.oldValue = oldValue;
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
				return this.source;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		internal void SetColumn(Column column)
		{
			this.source = column;
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
		/// 
		/// </summary>
		/// <param name="index"></param>
		internal void SetIndex(int index)
		{
			this.index = index;
		}


		/// <summary>
		/// Gets the type of event
		/// </summary>
		public ColumnEventType EventType
		{
			get
			{
				return this.eventType;
			}
		}


		/// <summary>
		/// Gets the old value of the Columns changed property
		/// </summary>
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		#endregion
	}

	#endregion
}
