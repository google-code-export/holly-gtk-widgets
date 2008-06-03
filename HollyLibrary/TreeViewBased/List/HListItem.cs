// HListItem.cs created with MonoDevelop
// User: dantes at 3:07 PM 6/3/2008
//

using System;
using Gtk;
using Gdk;

namespace HollyLibrary
{
	
	
	public class HListItem
	{
		string text;
		Pixbuf icon;
		bool _checked;
		
#region constructors
		public HListItem()
		{
		}
		
		public HListItem( String text, bool check, Pixbuf icon)
		{
			this.Text    = text;
			this.Checked = check;
			this.Icon    = icon;
		}
		
		public HListItem( String text, Pixbuf icon )
		{
			this.Text    = text;
			this.Icon    = icon;
		}
		
		public HListItem( String text, bool check)
		{
			this.Text    = text;
			this.Checked = check;
		}
#endregion
		
#region properties
		public bool Checked 
		{
			get 
			{
				return _checked;
			}
			set 
			{
				_checked = value;
			}
		}

		public Pixbuf Icon 
		{
			get 
			{
				return icon;
			}
			set 
			{
				icon = value;
			}
		}

		public string Text 
		{
			get 
			{
				return text;
			}
			set 
			{
				text = value;
			}
		}
#endregion
		
	}
}
