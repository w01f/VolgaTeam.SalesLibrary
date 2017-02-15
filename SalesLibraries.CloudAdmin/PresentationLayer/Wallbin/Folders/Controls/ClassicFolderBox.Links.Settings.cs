﻿using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		public void EditSingleLinkSettings(LinkSettingsType settingsType)
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			if (SettingsEditorFactory.Run(selectedRow.Source, settingsType) == DialogResult.OK)
			{
				UpdateContent(true);
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public void EditLinksGroupSettings(ILinksGroup linkGroup, LinkSettingsType settingsType, FileTypes? defaultLinkType = null)
		{
			if (SettingsEditorFactory.Run(linkGroup, settingsType, defaultLinkType) == DialogResult.OK)
			{
				MultiLinksSettingsChanged?.Invoke(this, EventArgs.Empty);
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public void EditAllLinksInFolderSettings(LinkSettingsType settingsType, FileTypes? defaultLinkType = null)
		{
			EditLinksGroupSettings(DataSource, settingsType, defaultLinkType);
		}

		private void EditImageSettings()
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			if (!selectedRow.AllowEditImageSettings) return;
			if (selectedRow.Source.Widget.Enabled)
				EditSingleLinkSettings(LinkSettingsType.Widget);
			else if (selectedRow.Source.Banner.Enable)
				EditSingleLinkSettings(LinkSettingsType.Banner);
			else if (selectedRow.Source.Thumbnail.Enable)
				EditSingleLinkSettings(LinkSettingsType.Thumbnail);
			else if (selectedRow.Source.Widget.HasAutoWidget)
				EditSingleLinkSettings(LinkSettingsType.Widget);
		}

		public void ResetSingleLinkSettings()
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;

			using (var form = new FormResetLinkSettings(selectedRow.Source))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var settingsGroupsForReset = form.SettingsGroups;
				using (var confirmation = new FormResetLinkSettingsConfirmation(settingsGroupsForReset))
				{
					if (confirmation.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					selectedRow.Source.ResetToDefault(settingsGroupsForReset);
					selectedRow.Info.Recalc();
					grFiles.Refresh();
					DataChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public void ResetMultiLinkSettings()
		{
			var targetLinks = SelectionManager.SelectedLinks.ToList();
			using (var form = new FormResetLinkSettings(targetLinks))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var settingsGroupsForReset = form.SettingsGroups;
				using (var confirmation = new FormResetLinkSettingsConfirmation(settingsGroupsForReset))
				{
					if (confirmation.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					targetLinks.ResetToDefault(settingsGroupsForReset);
					MultiLinksSettingsChanged?.Invoke(this, EventArgs.Empty);
					DataChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public void ResetAllLinksInFolderSettings()
		{
			using (var form = new FormResetLinkSettings(DataSource))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var settingsGroupsForReset = form.SettingsGroups;
				using (var confirmation = new FormResetLinkSettingsConfirmation(settingsGroupsForReset))
				{
					if (confirmation.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					DataSource.AllLinks.ResetToDefault(settingsGroupsForReset);
					UpdateContent(true);
					DataChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private void ResetSecurity()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to delete security settings?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplySecurity(new SecuritySettings());
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ResetTags()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to wipe tags?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplyCategories(new SearchGroup[] { });
			DataSource.AllLinks.ApplyKeywords(new SearchTag[] { });
			DataSource.AllLinks.ApplySuperFilters(new string[] { });
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ResetWidgets()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove widgets?") != DialogResult.Yes) return;
			DataSource.AllLinks.ResetWidgets();
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ResetBanners()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove banners?") != DialogResult.Yes) return;
			DataSource.AllLinks.ResetBanners();
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SetLinkTextWordWrap()
		{
			DataSource.AllLinks.SetLinkTextWordWrap();
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}