using System;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	public interface IPageView
	{
		LibraryPage Page { get; }
		PageContent Content { get; }
		bool IsActive { get; }
		void LoadPage(bool force = false);
		void DisposePage();
		void ShowPage();
		void UpdateView();
		void Suspend();
		void Resume();
	}
}
