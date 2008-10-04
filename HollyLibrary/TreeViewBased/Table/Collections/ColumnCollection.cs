// ColumnCollection.cs created with MonoDevelop
// User: dantes at 8:42 PM 9/19/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace HollyLibrary
{
	
	
	public class ColumnCollection : CollectionBase
	{
		
		public ColumnCollection()
		{
		}
		
		public event ListAddEventHandler      OnItemAdded;
		public event ListRemoveEventHandler OnItemRemoved;
		public event ListUpdateEventHandler OnItemUpdated;
		public event ListInsertEventHandler OnItemInserted;
		

		public void AddRange( object[] objects )
		{
			foreach( object obj in objects )
			{
				this.Add( obj );
			}
		}
		
		public void Add (object item)
		{
			
			List.Add(item);
			if( OnItemAdded != null ) 
				OnItemAdded( this, new ListAddEventArgs( item ) );
		}
		
		public object this[int index]
		{
			get
			{
				return this.List[index];
			}
			set
			{
				if( index >=0 && index < this.List.Count )
				{
					this.List[index] = value;
					if( OnItemUpdated != null) 
						OnItemUpdated( this, new ListUpdateEventArgs( index, value ) );
				}
			}
		}
		
		public void InsertAt( int index, object val )
		{
			this.InnerList.Insert( index, val );
			if( OnItemInserted != null ) 
				OnItemInserted( this, new ListInsertEventArgs( index, val ) );
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
				OnItemRemoved( this, new ListRemoveEventArgs( index ) );
		}
		
		public bool Contains (object item)
		{
			return this.List.Contains(item);
		}
		
		public int IndexOf( object item )
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
