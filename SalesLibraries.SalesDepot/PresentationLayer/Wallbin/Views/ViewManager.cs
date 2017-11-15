using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesLibraries.Common.DataState;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	class ViewManager
	{
		private ComboBoxEdit _librarySelector;

		public FormatState FormatState { get; private set; }

		public List<IWallbinView> Views { get; private set; }

		public IWallbinView ActiveWallbin { get; private set; }

		public ViewManager()
		{
			FormatState = new FormatState();
			FormatState.StateChanged += UpdateViewState;

			Views = new List<IWallbinView>();
		}

		public void Load()
		{
			Views.ForEach(v =>
			{
				if (_librarySelector != null)
					_librarySelector.EditValueChanged -= OnSelectedLibraryChanged;
				v.DisposeView();
				((Control)v).Parent = null;
				((Control)v).Dispose();
			});
			Views.Clear();
			ActiveWallbin = null;
			foreach (var libraryContext in MainController.Instance.Wallbin.Libraries)
			{
				var view = WallbinViewFactory.Create(libraryContext);
				Views.Add(view);
			}
			SetActiveWallbin(MainController.Instance.Settings.WallbinViewSettings.SelectedLibrary);
			InitControls();
		}

		private void InitControls()
		{
			_librarySelector = MainController.Instance.MainForm.comboBoxEditHomeLibrary;
			_librarySelector.Properties.Items.Clear();
			_librarySelector.Properties.Items.AddRange(Views);
			_librarySelector.EditValue = ActiveWallbin;
			_librarySelector.EditValueChanged += OnSelectedLibraryChanged;

			MainController.Instance.MainForm.itemContainerHomeWallbinLibrary.Visible = Views.Count > 1;
			MainController.Instance.MainForm.itemContainerHomeWallbinPage.Visible = !MainController.Instance.Settings.WallbinViewSettings.MultitabView;
			MainController.Instance.MainForm.itemContainerHomeWallbin.Visible = Views.Count > 1 || !MainController.Instance.Settings.WallbinViewSettings.MultitabView;
			MainController.Instance.MainForm.ribbonBarHomeWallbin.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelHome.PerformLayout();
		}

		private void SetActiveWallbin(string libraryName)
		{
			SetActiveWallbin(Views.FirstOrDefault(v => v.DataStorage.LibraryName.Equals(libraryName)) ?? Views.FirstOrDefault());
		}

		public void SetActiveWallbin(IWallbinView wallbinView)
		{
			if (ActiveWallbin != null)
				ActiveWallbin.UnloadView();
			ActiveWallbin = wallbinView;
			if (ActiveWallbin == null) return;
			ActiveWallbin.LoadView();
			MainController.Instance.Settings.WallbinViewSettings.SelectedLibrary = ActiveWallbin.DataStorage.LibraryName;
			MainController.Instance.Settings.SaveSettings();
			DataStateObserver.Instance.RaiseLibrarySelected();
		}

		private void UpdateViewState(object sender, EventArgs e)
		{
			if (ActiveWallbin == null || ActiveWallbin.ActivePage == null) return;
			ActiveWallbin.ActivePage.UpdateView();
		}

		#region Event Handlers
		private void OnSelectedLibraryChanged(object sender, EventArgs eventArgs)
		{
			var editor = sender as ComboBoxEdit;
			if (editor == null) return;
			var selectedView = editor.EditValue as IWallbinView;
			if (selectedView == null) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...",
				() => MainController.Instance.MainForm.Invoke(new MethodInvoker(() => SetActiveWallbin(selectedView))));
		}
		#endregion
	}
}
