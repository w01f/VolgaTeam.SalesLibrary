using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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
		private readonly VideoLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkVideoOptions(VideoLink data)
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
				ckForcePreview.Font = new Font(ckForcePreview.Font.FontFamily, ckForcePreview.Font.Size - 2, ckForcePreview.Font.Style);
				ckDownloadSource.Font = new Font(ckDownloadSource.Font.FontFamily, ckDownloadSource.Font.Size - 2, ckDownloadSource.Font.Style);
			}
		}

		public void LoadData()
		{
			ckForcePreview.Checked = ((VideoLinkSettings)_data.Settings).ForcePreview;
			ckDownloadSource.Checked = ((VideoLinkSettings)_data.Settings).DownloadSource;
			if (Directory.Exists(_data.PreviewContainerPath))
			{
				buttonXOpenWV.Enabled = true;
				buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _data.PreviewContainerName);
			}
			else
				buttonXOpenWV.Enabled = false;
		}

		public void SaveData()
		{
			((VideoLinkSettings)_data.Settings).ForcePreview = ckForcePreview.Checked;
			((VideoLinkSettings)_data.Settings).DownloadSource = ckDownloadSource.Checked;
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _data.NameWithExtension, Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", cancelationToken =>
			{
				_data.ClearPreviewContainer();
				var previewContainer = _data.GetPreviewContainer();
				var previewGenerator = previewContainer.GetPreviewGenerator();
				previewContainer.UpdateContent(previewGenerator, cancelationToken);
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
