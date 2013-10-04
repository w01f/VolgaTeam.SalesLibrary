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
	public partial class MainUserReportControl : UserControl, IActivitiesView
	{
		private readonly List<MainUserReportRecord> _records = new List<MainUserReportRecord>();
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

		private MainUserFilter _filterControl;
		public Control FilterControl
		{
			get { return _filterControl; }
		}

		public MainUserReportControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new MainUserFilter();
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
					var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetMainUserReport(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetMainUserReport(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.OrderBy(g => g.group).Select(x => x.group).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray());
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
				dialog.FileName = string.Format("UserActivity({0}).xls", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xls";
				dialog.Title = "Export General User Activity Report";
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
			var filteredRecords = new List<MainUserReportRecord>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => _filterControl.SelectedGroups.Contains(x.group)) : _records);
			gridControlData.DataSource = filteredRecords;
		}

		private void ApplyColumns()
		{
			gridColumnName.Visible = _filterControl.ShowUsers;
			gridColumnGroup.Visible = _filterControl.ShowGroups;
			if (_filterControl.ShowUsers)
			{
				gridColumnName.RowCount = !_filterControl.ShowGroups && _filterControl.ShowNumber && _filterControl.ShowPercent ? 2 : 1;
			}
			if (_filterControl.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroup, 1, _filterControl.ShowUsers ? 2 : 1);
			}

			gridColumnUserLoginNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserLoginPercent.Visible = _filterControl.ShowPercent;
			gridColumnGroupLoginNumber.Visible = _filterControl.ShowGroups;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLoginNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLoginPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			if (_filterControl.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupLoginNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
				gridColumnGroupLoginNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent && _filterControl.ShowUsers ? 2 : 1;
			}

			gridColumnUserDocsNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserDocsPercent.Visible = _filterControl.ShowPercent;
			gridColumnGroupDocsNumber.Visible = _filterControl.ShowGroups;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserDocsNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserDocsPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			if (_filterControl.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupDocsNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
				gridColumnGroupDocsNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent && _filterControl.ShowUsers ? 2 : 1;
			}

			gridColumnUserVideosNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserVideosPercent.Visible = _filterControl.ShowPercent;
			gridColumnGroupVideosNumber.Visible = _filterControl.ShowGroups;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserVideosNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserVideosPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			if (_filterControl.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupVideosNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
				gridColumnGroupVideosNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent && _filterControl.ShowUsers ? 2 : 1;
			}

			gridColumnUserTotalNumber.Visible = _filterControl.ShowNumber;
			gridColumnUserTotalPercent.Visible = _filterControl.ShowPercent;
			gridColumnGroupTotalNumber.Visible = _filterControl.ShowGroups;
			if (_filterControl.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalNumber, 0, 0);
			if (_filterControl.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalPercent, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1, 0);
			if (_filterControl.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupTotalNumber, 1, _filterControl.ShowPercent && !_filterControl.ShowNumber ? 0 : 1);
				gridColumnGroupTotalNumber.RowCount = !_filterControl.ShowNumber && !_filterControl.ShowPercent && _filterControl.ShowUsers ? 2 : 1;
			}
		}

		private void printableComponentLink_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("General User Activity Report: {0} - {1}{2}Selected Groups: {3}", StartDate.ToString("MM/dd/yy"), EndDate.AddDays(-1).ToString("MM/dd/yy"), Environment.NewLine, _filterControl.SelectedGroups.Count != _filterControl.AllGroups.Count ? string.Join(", ", _filterControl.SelectedGroups) : "All");
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}