using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public interface IHyperLinkEditControl
	{
		bool ValidateLinkInfo();
		BaseNetworkLink GetHyperLinkInfo();
		void ApplySharedSettings(BaseNetworkLink templateEditor);
	}
}
