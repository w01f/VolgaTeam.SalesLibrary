﻿using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalLibraryObjectLinkSettings : InternalLinkSettings
	{
		[JsonIgnore]
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryObject;

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

		private string _linkName;
		public string LinkName
		{
			get => _linkName;
			set
			{
				if (_linkName != value)
					OnSettingsChanged();
				_linkName = value;
			}
		}

		private string[] _thumbnailUrls;
		public string[] ThumbnailUrls
		{
			get => _thumbnailUrls;
			set
			{
				if (_thumbnailUrls != value)
					OnSettingsChanged();
				_thumbnailUrls = value;
			}
		}
	}
}
