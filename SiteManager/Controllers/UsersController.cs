using System;
using SalesDepot.Services;
using SalesDepot.SiteManager;
using SalesDepot.SiteManager.BusinessClasses;
using SalesDepot.SiteManager.PresentationClasses.Users;
using SalesDepot.SiteManager.TabPages;

namespace FileManager.Controllers
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
			FormMain.Instance.comboBoxEditUsersSite.Properties.Items.AddRange(SiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditUsersSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditUsersSite.EditValue as SiteClient);
			};
			FormMain.Instance.buttonItemUsersAdd.Click += buttonItemIPadUsersAdd_Click;
			FormMain.Instance.buttonItemUsersEdit.Click += buttonItemIPadUsersEdit_Click;
			FormMain.Instance.buttonItemUsersDelete.Click += buttonItemIPadUsersDelete_Click;
			FormMain.Instance.buttonItemUsersRefresh.Click += buttonItemIPadUsersRefresh_Click;
			FormMain.Instance.buttonItemUsersImport.Click += buttonItemIPadUsersImport_Click;
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
				FormMain.Instance.comboBoxEditUsersSite.EditValue = SiteManager.Instance.SelectedSite;
				PermissionsManagerControl.RefreshData(true);
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void buttonItemIPadUsersAdd_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.AddObject();
		}

		private void buttonItemIPadUsersEdit_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.EditObject();
		}

		private void buttonItemIPadUsersDelete_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.DeleteObject();
		}

		private void buttonItemIPadUsersRefresh_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.RefreshData(true);
		}

		private void buttonItemIPadUsersImport_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.ImportUsers();
		}
	}
}