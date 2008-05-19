// MainWin.cs created with MonoDevelop
// User: dantes at 9:35 PM 5/18/2008
//

using System;

namespace test
{
	
	
	public partial class MainWin : Gtk.Window
	{
		
		public MainWin() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			string[] items = new string[]{"gigi","duru","gigi","becali","daniel"};
			this.hsimplecombobox1.IsEditable = true;
			this.hsimplecombobox1.List.Items.AddRange( items );
			this.hsimplecombobox1.List.OnSelectedIndexChanged += new EventHandler( this.on_index_changed );
			this.hsimplecombobox1.DropDownOpened += new EventHandler( this.on_drop_down_opened );
			this.hsimplecombobox1.DropDownClosed += new EventHandler( this.on_drop_down_closed );
			this.hsimplecombobox1.TextChanged    += new EventHandler( this.on_text_changed );
		}
		
		private void on_text_changed( object o, EventArgs args )
		{
			Console.WriteLine( "text changed!" );
		}
		
		private void on_drop_down_closed( object o, EventArgs args )
		{
			Console.WriteLine( "drop down closed!" );
		}
		
		private void on_drop_down_opened( object o, EventArgs args )
		{
			Console.WriteLine( "drop down opened!" );
		}

		private void on_index_changed( object o, EventArgs args )
		{
			Console.WriteLine("index changed");
		}
		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		
	}
}
