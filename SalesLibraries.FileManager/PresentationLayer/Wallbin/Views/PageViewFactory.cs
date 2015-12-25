using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
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
