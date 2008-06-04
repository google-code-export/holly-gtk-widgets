// HSimpleList2.cs created with MonoDevelop
// User: dantes at 1:41 PM 5/15/2008
//

using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using HollyLibrary;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HollyLibrary
{

	public class HSimpleList : TreeView, IBaseListWidget, ICustomCellTreeView
	{
		//my standard properties
		int itemHeight                 = 25;
		ObjectCollection items         = new ObjectCollection();
		int selectedIndex              = -1;
		bool ownerDraw                 = false;
		//cells
		CellRendererToggle chk_cell  = new CellRendererToggle();
		CellRendererCustom text_cell;
		
		//events
		public event EventHandler            SelectedIndexChanged;
		public event DrawItemEventHandler    DrawItem;
		public event MeasureItemEventHandler MeasureItem;
		//TODO: new 2.0 event,add to wiki
		public event ListItemCheck           ItemCheck;
		
		//store
		Gtk.ListStore store = new Gtk.ListStore( typeof(bool), typeof( string ) );
		
		//checked items list
		List<int> checked_items = new List<int>();
		
		public HSimpleList()
		{
			this.HeadersVisible         = false;
			this.Reorderable            = true;
			
			//add columns
			constructColumns();
			//set model
			this.Model                  = getInnerListStore();
			
			//inner objectcollection events
			this.Items.OnItemAdded   += new ListAddEventHandler   ( this.on_item_added   );
			this.items.OnItemRemoved += new ListRemoveEventHandler( this.on_item_removed );
			this.items.OnItemUpdated += new ListUpdateEventHandler( this.on_item_updated );
			
		}
		
		internal virtual void RenderListItem (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CellRendererCustom mycell   = (CellRendererCustom) cell;
			mycell.ItemIndex            = int.Parse( model.GetPath(iter).ToString() );
		}

		//pot fi overrideuite pentru a crea o lista cat mai customizata
		public virtual void OnMeasureItem ( int ItemIndex, Widget widget, ref Gdk.Rectangle cell_area, out Gdk.Rectangle result )
		{
			//	
			MeasureItemEventArgs args = new MeasureItemEventArgs( ItemIndex, cell_area );
			
			if( ownerDraw &&  MeasureItem != null )
				MeasureItem( this, args );					
			else
				args.ItemHeight  = ItemHeight; 			
			result.X       = args.ItemLeft;
			result.Y       = args.ItemTop;
			result.Width   = args.ItemWidth;
			result.Height  = args.ItemHeight;
		}
		
		public virtual void OnDrawItem ( int ItemIndex, Gdk.Drawable window, Widget widget, Gdk.Rectangle background_area, Gdk.Rectangle cell_area, Gdk.Rectangle expose_area, CellRendererState flags)
		{
			DrawItemEventArgs args = new DrawItemEventArgs( ItemIndex, window, widget, background_area, cell_area, expose_area, flags );
			//
			if( OwnerDraw && DrawItem != null )
			{
				DrawItem( this, args );
			}
			else
			{				
				String text      = Items[ItemIndex].ToString();
				//take font from style
				Font font        = new Font( Style.FontDesc.Family , Style.FontDesc.Size / 1000 );
				// take color from style
				Gdk.Color gcolor = Style.Foreground( StateType.Normal );
				Color c          = Color.FromArgb( gcolor.Red, gcolor.Green, gcolor.Blue );
				Brush b          = new SolidBrush( c );
				//set quality to HighSpeed
				args.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
				args.Graphics.DrawString( text, font, b, args.CellArea.X, args.CellArea.Y );
				args.Graphics.Dispose();
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
			//remove the checked index if it's in the checked_items
			checked_items.Remove( args.Index );
			//change the value
			this.Model.SetValue         ( iter, 1, args.NewValue );
			this.QueueDraw();
		}
		
		private void on_item_added( object Sender, ListAddEventArgs args )
		{
			//adauga in store
			store.AppendValues( false, args.Value );
			this.QueueDraw();
		}
		
		private void on_item_removed( object Sender, ListRemoveEventArgs args )
		{
			//sterge din store iterul
			Gtk.TreeIter iter;
			this.Model.GetIterFromString( out iter, args.Index.ToString() );
			store.Remove( ref iter );
			//remove the index from the checked_items also
			checked_items.Remove( args.Index );
			this.QueueDraw();
		}
	
		protected override void OnRowActivated (TreePath path, TreeViewColumn column)
		{
			base.OnRowActivated (path, column);
			selectedIndex = getSelectedIndex();
			if( SelectedIndexChanged != null ) 
				SelectedIndexChanged( this, new EventArgs() );
		}
		
		protected override void OnCursorChanged ()
		{
			base.OnCursorChanged ();
			selectedIndex = getSelectedIndex();
			if( SelectedIndexChanged != null ) 
				SelectedIndexChanged( this, new EventArgs() );
		}
		
		public void Sort()
		{
			Items.Sort();
			store.Clear();
			//clear the checkboxes also
			checked_items.Clear();
			//add all values again
			foreach( object obj in Items ) store.AppendValues( false, obj );
		}
		
		public void Sort( IComparer comparer )
		{
			Items.Sort( comparer );
			store.Clear();
			//clear the checkboxes also
			checked_items.Clear();
			//add all values again
			foreach( object obj in Items ) store.AppendValues( false, obj );
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
		
		public int[] getSelectedIndexes()
		{
			TreePath[] paths = this.Selection.GetSelectedRows();
			int[] ret        = new int[ paths.Length ];
			for( int i = 0; i < paths.Length; i++ )
			{
				TreePath path = paths[i];
				ret[i]        =  int.Parse( path.ToString() );
				Console.WriteLine( "selectat->"+ret[i] );
			}
			return ret;
		}
		
		public object[] getSelectedItems()
		{
			int[] indexes  = getSelectedIndexes();
			object[] ret   = new object[ indexes.Length ];
			for( int i = 0; i< indexes.Length; i++ )
			{
				ret[i] = Items[ indexes[i] ];
			}
			return ret;
		}

		public ListStore getInnerListStore ()
		{
			return store;
		}

		public void constructColumns ()
		{
			TreeViewColumn column        = new Gtk.TreeViewColumn ();
			//create the text_cell
			text_cell = new CellRendererCustom( this );
			//
			chk_cell.Visible             = false;
			chk_cell.Toggled            += OnCelltoggled;
			//add cells to column
			column.PackStart( chk_cell , false );
			column.PackStart( text_cell, true  );
			//set data functions
			column.SetCellDataFunc( text_cell, new Gtk.TreeCellDataFunc ( RenderListItem ) );
			column.SetCellDataFunc( chk_cell , new TreeCellDataFunc     ( OnChkDataFunc  ) );
			//add the column
			AppendColumn( column );
		}
		
		//checkbox cell function
		private void OnChkDataFunc(
		                        Gtk.TreeViewColumn col, Gtk.CellRenderer cell,
		                        Gtk.TreeModel model, Gtk.TreeIter iter
		                            )
		{
			CellRendererToggle c = cell as CellRendererToggle;
			String path = model.GetPath(iter).ToString();

			int item_index = int.Parse( path );
			c.Active       = ( checked_items.IndexOf( item_index ) != -1 );
		}
		
		private void OnCelltoggled( object sender, ToggledArgs args )
		{
            TreeIter iter;
            if ( store.GetIterFromString( out iter, args.Path ) )
            {
				int item_index     = int.Parse( args.Path  );
				//if it's checked, remove the check index
				//else, add it
				bool new_value = ( checked_items.IndexOf( item_index ) != -1 );
				if( new_value )
					checked_items.Remove( item_index );
				else
					checked_items.Add( item_index );
				//raise itemcheck event
				if( ItemCheck != null ) 
					ItemCheck( this, new ListItemCheckEventArgs( item_index, new_value ) ); 
			}

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
		
		/// <value>
		/// returns the checked items list
		/// TODO: add to wiki
		/// </value>
		public List<System.Object> CheckedItems
		{
			get
			{
				List<System.Object> ret = new List<System.Object>();
				foreach( int index in checked_items )
				{
					ret.Add( Items[ index ] );
				}
				return ret;
			}
		}
		
		/// <value>
		/// returns the checked items indexes as a int list
		/// TODO: add to wiki
		/// </value>
		public List<int> CheckedItemIndexes
		{
			get
			{
				return checked_items;
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

		public bool OwnerDraw 
		{
			get 
			{
				return ownerDraw;
			}
			set 
			{
				ownerDraw = value;
				QueueDraw();
			}
		}

		public SelectionMode SelectionType 
		{
			get 
			{
				
				return this.Selection.Mode;
			}
			set 
			{
				this.Selection.Mode = value;
			}
		}
		
		public bool IsCheckBoxList 
		{
			get 
			{
				return chk_cell.Visible ;
			}
			set 
			{
				chk_cell.Visible = value;
				Columns[0].QueueResize();
				QueueDraw();
			}
		}
		
	}
}
