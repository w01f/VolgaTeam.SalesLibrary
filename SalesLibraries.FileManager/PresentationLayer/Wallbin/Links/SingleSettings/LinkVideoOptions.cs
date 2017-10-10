using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(VideoLink))]
	//public sealed partial class LinkVideoOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkVideoOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private VideoLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkVideoOptions()
		{
			InitializeComponent();
			Text = "Admin";

			layoutControlItemRefreshPreview.MinSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshPreview.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public LinkVideoOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (VideoLink)sourceLink;

			checkEditForcePreview.Checked = ((VideoLinkSettings)_data.Settings).ForcePreview;
			checkEditDownloadSource.Checked = ((VideoLinkSettings)_data.Settings).DownloadSource;
			buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _data.PreviewContainerName);
		}

		public void SaveData()
		{
			((VideoLinkSettings)_data.Settings).ForcePreview = checkEditForcePreview.Checked;
			((VideoLinkSettings)_data.Settings).DownloadSource = checkEditDownloadSource.Checked;
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _data.NameWithExtension, Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				_data.ClearPreviewContainer();
				var previewContainer = _data.GetPreviewContainer();
				var previewGenerator = previewContainer.GetPreviewGenerator();
				try
				{
					previewContainer.UpdateContent(previewGenerator, cancelationToken);
				}
				catch { }
			});
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1}Is now updated for the server!", _data.NameWithExtension, Environment.NewLine));
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
