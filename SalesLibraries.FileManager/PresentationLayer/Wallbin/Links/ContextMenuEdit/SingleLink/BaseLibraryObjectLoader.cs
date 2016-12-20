using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	abstract class BaseLibraryObjectLoader : BaseLinkLoader<LibraryObjectLink>
	{
		protected BaseLibraryObjectLoader(BaseContextMenuEditor editor, LibraryObjectLink targetLink) : 
			base(editor, targetLink)
		{
		}
	}
}
