using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Dictionaries
{
	public class LibraryLinksGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "librarylinks";

		[JsonIgnore]
		public Method Method => Method.POST;
	}
}
