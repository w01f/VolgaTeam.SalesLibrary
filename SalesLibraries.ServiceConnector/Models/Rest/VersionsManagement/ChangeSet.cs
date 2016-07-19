namespace SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement
{
	public class ChangeSet
	{
		public ChangeType ChangeType { get; set; }
		public IVersioned ChangedObject { get; set; }
	}
}
