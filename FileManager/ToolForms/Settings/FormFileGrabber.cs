﻿using System;
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
        }

        private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber = buttonXEnable.Checked;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.FileGrabSourceFolder = buttonEditSouceFolder.EditValue != null ? buttonEditSouceFolder.EditValue.ToString() : string.Empty;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            buttonXEnable.Checked = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber;
            buttonXDisable.Checked = !PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableFileGrabber;
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
