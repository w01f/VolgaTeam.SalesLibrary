﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetMainGroupReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetMainGroupReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.OrderBy(g => g.name).Select(x => x.name).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			gridControlData.DataSource = null;
			_records.Clear();
		}

		public void ExportData()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("GroupActivity({0}).xls", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xls";
				dialog.Title = "Export General Group Activity Report";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					var options = new XlsExportOptions();
					options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
					options.TextExportMode = TextExportMode.Text;
					options.ExportHyperlinks = true;
					options.ShowGridLines = true;
					options.ExportMode = XlsExportMode.SingleFile;
					printableComponentLink.CreateDocument();
					printableComponentLink.PrintingSystem.ExportToXls(dialog.FileName, options);

					if (File.Exists(dialog.FileName))
						Process.Start(dialog.FileName);
				}
			}
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

			gridColumnGroupDocsNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupDocsPercent.Visible = _filterControl.ShowPercent;

			gridColumnGroupVideosNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupVideosPercent.Visible = _filterControl.ShowPercent;

			gridColumnGroupTotalNumber.Visible = _filterControl.ShowNumber;
			gridColumnGroupTotalPercent.Visible = _filterControl.ShowPercent;
		}

		private void printableComponentLink_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("General Group Activity Report: {0} - {1}", StartDate.ToString("MM/dd/yy"), EndDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}

		private void advBandedGridViewData_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
		{
			if (e.Column != gridColumnName)
				e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
		}
	}
}