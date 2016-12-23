using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class QuickSiteEditControl : UserControl, IHyperLinkEditControl
	{
		public QuickSiteEditControl()
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

				laName.Font = new Font(laName.Font.FontFamily, laName.Font.Size - 2, laName.Font.Style);
				laPath.Font = new Font(laPath.Font.FontFamily, laPath.Font.Size - 2, laPath.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (QuickSiteLinkInfo)GetHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.Name))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link name before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.Path))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link path before saving");
				return false;
			}
			return true;
		}

		public BaseNetworkLinkInfo GetHyperLinkInfo()
		{
			return new QuickSiteLinkInfo()
			{
				Name = textEditName.EditValue as String,
				Path = textEditPath.EditValue as String,
				FormatAsBluelink = checkEditBlueHyperlink.Checked,
				FormatBold = checkEditBold.Checked,
				ForcePreview = checkEditForcePreview.Checked
			};
		}

		public void ApplySharedSettings(BaseNetworkLinkInfo templateInfo)
		{
			if (templateInfo != null)
			{
				textEditName.EditValue = templateInfo.Name;
				checkEditBlueHyperlink.Checked = templateInfo.FormatAsBluelink;
				checkEditBold.Checked = templateInfo.FormatBold;
			}
			if (templateInfo is HyperLinkInfo)
			{
				textEditPath.EditValue = ((HyperLinkInfo)templateInfo).Path;
			}
		}
	}
}
