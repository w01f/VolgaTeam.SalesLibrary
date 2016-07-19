using Newtonsoft.Json;

namespace SalesLibraries.ServiceConnector.Models.Rest.Common
{
	public class RestResponse
	{
		[JsonProperty(PropertyName = "resultCode")]
		public ResponseResult Result { get; set; }

		[JsonProperty(PropertyName = "dataEncoded")]
		public string DataEncoded { get; set; }
	}
}
