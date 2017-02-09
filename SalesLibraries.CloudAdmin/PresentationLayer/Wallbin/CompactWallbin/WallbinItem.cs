using System;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.CompactWallbin
{
	class WallbinItem
	{
		public WallbinItemType Type { get; set; }
		public string Name { get; set; }
		public Object Source { get; set; }
	}
}
