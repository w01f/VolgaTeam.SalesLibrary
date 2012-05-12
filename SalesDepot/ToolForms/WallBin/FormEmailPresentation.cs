using System;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
    public partial class FormEmailPresentation : Form
    {
        public int ActiveSlide { get; set; }
        public BusinessClasses.LibraryFile SelectedFile { get; set; }

        public FormEmailPresentation()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                ckConvertToPDF.Font = new System.Drawing.Font(ckConvertToPDF.Font.FontFamily, ckConvertToPDF.Font.Size - 2, ckConvertToPDF.Font.Style);
                ckChangeEmailName.Font = new System.Drawing.Font(ckChangeEmailName.Font.FontFamily, ckChangeEmailName.Font.Size - 2, ckChangeEmailName.Font.Style);
                rbActiveSlide.Font = new System.Drawing.Font(rbActiveSlide.Font.FontFamily, rbActiveSlide.Font.Size - 2, rbActiveSlide.Font.Style);
                rbAllSlides.Font = new System.Drawing.Font(rbAllSlides.Font.FontFamily, rbAllSlides.Font.Size - 2, rbAllSlides.Font.Style);
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
            ckConvertToPDF.Enabled = ConfigurationClasses.SettingsManager.Instance.EnablePdfConverting;
            ckConvertToPDF.Checked = ConfigurationClasses.SettingsManager.Instance.EnablePdfConverting & ConfigurationClasses.SettingsManager.Instance.EmailBinSendAsPdf;
        }

        private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string selectedName = ckChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : this.SelectedFile.NameWithoutExtesion;
                string destinationFilePath = Path.Combine(Path.GetTempPath(), ckConvertToPDF.Checked ? ((string.IsNullOrEmpty(selectedName) ? Path.GetFileNameWithoutExtension(this.SelectedFile.FullPath) : selectedName) + ".pdf") : (string.IsNullOrEmpty(selectedName) ? Path.GetFileName(this.SelectedFile.FullPath) : (selectedName + Path.GetExtension(this.SelectedFile.FullPath))));
                if (ckConvertToPDF.Checked)
                {
                    InteropClasses.PowerPointHelper.Instance.ExportPresentationAsPDF(rbActiveSlide.Checked ? this.ActiveSlide : -1, destinationFilePath);
                }
                else
                    if (rbActiveSlide.Checked)
                        InteropClasses.PowerPointHelper.Instance.SaveSingleSlide(this.ActiveSlide, destinationFilePath);
                    else
                        File.Copy(this.SelectedFile.FullPath, destinationFilePath, true);
                if (File.Exists(destinationFilePath))
                    BusinessClasses.LinkManager.EmailFile(destinationFilePath);
            }
        }
    }
}
