using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager.BusinessClasses
{
    public partial class FormAddUrl : Form
    {
        public FormAddUrl()
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
                if (textEditWebAddress.EditValue != null)
                    return textEditWebAddress.EditValue.ToString();
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
                if (textEditWebAddress.EditValue == null || string.IsNullOrEmpty(textEditWebAddress.EditValue.ToString()))
                {
                    AppManager.Instance.ShowWarning("You should set the link path before saving");
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
