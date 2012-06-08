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
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, false);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void FormProgress_Shown(object sender, System.EventArgs e)
        {
            laProgress.Focus();
            circularProgress.IsRunning = true;
        }
    }
}