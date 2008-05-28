// HTreeView.cs created with MonoDevelop
// User: dantes at 8:52 PM 5/24/2008
//

using System;
using Gtk;

namespace HollyLibrary
{
	
	public class HTreeView : TreeView
	{
		//checkbox, text, HTreeNode, expanded icon, unexpanded icon
		TreeStore store = new TreeStore( typeof(HTreeNode) );
		
		internal TreeViewColumn   BaseColumn;
		internal CellRendererText TextRenderer;

		//cells
		CellRendererToggle cell_chk    = new CellRendererToggle();
		CellRendererPixbuf cell_icon   = new CellRendererPixbuf();
		CellRendererText cell_text     = new CellRendererText();
		//node list
		private NodeCollection nodes   = new NodeCollection();
		
		public HTreeView()
		{
			this.Model          = store;
			//default settings
			this.HeadersVisible = false;
			this.EnableSearch   = true;
			//
			AddBaseColumn();
			this.RowExpanded  += OnExpandRow;
			this.RowCollapsed += OnCollapseRow;
		}
		
		public virtual void AddBaseColumn()
		{
			BaseColumn     = new TreeViewColumn();
			//set renderers properties
			cell_chk.Toggled           += OnCelltoggled;
			cell_chk.Visible            = false;
			cell_text.Editable          = true;
			cell_icon.Visible           = false;
			//
			BaseColumn.PackStart    ( cell_chk , false       );
			BaseColumn.PackStart    ( cell_icon, false       );
			BaseColumn.PackStart    ( cell_text, true        );
			//set datafuncs
			BaseColumn.SetCellDataFunc( cell_chk , new TreeCellDataFunc( OnChkDataFunc  ) );
			BaseColumn.SetCellDataFunc( cell_text, new TreeCellDataFunc( OnTextDataFunc ) );
			BaseColumn.SetCellDataFunc( cell_icon, new TreeCellDataFunc( OnIconDataFunc ) );
			//add column
			this.AppendColumn( BaseColumn );
			Nodes.NodeAdded   += OnNodeAdded;
			Nodes.NodeRemoved += OnNodeRemoved;
			Nodes.NodeUpdated += OnNodeUpdated;
			//
			
		}
		
		//checkbox cell function
		private void OnChkDataFunc(
		                        Gtk.TreeViewColumn col, Gtk.CellRenderer cell,
		                        Gtk.TreeModel model, Gtk.TreeIter iter
		                            )
		{
			HTreeNode nod        = getNodeFromIter( iter );
			CellRendererToggle c = cell as CellRendererToggle;
			c.Active             = nod.Checked;
		}
		
		//text cell function
		private void OnTextDataFunc(
		                        Gtk.TreeViewColumn col, Gtk.CellRenderer cell,
		                        Gtk.TreeModel model, Gtk.TreeIter iter
		                            )
		{
			//renderer node text
			HTreeNode nod      = getNodeFromIter( iter );
			CellRendererText c = cell as CellRendererText;
			c.Text             = nod.Text;
		}
		
		//icon data function
		private void OnIconDataFunc(
		                        Gtk.TreeViewColumn col, Gtk.CellRenderer cell,
		                        Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			//get the treenode linked to the iter;
			HTreeNode nod        = getNodeFromIter( iter );
			CellRendererPixbuf c = cell as CellRendererPixbuf;
			
			//if cell is expanded show opened icon, else show closed icon
			if( nod.OpenedIcon != null && c.IsExpanded )
				c.Pixbuf = nod.OpenedIcon;
			else
				c.Pixbuf = nod.Icon;
		}
		
		//checkbox state changed
		private void OnCelltoggled( object sender, ToggledArgs args )
		{
            TreeIter iter;
            if (store.GetIterFromString(out iter, args.Path))
            {
				
				HTreeNode nod = getNodeFromIter( iter );
				nod.Checked   = !nod.Checked;
				//(de)select all childs
				if( nod.Checked )
					nod.selectAllChilds();
				else
					nod.deselectAllChilds();
            }

		}
		
		//returns a HTreeNode from an iter
		private HTreeNode getNodeFromIter( TreeIter iter )
		{
			HTreeNode ret = store.GetValue( iter, 0 ) as HTreeNode;
			return ret;
		}
		
		
#region node-events
		public virtual void OnNodeAdded( object sender, NodeAddEventArgs args )
		{
			TreeIter iter       = store.AppendValues( args.Node );
			args.Node.Store     = store;
			args.Node.InnerIter = iter;
			args.Node.Treeview  = this;
		}
		
		public virtual void OnNodeRemoved( object sender, NodeRemoveEventArgs args )
		{
			Gtk.TreeIter iter = args.Node.InnerIter;
			store.Remove( ref iter );
		}
		
		public virtual void OnNodeUpdated( object sender, NodeUpdateEventArgs args )
		{
			args.NewNode.Store     = args.OldNode.Store;
			args.NewNode.InnerIter = args.OldNode.InnerIter;
			args.NewNode.Treeview  = args.OldNode.Treeview;
			store.SetValues( args.OldNode.InnerIter, args.NewNode );
		}
		
		public virtual void OnExpandRow( object sender, RowExpandedArgs args )
		{
			HTreeNode nod  = getNodeFromIter( args.Iter );
			nod.IsExpanded = true;
		}
		
		public virtual void OnCollapseRow( object sender, RowCollapsedArgs args )
		{
			HTreeNode nod  = getNodeFromIter( args.Iter );
			nod.IsExpanded = false;
		}
#endregion

#region properties
		public bool NodeIconVisible
		{
			get
			{
				return cell_icon.Visible;
			}
			set
			{
				cell_icon.Visible = value;
			}
		}
		
		public bool IsCheckBoxTree 
		{
			get 
			{
				return cell_chk.Visible ;
			}
			set 
			{
				cell_chk.Visible = value;
			}
		}

		public NodeCollection Nodes 
		{
			get 
			{
				return nodes;
			}
		}

		public bool Editable 
		{
			get 
			{
				return cell_text.Editable;
			}
			set 
			{
				cell_text.Editable = value;
			}
		}
		
		public HTreeNode[] SelectedNodes
		{
			get
			{
				TreePath[] paths = this.Selection.GetSelectedRows();
				HTreeNode[] ret  = new HTreeNode[ paths.Length ];
				for( int i = 0; i < ret.Length; i++ )
				{
					TreePath path = paths[i];
					TreeIter iter;
					store.GetIter( out iter, path );
					ret[i]        = getNodeFromIter(iter);
				}
				return ret;
			}
		}
		
		public HTreeNode SelectedNode
		{
			get
			{
				return SelectedNodes[0];
			}
		}
		
#endregion
	
		
	}
}