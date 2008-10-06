// CellStyle.cs created with MonoDevelop
// User: dantes at 3:39 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.ComponentModel;
using Gdk;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Stores visual appearance related properties for a Cell
	/// </summary>
	public class CellStyle
	{
		#region Class Data

		/// <summary>
		/// The background color of the Cell
		/// </summary>
		private Color backColor;

		/// <summary>
		/// The foreground color of the Cell
		/// </summary>
		private Color foreColor;

		/// <summary>
		/// The font used to draw the text in the Cell
		/// </summary>
		private Pango.FontDescription font;

		/// <summary>
		/// The amount of space between the Cells border and its contents
		/// </summary>
		private CellPadding padding;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellStyle class with default settings
		/// </summary>
		public CellStyle()
		{
			this.backColor = new Color( 0, 0, 1 );
			this.foreColor = new Color( 0, 0, 1 );
			this.font      = null;
			this.padding   = CellPadding.Empty;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the Font used by the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The font used to display text in the cell")]
		public Pango.FontDescription Font
		{
			get
			{
				return this.font;
			}

			set
			{
				this.font = value;
			}
		}


		/// <summary>
		/// Gets or sets the background color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The background color used to display text and graphics in the cell")]
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}

			set
			{
				this.backColor = value;
			}
		}


		/// <summary>
		/// Gets or sets the foreground color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The foreground color used to display text and graphics in the cell")]
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}

			set
			{
				this.foreColor = value;
			}
		}


		/// <summary>
		/// Gets or sets the amount of space between the Cells Border and its contents
		/// </summary>
		[Category("Appearance"),
		Description("The amount of space between the cells border and its contents")]
		public CellPadding Padding
		{
			get
			{
				return this.padding;
			}

			set
			{
				this.padding = value;
			}
		}

		#endregion
	}
}
