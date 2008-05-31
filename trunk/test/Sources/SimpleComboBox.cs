using Gtk;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using HollyLibrary;

namespace test
{
	
	public class SimpleComboBox : Window
	{
		//the list widget
		HSimpleComboBox cmb = new HSimpleComboBox();
		//buttons
		Button BtnAdd    = new Button("Add item");
		Button BtnRemove = new Button("Remove last item");
		Button BtnEdit   = new Button("Edit last item");
		//checkboxes
		CheckButton ChkOwnerDrawned = new CheckButton("Popup list is ownerdrawed");
		CheckButton ChkEditable     = new CheckButton("Editable combo");
		
		public SimpleComboBox() : base( WindowType.Toplevel )
		{
			this.Title          = "SimpleComboBox demo";
			this.WindowPosition = Gtk.WindowPosition.CenterAlways;
			this.Resize( 320, 240 );
			//
			initGui();
			//button events
			BtnAdd.Clicked          += OnBtnAddClicked;
			BtnEdit.Clicked         += OnBtnEditClicked;
			BtnRemove.Clicked       += OnBtnRemoveClicked;
			//checkbox events
			ChkOwnerDrawned.Toggled += OnOwnerDrawnedChecked;
			ChkEditable.Toggled     += OnEditableChecked;
			//ownerdrawned event
			cmb.List.DrawItem       += OnItemDraw;
		}
		
		private void OnBtnAddClicked( object sender, EventArgs args )
		{
			//add a list item
			cmb.List.Items.Add("new item !");
		}
		
		private void OnBtnEditClicked( object sender, EventArgs args )
		{
			//change the last item's text
			if( cmb.List.Items.Count > 0 )
			{
				cmb.List.Items[ cmb.List.Items.Count - 1 ] = "i'm modified";
			}
		}
		
		private void OnBtnRemoveClicked( object sender, EventArgs args )
		{
			//remove the selected item
			if( cmb.List.Items.Count > 0 )
				cmb.List.Items.RemoveAt( cmb.List.Items.Count - 1 );
			
		}
		
		private void OnEditableChecked( object sender, EventArgs args )
		{
			cmb.IsEditable = ChkEditable.Active;
		}
		
		private void OnOwnerDrawnedChecked( object sender, EventArgs args )
		{
			cmb.List.OwnerDraw      = ChkOwnerDrawned.Active;
		}
		
		private void OnItemDraw( object sender, DrawItemEventArgs args )
		{
			String text      = cmb.List.Items[ args.ItemIndex ].ToString();
			//take font from style
			Font font        = new Font( Style.FontDesc.Family , Style.FontDesc.Size / 1000, FontStyle.Bold );
			
			// take color from style
			Color c          = Color.Blue;
			if( args.ItemIndex % 2 == 0 ) c = Color.Red;
			
			Brush b          = new SolidBrush( c );
			//set quality to HighSpeed
			args.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
			args.Graphics.DrawString( text, font, b, args.CellArea.X, args.CellArea.Y );
			args.Graphics.Dispose();
		}
		
		private void initGui()
		{
			//create the layout
			VBox layout       = new VBox();
			//add the combo
			layout.PackStart( cmb, false, false, 2);
			//add the add/edit/remove buttons
			HBox hbox = new HBox();
			hbox.PackStart  ( BtnAdd    );
			hbox.PackStart  ( BtnEdit   );
			hbox.PackStart  ( BtnRemove );
			layout.PackStart( hbox, false, true, 0 );
			//add the checkbox
			hbox      = new HBox();
			hbox.PackStart( ChkOwnerDrawned );
			hbox.PackStart( ChkEditable     );
			layout.PackStart( hbox, false, true, 0 );
			//add layout
			this.Add( layout );
		}
		
	}
}
