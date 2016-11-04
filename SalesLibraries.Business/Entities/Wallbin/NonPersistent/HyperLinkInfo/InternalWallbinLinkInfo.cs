using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalWallbinLinkInfo : InternalLinkInfo
	{
		public override InternalLinkType InternalLinkType => InternalLinkType.Wallbin;

		public string PageName { get; set; }
		public string HeaderIcon { get; set; }
		public bool ShowHeaderText { get; set; }
		public string PageViewType { get; set; }
		public string PageSelectorType { get; set; }
		public bool ShowLogo { get; set; }
	}
}
