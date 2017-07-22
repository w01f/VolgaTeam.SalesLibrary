using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class VideoConvertSettings
	{
		public const int DefaultCrf = 29;

		public SettingsContainer SettingsContainer { get; set; }

		private int? _crf;
		public int? Crf
		{
			get { return _crf; }
			set
			{
				if (_crf != value)
					SettingsContainer.OnSettingsChanged();
				_crf = value;
			}
		}
	}
}
