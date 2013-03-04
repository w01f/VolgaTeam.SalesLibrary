using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	[ToolboxItem(false)]
	public sealed partial class ActivitiesManagerControl : UserControl
	{
		private Dictionary<ViewType, IActivitiesView> _views = new Dictionary<ViewType, IActivitiesView>();

		public ActivitiesManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			InitViews();

			var now = DateTime.Now;
			dateEditStart.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
			now = now.AddDays(1);
			dateEditEnd.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
		}

		private void InitViews()
		{
			var view1 = new RawDataControl();
			_views.Add(ViewType.RawData, view1);
			splitContainerControl.Panel2.Controls.Add(view1);
			var view2 = new MainUserReportControl();
			_views.Add(ViewType.MainUserReport, view2);
			splitContainerControl.Panel2.Controls.Add(view2);
			var view3 = new MainGroupReportControl();
			_views.Add(ViewType.MainGroupReport, view3);
			splitContainerControl.Panel2.Controls.Add(view3);
			var view4 = new NavigationUserReportControl();
			_views.Add(ViewType.NavigationUserReport, view4);
			splitContainerControl.Panel2.Controls.Add(view4);
			var view5 = new NavigationGroupReportControl();
			_views.Add(ViewType.NavigationGroupReport, view5);
			splitContainerControl.Panel2.Controls.Add(view5);
		}

		public void ChangeView(ViewType viewType)
		{
			foreach (var view in _views.Values)
				view.Active = false;
			if (_views.ContainsKey(viewType))
				_views[viewType].ShowView();
		}

		public void RefreshData(bool showMessages)
		{
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading Activities...";
					form.TopMost = true;
					form.Show();
					Application.DoEvents();
					foreach (var view in _views.Values)
					{
						view.StartDate = dateEditStart.DateTime;
						view.EndDate = dateEditEnd.DateTime.AddDays(1);
						view.UpdateData(false, ref message);
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
				foreach (var view in _views.Values)
					view.UpdateData(true, ref message);
		}

		public void ClearData()
		{
			foreach (var view in _views.Values)
				view.ClearData();
		}

		private void buttonXApplyFilter_Click(object sender, EventArgs e)
		{
			RefreshData(true);
		}
	}
}