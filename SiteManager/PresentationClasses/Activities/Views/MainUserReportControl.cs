using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.PresentationClasses.Activities.Filters;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.Views
{
	[ToolboxItem(false)]
	public partial class MainUserReportControl : UserControl, IActivitiesView
	{
		private readonly List<MainUserReportRecord> _records = new List<MainUserReportRecord>();
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		private bool _active;
		public bool Active
		{
			get { return _active; }
			set
			{
				_active = value;
				Visible = _active;
				_filterControl.Visible = _active;
			}
		}

		private MainUserFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public MainUserReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new MainUserFilter();
			_filterControl.FilterChanged += (o, e) => ApplyData();
		}

		public void ShowView()
		{
			Active = true;
			BringToFront();
			_filterControl.BringToFront();
		}

		public void UpdateData(bool showMessages, ref string updateMessage)
		{
			ClearData();
			if (!Active) return;
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetMainUserReport(StartDate, EndDate, out message)));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetMainUserReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.Select(x => x.group).Where(x => !string.IsNullOrEmpty(x)).ToArray());
			ApplyData();
			gridControlData.DataSource = _records;
		}

		public void ClearData()
		{
			gridControlData.DataSource = null;
			_records.Clear();
		}

		private void ApplyData()
		{
			var filteredRecords = new List<MainUserReportRecord>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => _filterControl.SelectedGroups.Contains(x.group)) : _records);
			gridControlData.DataSource = filteredRecords;
		}

		private void gridViewData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
		{
			if (e.Column.SortMode != DevExpress.XtraGrid.ColumnSortMode.Custom || e.Value1 == null || e.Value2 == null) return;
			e.Handled = true;
			e.Result = WinAPIHelper.StrCmpLogicalW(e.Value1.ToString(), e.Value2.ToString());
		}
	}
}