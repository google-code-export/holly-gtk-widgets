// AbstractBaseList.cs created with MonoDevelop
// User: dantes at 8:43 PM 6/3/2008
//

using System;
using Gtk;

namespace HollyLibrary
{
	
	
	public interface IBaseListWidget
	{
		
		ListStore getInnerListStore();
		
		void constructColumns();
		
	}
}
