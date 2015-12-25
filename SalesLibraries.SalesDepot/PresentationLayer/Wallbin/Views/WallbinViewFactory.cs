using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	static class WallbinViewFactory
	{
		public static IWallbinView Create(LibraryContext libraryContext)
		{
			return MainController.Instance.Settings.WallbinViewSettings.MultitabView ?
					(IWallbinView)new TabbedWallbin(libraryContext) :
					new SimpleWallbin(libraryContext);
		}
	}
}
