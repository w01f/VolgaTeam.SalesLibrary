namespace SalesLibraries.BatchTagger.PresentationLayer
{
	public class LibraryOpenEventArgs
	{
		public string LibraryName { get; private set; }

		public LibraryOpenEventArgs(string libraryName)
		{
			LibraryName = libraryName;
		}
	}
}
