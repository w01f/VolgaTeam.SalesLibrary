using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	public partial class FormResetLibraryContent : DevComponents.DotNetBar.Metro.MetroForm
	{
		private readonly string _libraryPath;
		public FormResetLibraryContent(string libraryPath)
		{
			InitializeComponent();
			_libraryPath = libraryPath;

			buttonXResetSecurity.Enabled = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit;
		}

		private void buttonXResetWV_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to delete all WV files?") != DialogResult.Yes) return;
			var wvFolderPath = Path.Combine(_libraryPath, Constants.WebPreviewContainersRootFolderName);
			if (!Directory.Exists(wvFolderPath)) return;
			MainController.Instance.ProcessManager.Run("Deleting Files...", (cancelationToken, formProgess) =>
			{
				Utils.DeleteFolder(wvFolderPath);
				try
				{
					Directory.Delete(wvFolderPath);
				}
				catch { }
			});
		}

		private void buttonXResetWidgets_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Widgets?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllLinks).ResetWidgets();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetBanners_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Banners?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllLinks).ResetBanners();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetNotes_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Notes?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllLinks).ResetNote();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetHoverNotes_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Hover Notes?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllLinks).ResetHoverNote();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetExpirationDates_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Expiration Dates?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllLinks).ResetExpirationSettings();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetSecurity_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Security Settings?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllLinks).ResetSecurity();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetLinks_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Links?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllLinks).ResetToDefault();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}
	}
}