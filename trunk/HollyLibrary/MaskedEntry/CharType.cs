// CharType.cs created with MonoDevelop
// User: dantes at 8:48 PM 4/13/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace HollyLibrary
{
	
	/*
	 * validation string format:
	 * * = any letter/digit
	 * ~ = letter
	 * ! = only uppercase letter
	 * _ = only lowercase letter
	 * # = digit
	 * % = double ( digit or point )
	 */
	
	public enum CharType
	{
		AnyLetterDigit, Letter, UpperCaseLetter, LowerCaseLetter,
		Digit, Fixed, Double
	}
	
}
