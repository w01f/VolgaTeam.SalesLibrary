using System;
using System.Collections.Generic;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.SalesDepot.Business.ProgramSchedule;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	interface IWallbinView
	{
		LibraryContext DataStorage { get; }
		ProgramScheduleContext ProgramSchedule { get; }
		IPageView ActivePage { get; }
		List<IPageView> Pages { get; }
		event EventHandler<EventArgs> PageChanged;
		void LoadView(bool force = false);
		void UnloadView();
		void ShowView();
		void DisposeView();
	}
}
