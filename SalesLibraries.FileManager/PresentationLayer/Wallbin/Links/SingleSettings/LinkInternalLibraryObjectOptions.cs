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
	[IntendForClass(typeof(InternalLibraryObjectLink))]
	//public sealed partial class LinkInternalLibraryObjectOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalLibraryObjectOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly InternalLibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalLibraryObjectOptions(InternalLibraryObjectLink data)
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
			textEditLibraryName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).LibraryName;
			textEditPageName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).PageName;
			textEditWindowName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).WindowName;
			textEditLinkName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).LinkName;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalLibraryObjectLinkSettings)_data.Settings).LibraryName = textEditLibraryName.EditValue as String;
			((InternalLibraryObjectLinkSettings)_data.Settings).PageName = textEditPageName.EditValue as String;
			((InternalLibraryObjectLinkSettings)_data.Settings).WindowName = textEditWindowName.EditValue as String;
			((InternalLibraryObjectLinkSettings)_data.Settings).LinkName = textEditLinkName.EditValue as String;
		}
	}
}
