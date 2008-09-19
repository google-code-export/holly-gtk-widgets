// HTable.cs created with MonoDevelop
// User: dantes at 8:37 PM 9/19/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;

namespace HollyLibrary
{
	
	public class HTable : TreeView
	{
		ListStore innerStore = null;
		RowCollection    rows    = new RowCollection   ();
		ColumnCollection columns = new ColumnCollection();
		
		public HTable()
		{
			
		}
		
		public HTableCell this[int row_index, int column_index ]
		{
			get
			{
				//TODO:
				return null;
			}
			set
			{
				//TODO:
				
			}
		}
		
		
		
#region properties
		public RowCollection TableRows 
		{
			get 
			{
				return rows;
			}
			set 
			{
				rows = value;
			}
		}
		
		public ColumnCollection TableColumns 
		{
			get 
			{
				return columns;
			}
			set 
			{
				columns = value;
			}
		}
#endregion
	}
}
