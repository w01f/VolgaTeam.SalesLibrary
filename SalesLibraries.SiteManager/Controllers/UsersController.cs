using System;
using SalesLibraries.ServiceConnector.Services.Soap;
using SalesLibraries.SiteManager.PresentationClasses.Users;
using SalesLibraries.SiteManager.TabPages;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.Controllers
{
	public class UsersController : IPageController
	{
		private TabUsersControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public PermissionsManagerControl PermissionsManagerControl { get; private set; }

		public UsersController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabUsersControl();

			PermissionsManagerControl = new PermissionsManagerControl();
			if (!_tabPage.Controls.Contains(PermissionsManagerControl))
				_tabPage.Controls.Add(PermissionsManagerControl);
			PermissionsManagerControl.BringToFront();

			FormMain.Instance.comboBoxEditUsersSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditUsersSite.Properties.Items.AddRange(WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditUsersSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditUsersSite.EditValue as SoapServiceConnection);
			};
			FormMain.Instance.buttonItemUsersAdd.Click += OnUsersAddClick;
			FormMain.Instance.buttonItemUsersEdit.Click += OnUsersEditClick;
			FormMain.Instance.buttonItemUsersDelete.Click += OnUsersDeleteClick;
			FormMain.Instance.buttonItemUsersRefresh.Click += OnUsersRefreshClick;
			FormMain.Instance.buttonItemUsersImport.Click += OnUsersImportClick;
			FormMain.Instance.buttonItemUsersExport.Click += OnUsersExportClick;
			FormMain.Instance.buttonItemUsersSettings.Click += OnUsersSettingsClick;
			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   PermissionsManagerControl.RefreshData(true);
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
				FormMain.Instance.comboBoxEditUsersSite.EditValue = WebSiteManager.Instance.SelectedSite;
				PermissionsManagerControl.RefreshData(true);
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void OnUsersAddClick(object sender, EventArgs e)
		{
			PermissionsManagerControl.AddObject();
		}

		private void OnUsersEditClick(object sender, EventArgs e)
		{
			PermissionsManagerControl.EditObject();
		}

		private void OnUsersDeleteClick(object sender, EventArgs e)
		{
			PermissionsManagerControl.DeleteObject();
		}

		private void OnUsersRefreshClick(object sender, EventArgs e)
		{
			PermissionsManagerControl.RefreshData(true);
		}

		private void OnUsersImportClick(object sender, EventArgs e)
		{
			PermissionsManagerControl.ImportUsers();
		}

		private void OnUsersExportClick(object sender, EventArgs e)
		{
			PermissionsManagerControl.Export();
		}

		private void OnUsersSettingsClick(Object sender, EventArgs e)
		{
			using (var form = new FormUsersSettings())
			{
				form.ShowDialog(FormMain.Instance);
			}
		}

	}
}