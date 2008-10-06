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
	
	protected virtual void OnGtkcontrol1KeyDown (object sender, HollyLibrary.GcKeyEventArgs args)
	{
		HollyLibrary.GcKeyEventArgs a = args;
		a.SuppressKeyPress = false;
		Console.WriteLine("Key down: " + a.KeyCode + " alt:" + a.Alt  + " ctrl:" + a.Control+ " shift:" + a.Shift );
	}


	protected virtual void OnGtkcontrol1KeyUp (object sender, HollyLibrary.GcKeyEventArgs args)
	{
		HollyLibrary.GcKeyEventArgs a = args;
		a.SuppressKeyPress = true;
		Console.WriteLine("Key down: " + a.KeyCode + " alt:" + a.Alt  + " ctrl:" + a.Control+ " shift:" + a.Shift );
	}

	
	protected virtual void OnGtkcontrol1KeyPressed (object sender, HollyLibrary.GcKeyEventArgs args)
	{
		HollyLibrary.GcKeyEventArgs a = args;
		Console.WriteLine("Key down: " + a.KeyCode + " alt:" + a.Alt  + " ctrl:" + a.Control+ " shift:" + a.Shift );
	}

	protected virtual void OnGtkcontrol1MouseMove (object sender, HollyLibrary.GcMouseEventArgs args)
	{
		Console.WriteLine( "mouse move!");
	}

	protected virtual void OnGtkcontrol1MouseUp (object sender, HollyLibrary.GcMouseEventArgs args)
	{
		HollyLibrary.GcMouseEventArgs a = args;
		Console.WriteLine( "mouse up!" + a.Button );
	}

	protected virtual void OnGtkcontrol1MouseDown (object sender, HollyLibrary.GcMouseEventArgs args)
	{
		HollyLibrary.GcMouseEventArgs a = args;
		Console.WriteLine( "mouse down!" + a.Button );
	}

	protected virtual void OnGtkcontrol1MouseClick (object sender, HollyLibrary.GcMouseEventArgs args)
	{
		HollyLibrary.GcMouseEventArgs a = args;
		Console.WriteLine( "mouse click!" + a.Button );
	}

	protected virtual void OnGtkcontrol1MouseDoubleClick (object sender, HollyLibrary.GcMouseEventArgs args)
	{
		HollyLibrary.GcMouseEventArgs a = args;
		Console.WriteLine( "mouse dbl click!" + a.Button );
		gtkcontrol1.BackColor = GraphUtil.gdkColorFromWinForms( System.Drawing.Color.AliceBlue );
	}

	protected virtual void OnGtkcontrol1MouseWheel (object sender, HollyLibrary.GcMouseEventArgs args)
	{
		HollyLibrary.GcMouseEventArgs a = args;
		Console.WriteLine( "mouse wheel!" + a.Delta );
		gtkcontrol1.BackgroundImageLayout = HollyLibrary.ImageLayout.Zoom;
		gtkcontrol1.BackgroundImage = new Gdk.Pixbuf("/home/dantes/gtk-widgets-mono.jpg");
	}
	


		
	
}