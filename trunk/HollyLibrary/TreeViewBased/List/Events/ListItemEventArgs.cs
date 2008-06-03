// ListItemEventArgs.cs created with MonoDevelop
// User: dantes at 3:08 PM 6/3/2008
//

using System;

namespace HollyLibrary
{
	
	
	public class ListItemEventArgs : EventArgs
	{
		HListItem item;
		
		public ListItemEventArgs( HListItem item )
		{
			this.item = item;
		}
		
		public HListItem Item 
		{
			get 
			{
				return item;
			}
		}
		
	}
	
}
