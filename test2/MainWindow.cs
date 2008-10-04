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
using System.ComponentModel;



public partial class MainWindow: Gtk.Window
{	
	
	
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		//
		//
		this.ShowAll();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	


		
	
}