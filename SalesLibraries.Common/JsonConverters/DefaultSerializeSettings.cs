using Newtonsoft.Json;

namespace SalesLibraries.Common.JsonConverters
{
	public class DefaultSerializeSettings:JsonSerializerSettings
	{
		public DefaultSerializeSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			PreserveReferencesHandling = PreserveReferencesHandling.Objects;
			TypeNameHandling = TypeNameHandling.All;
			ContractResolver = new EntitySettingsResolver();
			Converters.Add(new ImageConverter());
		}
	}
}
