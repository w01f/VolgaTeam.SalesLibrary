﻿using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Models.ExternalShortcuts;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(InternalShortcutLink))]
	//public sealed partial class LinkInternalShortcutOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalShortcutOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private InternalShortcutLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalShortcutOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkInternalShortcutOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (InternalShortcutLink)sourceLink;

			if (!MainController.Instance.Lists.ExternalShortcuts.IsLoaded)
			{
				MainController.Instance.ProcessManager.Run(
					"Loading Site Links...",
					(cancelationToken, formProgess) =>
					{
						MainController.Instance.Lists.ExternalShortcuts.Load();
					});
			}

			comboBoxEditShortcutGroup.Properties.Items.Clear();
			comboBoxEditShortcutGroup.Properties.Items.AddRange(MainController.Instance.Lists.ExternalShortcuts.ShortcutLinks
				.OrderBy(s => s.GroupOrder)
				.Select(s => s.GroupFolder)
				.Distinct()
				.ToArray());


			textEditName.EditValue = _data.Name;

			var shortcut = MainController.Instance.Lists.ExternalShortcuts.ShortcutLinks
				.FirstOrDefault(link => String.Compare(
					link.Id,
					((InternalShortcutLinkSettings)_data.Settings).ShortcutId,
					StringComparison.OrdinalIgnoreCase)
				== 0);
			if (shortcut != null)
			{
				comboBoxEditShortcutGroup.EditValue = shortcut.GroupFolder;
				comboBoxEditShortcutLink.EditValue = shortcut;
			}
			checkEditOpenOnSamePage.Checked = !((InternalShortcutLinkSettings)_data.Settings).OpenOnSamePage;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalShortcutLinkSettings)_data.Settings).ShortcutId = (comboBoxEditShortcutLink.EditValue as ShortcutLink)?.Id;
			((InternalShortcutLinkSettings)_data.Settings).OpenOnSamePage = !checkEditOpenOnSamePage.Checked;
		}

		private void OnShortcutGroupChanged(object sender, EventArgs e)
		{
			comboBoxEditShortcutLink.Properties.Items.Clear();
			comboBoxEditShortcutLink.EditValue = null;
			var groupFolder = comboBoxEditShortcutGroup.EditValue as String;
			var shortcutLinks = MainController.Instance.Lists.ExternalShortcuts.ShortcutLinks
				.Where(link => String.Compare(link.GroupFolder, groupFolder, StringComparison.OrdinalIgnoreCase) == 0)
				.OrderBy(link => link.Order)
				.ToArray();
			comboBoxEditShortcutLink.Properties.Items.AddRange(shortcutLinks);
		}

		private void OnShortcutLinkChanged(object sender, EventArgs e)
		{
			var shortcutLink = comboBoxEditShortcutLink.EditValue as ShortcutLink;
			if (shortcutLink != null)
				labelControlShortcutDescription.Text = String.Format("<color=gray>Static ID: {0}        {1}</color>",
					shortcutLink.Id,
					!String.IsNullOrEmpty(shortcutLink.Title) ? String.Format("Title: {0}", shortcutLink.Title) : String.Empty
				);
			else
				labelControlShortcutDescription.Text = String.Empty;
		}
	}
}
