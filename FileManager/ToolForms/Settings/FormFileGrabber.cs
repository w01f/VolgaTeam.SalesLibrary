using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormFileGrabber : Form
    {
        public FormFileGrabber()
        {
            InitializeComponent();
            spinEditGrabInterval.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditGrabInterval.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditGrabInterval.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laSourceFolder.Font = new System.Drawing.Font(laSourceFolder.Font.FontFamily, laSourceFolder.Font.Size - 2, laSourceFolder.Font.Style);
            }
        }

        private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber = buttonXEnable.Checked;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabInterval = (int)spinEditGrabInterval.Value;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabSourceFolder = buttonEditSouceFolder.EditValue != null ? buttonEditSouceFolder.EditValue.ToString() : string.Empty;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            buttonXEnable.Checked = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber;
            buttonXDisable.Checked = !PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber;
            spinEditGrabInterval.Value = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabInterval;
            buttonEditSouceFolder.EditValue = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabSourceFolder;
        }


        private void buttonXEnable_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && !button.Checked)
            {
                buttonXEnable.Checked = false;
                buttonXDisable.Checked = false;
                button.Checked = true;
            }
        }

        private void buttonXEnable_CheckedChanged(object sender, EventArgs e)
        {
            laGrabInterval.Enabled = buttonXEnable.Checked;
            spinEditGrabInterval.Enabled = buttonXEnable.Checked;

            laSourceFolder.Enabled = buttonXEnable.Checked;
            buttonEditSouceFolder.Enabled = buttonXEnable.Checked;
        }

        private void buttonEditSouceFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (buttonEditSouceFolder.EditValue != null)
                    dialog.SelectedPath = buttonEditSouceFolder.EditValue.ToString();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        buttonEditSouceFolder.EditValue = dialog.SelectedPath;
                }
            }
        }
    }
}
