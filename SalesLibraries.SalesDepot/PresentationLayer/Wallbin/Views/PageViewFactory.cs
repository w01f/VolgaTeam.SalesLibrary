using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	static class PageViewFactory
	{
		public static IPageView Create(LibraryPage libraryContext)
		{
			return MainController.Instance.Settings.WallbinViewSettings.MultitabView ?
					(IPageView)new TabPage(libraryContext) :
					new SimplePage(libraryContext);
		}
	}
}
