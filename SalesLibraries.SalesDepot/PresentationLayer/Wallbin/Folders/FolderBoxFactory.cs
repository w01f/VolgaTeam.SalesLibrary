using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Folders
{
	static class FolderBoxFactory
	{
		public static BaseFolderBox Create(LibraryFolder dataSource)
		{
			if (MainController.Instance.WallbinViews.FormatState.AccordionView)
				return new AccordionFolderBox(dataSource);
			return new ClassicFolderBox(dataSource);
		}
	}
}
