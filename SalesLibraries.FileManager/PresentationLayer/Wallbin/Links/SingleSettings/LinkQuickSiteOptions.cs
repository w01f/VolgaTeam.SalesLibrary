using System;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(QuickSiteLink))]
	//public sealed partial class LinkQuickSiteOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkQuickSiteOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private QuickSiteLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>QuickSite</size>", Logo = Resources.LinkAddQuickSite };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkQuickSiteOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkQuickSiteOptions(FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (QuickSiteLink)sourceLink;

			textEditName.EditValue = _data.Name;
			textEditPath.EditValue = _data.RelativePath;
			checkEditForcePreview.Checked = ((HyperLinkSettings)_data.Settings).ForcePreview;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			_data.RelativePath = textEditPath.EditValue as String;
			((HyperLinkSettings)_data.Settings).ForcePreview = checkEditForcePreview.Checked;
		}
	}
}
