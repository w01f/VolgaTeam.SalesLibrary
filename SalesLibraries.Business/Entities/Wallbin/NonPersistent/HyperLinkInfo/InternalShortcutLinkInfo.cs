using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalShortcutLinkInfo : InternalLinkInfo
	{
		public override InternalLinkType InternalLinkType => InternalLinkType.Shortcut;
		public string ShortcutId { get; set; }
		public bool OpenOnSamePage { get; set; }
	}
}
