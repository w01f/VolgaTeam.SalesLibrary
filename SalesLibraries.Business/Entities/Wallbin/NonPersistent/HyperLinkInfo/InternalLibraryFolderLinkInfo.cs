using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalLibraryFolderLinkInfo : InternalLinkInfo
	{
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryFolder;
		public string LibraryName { get; set; }
		public string PageName { get; set; }
		public string WindowName { get; set; }
		public bool ShowHeaderText { get; set; }
		public bool OpenOnSamePage { get; set; }
		public InternalLinkTemplate StyleSettings { get; set; }
	}
}
