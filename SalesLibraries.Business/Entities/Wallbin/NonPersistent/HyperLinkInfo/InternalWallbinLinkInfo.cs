using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalWallbinLinkInfo : InternalLinkInfo
	{
		public override InternalLinkType InternalLinkType => InternalLinkType.Wallbin;
		public string LibraryName { get; set; }
		public string PageName { get; set; }
		public bool ShowHeaderText { get; set; }
		public bool OpenOnSamePage { get; set; }
		public InternalLinkTemplate StyleSettings { get; set; }
	}
}
