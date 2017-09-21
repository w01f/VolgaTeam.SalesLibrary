using System;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(NetworkLink))]
	//public sealed partial class LinkLanOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkLanOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private NetworkLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>LAN</size>", Logo = Resources.LinkAddNetwork };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkLanOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkLanOptions(FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (NetworkLink)sourceLink;

			textEditName.EditValue = _data.Name;
			textEditPath.EditValue = _data.RelativePath;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			_data.RelativePath = textEditPath.EditValue as String;
		}
	}
}
