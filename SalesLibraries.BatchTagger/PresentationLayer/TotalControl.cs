using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl
	public partial class TotalControl : XtraTabPage
	{
		public List<LibraryFilesTotalModel> Records { get; set; }
		public string GroupName => Text;
		public event EventHandler<LibraryOpenEventArgs> LibraryOpen;

		public TotalControl(IEnumerable<LibraryFilesTotalModel> records)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<LibraryFilesTotalModel>();
			Text = "Total Summary";

			ApplyData(records);

			RestoreLayout();
		}

		public void ApplyData(IEnumerable<LibraryFilesTotalModel> records)
		{
			Records.Clear();
			Records.AddRange(records);
			gridControlData.DataSource = Records;
			gridView.RefreshData();
			gridControlData.Update();
		}

		public void SaveLayout()
		{
			gridView.SaveLayoutToXml(AppManager.Instance.Resources.TotalControlLayoutConfigPath);
		}

		private void RestoreLayout()
		{
			if (File.Exists(AppManager.Instance.Resources.TotalControlLayoutConfigPath))
				gridView.RestoreLayoutFromXml(AppManager.Instance.Resources.TotalControlLayoutConfigPath);
		}

		private void OnCustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			var selectedRows = gridView.GetSelectedRows();
			if (selectedRows.Contains(e.RowHandle)) return;
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}

		private void OnRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks < 2) return;
			var libraryRecord = gridView.GetRow(e.RowHandle) as LibraryFilesTotalModel;
			if (libraryRecord == null) return;
			LibraryOpen?.Invoke(this, new LibraryOpenEventArgs(libraryRecord.Name));
		}

		private void OnRowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var libraryRecord = gridView.GetRow(e.RowHandle) as LibraryFilesTotalModel;
			if (libraryRecord == null) return;

			if (e.Column == gridColumnName)
			{
				if (libraryRecord.FilesTotalCount > libraryRecord.FilesTaggedCount ||
					libraryRecord.VideoTotalCount > libraryRecord.VideoTaggedCount)
					e.Appearance.ForeColor = Color.Red;
			}

			if (e.Column == gridColumnTaggedFilesCount)
			{
				if (libraryRecord.FilesTotalCount > libraryRecord.FilesTaggedCount)
					e.Appearance.ForeColor = Color.Red;
			}

			if (e.Column == gridColumnTaggedVideoCount)
			{
				if (libraryRecord.VideoTotalCount > libraryRecord.VideoTaggedCount)
					e.Appearance.ForeColor = Color.Red;
			}
		}
	}
}