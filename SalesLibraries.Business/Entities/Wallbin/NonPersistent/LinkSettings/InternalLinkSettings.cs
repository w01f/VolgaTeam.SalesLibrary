using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public abstract class InternalLinkSettings : LibraryObjectLinkSettings
	{
		public const string PageViewTypeColumns = "columns";
		public const string PageViewTypeAccording = "accordion";

		public const string PageSelectorTypeTabs = "tabs";
		public const string PageSelectorTypeCombo = "combo";

		[JsonIgnore]
		public abstract InternalLinkType InternalLinkType { get; }

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
	}
}
