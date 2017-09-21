using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.Clipboard;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	class ViewManager
	{
		public FormatState FormatState { get; }
		public SelectionManager Selection { get; private set; }
		public LinksClipboardManager LinksClipboard { get; private set; }

		public List<IWallbinView> Views { get; }

		public IWallbinView ActiveWallbin { get; private set; }

		public event EventHandler<EventArgs> LibraryChanging;
		public event EventHandler<EventArgs> BeforeLoad;
		public event EventHandler<EventArgs> DataChanged;

		public ViewManager()
		{
			FormatState = new FormatState();
			FormatState.StateChanged += UpdateViewState;

			Selection = new SelectionManager();
			LinksClipboard = new LinksClipboardManager();
			Views = new List<IWallbinView>();

			BeforeLoad += OnViewBeforeLoad;
			LibraryChanging += OnLibraryChanging;
		}

		public void Load()
		{
			BeforeLoad?.Invoke(this, EventArgs.Empty);
			Views.ForEach(v =>
			{
				v.DisposeView();
				((Control)v).Parent = null;
				((Control)v).Dispose();
			});
			Views.Clear();
			ActiveWallbin = null;
			foreach (var libraryContext in MainController.Instance.Wallbin.Libraries)
			{
				var view = WallbinViewFactory.Create(libraryContext);
				view.DataChanged += OnLibraryDataChanged;
				Views.Add(view);
			}
			SetActiveWallbin(MainController.Instance.Settings.SelectedLibrary);
			InitControls();
		}

		private void InitControls()
		{
			MainController.Instance.TabWallbin.layoutControlItemPages.Visibility = !MainController.Instance.Settings.MultitabView ? LayoutVisibility.Always : LayoutVisibility.Never;
			MainController.Instance.MainForm.ribbonBarHomeLogo.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelHome.PerformLayout();
		}

		private void SetActiveWallbin(string libraryName)
		{
			SetActiveWallbin(Views.FirstOrDefault(v => v.DataStorage.LibraryName.Equals(libraryName)) ?? Views.FirstOrDefault());
		}

		public void SetActiveWallbin(IWallbinView wallbinView)
		{
			LibraryChanging?.Invoke(this, EventArgs.Empty);
			ActiveWallbin = wallbinView;
			if (ActiveWallbin == null) return;
			ActiveWallbin.LoadView();
			MainController.Instance.Settings.SelectedLibrary = ActiveWallbin.DataStorage.LibraryName;
			MainController.Instance.Settings.Save();
			DataStateObserver.Instance.RaiseLibrarySelected();
		}

		public void SaveActiveWallbin(bool runInQueue)
		{
			ActiveWallbin?.SaveData(runInQueue);
		}

		private void UpdateViewState(object sender, EventArgs e)
		{
			ActiveWallbin?.ActivePage?.UpdateView();
		}

		#region Event Handlers
		private void OnViewBeforeLoad(object sender, EventArgs e)
		{
			LibraryChanging?.Invoke(this, EventArgs.Empty);
		}

		private void OnLibraryChanging(object sender, EventArgs e)
		{
			SaveActiveWallbin(true);
		}

		private void OnLibraryDataChanged(object sender, EventArgs e)
		{
			DataChanged?.Invoke(sender, e);
		}
		#endregion
	}
}
