using System;
using System.Collections.Generic;
using System.Linq;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	public partial class FormResetLineBreaksConfirmation : MetroForm
	{
		public FormResetLineBreaksConfirmation(IList<LinkSettingsGroupType> settingsGroupsForReset)
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
						case LinkSettingsGroupType.TextNote:
							return "Text";
						case LinkSettingsGroupType.HoverNote:
							return "Hover Note";
						default:
							throw new ArgumentOutOfRangeException("Undefined Settings Group");
					}
				})));
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
