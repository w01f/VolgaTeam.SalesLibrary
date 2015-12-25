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
			ContractResolver = new EFProxyContractResolver();
			Converters.Add(new ImageConverter());
		}
	}
}
