using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Connection
{
	public class ConnectionGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "connection";

		[JsonIgnore]
		public Method Method => Method.POST;

		public ConnectionRequestType RequestType { get; set; }
		public string UserName { get; set; }
		public string LibraryName { get; set; }
	}
}
