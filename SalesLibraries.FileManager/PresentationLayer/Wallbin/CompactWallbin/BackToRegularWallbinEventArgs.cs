using System;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin
{
	public class BackToRegularWallbinEventArgs : EventArgs
	{
		public bool DataChanged { get; set; }
	}
}
