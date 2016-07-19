using System;

namespace SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement
{
	public interface IVersioned
	{
		Guid Id { get; }
		ObjectType ObjectType { get; }
		DateTime? LastModified { get; }
	}
}
