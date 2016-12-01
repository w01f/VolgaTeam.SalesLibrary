using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalLibraryFolderLinkSettings : InternalLinkSettings
	{
		[JsonIgnore]
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryFolder;

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

		private int _column;
		public int Column
		{
			get { return _column; }
			set
			{
				if (_column != value)
					OnSettingsChanged();
				_column = value;
			}
		}

		private string _windowViewType = InternalLinkSettings.PageViewTypeColumns;
		public string WindowViewType
		{
			get { return _windowViewType; }
			set
			{
				if (_windowViewType != value)
					OnSettingsChanged();
				_windowViewType = value;
			}
		}

		private bool _linksOnly;
		public bool LinksOnly
		{
			get { return _linksOnly; }
			set
			{
				if (_linksOnly != value)
					OnSettingsChanged();
				_linksOnly = value;
			}
		}
	}
}
