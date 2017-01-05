using Newtonsoft.Json;
using RestSharp;
using SalesLibraries.ServiceConnector.Models.Rest.Common;

namespace SalesLibraries.ServiceConnector.Models.Rest.Dictionaries
{
	public class ShortcutLinksGetRequestData : IRequestData
	{
		[JsonIgnore]
		public string ModelName => "shortcutlinks";

		[JsonIgnore]
		public Method Method => Method.POST;
	}
}
