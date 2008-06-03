// ListItemEventList.cs created with MonoDevelop
// User: dantes at 3:08 PM 6/3/2008
//

using System;

namespace HollyLibrary
{
	
	public class ListItemUpdateEventArgs : EventArgs
	{
		private HListItem new_value;
		private int index;
		
		public HListItem NewValue 
		{
			get 
			{
				return new_value;
			}
		}

		public int Index 
		{
			get
			{
				return index;
			}
		}

		public ListItemUpdateEventArgs( int index, HListItem new_value )
		{
			this.new_value = new_value;
			this.index     = index;
		}
	}
	
	public class ListItemAddEventArgs : EventArgs
	{
		private HListItem val;
		
		public HListItem Value 
		{
			get 
			{
				return val;
			}
		}
		
		public ListItemAddEventArgs( HListItem val )
		{
			this.val =  val;
		}
	}
	
	public class ListItemRemoveEventArgs : EventArgs
	{
		private int index;
		
		public int Index 
		{
			get 
			{
				return index;
			}
		}
		
		public ListItemRemoveEventArgs( int index )
		{
			this.index = index;
		}
	}
	
	public delegate void ListItemRemoveEventHandler ( object sender, ListItemRemoveEventArgs args  );
	public delegate void ListItemAddEventHandler    ( object sender, ListItemAddEventArgs args     );
	public delegate void ListItemUpdateEventHandler ( object sender, ListItemUpdateEventArgs args  );
	
}
