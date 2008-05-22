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
			this.hregexentry1.RegularExpression = "\\d{3}-\\d{2}-\\d{4}";
			this.AppPaintable = true;
			this.Decorated    = false;
			this.ExposeEvent += Expose;
		}
		
		private  void Expose (object o, Gtk.ExposeEventArgs args)
		{
			int width;
		int height;
		
		this.GetSize (out width, out height);
		
		// Create the cairo context
		Cairo.Context cr = Gdk.CairoHelper.Create (this.GdkWindow);
		
		// Make the window transparent.
		//if (supports_alpha)
			cr.Color = new Cairo.Color (1.0, 1.0, 1.0, 0.0);
		//else
			//cr.Color = new Cairo.Color (1.0, 1.0, 1.0);
		
		cr.Operator = Cairo.Operator.Source;
		cr.Paint ();
		
		//And draw everything we want
		//Some cos/sin magic is done, never mind

 		cr.Color = new Cairo.Color (1.0, 0.2, 0.2, 0.7);
 		
 		double radius = (double) (width < height ? width : height) / 2.0 - 0.8;
 		double x_center = ((double) width) / 2.0;
 		double y_center = ((double) height) / 2.0;
		cr.Arc (x_center, y_center, radius, 0, 2.0*3.14);
		cr.Fill ();
		cr.Stroke ();
		
		cr.MoveTo (x_center, y_center);
		//if (supports_alpha)
			cr.Color = new Cairo.Color  (0.0, 0.0, 0.0, 0.9);
		//else
		//	cr.Color = new Cairo.Color  (0.0, 0.0, 0.0);
		
		System.DateTime time = System.DateTime.Now;
		double hour = (double) time.Hour;
		double minutes = (double) time.Minute;
		double seconds = (double) time.Second;
		double per_hour = (2.0 * 3.14) / 12.0;
		double dh = (hour * per_hour) + (per_hour / 60.0) * minutes;
 		dh += 2.0 * 3.14 / 4.0;
 		cr.LineWidth = 0.05 * radius;
 		cr.RelLineTo (-0.5 * radius * System.Math.Cos (dh), - 0.5 * radius * System.Math.Sin (dh));
 		cr.MoveTo (x_center, y_center);
 		
		double per_minute = (2.0 * 3.14) / 60.0;
		double dm = minutes * per_minute;
		dm += 2.0 * 3.14 / 4.0;
		cr.RelLineTo(-0.9 * radius * System.Math.Cos (dm), -0.9 * radius * System.Math.Sin (dm));
 		cr.MoveTo (x_center, y_center);
 		
		double per_second = (2.0 * 3.14) / 60;
		double ds = seconds * per_second;
		ds += 2.0 * 3.14 / 4.0;
		cr.RelLineTo(-0.9 * radius * System.Math.Cos (ds), -0.9 * radius * System.Math.Sin (ds));
		cr.Stroke ();

		//if (supports_alpha)
			cr.Color = new Cairo.Color (0.5, 0.5, 0.5, 0.9);
		//else
		//	cr.Color = new Cairo.Color (0.5, 0.5, 0.5);
		cr.Arc (x_center, y_center, 0.1 * radius, 0, 2.0*3.14);
		cr.Fill ();
		cr.Stroke ();

		//Once everything has been drawn, create our XShape mask
		//Our window content is contained inside the big circle,
		//so let's use that one as our mask
		Gdk.Pixmap pm = new Gdk.Pixmap (null, width, height, 1);
		Cairo.Context pmcr =  Gdk.CairoHelper.Create (pm);
		pmcr.Arc (x_center, y_center, radius, 0, 2.0*3.14);
		pmcr.Fill ();
		pmcr.Stroke ();
		//Apply input mask
		this.InputShapeCombineMask (pm, 0, 0);

		}

		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
			args.RetVal = true;
		}

		
	}
}
