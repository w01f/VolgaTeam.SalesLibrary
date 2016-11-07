using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormResetLinkSettingsConfirmation : MetroForm
	{
		public FormResetLinkSettingsConfirmation(IList<LinkSettingsGroupType> settingsGroupsForReset)
		{
			InitializeComponent();

			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				String.Join(", ", settingsGroupsForReset.Select(linkSettingsGroupType =>
				{
					switch (linkSettingsGroupType)
					{
						case LinkSettingsGroupType.Banners:
							return "Clipart-Logo";
						case LinkSettingsGroupType.Widgets:
							return "Widget-Icon";
						case LinkSettingsGroupType.TextFormatting:
							return "Text Formatting";
						case LinkSettingsGroupType.TextNote:
							return "Link Note";
						case LinkSettingsGroupType.HoverNote:
							return "Hover Note";
						case LinkSettingsGroupType.SearchTags:
							return "Category Tags";
						case LinkSettingsGroupType.Keywords:
							return "Keyword Tags";
						case LinkSettingsGroupType.Security:
							return "Security";
						case LinkSettingsGroupType.Expiration:
							return "Expiration Date";
						case LinkSettingsGroupType.QuickLink:
							return "Quick Link";
						case LinkSettingsGroupType.AutoWidgets:
							return "Auto Widgets";
						default:
							throw new ArgumentOutOfRangeException("Undefined Settings Group");
					}
				})));

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
				buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
			}
		}
	}
}
