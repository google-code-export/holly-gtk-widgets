// ListEventHandlers.cs created with MonoDevelop
// User: dantes at 11:35 AM 5/17/2008
//

using System;

namespace HollyLibrary
{
	public class ListUpdateEventArgs : EventArgs
	{
		private object new_value;
		private int index;
		
		public object NewValue {
			get {
				return new_value;
			}
		}

		public int Index {
			get {
				return index;
			}
		}

		public ListUpdateEventArgs( int index, object new_value )
		{
			this.new_value = new_value;
			this.index     = index;
		}
	}
	
	public class ListAddEventArgs : EventArgs
	{
		private object val;
		
		public object Value 
		{
			get 
			{
				return val;
			}
		}
		
		public ListAddEventArgs( object val )
		{
			this.val =  val;
		}
	}
	
	public class ListRemoveEventArgs : EventArgs
	{
		private int index;
		
		public int Index 
		{
			get 
			{
				return index;
			}
		}
		
		public ListRemoveEventArgs( int index )
		{
			this.index = index;
		}
	}
	
	public delegate void ListRemoveEventHandler ( object sender, ListRemoveEventArgs args  );
	public delegate void ListAddEventHandler    ( object sender, ListAddEventArgs args     );
	public delegate void ListUpdateEventHandler ( object sender, ListUpdateEventArgs args  );
	//
	public delegate void MeasureItemEventHandler( object sender, MeasureItemEventArgs args );
	public delegate void DrawItemEventHandler   ( object sender, DrawItemEventArgs args    );
		
}
