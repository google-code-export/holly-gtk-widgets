// NodeEventArgs.cs created with MonoDevelop
// User: dantes at 3:23 PM 6/3/2008
//

using System;

namespace HollyLibrary
{
	
	
	public class NodeEventArgs : EventArgs
	{
		
		HTreeNode node;
		
		public NodeEventArgs(HTreeNode node)
		{
			this.node = node;
		}
		
		public HTreeNode Node 
		{
			get 
			{
				return node;
			}
			set 
			{
				node = value;
			}
		}
		
		
	}
}
