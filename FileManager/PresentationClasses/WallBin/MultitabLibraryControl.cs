using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolForms.Settings;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin
{
	[ToolboxItem(false)]
	public partial class MultitabLibraryControl : UserControl
	{
		private readonly List<PageDecorator> _pages = new List<PageDecorator>();
		private XtraTabHitInfo _menuHitInfo;

		public MultitabLibraryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void Init(PageDecorator[] pages)
		{
			_pages.Clear();
			_pages.AddRange(pages);
			xtraTabControl.TabPages.Clear();
			xtraTabControl.TabPages.AddRange(_pages.Select(x => x.TabPage).ToArray());
			if (MainController.Instance.ActiveDecorator != null && MainController.Instance.ActiveDecorator.ActivePage != null)
				xtraTabControl.SelectedTabPage = MainController.Instance.ActiveDecorator.ActivePage.TabPage;
			xtraTabControl.SelectedPageChanging += xtraTabControl_SelectedPageChanging;
			xtraTabControl.SelectedPageChanged += xtraTabControl_SelectedPageChanged;
		}

		void xtraTabControl_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
		{
			if (!MainController.Instance.SaveLibraryWarning())
				e.Cancel = true;
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.Page != null)
			{
				var pageDecorator = e.Page.Tag as PageDecorator;
				if (pageDecorator != null)
					MainController.Instance.WallbinController.SelectPage(pageDecorator.Page);
			}
		}

		private void xtraTabControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControl.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			contextMenuStripPageProperties.Show((Control)sender, e.Location);
		}

		private void toolStripMenuItemDeleteLinks_Click(object sender, System.EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page.Tag as PageDecorator;
			if (selectedPage == null) return;
			if (AppManager.Instance.ShowQuestion("Are You sure You want to remove links?") != DialogResult.Yes) return;
			selectedPage.DeleteLinks();
		}

		private void toolStripMenuItemDeleteSecurity_Click(object sender, System.EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page.Tag as PageDecorator;
			if (selectedPage == null) return;
			if (AppManager.Instance.ShowQuestion("Are You sure You want to delete security settings?") != DialogResult.Yes) return;
			selectedPage.DeleteSecurity();
		}

		private void toolStripMenuItemDeleteTags_Click(object sender, System.EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page.Tag as PageDecorator;
			if (selectedPage == null) return;
			if (AppManager.Instance.ShowQuestion("Are You sure You want to wipe tags?") != DialogResult.Yes) return;
			selectedPage.DeleteTags();
		}

		private void toolStripMenuItemDeleteWidgets_Click(object sender, System.EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page.Tag as PageDecorator;
			if (selectedPage == null) return;
			if (AppManager.Instance.ShowQuestion("Are You sure You want to remove widgets?") != DialogResult.Yes) return;
			selectedPage.DeleteWidgets();
		}

		private void toolStripMenuItemDeleteBanners_Click(object sender, System.EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page.Tag as PageDecorator;
			if (selectedPage == null) return;
			if (AppManager.Instance.ShowQuestion("Are You sure You want to remove banners?") != DialogResult.Yes) return;
			selectedPage.DeleteBanners();
		}

		private void toolStripMenuItemDelete_Click(object sender, System.EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page.Tag as PageDecorator;
			if (selectedPage == null) return;
			if (AppManager.Instance.ShowQuestion("Are You sure You want to delete this page?") != DialogResult.Yes) return;
			if (!MainController.Instance.SaveLibraryWarning()) return;
			selectedPage.Page.Folders.SelectMany(f => f.Files).OfType<LibraryLink>().ToList().ForEach(f => f.RemoveFromCollection());
			selectedPage.Page.Parent.Pages.Remove(selectedPage.Page);
			MainController.Instance.RequestUpdateLibrary((Library)selectedPage.Page.Parent);
		}

		private void toolStripMenuItemRename_Click(object sender, System.EventArgs e)
		{
			var selectedPage = _menuHitInfo.Page.Tag as PageDecorator;
			if (selectedPage == null) return;
			if (!MainController.Instance.SaveLibraryWarning()) return;
			using (var form = new FormPageName(selectedPage.Page))
			{
				if (form.ShowDialog(FormMain.Instance) != DialogResult.OK) return;
				SettingsManager.Instance.SelectedPage = selectedPage.Page.Name;
				SettingsManager.Instance.Save();
				MainController.Instance.RequestUpdateLibrary((Library)selectedPage.Page.Parent);
			}
		}
	}
}