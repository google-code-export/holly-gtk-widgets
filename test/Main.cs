// Main.cs created with MonoDevelop
// User: dantes at 8:30 PM 4/13/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;
using Gtk;

namespace test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow2 win = new MainWindow2();
			win.ShowAll ();
			Application.Run ();
		}
	}
}