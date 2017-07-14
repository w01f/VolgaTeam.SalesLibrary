using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Business.Synchronization;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin
{
	public partial class FormCompactWallbin : MetroForm
	{
		private bool _dataChanged;

		public event EventHandler<BackToRegularWallbinEventArgs> BackToRegularWallbin;
		public event EventHandler<EventArgs> CloseApplication;

		private IWallbinView ActiveWallbin => MainController.Instance.WallbinViews.ActiveWallbin;

		public FormCompactWallbin()
		{
			InitializeComponent();

			Text = String.Format(FormMain.TitleTemplate, AppProfileManager.Instance.LibraryAlias);
			Icon = MainController.Instance.ImageResources.AppIcon ?? Icon;
			barStaticItemLogo.Glyph = MainController.Instance.ImageResources.AppRibbonLogo ?? barStaticItemLogo.Glyph;

			Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 20;
			Top = Screen.PrimaryScreen.WorkingArea.Top + 20;
			Height = Screen.PrimaryScreen.WorkingArea.Height - 40;

			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Compact-Walbin-Form", false, true);

			InitSingleLinkContextMenuEditors();

			DataStateObserver.Instance.DataChanged += OnLinksDeleted;

			if (CreateGraphics().DpiX > 96)
			{
				buttonXCollapse.Font = new Font(buttonXCollapse.Font.FontFamily, buttonXCollapse.Font.Size - 2, buttonXCollapse.Font.Style);
				buttonXExpand.Font = new Font(buttonXExpand.Font.FontFamily, buttonXExpand.Font.Size - 2, buttonXExpand.Font.Style);
				buttonXBack.Font = new Font(buttonXBack.Font.FontFamily, buttonXBack.Font.Size - 2, buttonXBack.Font.Style);
				buttonXExit.Font = new Font(buttonXExit.Font.FontFamily, buttonXExit.Font.Size - 2, buttonXExit.Font.Style);
			}
		}

		private void RaiseDataChanged()
		{
			_dataChanged = true;
			ActiveWallbin.IsDataChanged = true;
		}

		private void UpdateSyncInfo()
		{
			labelControlSyncInfo.Text = String.Format("<color=gray>Site last updated: {0}</color>", ActiveWallbin.DataStorage.Library.SyncDate?.ToString("dd/MM/yy h:mm:tt"));
			panelSyncInfo.Visible = true;
		}

		private void OnBackClick(object sender, EventArgs e)
		{
			MainController.Instance.ProcessChanges();

			var backEventArgs = new BackToRegularWallbinEventArgs { DataChanged = _dataChanged };
			BackToRegularWallbin?.Invoke(this, backEventArgs);

			_dataChanged = false;
		}

		private void OnExitClick(object sender, EventArgs e)
		{
			CloseApplication?.Invoke(this, EventArgs.Empty);
		}

		private async void OnSyncClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			MainController.Instance.ProcessChanges();

			if ((MainController.Instance.Settings.NetworkPaths.Any() && MainController.Instance.Settings.NetworkPaths.Any(p => !Directory.Exists(p))) ||
				MainController.Instance.Settings.WebPaths.Any() && MainController.Instance.Settings.WebPaths.Any(p => !Directory.Exists(p)))
				MainController.Instance.PopupMessages.ShowWarning("Some of your Upload Directories are Not connected. Your changes will still be saved in your Source Directory.");

			barLargeButtonItemSync.Enabled =
				panelTop.Enabled =
				panelMain.Enabled =
				panelBottom.Enabled = false;
			panelSyncInfo.Visible = false;
			panelSyncProgress.Visible = true;
			circularProgressSyncProgress.IsRunning = true;

			var cancellationTokenSource = new CancellationTokenSource();
			try
			{
				await Task.Run(() =>
				{
					SyncManager.SyncRegular(cancellationTokenSource.Token);
				}, cancellationTokenSource.Token);
			}
			catch (Exception exception)
			{
				SyncManager.ProcessSyncException(exception);
			}

			circularProgressSyncProgress.IsRunning = false;
			panelSyncProgress.Visible = false;
			barLargeButtonItemSync.Enabled =
			panelTop.Enabled =
			panelMain.Enabled =
			panelBottom.Enabled = true;
			UpdateSyncInfo();
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				CloseApplication?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
