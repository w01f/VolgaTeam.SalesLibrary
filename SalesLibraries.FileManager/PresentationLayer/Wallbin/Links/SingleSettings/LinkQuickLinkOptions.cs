using System;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class LinkQuickLinkOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkQuickLinkOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private LibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes };
		public int Order => 3;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkQuickLinkOptions()
		{
			InitializeComponent();
			Text = "Quick Link";

			checkEditTitleInfo.Text = QuickLinkSettings.PredefinedQuickLinkTitleInfo;
			checkEditTitleHtml5.Text = QuickLinkSettings.PredefinedQuickLinkTitleHtml5;
			checkEditTitleLink.Text = QuickLinkSettings.PredefinedQuickLinkTitleLink;
			checkEditTitleResources.Text = QuickLinkSettings.PredefinedQuickLinkTitleResources;

			textEditUrl.Enter += EditorHelper.EditorEnter;
			textEditUrl.MouseUp += EditorHelper.EditorMouseUp;
			textEditUrl.MouseDown += EditorHelper.EditorMouseUp;

			layoutControlGroupControls.Enabled = false;
		}

		public LinkQuickLinkOptions(FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (LibraryObjectLink)sourceLink;

			checkEditEnable.Checked = !String.IsNullOrEmpty(_data.QuickLinkSettings.Url);
			textEditUrl.EditValue = _data.QuickLinkSettings.Url;
			var title = _data.QuickLinkSettings.Title;
			if (title.Equals(QuickLinkSettings.PredefinedQuickLinkTitleInfo))
				checkEditTitleInfo.Checked = true;
			else if (title.Equals(QuickLinkSettings.PredefinedQuickLinkTitleHtml5))
				checkEditTitleHtml5.Checked = true;
			else if (title.Equals(QuickLinkSettings.PredefinedQuickLinkTitleLink))
				checkEditTitleLink.Checked = true;
			else if (title.Equals(QuickLinkSettings.PredefinedQuickLinkTitleResources))
				checkEditTitleResources.Checked = true;
		}

		public void SaveData()
		{
			_data.QuickLinkSettings.Url = textEditUrl.EditValue as String;

			if (checkEditTitleInfo.Checked)
				_data.QuickLinkSettings.Title = QuickLinkSettings.PredefinedQuickLinkTitleInfo;
			else if (checkEditTitleHtml5.Checked)
				_data.QuickLinkSettings.Title = QuickLinkSettings.PredefinedQuickLinkTitleHtml5;
			else if (checkEditTitleLink.Checked)
				_data.QuickLinkSettings.Title = QuickLinkSettings.PredefinedQuickLinkTitleLink;
			else if (checkEditTitleResources.Checked)
				_data.QuickLinkSettings.Title = QuickLinkSettings.PredefinedQuickLinkTitleResources;
		}

		private void OnEnableCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupControls.Enabled = checkEditEnable.Checked;
			if (!checkEditEnable.Checked)
				textEditUrl.EditValue = null;
		}
	}
}
