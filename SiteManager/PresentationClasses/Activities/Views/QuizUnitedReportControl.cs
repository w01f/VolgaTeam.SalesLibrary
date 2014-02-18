using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.PresentationClasses.Activities.Filters;
using SalesDepot.SiteManager.ToolClasses;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.Views
{
	[ToolboxItem(false)]
	public partial class QuizUnitedReportControl : UserControl, IActivitiesView
	{
		private readonly List<QuizPassUserReportRecord> _records = new List<QuizPassUserReportRecord>();
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

		private readonly QuizUnitedFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public QuizUnitedReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new QuizUnitedFilter();
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetQuizPassUserReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetQuizPassUserReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.OrderBy(g => g.GroupName).Select(x => x.GroupName).Where(x => !String.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			xtraTabControlGroups.TabPages.Clear();
			_records.Clear();
		}

		public void ExportData()
		{
			if (!xtraTabControlGroups.TabPages.Any()) return;
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("UserSalesCertificationStatus({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "User Sales Certification Status";
				if (dialog.ShowDialog() != DialogResult.OK) return;

				var header = String.Format("Date Range: {0} - {1}{2}Active Quizzes in the System: {3}",
					StartDate.ToString("M/d/yy"),
					EndDate.ToString("M/d/yy"),
					Environment.NewLine,
					_records.Select(r => r.quizName).Distinct().Count());
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Exporting data...";
					form.TopMost = true;
					var thread = new Thread(() => QuizStatisticExportHelper.ExportQuizStatistic(dialog.FileName,
						header,
						xtraTabControlGroups.TabPages.OfType<QuizUnitedReportTotalControl>().First().Records,
						xtraTabControlGroups.TabPages.OfType<QuizUnitedReportGroupControl>().Select(tp => tp.Records)));
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
				if (File.Exists(dialog.FileName))
					Process.Start(dialog.FileName);
			}
		}

		private void ApplyData()
		{
			xtraTabControlGroups.TabPages.Clear();
			var filteredRecords = new List<QuizPassUserReportRecord>();
			filteredRecords.AddRange(_records.Where(record => record.GroupName != null && (!_filterControl.EnableFilter || _filterControl.SelectedGroups.Contains(record.GroupName))));
			var quizCount = _records.Select(r => r.quizName).Distinct().Count();
			var totalPage = new QuizUnitedReportTotalControl(
				filteredRecords.GroupBy(r => new { r.GroupName, r.quizName }).Select(g => new QuizPassGroupReportRecord
			{
				group = g.Key.GroupName,
				quizName = g.Key.quizName,
				Taken = g.Sum(r => r.quizTryCount),
				Passed = g.Count()
			}), quizCount) { Text = "Total Summary" };
			_filterControl.CollapsedAll += (o, e) => totalPage.CollapseAll();
			_filterControl.ExpandedAll += (o, e) => totalPage.ExpandAll();
			xtraTabControlGroups.TabPages.Add(totalPage);

			foreach (var group in filteredRecords.OrderBy(r => r.GroupName).Select(r => r.GroupName).Distinct())
			{
				var groupPage = new QuizUnitedReportGroupControl(filteredRecords.Where(r => r.GroupName.Equals(group)), quizCount) { Text = @group };
				_filterControl.CollapsedAll += (o, e) => groupPage.CollapseAll();
				_filterControl.ExpandedAll += (o, e) => groupPage.ExpandAll();
				xtraTabControlGroups.TabPages.Add(groupPage);
			}
		}

		private void ApplyColumns()
		{
		}
	}
}