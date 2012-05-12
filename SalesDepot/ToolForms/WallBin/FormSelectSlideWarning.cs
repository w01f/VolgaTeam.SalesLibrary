using System;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
    public partial class FormSelectSlideWarning : Form
    {
        public FormSelectSlideWarning()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laText.Font = new System.Drawing.Font(laText.Font.FontFamily, laText.Font.Size - 3, laText.Font.Style);
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
