using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class OneDriveLibrarySettings
	{
		public SettingsContainer SettingsContainer { get; set; }

		private bool _enableSync;
		public bool EnableSync
		{
			get => _enableSync;
			set
			{
				if (_enableSync != value)
					SettingsContainer.OnSettingsChanged();
				_enableSync = value;
			}
		}

		private string _token;
		public string Token
		{
			get => _token;
			set
			{
				if (_token != value)
					SettingsContainer.OnSettingsChanged();
				_token = value;
			}
		}

		private string _rootPath;
		public string RootPath
		{
			get => _rootPath;
			set
			{
				if (_rootPath != value)
					SettingsContainer.OnSettingsChanged();
				_rootPath = value;
			}
		}
	}
}
