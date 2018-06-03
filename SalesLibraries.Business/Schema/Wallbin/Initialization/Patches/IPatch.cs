using SalesLibraries.Business.Contexts.Wallbin;

namespace SalesLibraries.Business.Schema.Wallbin.Initialization.Patches
{
	public interface IPatch
	{
		int Version { get; }
		void Apply<TLibraryContext>(TLibraryContext context) where TLibraryContext : LibraryContext;
	}
}
