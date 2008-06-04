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
			
			//set colorpicker color
			ColorPicker.Color = new Gdk.Color( 255, 0, 0 );
			
			//add tooltip
			HToolTip.ToolTipInterval = 10;
			HToolTip.SetToolTip( BtnToolTip, "A nice bold title", "Hello! I am a baloon tooltip!");
			
			//set default regex
			RegExEntry.RegularExpression = "\\d{3}-\\d{2}-\\d{4}";
			
			//development tests:
			
			hsimplelist1.ItemHeight = 25;
			hsimplelist1.Items.AddRange( new String[] {"gigi1","gigi2","gigi3"} );
		}
		

		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		protected virtual void OnBtnTreeViewDemoClicked (object sender, System.EventArgs e)
		{
			TreeView win = new TreeView();
			win.ShowAll();
		}

		protected virtual void OnBtnSimpleListDemoClicked (object sender, System.EventArgs e)
		{
			SimpleList win = new SimpleList();
			win.ShowAll();
		}

		protected virtual void OnBtnStartComboDemoClicked (object sender, System.EventArgs e)
		{
			SimpleComboBox win =new SimpleComboBox();
			win.ShowAll();
		}

		protected virtual void OnComboTreeDemoClicked (object sender, System.EventArgs e)
		{
			ComboTree win = new ComboTree();
			win.ShowAll();
		}

		protected virtual void OnBtnApplyRegexClicked (object sender, System.EventArgs e)
		{
			RegExEntry.RegularExpression = TxtRegularExpresion.Text;
		}

		protected virtual void OnHsimplelist1ItemCheck (object sender, HollyLibrary.ListItemCheckEventArgs args)
		{
			Console.WriteLine("---");
			foreach( object obj in hsimplelist1.CheckedItems )
			{
				Console.WriteLine( obj.ToString() );
			}
			Console.WriteLine("---");
		}

	

		
	}
}

