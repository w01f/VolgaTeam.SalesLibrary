﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(BaseLibraryLink))]
	//public partial class ResetSettingsSchedulerOptions : UserControl, ILinkSettingsEditControl
	public partial class ResetSettingsSchedulerOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly BaseLibraryLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 4;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public ResetSettingsSchedulerOptions(BaseLibraryLink data)
		{
			InitializeComponent();
			_data = data;

			Text = "Link RESET Timer";

			dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}
		}

		public void LoadData()
		{
			checkEditEnableResetSettingsScheduler.Checked = _data.ResetSettingsScheduler.Enabled;
			dateEditExpirationDate.EditValue = _data.ResetSettingsScheduler.ResetDate;
			timeEditExpirationTime.EditValue = _data.ResetSettingsScheduler.ResetDate ?? DateTime.MinValue;
			if (_data.ResetSettingsScheduler.Enabled)
			{
				foreach (var settingsGroup in _data.ResetSettingsScheduler.SettingsGroups)
				{
					switch (settingsGroup)
					{
						case LinkSettingsGroupType.Expiration:
							checkEditLinkSettingsGroupExpirationDate.Checked = true;
							break;
						case LinkSettingsGroupType.HoverNote:
							checkEditLinkSettingsGroupHoverNotes.Checked = true;
							break;
						case LinkSettingsGroupType.Keywords:
							checkEditLinkSettingsGroupKeywords.Checked = true;
							break;
						case LinkSettingsGroupType.SearchTags:
							checkEditLinkSettingsGroupSearchTags.Checked = true;
							break;
						case LinkSettingsGroupType.Security:
							checkEditLinkSettingsGroupSecurity.Checked = true;
							break;
						case LinkSettingsGroupType.TextFormatting:
							checkEditLinkSettingsGroupTextFormatting.Checked = true;
							break;
						case LinkSettingsGroupType.TextNote:
							checkEditLinkSettingsGroupTextNotes.Checked = true;
							break;
						case LinkSettingsGroupType.Widgets:
						case LinkSettingsGroupType.Banners:
							checkEditLinkSettingsGroupWidgetsAndBanners.Checked = true;
							break;
					}
				}
			}
		}

		public void SaveData()
		{
			_data.ResetSettingsScheduler.ResetDate = checkEditEnableResetSettingsScheduler.Checked && dateEditExpirationDate != null ?
				new DateTime(dateEditExpirationDate.DateTime.Year, dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour, timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second) :
				(DateTime?)null;
			_data.ResetSettingsScheduler.SettingsGroups.Clear();
			if (_data.ResetSettingsScheduler.ResetDate.HasValue)
			{
				if (checkEditLinkSettingsGroupSecurity.Checked)
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.Security);
				if (checkEditLinkSettingsGroupTextNotes.Checked)
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.TextNote);
				if (checkEditLinkSettingsGroupWidgetsAndBanners.Checked)
				{
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.Widgets);
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.Banners);
				}
				if (checkEditLinkSettingsGroupExpirationDate.Checked)
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.Expiration);
				if (checkEditLinkSettingsGroupHoverNotes.Checked)
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.HoverNote);
				if (checkEditLinkSettingsGroupKeywords.Checked)
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.Keywords);
				if (checkEditLinkSettingsGroupSearchTags.Checked)
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.SearchTags);
				if (checkEditLinkSettingsGroupTextFormatting.Checked)
					_data.ResetSettingsScheduler.SettingsGroups.Add(LinkSettingsGroupType.TextFormatting);
			}
		}

		private void checkBoxEnableResetSettingsScheduler_CheckedChanged(object sender, EventArgs e)
		{
			dateEditExpirationDate.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			timeEditExpirationTime.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupExpirationDate.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupHoverNotes.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupKeywords.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupSearchTags.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupTextFormatting.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupSecurity.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupTextNotes.Enabled = checkEditEnableResetSettingsScheduler.Checked;
			checkEditLinkSettingsGroupWidgetsAndBanners.Enabled = checkEditEnableResetSettingsScheduler.Checked;

			if (checkEditEnableResetSettingsScheduler.Checked)
			{
				dateEditExpirationDate.ForeColor = Color.Black;
				timeEditExpirationTime.ForeColor = Color.Black;
			}
			else
			{
				dateEditExpirationDate.ForeColor = Color.Gray;
				timeEditExpirationTime.ForeColor = Color.Gray;
				dateEditExpirationDate.EditValue = DateTime.MinValue;
				timeEditExpirationTime.Time = DateTime.MinValue;

				checkEditLinkSettingsGroupExpirationDate.Checked = false;
				checkEditLinkSettingsGroupHoverNotes.Checked = false;
				checkEditLinkSettingsGroupKeywords.Checked = false;
				checkEditLinkSettingsGroupSearchTags.Checked = false;
				checkEditLinkSettingsGroupSecurity.Checked = false;
				checkEditLinkSettingsGroupTextFormatting.Checked = false;
				checkEditLinkSettingsGroupTextNotes.Checked = false;
				checkEditLinkSettingsGroupWidgetsAndBanners.Checked = false;
			}
		}
	}
}