using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(DocumentLink))]
	//public partial class LinkDocumentOptions : UserControl, ILinkSetSettingsEditControl
	public sealed partial class LinkDocumentOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly List<DocumentLink> _sourceLinks = new List<DocumentLink>();
		private readonly ILinksGroup _linksGroup;
		private readonly LinkType? _defaultLinkType;

		private DocumentLink DefaultLink => _sourceLinks.First();

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = String.Format("<size=+4>{0} Settings</size>", _defaultLinkType == LinkType.Pdf ? "PDF" : "Document") };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkDocumentOptions()
		{
			InitializeComponent();
			Text = "Admin";

			layoutControlItemRefreshPreview.MinSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshPreview.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public LinkDocumentOptions(LinkType? defaultLinkType = null) : this()
		{
			_defaultLinkType = defaultLinkType;
		}

		public LinkDocumentOptions(ILinksGroup linksGroup, LinkType? defaultLinkType = null) : this()
		{
			_linksGroup = linksGroup;
			_defaultLinkType = defaultLinkType;
		}

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_sourceLinks.Clear();
			_sourceLinks.Add((DocumentLink)sourceLink);

			LoadData();
		}

		public void LoadData(IEnumerable<BaseLibraryLink> sourceLinks)
		{
			_sourceLinks.Clear();
			_sourceLinks.AddRange(sourceLinks.OfType<DocumentLink>());

			LoadData();
		}

		private void LoadData()
		{
			layoutControlItemSaveAsTemplate.Visibility = _linksGroup != null ? LayoutVisibility.Always : LayoutVisibility.Never;

			var settingsTemplate = _linksGroup?.LinkGroupSettingsContainer.GetSettingsTemplate<DocumentLinkSettings>(
					LinkSettingsGroupType.AdminSettings, _defaultLinkType);
			if (settingsTemplate != null)
			{
				checkEditIsArchiveResource.Checked = settingsTemplate.IsArchiveResource;
				checkEditDoNotGeneratePreview.Checked = !settingsTemplate.GeneratePreviewImages;
				checkEditDoNotGenerateText.Checked = !settingsTemplate.GenerateContentText;
				checkEditForcePreview.Checked = settingsTemplate.ForcePreview;
				checkEditSaveAsTemplate.Checked = true;
			}
			else if (_sourceLinks.Any())
			{
				var linkSettings = _sourceLinks.Select(link => link.Settings).OfType<DocumentLinkSettings>().ToList();

				if (linkSettings.All(settings => settings.IsArchiveResource))
					checkEditIsArchiveResource.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.IsArchiveResource))
					checkEditIsArchiveResource.CheckState = CheckState.Indeterminate;
				else
					checkEditIsArchiveResource.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => !settings.GeneratePreviewImages))
					checkEditDoNotGeneratePreview.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => !settings.GeneratePreviewImages))
					checkEditDoNotGeneratePreview.CheckState = CheckState.Indeterminate;
				else
					checkEditDoNotGeneratePreview.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => !settings.GenerateContentText))
					checkEditDoNotGenerateText.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => !settings.GenerateContentText))
					checkEditDoNotGenerateText.CheckState = CheckState.Indeterminate;
				else
					checkEditDoNotGenerateText.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => settings.ForcePreview))
					checkEditForcePreview.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.ForcePreview))
					checkEditForcePreview.CheckState = CheckState.Indeterminate;
				else
					checkEditForcePreview.CheckState = CheckState.Unchecked;

				checkEditSaveAsTemplate.Checked = false;
			}

			if (_sourceLinks.Count == 1)
			{
				layoutControlItemOpenWV.Visibility = LayoutVisibility.Always;
				buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _sourceLinks.First().PreviewContainerName);
			}
			else
				layoutControlItemOpenWV.Visibility = LayoutVisibility.Never;

			layoutControlItemRefreshPreview.Enabled = _sourceLinks.Any();
		}

		public void SaveData()
		{
			if (!_sourceLinks.Any()) return;
			foreach (var link in _sourceLinks)
			{
				((DocumentLinkSettings)link.Settings).IsArchiveResource = checkEditIsArchiveResource.CheckState != CheckState.Indeterminate ?
					checkEditIsArchiveResource.Checked :
					((DocumentLinkSettings)link.Settings).IsArchiveResource;
				if (((DocumentLinkSettings)link.Settings).IsArchiveResource)
				{
					((DocumentLinkSettings)link.Settings).GeneratePreviewImages = false;
					((DocumentLinkSettings)link.Settings).GenerateContentText = false;
					((DocumentLinkSettings)link.Settings).ForcePreview = true;
				}
				else
				{
					((DocumentLinkSettings)link.Settings).GeneratePreviewImages = checkEditDoNotGeneratePreview.CheckState != CheckState.Indeterminate ?
						!checkEditDoNotGeneratePreview.Checked :
						((DocumentLinkSettings)link.Settings).GeneratePreviewImages;
					((DocumentLinkSettings)link.Settings).GenerateContentText = checkEditDoNotGenerateText.CheckState != CheckState.Indeterminate ?
						!checkEditDoNotGenerateText.Checked :
						((DocumentLinkSettings)link.Settings).GenerateContentText;
					((DocumentLinkSettings)link.Settings).ForcePreview = checkEditForcePreview.CheckState != CheckState.Indeterminate ?
						checkEditForcePreview.Checked :
						((DocumentLinkSettings)link.Settings).ForcePreview;
				}
			}

			if (checkEditSaveAsTemplate.Checked)
			{
				var setttingsTemplate = SettingsContainer.CreateInstance<DocumentLinkSettings>(null);
				setttingsTemplate.IsArchiveResource = checkEditIsArchiveResource.Checked;
				if (setttingsTemplate.IsArchiveResource)
				{
					setttingsTemplate.GeneratePreviewImages = false;
					setttingsTemplate.GenerateContentText = false;
					setttingsTemplate.ForcePreview = true;
				}
				else
				{
					setttingsTemplate.GeneratePreviewImages = checkEditDoNotGeneratePreview.CheckState == CheckState.Unchecked;
					setttingsTemplate.GenerateContentText = checkEditDoNotGenerateText.CheckState == CheckState.Unchecked;
					setttingsTemplate.ForcePreview = checkEditForcePreview.CheckState != CheckState.Unchecked;
				}
				_linksGroup.LinkGroupSettingsContainer.SaveSettingsTemplate(LinkSettingsGroupType.AdminSettings, _defaultLinkType, setttingsTemplate);
			}
		}

		private void ckIsArchiveResource_CheckedChanged(object sender, EventArgs e)
		{
			checkEditDoNotGeneratePreview.Enabled =
				checkEditDoNotGenerateText.Enabled =
					checkEditForcePreview.Enabled = checkEditIsArchiveResource.CheckState != CheckState.Checked;
			if (checkEditIsArchiveResource.CheckState == CheckState.Checked)
			{
				checkEditDoNotGeneratePreview.Checked =
				checkEditDoNotGenerateText.Checked =
					checkEditForcePreview.Checked = true;
			}
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _sourceLinks.Count == 1 ? _sourceLinks.First().NameWithExtension : "links", Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				foreach (var link in _sourceLinks)
				{
					link.ClearPreviewContainer();
					var previewContainer = link.GetPreviewContainer();
					var previewGenerator = previewContainer.GetPreviewGenerator();
					try
					{
						previewContainer.UpdateContent(previewGenerator, cancelationToken);
					}
					catch { }
				}
			});
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1} now updated for the server!", _sourceLinks.Count == 1 ? _sourceLinks.First().NameWithExtension : "Links", Environment.NewLine));
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_sourceLinks.First().PreviewContainerPath);
			}
			catch { }
		}
	}
}
