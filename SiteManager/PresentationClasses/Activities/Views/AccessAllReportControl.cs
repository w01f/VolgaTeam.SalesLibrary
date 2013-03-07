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
	public partial class AccessAllReportControl : UserControl, IActivitiesView
	{
		private readonly List<AccessReportRecord> _records = new List<AccessReportRecord>();
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

		private AccessAllFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public AccessAllReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new AccessAllFilter();
			_filterControl.FilterChanged += (o, e) => ApplyData();
			_filterControl.ColumnsChanged += (o, e) => ApplyColumns();
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetAccessReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetAccessReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.Select(x => x.name).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			gridControlData.DataSource = null;
			_records.Clear();
		}

		private void ApplyData()
		{
			var filteredRecords = new List<AccessReportRecord>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => _filterControl.SelectedGroups.Contains(x.name)) : _records);
			var allActive = filteredRecords.Sum(x => x.activeCount);
			var allInactive = filteredRecords.Sum(x => x.inactiveCount);
			var allUsers = filteredRecords.Sum(x => x.userCount);
			var groups = string.Join(", ", _filterControl.SelectedGroups);
			foreach (var record in filteredRecords)
			{
				record.Groups = groups;
				record.AllActive = allActive;
				record.AllInactive = allInactive;
				record.AllUsers = allUsers;
			}
			gridControlData.DataSource = filteredRecords.Count > 0 ? new[] { filteredRecords.FirstOrDefault() } : null;
		}

		private void ApplyColumns()
		{
			gridColumnActiveNumber.Visible = _filterControl.ShowNumber;
			gridColumnActivePercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnActiveNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnActivePercent, 0, 1);
			gridColumnInactiveNumber.Visible = _filterControl.ShowNumber;
			gridColumnInactivePercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnInactiveNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnInactivePercent, 0, 1);
		}

		private void gridViewData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
		{
			if (e.Column.SortMode != DevExpress.XtraGrid.ColumnSortMode.Custom || e.Value1 == null || e.Value2 == null) return;
			e.Handled = true;
			e.Result = WinAPIHelper.StrCmpLogicalW(e.Value1.ToString(), e.Value2.ToString());
		}
	}
}