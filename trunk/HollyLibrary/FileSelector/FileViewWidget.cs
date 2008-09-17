// FileViewWidget.cs created with MonoDevelop
// User: dantes at 1:58 PM 9/15/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.IO;
using Gtk;

namespace HollyLibrary
{

	public enum FileViewTypes
	{
		Thumbnail, Tiles, List
	}
	
	public class FileViewWidget : Gtk.Bin
	{
		ScrolledWindow view    = new ScrolledWindow();
		FileViewTypes viewType = FileViewTypes.List;
		String currentPath     = "";
		
		public FileViewWidget()
		{
			this.Add( view );
		}
		
		public void Renderer()
		{
			//daca este List
			if( ViewType == FileViewTypes.List )
			{
				//calculeaza nr de randuri in functie de height
				//calculeaza nr de coloane
			}
		}
		
		public FileViewTypes ViewType 
		{
			get 
			{
				return viewType;
			}
			set 
			{
				viewType = value;
			}
		}

		public string CurrentPath 
		{
			get 
			{
				return currentPath;
			}
			set 
			{
				currentPath = value;
			}
		}
	}
}
