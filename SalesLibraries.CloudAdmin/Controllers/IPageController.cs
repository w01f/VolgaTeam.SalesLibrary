using System;

namespace SalesLibraries.CloudAdmin.Controllers
{
	interface IPageController
	{
		bool IsActive { get; set; }
		bool NeedToUpdate { get; set; }
		void InitController();
		void ShowPage(TabPageEnum pageType);
		void ProcessChanges();
		void OnLibraryChanged(object sender, EventArgs e);
	}
}
