namespace SalesLibraries.FileManager.Business.Models.ExternalLibraryLinks
{
	class LibraryPage
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }

		public LibraryFolder[] Folders { get; set; }
	}
}
