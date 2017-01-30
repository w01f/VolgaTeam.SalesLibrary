using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Clipboard;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		private FolderClipboardManager _folderClipboardManager;

		public DialogResult EditFolderSettings()
		{
			using (var form = new FormWindow(DataSource, new BaseEditFormParams()))
			{
				var dialogResult = form.ShowDialog(MainController.Instance.MainForm);
				if (dialogResult != DialogResult.OK) return dialogResult;
				DataChanged?.Invoke(this, EventArgs.Empty);
				if (DataSource.Page.Library.Settings.ApplyAppearanceForAllWindows ||
					DataSource.Page.Library.Settings.ApplyWidgetForAllWindows ||
					DataSource.Page.Library.Settings.ApplyWidgetColorForAllWindows ||
					DataSource.Page.Library.Settings.ApplyBannerForAllWindows)
				{
					MainController.Instance.ProcessManager.Run("Updating Page...",
						(cancelationToken, formProgess) => MainController.Instance.MainForm.Invoke(new MethodInvoker(FolderContainer.UpdateContent)));
				}
				else
				{
					UpdateFont();
					SetupView();
					UpdateContent(true);
				}
				return dialogResult;
			}
		}

		private void EditFolderBanner()
		{
			using (var form = new FormWindow(DataSource, new BannerFormParams()))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				DataChanged?.Invoke(this, EventArgs.Empty);
				if (DataSource.Page.Library.Settings.ApplyBannerForAllWindows)
				{
					MainController.Instance.ProcessManager.Run("Updating Page...",
						(cancelationToken, formProgess) =>
							MainController.Instance.MainForm.Invoke(new MethodInvoker(FolderContainer.UpdateContent)));
				}
				else
				{
					SetupView();
					UpdateGridSize();
				}
			}
		}

		private void EditFolderImageSettings()
		{
			if (DataSource.Widget.Enabled)
				EditFolderWidget();
			else if (DataSource.Banner.Enable)
				EditFolderBanner();
		}

		private void EditFolderWidget()
		{
			using (var form = new FormWindow(DataSource, new WidgetFormParams()))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				DataChanged?.Invoke(this, EventArgs.Empty);
				if (DataSource.Page.Library.Settings.ApplyWidgetForAllWindows || DataSource.Page.Library.Settings.ApplyWidgetColorForAllWindows)
				{
					MainController.Instance.ProcessManager.Run("Updating Page...",
						(cancelationToken, formProgess) =>
							MainController.Instance.MainForm.Invoke(new MethodInvoker(FolderContainer.UpdateContent)));
				}
				else
				{
					SetupView();
					UpdateGridSize();
				}
			}
		}

		private void DeleteFolder()
		{
			using (var form = new FormCustomDialog(
					String.Format("{0}{1}{2}",
						"<size=+4>Are you SURE you want to DELETE this Window?</size><br>",
						String.Format("<size=+2>{0}</size>", DataSource.Name),
						"<br><br>*All Links in this window will be removed from your site"
					),
					new[]
					{
						new CustomDialogButtonInfo {Title = "DELETE",DialogResult = DialogResult.OK,Width = 100},
						new CustomDialogButtonInfo {Title = "CANCEL",DialogResult = DialogResult.Cancel,Width = 100}
					}
				))
			{
				form.Width = 500;
				form.Height = 160;
				if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
					FolderContainer.DeleteFolder(this);
			}
		}

		private void OnFolderMoved(object sender, FolderMovingEventArgs e)
		{
			if (e.DeleteFromCurrent)
				FolderContainer.DeleteFolder(this);
			var targetPageView = MainController.Instance.WallbinViews.ActiveWallbin.Pages
				.FirstOrDefault(pageView => pageView.Page == e.TargetPage);
			var isSamePage = MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == targetPageView;
			if (isSamePage)
				MainController.Instance.ProcessManager.Run("Loading Page...",
				(cancelationToken, formProgess) => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					MainController.Instance.WallbinViews.ActiveWallbin.ActivePage?.LoadPage(true);
					MainController.Instance.WallbinViews.ActiveWallbin.ActivePage?.ShowPage();
				})));
			else
			{
				targetPageView?.LoadPage(true);
				MainController.Instance.WallbinViews.ActiveWallbin.SelectPage(targetPageView);
			}
		}
	}
}
