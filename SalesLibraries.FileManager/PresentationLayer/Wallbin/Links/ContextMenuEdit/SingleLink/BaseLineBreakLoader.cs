﻿using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	abstract class BaseLineBreakLoader : BaseLinkLoader<LineBreak>
	{
		protected BaseLineBreakLoader(BaseContextMenuEditor editor, LineBreak targetLink) : 
			base(editor, targetLink)
		{
		}
	}
}