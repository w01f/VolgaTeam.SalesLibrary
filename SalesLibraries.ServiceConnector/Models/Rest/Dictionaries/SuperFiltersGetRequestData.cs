using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Dictionaries
{
	public class SuperFiltersGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "superfilters";

		[JsonIgnore]
		public Method Method => Method.POST;
	}
}
