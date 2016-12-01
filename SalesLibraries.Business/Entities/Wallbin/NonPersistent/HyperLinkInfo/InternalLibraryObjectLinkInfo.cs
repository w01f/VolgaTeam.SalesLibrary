using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalLibraryObjectLinkInfo : InternalLinkInfo
	{
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryObject;
		public string LibraryName { get; set; }
		public string PageName { get; set; }
		public string WindowName { get; set; }
		public string LinkName { get; set; }
	}
}
