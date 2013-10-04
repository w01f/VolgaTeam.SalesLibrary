using System;
using SalesDepot.Services;
using SalesDepot.SiteManager.PresentationClasses.Ticker;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager.Controllers
{
	public class TickerController : IPageController
	{
		private TabTickerControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public TickerManagerControl TickerManagerControl { get; private set; }

		public TickerController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabTickerControl();

			TickerManagerControl = new TickerManagerControl();
			if (!_tabPage.Controls.Contains(TickerManagerControl))
				_tabPage.Controls.Add(TickerManagerControl);
			TickerManagerControl.BringToFront();

			FormMain.Instance.comboBoxEditTickerSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditTickerSite.Properties.Items.AddRange(BusinessClasses.WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditTickerSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditTickerSite.EditValue as SiteClient);
			};
			FormMain.Instance.buttonItemTickerAdd.Click += buttonItemTickerAdd_Click;
			FormMain.Instance.buttonItemTickerEdit.Click += buttonItemTickerEdit_Click;
			FormMain.Instance.buttonItemTickerDelete.Click += buttonItemTickerDelete_Click;
			FormMain.Instance.buttonItemTickerExport.Click += buttonItemTickerExport_Click;
			FormMain.Instance.buttonItemTickerImport.Click += buttonItemTickerImport_Click;
			FormMain.Instance.buttonItemTickerRefresh.Click += buttonItemTickerRefresh_Click;
			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   TickerManagerControl.RefreshData(true);
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
				TickerManagerControl.RefreshData(true);
				FormMain.Instance.comboBoxEditTickerSite.EditValue = BusinessClasses.WebSiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void buttonItemTickerAdd_Click(object sender, EventArgs e)
		{
			TickerManagerControl.AddObject();
		}

		private void buttonItemTickerEdit_Click(object sender, EventArgs e)
		{
			TickerManagerControl.EditObject();
		}

		private void buttonItemTickerDelete_Click(object sender, EventArgs e)
		{
			TickerManagerControl.DeleteObject();
		}

		private void buttonItemTickerRefresh_Click(object sender, EventArgs e)
		{
			TickerManagerControl.RefreshData(true);
		}

		private void buttonItemTickerImport_Click(object sender, EventArgs e)
		{
			TickerManagerControl.ImportUsers();
		}

		private void buttonItemTickerExport_Click(object sender, EventArgs e)
		{
			TickerManagerControl.ExportUsers();
		}
	}
}
