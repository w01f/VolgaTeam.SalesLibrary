using System;
using System.Windows.Forms;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	class ViewManager
	{
		public FormatState FormatState { get; }
		public SelectionManager Selection { get; private set; }

		public IWallbinView ActiveWallbin { get; private set; }

		public event EventHandler<EventArgs> DataChanged;

		public ViewManager()
		{
			FormatState = new FormatState();
			FormatState.StateChanged += UpdateViewState;

			Selection = new SelectionManager();
		}

		public void Load()
		{
			if (ActiveWallbin != null)
			{
				ActiveWallbin.DataChanged -= OnLibraryDataChanged;
				ActiveWallbin.DisposeView();
				((Control)ActiveWallbin).Parent = null;
				((Control)ActiveWallbin).Dispose();
			}
			ActiveWallbin = WallbinViewFactory.Create(MainController.Instance.Wallbin.LocalContext);
			ActiveWallbin.DataChanged += OnLibraryDataChanged;
			ActiveWallbin.LoadView();
			InitControls();
		}

		private void InitControls()
		{
			MainController.Instance.MainForm.itemContainerHomeWallbin.Visible = !MainController.Instance.Settings.MultitabView;
			MainController.Instance.MainForm.ribbonBarHomeWallbin.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelHome.PerformLayout();
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
		private void OnLibraryDataChanged(object sender, EventArgs e)
		{
			DataChanged?.Invoke(sender, e);
		}
		#endregion
	}
}
