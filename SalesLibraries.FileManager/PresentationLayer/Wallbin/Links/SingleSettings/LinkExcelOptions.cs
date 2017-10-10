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
	[IntendForClass(typeof(ExcelLink))]
	//public partial class LinkExcelOptions : UserControl, ILinkSetSettingsEditControl
	public sealed partial class LinkExcelOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly List<ExcelLink> _sourceLinks = new List<ExcelLink>();
		private readonly ILinksGroup _linksGroup;
		private readonly LinkType? _defaultLinkType;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Excel Settings</size>" };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkExcelOptions()
		{
			InitializeComponent();
			Text = "Admin";

			layoutControlItemRefreshPreview.MinSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshPreview.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public LinkExcelOptions(LinkType? defaultLinkType = null) : this() { }

		public LinkExcelOptions(ILinksGroup linksGroup, LinkType? defaultLinkType = null) : this()
		{
			_linksGroup = linksGroup;
			_defaultLinkType = defaultLinkType;
		}

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_sourceLinks.Clear();
			_sourceLinks.Add((ExcelLink)sourceLink);

			LoadData();
		}

		public void LoadData(IEnumerable<BaseLibraryLink> sourceLinks)
		{
			_sourceLinks.Clear();
			_sourceLinks.AddRange(sourceLinks.OfType<ExcelLink>());

			LoadData();
		}

		private void LoadData()
		{
			layoutControlItemSaveAsTemplate.Visibility = _linksGroup != null ? LayoutVisibility.Always : LayoutVisibility.Never;

			var settingsTemplate = _linksGroup?.LinkGroupSettingsContainer.GetSettingsTemplate<ExcelLinkSettings>(
					LinkSettingsGroupType.AdminSettings, _defaultLinkType);
			if (settingsTemplate != null)
			{
				checkEditIsArchiveResource.Checked = settingsTemplate.IsArchiveResource;
				checkEditDoNotGenerateText.Checked = !settingsTemplate.GenerateContentText;
				checkEditForceDownload.Checked = settingsTemplate.ForceDownload;
				checkEditForceOpen.Checked = settingsTemplate.ForceOpen;
				checkEditSaveAsTemplate.Checked = true;
			}
			else if (_sourceLinks.Any())
			{
				var linkSettings = _sourceLinks.Select(link => link.Settings).OfType<ExcelLinkSettings>().ToList();

				if (linkSettings.All(settings => settings.IsArchiveResource))
					checkEditIsArchiveResource.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.IsArchiveResource))
					checkEditIsArchiveResource.CheckState = CheckState.Indeterminate;
				else
					checkEditIsArchiveResource.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => !settings.GenerateContentText))
					checkEditDoNotGenerateText.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => !settings.GenerateContentText))
					checkEditDoNotGenerateText.CheckState = CheckState.Indeterminate;
				else
					checkEditDoNotGenerateText.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => settings.ForceDownload))
					checkEditForceDownload.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.ForceDownload))
					checkEditForceDownload.CheckState = CheckState.Indeterminate;
				else
					checkEditForceDownload.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => settings.ForceDownload))
					checkEditForceDownload.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.ForceDownload))
					checkEditForceDownload.CheckState = CheckState.Indeterminate;
				else
					checkEditForceDownload.CheckState = CheckState.Unchecked;

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
				((ExcelLinkSettings)link.Settings).IsArchiveResource = checkEditIsArchiveResource.CheckState != CheckState.Indeterminate ?
					checkEditIsArchiveResource.Checked :
					((ExcelLinkSettings)link.Settings).IsArchiveResource;
				if (((ExcelLinkSettings)link.Settings).IsArchiveResource)
				{
					((ExcelLinkSettings)link.Settings).GenerateContentText = false;
					((ExcelLinkSettings)link.Settings).ForceDownload = true;
				}
				else
				{
					((ExcelLinkSettings)link.Settings).GenerateContentText = checkEditDoNotGenerateText.CheckState != CheckState.Indeterminate ?
						!checkEditDoNotGenerateText.Checked :
						((ExcelLinkSettings)link.Settings).GenerateContentText;
					((ExcelLinkSettings)link.Settings).ForceDownload = checkEditForceDownload.CheckState != CheckState.Indeterminate ?
						checkEditForceDownload.Checked :
						((ExcelLinkSettings)link.Settings).ForceDownload;
				}
				((ExcelLinkSettings)link.Settings).ForceOpen = checkEditForceOpen.CheckState != CheckState.Indeterminate ?
					checkEditForceOpen.Checked :
					((ExcelLinkSettings)link.Settings).ForceOpen;

			}

			if (checkEditSaveAsTemplate.Checked)
			{
				var setttingsTemplate = SettingsContainer.CreateInstance<ExcelLinkSettings>(null);
				setttingsTemplate.IsArchiveResource = checkEditIsArchiveResource.Checked;
				if (setttingsTemplate.IsArchiveResource)
				{
					setttingsTemplate.GenerateContentText = false;
					setttingsTemplate.ForceDownload = true;
				}
				else
				{
					setttingsTemplate.GenerateContentText = checkEditDoNotGenerateText.CheckState == CheckState.Unchecked;
					setttingsTemplate.ForceDownload = checkEditForceDownload.CheckState != CheckState.Unchecked;
				}
				setttingsTemplate.ForceOpen = checkEditForceOpen.CheckState != CheckState.Unchecked;
				_linksGroup.LinkGroupSettingsContainer.SaveSettingsTemplate(LinkSettingsGroupType.AdminSettings, _defaultLinkType, setttingsTemplate);
			}
		}

		private void ckIsArchiveResource_CheckedChanged(object sender, EventArgs e)
		{
			checkEditDoNotGenerateText.Enabled =
				checkEditForceDownload.Enabled = checkEditIsArchiveResource.CheckState != CheckState.Checked;
			if (checkEditIsArchiveResource.CheckState == CheckState.Checked)
			{
				checkEditDoNotGenerateText.Checked =
					checkEditForceDownload.Checked = true;
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