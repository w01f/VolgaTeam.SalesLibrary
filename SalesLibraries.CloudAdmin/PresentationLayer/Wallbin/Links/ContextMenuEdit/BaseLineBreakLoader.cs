using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	abstract class BaseLineBreakLoader : BaseLinkLoader<LineBreak>
	{
		protected BaseLineBreakLoader(BaseContextMenuEditor editor, LineBreak targetLink) : 
			base(editor, targetLink)
		{
		}
	}
}
