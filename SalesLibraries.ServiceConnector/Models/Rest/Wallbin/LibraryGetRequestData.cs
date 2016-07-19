using System;
using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Wallbin
{
	public class LibraryGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "librarydata";

		[JsonIgnore]
		public Method Method => Method.POST;

		public Guid LibraryId { get; set; }
	}
}
