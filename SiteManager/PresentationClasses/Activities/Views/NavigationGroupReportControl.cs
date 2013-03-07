﻿using System;
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
	public partial class NavigationGroupReportControl : UserControl, IActivitiesView
	{
		private readonly List<NavigationGroupReportRecord> _records = new List<NavigationGroupReportRecord>();
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

		private NavigationGroupFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public NavigationGroupReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new NavigationGroupFilter();
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetNavigationGroupReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetNavigationGroupReport(StartDate, EndDate, out message)));
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
			var filteredRecords = new List<NavigationGroupReportRecord>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => _filterControl.SelectedGroups.Contains(x.name)) : _records);
			var allLibraries = filteredRecords.Sum(x => x.libs);
			var allPages = filteredRecords.Sum(x => x.pages);
			var allTotals = filteredRecords.Sum(x => x.totals);
			foreach (var record in filteredRecords)
			{
				record.AllLibraries = allLibraries;
				record.AllPages = allPages;
				record.AllTotals = allTotals;
			}
			gridControlData.DataSource = filteredRecords;
		}

		private void ApplyColumns()
		{
			gridColumnGroupLibrariesNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupLibrariesPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupLibrariesNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupLibrariesPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnAllLibrariesNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnAllLibrariesNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnGroupPagesNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupPagesPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupPagesNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupPagesPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnAllPagesNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnAllPagesNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnGroupTotalNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupTotalPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupTotalNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupTotalPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnAllTotalNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnAllTotalNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;
		}

		private void gridViewData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
		{
			if (e.Column.SortMode != DevExpress.XtraGrid.ColumnSortMode.Custom || e.Value1 == null || e.Value2 == null) return;
			e.Handled = true;
			e.Result = WinAPIHelper.StrCmpLogicalW(e.Value1.ToString(), e.Value2.ToString());
		}
	}
}