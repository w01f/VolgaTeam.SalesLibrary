using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class ProgramDataSettings : SettingsContainer
	{
		private bool _enable;
		public bool Enable
		{
			get => _enable;
			set
			{
				if (_enable != value)
					OnSettingsChanged();
				_enable = value;
			}
		}

		private string _path;
		public string Path
		{
			get => _path;
			set
			{
				if (_path != value)
					OnSettingsChanged();
				_path = value;
			}
		}
	}
}
