// MainWin.cs created with MonoDevelop
// User: dantes at 9:35 PM 5/18/2008
//

using System;
using System.Drawing;
using HollyLibrary;

namespace test
{
	
	
	public partial class MainWin : Gtk.Window
	{
		
		public MainWin() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Resize( 640, 480 );
			
			this.Build();
			
			String text = "My first line of text \r\n";
			text       += "Second line of text bla bla \r\n";
			text       += "last line of text";
			HToolTip.ToolTipInterval = 500;
			HToolTip.SetToolTip( button2, "title 2", text, "gtk-yes" );
			HToolTip.SetToolTip( button3, "title 3", text + text, System.Drawing.Color.White, System.Drawing.Color.Black );
			HToolTip.SetToolTip( hsimplelist1, "title 4", "buga buga", System.Drawing.Color.Yellow, System.Drawing.Color.Orange, "gtk-no" );
			//tree tests:
			Gdk.Pixbuf icon1 = GraphUtil.pixbufFromStock("gtk-no", Gtk.IconSize.Button );
			Gdk.Pixbuf icon2 = GraphUtil.pixbufFromStock("gtk-yes" , Gtk.IconSize.Button );
			

			HollyLibrary.HTreeView htreeview1 = this.hcombotree1.Tree;
			htreeview1.NodeIconVisible = true;
			HTreeNode root   = new HTreeNode( "gigi1", icon1, icon2 );
			htreeview1.Nodes.Add( root );
			
			root.Nodes.Add( new HTreeNode( "gigi2",true ) );
			root.Nodes.Add( new HTreeNode( "gigi4", icon2 ) );
			root.Nodes.Add( new HTreeNode( "gigi5", icon2 ) );
			HTreeNode nod = new HTreeNode( "gigi6", icon2 );
			root.Nodes.Add( nod );
			
			nod.Nodes.Add ( new HTreeNode( "gigi7", icon2 ) );
			root.Nodes.Add( new HTreeNode( "gigi8", icon2 ) );
			root.Nodes.Add( new HTreeNode( "gigi9", icon2 ) );
			
			for( int i = 0; i < 100; i++ )
				hsimplelist1.Items.Add(i.ToString());
		}

		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			hsimplelist1.Items.Add("http://youtube.com/watch?v=oHutO06nGto&feature=related");
		}

		protected virtual void OnButton3Clicked (object sender, System.EventArgs e)
		{
		
		}

		protected virtual void OnHsimplelist1RowActivated (object o, Gtk.RowActivatedArgs args)
		{
			hsimplelist1.Items.RemoveAt( hsimplelist1.SelectedIndex );
		}

		
	}
}

