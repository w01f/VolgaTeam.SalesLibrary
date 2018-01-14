using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		private readonly Pen _folderBoxDraggedIndicatorPen = new Pen(Color.Black, 8);
		private readonly Pen _rowDraggedIndicatorPen = new Pen(Color.Black, 2);
		private IDataObject _dataDraggedOver;
		private Rectangle _dragStartBox;
		private DataGridView.HitTestInfo _mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		private DataGridView.HitTestInfo _mouseDragOverHitInfo = DataGridView.HitTestInfo.Nowhere;

		protected IDataObject DataDraggedOver
		{
			get { return _dataDraggedOver; }
			set
			{
				_dataDraggedOver = value;
				ResetPadding();
			}
		}

		protected bool IsDataDragged => DataDraggedOver != null;

		protected bool IsSourceLinksDragged => IsDataDragged &&
			DataDraggedOver.GetDataPresent(DataFormats.Serializable, true) &&
			DataDraggedOver.GetData(DataFormats.Serializable, true) is SourceLink[] &&
			((SourceLink[])DataDraggedOver.GetData(DataFormats.Serializable, true)).Any(link => !link.IsExternal);

		protected bool IsLinkRowDragged => IsDataDragged && DataDraggedOver.GetDataPresent(typeof(LinkRow));

		protected bool IsFolderBoxDragged
		{
			get
			{
				var data = IsDataDragged ? DataDraggedOver.GetData(typeof(ClassicFolderBox)) : null;
				return data != null && data != this;
			}
		}

		protected bool IsLinkBundleDragged => IsDataDragged &&
			DataDraggedOver.GetDataPresent(DataFormats.Serializable, true) &&
			EntitySettingsResolver.ExtractObjectTypeFromProxy(DataDraggedOver.GetData(DataFormats.Serializable, true).GetType()) == typeof(LinkBundle);

		protected bool IsNativeFilesDragged =>
			IsDataDragged &&
			((DataDraggedOver.GetDataPresent(DataFormats.FileDrop, true) &&
			DataDraggedOver.GetData(DataFormats.FileDrop, true) is String[]) ||
			(DataDraggedOver.GetDataPresent(DataFormats.Serializable, true) &&
			DataDraggedOver.GetData(DataFormats.Serializable, true) is SourceLink[] &&
			((SourceLink[])DataDraggedOver.GetData(DataFormats.Serializable, true)).Any(link => link.IsExternal)));

		private void ResetDragInfo()
		{
			bool needGridRefersh = IsSourceLinksDragged || IsLinkRowDragged || IsLinkBundleDragged || IsNativeFilesDragged;
			DataDraggedOver = null;
			_mouseDragOverHitInfo = DataGridView.HitTestInfo.Nowhere;
			if (needGridRefersh)
				grFiles.Refresh();
		}

		private void OnDragOver(object sender, DragEventArgs e)
		{
			FolderContainer.ProcessScrollOnDragOver(sender, e);
			if (!FormatState.AllowEdit) return;
			DataDraggedOver = e.Data;
			if (!IsDataDragged) return;
			if (IsSourceLinksDragged)
				e.Effect = DragDropEffects.Copy;
			else if (IsLinkRowDragged)
				e.Effect = DragDropEffects.Move;
			else if (IsFolderBoxDragged)
				e.Effect = DragDropEffects.Move;
			else if (IsLinkBundleDragged)
				e.Effect = DragDropEffects.Copy;
			else if (IsNativeFilesDragged)
				e.Effect = DragDropEffects.Copy;
			else
			{
				e.Effect = DragDropEffects.None;
				ResetDragInfo();
				return;
			}

			var pt = grFiles.PointToClient(new Point(e.X, e.Y));
			var dragOverInfo = grFiles.HitTest(pt.X, pt.Y);
			if (!dragOverInfo.Equals(_mouseDragOverHitInfo))
			{
				_mouseDragOverHitInfo = dragOverInfo;
				grFiles.Refresh();
			}
		}

		private void OnDragLeave(object sender, EventArgs e)
		{
			FolderContainer.ProcessScrollOnDragLeave(sender, e);
			if (!FormatState.AllowEdit) return;
			if (DataDraggedOver == null) return;
			if (IsFolderBoxDragged && ClientRectangle.Contains(PointToClient(MousePosition)))
				return;
			ResetDragInfo();
		}

		private void OnDragDrop(object sender, DragEventArgs e)
		{
			FolderContainer.ProcessScrollOnDragLeave(sender, e);
			if (!FormatState.AllowEdit) return;
			SelectionManager.SelectFolder(this);
			if (IsSourceLinksDragged || IsNativeFilesDragged)
			{
				if (IsSourceLinksDragged)
				{
					var sourceLinks =
						(DataDraggedOver?.GetData(DataFormats.Serializable, true) as SourceLink[] ?? new SourceLink[] { })
						.Where(link => !link.IsExternal)
						.ToList();
					if (sourceLinks.Any())
					{
						var confirmDrop = true;
						if (!sourceLinks.OfType<FolderLink>().Any() && sourceLinks.OfType<FileLink>().Count() == 1 &&
							FileFormatHelper.IsUrlFile(sourceLinks.OfType<FileLink>().Single().Path))
						{
							AddHyperLink(UrlLinkInfo.FromFile(sourceLinks.OfType<FileLink>().Single().Path));
							confirmDrop = false;
						}
						else if (
							sourceLinks.OfType<FolderLink>()
								.Any(folderLink => sourceLinks.OfType<FileLink>().Any(fileLink => fileLink.Path.Contains(folderLink.Path))))
						{
							using (var form = new FormCustomDialog(
								String.Format("{0}{1}",
									"<size=+4>Are you SURE you want to Drag the Folder AND all the Files into this Window?</size><br>",
									"<br>It might look kind of strange to have the entire folder in this window…"
								),
								new[]
								{
									new CustomDialogButtonInfo {Title = "Yep!", DialogResult = DialogResult.OK, Width = 100},
									new CustomDialogButtonInfo {Title = "CANCEL", DialogResult = DialogResult.Cancel, Width = 100}
								}
							))
							{
								form.Width = 500;
								form.Height = 210;
								form.Size = RectangleHelper.ScaleSize(form.Size, Utils.GetScaleFactor(CreateGraphics().DpiX));
								confirmDrop = form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
							}
						}
						if (confirmDrop)
							InsertDataSourceLinks(sourceLinks, _mouseDragOverHitInfo.RowIndex);
					}
				}
				if (IsNativeFilesDragged)
				{
					var sourceLinks = (DataDraggedOver?.GetData(DataFormats.Serializable, true) as SourceLink[] ?? new SourceLink[] { }).ToList();
					if (!sourceLinks.Any())
					{
						var droppedItemsPaths = (e.Data.GetData(DataFormats.FileDrop) as String[] ?? new string[] { }).ToList();
						sourceLinks =
							droppedItemsPaths.Select(itemPath => SourceLink.FromExternalPath(itemPath, DataSource.Page.Library)).ToList();
					}
					var extrernalLinks = sourceLinks.Where(link => link.IsExternal).ToList();
					if (extrernalLinks.Any())
					{
						var confirmDrop = true;
						if (extrernalLinks.Count == 1 && extrernalLinks.All(link =>
								FileFormatHelper.IsJpegFile(link.Path) || FileFormatHelper.IsPngFile(link.Path)))
						{
							var imageFilePath = extrernalLinks.Select(link => link.Path).First();
							using (var form = new FormAddImageRequest())
							{
								form.simpleLabelItemTitle.Text =
									String.Format("<color=gray>Add:<br>{0}</color>", Path.GetFileName(imageFilePath));
								if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
								{
									var addToGallery = false;
									if (form.checkEditGallery.Checked)
									{
										confirmDrop = false;
										addToGallery = true;
									}
									else if (form.checkEditFile.Checked)
									{
										confirmDrop = true;
										addToGallery = true;
									}
									else if (form.checkEditLinebreak.Checked)
									{
										using (var formEditImage = new FormAddImage(imageFilePath))
										{
											if (formEditImage.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
											{
												AddImageAsLineBreak(formEditImage.pictureEditImage.Image, _mouseDragOverHitInfo.RowIndex);
												addToGallery = true;
												confirmDrop = false;
											}
											else
												confirmDrop = false;
										}
									}
									else
										confirmDrop = false;
									if (addToGallery)
									{
										MainController.Instance.Lists.Banners.ImportedImages.AddImage<Banner>(imageFilePath);
										MainController.Instance.Lists.Widgets.ImportedImages.AddImage<Widget>(imageFilePath);
									}
								}
								else
									confirmDrop = false;
							}
						}
						else
						{
							foreach (var folderLink in extrernalLinks.OfType<FolderLink>().ToList())
								using (var form = new FormAddExternalFolder(folderLink))
									confirmDrop = form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
						}
						if (confirmDrop)
							InsertDataSourceLinks(extrernalLinks, _mouseDragOverHitInfo.RowIndex);
					}
				}
			}
			else if (IsLinkBundleDragged)
			{
				var droppedLinkBundle = DataDraggedOver?.GetData(DataFormats.Serializable, true) as LinkBundle;
				AddLinkBundle(droppedLinkBundle, _mouseDragOverHitInfo.RowIndex);
			}
			else if (IsLinkRowDragged)
			{
				if (DataDraggedOver?.GetData(typeof(LinkRow)) is LinkRow droppedRow)
					ProcessLinkRowMoving(droppedRow, _mouseDragOverHitInfo.RowIndex);
			}
			else if (IsFolderBoxDragged)
			{
				if (DataDraggedOver?.GetData(typeof(ClassicFolderBox)) is ClassicFolderBox droppedFolder && droppedFolder != this)
					FolderContainer.ProcessFolderMoving(droppedFolder, DataSource.ColumnOrder, DataSource.RowOrder);
			}
			ResetDragInfo();
		}

		private void OnGridCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			_dragStartBox = new Rectangle(
				new Point(
					e.X - (SystemInformation.DragSize.Width / 2),
					e.Y - (SystemInformation.DragSize.Height / 2)),
				SystemInformation.DragSize);
			_mouseDownHitInfo = grFiles.HitTest(e.X, e.Y);
		}

		private void OnGridCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			_mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		}

		private void OnGridCellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (DataSource.Links.Any())
				Cursor = Cursors.Hand;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left) ||
				Equals(_mouseDownHitInfo, DataGridView.HitTestInfo.Nowhere) ||
				_dragStartBox.Contains(e.X, e.Y))
				return;
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				grFiles.DoDragDrop(selectedLink, DragDropEffects.All);
			_mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		}

		private void OnDragDropHeaderMouseDown(object sender, MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					if (!FormatState.AllowEdit) return;
					_dragStartBox = new Rectangle(
						new Point(
							e.X - (SystemInformation.DragSize.Width / 2),
							e.Y - (SystemInformation.DragSize.Height / 2)),
						SystemInformation.DragSize);
					break;
				case MouseButtons.Right:
					if (FormatState.ShowSecurityTags)
						contextMenuStripSecurity.Show(
							(Control)sender,
							e.Location,
							Screen.AllScreens.Length > 0 && DataSource.ColumnOrder > 1 ?
								ToolStripDropDownDirection.Left :
								ToolStripDropDownDirection.Default);
					else
					{
						_folderClipboardManager.UpdateTargets();
						barButtonItemFolderPropertiesImageSettings.Visibility = DataSource.Widget.Enabled || DataSource.Banner.Enable ?
							BarItemVisibility.Always :
							BarItemVisibility.Never;

						barButtonItemFolderPropertiesMultiLinksSecurity.Visibility = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit
							? BarItemVisibility.Always
							: BarItemVisibility.Never;
						barButtonItemFolderPropertiesMultiLinksTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit && DataSource.AllGroupLinks.OfType<LibraryObjectLink>().Any()
								? BarItemVisibility.Always
								: BarItemVisibility.Never;
						barButtonItemFolderPropertiesMultiLinksExpirationDate.Visibility = DataSource.AllGroupLinks.OfType<LibraryObjectLink>().Any()
								? BarItemVisibility.Always
								: BarItemVisibility.Never;
						barButtonItemFolderPropertiesMultiLinksRefreshPreviewFiles.Visibility = DataSource.AllGroupLinks.OfType<IPreviewableLink>().Any()
								? BarItemVisibility.Always
								: BarItemVisibility.Never;

						LoadFolderLinksContextMenuEditors(DataSource.Links.ToList());
						popupMenuFolderProperties.ShowPopup(Cursor.Position);
					}
					break;
			}
		}

		private void OnDragDropHeaderMouseMove(object sender, MouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			Cursor = Cursors.Default;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left) || _dragStartBox.Contains(e.X, e.Y))
				return;
			grFiles.DoDragDrop(this, DragDropEffects.Move);
		}
	}
}
