using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.AppMetaData
{
	public class MetaDataGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "metadata";

		[JsonIgnore]
		public Method Method => Method.POST;

		public string DataTag { get; set; }
		public string PropertyName { get; set; }
	}
}
