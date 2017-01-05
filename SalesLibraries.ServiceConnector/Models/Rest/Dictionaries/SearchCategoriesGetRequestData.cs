using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Dictionaries
{
	public class SearchCategoriesGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "searchcategories";

		[JsonIgnore]
		public Method Method => Method.POST;
	}
}
