namespace SalesLibraries.ServiceConnector.Models.Rest.Dictionaries
{
	public class SiteUserGroup
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public SiteUser[] Users { get; set; }
		public bool AllUsers { get; set; }
		public string[] LibraryIds { get; set; }
	}
}
