using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileManager.ConfigurationClasses
{
    public partial class FormApplicationSettings : Form
    {
        public FormApplicationSettings()
        {
            InitializeComponent();
        }

        private void btBackup_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = tbBackup.Text;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        tbBackup.Text = dialog.SelectedPath;
                }
            }
        }

        private void btNetwork_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = tbNetwork.Text;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        tbNetwork.Text = dialog.SelectedPath;
                }
            }
        }

        private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
                if (!System.IO.Directory.Exists(tbBackup.Text))
                {
                    AppManager.Instance.ShowWarning("Primary Back Root is Incorrect");
                    return;
                }
                if (!System.IO.Directory.Exists(tbNetwork.Text))
                {
                    AppManager.Instance.ShowWarning("Network Sync Folder is Incorrect");
                    return;
                }
                e.Cancel = false;
                AppManager.Instance.Settings.paths[3] = tbBackup.Text;
                AppManager.Instance.Settings.paths[4] = tbNetwork.Text;
                AppManager.Instance.Settings.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            tbBackup.Text = AppManager.Instance.Settings.paths[3];
            tbNetwork.Text = AppManager.Instance.Settings.paths[4];
        }
    }
}
