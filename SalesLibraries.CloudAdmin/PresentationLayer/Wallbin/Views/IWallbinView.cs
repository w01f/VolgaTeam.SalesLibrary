using System;
using System.Collections.Generic;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.LinkBundles.BundleList;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	interface IWallbinView
	{
		bool IsDataChanged { get; set; }
		LibraryContext DataStorage { get; }
		IPageView ActivePage { get; }
		List<IPageView> Pages { get; }
		event EventHandler<EventArgs> PageChanging;
		event EventHandler<EventArgs> PageChanged;
		event EventHandler<EventArgs> DataChanged;
		LinkBundleListControl LinkBundleListControl { get; }
		void LoadView(bool force = false);
		void ShowView();
		void DisposeView();
		void SaveData();
		void SelectPage(IPageView pageView);
	}
}
