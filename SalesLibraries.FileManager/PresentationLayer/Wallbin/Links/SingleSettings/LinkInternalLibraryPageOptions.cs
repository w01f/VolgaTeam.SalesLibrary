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
	[IntendForClass(typeof(InternalLibraryPageLink))]
	//public sealed partial class LinkInternalLibraryPageOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalLibraryPageOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly InternalLibraryPageLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalLibraryPageOptions(InternalLibraryPageLink data)
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
			textEditLibraryName.EditValue = ((InternalLibraryPageLinkSettings)_data.Settings).LibraryName;
			textEditPageName.EditValue = ((InternalLibraryPageLinkSettings)_data.Settings).PageName;

			textEditHeaderIcon.EditValue = ((InternalLibraryPageLinkSettings)_data.Settings).HeaderIcon;
			checkEditShowHeaderText.Checked = ((InternalLibraryPageLinkSettings)_data.Settings).ShowHeaderText;
			comboBoxEditViewType.SelectedIndex = ((InternalLibraryPageLinkSettings)_data.Settings).PageViewType ==
												 InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
			colorEditTextColor.Color = ((InternalLibraryPageLinkSettings)_data.Settings).TextColor ?? Color.Black;
			colorEditBackColor.Color = ((InternalLibraryPageLinkSettings)_data.Settings).BackColor ?? Color.White;
			checkEditShowLogo.Checked = ((InternalLibraryPageLinkSettings)_data.Settings).ShowLogo;
			checkEditShowText.Checked = ((InternalLibraryPageLinkSettings)_data.Settings).ShowText;
			checkEditShowWindowHeaders.Checked = ((InternalLibraryPageLinkSettings)_data.Settings).ShowWindowHeaders;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalLibraryPageLinkSettings)_data.Settings).LibraryName = textEditLibraryName.EditValue as String;
			((InternalLibraryPageLinkSettings)_data.Settings).PageName = textEditPageName.EditValue as String;

			((InternalLibraryPageLinkSettings)_data.Settings).HeaderIcon = textEditHeaderIcon.EditValue as String;
			((InternalLibraryPageLinkSettings)_data.Settings).ShowHeaderText = checkEditShowHeaderText.Checked;
			((InternalLibraryPageLinkSettings)_data.Settings).PageViewType = comboBoxEditViewType.SelectedIndex == 0 ?
												 InternalLinkSettings.PageViewTypeColumns :
												 InternalLinkSettings.PageViewTypeAccording;
			((InternalLibraryPageLinkSettings)_data.Settings).TextColor = colorEditTextColor.Color;
			((InternalLibraryPageLinkSettings)_data.Settings).BackColor = colorEditBackColor.Color;
			((InternalLibraryPageLinkSettings)_data.Settings).ShowLogo = checkEditShowLogo.Checked;
			((InternalLibraryPageLinkSettings)_data.Settings).ShowText = checkEditShowText.Checked;
			((InternalLibraryPageLinkSettings)_data.Settings).ShowWindowHeaders = checkEditShowWindowHeaders.Checked;
		}
	}
}
