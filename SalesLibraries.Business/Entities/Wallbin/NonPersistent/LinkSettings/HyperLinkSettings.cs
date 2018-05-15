namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class HyperLinkSettings : LibraryObjectLinkSettings
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
	}
}
