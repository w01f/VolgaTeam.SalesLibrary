using System;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
    public partial class FormEmailLink : Form
    {
        public BusinessClasses.LibraryFile SelectedFile { get; set; }

        public FormEmailLink()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                ckChangeEmailName.Font = new System.Drawing.Font(ckChangeEmailName.Font.FontFamily, ckChangeEmailName.Font.Size - 2, ckChangeEmailName.Font.Style);
                buttonXEmail.Font = new System.Drawing.Font(buttonXEmail.Font.FontFamily, buttonXEmail.Font.Size - 2, buttonXEmail.Font.Style);
                buttonXCancel.Font = new System.Drawing.Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
            }
        }

        private void ckChangeEmailName_CheckedChanged(object sender, EventArgs e)
        {
            textEditEmailName.Enabled = ckChangeEmailName.Checked;
        }

        private void FormEmailPresentation_Load(object sender, EventArgs e)
        {
            this.Text = string.Format(this.Text, this.SelectedFile.PropertiesName);
        }

        private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string selectedName = ckChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : this.SelectedFile.PropertiesName;
                string destinationFilePath = Path.Combine(Path.GetTempPath(), (selectedName + Path.GetExtension(this.SelectedFile.FullPath)));
                File.Copy(this.SelectedFile.FullPath, destinationFilePath, true);
                if (File.Exists(destinationFilePath))
                    BusinessClasses.LinkManager.EmailFile(destinationFilePath);
            }
        }
    }
}
