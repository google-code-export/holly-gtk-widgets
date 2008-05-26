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
			
			this.hregexentry1.RegularExpression = "\\d{3}-\\d{2}-\\d{4}";
			String text = "My first line of text \r\n";
			text       += "Second line of text bla bla \r\n";
			text       += "last line of text";
			HToolTip.ToolTipInterval = 500;
			HToolTip.SetToolTip( button2, "title 2", text, "gtk-yes" );
			HToolTip.SetToolTip( button3, "title 3", text + text, System.Drawing.Color.White, System.Drawing.Color.Black );
			HToolTip.SetToolTip( hsimplelist1, "title 4", "buga buga", System.Drawing.Color.Yellow, System.Drawing.Color.Orange, "gtk-no" );
			//tree tests:
			Gdk.Pixbuf icon1 = GraphUtil.pixbufFromStock("gtk-yes", Gtk.IconSize.Button );
			Gdk.Pixbuf icon2 = GraphUtil.pixbufFromStock("gtk-no" , Gtk.IconSize.Button );
			
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
			htreeview1.Selection.Mode = Gtk.SelectionMode.Multiple;
		}

		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			htreeview1.Nodes[0].Text = "blah blah blah";
			HTreeNode root = htreeview1.Nodes[0];
			HTreeNode nod2 = new HTreeNode("test update");
			root.Nodes[2] = nod2;
		}

		protected virtual void OnButton3Clicked (object sender, System.EventArgs e)
		{
			foreach( HTreeNode nod in htreeview1.SelectedNodes )
			{
				Console.WriteLine("textul selectat:"+ nod.Text );
			}
		}

		
	}
}

