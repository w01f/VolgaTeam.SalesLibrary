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
	[IntendForClass(typeof(Html5Link))]
	//public sealed partial class LinkHtml5Options : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkHtml5Options : XtraTabPage, ILinkSettingsEditControl
	{
		private Html5Link _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>HTML5</size>", Logo = Resources.LinkAddHtml5 };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkHtml5Options()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkHtml5Options(FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (Html5Link)sourceLink;

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
