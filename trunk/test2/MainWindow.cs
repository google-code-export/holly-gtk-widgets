// MainWindow.cs created with MonoDevelop
// User: dantes at 11:21 AM 9/19/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;
using Gtk;
using HollyLibrary;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class MainWindow: Gtk.Window
{	
	
	HImageCheckBox chk = new HImageCheckBox("gigel duru este bun?");
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		this.Title          = "HTreeView demo";
		this.WindowPosition = Gtk.WindowPosition.CenterAlways;
		this.Resize( 320, 240 );
		//
		//
		VBox box = new VBox();
		chk.CheckedImage   = GraphUtil.pixbufFromStock( "gtk-yes", Gtk.IconSize.Button );
		chk.UncheckedImage = GraphUtil.pixbufFromStock( "gtk-no" , Gtk.IconSize.Button );
		box.PackStart( chk );
		box.PackStart( new Button("asdf") );
		this.Add( box );
		this.ShowAll();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	


		
	
}