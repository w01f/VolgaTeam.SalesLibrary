namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class VimeoLinkInfo : HyperLinkInfo
	{
		public override HyperLinkTypeEnum LinkType => HyperLinkTypeEnum.Vimeo;
		public bool ForcePreview { get; set; }
	}
}
