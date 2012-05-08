using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.SettingsForms
{
    public partial class FormAutoWidgets : Form
    {
        public BusinessClasses.Library Library { get; set; }

        public FormAutoWidgets()
        {
            InitializeComponent();
        }

        private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.Library.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            gridControlAutoWidgets.DataSource = new BindingList<BusinessClasses.AutoWidget>(this.Library.AutoWidgets);
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewAutoWidgets.FocusedRowHandle >= 0)
            {
                if (e.Button.Index == 0)
                {
                    this.Library.AutoWidgets.RemoveAt(gridViewAutoWidgets.GetDataSourceRowIndex(gridViewAutoWidgets.FocusedRowHandle));
                    (gridControlAutoWidgets.DataSource as BindingList<BusinessClasses.AutoWidget>).ResetBindings();
                }
                else if (e.Button.Index == 1)
                {
                    using (ToolForms.FormSelectWidget form = new ToolForms.FormSelectWidget())
                    {
                        BusinessClasses.AutoWidget autoWidget = this.Library.AutoWidgets[gridViewAutoWidgets.GetDataSourceRowIndex(gridViewAutoWidgets.FocusedRowHandle)];
                        form.pbSelectedWidget.Image = autoWidget.Widget;
                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            autoWidget.Widget = form.pbSelectedWidget.Image;
                            (gridControlAutoWidgets.DataSource as BindingList<BusinessClasses.AutoWidget>).ResetBindings();
                        }
                    }
                }
            }
        }

        private void buttonEditNewExtension_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonEditNewExtension_ButtonClick(null, null);
        }

        private void buttonEditNewExtension_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (buttonEditNewExtension.EditValue != null)
            {
                if (this.Library.AutoWidgets.Where(x => x.Extension.ToLower().Equals(buttonEditNewExtension.EditValue.ToString().ToLower())).Count() == 0)
                {
                    BusinessClasses.AutoWidget autoWidget = new BusinessClasses.AutoWidget();
                    autoWidget.Extension = buttonEditNewExtension.EditValue.ToString().ToLower();
                    this.Library.AutoWidgets.Add(autoWidget);
                    (gridControlAutoWidgets.DataSource as BindingList<BusinessClasses.AutoWidget>).ResetBindings();
                    buttonEditNewExtension.EditValue = null;
                    gridViewAutoWidgets.Focus();
                }
                else
                    AppManager.Instance.ShowWarning("Auto Widter is existed for this extension already");
            }
        }
    }
}
