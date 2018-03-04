using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;
using SalesLibraries.ServiceConnector.QBuilderService;
using SalesLibraries.SiteManager.ToolClasses;
using BorderSide = DevExpress.XtraPrinting.BorderSide;

namespace SalesLibraries.SiteManager.PresentationClasses.QBuilder
{
	[ToolboxItem(false)]
	public partial class QPagesManagerControl : UserControl
	{
		private readonly List<QPageModel> _records = new List<QPageModel>();
		private readonly QPagesFilter _filterControl;

		public QPagesManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			_filterControl = new QPagesFilter();
			_filterControl.FilterChanged += (o, e) => ApplyData();
			pnCustomFilter.Controls.Add(_filterControl);

			var now = DateTime.Now;
			dateEditStart.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
			now = now.AddDays(1);
			dateEditEnd.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

			if (CreateGraphics().DpiX > 96)
			{
				gridColumnPagesActions.Width =
					RectangleHelper.ScaleHorizontal(gridColumnPagesActions.Width, gridControlRecords.ScaleFactor.Width);
				gridColumnPagesDateCreate.Width =
					RectangleHelper.ScaleHorizontal(gridColumnPagesDateCreate.Width, gridControlRecords.ScaleFactor.Width);
				gridColumnPagesDateExpiration.Width =
					RectangleHelper.ScaleHorizontal(gridColumnPagesDateExpiration.Width, gridControlRecords.ScaleFactor.Width);
				gridColumnPagesSecurityType.Width =
					RectangleHelper.ScaleHorizontal(gridColumnPagesSecurityType.Width, gridControlRecords.ScaleFactor.Width);
				gridColumnPagesTitle.Width =
					RectangleHelper.ScaleHorizontal(gridColumnPagesTitle.Width, gridControlRecords.ScaleFactor.Width);
				gridColumnPagesType.Width =
					RectangleHelper.ScaleHorizontal(gridColumnPagesType.Width, gridControlRecords.ScaleFactor.Width);
				gridColumnPinCode.Width =
					RectangleHelper.ScaleHorizontal(gridColumnPinCode.Width, gridControlRecords.ScaleFactor.Width);
				gridColumnTotalViews.Width =
					RectangleHelper.ScaleHorizontal(gridColumnTotalViews.Width, gridControlRecords.ScaleFactor.Width);
			}
		}

		public void RefreshData(bool showMessages)
		{
			ClearData();
			var message = string.Empty;
			var fileterByViewDate = checkEditFilterByViewDate.Checked;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetAllPages(dateEditStart.DateTime, dateEditEnd.DateTime, fileterByViewDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetAllPages(dateEditStart.DateTime, dateEditEnd.DateTime, fileterByViewDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			_filterControl.UpdateDataSource(_records.SelectMany(x => x.GroupNameList).OrderBy(g => g).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			gridControlRecords.DataSource = null;
			_records.Clear();
		}

		private void ApplyData()
		{
			var filteredRecords = new List<QPageModel>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => x.GroupNameList.Any(y => _filterControl.SelectedGroups.Contains(y))) : _records);
			gridControlRecords.DataSource = filteredRecords;
		}

		public void DeletePage()
		{
			var pageRecord = gridViewRecords.GetFocusedRow() as QPageModel;
			if (pageRecord == null || AppManager.Instance.PopupMessages.ShowWarningQuestion(string.Format("Are you sure want to delete this {0}?", pageRecord.Type)) != DialogResult.Yes) return;
			string message = string.Empty;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = string.Format("Deleting {0}...", pageRecord.Type);
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.DeletePages(new[] { pageRecord.id }, out message));
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
			{
				AppManager.Instance.PopupMessages.ShowWarning(message);
				return;
			}
			RefreshData(false);
		}

		public void ExportData()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("quickSITES & Emails({0}).xlsx", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "Export quickSites & Emails";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				var options = new XlsxExportOptions();
				options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
				options.TextExportMode = TextExportMode.Text;
				options.ExportHyperlinks = true;
				options.ShowGridLines = true;
				options.ExportMode = XlsxExportMode.SingleFile;

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
						var tempFile = Path.Combine(Path.GetTempPath(), String.Format("{0}.xlsx", Guid.NewGuid()));
						Invoke(new Action(() =>
						{
							using (var printingSystem = new PrintingSystem())
							{
								var printLink = new PrintableComponentLink
								{
									Landscape = true,
									PaperKind = PaperKind.A4,
									Component = gridControlRecords
								};
								printLink.CreateReportHeaderArea += OnCreateReportHeaderArea;
								printLink.CreateDocument(printingSystem);
								Application.DoEvents();
								printingSystem.ExportToXlsx(tempFile, options);
								Application.DoEvents();
							}
						}));
						parts.Add("quickSITES & Emails", tempFile);
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

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("quickSITES & Emails: {0} - {1}", dateEditStart.DateTime.ToString("MM/dd/yy"), dateEditEnd.DateTime.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}

		private void buttonXLoadData_Click(object sender, EventArgs e)
		{
			RefreshData(true);
		}

		private void OnGridViewShownEditor(object sender, EventArgs e)
		{
			var view = (GridView)sender;
			view.ActiveEditor.MouseWheel -= OnActiveEditorMouseWheel;
			view.ActiveEditor.MouseWheel += OnActiveEditorMouseWheel;
		}

		private void OnActiveEditorMouseWheel(Object sender, MouseEventArgs e)
		{
			gridViewRecords.HideEditor();
			gridViewRecords.Focus();
		}

		private void OnGridViewMouseMove(object sender, MouseEventArgs e)
		{
			gridViewRecords.HideEditor();
			gridViewRecords.Focus();
		}

		private void repositoryItemHyperLinkEditPages_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewRecords.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
				{
					var pageRecord = gridViewRecords.GetFocusedRow() as QPageModel;
					if (pageRecord == null) return;
					Clipboard.SetText(pageRecord.url);
				}
			}
		}

		private void repositoryItemButtonEditPagesActions_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewRecords.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
				{
					DeletePage();
				}
			}
		}
	}
}