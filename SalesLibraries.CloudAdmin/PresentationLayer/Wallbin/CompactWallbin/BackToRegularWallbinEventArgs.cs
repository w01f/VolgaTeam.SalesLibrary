using System;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.CompactWallbin
{
	public class BackToRegularWallbinEventArgs : EventArgs
	{
		public bool DataChanged { get; set; }
	}
}
