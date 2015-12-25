using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	static class WallbinViewFactory
	{
		public static IWallbinView Create(LibraryContext libraryContext)
		{
			return MainController.Instance.Settings.MultitabView ?
				(IWallbinView)new TabbedWallbin(libraryContext) :
				new SimpleWallbin(libraryContext);
		}
	}
}
