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
	public partial class MainGroupReportControl : UserControl, IActivitiesView
	{
		private readonly List<MainGroupReportRecord> _records = new List<MainGroupReportRecord>();
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

		private MainGroupFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public MainGroupReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new MainGroupFilter();
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetMainGroupReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetMainGroupReport(StartDate, EndDate, out message)));
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
			var filteredRecords = new List<MainGroupReportRecord>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => _filterControl.SelectedGroups.Contains(x.name)) : _records);
			var allLogins = filteredRecords.Sum(x => x.logins);
			var allDocs = filteredRecords.Sum(x => x.docs);
			var allVideos = filteredRecords.Sum(x => x.videos);
			var allTotals = filteredRecords.Sum(x => x.totals);
			foreach (var record in filteredRecords)
			{
				record.AllLogins = allLogins;
				record.AllDocs = allDocs;
				record.AllVideos = allVideos;
				record.AllTotals = allTotals;
			}
			gridControlData.DataSource = filteredRecords;
		}

		private void ApplyColumns()
		{
			gridColumnGroupLoginNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupLoginPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupLoginNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupLoginPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnAllLoginNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnAllLoginNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnGroupDocsNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupDocsPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupDocsNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupDocsPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnAllDocsNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnAllDocsNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnGroupVideosNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupVideosPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupVideosNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnGroupVideosPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnAllVideosNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnAllVideosNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

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