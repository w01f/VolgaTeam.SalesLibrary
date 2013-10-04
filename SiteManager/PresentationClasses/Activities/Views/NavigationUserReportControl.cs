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
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.PresentationClasses.Activities.Filters;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.Views
{
	[ToolboxItem(false)]
	public partial class NavigationUserReportControl : UserControl, IActivitiesView
	{
		private readonly List<NavigationUserReportRecord> _records = new List<NavigationUserReportRecord>();
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

		private NavigationUserFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public NavigationUserReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new NavigationUserFilter();
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetNavigationUserReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetNavigationUserReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.OrderBy(x => x.group).Select(x => x.group).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
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
				dialog.FileName = string.Format("UserNavigation({0}).xls", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xls";
				dialog.Title = "Export User Navigation Analysis";
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
			var filteredRecords = new List<NavigationUserReportRecord>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => _filterControl.SelectedGroups.Contains(x.group)) : _records);
			gridControlData.DataSource = filteredRecords;
		}

		private void ApplyColumns()
		{
			gridColumnUserLibrariesNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserLibrariesPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLibrariesNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLibrariesPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupLibrariesNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnGroupLibrariesNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnUserPagesNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserPagesPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserPagesNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserPagesPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupPagesNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnGroupPagesNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;

			gridColumnUserTotalNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserTotalPercent.Visible = _filterControl.ShowPercent;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupTotalNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
			gridColumnGroupTotalNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent ? 2 : 1;
		}

		private void printableComponentLink_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("User Navigation Analysis: {0} - {1}{2}Selected Groups: {3}", StartDate.ToString("MM/dd/yy"), EndDate.AddDays(-1).ToString("MM/dd/yy"), Environment.NewLine, _filterControl.SelectedGroups.Count != _filterControl.AllGroups.Count ? string.Join(", ", _filterControl.SelectedGroups) : "All");
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}