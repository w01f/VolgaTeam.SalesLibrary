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
	public partial class NavigationUserReportControl : UserControl, IActivitiesView
	{
		private readonly List<NavigationUserReportRecord> _records = new List<NavigationUserReportRecord>();
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

		private NavigationUserFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public NavigationUserReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new NavigationUserFilter();
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetNavigationUserReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetNavigationUserReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.Select(x => x.group).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			gridControlData.DataSource = null;
			_records.Clear();
		}

		private void ApplyData()
		{
			var filteredRecords = new List<NavigationUserReportRecord>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => _filterControl.SelectedGroups.Contains(x.group)) : _records);
			gridControlData.DataSource = filteredRecords;
		}

		private void ApplyColumns()
		{
			gridColumnUserLibrariesNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserLibrariesPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLibrariesNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLibrariesPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupLibrariesNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnGroupLibrariesNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnUserPagesNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserPagesPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserPagesNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserPagesPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupPagesNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnGroupPagesNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnUserTotalNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserTotalPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupTotalNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnGroupTotalNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;
		}

		private void gridViewData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
		{
			if (e.Column.SortMode != DevExpress.XtraGrid.ColumnSortMode.Custom || e.Value1 == null || e.Value2 == null) return;
			e.Handled = true;
			e.Result = WinAPIHelper.StrCmpLogicalW(e.Value1.ToString(), e.Value2.ToString());
		}
	}
}