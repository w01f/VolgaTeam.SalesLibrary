using System;
using Newtonsoft.Json;
using SalesLibraries.ServiceConnector.WallbinContentService;

namespace SalesLibraries.ServiceConnector.Models.Rest.BatchTagger
{
	public class LinkTagsInfo
	{
		public Guid LinkId { get; set; }

		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public LinkCategory[] Categories { get; set; }
		public string Keywords { get; set; }
	}
}
