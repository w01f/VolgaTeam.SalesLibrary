using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class VideoConvertSettings
	{
		public SettingsContainer SettingsContainer { get; set; }

		private int? _crf;
		public int? Crf
		{
			get => _crf;
			set
			{
				if (_crf != value)
					SettingsContainer.OnSettingsChanged();
				_crf = value;
			}
		}
	}
}
