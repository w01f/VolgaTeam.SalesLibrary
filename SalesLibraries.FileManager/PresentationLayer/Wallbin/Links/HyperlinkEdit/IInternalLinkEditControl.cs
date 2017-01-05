using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public interface IInternalLinkEditControl
	{
		void InitControl();
		bool ValidateLinkInfo();
		InternalLinkInfo GetHyperLinkInfo();
		void ApplySharedSettings(InternalLinkInfo templateInfo);
	}
}
