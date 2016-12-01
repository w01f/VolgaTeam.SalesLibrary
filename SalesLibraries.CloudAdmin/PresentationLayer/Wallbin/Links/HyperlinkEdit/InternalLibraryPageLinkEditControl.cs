using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalLibraryPageLinkEditControl : UserControl, IInternalLinkEditControl
	{
		public InternalLibraryPageLinkEditControl()
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

				laLibraryName.Font = new Font(laLibraryName.Font.FontFamily, laLibraryName.Font.Size - 2, laLibraryName.Font.Style);
				laPageName.Font = new Font(laPageName.Font.FontFamily, laPageName.Font.Size - 2, laPageName.Font.Style);
				laHeaderIcon.Font = new Font(laHeaderIcon.Font.FontFamily, laHeaderIcon.Font.Size - 2, laHeaderIcon.Font.Style);
				laViewType.Font = new Font(laViewType.Font.FontFamily, laViewType.Font.Size - 2, laViewType.Font.Style);
				laTextColor.Font = new Font(laTextColor.Font.FontFamily, laTextColor.Font.Size - 2, laTextColor.Font.Style);
				laBackColor.Font = new Font(laBackColor.Font.FontFamily, laBackColor.Font.Size - 2, laBackColor.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalLibraryPageLinkInfo)GetHyperLinkInfo();
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
			if (String.IsNullOrEmpty(linkInfo.HeaderIcon))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the header icon before saving");
				return false;
			}
			return true;
		}

		public InternalLinkInfo GetHyperLinkInfo()
		{
			return new InternalLibraryPageLinkInfo
			{
				LibraryName = textEditLibraryName.EditValue as String,
				PageName = textEditPageName.EditValue as String,
				HeaderIcon = textEditHeaderIcon.EditValue as String,
				ShowHeaderText = checkEditShowHeaderText.Checked,
				PageViewType = comboBoxEditViewType.SelectedIndex == 0 ? InternalLinkSettings.PageViewTypeColumns : InternalLinkSettings.PageViewTypeAccording,
				TextColor = colorEditTextColor.Color,
				BackColor = colorEditBackColor.Color,
				ShowText = checkEditShowText.Checked,
				ShowLogo = checkEditShowLogo.Checked,
				ShowWindowHeaders = checkEditShowWindowHeaders.Checked
			};
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo)
		{
			if (templateInfo is InternalWallbinLinkInfo)
			{
				textEditLibraryName.EditValue = ((InternalWallbinLinkInfo)templateInfo).LibraryName;
				textEditPageName.EditValue = ((InternalWallbinLinkInfo)templateInfo).PageName;
				textEditHeaderIcon.EditValue = ((InternalWallbinLinkInfo)templateInfo).HeaderIcon;
				checkEditShowHeaderText.Checked = ((InternalWallbinLinkInfo)templateInfo).ShowHeaderText;
				comboBoxEditViewType.SelectedIndex = ((InternalWallbinLinkInfo)templateInfo).PageViewType == InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
				checkEditShowLogo.Checked = ((InternalWallbinLinkInfo)templateInfo).ShowLogo;
			}
			if (templateInfo is InternalLibraryFolderLinkInfo)
			{
				textEditLibraryName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).LibraryName;
				textEditPageName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).PageName;
				textEditHeaderIcon.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).HeaderIcon;
				checkEditShowHeaderText.Checked = ((InternalLibraryFolderLinkInfo)templateInfo).ShowHeaderText;
				comboBoxEditViewType.SelectedIndex = ((InternalLibraryFolderLinkInfo)templateInfo).WindowViewType == InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
			}
			if (templateInfo is InternalLibraryObjectLinkInfo)
			{
				textEditPageName.EditValue = ((InternalLibraryObjectLinkInfo)templateInfo).PageName;
			}
		}
	}
}
