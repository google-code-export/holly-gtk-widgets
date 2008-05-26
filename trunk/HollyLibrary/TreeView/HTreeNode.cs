// HTreeItem.cs created with MonoDevelop
// User: dantes at 7:44 PM 5/24/2008
//

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace HollyLibrary
{
	
	
	public class HTreeNode
	{
		Gtk.TreeIter innerIter;
		Gtk.TreeStore store;
		Gtk.TreeView treeview;
		//properties
		Gdk.Pixbuf icon         = null;
		Gdk.Pixbuf opened_icon  = null;
		string text             = "";
		//items
		NodeCollection nodes    = new NodeCollection();
		HTreeNode parent        = null;
		private bool isExpanded = false;
		bool _checked           = false;
		//events
		public event EventHandler DrawNode;
		public event EventHandler MeasureNode;
		
#region constructors
		public HTreeNode()
		{
			this.init();
		}
		
		public HTreeNode( String text )
		{
			this.Text = text;
			this.init();
		}
		
		public HTreeNode( String text, bool _checked )
		{
			this.Text    = text;
			this.Checked = _checked;
			this.init();
		}
		
		public HTreeNode( String text, Gdk.Pixbuf icon )
		{
			this.Text = text;
			this.Icon = icon;
			this.init();
		}
		
		public HTreeNode( String text, Gdk.Pixbuf opened_icon, Gdk.Pixbuf closed_icon )
		{
			this.Text       = text;
			this.Icon       = closed_icon;
			this.OpenedIcon = opened_icon;
			this.init();
		}
		
		public HTreeNode( String text, bool _checked, Gdk.Pixbuf opened_icon, Gdk.Pixbuf closed_icon )
		{
			this.Text       = text;
			this.Checked    = _checked;
			this.Icon       = closed_icon;
			this.OpenedIcon = opened_icon;
			this.init();
		}
#endregion

		public void selectAllChilds()
		{
			Checked = true;
			foreach( HTreeNode nod in Nodes )
			{
				nod.selectAllChilds();
			}
		}
		
		public void deselectAllChilds()
		{
			Checked = false;
			foreach( HTreeNode nod in Nodes )
			{
				nod.deselectAllChilds();
			}
		}
		
		private void init()
		{
			nodes.NodeAdded   += OnNodeAdded;
			nodes.NodeRemoved += OnNodeRemoved;
			nodes.NodeUpdated += OnNodeUpdated;
		}
		
		public virtual void OnNodeUpdated( object sender, NodeUpdateEventArgs args )
		{
			args.NewNode.Store     = args.OldNode.Store;
			args.NewNode.InnerIter = args.OldNode.InnerIter;
			args.NewNode.Treeview  = args.OldNode.Treeview;
			Store.SetValues( args.OldNode.InnerIter, args.NewNode );
		}
		
		public virtual void OnNodeRemoved( object sender, NodeRemoveEventArgs args )
		{
			Gtk.TreeIter iter = args.Node.InnerIter ;
			Store.Remove( ref iter );
		}
		
		public virtual void OnNodeAdded( object sender, NodeAddEventArgs args )
		{
			Gtk.TreeIter iter   = Store.AppendValues(InnerIter, args.Node );
			args.Node.Store     = Store;
			args.Node.innerIter = iter;
			args.Node.Treeview  = Treeview;
		}
		
		public virtual void OnDrawItem( DrawItemEventArgs args )	
		{
			//TODO:
		}
		
#region properties
		public Gdk.Pixbuf Icon 
		{
			get 
			{
				return icon;
			}
			set 
			{
				icon = value;
				if( treeview != null ) treeview.QueueDraw();
			}
		}

		public string Text 
		{
			get 
			{
				return text;
			}
			set 
			{
				text = value;
				if( treeview != null ) treeview.QueueDraw();
			}
		}

		public NodeCollection Nodes 
		{
			get 
			{
				return nodes;
			}
		}

		public HTreeNode Parent 
		{
			get {
				return parent;
			}
			set {
				parent = value;
			}
		}

		public bool IsExpanded {
			get {
				return isExpanded;
			}
		}

		public Gtk.TreeIter InnerIter {
			get {
				return innerIter;
			}
			set {
				innerIter = value;
			}
		}

		public bool Checked {
			get {
				return _checked;
			}
			set {
				_checked = value;
				if( treeview != null ) treeview.QueueDraw();
			}
		}

		public Gdk.Pixbuf OpenedIcon {
			get {
				return opened_icon;
			}
			set {
				opened_icon = value;
				if( treeview != null ) treeview.QueueDraw();
			}
		}

		public Gtk.TreeStore Store {
			get {
				return store;
			}
			set {
				store = value;
			}
		}

		public Gtk.TreeView Treeview {
			get {
				return treeview;
			}
			set {
				treeview = value;
			}
		}

		
#endregion
		
	}
}
