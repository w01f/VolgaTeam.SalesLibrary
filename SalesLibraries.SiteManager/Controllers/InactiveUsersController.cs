﻿using System;
using SalesLibraries.ServiceConnector.Services.Soap;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.PresentationClasses.InactiveUsers;
using SalesLibraries.SiteManager.TabPages;

namespace SalesLibraries.SiteManager.Controllers
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

			FormMain.Instance.buttonItemInactiveUsersReset.Click += OnInactiveUsersResetClick;
			FormMain.Instance.buttonItemInactiveUsersDelete.Click += OnInactiveUsersDeleteClick;
			FormMain.Instance.buttonItemInactiveUsersExport.Click += OnInactiveUsersExportClick;
			FormMain.Instance.comboBoxEditInactiveUsersSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditInactiveUsersSite.Properties.Items.AddRange(WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditInactiveUsersSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditInactiveUsersSite.EditValue as SoapServiceConnection);
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
				FormMain.Instance.comboBoxEditInactiveUsersSite.EditValue = WebSiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void OnInactiveUsersResetClick(object sender, EventArgs e)
		{
			InactiveUsersManagerControl.ResetUsers();
		}

		private void OnInactiveUsersDeleteClick(object sender, EventArgs e)
		{
			InactiveUsersManagerControl.DeleteUsers();
		}

		private void OnInactiveUsersExportClick(object sender, EventArgs e)
		{
			InactiveUsersManagerControl.ExportUsers();
		}
	}
}
