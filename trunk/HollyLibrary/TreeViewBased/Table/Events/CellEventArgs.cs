// CellEventArgs.cs created with MonoDevelop
// User: dantes at 3:33 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the PropertyChanged event of a Cell
	/// </summary>
	public delegate void CellEventHandler(object sender, CellEventArgs e);

	#endregion

	
	
	#region CellEventArgs
	
	/// <summary>
	/// Provides data for a Cell's PropertyChanged event
	/// </summary>
	public class CellEventArgs : CellEventArgsBase
	{
		#region Class Data

		/// <summary>
		/// The type of event
		/// </summary>
		private CellEventType eventType;

		/// <summary>
		/// The old value of the property
		/// </summary>
		private object oldValue;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source and event type
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the property</param>
		public CellEventArgs(Cell source, CellEventType eventType, object oldValue) : this(source, -1, -1, eventType, oldValue)
		{
			
		}

		
		/// <summary>
		/// Initializes a new instance of the CellEventArgs class with 
		/// the specified Cell source, column index, row index and event type
		/// </summary>
		/// <param name="source">The Cell that Raised the event</param>
		/// <param name="column">The Column index of the Cell</param>
		/// <param name="row">The Row index of the Cell</param>
		/// <param name="eventType">The type of event</param>
		/// <param name="oldValue">The old value of the property</param>
		public CellEventArgs(Cell source, int column, int row, CellEventType eventType, object oldValue) : base(source, column, row)
		{
			this.eventType = eventType;
			this.oldValue = oldValue;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the type of event
		/// </summary>
		public CellEventType EventType
		{
			get
			{
				return this.eventType;
			}
		}


		/// <summary>
		/// Gets the old value of the property
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
