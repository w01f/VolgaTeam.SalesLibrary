using System;
using System.ComponentModel;
using System.Drawing;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using FileManager.ConfigurationClasses;

namespace FileManager.ToolForms.WallBin
{
	public partial class FormSelectWidget : MetroForm
	{
		public FormSelectWidget()
		{
			InitializeComponent();
			gridControlWidgets.DataSource = new BindingList<Widget>(ListManager.Instance.Widgets);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laAvailableWidgets.Font = new Font(laAvailableWidgets.Font.FontFamily, laAvailableWidgets.Font.Size - 2, laAvailableWidgets.Font.Style);
				laSelectedWidget.Font = new Font(laSelectedWidget.Font.FontFamily, laSelectedWidget.Font.Size - 2, laSelectedWidget.Font.Style);
			}
		}

		private void layoutViewWidgets_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			Widget selectedWidget = null;
			if (layoutViewWidgets.FocusedRowHandle >= 0)
			{
				selectedWidget = ListManager.Instance.Widgets[layoutViewWidgets.GetDataSourceRowIndex(layoutViewWidgets.FocusedRowHandle)];
			}
			pbSelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
		}

		private void layoutViewWidgets_Click(object sender, EventArgs e)
		{
			Point pt = gridControlWidgets.PointToClient(MousePosition);

			if (layoutViewWidgets.CalcHitInfo(pt).RowHandle == layoutViewWidgets.FocusedRowHandle)
				layoutViewWidgets_FocusedRowChanged(null, null);
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControlWidgets)
				return;
			ToolTipControlInfo info = null;
			try
			{
				var view = gridControlWidgets.GetViewAt(e.ControlMousePosition) as LayoutView;
				if (view == null)
					return;
				LayoutViewHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
				if (hi.InFieldValue)
					info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ListManager.Instance.Widgets[layoutViewWidgets.GetDataSourceRowIndex(hi.RowHandle)].FileName);
			}
			finally
			{
				e.Info = info;
			}
		}
	}
}