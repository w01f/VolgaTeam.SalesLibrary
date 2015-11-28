using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries
{
	public partial class FormInactiveLinksNotification : MetroForm
	{
		public FormInactiveLinksNotification()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laQuestion.Font = new Font(laQuestion.Font.FontFamily, laQuestion.Font.Size - 2, laQuestion.Font.Style);
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				buttonXFix.Font = new Font(buttonXFix.Font.FontFamily, buttonXFix.Font.Size - 2, buttonXFix.Font.Style);
				buttonXIgnore.Font = new Font(buttonXIgnore.Font.FontFamily, buttonXIgnore.Font.Size - 2, buttonXIgnore.Font.Style);
			}
		}
	}
}