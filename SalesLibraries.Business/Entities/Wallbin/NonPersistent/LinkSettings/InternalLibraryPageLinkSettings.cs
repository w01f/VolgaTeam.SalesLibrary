using System;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

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

		private bool _showText = true;
		public bool ShowText
		{
			get { return _showText; }
			set
			{
				if (_showText != value)
					OnSettingsChanged();
				_showText = value;
			}
		}

		private bool _showWindowHeaders = true;
		public bool ShowWindowHeaders
		{
			get { return _showWindowHeaders; }
			set
			{
				if (_showWindowHeaders != value)
					OnSettingsChanged();
				_showWindowHeaders = value;
			}
		}

		private Color? _textColor;
		public Color? TextColor
		{
			get { return _textColor; }
			set
			{
				if (_textColor != value)
					OnSettingsChanged();
				_textColor = value;
			}
		}

		private Color? _backColor;
		public Color? BackColor
		{
			get { return _backColor; }
			set
			{
				if (_backColor != value)
					OnSettingsChanged();
				_backColor = value;
			}
		}
	}
}
