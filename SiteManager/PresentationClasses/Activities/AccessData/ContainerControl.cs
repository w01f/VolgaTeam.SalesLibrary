﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.ToolClasses;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.AccessData
{
	[ToolboxItem(false)]
	public partial class ContainerControl : UserControl, IActivitiesView
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
				_groupFilterControl.Visible = _active;
			}
		}

		private readonly GroupFilter _groupFilterControl;
		private readonly TotalFilter _totalFilterControl;
		public IEnumerable<Control> FilterControls
		{
			get { return new Control[] { _groupFilterControl, _totalFilterControl }; }
		}

		public ContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			_groupFilterControl = new GroupFilter();
			_groupFilterControl.FilterChanged += (o, e) =>
			{
				_totalFilterControl.EnableFilter = _groupFilterControl.EnableFilter;
				_totalFilterControl.SelectedGroups.Clear();
				_totalFilterControl.SelectedGroups.AddRange(_groupFilterControl.SelectedGroups);
				_totalFilterControl.UpdateDataSource(_groupFilterControl.AllGroups.ToArray(), false);
				ApplyData();
			};
			_groupFilterControl.ColumnsChanged += (o, e) =>
			{
				_totalFilterControl.ShowNumber = _groupFilterControl.ShowNumber;
				_totalFilterControl.ShowPercent = _groupFilterControl.ShowPercent;
			};

			_totalFilterControl = new TotalFilter();
			_totalFilterControl.FilterChanged += (o, e) =>
			{
				_groupFilterControl.EnableFilter = _totalFilterControl.EnableFilter;
				_groupFilterControl.SelectedGroups.Clear();
				_groupFilterControl.SelectedGroups.AddRange(_totalFilterControl.SelectedGroups);
				_groupFilterControl.UpdateDataSource(_totalFilterControl.AllGroups.ToArray(), false);
				ApplyData();
			};
			_totalFilterControl.ColumnsChanged += (o, e) =>
			{
				_groupFilterControl.ShowNumber = _totalFilterControl.ShowNumber;
				_groupFilterControl.ShowPercent = _totalFilterControl.ShowPercent;

			};
		}

		public void ShowView()
		{
			Active = true;
			BringToFront();
			xtraTabControlGroups_SelectedPageChanged(xtraTabControlGroups, new TabPageChangedEventArgs(null, xtraTabControlGroups.SelectedTabPage));
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetAccessReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetAccessReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_groupFilterControl.UpdateDataSource(_records.OrderBy(g => g.name).Select(x => x.name).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
			_totalFilterControl.UpdateDataSource(_groupFilterControl.AllGroups.ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			xtraTabControlGroups.TabPages.Clear();
			_records.Clear();
		}

		public void ExportData()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("GroupAccess({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "Export Individual User Group Site Access Report";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				var options = new XlsxExportOptions();
				options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
				options.TextExportMode = TextExportMode.Text;
				options.ExportHyperlinks = true;
				options.ShowGridLines = true;
				options.ExportMode = XlsxExportMode.SingleFile;

				var groupControls = xtraTabControlGroups.TabPages.OfType<IGroupControl>().Reverse();
				var parts = new Dictionary<string, string>();
				foreach (var groupControl in groupControls)
				{
					using (var printingSystem = new PrintingSystem())
					{
						groupControl.PrintLink.CreateDocument(printingSystem);
						var tempFile = Path.Combine(Path.GetTempPath(), String.Format("{0}.xlsx", Guid.NewGuid()));
						printingSystem.ExportToXlsx(tempFile, options);
						parts.Add(groupControl.GroupName, tempFile);
					}
				}
				ActivityExportHelper.ExportCommonData(dialog.FileName, parts);

				if (File.Exists(dialog.FileName))
					Process.Start(dialog.FileName);
			}
		}

		private void ApplyData()
		{
			xtraTabControlGroups.TabPages.Clear();

			var filteredRecords = new List<AccessReportRecord>();
			filteredRecords.AddRange(_groupFilterControl.EnableFilter ? _records.Where(x => _groupFilterControl.SelectedGroups.Contains(x.name)) : _records);
			var allActive = filteredRecords.Sum(x => x.activeCount);
			var allInactive = filteredRecords.Sum(x => x.inactiveCount);
			var allUsers = filteredRecords.Sum(x => x.userCount);
			var groups = string.Join(", ", _groupFilterControl.SelectedGroups);
			foreach (var record in filteredRecords)
			{
				record.Groups = groups;
				record.AllActive = allActive;
				record.AllInactive = allInactive;
				record.AllUsers = allUsers;
			}
			var totalPage = new TotalControl(filteredRecords, StartDate, EndDate);
			_totalFilterControl.ColumnsChanged += (o, e) => totalPage.ApplyColumns(_totalFilterControl);
			xtraTabControlGroups.TabPages.Add(totalPage);


			foreach (var group in filteredRecords.OrderBy(r => r.name).Select(r => r.name).Distinct())
			{
				var groupPage = new GroupControl(filteredRecords.Where(r => r.name == group), StartDate, EndDate) { GroupName = group };
				_groupFilterControl.ColumnsChanged += (o, e) => groupPage.ApplyColumns(_groupFilterControl);
				xtraTabControlGroups.TabPages.Add(groupPage);
			}
			xtraTabControlGroups_SelectedPageChanged(xtraTabControlGroups, new TabPageChangedEventArgs(null, xtraTabControlGroups.SelectedTabPage));
		}

		private void xtraTabControlGroups_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.Page is TotalControl)
				_totalFilterControl.BringToFront();
			else
				_groupFilterControl.BringToFront();
		}
	}
}