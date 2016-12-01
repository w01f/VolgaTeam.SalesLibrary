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
	}
}
