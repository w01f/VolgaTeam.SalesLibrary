using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(YouTubeLink))]
	//public sealed partial class LinkYouTubeOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkYouTubeOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private YouTubeLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>YouTube</size>", Logo = Resources.LinkAddYoutube };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkYouTubeOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkYouTubeOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (YouTubeLink)sourceLink;

			textEditName.EditValue = _data.Name;
			textEditPath.EditValue = _data.RelativePath;
			checkEditForcePreview.Checked = ((HyperLinkSettings)_data.Settings).ForcePreview;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			_data.RelativePath = textEditPath.EditValue as String;
			((HyperLinkSettings)_data.Settings).ForcePreview = checkEditForcePreview.Checked;
			buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _data.PreviewContainerName);
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _data.LinkInfoDisplayName, Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				_data.ClearPreviewContainer();
				var previewContainer = _data.GetPreviewContainer();
				var previewGenerator = previewContainer.GetPreviewContentGenerator();
				try
				{
					previewContainer.UpdatePreviewContent(previewGenerator, cancelationToken);
				}
				catch { }
			});
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1} now updated for the server!", _data.LinkInfoDisplayName, Environment.NewLine));
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.PreviewContainerPath);
			}
			catch { }
		}
	}
}
