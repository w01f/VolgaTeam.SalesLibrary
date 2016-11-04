using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Formatting = Newtonsoft.Json.Formatting;

namespace SalesLibraries.Common.JsonConverters
{
	public class DefaultSerializeSettings : JsonSerializerSettings
	{
		public DefaultSerializeSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			PreserveReferencesHandling = PreserveReferencesHandling.Objects;
			TypeNameHandling = TypeNameHandling.All;
			ContractResolver = new EntitySettingsResolver();
			Formatting = Formatting.None;

			Converters.Add(new GuidConverter());
			Converters.Add(new StringEnumConverter
			{
				AllowIntegerValues = true,
				CamelCaseText = false
			});
			Converters.Add(new ImageConverter());
		}
	}
}
