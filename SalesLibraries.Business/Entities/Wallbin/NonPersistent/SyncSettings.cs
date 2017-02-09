using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class SyncSettings : SettingsContainer
	{
		private bool _minimizeOnSync;
		public bool MinimizeOnSync
		{
			get { return _minimizeOnSync; }
			set
			{
				if (_minimizeOnSync != value)
					OnSettingsChanged();
				_minimizeOnSync = value;
			}
		}

		private bool _closeAfterSync;
		public bool CloseAfterSync
		{
			get { return _closeAfterSync; }
			set
			{
				if (_closeAfterSync != value)
					OnSettingsChanged();
				_closeAfterSync = value;
			}
		}
	}
}
