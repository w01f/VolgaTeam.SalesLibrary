using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SalesLibraries.Common.JsonConverters
{
	public class DefaultSerializeSettings : JsonSerializerSettings
	{
		public DefaultSerializeSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			PreserveReferencesHandling = PreserveReferencesHandling.Objects;
			TypeNameHandling = TypeNameHandling.All;
			NullValueHandling = NullValueHandling.Ignore;
			MissingMemberHandling = MissingMemberHandling.Ignore;
			ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
			Formatting = Formatting.None;
			ContractResolver = new EntitySettingsResolver();

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
