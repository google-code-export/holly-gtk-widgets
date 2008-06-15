// HTreeView.cs created with MonoDevelop
// User: dantes at 8:52 PM 5/24/2008
//

using System;
using Gtk;
using System.Drawing;

namespace HollyLibrary
{
	
	public delegate void NodeEventHandler      ( object sender, NodeEventArgs args );
	
	public class HTreeView : TreeView, ICustomCellTreeView
	{
		//
		private bool ownerDraw         = false;
		private int itemHeight         = 20;
		//checkbox, text, HTreeNode, expanded icon, unexpanded icon
		TreeStore store = new TreeStore( typeof(HTreeNode) );
		
		internal TreeViewColumn   BaseColumn;

		//cells
		CellRendererToggle cell_chk    = new CellRendererToggle();
		CellRendererPixbuf cell_icon   = new CellRendererPixbuf();
		CellRendererCustom cell_text   = null;
		//node list
		private NodeCollection nodes   = new NodeCollection();
		//my events
		public event NodeEventHandler       NodeExpanded;
		public event NodeEventHandler      NodeCollapsed;
		public event NodeEventHandler BeforeNodeCollapse;
		public event NodeEventHandler   BeforeNodeExpand;
		public event NodeEventHandler         NodeEdited;
		//
		public event DrawItemEventHandler    DrawItem;
		public event MeasureItemEventHandler MeasureItem;
		public event NodeEventHandler        NodeRightClick;
		
		public HTreeView()
		{
			//create the text cell
			cell_text = new CellRendererCustom( this );
			//create model
			this.Model          = store;
			//default settings
			this.HeadersVisible = false;
			this.EnableSearch   = true;
			//
			AddBaseColumn();
			this.RowExpanded      += OnExpandRow;
			this.RowCollapsed     += OnCollapseRow;
			this.TestExpandRow    += OnTestExpandRow;
			this.TestCollapseRow  += OnTestCollapseRow;
			//add cell edited change listener
			cell_text.Edited      += OnTextEdited;
			//key listener
			this.KeyReleaseEvent  +=OnKeyReleased;
		}
		
		public virtual void AddBaseColumn()
		{
			BaseColumn                  = new TreeViewColumn();
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
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			if( evnt.Button == 3 )
			{
				//get path at current mouse position
				TreePath path;
				this.GetPathAtPos( (int)evnt.X, (int)evnt.Y , out path );
				//get iter from path
				TreeIter iter;
				store.GetIter( out iter, path );
				if( NodeRightClick != null)
				{
					//get node from iter
					HTreeNode node = getNodeFromIter( iter );
					//raise event
					NodeRightClick( this, new NodeEventArgs( node ) );
				}
				
			}
			return base.OnButtonPressEvent (evnt);
		}
		
		private void OnTextEdited( object sender, EditedArgs args )
		{
			TreeIter iter;
			store.GetIter( out iter, new TreePath( args.Path ) );
			HTreeNode node = getNodeFromIter( iter );
			node.Text      = args.NewText;
			if( NodeEdited != null ) NodeEdited( this, new NodeEventArgs( node ) );
		}
		
		private void OnKeyReleased( object sender, KeyReleaseEventArgs args )
		{
			//if selected node has children nodes, expanded or collapsed
			//on right/left arrow button press
			HTreeNode node = SelectedNode;
			if( node != null && node.Nodes.Count > 0 )
			{
				if( args.Event.Key == Gdk.Key.Right )
					expandNode  ( node );
				else if( args.Event.Key == Gdk.Key.Left )
					collapseNode( node );
			}
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
			CellRendererCustom c = cell as CellRendererCustom;
			c.Text             = nod.Text;
			c.Iter             = iter;
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
		public HTreeNode getNodeFromIter( TreeIter iter )
		{
			HTreeNode ret = null;
			try
			{
				ret = store.GetValue( iter, 0 ) as HTreeNode;
			}
			catch
			{
				ret = null;
			}
			return ret;
		}
		
		
#region node-events
		public virtual void OnNodeAdded( object sender, NodeAddEventArgs args )
		{
			TreeIter iter       = store.AppendValues( args.Node );
			args.Node.Store     = store;
			args.Node.InnerIter = iter;
			args.Node.Treeview  = this;
			QueueDraw();
		}
		
		public virtual void OnNodeRemoved( object sender, NodeRemoveEventArgs args )
		{
			Gtk.TreeIter iter = args.Node.InnerIter;
			store.Remove( ref iter );
			QueueDraw();
		}
		
		public virtual void OnNodeUpdated( object sender, NodeUpdateEventArgs args )
		{
			args.NewNode.Store     = args.OldNode.Store;
			args.NewNode.InnerIter = args.OldNode.InnerIter;
			args.NewNode.Treeview  = args.OldNode.Treeview;
			store.SetValues( args.OldNode.InnerIter, args.NewNode );
			QueueDraw();
		}
		
		public virtual void OnExpandRow( object sender, RowExpandedArgs args )
		{
			HTreeNode nod  = getNodeFromIter( args.Iter );
			nod.IsExpanded = true;
			if( NodeExpanded != null ) NodeExpanded( this, new NodeEventArgs( nod ) );
			QueueDraw();
		}
		
		public virtual void OnCollapseRow( object sender, RowCollapsedArgs args )
		{
			HTreeNode nod  = getNodeFromIter( args.Iter );
			nod.IsExpanded = false;
			if( NodeCollapsed != null ) NodeCollapsed( this, new NodeEventArgs( nod ) );
			QueueDraw();
		}
		
		public virtual void OnTestExpandRow( object sender, TestExpandRowArgs args )
		{
			HTreeNode node = getNodeFromIter( args.Iter );
			if( BeforeNodeExpand != null ) BeforeNodeExpand( sender, new NodeEventArgs( node ) );
		}
		
		public virtual void OnTestCollapseRow( object sender, TestCollapseRowArgs args )
		{
			HTreeNode node = getNodeFromIter( args.Iter );
			if( BeforeNodeCollapse != null ) BeforeNodeCollapse( sender, new NodeEventArgs( node ) );
		}
#endregion

		public void selectNode( HTreeNode node )
		{
			//this.Selection.SelectIter( node.InnerIter );
			TreePath path = Model.GetPath( node.InnerIter );
			this.Selection.SelectPath( path );
			//move cursor to selection
			this.SetCursor( path, this.Columns[0], false );
		}
		
		public void expandNode( HTreeNode node )
		{
			TreePath path = this.Model.GetPath( node.InnerIter );
			ExpandToPath( path );
		}
		
		public void collapseNode( HTreeNode node )
		{
			TreePath path = this.Model.GetPath( node.InnerIter );
			CollapseRow( path );
		}

		
		public virtual void OnMeasureItem ( int ItemIndex, Gtk.TreeIter iter, Widget widget, ref Gdk.Rectangle cell_area, out Gdk.Rectangle result )
		{
			//	
			MeasureItemEventArgs args = new MeasureItemEventArgs( ItemIndex, iter, cell_area );
			
			if( ownerDraw &&  MeasureItem != null )
				MeasureItem( this, args );					
			else
				args.ItemHeight  = ItemHeight; 			
			result.X       = args.ItemLeft;
			result.Y       = args.ItemTop;
			result.Width   = args.ItemWidth;
			result.Height  = args.ItemHeight;
		}
		
		public virtual void OnDrawItem ( int ItemIndex, TreeIter iter, Gdk.Drawable window, Widget widget, Gdk.Rectangle background_area, Gdk.Rectangle cell_area, Gdk.Rectangle expose_area, CellRendererState flags)
		{
			DrawItemEventArgs args = new DrawItemEventArgs( ItemIndex, iter, window, widget, background_area, cell_area, expose_area, flags );
			//
			if( OwnerDraw && DrawItem != null )
			{
				DrawItem( this, args );
			}
			else
			{				
				
				String text      = getNodeFromIter( args.Iter ).Text;
				//take font from style
				Font font        = new Font( Style.FontDesc.Family , Style.FontDesc.Size / 1000 );
				// take color from style
				Gdk.Color gcolor = Style.Foreground( StateType.Normal );
				
				Color c          = Color.Black;
				try
				{
					Color.FromArgb( gcolor.Red, gcolor.Green, gcolor.Blue );
				}
				catch(Exception ex)
				{
					Console.WriteLine( ex.Message );
				}
				
				Brush b          = new SolidBrush( c );
				//set quality to HighSpeed
				args.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
				args.Graphics.DrawString( text, font, b, args.CellArea.X, args.CellArea.Y );
				args.Graphics.Dispose();
			}
		}
		
		
#region properties
		
		
		public bool OwnerDraw 
		{
			get 
			{
				return ownerDraw;
			}
			set 
			{
				ownerDraw = value;
				QueueDraw();
			}
		}
		
		public int ItemHeight 
		{
			get
			{
				return itemHeight;
			}
			set
			{
				itemHeight = value;
			}
		}
		
		public bool NodeIconVisible
		{
			get
			{
				return cell_icon.Visible;
			}
			set
			{
				cell_icon.Visible = value;
				Columns[0].QueueResize();
				QueueDraw();
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
				Columns[0].QueueResize();
				QueueDraw();
			}
		}

		public NodeCollection Nodes 
		{
			get 
			{
				return nodes;
			}
		}
		
		public bool IsDragAndDropEnable
		{
			get
			{
				return this.Reorderable;
			}
			set
			{
				this.Reorderable = value;
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
				Columns[0].QueueResize();
				QueueDraw();
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
				if( SelectedNodes.Length > 0 )
					return SelectedNodes[0];
				else
					return null;
			}
		}
		
#endregion
	
		
	}
}
