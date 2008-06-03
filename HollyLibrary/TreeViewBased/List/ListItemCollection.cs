// ListItemCollection.cs created with MonoDevelop
// User: dantes at 3:07 PM 6/3/2008
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace HollyLibrary
{
	
	
	public class ListItemCollection : CollectionBase
	{
		
		public event ListItemAddEventHandler      OnItemAdded;
		public event ListItemRemoveEventHandler OnItemRemoved;
		public event ListItemUpdateEventHandler OnItemUpdated;
		
		public void AddRange( HListItem[] items )
		{
			foreach( HListItem item in items )
			{
				this.Add( item );
			}
		}
		
		public void Add (HListItem item)
		{
			
			List.Add(item);
			if( OnItemAdded != null ) 
				OnItemAdded( this, new ListItemAddEventArgs( item ) );
		}
		
		public HListItem this[int index]
		{
			get
			{
				return (HListItem)this.List[index];
			}
			set
			{
				if( index >=0 && index < this.List.Count )
				{
					this.List[index] = value;
					if( OnItemUpdated != null) 
						OnItemUpdated( this, new ListItemUpdateEventArgs( index, value ) );
				}
			}
		}
		
		internal void Sort()
		{
			this.InnerList.Sort();
		}
		
		internal void Sort( IComparer comparer )
		{
			this.InnerList.Sort(comparer);
		}
		
		public new void RemoveAt( int index )
		{
			if( index >=0 && index < this.List.Count )
				Remove( this.List[index] );
		}
		
		public void Remove( object item )
		{
			int index = this.List.IndexOf(item);
			this.List.Remove(item);
			if( OnItemRemoved != null ) 
				OnItemRemoved( this, new ListItemRemoveEventArgs( index ) );
		}
		
		public bool Contains (HListItem item)
		{
			return this.List.Contains(item);
		}
		
		public int IndexOf( HListItem item )
		{
			return this.List.IndexOf(item);
		}
		
		public new void Clear()
		{
			for( int i = this.List.Count - 1; i >=0; i-- )
			{
				RemoveAt(i);
			}
		}
		
	}
	

}
