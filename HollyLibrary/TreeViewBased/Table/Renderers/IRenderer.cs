// IRenderer.cs created with MonoDevelop
// User: dantes at 3:29 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Exposes common methods provided by renderers
	/// </summary>
	public interface IRenderer
	{
		/// <summary>
		/// Gets a Rectangle that represents the client area of the object 
		/// being rendered
		/// </summary>
		Gdk.Rectangle ClientRectangle
		{
			get;
		}


		/// <summary>
		/// Gets or sets a Rectangle that represents the size and location 
		/// of the object being rendered
		/// </summary>
		Gdk.Rectangle Bounds
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the font of the text displayed by the object being 
		/// rendered
		/// </summary>
		Pango.FontDescription Font
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the foreground color of the object being rendered
		/// </summary>
		Gdk.Color ForeColor
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the background color for the object being rendered
		/// </summary>
		Gdk.Color BackColor
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets how the Renderers contents are aligned horizontally
		/// </summary>
		ColumnAlignment Alignment
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets how the Renderers contents are aligned vertically
		/// </summary>
		RowAlignment LineAlignment
		{
			get;
			set;
		}
	}
}
