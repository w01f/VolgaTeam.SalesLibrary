﻿using System;
using System.Collections.Generic;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.BundleList;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
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
		DataSourceTreeViewControl DataSourcesControl { get; }
		LinkBundleListControl LinkBundleListControl { get; }
		void LoadView(bool force = false);
		void ShowView();
		void DisposeView();
		void SaveData(bool runInQueue);
		void SelectPage(IPageView pageView);
	}
}
