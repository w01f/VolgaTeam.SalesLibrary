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

namespace SalesLibraries.SiteManager.PresentationClasses.LibraryFiles
{
	[ToolboxItem(false)]
	public partial class LibraryFilesManagerControl : UserControl
	{
		private readonly List<LibraryFilesModel> _records = new List<LibraryFilesModel>();
		private readonly Filter _filterControl;

		public LibraryFilesManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new Filter();
			pnCustomFilter.Controls.Add(_filterControl);
			_filterControl.FilterChanged += (o, e) =>
			{
				ApplyData();
			};
		}

		public void RefreshData(bool showMessages)
		{
			ClearData();
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetLibraryFiles(out message)));
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
				var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetLibraryFiles(out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			_filterControl.UpdateDataSource(_records.OrderBy(g => g.library).Select(x => x.library).Where(x => !String.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			xtraTabControlLibraries.TabPages.Clear();
			_records.Clear();
		}

		public void ExportData()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("ActiveLibraries({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "Export Active Libraries";
				if (dialog.ShowDialog() != DialogResult.OK) return;

				var options = new XlsxExportOptions();
				options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
				options.TextExportMode = TextExportMode.Text;
				options.ExportHyperlinks = true;
				options.ShowGridLines = true;
				options.ExportMode = XlsxExportMode.SingleFile;
				var groupControls = xtraTabControlLibraries.TabPages.OfType<IGroupControl>().Reverse();
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
			xtraTabControlLibraries.TabPages.Clear();
			var filteredRecords = new List<LibraryFilesModel>();
			filteredRecords.AddRange(_filterControl.EnableFilter ?
				_records.Where(g => _filterControl.SelectedGroups.Contains(g.library)) :
				_records);

			var totalRecords = filteredRecords
				.GroupBy(g=>g.library)
				.Select(g=> new LibraryFilesTotalModel()
				{
					Name = g.Key,
					FilesCount = g.Count(),
					VideoCount = g.Count(r => "video".Equals(r.fileFormat,StringComparison.OrdinalIgnoreCase)),
					LibraryDate = g.Select(r=>r.LibraryDate).FirstOrDefault()
				})
				.OrderBy(r => r.Name).ToList();
			var totalPage = new TotalControl(totalRecords); 
			xtraTabControlLibraries.TabPages.Add(totalPage);

			foreach (var group in filteredRecords.OrderBy(r => r.library).Select(g => g.library).Distinct())
			{
				var groupPage = new LibraryControl(filteredRecords.Where(r => r.library == group).OrderBy(r => r.linkName).ToList()) { GroupName = group };
				xtraTabControlLibraries.TabPages.Add(groupPage);
			}
		}

		private void buttonXLoadData_Click(object sender, EventArgs e)
		{
			RefreshData(true);
		}
	}
}