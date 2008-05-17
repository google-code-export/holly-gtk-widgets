// ExEntry.cs created with MonoDevelop
// User: dantes at 8:41 PM 4/13/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Text;
using System.Collections.Generic;
using Gtk;
using HollyLibrary;

namespace HollyLibrary
{
	
	public class HMaskedEntry : Entry
	{
		private List<CharType> formatTypes = new List<CharType>();
		private char maskChar              = '_';
		private String validationString    = "";
		
		public HMaskedEntry()
		{
			
		}
		
		private void setValidationString( String val )
		{
			//read format
			FormatTypes.Clear();
			foreach( char ch in val.ToCharArray() )
			{
				if( ch.Equals('*') ) 
					FormatTypes.Add( CharType.AnyLetterDigit  );
				else if( ch.Equals('~') ) 
					FormatTypes.Add( CharType.Letter          );
				else if( ch.Equals('!') ) 
					FormatTypes.Add( CharType.UpperCaseLetter );
				else if( ch.Equals('_') ) 
					FormatTypes.Add( CharType.LowerCaseLetter );
				else if( ch.Equals('#') ) 
					FormatTypes.Add( CharType.Digit           );
				else if( ch.Equals('%') )
					FormatTypes.Add( CharType.Double          );
				else
					FormatTypes.Add( CharType.Fixed           );
			}
			//show completion string
			String str = "";
			for( int i = 0; i < FormatTypes.Count; i++ )
			{
				CharType type = FormatTypes[i];
				if( type == CharType.Fixed )
					str += val[i];
				else
					str += MaskChar;
			}
			this.Text        = str;
			this.FormatTypes = FormatTypes;
		}	
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			int index     = -1;
			int sel_stop  = -1;
			bool ret      = false;			
			this.GetSelectionBounds( out index, out sel_stop );
			//disable multi-selection
			if( sel_stop != index )
			{
				return false;
			}
			bool enter_val = false;
			//verify if cursor is between 0 and formatTypes.count
			if( index >= 0 && index < FormatTypes.Count )
			{
				//verify inserted value
				CharType type = FormatTypes[ index ];
				if( type != CharType.Fixed )
				{
					char key       = (char)evnt.KeyValue;					
					if( type == CharType.AnyLetterDigit && this.isAnyLetterDigit( key ) )
						enter_val  = true;
					if( type == CharType.Digit && this.isDigit( key ) )
						enter_val  = true;
					if( type == CharType.Double && this.isDouble( key ) )
						enter_val  = true;
					if( type == CharType.Letter && this.isLetter( key ) )
						enter_val  = true;
					if( type == CharType.LowerCaseLetter && this.isLowerCaseLetter( key ) )
						enter_val  = true;
					if( type == CharType.UpperCaseLetter && this.isUpperCaseLetter( key ) )
						enter_val  = true;
					if( enter_val ) 
					{
						ret       = base.OnKeyPressEvent (evnt);
						this.Text = this.Text.Remove( index + 1, 1 );
					}
				}
				else
				{
					enter_val = true;
				}
				//skip into next non-fixed
				if( enter_val )
				{
					for( int i = index + 1; i < FormatTypes.Count; i++ )
					{
						type      = FormatTypes[ i ];
						if( type != CharType.Fixed )
						{
							this.SelectRegion( i , i );
							break;
						}
					}
				}
			}
			return ret;
		}


		public List<CharType> FormatTypes 
		{
			get {
				return formatTypes;
			}
			set
			{
				formatTypes = value;
			}
		}
		
		private bool isAnyLetterDigit( char ch )
		{
			return ( Char.IsLetter( ch ) || Char.IsDigit( ch ) );
		}
		
		private bool isLetter( char ch )
		{
			return Char.IsLetter( ch );
		}
		
		private bool isUpperCaseLetter( char ch )
		{
			return ( Char.IsLetter( ch ) && Char.IsUpper( ch ) );
		}
		
		private bool isLowerCaseLetter( char ch )
		{
			return ( Char.IsLetter( ch ) && Char.IsLower( ch ) );
		}
		
		private bool isDigit( char ch )
		{
			return Char.IsDigit( ch );
		}
		
		private bool isDouble( char ch )
		{
			return ( Char.IsDigit( ch ) || ch == '.' );
		}
		
			
		public char MaskChar
		{
			get
			{
				return maskChar;
			}
			set
			{
				maskChar = value;
			}
		}
		
		public String ValidationString
		{
			get
			{
				return validationString;
			}
			set
			{
				validationString = value;
				setValidationString( value );
			}
		}
		
	}
}
