// HNumericEntry.cs created with MonoDevelop
// User: dantes at 9:58 PM 8/28/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using Gtk;

namespace HollyLibrary
{	

	/// <summary>
	/// The numeric texbox takes numeric(decimal) values as input.
	/// It has the following extra properties:
	///		NumericPrecision: Precision
	///		NumericScaleOnFocus: The scale to display when the textbox has got the focus.
	///		NumericScaleOnLostFocus: The scale to display when the textbox hasn't got the focus.
	///		NumericValue: The current numeric value displayed in the textbox (decimal)
	///		ZeroIsValid: Zero is a valid as input
	///		AllowNegative: Allow input of negative decimal numbers
	///	It has the following extra events:
	///		NumericValueChanged: The event fires when the numeric value changes 
	///							 by user input or programmaticly.(Like TextChanged)
	///	Use NumericValueChanged event instead of the TextChanged event!!!!
	///	The NumericValue property is also capable of databinding.
	///	The decimal number is displayed with grouping char.
	/// </summary>
	[
	 System.ComponentModel.DefaultEvent("NumericValueChanged"),
	 System.ComponentModel.DefaultProperty("NumericValue")
	]
	public class HNumericEntry : Entry
	{

		public HNumericEntry()
		{

			// TODO: Add any initialization after the InitializeComponent call
			this.Xalign = 0;
			this.Text   = "0"; 
			this.FocusOutEvent   += NumericTextBox_LostFocus;
			this.FocusInEvent    += NumericTextBox_GotFocus;
			this.Changed         += NumericTextBox_TextChanged;
		}

		#region "Variables"
		private int ii_ScaleOnLostFocus = 0;
		private Decimal idec_InternalValue = 0;
		private Decimal idec_NumericValue = 0;
		private int ii_ScaleOnFocus = 0;
		private int ii_Precision = 1;
		private bool ib_AllowNegative = true;
		private bool ib_NoChangeEvent = false;
		private bool ib_ZeroNotValid = false;
		#endregion

		public event EventHandler NumericValueChanged;

		#region "Properties"

		/// <summary>
		/// Indicates if the value zero (0) valid.
		/// </summary>
		[System.ComponentModel.Category("Numeric settings")]
		public bool ZeroIsValid
		{
			get {return ib_ZeroNotValid;}
			set {ib_ZeroNotValid = value;}
		}
		
		private void ShowMessage( String msg )
		{
			MessageDialog dlg = new MessageDialog (
			                    null, 
							    DialogFlags.DestroyWithParent,
							    MessageType.Info,
							    ButtonsType.Ok,
							    msg);
			dlg.Run();
			dlg.Destroy();
			Console.WriteLine("msg:" +msg);
		}

		/// <summary>
		/// Maximum allowed precision
		/// </summary>
		[System.ComponentModel.Category("Numeric settings")]
		public int NumericPrecision
		{
			get{return ii_Precision;}
			set
			{
				//Precision cannot be negative
				if ( value < 0 ) 
				{
					ShowMessage( "Precision cannot be negative!" );
					return;
				}

				if ( value < this.NumericScaleOnFocus ) 
				{
					this.NumericScaleOnFocus = value;
				}

				ii_Precision = value;
			}
		}

		
		/// <summary>
		/// The maximum scale allowed
		/// </summary>
		[System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All),
		System.ComponentModel.Category("Numeric settings")]
		public int NumericScaleOnFocus  
		{
			get{return ii_ScaleOnFocus;}
			set
			{
				//Scale cannot be negative
				if ( value < 0 ) 
				{
					ShowMessage( "Scale cannot be negative!" );
					return;
				}

				//Scale cannot be larger than precision
				if ( value >= this.NumericPrecision ) 
				{
					ShowMessage("Scale cannot be larger than precission!" );
					return;
				}

				ii_ScaleOnFocus = value;

				if ( ii_ScaleOnFocus > 0 ) 
				{
					this.Text = "0" + DecimalSeperator + new string (Convert.ToChar("0"), ii_ScaleOnFocus);
				} 
				else 
				{
					this.Text = "0";
				}
			}
		}

		
		/// <summary>
		/// Scale displayed when the textbox has no focus 
		/// </summary>
		[System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All),
		System.ComponentModel.Category("Numeric settings")]
		public int NumericScaleOnLostFocus  
		{
			get{return ii_ScaleOnLostFocus;}
			set
			{
				//Scale cannot be negative
				if ( value < 0 ) 
				{
					ShowMessage( "Scale cannot be negative!");
					return;
				}

				//Scale cannot be larger than precision
				if ( value >= this.NumericPrecision ) 
				{
					ShowMessage( "Scale cannot be larger than precesion!");
					return;
				}

				ii_ScaleOnLostFocus = value;
			}
		}

		private string  DecimalSeperator  
		{
			get 
			{
				return System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
			}
		}

		private string  GroupSeperator  
		{
			get 
			{
				return System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator;
			}
		}

		
		/// <summary>
		/// Indicates if negative numbers are allowed?
		/// </summary>
		[System.ComponentModel.Category("Numeric settings")]
		public bool AllowNegative  
		{
			get{return ib_AllowNegative;}
			set{ib_AllowNegative = value;}
		}

		/// <summary>
		/// The current numeric value displayed in the textbox
		/// </summary>
		[System.ComponentModel.Bindable(true),
		System.ComponentModel.Category("Numeric settings")]
		public object NumericValue  
		{
			get{return idec_NumericValue;}
			set
			{
				if (value.Equals(DBNull.Value))
				{
					if (value.Equals(0))
					{
						this.Text = Convert.ToString(0);
						idec_NumericValue = Convert.ToDecimal(0);
						OnNumericValueChanged(new System.EventArgs());
						return;
					}
				}
                
				if (!value.Equals(idec_NumericValue)) 
				{
					this.Text = Convert.ToString(value);
					idec_NumericValue = Convert.ToDecimal(value);
					OnNumericValueChanged(new System.EventArgs());
				}
			}
		}
		#endregion

		#region "Subs"
		

		private void NumericTextBox_LostFocus(object sender, EventArgs e)
		{
			ib_NoChangeEvent = true;

			idec_InternalValue = Convert.ToDecimal(this.Text);

			//set { the text to the new format
			//if (! li_Precision = 0 ) {
			if (!(ii_ScaleOnLostFocus == 0)) 
			{
				//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnLostFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
				this.Text = this.FormatNumber();
				
			} 
			else 
			{
				if ( this.Text.IndexOf('-') < 0 ) 
				{
					//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnLostFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
					this.Text = this.FormatNumber();
				} 
				else 
				{
					if ( this.Text == "-" ) 
					{
						this.Text = "";
					} 
					else 
					{
						//this.Text = CStr(System.Math.Abs(CDbl(this.Text)));
						//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnLostFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
						this.Text = this.FormatNumber();
					}
				}
			}

			ib_NoChangeEvent = false;
		}

		private void NumericTextBox_GotFocus(object sender, EventArgs e)
		{
			ib_NoChangeEvent = true;

			this.Text = Convert.ToString(idec_InternalValue);

			//set { the text to the new format
			//if (! li_Precision = 0 ) {
			if (!( ii_ScaleOnFocus == 0) ) 
			{
				//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
				this.Text = this.FormatNumber();
			} 
			else 
			{
				if ( this.Text.IndexOf('-') < 0 ) 
				{
					//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
					this.Text = this.FormatNumber();
				} 
				else 
				{
					if ( this.Text == "-" ) 
					{
						this.Text = "";
					} 
					else 
					{
						//this.Text = Convert.ToString(System.Math.Abs(Convert.ToDouble(this.Text)));
						//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
						this.Text = this.FormatNumber();
					}
				}
			}


			ib_NoChangeEvent = false;
		}
		
		public int SelectionStart
		{
			get
			{
				int start, end;
				this.GetSelectionBounds( out start, out end );
				return start;
			}
			set
			{
				this.SelectRegion( value, SelectionEnd );
			}
		}
		
		public int SelectionEnd
		{
			get
			{
				int start, end;
				this.GetSelectionBounds( out start, out end );
				return end;
			}
		}
		
		

		private void NumericTextBox_TextChanged(object sender, EventArgs e)
		{
			int li_SelStart = 0;
			bool lb_PositionCursorBeforeComma = false;

			//Indicates that no change event should happen
			//Prevent event from firing on changing the text in the change
			//event
			
			if ( ib_NoChangeEvent || (this.SelectionStart == -1) ) 
			{
				return;
			}

			//No Change event
			ib_NoChangeEvent = true;

			if ( string.Empty.Equals(this.Text.Trim())) 
			{
				this.Text = "0";
			}

			if ( this.Text.Substring(0, 1) == GroupSeperator ) 
			{
				this.Text = this.Text.Substring(1);
			}

			//if (! ii_Precision = 0 ) {
			if (!(ii_ScaleOnFocus == 0)) 
			{
				//if ( the current position is just before the comma
				if (this.SelectionStart == (this.Text.IndexOf(DecimalSeperator))) 
				{
					lb_PositionCursorBeforeComma = true;
				} 
				else 
				{
					li_SelStart = this.SelectionStart;
				}
			} 
			else 
			{
				li_SelStart = this.SelectionStart;
			}

			idec_InternalValue = Convert.ToDecimal(this.Text);
			this.NumericValue  = Convert.ToDecimal(this.Text);



			if ( this.HasFocus ) 
			{
				//set { the text to the new format
				//if (! ii_Precision = 0 ) {
				if (!(ii_ScaleOnFocus == 0)) 
				{
					//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
					this.Text = this.FormatNumber();
				} 
				else 
				{
					if ( this.Text.IndexOf('-') < 0 ) 
					{
						//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
						this.Text = this.FormatNumber();
					} 
					else 
					{
						if ( this.Text.Equals('-')) 
						{
							this.Text = "";
						} 
						else 
						{
							//this.Text = Convert.ToString(System.Math.Abs(Convert.ToDouble(this.Text)));
							//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
							this.Text = this.FormatNumber();
						}
					}
				}
			} 
			else 
			{
				//set { the text to the new format
				//if (! ii_Precision = 0 ) {
				if (!(ii_ScaleOnLostFocus == 0) ) 
				{
					//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnLostFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
					this.Text = this.FormatNumber();
				} 
				else 
				{
					if ( this.Text.IndexOf('-') < 0 ) 
					{
						//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnLostFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
						this.Text = this.FormatNumber();
					} 
					else 
					{
						if ( this.Text.Equals('-')) 
						{
							this.Text = "";
						} 
						else 
						{
							//this.Text = Convert.ToString(System.Math.Abs(Convert.ToDouble(this.Text)));
							//this.Text = Strings.FormatNumber(this.Text, this.NumericScaleOnLostFocus, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.True);
							this.Text = this.FormatNumber();
						}
					}
				}

			}

			//if ( the position was before the comma
			//then put again before the comma
			if (!(ii_ScaleOnFocus == 0) ) 
			{
				if ( lb_PositionCursorBeforeComma ) 
				{
					this.SelectionStart = (this.Text.IndexOf(DecimalSeperator));
				} 
				else 
				{
					this.SelectionStart = li_SelStart;
				}
			} 
			else 
			{
				this.SelectionStart = li_SelStart;
			}

			//Change event may fire
			ib_NoChangeEvent = false;


		}
		
		public String SelectedText
		{
			get
			{
				String ret = "";
				if( SelectionLength > 0 )
					ret = this.Text.Substring( SelectionStart, SelectionEnd );
				return ret;
			}
		}
		
		
		public int SelectionLength
		{
			get
			{
				return this.SelectionBound;
			}
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			bool ret = false;
			bool lb_PositionCursorJustBeforeComma = false;
			
			if (!(ii_ScaleOnFocus == 0) ) 
			{
				//Is the position of the cursor just before the comma
				lb_PositionCursorJustBeforeComma = (this.SelectionStart == (this.Text.IndexOf(DecimalSeperator)));
			}

			switch(evnt.Key) 
			{
				case Gdk.Key.Delete:
					//Otherwise strange effect
					if ( lb_PositionCursorJustBeforeComma ) 
					{
						this.SelectionStart = this.Text.IndexOf(DecimalSeperator) + 1;
					    ret = true;
						break;
					}
					//if ( all selected on delete pressed

					if ( this.Text.IndexOf('-') < 0 ) 
					{
					
						if ( this.SelectionBound == this.Text.Length) 
						{
							this.Text = "0";
							this.SelectionStart = 1;
						
							ret = true;
						
							break;
						}
					} 
					else 
					{

						if ( this.SelectionBound == this.Text.Length) 
						{
							this.Text = "0";
							this.SelectionStart = 1;
							ret= true;
							break;
						}

						if ( this.SelectionLength > 0 ) 
						{
							if ( this.SelectedText != "-" ) 
							{
								if ( Convert.ToDouble(this.SelectedText) == System.Math.Abs(Convert.ToDouble(this.Text)) ) 
								{
									this.Text = "0";
									this.SelectionStart = 1;
									ret = true;
									break;
								}
							}
						}
					}
					break;
				default:
				    ret = false;
					break;
			}
			
			if( !ret ) 
				return base.OnKeyPressEvent( evnt );
			else
				return ret;
		}

		protected override bool OnKeyReleaseEvent (Gdk.EventKey evnt)
		{
			bool ret = false;
			bool lb_PositionCursorBeforeComma = false;
			bool lb_InputBeforeCommaValid = false;
			bool lb_PositionCursorJustAfterComma = false;
			int li_SelStart = 0;

			lb_InputBeforeCommaValid = true;

			//Minus pressed
			
			if ( evnt.Key.Equals( Gdk.Key.minus ) ) 
			{
				if ( this.AllowNegative ) 
				{
					if ( this.Text.IndexOf('-') < 0 ) 
					{

						li_SelStart = this.SelectionStart;

						if (!(Convert.ToDecimal(this.Text) == 0))
						{
							this.Text = "-" + this.Text;
						
							this.SelectionStart = li_SelStart + 1;
						}
						ret = true;
						return base.OnKeyReleaseEvent(evnt);
					} 
					else 
					{
						
						switch (this.SelectionLength) 
						{
							
							case 0:
								li_SelStart = this.SelectionStart;

								this.Text = Convert.ToString(Convert.ToDouble(this.Text) * -1);

								this.SelectionStart = li_SelStart - 1;

								ret = true;
								break;
							default:
								//Is everything selected
								if(this.SelectionLength == this.Text.Length)
									this.Text = "-0";
								ret = true;
								break;
						}
					}
					ret = true;
					base.OnKeyReleaseEvent(evnt);;
				}
			}

			//The + key
			if ( evnt.Key.Equals( Gdk.Key.plus )) 
			{
				if (!(this.Text.IndexOf('-') < 0) ) 
				{
					//Is everything selected
					switch (this.SelectionLength) 
					{	
						case 0:
							li_SelStart = this.SelectionStart;

							this.Text = Convert.ToString(Convert.ToDouble(this.Text) * -1);

							this.SelectionStart = li_SelStart - 1;

							ret = true;
							break;
						default:
							if (this.Text.Length== this.SelectionLength)
							{
								this.Text = "0";
								ret  = true;
							}
							break;
					}
				}
				ret = true;
				 base.OnKeyReleaseEvent(evnt);;
			}

			if (!(ii_ScaleOnFocus == 0)) 
			{
				//Is the position of the cursor just after the comma
				lb_PositionCursorJustAfterComma = (this.SelectionStart == this.Text.IndexOf(DecimalSeperator) + 1);
			}

			if ( evnt.Key.Equals( Gdk.Key.BackSpace ) ) 
			{
				//Backspace
				if ( lb_PositionCursorJustAfterComma ) 
				{
					this.SelectionStart = this.Text.IndexOf(DecimalSeperator);
					ret = true;
				}

				//if ( all selected on delete pressed)
				if ( this.SelectionLength == this.Text.Length) 
				{
					this.Text = "0";
					this.SelectionStart = 1;
					ret = true;
					
				}

				if (evnt.Key.Equals(null))
				{
					ret = true;
				}
				 base.OnKeyReleaseEvent(evnt);;
			}

			//Prevent other keys than numeric and ,
			string ls_AllowedKeyChars = "1234567890" + DecimalSeperator;
			Console.WriteLine( "------------------------------- " );
			Console.WriteLine( "KEY=" + (char)evnt.KeyValue );
			Console.WriteLine( "------------------------------- " );
			if (ls_AllowedKeyChars.IndexOf( (char)evnt.KeyValue ) < 0)  
			{
				ret = true;
				return base.OnKeyReleaseEvent(evnt);;
			}

			if (!(ii_ScaleOnFocus == 0)) 
			{
				//position of cursor is before comma?
				lb_PositionCursorBeforeComma = ! (this.SelectionStart >= this.Text.IndexOf(DecimalSeperator) + 1);
			}

			//Comma pressed
			if ( evnt.Key.ToString() == DecimalSeperator ) 
			{
				if ( lb_PositionCursorBeforeComma ) 
				{
					this.SelectionStart = this.Text.IndexOf(DecimalSeperator) + 1 ;
					this.SelectRegion(0,0);
				}

				ret = true;
				return base.OnKeyReleaseEvent(evnt);;
			}

			//Prevent more than the precission numbers entered
			if (!(ii_ScaleOnFocus == 0)) 
			{
				if ( this.SelectionStart == this.Text.Length ) 
				{
					ret = true;
					return base.OnKeyReleaseEvent(evnt);;
				}
			}

			if (!(ii_ScaleOnFocus == 0)) 
			{
				//if ( the character entered would violate the numbers before the comma
				if ( this.Text.IndexOf('-') < 0 ) 
				{
					lb_InputBeforeCommaValid = !(this.Text.Substring(0,this.Text.IndexOf(DecimalSeperator)).Length >= (ii_Precision - ii_ScaleOnFocus));
				} 
				else 
				{
					lb_InputBeforeCommaValid = !(this.Text.Substring(0,this.Text.IndexOf(DecimalSeperator)).Length >= (ii_Precision - ii_ScaleOnFocus + 1));
				}
			} 
			else 
			{
				if ( this.Text.IndexOf('-') < 0 ) 
				{
					lb_InputBeforeCommaValid = ! ((this.Text.Length) >= ii_Precision);
				} 
				else 
				{
					lb_InputBeforeCommaValid = ! ((this.Text.Length) >= ii_Precision + 1);
				}
			}

			//if first char is 0 another may be entered
			if (!(ii_ScaleOnFocus == 0)) 
			{
				if ( (this.Text.Substring(0,1) == "0") && !(this.SelectionStart == 0)) 
				{
					lb_InputBeforeCommaValid = true;
				}
				if ( this.SelectionLength > 0 ) 
				{
					lb_InputBeforeCommaValid = true;
				}
			} 
			else 
			{
				if ( (this.Text.Substring(0,1) == "0") && ((this.SelectionStart == this.Text.Length) || (this.SelectionLength == 1)) ) 
				{
					lb_InputBeforeCommaValid = true;
				}
				if ( this.SelectionLength > 0 ) 
				{
					lb_InputBeforeCommaValid = true;
				}
			}

			if (!(ii_ScaleOnFocus == 0)) 
			{
				if ( lb_PositionCursorBeforeComma && !(lb_InputBeforeCommaValid) ) 
				{
					ret = true;
					return base.OnKeyReleaseEvent(evnt);;
				}
			} 
			else 
			{
				if (! (lb_InputBeforeCommaValid) ) 
				{
					ret = true;
					return base.OnKeyReleaseEvent(evnt);;
				}
			}
			
			if( ret )
				return base.OnKeyReleaseEvent (evnt);
			else
				return ret;
		}


		/// <summary>
		/// Raises the NumericValueChanged event
		/// </summary>
		/// <param name="e">The eventargs</param>
		protected void OnNumericValueChanged(System.EventArgs e)
		{
			if (NumericValueChanged != null)
			{
				NumericValueChanged(this,e);
			}
		}

		/// <summary>
		/// Formats a the text inf the textbox (which represents a number) according to
		/// the scale,precision and the enviroment settings.
		/// </summary>
		protected string FormatNumber()
		{
			StringBuilder lsb_Format = new StringBuilder();
			int li_Counter = 1;
			long ll_Remainder = 0;

			if (this.HasFocus)
			{
				while(li_Counter <= ii_Precision - ii_ScaleOnFocus)
				{
					if (li_Counter == 1)
					{
						lsb_Format.Insert(0,'0');
					}
					else
					{
						lsb_Format.Insert(0,'#');
					}

					System.Math.DivRem(li_Counter,3,out ll_Remainder); 
					if ((ll_Remainder == 0) && (li_Counter + 1 <= ii_Precision - ii_ScaleOnFocus))
					{
						lsb_Format.Insert(0,',');
					}

					li_Counter++;
				}
	
				li_Counter = 1;

				if (ii_ScaleOnFocus > 0)
				{
					lsb_Format.Append(".");

					while(li_Counter <= ii_ScaleOnFocus)
					{
						lsb_Format.Append('0');
						li_Counter++;
					}
				}
			
			}
			else
			{
				while(li_Counter <= ii_Precision - ii_ScaleOnLostFocus)
				{
					if (li_Counter == 1)
					{
						lsb_Format.Insert(0,'0');
					}
					else
					{
						lsb_Format.Insert(0,'#');
					}
					System.Math.DivRem(li_Counter,3, out ll_Remainder); 
					if ((ll_Remainder == 0) && (li_Counter + 1 <= ii_Precision - ii_ScaleOnLostFocus))
					{
						lsb_Format.Insert(0,',');
					}
					li_Counter++;
				}

				li_Counter = 1;

				if (ii_ScaleOnLostFocus > 0)
				{
					lsb_Format.Append(".");

					while(li_Counter <= ii_ScaleOnLostFocus)
					{
						lsb_Format.Append('0');
						li_Counter++;
					}
				}
			}
			return Convert.ToDecimal(this.Text).ToString(lsb_Format.ToString());

		}

		private void NumericTextBox_Validating(object sender, CancelEventArgs e)
		{
			if ((this.Text.Equals(string.Empty) || Convert.ToDecimal(this.NumericValue).Equals(Convert.ToDecimal(0))) && !this.ZeroIsValid)
			{
				e.Cancel = true;
			}
		}

		#endregion

		
	}
}