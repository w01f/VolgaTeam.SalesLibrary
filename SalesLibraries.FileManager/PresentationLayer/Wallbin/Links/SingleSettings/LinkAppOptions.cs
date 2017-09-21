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
	[IntendForClass(typeof(AppLink))]
	//public sealed partial class LinkAppOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkAppOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private AppLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>App</size>", Logo = Resources.LinkAddApp };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkAppOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkAppOptions(FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (AppLink)sourceLink;

			textEditName.EditValue = _data.Name;
			textEditPath.EditValue = _data.RelativePath;
			textEditSecondPath.EditValue = ((AppLinkSettings)_data.Settings).SecondPath;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			_data.RelativePath = textEditPath.EditValue as String;
			((AppLinkSettings)_data.Settings).SecondPath = textEditSecondPath.EditValue as String;
		}
	}
}
