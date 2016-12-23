namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class QuickSiteLinkInfo : HyperLinkInfo
	{
		public override HyperLinkTypeEnum LinkType => HyperLinkTypeEnum.QuickSite;
		public bool ForcePreview { get; set; }

		public override void SetDefaults()
		{
			base.SetDefaults();
			ForcePreview = true;
		}
	}
}
