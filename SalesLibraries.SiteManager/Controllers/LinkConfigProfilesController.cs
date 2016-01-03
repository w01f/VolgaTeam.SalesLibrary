using System;
using SalesLibraries.ServiceConnector.Services;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.PresentationClasses.LinkConfigProfiles;
using SalesLibraries.SiteManager.TabPages;

namespace SalesLibraries.SiteManager.Controllers
{
	public class LinkConfigProfilesController : IPageController
	{
		private TabLinkConfigProfilesControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public LinkConfigProfilesManagerControl LinkConfigProfilesManagerControl { get; private set; }

		public LinkConfigProfilesController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabLinkConfigProfilesControl();

			LinkConfigProfilesManagerControl = new LinkConfigProfilesManagerControl();
			if (!_tabPage.Controls.Contains(LinkConfigProfilesManagerControl))
				_tabPage.Controls.Add(LinkConfigProfilesManagerControl);
			LinkConfigProfilesManagerControl.BringToFront();

			FormMain.Instance.buttonItemLinkConfigProfilesAdd.Click += OnLinkConfigProfilesAdd;
			FormMain.Instance.buttonItemLinkConfigProfilesDelete.Click += OnLinkConfigProfilesDelete;
			FormMain.Instance.buttonItemLinkConfigProfilesSave.Click += OnLinkConfigProfilesSave;
			FormMain.Instance.buttonItemLinkConfigProfilesExportFiles.Click += OnLinkConfigProfilesExportFiles;
			
			FormMain.Instance.comboBoxEditLinkConfigProfilesSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditLinkConfigProfilesSite.Properties.Items.AddRange(WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditLinkConfigProfilesSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditLinkConfigProfilesSite.EditValue as ServiceConnection);
			};
			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   LinkConfigProfilesManagerControl.RefreshData(true);
														   else
															   NeedToUpdate = true;
													   };

			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);
		}

		public void PrepareTab(TabPageEnum tabPage) { }

		public void ShowTab()
		{
			_tabPage.BringToFront();
			if (NeedToUpdate)
			{
				FormMain.Instance.comboBoxEditLinkConfigProfilesSite.EditValue = WebSiteManager.Instance.SelectedSite;
				LinkConfigProfilesManagerControl.RefreshData(true);
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void OnLinkConfigProfilesAdd(object sender, EventArgs e)
		{
			LinkConfigProfilesManagerControl.AddProfile();
		}

		private void OnLinkConfigProfilesDelete(object sender, EventArgs e)
		{
			LinkConfigProfilesManagerControl.DeleteProfile();
		}
		private void OnLinkConfigProfilesSave(object sender, EventArgs e)
		{
			LinkConfigProfilesManagerControl.SaveData();
		}

		private void OnLinkConfigProfilesExportFiles(object sender, EventArgs e)
		{
			LinkConfigProfilesManagerControl.ExportFiles();
		}
	}
}
