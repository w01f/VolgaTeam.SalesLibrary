using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(InternalWallbinLink))]
	//public sealed partial class LinkInternalWallbinOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalWallbinOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly InternalWallbinLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalWallbinOptions(InternalWallbinLink data)
		{
			InitializeComponent();
			Text = "Admin";
			_data = data;
			if ((CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
			}
		}

		public void LoadData()
		{
			textEditName.EditValue = _data.Name;
			textEditLibraryName.EditValue = ((InternalWallbinLinkSettings)_data.Settings).LibraryName;
			textEditPageName.EditValue = ((InternalWallbinLinkSettings)_data.Settings).PageName;

			textEditHeaderIcon.EditValue = ((InternalWallbinLinkSettings)_data.Settings).HeaderIcon;
			checkEditShowHeaderText.Checked = ((InternalWallbinLinkSettings)_data.Settings).ShowHeaderText;
			comboBoxEditViewType.SelectedIndex = ((InternalWallbinLinkSettings)_data.Settings).PageViewType ==
												 InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
			comboBoxEditSelectorType.SelectedIndex = ((InternalWallbinLinkSettings)_data.Settings).PageSelectorType ==
												 InternalLinkSettings.PageSelectorTypeTabs ? 0 : 1;
			checkEditShowLogo.Checked = ((InternalWallbinLinkSettings)_data.Settings).ShowLogo;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalWallbinLinkSettings)_data.Settings).LibraryName = textEditLibraryName.EditValue as String;
			((InternalWallbinLinkSettings)_data.Settings).PageName = textEditPageName.EditValue as String;

			((InternalWallbinLinkSettings)_data.Settings).HeaderIcon = textEditHeaderIcon.EditValue as String;
			((InternalWallbinLinkSettings)_data.Settings).ShowHeaderText = checkEditShowHeaderText.Checked;
			((InternalWallbinLinkSettings)_data.Settings).PageViewType = comboBoxEditViewType.SelectedIndex == 0 ?
												 InternalLinkSettings.PageViewTypeColumns :
												 InternalLinkSettings.PageViewTypeAccording;
			((InternalWallbinLinkSettings)_data.Settings).PageSelectorType = comboBoxEditSelectorType.SelectedIndex == 0 ?
												 InternalLinkSettings.PageSelectorTypeTabs :
												 InternalLinkSettings.PageSelectorTypeCombo;
			((InternalWallbinLinkSettings)_data.Settings).ShowLogo = checkEditShowLogo.Checked;
		}
	}
}
