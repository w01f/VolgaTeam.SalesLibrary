using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
    public partial class FormSlideOutput : Form
    {
        public FormSlideOutput()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new System.Drawing.Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
                laDescription.Font = new System.Drawing.Font(laDescription.Font.FontFamily, laDescription.Font.Size - 2, laDescription.Font.Style);
                buttonXBackToForm.Font = new System.Drawing.Font(buttonXBackToForm.Font.FontFamily, buttonXBackToForm.Font.Size - 2, buttonXBackToForm.Font.Style);
                buttonXStayHere.Font = new System.Drawing.Font(buttonXStayHere.Font.FontFamily, buttonXStayHere.Font.Size - 2, buttonXStayHere.Font.Style);
                buttonXClose.Font = new System.Drawing.Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
            }
        }

        private void FormSlideOutput_Load(object sender, System.EventArgs e)
        {
            buttonXBackToForm.Text = string.Format(buttonXBackToForm.Text, ConfigurationClasses.SettingsManager.Instance.SalesDepotName);
            AppManager.Instance.ActivateForm(this.Handle, false, false);
        }
    }
}
