using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SalesLibraries.Common.JsonConverters
{
	class RestSerializeSettings : JsonSerializerSettings
	{
		public RestSerializeSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			PreserveReferencesHandling = PreserveReferencesHandling.None;
			TypeNameHandling = TypeNameHandling.All;
			Formatting = Formatting.None;

			ContractResolver = new CamelCasePropertyNamesContractResolver();

			Converters.Add(new GuidConverter());
			Converters.Add(new ImageConverter());
			Converters.Add(new RestEnumConverter
			{
				AllowIntegerValues = true,
				CamelCaseText = false
			});
		}
	}
}
