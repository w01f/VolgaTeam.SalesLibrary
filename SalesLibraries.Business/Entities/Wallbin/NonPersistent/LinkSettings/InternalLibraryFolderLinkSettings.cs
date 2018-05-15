using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalLibraryFolderLinkSettings : InternalLinkSettings
	{
		[JsonIgnore]
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryFolder;

		private string _libraryName;
		public string LibraryName
		{
			get => _libraryName;
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
			get => _pageName;
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
			get => _windowName;
			set
			{
				if (_windowName != value)
					OnSettingsChanged();
				_windowName = value;
			}
		}

		private bool _showHeaderText = true;
		public bool ShowHeaderText
		{
			get => _showHeaderText;
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
			get => _openOnSamePage;
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
			get => _styleSettings;
			set
			{
				if (_styleSettings != value)
					OnSettingsChanged();
				_styleSettings = value;
			}
		}
	}
}
