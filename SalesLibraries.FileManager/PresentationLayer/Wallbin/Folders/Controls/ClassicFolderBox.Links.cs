﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Business.Synchronization;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		public void AddHyperLink(BaseNetworkLinkInfo initialLinkInfo = null, int position = -1)
		{
			using (var form = new FormAddHyperLink(initialLinkInfo))
			{
				if (form.ShowDialog() != DialogResult.OK) return;

				if (position == -1)
				{
					var selectedLink = SelectedLinkRow;
					if (selectedLink != null)
						position = selectedLink.Index;
				}

				_outsideChangesInProgress = true;

				LibraryObjectLink newLink;
				switch (form.SelectedEditorType)
				{
					case HyperLinkTypeEnum.Url:
						newLink = WebLink.Create(
							(UrlLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.YouTube:
						newLink = YouTubeLink.Create(
							(YouTubeLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.Network:
						newLink = NetworkLink.Create(
							(LanLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.QuickSite:
						newLink = QuickSiteLink.Create(
							(QuickSiteLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.App:
						newLink = AppLink.Create(
							(AppLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.Internal:
						var internalLinkInfo = (InternalLinkInfo)form.SelectedEditor.GetHyperLinkInfo();
						switch (internalLinkInfo.InternalLinkType)
						{
							case InternalLinkType.Wallbin:
								newLink = InternalWallbinLink.Create(
									(InternalWallbinLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.LibraryPage:
								newLink = InternalLibraryPageLink.Create(
									(InternalLibraryPageLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.LibraryFolder:
								newLink = InternalLibraryFolderLink.Create(
									(InternalLibraryFolderLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.LibraryObject:
								newLink = InternalLibraryObjectLink.Create(
									(InternalLibraryObjectLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.Shortcut:
								newLink = InternalShortcutLink.Create(
									(InternalShortcutLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							default:
								throw new ArgumentOutOfRangeException("Link type not found");
						}
						break;
					case HyperLinkTypeEnum.Html5:
						newLink = Html5Link.Create(
							(Html5LinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.Vimeo:
						newLink = VimeoLink.Create(
							(VimeoLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					default:
						throw new ArgumentOutOfRangeException("Link type not found");
				}

				if (position >= 0)
					((List<BaseLibraryLink>)DataSource.Links).InsertItem(newLink, position);
				else
					DataSource.Links.AddItem(newLink);
				var newRow = InsertLinkRow(newLink, position);
				_outsideChangesInProgress = false;

				UpdateGridSize();

				SelectSingleRow(newRow);

				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public void AddLineBreak()
		{
			var position = -1;
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				position = selectedLink.Index;

			_outsideChangesInProgress = true;
			var newLink = LineBreak.Create(DataSource);
			if (position >= 0)
				((List<BaseLibraryLink>)DataSource.Links).InsertItem(newLink, position);
			else
				DataSource.Links.AddItem(newLink);
			var newRow = InsertLinkRow(newLink, position);
			_outsideChangesInProgress = false;

			UpdateGridSize();

			SelectSingleRow(newRow);

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void AddImageAsLineBreak(Image image, int position)
		{
			_outsideChangesInProgress = true;
			var newLink = LineBreak.CreateWithBanner(DataSource, image);
			if (position >= 0)
				((List<BaseLibraryLink>)DataSource.Links).InsertItem(newLink, position);
			else
				DataSource.Links.AddItem(newLink);
			var newRow = InsertLinkRow(newLink, position);
			_outsideChangesInProgress = false;

			UpdateGridSize();

			SelectSingleRow(newRow);

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void AddLinkBundle(LinkBundle bundle, int position = -1)
		{
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				position = selectedLink.Index;

			_outsideChangesInProgress = true;
			var newLink = LinkBundleLink.Create(DataSource, bundle);
			if (position >= 0)
				((List<BaseLibraryLink>)DataSource.Links).InsertItem(newLink, position);
			else
				DataSource.Links.AddItem(newLink);
			var newRow = InsertLinkRow(newLink, position);
			_outsideChangesInProgress = false;

			UpdateGridSize();

			SelectSingleRow(newRow);

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void OpenLink()
		{
			var selectedRow = SelectedLinkRow;
			if (!(selectedRow?.Source is LibraryObjectLink sourceLink))
				return;
			Utils.OpenFile(sourceLink.OpenPaths);
		}

		public void OpenLinkLocation()
		{
			var selectedRow = SelectedLinkRow;
			if (!(selectedRow?.Source is LibraryFileLink sourceLink))
				return;
			Utils.OpenFile(sourceLink.LocationPath);
			MainController.Instance.WallbinViews.ActiveWallbin.DataSourcesControl.ShowFileInTree(sourceLink.FullPath);
		}

		public void OpenLinkOnSite()
		{
			var selectedRow = SelectedLinkRow;
			var linkId = selectedRow?.Source?.ExtId.ToString();
			if (String.IsNullOrEmpty(linkId)) return;

			var linkUrl = String.Format("{0}/preview/getSingleInternalLink?linkId={1}",
				MainController.Instance.Settings.WebServiceSite, linkId);

			Process.Start(linkUrl);
		}

		public void OpenLinkOnOneDrive()
		{
			var selectedRow = SelectedLinkRow;
			if (!(selectedRow?.Source is PreviewableFileLink sourceLink) || String.IsNullOrEmpty(sourceLink.OneDriveSettings.Url))
				return;
			Process.Start(sourceLink.OneDriveSettings.Url);
		}

		public void CopyOneDriveUrl()
		{
			var selectedRow = SelectedLinkRow;
			if (!(selectedRow?.Source is PreviewableFileLink sourceLink) || String.IsNullOrEmpty(sourceLink.OneDriveSettings.Url))
				return;

			var url = sourceLink.OneDriveSettings.Url;

			Task.Run(() =>
			{
				var urlCopied = false;
				var retryCount = 0;
				do
				{
					Invoke(new MethodInvoker(() =>
					{
						try
						{
							System.Windows.Forms.Clipboard.Clear();
							System.Windows.Forms.Clipboard.SetText(url);
							urlCopied = true;
						}
						catch (ExternalException)
						{
							Thread.Sleep(1000);
							System.Windows.Forms.Clipboard.Clear();
						}
					}));
					retryCount++;
				} while (!urlCopied && retryCount < 10);
			});

			MainController.Instance.PopupMessages.ShowInfo("Url successfully copied");
		}

		public void ResetOneDriveUrl()
		{
			var selectedRow = SelectedLinkRow;
			if (!(selectedRow?.Source is PreviewableFileLink sourceLink) || String.IsNullOrEmpty(sourceLink.OneDriveSettings.Url))
				return;
			var successfullResult = false;
			MainController.Instance.ProcessManager.Run("Processing OneDrive links...", (cancelationToken, formProgess) =>
			{
				var links = new[] { sourceLink };
				var oneDriveConnector = new OneDriveConnector();
				AsyncHelper.RunSync(async () =>
				{
					try
					{
						await oneDriveConnector.ProcessLinksResetUrl(links
								.Select(f => f.GetPreviewContainer())
								.OfType<FilePreviewContainer>()
								.ToList()
							, cancelationToken);
						successfullResult = true;
					}
					catch { }

				});
			});

			if (successfullResult)
			{
				MainController.Instance.PopupMessages.ShowInfo("Url successfully updated");
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
			else
				MainController.Instance.PopupMessages.ShowInfo(
					"Url updating failed because of external service error. Try again later");
		}

		public void DeleteSingleLink()
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			grFiles.SuspendLayout();
			var relatedLinks = selectedRow.SourceObject?.GetRelatedLinks();
			if (relatedLinks != null && relatedLinks.Any())
			{
				using (var form = new FormDeleteLink())
				{
					var result = form.ShowDialog(MainController.Instance.MainForm);
					switch (result)
					{
						case DialogResult.OK:
							selectedRow.DeleteWithSourceLink();
							DataStateObserver.Instance.RaiseLinksDeleted(new Guid[] { });
							break;
						case DialogResult.Yes:
							DataStateObserver.Instance.RaiseLinksDeleted(relatedLinks.Select(l => l.ExtId));
							selectedRow.SourceObject?.DeleteLinkAndRelatedLinks();
							break;
						default:
							grFiles.ResumeLayout();
							return;
					}
				}
			}
			else
			{
				if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove this link/line break?") !=
					DialogResult.Yes)
				{
					grFiles.ResumeLayout();
					return;
				}
				selectedRow.DeleteWithSourceLink();
			}
			grFiles.ResumeLayout();
			UpdateGridSize();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void DeleteMultiLinks(IList<BaseLibraryLink> links)
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove links?") != DialogResult.Yes) return;
			DataStateObserver.Instance.RaiseLinksDeleted(links.Select(l => l.ExtId));
			foreach (var link in links)
				link.DeleteLink();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void DeleteFolderLinks()
		{
			DeleteMultiLinks(grFiles.Rows.OfType<LinkRow>().Where(r => r.Source != null).Select(r => r.Source).ToList());
		}

		public void EditLinkCustomThumbnail()
		{
			var selectedRow = SelectedLinkRow;
			if (!(selectedRow?.Source is PreviewableHyperLink sourceLink))
				return;
			var hyperLinkPreviewContainer = sourceLink.HyperlinkPreviewContainer;
			if (hyperLinkPreviewContainer == null)
				return;
			if (!(hyperLinkPreviewContainer.Settings is HyperlinkPreviewContainerSettings hyperLinkPreviewSettings))
				return;
			using (var form = new FormEditLinkCustomThumbnail())
			{
				form.pictureEdit.Image = hyperLinkPreviewSettings.CustomThumbnail;
				if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
				{
					hyperLinkPreviewSettings.CustomThumbnail = form.pictureEdit.Image;
					DataChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public void SelectAll(bool handleSelection = true)
		{
			_outsideChangesInProgress = !handleSelection;
			grFiles.SelectAll();
			_outsideChangesInProgress = false;
		}

		private void InsertDataSourceLinks(IList<SourceLink> sourceLinks, int position = -1)
		{
			if (!sourceLinks.Any()) return;

			_outsideChangesInProgress = true;

			var libraryLinks = new List<BaseLibraryLink>();
			MainController.Instance.ProcessManager.Run(String.Format("Adding Link{0}...", sourceLinks.Count > 1 ? "s" : String.Empty),
				(cancelationToken, formProgess) =>
				{
					var dataRecordPosition = position;
					foreach (var sourceLink in sourceLinks)
					{
						var link = LibraryFileLink.Create(sourceLink, DataSource);
						if (dataRecordPosition >= 0)
						{
							((List<BaseLibraryLink>)DataSource.Links).InsertItem(link, dataRecordPosition);
							dataRecordPosition++;
						}
						else
							DataSource.Links.AddItem(link);
						libraryLinks.Add(link);
					}
				});

			LinkRow row = null;
			var rowItemPosition = position;
			foreach (var link in libraryLinks)
			{
				row = InsertLinkRow(link, rowItemPosition);
				if (rowItemPosition != -1)
					rowItemPosition++;
			}

			_outsideChangesInProgress = false;

			UpdateGridSize();

			SelectSingleRow(row);

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ProcessLinkRowMoving(LinkRow targetRow, int newPosition = -1)
		{
			int positionToInsert;
			if (targetRow.DataGridView == grFiles)
				positionToInsert = newPosition == -1 ?
					grFiles.RowCount - 1 :
					(targetRow.Index < newPosition ? newPosition - 1 : newPosition);
			else
				positionToInsert = newPosition == -1 ? grFiles.RowCount : newPosition;
			var targetLink = targetRow.Source;
			targetRow.DeleteFromFolder();
			targetRow.Dispose();

			grFiles.ClearSelection();
			targetLink.Folder = DataSource;
			((List<BaseLibraryLink>)DataSource.Links).InsertItem(targetLink, positionToInsert);

			var newRow = InsertLinkRow(targetLink, positionToInsert);
			SelectSingleRow(newRow);

			UpdateGridSize();

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ProcessLinkTextEdit(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			if (!FormatState.AllowEdit) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			if (!linkRow.AllowEditLinkText) return;
			using (var form = new FormEditLinkText())
			{
				form.Text = String.Format(form.Text, linkRow.Source is LineBreak ?
					"LINE BREAK" :
					(linkRow.Source as LibraryFileLink)?.NameWithExtension ??
						(linkRow.Source as LibraryObjectLink)?.RelativePath ??
						String.Empty
					);
				form.TextWordWrap = linkRow.Source.Settings.TextWordWrap;
				form.EditedText = linkRow.Source.DisplayNameWithoutNote;
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				linkRow.Source.DisplayNameWithoutNote = form.EditedText;
				linkRow.Source.Settings.TextWordWrap = form.TextWordWrap;
				linkRow.Info.Recalc();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void ProcessLinkOpen(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			if (FormatState.AllowEdit) return;
			OpenLink();
		}

		private void OnLinksDeleted(object sender, DataChangeEventArgs e)
		{
			if (e.ChangeType != DataChangeType.LinksDeleted) return;
			var linksDeletedArgs = (LinksDeletedEventArgs)e;
			grFiles.SuspendLayout();
			var targetRows = grFiles.Rows
				.OfType<LinkRow>()
				.Where(row => linksDeletedArgs.LinkIds.Any(id => id.Equals(row.Source.ExtId)))
				.ToList();
			foreach (var linkRow in targetRows)
				linkRow.RemoveFromGrid();
			grFiles.ResumeLayout();
			UpdateGridSize();
		}

		private void SortLinkByName()
		{
			grFiles.ClearSelection();
			DataSource.SortLinksByDisplayName();
			var rows = grFiles.Rows.OfType<LinkRow>().ToList();
			grFiles.Rows.Clear();
			grFiles.Rows.AddRange(rows.OrderBy(linkRow => linkRow.Source.Order).ToArray());
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void RefreshPreviewFiles(IList<IPreviewableLink> links)
		{
			var thubnailHolders = links.OfType<IThumbnailSettingsHolder>().ToList();
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
								((PowerPointLinkSettings)powerPointLink.Settings).ClearQuickViewContent();
								((PowerPointLinkSettings)powerPointLink.Settings).UpdateQuickViewContent(powerPointProcessor);
								((PowerPointLinkSettings)powerPointLink.Settings).UpdatePresentationInfo(powerPointProcessor);
							}
						}
					}
				}

				foreach (var link in links)
				{
					link.ClearPreviewContainer();
					var previewContainer = link.GetPreviewContainer();
					var previewGenerator = previewContainer.GetPreviewContentGenerator();
					try
					{
						previewContainer.UpdatePreviewContent(previewGenerator, cancelationToken);
					}
					catch { }
				}

				foreach (var thumbnailSettingsHolder in thubnailHolders.OfType<BaseLibraryLink>().ToList())
				{
					thumbnailSettingsHolder.Thumbnail = null;
					thumbnailSettingsHolder.ThumbnailEncoded = null;
				}
			});

			if (thubnailHolders.Any())
			{
				UpdateContent(true);
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void CopyLinks()
		{
			if (!SelectionManager.SelectedLinks.Any()) return;
			MainController.Instance.WallbinViews.LinksClipboard.Copy(SelectionManager.SelectedLinks.Select(link => link.ExtId));
		}

		private void CutLinks()
		{
			if (!SelectionManager.SelectedLinks.Any()) return;
			MainController.Instance.WallbinViews.LinksClipboard.Cut(SelectionManager.SelectedLinks.Select(link => link.ExtId));
		}

		private void PasteLinks()
		{
			var position = -1;
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				position = selectedLink.Index;

			var pastedLinks = MainController.Instance.WallbinViews.LinksClipboard.Paste(DataSource, position);

			_outsideChangesInProgress = true;
			LinkRow row = null;
			foreach (var libraryLink in pastedLinks)
			{
				row = InsertLinkRow(libraryLink, position);
				if (position != -1)
					position++;
			}
			_outsideChangesInProgress = false;

			if (row != null)
			{
				UpdateGridSize();

				SelectSingleRow(row);

				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
