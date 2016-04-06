using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class UrlEditControl : UserControl, IHyperLinkEditControl
	{
		public UrlEditControl()
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
				ckBlueHyperlink.Font = new Font(ckBlueHyperlink.Font.FontFamily, ckBlueHyperlink.Font.Size - 2,
					ckBlueHyperlink.Font.Style);
				ckForcePreview.Font = new Font(ckForcePreview.Font.FontFamily, ckForcePreview.Font.Size - 2,
					ckForcePreview.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (UrlLinkInfo)GetHyperLinkInfo();
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

		public BaseNetworkLink GetHyperLinkInfo()
		{
			return new UrlLinkInfo
			{
				Name = textEditName.EditValue as String,
				Path = textEditPath.EditValue as String,
				FormatAsBluelink = ckBlueHyperlink.Checked,
				ForcePreview = ckForcePreview.Checked
			};
		}

		public void ApplySharedSettings(BaseNetworkLink templateEditor)
		{
			if (templateEditor != null)
			{
				textEditName.EditValue = templateEditor.Name;
				ckBlueHyperlink.Checked = templateEditor.FormatAsBluelink;
			}
			if (templateEditor is HyperLinkInfo)
			{
				textEditPath.EditValue = ((HyperLinkInfo)templateEditor).Path;
			}
			if (templateEditor is UrlLinkInfo)
			{
				ckForcePreview.Checked = ((UrlLinkInfo)templateEditor).ForcePreview;
			}
			if (templateEditor is YouTubeLinkInfo)
			{
				ckForcePreview.Checked = ((YouTubeLinkInfo)templateEditor).ForcePreview;
			}
		}
	}
}
