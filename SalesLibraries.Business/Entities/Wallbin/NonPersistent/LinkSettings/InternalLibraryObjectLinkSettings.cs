using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalLibraryObjectLinkSettings : InternalLinkSettings
	{
		[JsonIgnore]
		public override InternalLinkType InternalLinkType => InternalLinkType.LibraryObject;

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
	}
}
