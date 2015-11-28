using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	interface ILinkViewer
	{
		LibraryObjectLink Link { get; }
		string DisplayName { get; }

		void ReleaseResources();
		void Open();
		void Save();
		void Email();
		void Print();
	}
}
