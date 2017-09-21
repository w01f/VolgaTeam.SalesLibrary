using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.CompactWallbin
{
	public partial class FormCompactWallbin
	{
		private const int FolderStateImageHeight = 40;
		private const int LinkStateImageHeight = 32;

		public async void LoadData()
		{
			simpleLabelItemTreeViewProgressLabel.Text = "Loading Library...";
			layoutControlGroupTreeViewProgress.Visibility = LayoutVisibility.Always;
			circularProgressTreeView.IsRunning = true;
			layoutControlGroupTreeView.Enabled = false;
			treeListWallbinItems.SuspendLayout();
			treeListWallbinItems.Nodes.Clear();

			await Task.Run(() => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(() =>
			{
				foreach (var page in ActiveWallbin.DataStorage.Library.Pages.OrderBy(page => page.Order))
				{
					var wallbinItem = new WallbinItem
					{
						Name = page.Name,
						Type = WallbinItemType.Page,
						Source = page
					};
					treeListWallbinItems.AppendNode(new[] { wallbinItem.Name }, null, wallbinItem);
				}
			})));

			treeListWallbinItems.ResumeLayout();
			layoutControlGroupTreeView.Enabled = true;
			circularProgressTreeView.IsRunning = false;
			layoutControlGroupTreeViewProgress.Visibility = LayoutVisibility.Never;

			UpdateSyncInfo();
		}

		private void FillNode(TreeListNode node, bool showSubItems = false)
		{
			var wallbinItem = node.Tag as WallbinItem;
			if (wallbinItem == null) return;
			if (wallbinItem.Type == WallbinItemType.Link) return;
			if (node.Nodes.Count == 0)
			{
				IList<WallbinItem> childItems;
				switch (wallbinItem.Type)
				{
					case WallbinItemType.Page:
						childItems = ((LibraryPage)wallbinItem.Source).Folders
							.OrderBy(folder => folder.RowOrder)
							.ThenBy(folder => folder.CollectionOrder)
							.Select(folder => new WallbinItem
							{
								Type = WallbinItemType.Folder,
								Name = folder.Name,
								Source = folder
							})
							.ToList();
						break;
					case WallbinItemType.Folder:
						childItems = ((LibraryFolder)wallbinItem.Source).Links
							.OrderBy(link => link.Order)
							.Select(link => new WallbinItem
							{
								Type = WallbinItemType.Link,
								Name = link.LinkInfoDisplayName,
								Source = link
							})
							.ToList();
						break;
					default:
						childItems = new List<WallbinItem>();
						break;
				}
				foreach (var childItem in childItems)
				{
					var childNode = node.TreeList.AppendNode(new[] { childItem.Name }, node, childItem);
					if (showSubItems && childItem.Type != WallbinItemType.Link)
						FillNode(childNode, true);
				}
			}
			else
				foreach (TreeListNode childNode in node.Nodes)
					FillNode(childNode, true);
			node.Expanded = true;
		}

		private void OnTreeViewCalcNodeHeight(object sender, CalcNodeHeightEventArgs e)
		{
			var targetWallbinItem = (WallbinItem)e.Node.Tag;
			switch (targetWallbinItem.Type)
			{
				case WallbinItemType.Page:
				case WallbinItemType.Folder:
					e.NodeHeight = FolderStateImageHeight;
					break;
				case WallbinItemType.Link:
					e.NodeHeight = LinkStateImageHeight;
					break;
			}
			if (targetWallbinItem.Type == WallbinItemType.Link)
				e.NodeHeight = LinkStateImageHeight;
		}

		private void OnTreeViewCustomDrawNodeImages(object sender, CustomDrawNodeImagesEventArgs e)
		{
			e.Handled = false;
			var targetWallbinItem = (WallbinItem)e.Node.Tag;
			Image iconImage = null;
			switch (targetWallbinItem.Type)
			{
				case WallbinItemType.Page:
					iconImage = e.Node.Expanded
						? Properties.Resources.CompactWallbinPageOpen
						: Properties.Resources.CompactWallbinPageClosed;
					break;
				case WallbinItemType.Folder:
					iconImage = e.Node.Expanded
						? Properties.Resources.CompactWallbinFolderOpen
						: Properties.Resources.CompactWallbinFolderClosed;
					break;
				case WallbinItemType.Link:
					var baseLibraryLink = (BaseLibraryLink)targetWallbinItem.Source;
					iconImage = baseLibraryLink is LineBreak
						? Properties.Resources.CompactWallbinLineBreak
						: baseLibraryLink.Widget.DisplayedImage;
					break;
			}
			if (iconImage == null) return;
			var imageRect = new Rectangle(
					e.StateRect.X - (iconImage.Width - e.StateRect.Width),
					e.StateRect.Y,
					iconImage.Width,
					e.StateRect.Height
				);
			e.Graphics.DrawImage(iconImage, imageRect);
			e.Handled = true;
		}

		private void OnTreeViewCustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
		{
			var targetWallbinItem = (WallbinItem)e.Node.Tag;
			switch (targetWallbinItem.Type)
			{
				case WallbinItemType.Page:
					e.Appearance.ForeColor = Color.Green;
					break;
				case WallbinItemType.Folder:
					e.Appearance.ForeColor = Color.RoyalBlue;
					break;
			}
			if (e.Node == _dragOverNode)
			{
				var textSize = e.Graphics.MeasureString(
					e.CellText,
					e.Appearance.Font,
					new Size(Int32.MaxValue, Int32.MaxValue));
				e.Graphics.DrawLine(_penBorder, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Left + textSize.Width + 3,
					e.Bounds.Bottom - 1);
			}
		}

		private void OnTreeViewMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Clicks != 2) return;
			var treeList = sender as TreeList;
			if (treeList == null) return;
			var hitPoint = new Point(e.X, e.Y);
			var hitInfo = treeList.CalcHitInfo(hitPoint);
			var wallbinItem = hitInfo.Node?.Tag as WallbinItem;
			if (wallbinItem == null || wallbinItem.Type == WallbinItemType.Link) return;
			if (hitInfo.Node.Nodes.Count == 0)
				FillNode(hitInfo.Node);
		}

		private void OnCollapseClick(object sender, EventArgs e)
		{
			treeListWallbinItems.CollapseAll();
		}

		private void OnExpandClick(object sender, EventArgs e)
		{
			foreach (TreeListNode node in treeListWallbinItems.Nodes)
				FillNode(node, true);
		}

		private void OnLinksDeleted(object sender, DataChangeEventArgs e)
		{
			if (e.ChangeType != DataChangeType.LinksDeleted) return;
			if (MainController.Instance.MainForm.ActiveForm != this) return;
			var linkDeletEventArgs = (LinksDeletedEventArgs)e;
			treeListWallbinItems.SuspendLayout();
			foreach (TreeListNode pageNode in treeListWallbinItems.Nodes)
				foreach (var folderNode in pageNode.Nodes
					.Where(node => node.Nodes.Count > 0 && (((node.Tag as WallbinItem)?.Source as LibraryFolder)?.Links
						.Any(link => linkDeletEventArgs.LinkIds.Contains(link.ExtId)) ?? false)))
					FillNode(folderNode);
			treeListWallbinItems.ResumeLayout();
		}
	}
}
