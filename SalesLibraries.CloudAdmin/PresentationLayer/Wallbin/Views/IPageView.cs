using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Libraries;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	public interface IPageView
	{
		LibraryPage Page { get; }
		PageContent Content { get; }
		bool IsActive { get; }
		LibraryPageTagInfo TagInfoControl { get; }
		void LoadPage(bool force = false);
		void DisposePage();
		void ShowPage();
		void UpdateView();
		void Suspend();
		void Resume();
	}
}
