using System;
using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement
{
	public class ChangesGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "changes";

		[JsonIgnore]
		public Method Method => Method.POST;

		public Guid LibraryId { get; set; }
		public DateTime LastUpdate { get; set; }
	}
}
