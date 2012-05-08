using System;
using System.Windows.Forms;

namespace SalesDepot.ToolForms
{
    public partial class FormVideoViewOptions : Form
    {
        private bool isAdd;

        private bool doNotAdd = false;

        public FormVideoViewOptions()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                buttonXAddToPresentation.Font = new System.Drawing.Font(buttonXAddToPresentation.Font.FontFamily, buttonXAddToPresentation.Font.Size - 3, buttonXAddToPresentation.Font.Style);
                buttonXClose.Font = new System.Drawing.Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
                buttonXReview.Font = new System.Drawing.Font(buttonXReview.Font.FontFamily, buttonXReview.Font.Size - 3, buttonXReview.Font.Style);
            }
        }

        public bool IsAdd
        {
            get
            {
                return isAdd;
            }
        }

        public bool DoNotAdd
        {
            set
            {
                doNotAdd = value;
            }
        }

        private void VideoViewOptionsForm_Load(object sender, EventArgs e)
        {
            if (doNotAdd)
                buttonXAddToPresentation.Enabled = false;
        }

        private void buttonXAddToPresentation_Click(object sender, EventArgs e)
        {
            isAdd = true;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonXReview_Click(object sender, EventArgs e)
        {
            isAdd = false;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonXClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}