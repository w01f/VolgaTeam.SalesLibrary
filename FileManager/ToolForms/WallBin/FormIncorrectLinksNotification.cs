using System.Drawing;
using System.Windows.Forms;

namespace FileManager.ToolForms.WallBin
{
    public partial class FormIncorrectLinksNotification : Form
    {
        public FormIncorrectLinksNotification()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laQuestion.Font = new Font(laQuestion.Font.FontFamily, laQuestion.Font.Size - 2, laQuestion.Font.Style);
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
                btFix.Font = new Font(btFix.Font.FontFamily, btFix.Font.Size - 2, btFix.Font.Style);
                btIgnore.Font = new Font(btIgnore.Font.FontFamily, btIgnore.Font.Size - 2, btIgnore.Font.Style);
            }
        }
    }
}
