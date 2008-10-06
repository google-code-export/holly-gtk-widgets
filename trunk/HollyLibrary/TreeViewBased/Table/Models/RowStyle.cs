// RowStyle.cs created with MonoDevelop
// User: dantes at 3:50 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.ComponentModel;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Stores visual appearance related properties for a Row
	/// </summary>
	public class RowStyle
	{
		#region Class Data

		/// <summary>
		/// The background color of the Row
		/// </summary>
		private Gdk.Color backColor;

		/// <summary>
		/// The foreground color of the Row
		/// </summary>
		private Gdk.Color foreColor;

		/// <summary>
		/// The font used to draw the text in the Row
		/// </summary>
		private Pango.FontDescription font;

		/// <summary>
		/// The alignment of the text in the Row
		/// </summary>
		private RowAlignment alignment;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the RowStyle class with default settings
		/// </summary>
		public RowStyle()
		{
			this.backColor = Color.Empty;
			this.foreColor = Color.Empty;
			this.font = null;
			this.alignment = RowAlignment.Center;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the Font used by the Row
		/// </summary>
		[Category("Appearance"),
		Description("The font used to display text in the row")]
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
		/// Gets or sets the background color for the Row
		/// </summary>
		[Category("Appearance"),
		Description("The background color used to display text and graphics in the row")]
		public Gdk.Color BackColor
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
		/// Gets or sets the foreground color for the Row
		/// </summary>
		[Category("Appearance"),
		Description("The foreground color used to display text and graphics in the row")]
		public Gdk.Color ForeColor
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
		/// Gets or sets the vertical alignment of the text displayed in the Row
		/// </summary>
		[Category("Appearance"),
		DefaultValue(RowAlignment.Center),
		Description("The vertical alignment of the objects displayed in the row")]
		public RowAlignment Alignment
		{
			get
			{
				return this.alignment;
			}

			set
			{
				this.alignment = value;
			}
		}

		#endregion
	}
}
