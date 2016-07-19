using Newtonsoft.Json;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin.Entities;

namespace SalesLibraries.ServiceConnector.Models.Rest.Wallbin
{
	public class LibraryDataPackage
	{
		public Library Library { get; set; }

		[JsonProperty(TypeNameHandling = TypeNameHandling.None)]
		public LibraryPage[] Pages { get; set; }
	}
}
