﻿using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class OneDriveLinkSettings : SettingsContainer
	{
		private string _itemId;
		public string ItemId
		{
			get => _itemId;
			set
			{
				if (_itemId != value)
					OnSettingsChanged();
				_itemId = value;
			}
		}

		private string _urlId;
		public string UrlId
		{
			get => _urlId;
			set
			{
				if (_urlId != value)
					OnSettingsChanged();
				_urlId = value;
			}
		}

		private string _url;
		public string Url
		{
			get => _url;
			set
			{
				if (_url != value)
					OnSettingsChanged();
				_url = value;
			}
		}

		private DateTime? _urlGeneratingDate;
		public DateTime? UrlGeneratingDate
		{
			get => _urlGeneratingDate;
			set
			{
				if (_urlGeneratingDate != value)
					OnSettingsChanged();
				_urlGeneratingDate = value;
			}
		}

		private string _appId;
		public string AppId
		{
			get => _appId;
			set
			{
				if (_appId != value)
					OnSettingsChanged();
				_appId = value;
			}
		}

		private string _appRoot;
		public string AppRoot
		{
			get => _appRoot;
			set
			{
				if (_appRoot != value)
					OnSettingsChanged();
				_appRoot = value;
			}
		}

		[JsonIgnore]
		public bool Enable => !String.IsNullOrEmpty(Url);

		public void Reset()
		{
			ItemId = null;
			UrlId = null;
			Url = null;
			UrlGeneratingDate = null;
			AppId = null;
			AppRoot = null;
		}
	}
}
