using Gtk;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using HollyLibrary;

namespace test
{
	
	public class ComboTree : Window
	{
		//the combotree widget
		HComboTree cmb     = new HComboTree();
		//buttons
		Button BtnAdd      = new Button( "Add node"       );
		Button BtnAddChild = new Button( "Add child node" );
		Button BtnRemove   = new Button( "Remove node"    );
		//checkboxes
		CheckButton ChkShowIcon = new CheckButton("Show tree icons");
		CheckButton ChkEditable = new CheckButton("Editable tree items");
		
		public ComboTree() : base( WindowType.Toplevel )
		{
			this.Title          = "SimpleComboBox demo";
			this.WindowPosition = Gtk.WindowPosition.CenterAlways;
			this.Resize( 320, 240 );
			//
			initGui();
			//button events
			BtnAdd.Clicked          += OnBtnAddClicked;
			BtnAddChild.Clicked     += OnBtnAddChildClicked;
			BtnRemove.Clicked       += OnBtnRemoveClicked;
			//checkbox events
			ChkShowIcon.Toggled     += OnShowIconChecked;
			ChkEditable.Toggled     += OnEditableChecked;
		}
		
		private void OnBtnAddClicked( object sender, EventArgs args )
		{
			//add a list item
			Gdk.Pixbuf icon = GraphUtil.pixbufFromStock("gtk-add", IconSize.Button );
			int node_nr     = cmb.Tree.Nodes.Count;
			cmb.Tree.Nodes.Add( new HTreeNode( "node " + node_nr , icon ) );
		}
		
		private void OnBtnAddChildClicked( object sender, EventArgs args )
		{
			//add a new child node
			if( cmb.Tree.SelectedNode != null )
			{
				HTreeNode father_node = cmb.Tree.SelectedNode;
				Gdk.Pixbuf icon       = GraphUtil.pixbufFromStock("gtk-remove", IconSize.Button );
				HTreeNode new_node    = new HTreeNode("gigi kent", icon );
				father_node.Nodes.Add( new_node );
			}
		}
		
		private void OnBtnRemoveClicked( object sender, EventArgs args )
		{
			//remove the selected node
			HTreeNode node = cmb.Tree.SelectedNode;
			if( node != null )
			{
				HTreeNode parent_node = node.ParentNode;
				if( parent_node == null )
					cmb.Tree.Nodes.Remove( node );
				else
					parent_node.Nodes.Remove( node );
			}
			
		}
		
		private void OnEditableChecked( object sender, EventArgs args )
		{
			cmb.Tree.Editable        = ChkEditable.Active;
		}
		
		private void OnShowIconChecked( object sender, EventArgs args )
		{
			cmb.Tree.NodeIconVisible = ChkShowIcon.Active;
		}
		
		private void initGui()
		{
			//create the layout
			VBox layout       = new VBox();
			//add the combo
			layout.PackStart( cmb, false, false, 2);
			//add the add/edit/remove buttons
			HBox hbox = new HBox();
			hbox.PackStart  ( BtnAdd    );
			hbox.PackStart  ( BtnAddChild   );
			hbox.PackStart  ( BtnRemove );
			layout.PackStart( hbox, false, true, 0 );
			//add the checkbox
			hbox      = new HBox();
			hbox.PackStart( ChkShowIcon );
			hbox.PackStart( ChkEditable );
			layout.PackStart( hbox, false, true, 0 );
			//add layout
			this.Add( layout );
		}
		
	}
}
