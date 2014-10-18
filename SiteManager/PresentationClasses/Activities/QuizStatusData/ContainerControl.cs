using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraPrinting;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.ToolClasses;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.QuizStatusData
{
	[ToolboxItem(false)]
	public sealed partial class ContainerControl : UserControl, IActivitiesView
	{
		private readonly List<QuizPassUserReportModel> _records = new List<QuizPassUserReportModel>();
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

		private readonly Filter _filterControl;
		public IEnumerable<Control> FilterControls
		{
			get { return new[] { _filterControl }; }
		}

		public ContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new Filter();
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
			_filterControl.UpdateDataSource(_records.OrderBy(g => g.GroupName).Select(x => x.GroupName).Where(x => !String.IsNullOrEmpty(x)).Distinct().ToArray(), _records.Select(r => r.topLevelName).Distinct());
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
				dialog.FileName = string.Format("UserSalesCertificationStatus({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "User Sales Certification Status";
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
						groupControl.GetPrintLink().CreateDocument(printingSystem);
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
			var filteredRecords = new List<QuizPassUserReportModel>();
			var groupedRecords = _records.GroupBy(r => new { r.FullName, r.GroupName, r.topLevelName }).Select(g => new QuizPassUserReportModel
			{
				FullName = g.Key.FullName,
				GroupName = g.Key.GroupName,
				topLevelName = g.Key.topLevelName,
				QuizzesPassed = String.Join(Environment.NewLine, g.OrderBy(x => x.Date).Select(x => x.quizName)),
				TotalPassed = String.Format("(Tests Passed: {0})", g.Count())
			});
			filteredRecords.AddRange(_filterControl.EnableFilter ?
				groupedRecords.Where(g => _filterControl.SelectedGroups.Contains(g.GroupName) && (g.topLevelName == _filterControl.TopLevelQuizGroup || String.IsNullOrEmpty(_filterControl.TopLevelQuizGroup))) :
				groupedRecords);

			var totalPage = new GroupControl(filteredRecords, StartDate, EndDate) { GroupName = "Total Summary" };
			totalPage.gridColumnGroup.SortOrder = ColumnSortOrder.Ascending;
			xtraTabControlGroups.TabPages.Add(totalPage);

			foreach (var group in filteredRecords.OrderBy(r => r.GroupName).Select(g => g.GroupName).Distinct())
			{
				var groupPage = new GroupControl(filteredRecords.Where(r => r.GroupName == group), StartDate, EndDate) { GroupName = group };
				groupPage.gridColumnUser.SortOrder = ColumnSortOrder.Ascending;
				xtraTabControlGroups.TabPages.Add(groupPage);
			}
		}
	}
}