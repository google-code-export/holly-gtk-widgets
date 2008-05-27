// TreePopupDialog.cs created with MonoDevelop
// User: dantes at 6:42 PM 5/26/2008
//

using System;

namespace HollyLibrary
{
	
	
	public partial class TreePopupDialog : Gtk.Window
	{
		HComboTree father;
		
		public TreePopupDialog( HComboTree father ) : 
				base(Gtk.WindowType.Popup)
		{
			this.father = father;
			this.Build();
		}
		
		internal HTreeView TreeView
		{
			get
			{
				return this.Tree;
			}
		}
		
		public void ShowMe( Gdk.Rectangle rect )
		{
			Move  ( rect.X    , rect.Y      );
			Resize( rect.Width, rect.Height );
			
			this.Visible = true;
			this.ShowAll();
			
			GrabUtil.GrabWindow( this );
			Tree.GrabFocus();
		}

		public void Close()
		{
			GrabUtil.RemoveGrab(this);
			Hide();
		}

		protected virtual void OnTreeButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			args.RetVal = true;
		}

		protected virtual void OnButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			Close();
		}

		protected virtual void OnTreeRowActivated (object o, Gtk.RowActivatedArgs args)
		{
			father.Text = Tree.SelectedNode.Text;
			Close();
		}
		
	}
}
