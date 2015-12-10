using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public partial class TagsCleaner : UserControl, IGroupSettingsEditor
	{
		public TagsCleaner()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region IGroupSettingsEditor Members
		public string Title
		{
			get { return "Blow Up Tags"; }
		}
		public bool NeedToApply { get; set; }

		public event EventHandler<EventArgs> EditorChanged;
		public void UpdateData() { }
		public void ApplyData() { }
		public void ResetData() { }
		#endregion

		private void ClearCategories(bool allPages)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = (allPages ? MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages : new[] { MainController.Instance.WallbinViews.ActiveWallbin.ActivePage.Page }).ToList();
			pages.SelectMany(p => p.AllLinks).ApplyCategories(new SearchGroup[] { });
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void ClearSuperFilters(bool allPages)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = (allPages ? MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages : new[] { MainController.Instance.WallbinViews.ActiveWallbin.ActivePage.Page }).ToList();
			pages.SelectMany(p => p.AllLinks).ApplySuperFilters(new string[] { });
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void ClearKeywords(bool allPages)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = (allPages ? MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages : new[] { MainController.Instance.WallbinViews.ActiveWallbin.ActivePage.Page }).ToList();
			pages.SelectMany(p => p.AllLinks).ApplyKeywords(new SearchTag[] { });
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void ClearSecurity(bool allPages)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = (allPages ? MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages : new[] { MainController.Instance.WallbinViews.ActiveWallbin.ActivePage.Page }).ToList();
			pages.SelectMany(p => p.AllLinks).ResetSecurity();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		private void hyperLinkEditCategoriesActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearCategories(false);
			e.Handled = true;
		}

		private void hyperLinkEditCategoriesAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearCategories(true);
			e.Handled = true;
		}

		private void hyperLinkEditSuperFiltersActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSuperFilters(false);
			e.Handled = true;
		}

		private void hyperLinkEditSuperFiltersAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSuperFilters(true);
			e.Handled = true;
		}

		private void hyperLinkEditKeywordsActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearKeywords(false);
			e.Handled = true;
		}

		private void hyperLinkEditKeywordsAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearKeywords(true);
			e.Handled = true;
		}

		private void hyperLinkEditSecurityActivePage_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSecurity(false);
			e.Handled = true;
		}

		private void hyperLinkEditSecurityAllPages_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ClearSecurity(true);
			e.Handled = true;
		}
	}
}
