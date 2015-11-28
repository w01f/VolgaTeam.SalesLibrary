using System;

namespace SalesLibraries.SalesDepot.Controllers
{
	interface IPageController
	{
		bool IsActive { get; set; }
		bool NeedToUpdate { get; set; }
		void InitController();
		void ShowPage(TabPageEnum pageType);
		void OnLibraryChanged(object sender, EventArgs e);
	}
}
