namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class WebLinkSettings : LibraryObjectLinkSettings
	{
		private bool _isUrl365;
		public bool IsUrl365
		{
			get { return _isUrl365; }
			set
			{
				if (_isUrl365 != value)
					OnSettingsChanged();
				_isUrl365 = value;
			}
		}

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
