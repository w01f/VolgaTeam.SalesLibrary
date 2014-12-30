using System;
using SalesDepot.Services;
using SalesDepot.SiteManager.BusinessClasses;
using SalesDepot.SiteManager.PresentationClasses.Activities;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager.Controllers
{
	public class ActivitiesController : IPageController
	{
		private TabActivitiesControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public ActivitiesManagerControl ActivitiesManagerControl { get; private set; }

		public ActivitiesController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabActivitiesControl();

			ActivitiesManagerControl = new ActivitiesManagerControl();
			if (!_tabPage.Controls.Contains(ActivitiesManagerControl))
				_tabPage.Controls.Add(ActivitiesManagerControl);
			ActivitiesManagerControl.BringToFront();

			FormMain.Instance.comboBoxEditActivitiesSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditActivitiesSite.Properties.Items.AddRange(WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditActivitiesSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditActivitiesSite.EditValue as SiteClient);
			};

			FormMain.Instance.buttonItemActivitiesViewsRawData.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport1.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport2.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport3.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport4.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport5.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport6.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport7.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsReport8.Click += buttonItemActivitiesViews_Click;
			FormMain.Instance.buttonItemActivitiesViewsRawData.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport1.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport2.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport3.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport4.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport5.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport6.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport7.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsReport8.CheckedChanged += buttonItemActivitiesViews_CheckedChanged;
			FormMain.Instance.buttonItemActivitiesViewsRawData.Checked = true;

			FormMain.Instance.buttonItemActivitiesExport.Click += buttonItemActivitiesExport_Click;

			MainController.Instance.SiteChanged += (sender, args) =>
			{
				if (IsActive)
					ActivitiesManagerControl.ClearData();
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
				ActivitiesManagerControl.ClearData();
				FormMain.Instance.comboBoxEditActivitiesSite.EditValue = WebSiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void buttonItemActivitiesViews_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonItem;
			if (button == null || !button.Checked || button.Tag == null) return;
			ViewType viewType;
			Enum.TryParse(button.Tag.ToString(), out viewType);
			ActivitiesManagerControl.ChangeView(viewType);
		}

		private void buttonItemActivitiesViews_Click(object sender, System.EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonItem;
			if (button == null || button.Checked) return;
			FormMain.Instance.buttonItemActivitiesViewsRawData.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport1.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport2.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport2.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport4.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport3.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport4.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport5.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport6.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport7.Checked = false;
			FormMain.Instance.buttonItemActivitiesViewsReport8.Checked = false;
			button.Checked = true;
		}

		private void buttonItemActivitiesExport_Click(object sender, EventArgs e)
		{
			ActivitiesManagerControl.ExportData();
		}
	}
}