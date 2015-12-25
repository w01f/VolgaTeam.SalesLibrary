namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class VideoLinkSettings : LibraryFileLinkSettings
	{
		private bool _forcePreview;
		public bool ForcePreview
		{
			get { return _forcePreview; }
			set
			{
				if (_forcePreview != value)
					OnSettingsChanged();
				_forcePreview = value;
			}
		}
	}
}
