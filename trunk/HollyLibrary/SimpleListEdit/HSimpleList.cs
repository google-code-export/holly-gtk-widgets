// HSimpleList2.cs created with MonoDevelop
// User: dantes at 1:41 PM 5/15/2008
//

using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using HollyLibrary;

namespace HollyLibrary
{
	
	public class HSimpleList : TreeView
	{
		//my standard properties
		int itemHeight             = 25;
		ObjectCollection items     = new ObjectCollection();
		int selectedIndex          = -1;
		bool ownerDraw             = false;
		
		public event EventHandler OnSelectedIndexChanged;
		public event DrawItemEventHandler OnDrawItem;
		public event MeasureItemEventHandler OnMeasureItem;
		
		Gtk.ListStore store = new Gtk.ListStore( typeof( string ) );
		
		public HSimpleList()
		{
			
			this.HeadersVisible         = false;
			TreeViewColumn firstColumn  = new Gtk.TreeViewColumn ();
			CellRendererCustom cell     = new CellRendererCustom( this );
			this.Reorderable            = true;
			firstColumn.PackStart (cell, true);
			
			
			firstColumn.SetCellDataFunc (cell, new Gtk.TreeCellDataFunc (RenderListItem));
			
			this.Model                  = store;
			this.AppendColumn( firstColumn );
			
			
			this.Items.OnItemAdded   += new ListAddEventHandler( this.on_item_added   );
			this.items.OnItemRemoved += new ListRemoveEventHandler( this.on_item_removed );
			this.items.OnItemUpdated += new ListUpdateEventHandler( this.on_item_updated );
			this.items.OnClear       += new EventHandler( this.on_list_cleared );
		}
		
		private void RenderListItem (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CellRendererCustom mycell   = (CellRendererCustom) cell;
			mycell.ItemIndex            = int.Parse( model.GetPath(iter).ToString() );
		}

		//pot fi overrideuite pentru a crea o lista cat mai customizata
		public virtual void MeasureItem ( int ItemIndex, Widget widget, ref Gdk.Rectangle cell_area, out Gdk.Rectangle result )
		{
			//	
			MeasureItemEventArgs args = new MeasureItemEventArgs( ItemIndex, cell_area );
			
			if( ownerDraw &&  OnMeasureItem != null )
				OnMeasureItem( this, args );					
			else
				args.ItemHeight  = ItemHeight; 			
			result.X       = args.ItemLeft;
			result.Y       = args.ItemTop;
			result.Width   = args.ItemWidth;
			result.Height  = args.ItemHeight;
		}
		
		public virtual void DrawItem ( int ItemIndex, Gdk.Drawable window, Widget widget, Gdk.Rectangle background_area, Gdk.Rectangle cell_area, Gdk.Rectangle expose_area, CellRendererState flags)
		{
			//
			if( OwnerDraw && OnDrawItem != null )
			{
				DrawItemEventArgs args = new DrawItemEventArgs( ItemIndex, window, widget, background_area, cell_area, expose_area, flags );
				OnDrawItem( this, args );
			}
			else
			{
				Pango.Layout layout;
				layout       = new Pango.Layout( widget.PangoContext );
				
				layout.Width = widget.Allocation.Width * (int)Pango.Scale.PangoScale;
				String text  = Items[ItemIndex].ToString();
				layout.FontDescription = this.Style.FontDescription;
				layout.SetMarkup( text );
				
				window.DrawLayout( 
				                  widget.Style.TextGC( widget.State ),
				                  cell_area.X, cell_area.Y, layout 
				                 );
				layout.Dispose();
			}
		}
		
		public bool IsSelected( int index )
		{
			return ( SelectedIndex == index );
		}
		
		public void RemoveAllItems()
		{
			Items.Clear();
		}

		private void on_item_updated( object sender, ListUpdateEventArgs args )
		{
			//updateaza itemul din store
			Gtk.TreeIter iter;
			this.Model.GetIterFromString( out iter, args.Index.ToString() );
			this.Model.SetValue( iter, 0, args.NewValue );
		}
		
		private void on_item_added( object Sender, ListAddEventArgs args )
		{
			//adauga in store
			store.AppendValues( args.Value );
		}
		
		private void on_item_removed( object Sender, ListRemoveEventArgs args )
		{
			//sterge din store iterul
			Gtk.TreeIter iter;
			this.Model.GetIterFromString( out iter, args.Index.ToString() );
			store.Remove( ref iter );
		}
		
		private void on_list_cleared( object Sender, EventArgs args )
		{
			//sterge toate itemurile
			store.Clear();
		}

		protected override void OnRowActivated (TreePath path, TreeViewColumn column)
		{
			base.OnRowActivated (path, column);
			selectedIndex = getSelectedIndex();
			if( OnSelectedIndexChanged != null ) 
				OnSelectedIndexChanged( this, new EventArgs() );
		}
		
		protected override void OnCursorChanged ()
		{
			base.OnCursorChanged ();
			selectedIndex = getSelectedIndex();
			if( OnSelectedIndexChanged != null ) 
				OnSelectedIndexChanged( this, new EventArgs() );
		}

		public int ItemHeight 
		{
			get
			{
				return itemHeight;
			}
			set
			{
				itemHeight = value;
			}
		}

		public ObjectCollection Items 
		{
			get
			{
				return items;
			}
			set
			{
				items = value;
			}
		}

		private int getSelectedIndex()
		{
			int ret    = -1;
			TreePath[] paths = this.Selection.GetSelectedRows();
			if( paths != null && paths.Length > 0 )
			{
				 ret = int.Parse( paths[0].ToString() );
			}
			return ret;
		}
		
		public int SelectedIndex 
		{
			get 
			{
				return selectedIndex;
			}
			set
			{
				//selecteaza un anumit index
				int index     = value;
				if( index < Items.Count && index >= 0 )
				{
					this.Selection.UnselectAll();
					TreePath path = new TreePath( index.ToString() );
					this.Selection.SelectPath( path );
					this.SetCursor( path , this.Columns[0], false );
					selectedIndex = index;
				}
			}
		}

		public object SelectedItem 
		{
			get 
			{
				object ret = null;
				int index  = SelectedIndex;
				if( SelectedIndex != -1 ) ret = Items[index];
				return ret;
			}
			set 
			{
				//ciclez printre itemuri sa il selectez pe cel ales
				for( int i = 0; i < Items.Count; i++ )
				{
					object obj = Items[i];
					if( value.Equals( obj ) )
					{
						SelectedIndex = i;
						break;
					}	
				}
				//
			}
		}


		public string Text 
		{
			get 
			{
				String ret = "";
				if( SelectedItem != null ) ret = SelectedItem.ToString();
				return ret;
			}
			set 
			{
				//ciclez printre itemuri sa il selectez pe cel ales
				for( int i = 0; i < Items.Count; i++ )
				{
					object obj = Items[i];
					if( value.Equals( obj.ToString() ) )
					{
						SelectedIndex = i;
						break;
					}	
				}
				//
			}
		}

		public bool OwnerDraw {
			get {
				return ownerDraw;
			}
			set {
				ownerDraw = value;
			}
		}
		
	}
}
