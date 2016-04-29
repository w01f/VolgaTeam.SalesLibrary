namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalLinkSettings : LibraryObjectLinkSettings
	{
		private string _libraryName;
		public string LibraryName
		{
			get { return _libraryName; }
			set
			{
				if (_libraryName != value)
					OnSettingsChanged();
				_libraryName = value;
			}
		}

		private string _pageName;
		public string PageName
		{
			get { return _pageName; }
			set
			{
				if (_pageName != value)
					OnSettingsChanged();
				_pageName = value;
			}
		}

		private string _windowName;
		public string WindowName
		{
			get { return _windowName; }
			set
			{
				if (_windowName != value)
					OnSettingsChanged();
				_windowName = value;
			}
		}

		private string _linkName;
		public string LinkName
		{
			get { return _linkName; }
			set
			{
				if (_linkName != value)
					OnSettingsChanged();
				_linkName = value;
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
