using System.Drawing;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalLibraryPageLinkInfo : InternalLinkInfo
	{
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryPage;
		public string LibraryName { get; set; }
		public string PageName { get; set; }
		public string HeaderIcon { get; set; }
		public bool ShowHeaderText { get; set; }
		public string PageViewType { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowText { get; set; }
		public bool ShowWindowHeaders { get; set; }
		public Color? TextColor { get; set; }
		public Color? BackColor { get; set; }
	}
}
