using System;
using FileManager.Controllers;
using SalesDepot.Services;
using SalesDepot.SiteManager.PresentationClasses.InactiveUsers;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager.Controllers
{
	public class InactiveUsersController : IPageController
	{
		private TabInactiveUsersControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public InactiveUsersManagerControl InactiveUsersManagerControl { get; private set; }

		public InactiveUsersController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabInactiveUsersControl();

			InactiveUsersManagerControl = new InactiveUsersManagerControl();
			if (!_tabPage.Controls.Contains(InactiveUsersManagerControl))
				_tabPage.Controls.Add(InactiveUsersManagerControl);
			InactiveUsersManagerControl.BringToFront();

			FormMain.Instance.buttonItemInactiveUsersExport.Click += buttonItemInactiveUsersExport_Click;
			FormMain.Instance.comboBoxEditInactiveUsersSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditInactiveUsersSite.Properties.Items.AddRange(BusinessClasses.SiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditInactiveUsersSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditInactiveUsersSite.EditValue as SiteClient);
			};
			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   InactiveUsersManagerControl.RefreshData(true);
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
				InactiveUsersManagerControl.ClearData();
				FormMain.Instance.comboBoxEditInactiveUsersSite.EditValue = BusinessClasses.SiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void buttonItemInactiveUsersExport_Click(object sender, EventArgs e)
		{
			InactiveUsersManagerControl.ExportUsers();
		}
	}
}
