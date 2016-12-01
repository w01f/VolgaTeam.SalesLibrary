using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public abstract class InternalLinkInfo : BaseNetworkLinkInfo
	{
		public override HyperLinkTypeEnum LinkType => HyperLinkTypeEnum.Internal;
		public abstract InternalLinkType InternalLinkType { get; }
	}
}
