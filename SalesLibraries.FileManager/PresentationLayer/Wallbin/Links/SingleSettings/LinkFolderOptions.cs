using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryFolderLink))]
	//public partial class LinkFolderOptions : UserControl, ILinkSetSettingsEditControl
	public sealed partial class LinkFolderOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly List<LibraryFolderLink> _sourceLinks = new List<LibraryFolderLink>();

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Folder Settings</size>" };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkFolderOptions()
		{
			InitializeComponent();
			Text = "Admin";

			layoutControlItemRefreshPreview.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshPreview.MinSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public LinkFolderOptions(LinkType? defaultLinkType = null) : this() { }

		public LinkFolderOptions(ILinksGroup linksGroup, LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_sourceLinks.Clear();
			_sourceLinks.Add((LibraryFolderLink)sourceLink);

			LoadData();
		}

		public void LoadData(IEnumerable<BaseLibraryLink> sourceLinks)
		{
			_sourceLinks.Clear();
			_sourceLinks.AddRange(sourceLinks.OfType<LibraryFolderLink>());

			LoadData();
		}

		private void LoadData()
		{
			layoutControlItemRefreshPreview.Enabled = _sourceLinks.SelectMany(folderLink => folderLink.AllLinks.OfType<PreviewableFileLink>()).Any();
		}

		public void SaveData() { }

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _sourceLinks.Count == 1 ? _sourceLinks.First().NameWithExtension : "links", Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				foreach (var link in _sourceLinks
					.SelectMany(folderLink => folderLink.AllLinks.OfType<PreviewableFileLink>())
					.ToList())
				{
					link.ClearPreviewContainer();
					var previewContainer = link.GetPreviewContainer();
					var previewGenerator = previewContainer.GetPreviewContentGenerator();
					try
					{
						previewContainer.UpdatePreviewContent(previewGenerator, cancelationToken);
					}
					catch { }
				}
			});
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1} now updated for the server!", _sourceLinks.Count == 1 ? _sourceLinks.First().NameWithExtension : "Links", Environment.NewLine));
		}
	}
}
