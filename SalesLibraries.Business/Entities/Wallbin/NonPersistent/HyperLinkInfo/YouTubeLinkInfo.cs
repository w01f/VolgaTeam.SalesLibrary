namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class YouTubeLinkInfo : HyperLinkInfo
	{
		public override HyperLinkTypeEnum LinkType => HyperLinkTypeEnum.YouTube;
		public bool ForcePreview { get; set; }
	}
}
