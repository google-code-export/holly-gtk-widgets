// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace HollyLibrary {
    
    
    public partial class DateEditDialog {
        
        private Gtk.HBox hbox1;
        
        private Gtk.VBox vbox2;
        
        private Gtk.Calendar CCalendar;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Button BtnClear;
        
        private Gtk.VBox vbox3;
        
        private HollyLibrary.AnalogClock Clock;
        
        private Gtk.HBox hbox3;
        
        private Gtk.Label label4;
        
        private Gtk.SpinButton TxtHour;
        
        private Gtk.Label label2;
        
        private Gtk.SpinButton TxtMin;
        
        private Gtk.Label label3;
        
        private Gtk.SpinButton TxtSec;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget HollyLibrary.DateEditDialog
            this.Name = "HollyLibrary.DateEditDialog";
            this.Title = Mono.Unix.Catalog.GetString("DateEditDialog");
            this.TypeHint = ((Gdk.WindowTypeHint)(2));
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.BorderWidth = ((uint)(1));
            this.Resizable = false;
            this.AllowGrow = false;
            this.Decorated = false;
            this.DestroyWithParent = true;
            this.SkipPagerHint = true;
            this.SkipTaskbarHint = true;
            // Container child HollyLibrary.DateEditDialog.Gtk.Container+ContainerChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            this.hbox1.BorderWidth = ((uint)(3));
            // Container child hbox1.Gtk.Box+BoxChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.CCalendar = new Gtk.Calendar();
            this.CCalendar.CanFocus = true;
            this.CCalendar.Name = "CCalendar";
            this.CCalendar.DisplayOptions = ((Gtk.CalendarDisplayOptions)(3));
            this.vbox2.Add(this.CCalendar);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.vbox2[this.CCalendar]));
            w1.Position = 0;
            // Container child vbox2.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Homogeneous = true;
            this.hbox2.Spacing = 6;
            // Container child hbox2.Gtk.Box+BoxChild
            this.BtnClear = new Gtk.Button();
            this.BtnClear.CanFocus = true;
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.UseStock = true;
            this.BtnClear.UseUnderline = true;
            this.BtnClear.Label = "gtk-clear";
            this.hbox2.Add(this.BtnClear);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox2[this.BtnClear]));
            w2.Position = 0;
            this.vbox2.Add(this.hbox2);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
            w3.Position = 1;
            w3.Expand = false;
            w3.Fill = false;
            this.hbox1.Add(this.vbox2);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.hbox1[this.vbox2]));
            w4.Position = 0;
            w4.Expand = false;
            w4.Fill = false;
            // Container child hbox1.Gtk.Box+BoxChild
            this.vbox3 = new Gtk.VBox();
            this.vbox3.Name = "vbox3";
            this.vbox3.Spacing = 6;
            // Container child vbox3.Gtk.Box+BoxChild
            this.Clock = new HollyLibrary.AnalogClock();
            this.Clock.Name = "Clock";
            this.Clock.Datetime = new System.DateTime(0);
            this.vbox3.Add(this.Clock);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox3[this.Clock]));
            w5.Position = 0;
            // Container child vbox3.Gtk.Box+BoxChild
            this.hbox3 = new Gtk.HBox();
            this.hbox3.Name = "hbox3";
            this.hbox3.Spacing = 6;
            // Container child hbox3.Gtk.Box+BoxChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.LabelProp = Mono.Unix.Catalog.GetString("H:");
            this.hbox3.Add(this.label4);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.hbox3[this.label4]));
            w6.Position = 0;
            w6.Expand = false;
            w6.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.TxtHour = new Gtk.SpinButton(0, 24, 1);
            this.TxtHour.CanFocus = true;
            this.TxtHour.Name = "TxtHour";
            this.TxtHour.Adjustment.PageIncrement = 1;
            this.TxtHour.ClimbRate = 1;
            this.TxtHour.Numeric = true;
            this.hbox3.Add(this.TxtHour);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.hbox3[this.TxtHour]));
            w7.Position = 1;
            w7.Expand = false;
            w7.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("M:");
            this.hbox3.Add(this.label2);
            Gtk.Box.BoxChild w8 = ((Gtk.Box.BoxChild)(this.hbox3[this.label2]));
            w8.Position = 2;
            w8.Expand = false;
            w8.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.TxtMin = new Gtk.SpinButton(0, 60, 1);
            this.TxtMin.CanFocus = true;
            this.TxtMin.Name = "TxtMin";
            this.TxtMin.Adjustment.PageIncrement = 10;
            this.TxtMin.ClimbRate = 1;
            this.TxtMin.Numeric = true;
            this.hbox3.Add(this.TxtMin);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.hbox3[this.TxtMin]));
            w9.Position = 3;
            w9.Expand = false;
            w9.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = Mono.Unix.Catalog.GetString("S:");
            this.hbox3.Add(this.label3);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.hbox3[this.label3]));
            w10.Position = 4;
            w10.Expand = false;
            w10.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.TxtSec = new Gtk.SpinButton(0, 60, 1);
            this.TxtSec.CanFocus = true;
            this.TxtSec.Name = "TxtSec";
            this.TxtSec.Adjustment.PageIncrement = 10;
            this.TxtSec.ClimbRate = 1;
            this.TxtSec.Numeric = true;
            this.hbox3.Add(this.TxtSec);
            Gtk.Box.BoxChild w11 = ((Gtk.Box.BoxChild)(this.hbox3[this.TxtSec]));
            w11.Position = 5;
            w11.Expand = false;
            w11.Fill = false;
            this.vbox3.Add(this.hbox3);
            Gtk.Box.BoxChild w12 = ((Gtk.Box.BoxChild)(this.vbox3[this.hbox3]));
            w12.Position = 1;
            w12.Expand = false;
            this.hbox1.Add(this.vbox3);
            Gtk.Box.BoxChild w13 = ((Gtk.Box.BoxChild)(this.hbox1[this.vbox3]));
            w13.Position = 1;
            w13.Expand = false;
            w13.Fill = false;
            this.Add(this.hbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 452;
            this.DefaultHeight = 227;
            this.Show();
            this.ButtonPressEvent += new Gtk.ButtonPressEventHandler(this.OnButtonPressEvent);
            this.CCalendar.ButtonPressEvent += new Gtk.ButtonPressEventHandler(this.OnCalendar4ButtonPressEvent);
            this.CCalendar.DaySelected += new System.EventHandler(this.OnCCalendarDaySelected);
            this.CCalendar.DaySelectedDoubleClick += new System.EventHandler(this.OnCCalendarDaySelectedDoubleClick);
            this.BtnClear.Clicked += new System.EventHandler(this.OnBtnClearClicked);
            this.TxtHour.ValueChanged += new System.EventHandler(this.OnTxtHourValueChanged);
            this.TxtHour.ButtonPressEvent += new Gtk.ButtonPressEventHandler(this.OnTxtHourButtonPressEvent);
            this.TxtMin.ValueChanged += new System.EventHandler(this.OnTxtMinValueChanged);
            this.TxtMin.ButtonPressEvent += new Gtk.ButtonPressEventHandler(this.OnTxtMinButtonPressEvent);
            this.TxtSec.ValueChanged += new System.EventHandler(this.OnTxtSecValueChanged);
            this.TxtSec.ButtonPressEvent += new Gtk.ButtonPressEventHandler(this.OnTxtSecButtonPressEvent);
        }
    }
}