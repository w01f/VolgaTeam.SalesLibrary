using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
    public partial class FormZipFileName : Form
    {
        public FormZipFileName()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laLogo.Font = new System.Drawing.Font(laLogo.Font.FontFamily, laLogo.Font.Size - 2, laLogo.Font.Style);
                buttonXOK.Font = new System.Drawing.Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
                buttonXCancel.Font = new System.Drawing.Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
            }
        }

        public string FileName
        {
            get 
            {
                if (textEditFileName.EditValue != null)
                    return textEditFileName.EditValue.ToString();
                else
                    return null;
            }
        }
    }
}
