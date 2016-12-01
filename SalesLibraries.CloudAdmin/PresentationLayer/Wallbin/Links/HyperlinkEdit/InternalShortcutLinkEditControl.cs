using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalShortcutLinkEditControl : UserControl, IInternalLinkEditControl
	{
		public InternalShortcutLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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

				laShortcutId.Font = new Font(laShortcutId.Font.FontFamily, laShortcutId.Font.Size - 2, laShortcutId.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalShortcutLinkInfo)GetHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.ShortcutId))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the shortcut id before saving");
				return false;
			}
			return true;
		}

		public InternalLinkInfo GetHyperLinkInfo()
		{
			return new InternalShortcutLinkInfo
			{
				ShortcutId = textEditShortcutId.EditValue as String,
				OpenOnSamePage = !checkEditOpenOnSamePage.Checked
			};
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo) { }
	}
}
