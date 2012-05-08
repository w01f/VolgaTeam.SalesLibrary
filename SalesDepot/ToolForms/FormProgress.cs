using System.Windows.Forms;

namespace SalesDepot.ToolForms
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laProgress.Font = new System.Drawing.Font(laProgress.Font.FontFamily, laProgress.Font.Size - 2, laProgress.Font.Style);
            }
        }
    }
}