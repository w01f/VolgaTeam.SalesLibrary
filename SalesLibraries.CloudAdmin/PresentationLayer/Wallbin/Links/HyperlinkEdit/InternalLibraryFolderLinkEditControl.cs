using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalLibraryFolderLinkEditControl : UserControl, IInternalLinkEditControl
	{
		public InternalLibraryFolderLinkEditControl()
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
				laWindowName.Font = new Font(laWindowName.Font.FontFamily, laWindowName.Font.Size - 2, laWindowName.Font.Style);
				laHeaderIcon.Font = new Font(laHeaderIcon.Font.FontFamily, laHeaderIcon.Font.Size - 2, laHeaderIcon.Font.Style);
				laViewType.Font = new Font(laViewType.Font.FontFamily, laViewType.Font.Size - 2, laViewType.Font.Style);
				laColumn.Font = new Font(laColumn.Font.FontFamily, laColumn.Font.Size - 2, laColumn.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalLibraryFolderLinkInfo)GetHyperLinkInfo();
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
			if (String.IsNullOrEmpty(linkInfo.WindowName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target window before saving");
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
			return new InternalLibraryFolderLinkInfo
			{
				LibraryName = textEditLibraryName.EditValue as String,
				PageName = textEditPageName.EditValue as String,
				WindowName = textEditWindowName.EditValue as String,
				HeaderIcon = textEditHeaderIcon.EditValue as String,
				ShowHeaderText = checkEditShowHeaderText.Checked,
				WindowViewType = comboBoxEditViewType.SelectedIndex == 0 ? InternalLinkSettings.PageViewTypeColumns : InternalLinkSettings.PageViewTypeAccording,
				Column = (Int32)spinEditColumn.Value,
				LinksOnly = checkEditLinksOnly.Checked
			};
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo)
		{
			if (templateInfo != null)
			{
				textEditLibraryName.EditValue = templateInfo.LibraryName;
			}
			if (templateInfo is InternalWallbinLinkInfo)
			{
				textEditPageName.EditValue = ((InternalWallbinLinkInfo)templateInfo).PageName;
				textEditHeaderIcon.EditValue = ((InternalWallbinLinkInfo)templateInfo).HeaderIcon;
				checkEditShowHeaderText.Checked = ((InternalWallbinLinkInfo)templateInfo).ShowHeaderText;
				comboBoxEditViewType.SelectedIndex = ((InternalWallbinLinkInfo)templateInfo).PageViewType == InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
			}
			if (templateInfo is InternalLibraryPageLinkInfo)
			{
				textEditPageName.EditValue = ((InternalLibraryPageLinkInfo)templateInfo).PageName;
				textEditHeaderIcon.EditValue = ((InternalLibraryPageLinkInfo)templateInfo).HeaderIcon;
				checkEditShowHeaderText.Checked = ((InternalLibraryPageLinkInfo)templateInfo).ShowHeaderText;
				comboBoxEditViewType.SelectedIndex = ((InternalLibraryPageLinkInfo)templateInfo).PageViewType == InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
			}
			if (templateInfo is InternalLibraryObjectLinkInfo)
			{
				textEditPageName.EditValue = ((InternalLibraryObjectLinkInfo)templateInfo).PageName;
				textEditWindowName.EditValue = ((InternalLibraryObjectLinkInfo)templateInfo).WindowName;
			}
		}
	}
}
