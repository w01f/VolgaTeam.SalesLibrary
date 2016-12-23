using System;
using System.IO;
using System.Linq;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo
{
	public class UrlLinkInfo : HyperLinkInfo
	{
		public override HyperLinkTypeEnum LinkType => HyperLinkTypeEnum.Url;
		public bool ForcePreview { get; set; }

		public override void SetDefaults()
		{
			base.SetDefaults();
			ForcePreview = true;
		}

		public static UrlLinkInfo FromFile(string filePath)
		{
			var urlInfo = GetDefault<UrlLinkInfo>();
			urlInfo.Name = System.IO.Path.GetFileNameWithoutExtension(filePath);
			urlInfo.Path = File.ReadAllLines(filePath)
				.FirstOrDefault(l => l.StartsWith("URL=", StringComparison.OrdinalIgnoreCase))?.Replace("URL=", String.Empty);
			return urlInfo;
		}
	}
}
