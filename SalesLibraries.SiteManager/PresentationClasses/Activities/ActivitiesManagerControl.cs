using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.SiteManager.ToolForms;
using ContainerControl = SalesLibraries.SiteManager.PresentationClasses.Activities.RawData.ContainerControl;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities
{
	[ToolboxItem(false)]
	public sealed partial class ActivitiesManagerControl : UserControl
	{
		private ViewType _selectedViewType;
		private readonly Dictionary<ViewType, IActivitiesView> _views = new Dictionary<ViewType, IActivitiesView>();

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
			var view1 = new ContainerControl();
			_views.Add(ViewType.RawData, view1);
			splitContainerControl.Panel2.Controls.Add(view1);
			pnCustomFilter.Controls.AddRange(view1.FilterControls.ToArray());
			var view2 = new MainData.ContainerControl();
			_views.Add(ViewType.MainUserReport, view2);
			splitContainerControl.Panel2.Controls.Add(view2);
			pnCustomFilter.Controls.AddRange(view2.FilterControls.ToArray());
			var view3 = new AccessData.ContainerControl();
			_views.Add(ViewType.AccessGroupReport, view3);
			splitContainerControl.Panel2.Controls.Add(view3);
			pnCustomFilter.Controls.AddRange(view3.FilterControls.ToArray());
			var view4 = new QuizPassData.ContainerControl();
			_views.Add(ViewType.QuizPassUserReport, view4);
			splitContainerControl.Panel2.Controls.Add(view4);
			pnCustomFilter.Controls.AddRange(view4.FilterControls.ToArray());
			var view5 = new QuizStatusData.ContainerControl();
			_views.Add(ViewType.QuizStatusUserReport, view5);
			splitContainerControl.Panel2.Controls.Add(view5);
			pnCustomFilter.Controls.AddRange(view5.FilterControls.ToArray());
			var view6 = new QuizUnitedData.ContainerControl();
			_views.Add(ViewType.QuizUnitedReport, view6);
			splitContainerControl.Panel2.Controls.Add(view6);
			pnCustomFilter.Controls.AddRange(view6.FilterControls.ToArray());
			var view7 = new FileActivityData.ContainerControl();
			_views.Add(ViewType.FileActivityReport, view7);
			splitContainerControl.Panel2.Controls.Add(view7);
			pnCustomFilter.Controls.AddRange(view7.FilterControls.ToArray());
			var view8 = new VideoLinkData.ContainerControl();
			_views.Add(ViewType.VideoInfoReport, view8);
			splitContainerControl.Panel2.Controls.Add(view8);
			pnCustomFilter.Controls.AddRange(view8.FilterControls.ToArray());
			var view9 = new FileActivityData.LegacyContainerControl();
			_views.Add(ViewType.FileActivityReportLegacy, view9);
			splitContainerControl.Panel2.Controls.Add(view9);
			pnCustomFilter.Controls.AddRange(view9.FilterControls.ToArray());
		}

		public void ChangeView(ViewType viewType)
		{
			_selectedViewType = viewType;
			foreach (var view in _views.Values)
				view.Active = false;
			if (_views.ContainsKey(viewType))
				_views[viewType].ShowView();
			gbDate.Visible = viewType != ViewType.VideoInfoReport;
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