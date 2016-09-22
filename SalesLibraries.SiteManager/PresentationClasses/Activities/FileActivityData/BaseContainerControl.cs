using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.PresentationClasses.Common;
using SalesLibraries.SiteManager.ToolClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.FileActivityData
{
	[ToolboxItem(false)]
	public abstract partial class BaseContainerControl : UserControl, IActivitiesView
	{
		private readonly List<FileActivityReportModel> _records = new List<FileActivityReportModel>();
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

		protected abstract bool ShowLibrary { get; }

		private readonly Filter _filterControl;
		public IEnumerable<Control> FilterControls => new[] { _filterControl };

		public BaseContainerControl()
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
					var thread = new Thread(() => _records.AddRange(GetFileActivityReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(GetFileActivityReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.OrderBy(g => g.group).Select(x => x.group).Where(x => !String.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public abstract IEnumerable<FileActivityReportModel> GetFileActivityReport(DateTime startDate, DateTime endDate, out string message);

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
				dialog.FileName = string.Format("FileAccessSummary({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "File Access Summary";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				var options = new XlsxExportOptions();
				options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
				options.TextExportMode = TextExportMode.Text;
				options.ExportHyperlinks = true;
				options.ShowGridLines = true;
				options.ExportMode = XlsxExportMode.SingleFile;

				var groupControls = xtraTabControlGroups.TabPages.OfType<IGroupControl>().Reverse();
				var parts = new Dictionary<string, string>();

				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Exporting data...";
					form.TopMost = true;
					form.Show();
					Application.DoEvents();
					var thread = new Thread(() =>
					{
						foreach (var groupControl in groupControls)
						{
							var tempFile = Path.Combine(Path.GetTempPath(), String.Format("{0}.xlsx", Guid.NewGuid()));
							BeginInvoke(new Action(() =>
							{
								using (var printingSystem = new PrintingSystem())
								{
									groupControl.GetPrintLink().CreateDocument(printingSystem);
									printingSystem.ExportToXlsx(tempFile, options);
								}
							}));
							parts.Add(groupControl.GroupName, tempFile);
						}
						ActivityExportHelper.ExportCommonData(dialog.FileName, parts);
					});
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
			var filteredRecords = new List<FileActivityReportModel>();
			filteredRecords.AddRange(_filterControl.EnableFilter ?
				_records.Where(g => _filterControl.SelectedGroups.Contains(g.group)) :
				_records);

			var totalRecords = _records.GroupBy(r => new { r.fileType, r.fileName, r.fileLink, r.fileDetail, r.fileExtension, r.library }).Select(g => new FileActivityReportModel
			{
				fileType = g.Key.fileType,
				fileName = g.Key.fileName,
				fileLink = g.Key.fileLink,
				fileDetail = g.Key.fileDetail,
				fileExtension = g.Key.fileExtension,
				library = g.Key.library,
				group = "Total Summary",
				activityCount = g.Sum(x => x.activityCount)
			});

			var totalPage = new GroupControl(totalRecords.OrderByDescending(r => r.activityCount).Take(_filterControl.RecordsCount > 0 ? _filterControl.RecordsCount : 999999999).ToList(), StartDate, EndDate, ShowLibrary) { GroupName = "Total Summary" };
			xtraTabControlGroups.TabPages.Add(totalPage);

			foreach (var group in filteredRecords.OrderBy(r => r.group).Select(g => g.group).Distinct())
			{
				var groupPage = new GroupControl(filteredRecords.Where(r => r.group == group).OrderByDescending(r => r.activityCount).Take(_filterControl.RecordsCount > 0 ? _filterControl.RecordsCount : 999999999).ToList().ToList(), StartDate, EndDate, ShowLibrary) { GroupName = group };
				xtraTabControlGroups.TabPages.Add(groupPage);
			}
		}
	}
}