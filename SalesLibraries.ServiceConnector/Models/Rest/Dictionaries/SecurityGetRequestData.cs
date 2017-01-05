using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Dictionaries
{
	public class SecurityGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "security";

		[JsonIgnore]
		public Method Method => Method.POST;
	}
}
