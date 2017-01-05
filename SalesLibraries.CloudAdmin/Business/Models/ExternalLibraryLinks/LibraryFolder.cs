namespace SalesLibraries.CloudAdmin.Business.Models.ExternalLibraryLinks
{
	class LibraryFolder
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }

		public LibraryLink[] Links { get; set; }
	}
}
