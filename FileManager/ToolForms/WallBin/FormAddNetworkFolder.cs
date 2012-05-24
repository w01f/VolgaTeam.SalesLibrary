using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager.ToolForms.WallBin
{
    public partial class FormAddNetworkFolder : Form
    {
        public FormAddNetworkFolder()
        {
            InitializeComponent();
        }

        public string LinkName
        {
            get
            {
                return edLinkName.Text;
            }
        }

        public string LinkPath
        {
            get
            {
                if (buttonEditFolderPath.EditValue != null)
                    return buttonEditFolderPath.EditValue.ToString();
                else
                    return string.Empty;
            }
        }

        private void AddLinkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(edLinkName.Text))
                {
                    AppManager.Instance.ShowWarning("You should set the link name before saving");
                    e.Cancel = true;
                    return;
                }
                if (buttonEditFolderPath.EditValue == null || string.IsNullOrEmpty(buttonEditFolderPath.EditValue.ToString()))
                {
                    AppManager.Instance.ShowWarning("You should set the link path before saving");
                    e.Cancel = true;
                    return;
                }
                else if (!Directory.Exists(buttonEditFolderPath.EditValue.ToString()))
                {
                    AppManager.Instance.ShowWarning("Link path is not correct");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void buttonEditFolderPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                buttonEditFolderPath.EditValue = folderBrowserDialog.SelectedPath;

        }
    }
}
