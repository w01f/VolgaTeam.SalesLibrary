using Newtonsoft.Json;

namespace SalesLibraries.ServiceConnector.Models.Rest.Wallbin.Settings
{
	public class LibrarySettings
	{
		public bool? ApplyAppearanceForAllWindows { get; set; }
		public bool? ApplyWidgetForAllWindows { get; set; }
		public bool? ApplyBannerForAllWindows { get; set; }

		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public AutoWidget[] AutoWidgets { get; set; }
	}
}
