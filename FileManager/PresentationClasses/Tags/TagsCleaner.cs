using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using FileManager.Controllers;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class TagsCleaner : UserControl, ITagsEditor
	{
		private bool _needToApply;
		public bool NeedToApply
		{
			get { return _needToApply; }
			set
			{
				_needToApply = value;
				var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
				if (activePage != null) activePage.Parent.StateChanged = true;
				if (EditorChanged != null)
					EditorChanged(this, new EventArgs());
				MainController.Instance.WallbinController.UpdateTagCountInfo();

			}
		}
		public TagsCleaner()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;
		public void UpdateData() { }
		public void ApplyData() { }

		public void ResetData() { }
		#endregion

		private void ClearCategories(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = allPages ? MainController.Instance.ActiveDecorator.Library.Pages : new[] { activePage.Page }.ToList();
			foreach (var link in pages.SelectMany(p => p.Folders.SelectMany(folder => folder.Files)))
				link.SearchTags.SearchGroups.Clear();
			NeedToApply = true;
		}

		private void ClearSuperFilters(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = allPages ? MainController.Instance.ActiveDecorator.Library.Pages : new[] { activePage.Page }.ToList();
			foreach (var link in pages.SelectMany(p => p.Folders.SelectMany(folder => folder.Files)))
				link.SuperFilters.Clear(); ;
			NeedToApply = true;
		}

		private void ClearKeywords(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = allPages ? MainController.Instance.ActiveDecorator.Library.Pages : new[] { activePage.Page }.ToList();
			foreach (var link in pages.SelectMany(p => p.Folders.SelectMany(folder => folder.Files)))
				link.CustomKeywords.Tags.Clear();
			NeedToApply = true;
		}

		private void ClearSecurity(bool allPages)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are You Sure ?") != DialogResult.Yes) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you ABSOLUTELY 100% POSITIVE?") != DialogResult.Yes) return;
			var pages = allPages ? MainController.Instance.ActiveDecorator.Library.Pages : new[] { activePage.Page }.ToList();
			foreach (var link in pages.SelectMany(p => p.Folders.SelectMany(folder => folder.Files)))
			{
				link.IsRestricted = false;
				link.NoShare = false;
				link.IsForbidden = false;
				link.AssignedUsers = null;
				link.DeniedUsers = null;
			}
			NeedToApply = true;
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
