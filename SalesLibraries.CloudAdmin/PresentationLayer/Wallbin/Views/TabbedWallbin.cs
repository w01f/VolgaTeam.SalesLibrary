using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class TabbedWallbin : BaseWallbin
	{
		private XtraTabDragDropHelper<TabPage> _tabDragDropHelper;

		public TabbedWallbin(LibraryContext dataStorage)
			: base(dataStorage)
		{
			InitializeComponent();

			barButtonItemPagePropertiesDeleteLinkSecurity.Visibility = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
			barButtonItemPagePropertiesDeleteLinkTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
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
			xtraTabControl.TabPages.AddRange(Pages.OfType<XtraTabPage>().ToArray());
			xtraTabControl.SelectedTabPage = (XtraTabPage)ActivePage;
			xtraTabControl.SelectedPageChanging += OnSelectedPageChanging;
			xtraTabControl.SelectedPageChanged += OnSelectedPageChanged;

			_tabDragDropHelper = new XtraTabDragDropHelper<TabPage>(xtraTabControl);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			InitLinksContextMenuEditors();
		}

		public override void SelectPage(IPageView pageView)
		{
			xtraTabControl.SelectedTabPage = (XtraTabPage)pageView;
		}

		private void ClonePage(TabPage sourceTabPage, bool cloneWithLinks)
		{
			using (var form = new FormClonePage(sourceTabPage.Page.Name))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK)
					return;
				var newPage = sourceTabPage.Page.Clone(form.NewPageName, cloneWithLinks);
				var pageView = PageViewFactory.Create(newPage);
				Pages.Add(pageView);
				xtraTabControl.TabPages.Insert(xtraTabControl.SelectedTabPageIndex + 1, (XtraTabPage)pageView);

				IsDataChanged = true;

				SelectPage(pageView);
			}
		}

		private void OnSelectedPageChanging(object sender, TabPageChangingEventArgs e)
		{
			var newPage = e.Page as IPageView;
			if (newPage == null) return;
			MainController.Instance.ProcessManager.Run("Loading Page...",
				(cancelationToken, formProgess) => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(() =>
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

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			((List<LibraryPage>)DataStorage.Library.Pages).ChangeItemPosition(
				((IPageView)e.MovedPage).Page,
				((IPageView)e.TargetPage).Page.Order + (1 * e.Offset));
			IsDataChanged = true;
		}

		#region Page Context Menu Processing
		private XtraTabHitInfo _menuHitInfo;

		private readonly List<BaseContextMenuEditor> _linksContextMenuEditors = new List<BaseContextMenuEditor>();

		private void InitLinksContextMenuEditors()
		{
			var libraryObjectNotesEditor = new LibraryObjectNotesEditor(barSubItemPagePropertiesMultiLinksNotesObject);
			libraryObjectNotesEditor.EditValueChanged += OnLinksContextEditorValueChanged;
			_linksContextMenuEditors.Add(libraryObjectNotesEditor);

			var lineBreakNotesEditor = new LineBreakNotesEditor(barSubItemPagePropertiesMultiLinksNotesLineBreak);
			lineBreakNotesEditor.EditValueChanged += OnLinksContextEditorValueChanged;
			_linksContextMenuEditors.Add(lineBreakNotesEditor);

			var libraryObjectFormatEditor = new LibraryObjectTextFormatEditor(barSubItemPagePropertiesMultiLinksFormatObject);
			libraryObjectFormatEditor.EditValueChanged += OnLinksContextEditorValueChanged;
			_linksContextMenuEditors.Add(libraryObjectFormatEditor);

			var lineBreakFormatEditor = new LineBreakTextFormatEditor(barSubItemPagePropertiesMultiLinksFormatLineBreak);
			lineBreakFormatEditor.EditValueChanged += OnLinksContextEditorValueChanged;
			_linksContextMenuEditors.Add(lineBreakFormatEditor);

			popupMenuPageProperties.CloseUp += OnLinkPropertiesMenuCloseUp;
		}

		private void LoadLinksContextMenuEditors(IList<BaseLibraryLink> links)
		{
			_linksContextMenuEditors.ForEach(e => e.LoadLinks(links));
		}

		private void ApplyLinksContextMenuEditorChanges()
		{
			_linksContextMenuEditors.ForEach(e => e.ApplyChanges());
		}

		private void OnLinksContextEditorValueChanged(object sender, EventArgs e)
		{
			IsDataChanged = true;

			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			MainController.Instance.ProcessManager.Run("Updating Page...",
				(cancelationToken, formProgess) => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(selectedPage.Content.UpdateContent)));
		}

		private void OnLinkPropertiesMenuCloseUp(object sender, EventArgs e)
		{
			ApplyLinksContextMenuEditorChanges();
		}

		private void xtraTabControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControl.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;

			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;

			barButtonItemPagePropertiesMultiLinksSecurity.Visibility = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit
				? BarItemVisibility.Always
				: BarItemVisibility.Never;
			barButtonItemPagePropertiesMultiLinksTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit && selectedPage.Page.AllLinks.OfType<LibraryObjectLink>().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemPagePropertiesMultiLinksExpirationDate.Visibility = selectedPage.Page.AllLinks.OfType<LibraryObjectLink>().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
			barButtonItemPagePropertiesMultiLinksRefreshPreviewFiles.Visibility = selectedPage.Page.AllLinks.OfType<PreviewableLink>().Any()
					? BarItemVisibility.Always
					: BarItemVisibility.Never;

			LoadLinksContextMenuEditors(selectedPage.Page.AllLinks.ToList());

			popupMenuPageProperties.ShowPopup(Cursor.Position);
		}

		private void barButtonItemPagePropertiesPageSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			using (var form = new FormPageSettings())
			{
				form.PageName = selectedPage.Page.Name;
				form.Icon = selectedPage.Page.Settings.Icon;
				form.IconColor = selectedPage.Page.Settings.IconColor;
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				selectedPage.Page.Name = form.PageName;
				selectedPage.Page.Settings.Icon = form.Icon;
				selectedPage.Page.Settings.IconColor = form.IconColor;
				selectedPage.Text = form.PageName;
				IsDataChanged = true;
			}
		}

		private void barButtonItemPagePropertiesDeletePage_ItemClick(object sender, ItemClickEventArgs e)
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
					(cancelationToken, formProgess) =>
					{
						selectedPage.Page.Delete(DataStorage);
						DataStorage.Library.MarkAsModified();
						DataStorage.Library.Pages.RemoveItem(selectedPage.Page);
						ActivePage = null;
					});
				IsDataChanged = true;
				SaveData(true);
				pnEmpty.BringToFront();
				MainController.Instance.ProcessManager.RunInQueue("Loading Library...",
					() => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(() =>
					{
						LoadView(true);
					})));
			}
		}

		private void barButtonItemPagePropertiesClonePageWindowsAndLinks_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			ClonePage(selectedPage, true);
		}

		private void barButtonItemPagePropertiesClonePageOnlyWindows_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			ClonePage(selectedPage, false);
		}

		private void barButtonItemPagePropertiesDeleteLinkWidgets_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove widgets?") != DialogResult.Yes) return;
			selectedPage.Content.ResetWidgets();
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesDeleteLinkBanners_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove banners?") != DialogResult.Yes) return;
			selectedPage.Content.ResetBanners();
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesDeleteLinkTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to wipe tags?") != DialogResult.Yes) return;
			selectedPage.Content.ResetTags();
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesResetLinkExpirationDates_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove expiration dates?") != DialogResult.Yes) return;
			selectedPage.Content.ResetExpirationDates();
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesDeleteLinkSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to delete security settings?") != DialogResult.Yes) return;
			selectedPage.Content.ResetSecurity();
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesSetLinkTextWordWrap_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.SetLinkTextWordWrap();
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesLinkAdminSettingsExcel_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditLinksGroupSettings(LinkSettingsType.AdminSettings, FileTypes.Excel, false);
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesLinkAdminSettingsPdf_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditLinksGroupSettings(LinkSettingsType.AdminSettings, FileTypes.Pdf, false);
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesMultiLinksResetSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			using (var form = new FormResetLinkSettings(selectedPage.Page))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var settingsGroupsForReset = form.SettingsGroups;
				using (var confirmation = new FormResetLinkSettingsConfirmation(settingsGroupsForReset))
				{
					if (confirmation.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					selectedPage.Content.ResetAllSettings(settingsGroupsForReset);
					IsDataChanged = true;
				}
			}
		}

		private void barButtonItemPagePropertiesMultiLinksDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove links?") != DialogResult.Yes) return;
			selectedPage.Content.DeleteLinks();
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesMultiLinksWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditLinksGroupSettings(LinkSettingsType.Widget);
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesMultiLinksBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditLinksGroupSettings(LinkSettingsType.Banner);
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesMultiLinksTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditLinksGroupSettings(LinkSettingsType.Tags, updateContent: false);
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesMultiLinksSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditLinksGroupSettings(LinkSettingsType.Security, updateContent: false);
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesMultiLinksExpirationDate_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			selectedPage.Content.EditLinksGroupSettings(LinkSettingsType.Tags);
			IsDataChanged = true;
		}

		private void barButtonItemPagePropertiesMultiLinksRefreshPreviewFiles_ItemClick(object sender, ItemClickEventArgs e)
		{
			var selectedPage = _menuHitInfo.Page as TabPage;
			if (selectedPage == null) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to refresh the server files for links on this page?") != DialogResult.Yes) return;
			selectedPage.Content.RefreshPreviewFiles();
			MainController.Instance.PopupMessages.ShowInfo("Links are now updated for the server!");
		}

		private void barButtonItemPagePropertiesDeleteAllLinks_ItemClick(object sender, ItemClickEventArgs e)
		{
			barButtonItemPagePropertiesMultiLinksDelete_ItemClick(sender, e);
		}
		#endregion
	}
}
