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
	[IntendForClass(typeof(InternalShortcutLink))]
	//public sealed partial class LinkInternalShortcutOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalShortcutOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly InternalShortcutLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalShortcutOptions(InternalShortcutLink data)
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

				ckOpenOnSamePage.Font = new Font(ckOpenOnSamePage.Font.FontFamily, ckOpenOnSamePage.Font.Size - 2, ckOpenOnSamePage.Font.Style);
			}
		}

		public void LoadData()
		{
			textEditName.EditValue = _data.Name;
			textEditShortcutId.EditValue = ((InternalShortcutLinkSettings)_data.Settings).ShortcutId;
			ckOpenOnSamePage.Checked = !((InternalShortcutLinkSettings)_data.Settings).OpenOnSamePage;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalShortcutLinkSettings)_data.Settings).ShortcutId = textEditShortcutId.EditValue as String;
			((InternalShortcutLinkSettings)_data.Settings).OpenOnSamePage = !ckOpenOnSamePage.Checked;
		}
	}
}
