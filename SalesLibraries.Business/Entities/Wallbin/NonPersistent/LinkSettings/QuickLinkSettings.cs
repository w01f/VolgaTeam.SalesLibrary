using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class QuickLinkSettings : SettingsContainer
	{
		public const string PredefinedQuickLinkTitleInfo = "Info";
		public const string PredefinedQuickLinkTitleHtml5 = "HTML5";
		public const string PredefinedQuickLinkTitleLink = "Link";
		public const string PredefinedQuickLinkTitleResources = "Resources";

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

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				if (_title != value)
					OnSettingsChanged();
				_title = value;
			}
		}

		[JsonIgnore]
		public bool Enable => !String.IsNullOrEmpty(Url);

		public QuickLinkSettings()
		{
			_title = PredefinedQuickLinkTitleInfo;
		}
	}
}
