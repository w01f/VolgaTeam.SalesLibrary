using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormSelectSlideWarning : MetroForm
	{
		public FormSelectSlideWarning()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laText.Font = new Font(laText.Font.FontFamily, laText.Font.Size - 3, laText.Font.Style);
			}
		}
	}
}