using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SalesDepot.SiteManager.PresentationClasses.Activities.Views;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	[ToolboxItem(false)]
	public sealed partial class ActivitiesManagerControl : UserControl
	{
		private ViewType _selectedViewType;
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
			if (view1.FilterControl != null)
				pnCustomFilter.Controls.Add(view1.FilterControl);
			var view2 = new MainUserReportControl();
			_views.Add(ViewType.MainUserReport, view2);
			splitContainerControl.Panel2.Controls.Add(view2);
			if (view2.FilterControl != null)
				pnCustomFilter.Controls.Add(view2.FilterControl);
			var view3 = new MainGroupReportControl();
			_views.Add(ViewType.MainGroupReport, view3);
			splitContainerControl.Panel2.Controls.Add(view3);
			if (view3.FilterControl != null)
				pnCustomFilter.Controls.Add(view3.FilterControl);
			var view4 = new NavigationUserReportControl();
			_views.Add(ViewType.NavigationUserReport, view4);
			splitContainerControl.Panel2.Controls.Add(view4);
			if (view4.FilterControl != null)
				pnCustomFilter.Controls.Add(view4.FilterControl);
			var view5 = new NavigationGroupReportControl();
			_views.Add(ViewType.NavigationGroupReport, view5);
			splitContainerControl.Panel2.Controls.Add(view5);
			if (view5.FilterControl != null)
				pnCustomFilter.Controls.Add(view5.FilterControl);
			var view6 = new AccessGroupReportControl();
			_views.Add(ViewType.AccessGroupReport, view6);
			splitContainerControl.Panel2.Controls.Add(view6);
			if (view6.FilterControl != null)
				pnCustomFilter.Controls.Add(view6.FilterControl);
			var view7 = new AccessAllReportControl();
			_views.Add(ViewType.AccessAllReport, view7);
			splitContainerControl.Panel2.Controls.Add(view7);
			if (view7.FilterControl != null)
				pnCustomFilter.Controls.Add(view7.FilterControl);
			var view8 = new QuizPassUserReportControl();
			_views.Add(ViewType.QuizPassUserReport, view8);
			splitContainerControl.Panel2.Controls.Add(view8);
			if (view8.FilterControl != null)
				pnCustomFilter.Controls.Add(view8.FilterControl);
			var view9 = new QuizPassGroupReportControl();
			_views.Add(ViewType.QuizPassGroupReport, view9);
			splitContainerControl.Panel2.Controls.Add(view9);
			if (view9.FilterControl != null)
				pnCustomFilter.Controls.Add(view9.FilterControl);
			var view10 = new QuizStatusUserReportControl();
			_views.Add(ViewType.QuizStatusUserReport, view10);
			splitContainerControl.Panel2.Controls.Add(view10);
			if (view10.FilterControl != null)
				pnCustomFilter.Controls.Add(view10.FilterControl);
			var view11 = new QuizUnitedReportControl();
			_views.Add(ViewType.QuizUnitedReport, view11);
			splitContainerControl.Panel2.Controls.Add(view11);
			if (view11.FilterControl != null)
				pnCustomFilter.Controls.Add(view11.FilterControl);
		}

		public void ChangeView(ViewType viewType)
		{
			_selectedViewType = viewType;
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

		public void ExportData()
		{
			if (_views.ContainsKey(_selectedViewType))
				_views[_selectedViewType].ExportData();
		}

		private void buttonXLoadData_Click(object sender, EventArgs e)
		{
			RefreshData(true);
		}
	}
}