using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public abstract class InternalLinkInfo : BaseNetworkLinkInfo
	{
		public abstract InternalLinkType InternalLinkType { get; }
		public string LibraryName { get; set; }
	}
}
