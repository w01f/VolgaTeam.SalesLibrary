using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	abstract class BaseLibraryObjectsLoader : BaseLinksLoader<LibraryObjectLink>
	{
		protected BaseLibraryObjectsLoader(BaseContextMenuEditor editor, IEnumerable<LibraryObjectLink> targetLinks) :
			base(editor, targetLinks)
		{
		}
	}
}
