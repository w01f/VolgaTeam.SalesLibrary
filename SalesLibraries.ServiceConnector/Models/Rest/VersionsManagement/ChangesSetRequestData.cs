using System;
using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement
{
	public class ChangesSetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "changes";

		[JsonIgnore]
		public Method Method => Method.PUT;

		public Guid LibraryId { get; set; }
		public string User { get; set; }

		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public ChangeSet[] PendingChanges { get; set; }
	}
}
