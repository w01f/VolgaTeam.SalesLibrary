using Newtonsoft.Json;

namespace SalesLibraries.ServiceConnector.Models.Rest.Common
{
	public class RestError
	{
		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }
	}
}
