// NodeEventList.cs created with MonoDevelop
// User: dantes at 12:15 PM 5/26/2008
//

using System;

namespace HollyLibrary
{
	
	

		
		public class NodeUpdateEventArgs : EventArgs
		{
			private HTreeNode new_node;
			private HTreeNode old_node;
			
			public HTreeNode NewNode
			{
				get 
				{
					return new_node;
				}
			}
	
			public HTreeNode OldNode
			{
				get
				{
					return old_node;
				}
			}
	
			public NodeUpdateEventArgs( HTreeNode old_node, HTreeNode new_node )
			{
				this.old_node  = old_node;
				this.new_node  = new_node;
			}
		}
		
		public class NodeAddEventArgs : EventArgs
		{
			private HTreeNode val;
			
			public HTreeNode Node 
			{
				get 
				{
					return val;
				}
			}
			
			public NodeAddEventArgs( HTreeNode val )
			{
				this.val =  val;
			}
		}
		
		public class NodeRemoveEventArgs : EventArgs
		{
			private HTreeNode node;
			
			public HTreeNode Node
			{
				get 
				{
					return node;
				}
			}
			
			public NodeRemoveEventArgs( HTreeNode node )
			{
				this.node = node;
			}
		}
		
		
	
}
