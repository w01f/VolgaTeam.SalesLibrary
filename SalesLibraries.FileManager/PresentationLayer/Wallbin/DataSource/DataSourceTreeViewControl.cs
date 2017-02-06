using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	[ToolboxItem(false)]
	public sealed partial class DataSourceTreeViewControl : UserControl
	{
		public DataSourceTreeViewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if (!((CreateGraphics()).DpiX > 96)) return;
			laDoubleClick.Font = new Font(laDoubleClick.Font.FontFamily, laDoubleClick.Font.Size - 3, laDoubleClick.Font.Style);
			laEndDate.Font = new Font(laEndDate.Font.FontFamily, laEndDate.Font.Size - 2, laEndDate.Font.Style);
			laStartDate.Font = new Font(laStartDate.Font.FontFamily, laStartDate.Font.Size - 2, laStartDate.Font.Style);
			laTreeViewProgressLabel.Font = new Font(laTreeViewProgressLabel.Font.FontFamily, laTreeViewProgressLabel.Font.Size - 3, laTreeViewProgressLabel.Font.Style);
			checkEditDateRange.Font = new Font(checkEditDateRange.Font.FontFamily, checkEditDateRange.Font.Size - 2, checkEditDateRange.Font.Style);
			buttonXRefresh.Font = new Font(buttonXRefresh.Font.FontFamily, buttonXRefresh.Font.Size - 2, buttonXRefresh.Font.Style);
			buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
		}

		public void LoadData(IEnumerable<IDataSource> dataSources)
		{
			_dataSources.Clear();
			_dataSources.AddRange(dataSources.OrderBy(ds => ds.Order));
			OnDateRangeCheckedChanged(checkEditDateRange, EventArgs.Empty);
			RefreshRegularFiles();
			RefreshExternalFiles();
		}

		private void OnXtraTabControlFilesSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			switch (xtraTabControlFiles.SelectedTabPageIndex)
			{
				case 0:
					treeListSearchFiles.Selection.Clear();
					break;
				case 1:
					treeListRegularFiles.Selection.Clear();
					buttonXSearch.Refresh();
					break;
			}
		}

		private async void OnRefreshRegularFilesClick(object sender, EventArgs e)
		{
			laTreeViewProgressLabel.Text = "Loading Tree View...";
			pnTreeViewProgress.Visible = true;
			circularProgressTreeView.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			switch (xtraTabControlFiles.SelectedTabPageIndex)
			{
				case 0:
					await RefreshRegularFiles();
					break;
				case 1:
					await RefreshExternalFiles();
					break;
			}

			xtraTabControlFiles.Enabled = true;
			circularProgressTreeView.IsRunning = false;
			pnTreeViewProgress.Visible = false;
		}
	}
}