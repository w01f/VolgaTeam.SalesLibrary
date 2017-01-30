using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	abstract class BaseLineBreakLoader : BaseLinkLoader<LineBreak>
	{
		protected BaseLineBreakLoader(BaseContextMenuEditor editor, IList<LineBreak> targetLinks) : 
			base(editor, targetLinks)
		{
		}
	}
}
