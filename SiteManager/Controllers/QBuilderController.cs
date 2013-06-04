using System;
using FileManager.Controllers;
using SalesDepot.Services;
using SalesDepot.SiteManager.PresentationClasses.QBuilder;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager.Controllers
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
			FormMain.Instance.comboBoxEditQBuilderSite.Properties.Items.AddRange(BusinessClasses.SiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditQBuilderSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditQBuilderSite.EditValue as SiteClient);
			};
			FormMain.Instance.buttonItemQBuilderRefresh.Click += buttonItemQBuilderRefresh_Click;
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
				FormMain.Instance.comboBoxEditQBuilderSite.EditValue = BusinessClasses.SiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void buttonItemQBuilderRefresh_Click(object sender, EventArgs e)
		{
			QPagesManagerControl.RefreshData(true);
		}
	}
}
