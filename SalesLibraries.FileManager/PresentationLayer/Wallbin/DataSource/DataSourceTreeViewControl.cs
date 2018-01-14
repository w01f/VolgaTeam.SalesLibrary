using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	[ToolboxItem(false)]
	public sealed partial class DataSourceTreeViewControl : UserControl
	{
		public DataSourceTreeViewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			layoutControlItemRefresh.MaxSize = new Size(
				0,
				RectangleHelper.ScaleVertical(layoutControlItemRefresh.MaxSize.Height, treeListRegularFiles.ScaleFactor.Height));
		}

		public void LoadData(IEnumerable<IDataSource> dataSources)
		{
			_dataSources.Clear();
			_dataSources.AddRange(dataSources.OrderBy(ds => ds.Order));
			OnDateRangeCheckedChanged(checkEditDateRange, EventArgs.Empty);
			RefreshRegularFiles();
			RefreshExternalFiles();
		}

		private void OnSelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
		{
			switch (tabbedControlGroupFiles.SelectedTabPageIndex)
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
			labelControlProgress.Text = "Loading Tree View...";
			layoutControlGroupProgress.Visibility = LayoutVisibility.Always;
			circularProgressTreeView.IsRunning = true;
			layoutControlGroupFiles.Enabled = false;

			switch (tabbedControlGroupFiles.SelectedTabPageIndex)
			{
				case 0:
					await RefreshRegularFiles();
					break;
				case 1:
					await RefreshExternalFiles();
					break;
			}

			layoutControlGroupFiles.Enabled = true;
			circularProgressTreeView.IsRunning = false;
			layoutControlGroupProgress.Visibility = LayoutVisibility.Never;
		}

		private void OnOutsideClick(object sender, EventArgs e)
		{
			MainController.Instance.WallbinViews.Selection.ResetAll();
		}
	}
}