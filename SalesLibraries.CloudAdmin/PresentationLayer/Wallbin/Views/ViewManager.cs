using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.DataState;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	class ViewManager
	{
		public FormatState FormatState { get; private set; }
		public SelectionManager Selection { get; private set; }

		public List<IWallbinView> Views { get; private set; }

		public IWallbinView ActiveWallbin { get; private set; }

		public event EventHandler<EventArgs> LibraryChanging;
		public event EventHandler<EventArgs> BeforeLoad;
		public event EventHandler<EventArgs> DataChanged;

		public ViewManager()
		{
			FormatState = new FormatState();
			FormatState.StateChanged += UpdateViewState;

			Selection = new SelectionManager();

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
			SetActiveWallbin(Views.FirstOrDefault());
			InitControls();
		}

		private void InitControls()
		{
			MainController.Instance.MainForm.itemContainerHomeWallbin.Visible = !MainController.Instance.Settings.MultitabView;
			MainController.Instance.MainForm.ribbonBarHomeWallbin.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelHome.PerformLayout();
		}

		public void SetActiveWallbin(IWallbinView wallbinView)
		{
			LibraryChanging?.Invoke(this, EventArgs.Empty);
			ActiveWallbin = wallbinView;
			if (ActiveWallbin == null) return;
			ActiveWallbin.LoadView();
			DataStateObserver.Instance.RaiseLibrarySelected();
		}

		public void SaveActiveWallbin()
		{
			ActiveWallbin?.SaveData();
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
			SaveActiveWallbin();
		}

		private void OnLibraryDataChanged(object sender, EventArgs e)
		{
			DataChanged?.Invoke(sender, e);
		}
		#endregion
	}
}
