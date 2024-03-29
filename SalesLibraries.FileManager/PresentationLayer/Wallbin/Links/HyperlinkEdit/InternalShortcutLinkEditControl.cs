﻿using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Business.Models.ExternalShortcuts;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalShortcutLinkEditControl : UserControl, IInternalLinkEditControl
	{
		private bool IsListsLoaded { get; set; }

		public InternalShortcutLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void InitControl()
		{
			if (IsListsLoaded) return;
			if (!MainController.Instance.Lists.ExternalShortcuts.IsLoaded)
			{
				MainController.Instance.ProcessManager.Run(
					"Loading Shortcut Links...",
					(cancelationToken, formProgess) =>
					{
						MainController.Instance.Lists.ExternalShortcuts.Load();
					});
				IsListsLoaded = true;
			}

			comboBoxEditShortcutGroup.Properties.Items.Clear();
			comboBoxEditShortcutGroup.Properties.Items.AddRange(MainController.Instance.Lists.ExternalShortcuts.ShortcutLinks
				.OrderBy(s => s.GroupOrder)
				.Select(s => s.GroupFolder)
				.Distinct()
				.ToArray());
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalShortcutLinkInfo)PrepareHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.ShortcutId))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the shortcut id before saving");
				return false;
			}
			return true;
		}

		public InternalLinkInfo PrepareHyperLinkInfo()
		{
			return new InternalShortcutLinkInfo
			{
				ShortcutId = (comboBoxEditShortcutLink.EditValue as ShortcutLink)?.Id,
				OpenOnSamePage = !checkEditOpenOnSamePage.Checked
			};
		}

		public InternalLinkInfo GetFinalHyperLinkInfo()
		{
			return PrepareHyperLinkInfo();
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo)
		{
			if (templateInfo is InternalWallbinLinkInfo)
			{
			}
			if (templateInfo is InternalLibraryPageLinkInfo)
			{
			}
			if (templateInfo is InternalLibraryFolderLinkInfo)
			{
			}
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
