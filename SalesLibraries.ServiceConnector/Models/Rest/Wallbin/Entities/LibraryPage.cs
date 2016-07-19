using System;
using SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin.Settings;

namespace SalesLibraries.ServiceConnector.Models.Rest.Wallbin.Entities
{
	public class LibraryPage : IVersioned
	{
		public Guid Id { get; set; }
		public Guid LibraryId { get; set; }
		public ObjectType ObjectType => ObjectType.Page;
		public string Name { get; set; }
		public int Order { get; set; }
		public DateTime? LastModified { get; set; }
		public LibraryPageSettings Settings { get; set; }
	}
}
