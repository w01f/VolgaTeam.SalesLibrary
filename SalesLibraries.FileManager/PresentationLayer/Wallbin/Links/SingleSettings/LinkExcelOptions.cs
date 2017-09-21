using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(ExcelLink))]
	//public partial class LinkExcelOptions : UserControl, ILinkSetSettingsEditControl
	public sealed partial class LinkExcelOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly List<ExcelLink> _sourceLinks = new List<ExcelLink>();
		private readonly ILinksGroup _linksGroup;
		private readonly FileTypes? _defaultLinkType;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Excel Settings</size>" };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkExcelOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkExcelOptions(FileTypes? defaultLinkType = null) : this() { }

		public LinkExcelOptions(ILinksGroup linksGroup, FileTypes? defaultLinkType = null) : this()
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
	}
}