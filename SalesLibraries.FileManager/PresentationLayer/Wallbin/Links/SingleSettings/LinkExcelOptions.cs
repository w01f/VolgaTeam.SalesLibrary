using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(ExcelLink))]
	//public partial class LinkExcelOptions : UserControl, ILinkSetSettingsEditControl
	public sealed partial class LinkExcelOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly ILinksGroup _linksGroup;
		private readonly List<ExcelLink> _links = new List<ExcelLink>();
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
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;

				ckIsArchiveResource.Font = new Font(ckIsArchiveResource.Font.FontFamily, ckIsArchiveResource.Font.Size - 2, ckIsArchiveResource.Font.Style);
				ckForceOpen.Font = new Font(ckForceOpen.Font.FontFamily, ckForceOpen.Font.Size - 2, ckForceOpen.Font.Style);
				ckForceDownload.Font = new Font(ckForceDownload.Font.FontFamily, ckForceDownload.Font.Size - 2, ckForceDownload.Font.Style);
				ckDoNotGenerateText.Font = new Font(ckDoNotGenerateText.Font.FontFamily, ckDoNotGenerateText.Font.Size - 2, ckDoNotGenerateText.Font.Style);
				ckSaveAsTemplate.Font = new Font(ckSaveAsTemplate.Font.FontFamily, ckSaveAsTemplate.Font.Size - 2, ckSaveAsTemplate.Font.Style);
			}
		}

		public LinkExcelOptions(ExcelLink link) : this()
		{
			_links.Add(link);
		}

		public LinkExcelOptions(ILinksGroup linksGroup, FileTypes? defaultLinkType = null) : this()
		{
			_linksGroup = linksGroup;
			_links.AddRange(linksGroup.AllLinks.Where(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value).OfType<ExcelLink>());
			_defaultLinkType = defaultLinkType;
		}

		public void LoadData()
		{
			ckSaveAsTemplate.Visible = _linksGroup != null;

			var settingsTemplate = _linksGroup?.LinkGroupSettingsContainer.GetSettingsTemplate<ExcelLinkSettings>(
					LinkSettingsGroupType.AdminSettings, _defaultLinkType);
			if (settingsTemplate != null)
			{
				ckIsArchiveResource.Checked = settingsTemplate.IsArchiveResource;
				ckDoNotGenerateText.Checked = !settingsTemplate.GenerateContentText;
				ckForceDownload.Checked = settingsTemplate.ForceDownload;
				ckForceOpen.Checked = settingsTemplate.ForceOpen;
				ckSaveAsTemplate.Checked = true;
			}
			else if (_links.Any())
			{
				var linkSettings = _links.Select(link => link.Settings).OfType<ExcelLinkSettings>().ToList();

				if (linkSettings.All(settings => settings.IsArchiveResource))
					ckIsArchiveResource.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.IsArchiveResource))
					ckIsArchiveResource.CheckState = CheckState.Indeterminate;
				else
					ckIsArchiveResource.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => !settings.GenerateContentText))
					ckDoNotGenerateText.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => !settings.GenerateContentText))
					ckDoNotGenerateText.CheckState = CheckState.Indeterminate;
				else
					ckDoNotGenerateText.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => settings.ForceDownload))
					ckForceDownload.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.ForceDownload))
					ckForceDownload.CheckState = CheckState.Indeterminate;
				else
					ckForceDownload.CheckState = CheckState.Unchecked;

				if (linkSettings.All(settings => settings.ForceDownload))
					ckForceDownload.CheckState = CheckState.Checked;
				else if (linkSettings.Any(settings => settings.ForceDownload))
					ckForceDownload.CheckState = CheckState.Indeterminate;
				else
					ckForceDownload.CheckState = CheckState.Unchecked;

				ckSaveAsTemplate.Checked = false;
			}
		}

		public void SaveData()
		{
			foreach (var link in _links)
			{
				((ExcelLinkSettings)link.Settings).IsArchiveResource = ckIsArchiveResource.CheckState != CheckState.Indeterminate ?
					ckIsArchiveResource.Checked :
					((ExcelLinkSettings)link.Settings).IsArchiveResource;
				if (((ExcelLinkSettings)link.Settings).IsArchiveResource)
				{
					((ExcelLinkSettings)link.Settings).GenerateContentText = false;
					((ExcelLinkSettings)link.Settings).ForceDownload = true;
				}
				else
				{
					((ExcelLinkSettings)link.Settings).GenerateContentText = ckDoNotGenerateText.CheckState != CheckState.Indeterminate ?
						!ckDoNotGenerateText.Checked :
						((ExcelLinkSettings)link.Settings).GenerateContentText;
					((ExcelLinkSettings)link.Settings).ForceDownload = ckForceDownload.CheckState != CheckState.Indeterminate ?
						ckForceDownload.Checked :
						((ExcelLinkSettings)link.Settings).ForceDownload;
				}
				((ExcelLinkSettings)link.Settings).ForceOpen = ckForceOpen.CheckState != CheckState.Indeterminate ?
					ckForceOpen.Checked :
					((ExcelLinkSettings)link.Settings).ForceOpen;

			}

			if (ckSaveAsTemplate.Checked)
			{
				var setttingsTemplate = SettingsContainer.CreateInstance<ExcelLinkSettings>(null);
				setttingsTemplate.IsArchiveResource = ckIsArchiveResource.Checked;
				if (setttingsTemplate.IsArchiveResource)
				{
					setttingsTemplate.GenerateContentText = false;
					setttingsTemplate.ForceDownload = true;
				}
				else
				{
					setttingsTemplate.GenerateContentText = ckDoNotGenerateText.CheckState == CheckState.Unchecked;
					setttingsTemplate.ForceDownload = ckForceDownload.CheckState != CheckState.Unchecked;
				}
				setttingsTemplate.ForceOpen = ckForceOpen.CheckState != CheckState.Unchecked;
				_linksGroup.LinkGroupSettingsContainer.SaveSettingsTemplate(LinkSettingsGroupType.AdminSettings, _defaultLinkType, setttingsTemplate);
			}
		}

		private void ckIsArchiveResource_CheckedChanged(object sender, EventArgs e)
		{
				ckDoNotGenerateText.Enabled =
					ckForceDownload.Enabled = ckIsArchiveResource.CheckState != CheckState.Checked;
			if (ckIsArchiveResource.CheckState == CheckState.Checked)
			{
				ckDoNotGenerateText.Checked =
					ckForceDownload.Checked = true;
			}
		}
	}
}