using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class InternalShortcutLinkSettings : InternalLinkSettings
	{
		[JsonIgnore]
		public override InternalLinkType InternalLinkType => InternalLinkType.Shortcut;

		private string _shortcutId;
		public string ShortcutId
		{
			get { return _shortcutId; }
			set
			{
				if (_shortcutId != value)
					OnSettingsChanged();
				_shortcutId = value;
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
	}
}
