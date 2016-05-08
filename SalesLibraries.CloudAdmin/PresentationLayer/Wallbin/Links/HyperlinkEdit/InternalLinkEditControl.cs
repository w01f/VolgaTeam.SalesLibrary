using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalLinkEditControl : UserControl, IHyperLinkEditControl
	{
		public InternalLinkEditControl()
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

				laLinkName.Font = new Font(laLinkName.Font.FontFamily, laLinkName.Font.Size - 2, laLinkName.Font.Style);
				laLibraryName.Font = new Font(laLibraryName.Font.FontFamily, laLibraryName.Font.Size - 2, laLibraryName.Font.Style);
				laPageName.Font = new Font(laPageName.Font.FontFamily, laPageName.Font.Size - 2, laPageName.Font.Style);
				laWindowName.Font = new Font(laWindowName.Font.FontFamily, laWindowName.Font.Size - 2, laWindowName.Font.Style);
				laLibraryLinkName.Font = new Font(laLibraryLinkName.Font.FontFamily, laLibraryLinkName.Font.Size - 2, laLibraryLinkName.Font.Style);
				laPageName.Font = new Font(laPageName.Font.FontFamily, laPageName.Font.Size - 2, laPageName.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalLinkInfo)GetHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.Name))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link name before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.LibraryName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target library before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.PageName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target page before saving");
				return false;
			}
			if (!String.IsNullOrEmpty(linkInfo.WindowName) || !String.IsNullOrEmpty(linkInfo.LinkName))
			{
				if (String.IsNullOrEmpty(linkInfo.WindowName))
				{
					MainController.Instance.PopupMessages.ShowWarning("You should set the target window before saving");
					return false;
				}
				if (String.IsNullOrEmpty(linkInfo.LinkName))
				{
					MainController.Instance.PopupMessages.ShowWarning("You should set the target link before saving");
					return false;
				}
			}
			return true;
		}

		public BaseNetworkLink GetHyperLinkInfo()
		{
			return new InternalLinkInfo
			{
				Name = textEditLinkName.EditValue as String,
				LibraryName = textEditLibraryName.EditValue as String,
				PageName = textEditPageName.EditValue as String,
				WindowName = textEditWindowName.EditValue as String,
				LinkName = textEditLibraryLinkName.EditValue as String,
				ForcePreview = checkEditForcePreview.Checked,
				FormatAsBluelink = checkEditBlueHyperlink.Checked,
				FormatBold = checkEditBold.Checked,
			};
		}

		public void ApplySharedSettings(BaseNetworkLink templateEditor)
		{
			if (templateEditor != null)
			{
				textEditLinkName.EditValue = templateEditor.Name;
				checkEditBlueHyperlink.Checked = templateEditor.FormatAsBluelink;
				checkEditBold.Checked = templateEditor.FormatBold;
			}
		}
	}
}
