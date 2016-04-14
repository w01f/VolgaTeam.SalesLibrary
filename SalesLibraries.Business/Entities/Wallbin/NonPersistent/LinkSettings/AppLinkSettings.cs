namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class AppLinkSettings : LibraryObjectLinkSettings
	{
		private string _secondPath;
		public string SecondPath
		{
			get { return _secondPath; }
			set
			{
				if (_secondPath != value)
					OnSettingsChanged();
				_secondPath = value;
			}
		}
	}
}
