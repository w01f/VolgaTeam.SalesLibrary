using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
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

			//buttonXResetQV.Enabled = MainController.Instance.Settings.EnableLocalSync;
			buttonXResetSecurity.Enabled = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit;
			buttonXResetCategories.Enabled = MainController.Instance.Settings.EnableTagsTab;
			buttonXResetKeywords.Enabled = MainController.Instance.Settings.EnableTagsTab;
		}

		private void buttonXResetQV_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to delete all QV files?") != DialogResult.Yes) return;
			var qvFolderPath = Path.Combine(_libraryPath, Constants.RegularPreviewContainersRootFolderName);
			if (!Directory.Exists(qvFolderPath)) return;
			MainController.Instance.ProcessManager.Run("Deleting Files...", (cancelationToken, formProgess) =>
			{
				Utils.DeleteFolder(qvFolderPath);
				try
				{
					Directory.Delete(qvFolderPath);
				}
				catch { }
			});
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
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ResetWidgets();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetBanners_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Banners?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ResetBanners();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetNotes_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Notes?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ResetNote();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetHoverNotes_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Hover Notes?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ResetHoverNote();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetExpirationDates_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Expiration Dates?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ResetExpirationSettings();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetSecurity_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Security Settings?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ResetSecurity();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetLinks_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Links?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ResetToDefault();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetCategories_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Categories?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ApplyCategories(new SearchGroup[] { });
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ApplySuperFilters(new string[] { });
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}

		private void buttonXResetKeywords_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to reset ALL Keywords?") != DialogResult.Yes) return;
			MainController.Instance.ProcessChanges();
			MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages.SelectMany(page => page.AllGroupLinks).ApplyKeywords(new SearchTag[] { });
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
		}
	}
}