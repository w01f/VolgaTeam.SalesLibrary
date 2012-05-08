using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabOvernightsCalendarControl : UserControl
    {
        public TabOvernightsCalendarControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void buttonItemCalendarSyncStatus_Click(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemCalendarSyncStatusDisabled.Checked = false;
            FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;

        }

        public void buttonItemCalendarSyncStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator != null && PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.AllowToSave)
            {
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled = FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked;
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged = true;
            }
        }

        public void buttonEditCalendarLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator != null && PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.AllowToSave)
            {
                string path = FormMain.Instance.buttonEditCalendarLocation.EditValue != null ? FormMain.Instance.buttonEditCalendarLocation.EditValue.ToString() : string.Empty;
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                    PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.RootFolder = new DirectoryInfo(path);
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged = true;
            }
        }

        public void buttonEditCalendarLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.RootFolder.FullName;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        FormMain.Instance.buttonEditCalendarLocation.EditValue = dialog.SelectedPath;
                }
            }
        }
    }
}
