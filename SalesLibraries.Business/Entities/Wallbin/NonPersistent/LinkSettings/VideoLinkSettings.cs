using System;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class VideoLinkSettings : LibraryFileLinkSettings
	{
		private bool _forcePreview;
		public bool ForcePreview
		{
			get => _forcePreview;
			set
			{
				if (_forcePreview != value)
					OnSettingsChanged();
				_forcePreview = value;
			}
		}

		private bool _downloadSource;
		public bool DownloadSource
		{
			get => _downloadSource;
			set
			{
				if (_downloadSource != value)
					OnSettingsChanged();
				_downloadSource = value;
			}
		}
	}
}
