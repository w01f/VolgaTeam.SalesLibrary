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

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_generateContentText = false;
		}
	}
}
