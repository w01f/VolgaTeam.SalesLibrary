using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FileManager.ToolForms
{
    public partial class FormSelectWidget : Form
    {
        public FormSelectWidget()
        {
            InitializeComponent();
            gridControlWidgets.DataSource = new BindingList<ConfigurationClasses.Widget>(ConfigurationClasses.ListManager.Instance.Widgets);
        }

        private void layoutViewWidgets_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Widget selectedWidget = null;
            if (layoutViewWidgets.FocusedRowHandle >= 0)
            {
                selectedWidget = ConfigurationClasses.ListManager.Instance.Widgets[layoutViewWidgets.GetDataSourceRowIndex(layoutViewWidgets.FocusedRowHandle)];
            }
            pbSelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
        }

        private void layoutViewWidgets_Click(object sender, EventArgs e)
        {
            Point pt = gridControlWidgets.PointToClient(Control.MousePosition);

            if (layoutViewWidgets.CalcHitInfo(pt).RowHandle == layoutViewWidgets.FocusedRowHandle)
                layoutViewWidgets_FocusedRowChanged(null, null);
        }

        private void toolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControlWidgets)
                return;
            DevExpress.Utils.ToolTipControlInfo info = null;
            try
            {
                DevExpress.XtraGrid.Views.Layout.LayoutView view = gridControlWidgets.GetViewAt(e.ControlMousePosition) as DevExpress.XtraGrid.Views.Layout.LayoutView;
                if (view == null)
                    return;
                DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.InFieldValue)
                    info = new DevExpress.Utils.ToolTipControlInfo(new DevExpress.XtraGrid.Views.Base.CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ConfigurationClasses.ListManager.Instance.Widgets[layoutViewWidgets.GetDataSourceRowIndex(hi.RowHandle)].FileName);
            }
            finally
            {
                e.Info = info;
            }
        }
    }
}
