using System;
using SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin.Settings;

namespace SalesLibraries.ServiceConnector.Models.Rest.Wallbin.Entities
{
	public class Library : IVersioned
	{
		public Guid Id { get; set; }
		public ObjectType ObjectType => ObjectType.Library;
		public string Name { get; set; }
		public DateTime? LastModified { get; set; }
		public LibrarySettings Settings { get; set; }
	}
}
