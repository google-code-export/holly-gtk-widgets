// Cell.cs created with MonoDevelop
// User: dantes at 3:33 PM 10/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.ComponentModel;

namespace HollyLibrary.Table
{
	
	
	/// <summary>
	/// Represents a Cell that is displayed in a Table
	/// </summary>
	public class Cell : IDisposable
	{
		#region EventHandlers

		/// <summary>
		/// Occurs when the value of a Cells property changes
		/// </summary>
		public event CellEventHandler PropertyChanged;

		#endregion


		#region Class Data

		// Cell state flags
		private static readonly int STATE_EDITABLE = 1;
		private static readonly int STATE_ENABLED = 2;
		private static readonly int STATE_SELECTED = 4;

		/// <summary>
		/// The text displayed in the Cell
		/// </summary>
		private string text;

		/// <summary>
		/// An object that contains data to be displayed in the Cell
		/// </summary>
		private object data;

		/// <summary>
		/// An object that contains data about the Cell
		/// </summary>
		private object tag;

		/// <summary>
		/// Stores information used by CellRenderers to record the current 
		/// state of the Cell
		/// </summary>
		private object rendererData;

		/// <summary>
		/// The Row that the Cell belongs to
		/// </summary>
		private Row row;

		/// <summary>
		/// The index of the Cell
		/// </summary>
		private int index;

		/// <summary>
		/// Contains the current state of the the Cell
		/// </summary>
		private byte state;
		
		/// <summary>
		/// The Cells CellStyle settings
		/// </summary>
		private CellStyle cellStyle;


		/// <summary>
		/// The text displayed in the Cells tooltip
		/// </summary>
		private string tooltipText;

		/// <summary>
		/// Specifies whether the Cell has been disposed
		/// </summary>
		private bool disposed = false;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the Cell class with default settings
		/// </summary>
		public Cell() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		public Cell(string text)
		{	
			this.Init();

			this.text = text;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified object
		/// </summary>
		/// <param name="value">The object displayed in the Cell</param>
		public Cell(object value)
		{
			this.Init();

			this.data = value;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and object
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="value">The object displayed in the Cell</param>
		public Cell(string text, object value)
		{
			this.Init();

			this.text = text;
			this.data = value;
		}


		
				
		
		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// fore Color, back Color and Font
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(string text, Gdk.Color foreColor, Gdk.Color backColor, Pango.FontDescription font)
		{
			this.Init();

			this.text = text;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and CellStyle
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(string text, CellStyle cellStyle)
		{
			this.Init();

			this.text = text;
			this.cellStyle = cellStyle;
		}
		
		
		/// <summary>
		/// Initializes a new instance of the Cell class with the specified object, 
		/// fore Color, back Color and Font
		/// </summary>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(object value, Gdk.Color foreColor, Gdk.Color backColor, Pango.FontDescription font)
		{
			this.Init();

			this.data = value;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text 
		/// and CellStyle
		/// </summary>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(object value, CellStyle cellStyle)
		{
			this.Init();

			this.data = value;
			this.cellStyle = cellStyle;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// object, fore Color, back Color and Font
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="foreColor">The foreground Color of the Cell</param>
		/// <param name="backColor">The background Color of the Cell</param>
		/// <param name="font">The Font used to draw the text in the Cell</param>
		public Cell(string text, object value, Gdk.Color foreColor, Gdk.Color backColor, Pango.FontDescription font)
		{
			this.Init();

			this.text = text;
			this.data = value;
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}


		/// <summary>
		/// Initializes a new instance of the Cell class with the specified text, 
		/// object and CellStyle
		/// </summary>
		/// <param name="text">The text displayed in the Cell</param>
		/// <param name="value">The object displayed in the Cell</param>
		/// <param name="cellStyle">A CellStyle that specifies the visual appearance 
		/// of the Cell</param>
		public Cell(string text, object value, CellStyle cellStyle)
		{
			this.Init();

			this.text = text;
			this.data = value;
			this.cellStyle = cellStyle;
		}




		
		
		
		
	

		/// <summary>
		/// Initialise default values
		/// </summary>
		private void Init()
		{
			this.text = null;
			this.data = null;
			this.rendererData = null;
			this.tag = null;
			this.row = null;
			this.index = -1;
			this.cellStyle = null;
			this.tooltipText = null;

			this.state = (byte) (STATE_EDITABLE | STATE_ENABLED);
		}

		#endregion


		#region Methods

		/// <summary>
		/// Releases all resources used by the Cell
		/// </summary>
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.text = null;
				this.data = null;
				this.tag = null;
				this.rendererData = null;

				if (this.row != null)
				{
					this.row.Cells.Remove(this);
				}

				this.row = null;
				this.index = -1;
				this.cellStyle = null;
				this.tooltipText = null;

				this.state = (byte) 0;
				
				this.disposed = true;
			}
		}


		/// <summary>
		/// Returns the state represented by the specified state flag
		/// </summary>
		/// <param name="flag">A flag that represents the state to return</param>
		/// <returns>The state represented by the specified state flag</returns>
		internal bool GetState(int flag)
		{
			return ((this.state & flag) != 0);
		}


		/// <summary>
		/// Sets the state represented by the specified state flag to the specified value
		/// </summary>
		/// <param name="flag">A flag that represents the state to be set</param>
		/// <param name="value">The new value of the state</param>
		internal void SetState(int flag, bool value)
		{
			this.state = (byte) (value ? (this.state | flag) : (this.state & ~flag));
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the text displayed by the Cell
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The text displayed by the cell")]
		public string Text
		{
			get
			{
				return this.text;
			}

			set
			{
				if (this.text == null || !this.text.Equals(value))
				{
					string oldText = this.Text;
					
					this.text = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ValueChanged, oldText));
				}
			}
		}


		/// <summary>
		/// Gets or sets the Cells non-text data
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The non-text data displayed by the cell"),
		TypeConverter(typeof(StringConverter))]
		public object Data
		{
			get
			{
				return this.data;
			}

			set
			{
				if (this.data != value)
				{
					object oldData = this.Data;
					
					this.data = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ValueChanged, oldData));
				}
			}
		}


		/// <summary>
		/// Gets or sets the object that contains data about the Cell
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("User defined data associated with the cell"),
		TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}

			set
			{
				this.tag = value;
			}
		}


		/// <summary>
		/// Gets or sets the CellStyle used by the Cell
		/// </summary>
		[Browsable(false),
		DefaultValue(null)]
		public CellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}

			set
			{
				if (this.cellStyle != value)
				{
					CellStyle oldStyle = this.CellStyle;
					
					this.cellStyle = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.StyleChanged, oldStyle));
				}
			}
		}


		/// <summary>
		/// Gets or sets whether the Cell is selected
		/// </summary>
		[Browsable(false)]
		public bool Selected
		{
			get
			{
				return this.GetState(STATE_SELECTED);
			}
		}


		/// <summary>
		/// Sets whether the Cell is selected
		/// </summary>
		/// <param name="selected">A boolean value that specifies whether the 
		/// cell is selected</param>
		internal void SetSelected(bool selected)
		{
			this.SetState(STATE_SELECTED, selected);
		}


		/// <summary>
		/// Gets or sets the background Color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The background color used to display text and graphics in the cell")]
		public Gdk.Color BackColor
		{
			get
			{
				if (this.CellStyle == null)
				{
					if (this.Row != null)
					{
						return this.Row.BackColor;
					}

					return GraphUtil.gdkColorFromWinForms( System.Drawing.Color.Transparent );
				}
				
				return this.CellStyle.BackColor;
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (!this.CellStyle.BackColor .Equal( value ))
				{
					Gdk.Color oldBackColor = this.BackColor;
					
					this.CellStyle.BackColor = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.BackColorChanged, oldBackColor));
				}
			}
		}


		/// <summary>
		/// Specifies whether the BackColor property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the BackColor property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeBackColor()
		{
			return (this.cellStyle != null );
		}


		/// <summary>
		/// Gets or sets the foreground Color for the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The foreground color used to display text and graphics in the cell")]
		public Gdk.Color ForeColor
		{
			get
			{
				if (this.CellStyle == null)
				{
					if (this.Row != null)
					{
						return this.Row.ForeColor;
					}

					return GraphUtil.GdkTransparentColor;
				}
				else
				{
					if (this.CellStyle.ForeColor.Equals(null)|| this.CellStyle.ForeColor.Equal( GraphUtil.GdkTransparentColor ) )
					{
						if (this.Row != null)
						{
							return this.Row.ForeColor;
						}
					}

					return this.CellStyle.ForeColor;
				}
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (!this.CellStyle.ForeColor.Equal( value ) )
				{
					Gdk.Color oldForeColor = this.ForeColor;
					
					this.CellStyle.ForeColor = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ForeColorChanged, oldForeColor));
				}
			}
		}


		/// <summary>
		/// Specifies whether the ForeColor property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the ForeColor property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeForeColor()
		{
			return (this.cellStyle != null && !this.cellStyle.ForeColor.Equals( null ) );
		}


		/// <summary>
		/// Gets or sets the Font used by the Cell
		/// </summary>
		[Category("Appearance"),
		Description("The font used to display text in the cell")]
		public Pango.FontDescription Font
		{
			get
			{
				if (this.CellStyle == null)
				{
					if (this.Row != null)
					{
						return this.Row.Font;
					}

					return null;
				}
				else
				{
					if (this.CellStyle.Font == null)
					{
						if (this.Row != null)
						{
							return this.Row.Font;
						}
					}

					return this.CellStyle.Font;
				}
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (this.CellStyle.Font != value)
				{
					Pango.FontDescription oldFont = this.Font;
					
					this.CellStyle.Font = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.FontChanged, oldFont));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Font property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Font property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeFont()
		{
			return (this.cellStyle != null && this.cellStyle.Font != null);
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
				if (this.CellStyle == null)
				{
					return CellPadding.Empty;
				}
				
				return this.CellStyle.Padding;
			}

			set
			{
				if (this.CellStyle == null)
				{
					this.CellStyle = new CellStyle();
				}
				
				if (this.CellStyle.Padding != value)
				{
					CellPadding oldPadding = this.Padding;
					
					this.CellStyle.Padding = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.PaddingChanged, oldPadding));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Padding property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Padding property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializePadding()
		{
			return this.Padding != CellPadding.Empty;
		}



		/// <summary>
		/// Gets or sets a value indicating whether the Cells contents are able 
		/// to be edited
		/// </summary>
		[Category("Appearance"),
		Description("Controls whether the cells contents are able to be changed by the user")]
		public bool Editable
		{
			get
			{
				if (!this.GetState(STATE_EDITABLE))
				{
					return false;
				}

				if (this.Row == null)
				{
					return this.Enabled;
				}

				return this.Enabled && this.Row.Editable;
			}

			set
			{
				bool editable = this.Editable;
				
				this.SetState(STATE_EDITABLE, value);
				
				if (editable != value)
				{
					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.EditableChanged, editable));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Editable property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Editable property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeEditable()
		{
			return !this.GetState(STATE_EDITABLE);
		}


		/// <summary>
		/// Gets or sets a value indicating whether the Cell 
		/// can respond to user interaction
		/// </summary>
		[Category("Appearance"),
		Description("Indicates whether the cell is enabled")]
		public bool Enabled
		{
			get
			{
				if (!this.GetState(STATE_ENABLED))
				{
					return false;
				}

				if (this.Row == null)
				{
					return true;
				}

				return this.Row.Enabled;
			}

			set
			{
				bool enabled = this.Enabled;
				
				this.SetState(STATE_ENABLED, value);
				
				if (enabled != value)
				{
					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.EnabledChanged, enabled));
				}
			}
		}


		/// <summary>
		/// Specifies whether the Enabled property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the Enabled property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeEnabled()
		{
			return !this.GetState(STATE_ENABLED);
		}


		/// <summary>
		/// Gets or sets the text displayed in the Cells tooltip
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The text displayed in the cells tooltip")]
		public string ToolTipText
		{
			get
			{
				return this.tooltipText;
			}

			set
			{
				if (this.tooltipText != value)
				{
					string oldToolTip = this.tooltipText;
					
					this.tooltipText = value;

					this.OnPropertyChanged(new CellEventArgs(this, CellEventType.ToolTipTextChanged, oldToolTip));
				}
			}
		}


		/// <summary>
		/// Gets or sets the information used by CellRenderers to record the current 
		/// state of the Cell
		/// </summary>
		protected internal object RendererData
		{
			get
			{
				return this.rendererData;
			}

			set
			{
				this.rendererData = value;
			}
		}


		/// <summary>
		/// Gets the Row that the Cell belongs to
		/// </summary>
		[Browsable(false)]
		public Row Row
		{
			get
			{
				return this.row;
			}
		}


		/// <summary>
		/// Gets or sets the Row that the Cell belongs to
		/// </summary>
		internal Row InternalRow
		{
			get
			{
				return this.row;
			}

			set
			{
				this.row = value;
			}
		}


		/// <summary>
		/// Gets the index of the Cell within its Row
		/// </summary>
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.index;
			}
		}


		/// <summary>
		/// Gets or sets the index of the Cell within its Row
		/// </summary>
		internal int InternalIndex
		{
			get
			{
				return this.index;
			}

			set
			{
				this.index = value;
			}
		}


		/// <summary>
		/// Gets whether the Cell is able to raise events
		/// </summary>
		protected internal bool CanRaiseEvents
		{
			get
			{
				// check if the Row that the Cell belongs to is able to 
				// raise events (if it can't, the Cell shouldn't raise 
				// events either)
				if (this.Row != null)
				{
					return this.Row.CanRaiseEvents;
				}

				return true;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the PropertyChanged event
		/// </summary>
		/// <param name="e">A CellEventArgs that contains the event data</param>
		protected virtual void OnPropertyChanged(CellEventArgs e)
		{
			e.SetColumn(this.Index);

			if (this.Row != null)
			{
				e.SetRow(this.Row.Index);
			}
			
			if (this.CanRaiseEvents)
			{
				if (this.Row != null)
				{
					this.Row.OnCellPropertyChanged(e);
				}
				
				if (PropertyChanged != null)
				{
					PropertyChanged(this, e);
				}
			}
		}

		#endregion
	}
}
