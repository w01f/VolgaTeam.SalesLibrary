namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class InternalLinkInfo : BaseNetworkLink
	{
		public string LibraryName { get; set; }
		public string PageName { get; set; }
		public string WindowName { get; set; }
		public string LinkName { get; set; }
		public bool ForcePreview { get; set; }
	}
}
