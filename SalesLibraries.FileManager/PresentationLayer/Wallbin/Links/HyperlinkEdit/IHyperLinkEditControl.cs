﻿using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public interface IHyperLinkEditControl
	{
		bool ValidateLinkInfo();
		BaseNetworkLinkInfo PrepareHyperLinkInfo();
		BaseNetworkLinkInfo GetFinalHyperLinkInfo();
		void ApplyDataFromTemplate(BaseNetworkLinkInfo templateInfo);
	}
}
