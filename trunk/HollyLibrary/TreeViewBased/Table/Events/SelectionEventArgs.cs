// SelectionEventArgs.cs created with MonoDevelop
// User: dantes at 3:53 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	#region Delegates

	/// <summary>
	/// Represents the methods that will handle the SelectionChanged event of a TableModel
	/// </summary>
	public delegate void SelectionEventHandler(object sender, SelectionEventArgs e);

	#endregion



	#region SelectionEventArgs
	
	/// <summary>
	/// Provides data for a TableModel's SelectionChanged event
	/// </summary>
	public class SelectionEventArgs : EventArgs
	{
		#region Class Data
		
		/// <summary>
		/// The TableModel that Raised the event
		/// </summary>
		private TableModel source;

		/// <summary>
		/// The previously selected Row indicies
		/// </summary>
		private int[] oldSelectedIndicies;

		/// <summary>
		/// The newly selected Row indicies
		/// </summary>
		private int[] newSelectedIndicies;

		/// <summary>
		/// The Rectangle that bounds the previously selected Rows
		/// </summary>
		private Gdk.Rectangle oldSelectionBounds;

		/// <summary>
		/// The Rectangle that bounds the newly selected Rows
		/// </summary>
		private Gdk.Rectangle newSelectionBounds;

		#endregion
		

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the SelectionEventArgs class with 
		/// the specified TableModel source, old selected indicies and new 
		/// selected indicies
		/// </summary>
		/// <param name="source">The TableModel that originated the event</param>
		/// <param name="oldSelectedIndicies">An array of the previously selected Rows</param>
		/// <param name="newSelectedIndicies">An array of the newly selected Rows</param>
		public SelectionEventArgs(TableModel source, int[] oldSelectedIndicies, int[] newSelectedIndicies) : base()
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", "TableModel cannot be null");
			}
			
			this.source = source;
			this.oldSelectedIndicies = oldSelectedIndicies;
			this.newSelectedIndicies = newSelectedIndicies;

			this.oldSelectionBounds = new Gdk.Rectangle();
			this.newSelectionBounds = new Gdk.Rectangle();
			
			if (oldSelectedIndicies.Length > 0)
			{
				this.oldSelectionBounds = source.Selections.CalcSelectionBounds(oldSelectedIndicies[0], 
																				oldSelectedIndicies[oldSelectedIndicies.Length-1]);
			}
			
			if (newSelectedIndicies.Length > 0)
			{
				this.newSelectionBounds = source.Selections.CalcSelectionBounds(newSelectedIndicies[0], 
																				newSelectedIndicies[newSelectedIndicies.Length-1]);
			}
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
		/// Gets the previously selected Row indicies
		/// </summary>
		public int[] OldSelectedIndicies
		{
			get
			{
				return this.oldSelectedIndicies;
			}
		}


		/// <summary>
		/// Gets the newly selected Row indicies
		/// </summary>
		public int[] NewSelectedIndicies
		{
			get
			{
				return this.newSelectedIndicies;
			}
		}


		/// <summary>
		/// Gets the Rectangle that bounds the previously selected Rows
		/// </summary>
		internal Gdk.Rectangle OldSelectionBounds
		{
			get
			{
				return this.oldSelectionBounds;
			}
		}


		/// <summary>
		/// Gets the Rectangle that bounds the newly selected Rows
		/// </summary>
		internal Gdk.Rectangle NewSelectionBounds
		{
			get
			{
				return this.newSelectionBounds;
			}
		}

		#endregion
	}

	#endregion
}
