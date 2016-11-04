using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalWallbinLinkEditControl : UserControl, IInternalLinkEditControl
	{
		public InternalWallbinLinkEditControl()
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
				laSelectorType.Font = new Font(laSelectorType.Font.FontFamily, laSelectorType.Font.Size - 2, laSelectorType.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalWallbinLinkInfo)GetHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.LibraryName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target library before saving");
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
			return new InternalWallbinLinkInfo
			{
				LibraryName = textEditLibraryName.EditValue as String,
				PageName = textEditPageName.EditValue as String,
				HeaderIcon = textEditHeaderIcon.EditValue as String,
				ShowHeaderText = checkEditShowHeaderText.Checked,
				PageViewType = comboBoxEditViewType.SelectedIndex == 0 ? InternalLinkSettings.PageViewTypeColumns : InternalLinkSettings.PageViewTypeAccording,
				PageSelectorType = comboBoxEditSelectorType.SelectedIndex == 0 ? InternalLinkSettings.PageSelectorTypeTabs : InternalLinkSettings.PageSelectorTypeCombo,
				ShowLogo = checkEditShowLogo.Checked
			};
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo)
		{
			if (templateInfo != null)
			{
				textEditLibraryName.EditValue = templateInfo.LibraryName;
			}
			if (templateInfo is InternalLibraryPageLinkInfo)
			{
				textEditPageName.EditValue = ((InternalLibraryPageLinkInfo)templateInfo).PageName;
				textEditHeaderIcon.EditValue = ((InternalLibraryPageLinkInfo)templateInfo).HeaderIcon;
				checkEditShowHeaderText.Checked = ((InternalLibraryPageLinkInfo)templateInfo).ShowHeaderText;
				comboBoxEditViewType.SelectedIndex = ((InternalLibraryPageLinkInfo)templateInfo).PageViewType == InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
				checkEditShowLogo.Checked = ((InternalLibraryPageLinkInfo) templateInfo).ShowLogo;
			}
			if (templateInfo is InternalLibraryFolderLinkInfo)
			{
				textEditPageName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).PageName;
				textEditHeaderIcon.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).HeaderIcon;
				checkEditShowHeaderText.Checked = ((InternalLibraryFolderLinkInfo)templateInfo).ShowHeaderText;
				comboBoxEditViewType.SelectedIndex = ((InternalLibraryFolderLinkInfo)templateInfo).WindowViewType == InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
			}
		}
	}
}
