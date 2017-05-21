using System;
using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.WallbinContentService;

namespace SalesLibraries.ServiceConnector.Models.Rest.BatchTagger
{
	public class BatchTaggerSetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "batchtagger";

		[JsonIgnore]
		public Method Method => Method.PUT;

		public Guid LibraryId { get; set; }
		public Guid LinkId { get; set; }

		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public LinkCategory[] Categories { get; set; }
		public string Keywords { get; set; }
		public string EncodedDatabase { get; set; }
	}
}
