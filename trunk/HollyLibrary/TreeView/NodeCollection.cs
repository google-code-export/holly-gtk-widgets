// NodeCollection.cs created with MonoDevelop
// User: dantes at 12:19 PM 5/26/2008
//

using System;
using System.Collections;
using System.Collections.Generic;


namespace HollyLibrary
{
	
	public delegate void NodeRemoveEventHandler ( object sender, NodeRemoveEventArgs args  );
	public delegate void NodeAddEventHandler    ( object sender, NodeAddEventArgs args     );
	public delegate void NodeUpdateEventHandler ( object sender, NodeUpdateEventArgs args  );

	public class NodeCollection : CollectionBase
	{
		
		public event NodeAddEventHandler      NodeAdded;
		public event NodeRemoveEventHandler NodeRemoved;
		public event NodeUpdateEventHandler NodeUpdated;
		public event EventHandler ClearNodes;
		

		public void AddRange( HTreeNode[] nodes )
		{
			foreach( HTreeNode node in nodes )
			{
				this.Add( node );
			}
		}
		
		public void Add (HTreeNode node)
		{	
			List.Add(node);
			if( NodeAdded != null ) 
				NodeAdded( this, new NodeAddEventArgs( node ) );
		}
		
		public HTreeNode this[int index]
		{
			get
			{
				return (HTreeNode)this.List[index];
			}
			set
			{
				HTreeNode oldnode= (HTreeNode)this.List[index];
				this.List[index] = value;
				if( NodeUpdated != null) 
					NodeUpdated( this, new NodeUpdateEventArgs( oldnode, value ) );
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
			Remove( (HTreeNode) List[index] );
		}
		
		public void Remove( HTreeNode node )
		{
			this.List.Remove(node);
			if( NodeRemoved != null ) 
				NodeRemoved( this, new NodeRemoveEventArgs( node ) );
		}
		
		public bool Contains (HTreeNode node)
		{
			return this.List.Contains(node);
		}
		
		public int IndexOf( HTreeNode node)
		{
			return this.List.IndexOf(node);
		}
		
		protected override void OnClear ()
		{
			base.OnClear ();
			if( ClearNodes != null ) ClearNodes( this, new EventArgs() );
		}

	}
}
