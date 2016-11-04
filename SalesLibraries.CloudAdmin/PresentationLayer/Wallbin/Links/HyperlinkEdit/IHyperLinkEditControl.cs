using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public interface IHyperLinkEditControl
	{
		bool ValidateLinkInfo();
		BaseNetworkLinkInfo GetHyperLinkInfo();
		void ApplySharedSettings(BaseNetworkLinkInfo templateInfo);
	}
}
