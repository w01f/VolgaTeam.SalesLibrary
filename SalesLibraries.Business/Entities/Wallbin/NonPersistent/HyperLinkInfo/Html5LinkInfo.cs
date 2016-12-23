namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class Html5LinkInfo : HyperLinkInfo
	{
		public override HyperLinkTypeEnum LinkType => HyperLinkTypeEnum.Html5;
		public bool ForcePreview { get; set; }

		public override void SetDefaults()
		{
			base.SetDefaults();
			ForcePreview = true;
		}
	}
}
