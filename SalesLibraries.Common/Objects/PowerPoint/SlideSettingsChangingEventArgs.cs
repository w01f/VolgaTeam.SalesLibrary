using System;

namespace SalesLibraries.Common.Objects.PowerPoint
{
	public class SlideSettingsChangingEventArgs : EventArgs
	{
		public bool Cancel { get; set; }

		public SlideSettingsChangingEventArgs()
		{
			Cancel = false;
		}
	}
}
