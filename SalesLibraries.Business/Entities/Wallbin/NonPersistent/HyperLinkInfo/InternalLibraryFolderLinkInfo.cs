using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalLibraryFolderLinkInfo : InternalLinkInfo
	{
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryFolder;
		public string LibraryName { get; set; }
		public string PageName { get; set; }
		public string WindowName { get; set; }
		public string HeaderIcon { get; set; }
		public bool ShowHeaderText { get; set; }
		public int Column { get; set; }
		public string WindowViewType { get; set; }
		public bool LinksOnly { get; set; }
	}
}
