using System;
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

namespace SalesDepot.SiteManager.PresentationClasses.Activities.NavigationData
{
	[ToolboxItem(false)]
	public partial class ContainerControl : UserControl, IActivitiesView
	{
		private readonly List<NavigationUserReportModel> _userRecords = new List<NavigationUserReportModel>();
		private readonly List<NavigationGroupReportModel> _groupRecords = new List<NavigationGroupReportModel>();
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
				_userFilterControl.Visible = _active;
			}
		}

		private readonly UserFilter _userFilterControl;
		private readonly GroupFilter _groupFilterControl;
		public IEnumerable<Control> FilterControls
		{
			get { return new Control[] { _userFilterControl, _groupFilterControl }; }
		}

		public ContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			_userFilterControl = new UserFilter();
			_userFilterControl.FilterChanged += (o, e) =>
			{
				_groupFilterControl.EnableFilter = _userFilterControl.EnableFilter;
				_groupFilterControl.SelectedGroups.Clear();
				_groupFilterControl.SelectedGroups.AddRange(_userFilterControl.SelectedGroups);
				_groupFilterControl.UpdateDataSource(_userFilterControl.AllGroups.ToArray(), false);
				ApplyData();
			};
			_userFilterControl.ColumnsChanged += (o, e) =>
			{
				_groupFilterControl.ShowNumber = _userFilterControl.ShowNumber;
				_groupFilterControl.ShowPercent = _userFilterControl.ShowPercent;
			};

			_groupFilterControl = new GroupFilter();
			_groupFilterControl.FilterChanged += (o, e) =>
			{
				_userFilterControl.EnableFilter = _groupFilterControl.EnableFilter;
				_userFilterControl.SelectedGroups.Clear();
				_userFilterControl.SelectedGroups.AddRange(_groupFilterControl.SelectedGroups);
				_userFilterControl.UpdateDataSource(_groupFilterControl.AllGroups.ToArray(), false);
				ApplyData();
			};
			_groupFilterControl.ColumnsChanged += (o, e) =>
			{
				_userFilterControl.ShowNumber = _groupFilterControl.ShowNumber;
				_userFilterControl.ShowPercent = _groupFilterControl.ShowPercent;

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
					var thread = new Thread(() =>
					{
						_userRecords.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetNavigationUserReport(StartDate, EndDate, out message));
						_groupRecords.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetNavigationGroupReport(StartDate, EndDate, out message));

					});
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
				var thread = new Thread(() =>
				{
					_userRecords.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetNavigationUserReport(StartDate, EndDate, out message));
					_groupRecords.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetNavigationGroupReport(StartDate, EndDate, out message));

				});
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_userFilterControl.UpdateDataSource(_userRecords.OrderBy(g => g.group).Select(x => x.group).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
			_groupFilterControl.UpdateDataSource(_groupRecords.OrderBy(g => g.name).Select(x => x.name).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			xtraTabControlGroups.TabPages.Clear();
			_userRecords.Clear();
			_groupRecords.Clear();
		}

		public void ExportData()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("UserNavigation({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "Export User Navigation Analysis";
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

			var filteredGroupModels = new List<NavigationGroupReportModel>();
			filteredGroupModels.AddRange(_groupFilterControl.EnableFilter ? _groupRecords.Where(x => _groupFilterControl.SelectedGroups.Contains(x.name)) : _groupRecords);
			var allLibraries = filteredGroupModels.Sum(x => x.libs);
			var allPages = filteredGroupModels.Sum(x => x.pages);
			var allTotals = filteredGroupModels.Sum(x => x.totals);
			foreach (var record in filteredGroupModels)
			{
				record.AllLibraries = allLibraries;
				record.AllPages = allPages;
				record.AllTotals = allTotals;
			}
			var totalPage = new TotalControl(filteredGroupModels, StartDate, EndDate);
			_groupFilterControl.ColumnsChanged += (o, e) => totalPage.ApplyColumns(_groupFilterControl);
			xtraTabControlGroups.TabPages.Add(totalPage);

			var filteredUserModels = new List<NavigationUserReportModel>();
			filteredUserModels.AddRange(_userFilterControl.EnableFilter ? _userRecords.Where(x => _userFilterControl.SelectedGroups.Contains(x.group)) : _userRecords);
			foreach (var group in filteredUserModels.OrderBy(r => r.group).Select(r => r.group).Distinct())
			{
				var groupPage = new GroupControl(filteredUserModels.Where(r => r.group == group), StartDate, EndDate) { GroupName = group };
				_userFilterControl.ColumnsChanged += (o, e) => groupPage.ApplyColumns(_userFilterControl);
				xtraTabControlGroups.TabPages.Add(groupPage);
			}
			xtraTabControlGroups_SelectedPageChanged(xtraTabControlGroups, new TabPageChangedEventArgs(null, xtraTabControlGroups.SelectedTabPage));
		}

		private void xtraTabControlGroups_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.Page is TotalControl)
				_groupFilterControl.BringToFront();
			else
				_userFilterControl.BringToFront();
		}
	}
}