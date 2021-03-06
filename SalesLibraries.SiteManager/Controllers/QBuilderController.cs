﻿using System;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.PresentationClasses.QBuilder;
using SalesLibraries.SiteManager.TabPages;
using SalesLibraries.ServiceConnector.Services.Soap;

namespace SalesLibraries.SiteManager.Controllers
{
	public class QBuilderController : IPageController
	{
		private TabQBuilderControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public QPagesManagerControl QPagesManagerControl { get; private set; }

		public QBuilderController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabQBuilderControl();

			QPagesManagerControl = new QPagesManagerControl();
			if (!_tabPage.Controls.Contains(QPagesManagerControl))
				_tabPage.Controls.Add(QPagesManagerControl);
			QPagesManagerControl.BringToFront();

			FormMain.Instance.comboBoxEditQBuilderSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditQBuilderSite.Properties.Items.AddRange(WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditQBuilderSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditQBuilderSite.EditValue as SoapServiceConnection);
			};

			FormMain.Instance.buttonItemQBuilder.Click += buttonItemQBuilderExport_Click;

			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   QPagesManagerControl.RefreshData(true);
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
				QPagesManagerControl.RefreshData(true);
				FormMain.Instance.comboBoxEditQBuilderSite.EditValue = WebSiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void buttonItemQBuilderExport_Click(object sender, EventArgs e)
		{
			QPagesManagerControl.ExportData();
		}
	}
}
