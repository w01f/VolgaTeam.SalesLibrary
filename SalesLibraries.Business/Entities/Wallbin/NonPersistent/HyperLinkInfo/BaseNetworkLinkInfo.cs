using System;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public abstract class BaseNetworkLinkInfo
	{
		public abstract HyperLinkTypeEnum LinkType { get; }
		public string Name { get; set; }
		public bool FormatAsBluelink { get; set; }
		public bool FormatBold { get; set; }

		public static TLinkInfo GetDefault<TLinkInfo>() where TLinkInfo : BaseNetworkLinkInfo
		{
			var linkInfo = Activator.CreateInstance<TLinkInfo>();
			linkInfo.FormatAsBluelink = true;
			return linkInfo;
		}
	}
}
