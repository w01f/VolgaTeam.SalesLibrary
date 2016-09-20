using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class TabbedWallbin : BaseWallbin
	{
		private XtraTabDragDropHelper<TabPage> _tabDragDropHelper;

		public TabbedWallbin(LibraryContext dataStorage)
			: base(dataStorage)
		{
			InitializeComponent();

			toolStripMenuItemDeleteSecurity.Visible = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit;
			toolStripMenuItemDeleteTags.Visible = MainController.Instance.Settings.EditorSettings.EnableTagsEdit;
		}

		public override void DisposeView()
		{
			xtraTabControl.SelectedPageChanging -= OnSelectedPageChanging;
			xtraTabControl.SelectedPageChanged -= OnSelectedPageChanged;
			base.DisposeView();
		}

		protected override void InitControls()
		{
			base.InitControls();
			xtraTabControl.TabPages.Clear();
			xtraTabControl.TabPages.AddRange(Pages.Cast<XtraTabPage>().ToArray());
			xtraTabControl.SelectedTabPage = (XtraTabPage)ActivePage;
			xtraTabControl.SelectedPageChanging += OnSelectedPageChanging;
			xtraTabControl.SelectedPageChanged += OnSelectedPageChanged;

			_tabDragDropHelper = new XtraTabDragDropHelper<TabPage>(xtraTabControl);
			_tabDragDropHelper.TabMoved += OnTabMoved;
		}

		public override void SelectPage(IPageView pageView)
		{
			xtraTabControl.SelectedTabPage = (XtraTabPage)pageView;
		}

		private void OnSelectedPageChanging(object sender, TabPageChangingEventArgs e)
		{
			var newPage = e.Page as IPageView;
			if (newPage == null) return;
			MainController.Instance.ProcessManager.Run("Loading Page...",
				cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					if (e.PrevPage != null)
						WinAPIHelper.SendMessage(e.PrevPage.Handle, 11, IntPtr.Zero, IntPtr.Zero);
					newPage.Suspend();
					WinAPIHelper.SendMessage(e.Page.Handle, 11, new IntPtr(0), IntPtr.Zero);
					SetActivePage(newPage);
				})));
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			var newPage = e.Page as IPageView;
			if (newPage == null) return;
			newPage.ShowPage();
			WinAPIHelper.SendMessage(e.Page.Handle, 11, new IntPtr(1), IntPtr.Zero);
			newPage.Resume();
		}

		private void OnTabMoved(Object sender, TabMoveEventArgs e)
		{
			((List<LibraryPage>)DataStorage.Library.Pages).ChangeItemPosition(
				((IPageView)e.MovedPage).Page,
				((IPageView)e.TargetPage).Page.Order + (1 * e.Offset));
			IsDataChanged = true;
		}

		#region Page Context Menu Processing
		private XtraTabHitInfo _menuHitInfo;
		private void xtraTabControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControl.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			contextMenuStripPageProperties.Show((Control)sender, e.Location);
		}

		private void toolStripMenuItemDeleteLinks_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove links?") != DialogResult.Yes) return;
			selectedPage.Content.DeleteLinks();
			IsDataChanged = true;
		}

		private void toolStripMenuItemDeleteExpirationDates_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove expiration dates?") != DialogResult.Yes) return;
			selectedPage.Content.ResetExpirationDates();
			IsDataChanged = true;
		}

		private void toolStripMenuItemDeleteSecurity_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to delete security settings?") != DialogResult.Yes) return;
			selectedPage.Content.ResetSecurity();
			IsDataChanged = true;
		}

		private void toolStripMenuItemDeleteTags_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to wipe tags?") != DialogResult.Yes) return;
			selectedPage.Content.ResetTags();
			IsDataChanged = true;
		}

		private void toolStripMenuItemDeleteWidgets_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove widgets?") != DialogResult.Yes) return;
			selectedPage.Content.ResetWidgets();
			IsDataChanged = true;
		}

		private void toolStripMenuItemDeleteBanners_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove banners?") != DialogResult.Yes) return;
			selectedPage.Content.ResetBanners();
			IsDataChanged = true;
		}

		private void toolStripMenuItemRename_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			using (var form = new FormPageName())
			{
				form.PageName = selectedPage.Page.Name;
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				selectedPage.Page.Name = form.PageName;
				selectedPage.Text = form.PageName;
				IsDataChanged = true;
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			using (var form = new FormCustomDialog(
					String.Format("{0}{1}{2}",
						"<size=+4>Are you SURE you want to DELETE this Page?</size><br>",
						String.Format("<size=+2>{0}</size>", selectedPage.Page.Name),
						"<br><br>*All Links on this page will be removed from your site"
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
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				selectedPage.DisposePage();
				MainController.Instance.ProcessManager.Run("Deleting Page...",
					cancelationToken =>
					{
						selectedPage.Page.Delete(DataStorage);
						DataStorage.Library.MarkAsModified();
						DataStorage.Library.Pages.RemoveItem(selectedPage.Page);
					});
				IsDataChanged = true;
				SaveData();
				pnEmpty.BringToFront();
				MainController.Instance.ProcessManager.RunInQueue("Loading Library...",
					() => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
					{
						LoadView(true);
						MainController.Instance.WallbinViews.SetActiveWallbin(this);
					})));
			}

		}

		private void toolStripMenuItemEditTags_Click(object sender, EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditTags();
			IsDataChanged = true;
		}
		#endregion

	}
}
