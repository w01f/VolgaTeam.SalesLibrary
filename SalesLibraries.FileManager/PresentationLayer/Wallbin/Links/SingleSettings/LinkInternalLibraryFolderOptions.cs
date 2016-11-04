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
	[IntendForClass(typeof(InternalLibraryFolderLink))]
	//public sealed partial class LinkInternalLibraryFolderOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalLibraryFolderOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly InternalLibraryFolderLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalLibraryFolderOptions(InternalLibraryFolderLink data)
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
			textEditLibraryName.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).LibraryName;
			textEditPageName.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).PageName;
			textEditWindowName.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).WindowName;

			textEditHeaderIcon.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).HeaderIcon;
			checkEditShowHeaderText.Checked = ((InternalLibraryFolderLinkSettings)_data.Settings).ShowHeaderText;
			comboBoxEditViewType.SelectedIndex = ((InternalLibraryFolderLinkSettings)_data.Settings).WindowViewType ==
												 InternalLinkSettings.PageViewTypeColumns ? 0 : 1;
			checkEditLinksOnly.Checked = ((InternalLibraryFolderLinkSettings)_data.Settings).LinksOnly;
			spinEditColumn.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).Column;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalLibraryFolderLinkSettings)_data.Settings).LibraryName = textEditLibraryName.EditValue as String;
			((InternalLibraryFolderLinkSettings)_data.Settings).PageName = textEditPageName.EditValue as String;
			((InternalLibraryFolderLinkSettings)_data.Settings).WindowName = textEditWindowName.EditValue as String;

			((InternalLibraryFolderLinkSettings)_data.Settings).HeaderIcon = textEditHeaderIcon.EditValue as String;
			((InternalLibraryFolderLinkSettings)_data.Settings).ShowHeaderText = checkEditShowHeaderText.Checked;
			((InternalLibraryFolderLinkSettings)_data.Settings).WindowViewType = comboBoxEditViewType.SelectedIndex == 0 ?
												 InternalLinkSettings.PageViewTypeColumns :
												 InternalLinkSettings.PageViewTypeAccording;
			((InternalLibraryFolderLinkSettings)_data.Settings).LinksOnly = checkEditLinksOnly.Checked;
			((InternalLibraryFolderLinkSettings)_data.Settings).Column = (Int32)spinEditColumn.Value;
		}
	}
}
