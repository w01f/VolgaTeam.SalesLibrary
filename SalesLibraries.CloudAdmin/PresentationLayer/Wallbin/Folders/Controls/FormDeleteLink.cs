using System;
using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class FormDeleteLink : MetroForm
	{
		public FormDeleteLink()
		{
			InitializeComponent();

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXDeleteLink.Font = new Font(buttonXDeleteLink.Font.FontFamily, buttonXDeleteLink.Font.Size - 2, buttonXDeleteLink.Font.Style);
			}
		}
	}
}
