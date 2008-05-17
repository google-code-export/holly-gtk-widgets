// MainWindow.cs created with MonoDevelop
// User: dantes at 8:30 PM 4/13/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;
using Gtk;
using HollyLibrary;
using System.Drawing;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		this.hdateedit1.DateTimeFormatType = HollyLibrary.DateTimeFormatTypeEnum.ShortDate;
		this.hdateedit1.CurrentDate        = DateTime.Now;
		//
		for( int i = 0; i < 1000; i++ )
		{
			hsimplelist1.Items.Add( "my item " + i );
		}
		hsimplelist1.Items[10] = "my modified item!";
		//hsimplelist1.Items.Clear();
		hsimplelist1.SelectionType = SelectionMode.Multiple;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
		Pango.FontFamily[] fonts = this.PangoContext.FontMap.Families;
		foreach( Pango.FontFamily font in fonts )
		{
			hsimplelist1.Items.Add( font.Name );
		}

	}

	protected virtual void OnHsimplelist1OnSelectedIndexChanged (object sender, System.EventArgs e)
	{
		Console.WriteLine( hsimplelist1.Text );
	}

	protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
	{
		hsimplelist1.Sort();
	}

	protected virtual void OnHsimplelist1OnMeasureItem (object sender, HollyLibrary.MeasureItemEventArgs args)
	{		
		args.ItemHeight = 20;
	}

	protected virtual void OnHsimplelist1OnDrawItem (object sender, DrawItemEventArgs args)
	{
		Font font    = new Font("Times New Roman",12F,FontStyle.Bold);
		SolidBrush b = new SolidBrush( Color.Red );
		if( args.ItemIndex % 2 == 0 ) b.Color = Color.DarkBlue;
		String item  = hsimplelist1.Items[ args.ItemIndex ].ToString();
		args.Graphics.DrawString( item, font, b, args.CellArea.X, args.CellArea.Y ); 
	}

	protected virtual void OnHfontpicker1FontChange (object sender, HollyLibrary.FontEventArgs args)
	{
		Console.WriteLine("bau:"+args );
	}

	protected virtual void OnHdateedit1Changed (object sender, System.EventArgs e)
	{
	}

	protected virtual void OnButton3Clicked (object sender, System.EventArgs e)
	{
		object[] items = hsimplelist1.getSelectedItems();
		foreach( object obj in items )
		{
			Console.WriteLine( obj );
		}
	}

	
	
}