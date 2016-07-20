namespace SalesLibraries.Common.OfficeInterops
{
	public class WordSingleton : WordProcessor
	{
		private static readonly WordSingleton _instance = new WordSingleton();

		public static WordSingleton Instance => _instance;

		private WordSingleton() { }
	}
}
