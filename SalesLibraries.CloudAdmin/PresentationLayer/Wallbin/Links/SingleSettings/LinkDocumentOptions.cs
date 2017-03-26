using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(DocumentLink))]
	//public partial class LinkDocumentOptions : UserControl, ILinkSetSettingsEditControl
	public sealed partial class LinkDocumentOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly List<DocumentLink> _sourceLinks = new List<DocumentLink>();
		private readonly ILinksGroup _linksGroup;
		private readonly FileTypes? _defaultLinkType;

		private DocumentLink DefaultLink => _sourceLinks.First();

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = String.Format("<size=+4>{0} Settings</size>", _defaultLinkType == FileTypes.Pdf ? "PDF" : "Document") };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkDocumentOptions()
		{
			InitializeComponent();
			Text = "Admin";
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;

				ckIsArchiveResource.Font = new Font(ckIsArchiveResource.Font.FontFamily, ckIsArchiveResource.Font.Size - 2, ckIsArchiveResource.Font.Style);
				ckForcePreview.Font = new Font(ckForcePreview.Font.FontFamily, ckForcePreview.Font.Size - 2, ckForcePreview.Font.Style);
				ckDoNotGeneratePreview.Font = new Font(ckDoNotGeneratePreview.Font.FontFamily, ckDoNotGeneratePreview.Font.Size - 2, ckDoNotGeneratePreview.Font.Style);
				ckDoNotGenerateText.Font = new Font(ckDoNotGenerateText.Font.FontFamily, ckDoNotGenerateText.Font.Size - 2, ckDoNotGenerateText.Font.Style);
				ckSaveAsTemplate.Font = new Font(ckSaveAsTemplate.Font.FontFamily, ckSaveAsTemplate.Font.Size - 2, ckSaveAsTemplate.Font.Style);

				buttonXOpenWV.Font = new Font(buttonXOpenWV.Font.FontFamily, buttonXOpenWV.Font.Size - 2, buttonXOpenWV.Font.Style);
				buttonXRefreshPreview.Font = new Font(buttonXRefreshPreview.Font.FontFamily, buttonXRefreshPreview.Font.Size - 2, buttonXRefreshPreview.Font.Style);
			}
		}

		public LinkDocumentOptions(ILinksGroup linksGroup, FileTypes? defaultLinkType = null) : this()
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
			ckSaveAsTemplate.Visible = _linksGroup != null;

			var settingsTemplate = _linksGroup?.LinkGroupSettingsContainer.GetSettingsTemplate<DocumentLinkSettings>(
					LinkSettingsGroupType.AdminSettings, _defaultLinkType);
			if (settingsTemplate != null)
			{
				ckIsArchiveResource.Checked = settingsTemplate.IsArchiveResource;
				ckDoNotGeneratePreview.Checked = !settingsTemplate.GeneratePreviewImages;
				ckDoNotGenerateText.Checked = !settingsTemplate.GenerateContentText;
				ckForcePreview.Checked = settingsTemplate.ForcePreview;
				ckSaveAsTemplate.Checked = true;
			}
			else if (_sourceLinks.Any())
			{
				var linkSettings = _sourceLinks.Select(link => link.Settings).OfType<DocumentLinkSettings>().ToList();

				if (linkSettings.All(settings => settings.IsArchiveResource))
					ckIsArchiveResource.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.IsArchiveResource))
					ckIsArchiveResource.CheckState = CheckState.Indeterminate;
				else
					ckIsArchiveResource.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => !settings.GeneratePreviewImages))
					ckDoNotGeneratePreview.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => !settings.GeneratePreviewImages))
					ckDoNotGeneratePreview.CheckState = CheckState.Indeterminate;
				else
					ckDoNotGeneratePreview.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => !settings.GenerateContentText))
					ckDoNotGenerateText.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => !settings.GenerateContentText))
					ckDoNotGenerateText.CheckState = CheckState.Indeterminate;
				else
					ckDoNotGenerateText.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => settings.ForcePreview))
					ckForcePreview.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.ForcePreview))
					ckForcePreview.CheckState = CheckState.Indeterminate;
				else
					ckForcePreview.CheckState = CheckState.Unchecked;

				ckSaveAsTemplate.Checked = false;
			}

			if (_sourceLinks.Count == 1 && Directory.Exists(_sourceLinks.First().PreviewContainerPath))
			{
				buttonXOpenWV.Enabled = true;
				buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _sourceLinks.First().PreviewContainerName);
			}
			else
				buttonXOpenWV.Enabled = false;

			buttonXRefreshPreview.Enabled = _sourceLinks.Any();
		}

		public void SaveData()
		{
			if (!_sourceLinks.Any()) return;
			foreach (var link in _sourceLinks)
			{
				((DocumentLinkSettings)link.Settings).IsArchiveResource = ckIsArchiveResource.CheckState != CheckState.Indeterminate ?
					ckIsArchiveResource.Checked :
					((DocumentLinkSettings)link.Settings).IsArchiveResource;
				if (((DocumentLinkSettings)link.Settings).IsArchiveResource)
				{
					((DocumentLinkSettings)link.Settings).GeneratePreviewImages = false;
					((DocumentLinkSettings)link.Settings).GenerateContentText = false;
					((DocumentLinkSettings)link.Settings).ForcePreview = true;
				}
				else
				{
					((DocumentLinkSettings)link.Settings).GeneratePreviewImages = ckDoNotGeneratePreview.CheckState != CheckState.Indeterminate ?
						!ckDoNotGeneratePreview.Checked :
						((DocumentLinkSettings)link.Settings).GeneratePreviewImages;
					((DocumentLinkSettings)link.Settings).GenerateContentText = ckDoNotGenerateText.CheckState != CheckState.Indeterminate ?
						!ckDoNotGenerateText.Checked :
						((DocumentLinkSettings)link.Settings).GenerateContentText;
					((DocumentLinkSettings)link.Settings).ForcePreview = ckForcePreview.CheckState != CheckState.Indeterminate ?
						ckForcePreview.Checked :
						((DocumentLinkSettings)link.Settings).ForcePreview;
				}
			}

			if (ckSaveAsTemplate.Checked)
			{
				var setttingsTemplate = SettingsContainer.CreateInstance<DocumentLinkSettings>(null);
				setttingsTemplate.IsArchiveResource = ckIsArchiveResource.Checked;
				if (setttingsTemplate.IsArchiveResource)
				{
					setttingsTemplate.GeneratePreviewImages = false;
					setttingsTemplate.GenerateContentText = false;
					setttingsTemplate.ForcePreview = true;
				}
				else
				{
					setttingsTemplate.GeneratePreviewImages = ckDoNotGeneratePreview.CheckState == CheckState.Unchecked;
					setttingsTemplate.GenerateContentText = ckDoNotGenerateText.CheckState == CheckState.Unchecked;
					setttingsTemplate.ForcePreview = ckForcePreview.CheckState != CheckState.Unchecked;
				}
				_linksGroup.LinkGroupSettingsContainer.SaveSettingsTemplate(LinkSettingsGroupType.AdminSettings, _defaultLinkType, setttingsTemplate);
			}
		}

		private void ckIsArchiveResource_CheckedChanged(object sender, EventArgs e)
		{
			ckDoNotGeneratePreview.Enabled =
				ckDoNotGenerateText.Enabled =
					ckForcePreview.Enabled = ckIsArchiveResource.CheckState != CheckState.Checked;
			if (ckIsArchiveResource.CheckState == CheckState.Checked)
			{
				ckDoNotGeneratePreview.Checked =
				ckDoNotGenerateText.Checked =
					ckForcePreview.Checked = true;
			}
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _sourceLinks.Count == 1 ? _sourceLinks.First().NameWithExtension : "links", Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				foreach (var link in _sourceLinks)
				{
					//link.ClearPreviewContainer();
					//var previewContainer = link.GetPreviewContainer();
					//var previewGenerator = previewContainer.GetPreviewGenerator();
					//previewContainer.UpdateContent(previewGenerator, cancelationToken);
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
