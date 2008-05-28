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

	public class HSimpleList : TreeView
	{
		//my standard properties
		int itemHeight                 = 25;
		ObjectCollection items         = new ObjectCollection();
		int selectedIndex              = -1;
		bool ownerDraw                 = false;
		
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
			DrawItemEventArgs args = new DrawItemEventArgs( ItemIndex, window, widget, background_area, cell_area, expose_area, flags );
			//
			if( OwnerDraw && OnDrawItem != null )
			{
				OnDrawItem( this, args );
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
			this.Model.SetValue( iter, 0, args.NewValue );
			this.QueueDraw();
		}
		
		private void on_item_added( object Sender, ListAddEventArgs args )
		{
			//adauga in store
			store.AppendValues( args.Value );
			this.QueueDraw();
		}
		
		private void on_item_removed( object Sender, ListRemoveEventArgs args )
		{
			//sterge din store iterul
			Gtk.TreeIter iter;
			this.Model.GetIterFromString( out iter, args.Index.ToString() );
			store.Remove( ref iter );
			this.QueueDraw();
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
		
		public void Sort()
		{
			Items.Sort();
			store.Clear();
			foreach( object obj in Items ) store.AppendValues( obj );
		}
		
		public void Sort( IComparer comparer )
		{
			Items.Sort( comparer );
			store.Clear();
			foreach( object obj in Items ) store.AppendValues( obj );
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
		
		private int[] getSelectedIndexes()
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
		
	}
}
