using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesLibraries.Common.JsonConverters
{
	public class DefaultSerializeSettings : JsonSerializerSettings
	{
		public DefaultSerializeSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			PreserveReferencesHandling = PreserveReferencesHandling.Objects;
			ContractResolver = new EntitySettingsResolver();
			Formatting = Formatting.None;

			Converters.Add(new GuidConverter());
			Converters.Add(new StringEnumConverter
			{
				AllowIntegerValues = true,
				CamelCaseText = false
			});
			Converters.Add(new ImageConverter());
			TypeNameHandling = TypeNameHandling.All;
		}
	}
}
