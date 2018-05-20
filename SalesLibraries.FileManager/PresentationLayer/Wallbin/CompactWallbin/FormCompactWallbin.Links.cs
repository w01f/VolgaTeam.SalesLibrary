using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is LibraryObjectLink sourceLink)) return;
			Utils.OpenFile(sourceLink.OpenPaths);
		}

		private void OpenLinkLocation(TreeListNode targetLinkNode)
		{
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is LibraryFileLink sourceLink)) return;
			Utils.OpenFile(sourceLink.LocationPath);
		}

		public void OpenLinkOnSite(TreeListNode targetLinkNode)
		{
			var sourceLink = (targetLinkNode?.Tag as WallbinItem)?.Source as LibraryObjectLink;
			var linkId = sourceLink?.ExtId.ToString();
			if (String.IsNullOrEmpty(linkId)) return;

			var linkUrl = String.Format("{0}/preview/getSingleInternalLink?linkId={1}",
				MainController.Instance.Settings.WebServiceSite, linkId);

			Process.Start(linkUrl);
		}

		public void OpenLinkOnOneDrive(TreeListNode targetLinkNode)
		{
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is LibraryFileLink sourceLink) || String.IsNullOrEmpty(sourceLink.OneDriveSettings.Url))
				return;
			Process.Start(sourceLink.OneDriveSettings.Url);
		}

		public void DeleteSingleLink(TreeListNode targetLinkNode)
		{
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is BaseLibraryLink sourceLink)) return;
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
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is IPreviewableLink sourceLink)) return;
			RefreshPreviewFiles(new[] { sourceLink });
		}

		private void RefreshPreviewFiles(IList<IPreviewableLink> links)
		{
			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				if (MainController.Instance.Settings.EnableLocalSync)
				{
					var powerPointLinks = links.OfType<PowerPointLink>().ToList();
					if (powerPointLinks.Any())
					{
						using (var powerPointProcessor = new PowerPointHidden())
						{
							if (!powerPointProcessor.Connect(true)) return;
							foreach (var powerPointLink in powerPointLinks)
							{
								((PowerPointLinkSettings) powerPointLink.Settings).ClearQuickViewContent();
								((PowerPointLinkSettings) powerPointLink.Settings).UpdateQuickViewContent(powerPointProcessor);
								((PowerPointLinkSettings) powerPointLink.Settings).UpdatePresentationInfo(powerPointProcessor);
							}
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

				var thubnailHolders = links.OfType<IThumbnailSettingsHolder>().ToList();
				foreach (var thumbnailSettingsHolder in thubnailHolders.OfType<BaseLibraryLink>().ToList())
				{
					thumbnailSettingsHolder.Thumbnail = null;
					thumbnailSettingsHolder.ThumbnailEncoded = null;
				}
			});
		}
		
		private void EditSingleLinkSettings(TreeListNode targetLinkNode, LinkSettingsType settingsType, LinkType? defaultLinkType = null)
		{
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is BaseLibraryLink sourceLink)) return;
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
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is BaseLibraryLink sourceLink)) return;
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
			if (!((targetLinkNode?.Tag as WallbinItem)?.Source is BaseLibraryLink sourceLink)) return;

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
