using System.Windows.Forms;

namespace SalesDepot.ToolForms
{
    public partial class FormSaveAsPDF : Form
    {
        public bool WholeFile { get; set; }

        public FormSaveAsPDF()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                rbFile.Font = new System.Drawing.Font(rbFile.Font.FontFamily, rbFile.Font.Size - 2, rbFile.Font.Style);
                rbSlide.Font = new System.Drawing.Font(rbSlide.Font.FontFamily, rbSlide.Font.Size - 2, rbSlide.Font.Style);
                btSave.Font = new System.Drawing.Font(btSave.Font.FontFamily, btSave.Font.Size - 2, btSave.Font.Style);
                btCancel.Font = new System.Drawing.Font(btCancel.Font.FontFamily, btCancel.Font.Size - 2, btCancel.Font.Style);
            }
        }

        private void FormSaveAsPDF_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.WholeFile = rbFile.Checked;
        }
    }
}
