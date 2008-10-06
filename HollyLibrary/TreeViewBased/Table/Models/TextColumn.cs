// TextColumn.cs created with MonoDevelop
// User: dantes at 4:25 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.ComponentModel;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Summary description for TextColumn.
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class TextColumn : Column
	{
		#region Constructor
		
		/// <summary>
		/// Creates a new TextColumn with default values
		/// </summary>
		public TextColumn() : base()
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public TextColumn(string text) : base(text)
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public TextColumn(string text, int width) : base(text, width)
		{

		}


		/// <summary>
		/// Creates a new TextColumn with the specified header text, width and visibility
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		/// <param name="visible">Specifies whether the column is visible</param>
		public TextColumn(string text, int width, bool visible) : base(text, width, visible)
		{

		}



		#endregion


		#region Methods

		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public override string GetDefaultRendererName()
		{
			return "TEXT";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override ICellRenderer CreateDefaultRenderer()
		{
			return new TextCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return "TEXT";
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override ICellEditor CreateDefaultEditor()
		{
			return new TextCellEditor();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get
			{
				return typeof(TextComparer);
			}
		}

		#endregion
	}
	
}
