﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.PresentationClasses.Common;
using SalesLibraries.SiteManager.ToolClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.QuizPassData
{
	[ToolboxItem(false)]
	public partial class ContainerControl : UserControl, IActivitiesView
	{
		private readonly List<QuizPassUserReportModel> _userRecords = new List<QuizPassUserReportModel>();
		private readonly List<QuizPassGroupReportModel> _groupRecords = new List<QuizPassGroupReportModel>();
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
		public IEnumerable<Control> FilterControls => new Control[] { _filterControl };

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
					var thread = new Thread(() =>
					{
						_userRecords.AddRange(WebSiteManager.Instance.SelectedSite.GetQuizPassUserReport(StartDate, EndDate, out message));
						_groupRecords.AddRange(WebSiteManager.Instance.SelectedSite.GetQuizPassGroupReport(StartDate, EndDate, out message));
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
					AppManager.Instance.PopupMessages.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() =>
				{
					_userRecords.AddRange(WebSiteManager.Instance.SelectedSite.GetQuizPassUserReport(StartDate, EndDate, out message));
					_groupRecords.AddRange(WebSiteManager.Instance.SelectedSite.GetQuizPassGroupReport(StartDate, EndDate, out message));
				});
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_userRecords.OrderBy(g => g.group).Select(x => x.group).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray(), _userRecords.Select(r => r.topLevelName).Distinct());
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
				dialog.FileName = string.Format("UserQuizPerformanceReport({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "User Quiz Performance Report";
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
							Invoke(new Action(() =>
							{
								using (var printingSystem = new PrintingSystem())
								{
									groupControl.GetPrintLink().CreateDocument(printingSystem);
									Application.DoEvents();
									printingSystem.ExportToXlsx(tempFile, options);
									Application.DoEvents();
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

			var filteredGroupModels = new List<QuizPassGroupReportModel>();
			filteredGroupModels.AddRange(_filterControl.EnableFilter ?
				_groupRecords.Where(g => _filterControl.SelectedGroups.Contains(g.group) && (g.topLevelName == _filterControl.TopLevelQuizGroup || String.IsNullOrEmpty(_filterControl.TopLevelQuizGroup))) :
				_groupRecords);
			var totalPage = new TotalControl(filteredGroupModels, StartDate, EndDate);
			xtraTabControlGroups.TabPages.Add(totalPage);

			var filteredUserModels = new List<QuizPassUserReportModel>();
			filteredUserModels.AddRange(_filterControl.EnableFilter ?
				_userRecords.Where(g => _filterControl.SelectedGroups.Contains(g.group) && (g.topLevelName == _filterControl.TopLevelQuizGroup || String.IsNullOrEmpty(_filterControl.TopLevelQuizGroup))) :
				_userRecords);
			foreach (var group in filteredUserModels.OrderBy(r => r.group).Select(r => r.group).Distinct())
			{
				var groupPage = new GroupControl(filteredUserModels.Where(r => r.group == group), StartDate, EndDate) { GroupName = group };
				xtraTabControlGroups.TabPages.Add(groupPage);
			}
		}
	}
}