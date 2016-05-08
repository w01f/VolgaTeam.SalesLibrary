using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	static class PageViewFactory
	{
		public static IPageView Create(LibraryPage libraryPage)
		{
			return MainController.Instance.Settings.MultitabView ?
					(IPageView)new TabPage(libraryPage) :
					new SimplePage(libraryPage);
		}
	}
}
