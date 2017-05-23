using System;
using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.BatchTagger
{
	public class BatchTaggerSetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "batchtagger";

		[JsonIgnore]
		public Method Method => Method.PUT;
		
		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public LinkTagsInfo[] LinkInfo { get; set; }
	}
}
