using Gtk;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using HollyLibrary;

namespace test
{
	
	public class SimpleList : Window
	{
		//the list widget
		HSimpleList list = new HSimpleList();
		//buttons
		Button BtnAdd    = new Button("Add item");
		Button BtnRemove = new Button("Remove item");
		Button BtnEdit   = new Button("Edit item");
		//checkboxes
		CheckButton ChkOwnerDrawned = new CheckButton("List is ownerdrawed");
		
		public SimpleList() : base( WindowType.Toplevel )
		{
			this.Title          = "HSimpleList demo";
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
			//ownerdrawned event
			list.DrawItem           += OnItemDraw;
		}
		
		private void OnBtnAddClicked( object sender, EventArgs args )
		{
			//add a list item
			list.Items.Add("new item !");
		}
		
		private void OnBtnEditClicked( object sender, EventArgs args )
		{
			//change the current selected item's text
			if( list.SelectedIndex != -1 )
			{
				list.Items[ list.SelectedIndex ] = "i'm modified";
			}
		}
		
		private void OnBtnRemoveClicked( object sender, EventArgs args )
		{
			//remove the selected item
			if( list.SelectedIndex != -1 )
				list.Items.RemoveAt( list.SelectedIndex );
			
		}
		
		private void OnOwnerDrawnedChecked( object sender, EventArgs args )
		{
			list.OwnerDraw      = ChkOwnerDrawned.Active;
		}
		
		private void OnItemDraw( object sender, DrawItemEventArgs args )
		{
			String text      = list.Items[ args.ItemIndex ].ToString();
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
			//add the list
			ScrolledWindow sw = new ScrolledWindow();
			sw.AddWithViewport( list );
			layout.PackStart  ( sw   );
			//add the add/edit/remove buttons
			HBox hbox = new HBox();
			hbox.PackStart  ( BtnAdd    );
			hbox.PackStart  ( BtnEdit   );
			hbox.PackStart  ( BtnRemove );
			layout.PackStart( hbox, false, true, 0 );
			//add the checkbox
			layout.PackStart( ChkOwnerDrawned, false, true, 0 );
			//add layout
			this.Add( layout );
		}
		
	}
}
