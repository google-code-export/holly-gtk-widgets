// ObjectCollection.cs created with MonoDevelop
// User: dantes at 11:39 AM 5/17/2008
//

using System;
using System.Collections;
using System.Collections.Generic;
using HollyLibrary;

namespace HollyLibrary
{

	public class ObjectCollection 
	{
		
		List<object> inner = new List<object>();
		
		public event ListAddEventHandler OnItemAdded;
		public event ListRemoveEventHandler OnItemRemoved;
		public event ListUpdateEventHandler OnItemUpdated;
		public event EventHandler OnClear;
		
		public void Add (object item)
		{
			inner.Add(item);
			if( OnItemAdded != null ) 
				OnItemAdded( this, new ListAddEventArgs( item ) );
		}
		
		public List<object> InnerList
		{
			get
			{
				return inner;
			}
		}
		
		public object this[int index]
		{
			get
			{
				return inner[index];
			}
			set
			{
				inner[index] = value;
				if( OnItemUpdated != null) 
					OnItemUpdated( this, new ListUpdateEventArgs( index, value ) );
			}
		}
		
		public void Remove( object item )
		{
			int index = inner.IndexOf(item);
			inner.Remove(item);
			if( OnItemRemoved != null ) 
				OnItemRemoved( this, new ListRemoveEventArgs( index ) );
		}
		
		public void RemoveAt(int index)
		{
			inner.RemoveAt(index);
			if( OnItemRemoved != null ) 
				OnItemRemoved( this, new ListRemoveEventArgs( index ) );
		}
		
		public void Clear()
		{
			inner.Clear();
			if( OnClear != null ) OnClear( this, new EventArgs() );
		}
		
		public bool Contains (object item)
		{
			return inner.Contains(item);
		}
		
		public int IndexOf( object item )
		{
			return inner.IndexOf(item);
		}
		
		public int Count {
			get 
			{
				return inner.Count;
			}
		}
	
		
	
	}
}
