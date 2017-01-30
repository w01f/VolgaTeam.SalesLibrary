using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	abstract class BaseLibraryObjectLoader : BaseLinkLoader<LibraryObjectLink>
	{
		protected BaseLibraryObjectLoader(BaseContextMenuEditor editor, IList<LibraryObjectLink> targetLinks) : 
			base(editor, targetLinks)
		{
		}
	}
}
