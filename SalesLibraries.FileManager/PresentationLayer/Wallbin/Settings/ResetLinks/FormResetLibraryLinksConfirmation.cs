using System;
using System.Collections.Generic;
using System.Linq;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	public partial class FormResetLibraryLinksConfirmation : MetroForm
	{
		public FormResetLibraryLinksConfirmation(IList<LinkSettingsGroupType> settingsGroupsForReset)
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
						case LinkSettingsGroupType.Thumbnails:
							return "Thumbnail";
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
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
