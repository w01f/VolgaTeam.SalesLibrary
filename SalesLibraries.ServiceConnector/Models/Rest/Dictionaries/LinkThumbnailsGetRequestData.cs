using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Dictionaries
{
	public class LinkThumbnailsGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "linkthumbnails";

		[JsonIgnore]
		public Method Method => Method.POST;

		public string LinkId { get; set; }
	}
}
