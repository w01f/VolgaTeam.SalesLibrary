using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin
{
	public partial class FormCompactWallbin
	{
		private static readonly LinkSettingsType[] LinkGroupSettings = {
			LinkSettingsType.Tags,
			LinkSettingsType.AdminSettings,
		};

		private void OpenLink(TreeListNode targetLinkNode)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as LibraryObjectLink;
			if (sourceLink == null) return;
			Utils.OpenFile(sourceLink.OpenPaths);
		}

		private void OpenLinkLocation(TreeListNode targetLinkNode)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as LibraryFileLink;
			if (sourceLink == null) return;
			Utils.OpenFile(sourceLink.LocationPath);
		}

		public void DeleteSingleLink(TreeListNode targetLinkNode)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as BaseLibraryLink;
			if (sourceLink == null) return;
			var parentFolderNode = targetLinkNode.ParentNode;
			var relatedLinks = (sourceLink as LibraryFileLink)?.GetRelatedLinks();
			if (relatedLinks != null && relatedLinks.Any())
			{
				using (var form = new FormDeleteLink())
				{
					var result = form.ShowDialog(MainController.Instance.MainForm);
					switch (result)
					{
						case DialogResult.OK:
							parentFolderNode.Nodes.Remove(targetLinkNode);
							sourceLink.DeleteLink();
							break;
						case DialogResult.Yes:
							parentFolderNode.Nodes.Remove(targetLinkNode);
							DataStateObserver.Instance.RaiseLinksDeleted(relatedLinks.Select(l => l.ExtId));
							(sourceLink as LibraryFileLink)?.DeleteLinkAndRelatedLinks();
							break;
						default:
							return;
					}
				}
			}
			else
			{
				if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove this link/line break?") !=
					DialogResult.Yes)
				{
					return;
				}
				parentFolderNode.Nodes.Remove(targetLinkNode);
				sourceLink.DeleteLink();
			}

			parentFolderNode.TreeList.SuspendLayout();
			FillNode(parentFolderNode);
			parentFolderNode.TreeList.ResumeLayout();
			RaiseDataChanged();
		}

		private void RefreshLinkPreviewFiles(TreeListNode targetLinkNode)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as IPreviewableLink;
			if (sourceLink == null) return;
			RefreshPreviewFiles(new[] { sourceLink });
		}

		private void RefreshPreviewFiles(IList<IPreviewableLink> links)
		{
			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				var powerPointLinks = links.OfType<PowerPointLink>().ToList();
				if (powerPointLinks.Any())
				{
					using (var powerPointProcessor = new PowerPointHidden())
					{
						if (!powerPointProcessor.Connect(true)) return;
						foreach (var powerPointLink in powerPointLinks)
						{
							((PowerPointLinkSettings)powerPointLink.Settings).ClearQuickViewContent();
							((PowerPointLinkSettings)powerPointLink.Settings).UpdateQuickViewContent(powerPointProcessor);
							((PowerPointLinkSettings)powerPointLink.Settings).UpdatePresentationInfo(powerPointProcessor);
						}
					}
				}

				foreach (var link in links)
				{
					link.ClearPreviewContainer();
					var previewContainer = link.GetPreviewContainer();
					var previewGenerator = previewContainer.GetPreviewGenerator();
					previewContainer.UpdateContent(previewGenerator, cancelationToken);
				}
			});
		}


		private void EditSingleLinkSettings(TreeListNode targetLinkNode, LinkSettingsType settingsType, FileTypes? defaultLinkType = null)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as BaseLibraryLink;
			if (sourceLink == null) return;
			if ((sourceLink is ILinksGroup && LinkGroupSettings.Contains(settingsType) ?
				SettingsEditorFactory.Run((ILinksGroup)sourceLink, settingsType, defaultLinkType) :
				SettingsEditorFactory.Run(sourceLink, settingsType)) == DialogResult.OK)
			{
				targetLinkNode.TreeList.InvalidateNode(targetLinkNode);
				RaiseDataChanged();
			}
		}

		private void EditImageSettings(TreeListNode targetLinkNode)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as BaseLibraryLink;
			if (sourceLink == null) return;
			if (sourceLink.Widget.Enabled)
				EditSingleLinkSettings(targetLinkNode, LinkSettingsType.Widget);
			else if (sourceLink.Banner.Enable)
				EditSingleLinkSettings(targetLinkNode, LinkSettingsType.Banner);
			else if (sourceLink.Thumbnail.Enable)
				EditSingleLinkSettings(targetLinkNode, LinkSettingsType.Thumbnail);
			else if (sourceLink.Widget.HasAutoWidget)
				EditSingleLinkSettings(targetLinkNode, LinkSettingsType.Widget);
		}

		private void ResetSingleLinkSettings(TreeListNode targetLinkNode)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as BaseLibraryLink;
			if (sourceLink == null) return;

			using (var form = new FormResetLinkSettings(sourceLink))
			{
				if (form.ShowDialog(this) != DialogResult.OK) return;
				var settingsGroupsForReset = form.SettingsGroups;
				using (var confirmation = new FormResetLinkSettingsConfirmation(settingsGroupsForReset))
				{
					if (confirmation.ShowDialog(this) != DialogResult.OK) return;
					sourceLink.ResetToDefault(settingsGroupsForReset);
					targetLinkNode.TreeList.InvalidateNode(targetLinkNode);
					RaiseDataChanged();
				}
			}
		}
	}
}
