using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTreeList;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.LinksTreeSelector
{
	public partial class LinksTreeSelectorControl : UserControl
	{
		private bool _dataLoading;

		public List<BaseLibraryLink> SelectedLinks { get; } = new List<BaseLibraryLink>();
		public TreeGroup SelectedGroup => treeList.FocusedNode?.Tag as TreeGroup;

		public event EventHandler<EventArgs> LinkSelected;

		public LinksTreeSelectorControl()
		{
			InitializeComponent();

			layoutControlItemCollapseAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCollapseAll.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCollapseAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemCollapseAll.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExpandAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemExpandAll.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExpandAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemExpandAll.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void LoadData(ILinksGroup linkGroup, LinkType? defaultLinkType = null, IList<LinkType> excludeFileTypes = null)
		{
			_dataLoading = true;

			var rootGroup = new RootTreeGroup(linkGroup, defaultLinkType);
			var linksTreeGroups = new List<LinksFormatTreeGroup>();
			linksTreeGroups.AddRange(LinksFormatTreeGroup.GetDefaultGroups());

			foreach (var libraryLink in linkGroup.AllGroupLinks.Where(link => link != linkGroup && (defaultLinkType == null || link.Type == defaultLinkType.Value) && (excludeFileTypes == null || !excludeFileTypes.Contains(link.Type))).ToList())
			{
				var targetLinkGroup = linksTreeGroups.FirstOrDefault(g => g.TargetLinkFormats.Contains(libraryLink.WebFormat)) ??
					linksTreeGroups.OfType<UndefinedTreeGroup>().First();
				targetLinkGroup.Links.Add(libraryLink);
			}

			treeList.Nodes.Clear();

			var rootNode = treeList.AppendNode(new object[] { rootGroup.TitleAndLinksCount }, null);
			rootNode.Tag = rootGroup;
			rootNode.StateImageIndex = 0;

			var linkGroupsWithLinks = linksTreeGroups.Where(g => g.Links.Any()).ToList();

			layoutControlGroupButtons.Visibility = linkGroupsWithLinks.Count > 1 ? LayoutVisibility.Always : LayoutVisibility.Never;

			if (linkGroupsWithLinks.Count > 1)
			{
				foreach (var formatTreeGroup in linksTreeGroups.Where(g => g.Links.Any()))
				{
					var formatGroupNode = treeList.AppendNode(new object[] { formatTreeGroup.TitleAndLinksCount }, rootNode);
					formatGroupNode.Tag = formatTreeGroup;
					formatGroupNode.StateImageIndex = 0;

					foreach (var libraryLink in formatTreeGroup.Links)
					{
						var linkNode = treeList.AppendNode(new object[] { (libraryLink as LibraryFileLink)?.NameWithExtension ?? libraryLink.LinkInfoDisplayName }, formatGroupNode);
						linkNode.Tag = libraryLink;
						linkNode.StateImageIndex = formatTreeGroup.StateImageIndex;
					}
				}
			}
			else
			{
				var defaultGroup = linkGroupsWithLinks.FirstOrDefault();
				if (defaultGroup != null)
				{
					foreach (var libraryLink in defaultGroup.Links)
					{
						var linkNode = treeList.AppendNode(new object[] { (libraryLink as LibraryFileLink)?.NameWithExtension ?? libraryLink.LinkInfoDisplayName }, rootNode);
						linkNode.Tag = libraryLink;
						linkNode.StateImageIndex = defaultGroup.StateImageIndex;
					}
				}
			}
			rootNode.Expanded = true;
			treeList.SetFocusedNode(rootNode);

			_dataLoading = false;

			OnTreeViewFocusedNodeChanged(treeList, new FocusedNodeChangedEventArgs(null, rootNode));
		}

		private void OnTreeViewAfterCollapse(object sender, NodeEventArgs e)
		{
			e.Node.StateImageIndex = 0;
		}

		private void OnTreeViewAfterExpand(object sender, NodeEventArgs e)
		{
			e.Node.StateImageIndex = 1;
		}

		private void OnTreeViewBeforeCollapse(object sender, BeforeCollapseEventArgs e)
		{
			e.CanCollapse = e.Node.Tag is LinksFormatTreeGroup;
		}

		private void OnTreeViewFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			SelectedLinks.Clear();
			if (e.Node.Tag is TreeGroup)
				SelectedLinks.AddRange(((TreeGroup)e.Node.Tag).Links);
			if (e.Node.Tag is BaseLibraryLink)
				SelectedLinks.Add((BaseLibraryLink)e.Node.Tag);
			if (!_dataLoading)
				LinkSelected?.Invoke(this, EventArgs.Empty);
		}

		private void OnTreeViewNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (e.Node.Focused)
			{
				e.Appearance.ForeColor = Color.Green;
				e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Bold);
			}
			else
			{
				e.Appearance.ForeColor = Color.Black;
				e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Regular);
			}
		}

		private void buttonXExpandAll_Click(object sender, EventArgs e)
		{
			treeList.ExpandAll();
		}

		private void buttonXCollapseAll_Click(object sender, EventArgs e)
		{
			treeList.CollapseAll();
		}
	}
}
