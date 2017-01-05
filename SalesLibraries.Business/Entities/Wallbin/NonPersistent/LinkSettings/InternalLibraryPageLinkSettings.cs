using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalLibraryPageLinkSettings : InternalLinkSettings
	{
		[JsonIgnore]
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryPage;

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

		private bool _openOnSamePage;
		public bool OpenOnSamePage
		{
			get { return _openOnSamePage; }
			set
			{
				if (_openOnSamePage != value)
					OnSettingsChanged();
				_openOnSamePage = value;
			}
		}

		private InternalLinkTemplate _styleSettings;
		public InternalLinkTemplate StyleSettings
		{
			get { return _styleSettings; }
			set
			{
				if (_styleSettings != value)
					OnSettingsChanged();
				_styleSettings = value;
			}
		}
	}
}
