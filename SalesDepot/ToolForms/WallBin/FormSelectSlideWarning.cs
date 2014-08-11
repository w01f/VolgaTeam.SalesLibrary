using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormSelectSlideWarning : MetroForm
	{
		public FormSelectSlideWarning()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laText.Font = new Font(laText.Font.FontFamily, laText.Font.Size - 3, laText.Font.Style);
			}
		}
	}
}