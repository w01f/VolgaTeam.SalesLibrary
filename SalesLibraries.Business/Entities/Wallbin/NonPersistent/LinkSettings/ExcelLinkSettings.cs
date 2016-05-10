namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class ExcelLinkSettings : LibraryFileLinkSettings
	{
		private bool _generateContentText;
		public bool GenerateContentText
		{
			get { return _generateContentText; }
			set
			{
				if (_generateContentText != value)
					OnSettingsChanged();
				_generateContentText = value;
			}
		}

		private bool _forceDownload;
		public bool ForceDownload
		{
			get { return _forceDownload; }
			set
			{
				if (_forceDownload != value)
					OnSettingsChanged();
				_forceDownload = value;
			}
		}

		private bool _forceOpen;
		public bool ForceOpen
		{
			get { return _forceOpen; }
			set
			{
				if (_forceOpen != value)
					OnSettingsChanged();
				_forceOpen = value;
			}
		}

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_generateContentText = false;
		}
	}
}
