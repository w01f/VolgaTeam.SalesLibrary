using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace SalesDepot.ToolForms.Settings
{
	public partial class FormAuthorizeWarning : MetroForm
	{
		public FormAuthorizeWarning()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXClose.Font = new System.Drawing.Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
			}
		}
	}
}
