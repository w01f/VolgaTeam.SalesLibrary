using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
    public partial class FormVideoOutput : Form
    {
        public FormVideoOutput()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                labelControlDescription.Font = new System.Drawing.Font(labelControlDescription.Font.FontFamily, labelControlDescription.Font.Size - 2, labelControlDescription.Font.Style);
                buttonXBackToForm.Font = new System.Drawing.Font(buttonXBackToForm.Font.FontFamily, buttonXBackToForm.Font.Size - 2, buttonXBackToForm.Font.Style);
                buttonXStayHere.Font = new System.Drawing.Font(buttonXStayHere.Font.FontFamily, buttonXStayHere.Font.Size - 2, buttonXStayHere.Font.Style);
                buttonXClose.Font = new System.Drawing.Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
            }
        }

        private void FormSlideOutput_Load(object sender, System.EventArgs e)
        {
            buttonXBackToForm.Text = string.Format(buttonXBackToForm.Text, ConfigurationClasses.SettingsManager.Instance.SalesDepotName);
        }
    }
}
