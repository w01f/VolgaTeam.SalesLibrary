using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalWallbinLinkSettings : InternalLinkSettings
	{
		[JsonIgnore]
		public override InternalLinkType InternalLinkType => InternalLinkType.Wallbin;

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

		private string _headerIcon;
		public string HeaderIcon
		{
			get { return _headerIcon; }
			set
			{
				if (_headerIcon != value)
					OnSettingsChanged();
				_headerIcon = value;
			}
		}

		private bool _showHeaderText = true;
		public bool ShowHeaderText
		{
			get { return _showHeaderText; }
			set
			{
				if (_showHeaderText != value)
					OnSettingsChanged();
				_showHeaderText = value;
			}
		}

		private string _pageViewType = InternalLinkSettings.PageViewTypeColumns;
		public string PageViewType
		{
			get { return _pageViewType; }
			set
			{
				if (_pageViewType != value)
					OnSettingsChanged();
				_pageViewType = value;
			}
		}

		private string _pageSelectorType = InternalLinkSettings.PageSelectorTypeTabs;
		public string PageSelectorType
		{
			get { return _pageSelectorType; }
			set
			{
				if (_pageSelectorType != value)
					OnSettingsChanged();
				_pageSelectorType = value;
			}
		}

		private bool _showLogo = true;
		public bool ShowLogo
		{
			get { return _showLogo; }
			set
			{
				if (_showLogo != value)
					OnSettingsChanged();
				_showLogo = value;
			}
		}
	}
}
