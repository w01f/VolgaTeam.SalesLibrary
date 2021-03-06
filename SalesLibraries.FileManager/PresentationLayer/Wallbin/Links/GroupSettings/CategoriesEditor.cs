﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public sealed partial class CategoriesEditor : UserControl, IGroupSettingsEditor
	{
		private readonly List<SearchSuperGroup> _groupTemplates = new List<SearchSuperGroup>();
		private TreeListNode _rootNode;
		private bool _handleCheckEvens;

		private SelectionManager Selection => MainController.Instance.WallbinViews.Selection;

		public CategoriesEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExpand.MaxSize = RectangleHelper.ScaleSize(layoutControlItemExpand.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExpand.MinSize = RectangleHelper.ScaleSize(layoutControlItemExpand.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCollapse.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCollapse.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCollapse.MinSize = RectangleHelper.ScaleSize(layoutControlItemCollapse.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));

			_groupTemplates.AddRange(MainController.Instance.Lists.SearchTags.SearchSuperGroups);

			treeListCategories.AllowCheckMinLevel = 2;
			LoadTreeView();
		}

		#region IGroupSettingsEditor Members
		public string Title => MainController.Instance.Lists.SearchTags.MaxTags > 0 ?
					String.Format("Only {0} Search Tags are ALLOWED", MainController.Instance.Lists.SearchTags.MaxTags) :
					"Manage Search Tags";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			_handleCheckEvens = false;

			treeListCategories.SuspendLayout();

			_rootNode.Nodes.ToList().ForEach(superGroupNode =>
			{
				superGroupNode.Checked = false;
				superGroupNode.Nodes.ToList().ForEach(groupNode => groupNode.Checked = false);
				superGroupNode.Nodes.SelectMany(groupNode => groupNode.Nodes).ToList().ForEach(tagNode => tagNode.Checked = false);
			});

			Enabled = Selection.SelectedObjects.Any();

			var commonCategories = Selection.SelectedObjects.GetCommonCategories();
			foreach (var link in Selection.SelectedObjects)
			{
				foreach (var group in link.Tags.Categories)
				{
					var groupNode = _rootNode.Nodes.SelectMany(sgNode => sgNode.Nodes).FirstOrDefault(node => ((SearchGroup)node.Tag).Equals(group));
					if (groupNode == null) continue;
					var isCommonGroup = commonCategories.Any(commonGroup => commonGroup.Equals(group));
					groupNode.CheckState = isCommonGroup ? CheckState.Checked : CheckState.Indeterminate;

					foreach (var tag in group.Tags)
					{
						var tagNode = groupNode.Nodes.FirstOrDefault(node => ((SearchTag)node.Tag).Equals(tag));
						if (tagNode == null) continue;
						var isCommonTag = commonCategories
							.Where(commonCategory => commonCategory.Equals(group))
							.SelectMany(commonCategory => commonCategory.Tags)
							.Any(commonTag => commonTag.Equals(tag));
						tagNode.CheckState = isCommonTag ? CheckState.Checked : CheckState.Indeterminate;
					}
				}
			}

			_handleCheckEvens = true;

			treeListCategories.ResumeLayout();
		}

		private void ApplyData()
		{
			var sharedGroups = _rootNode.Nodes.SelectMany(superGroupNode => superGroupNode.Nodes)
				.Where(groupNode => groupNode.CheckState == CheckState.Checked)
				.Select(groupNode =>
				{
					var sourceGroup = (SearchGroup)groupNode.Tag;
					var newGroup = new SearchGroup
					{
						Name = sourceGroup.Name,
						SuperGroup = sourceGroup.SuperGroup,
						Description = sourceGroup.Description
					};
					newGroup.Tags.AddRange(groupNode.Nodes
						.Where(tagNode => tagNode.CheckState == CheckState.Checked)
						.Select(tagNode =>
						{
							var sourceTag = (SearchTag)tagNode.Tag;
							var newTag = new SearchTag { Name = sourceTag.Name };
							return newTag;
						}));
					return newGroup;
				})
				.Where(g => g.Tags.Any())
				.ToArray();

			var partialGroups = _rootNode.Nodes.SelectMany(superGroupNode => superGroupNode.Nodes)
				.Where(groupNode => groupNode.CheckState != CheckState.Unchecked)
				.Select(groupNode =>
				{
					var sourceGroup = (SearchGroup)groupNode.Tag;
					var newGroup = new SearchGroup
					{
						Name = sourceGroup.Name,
						SuperGroup = sourceGroup.SuperGroup,
						Description = sourceGroup.Description
					};
					newGroup.Tags.AddRange(groupNode.Nodes
						.Where(tagNode => tagNode.CheckState == CheckState.Indeterminate)
						.Select(tagNode =>
						{
							var sourceTag = (SearchTag)tagNode.Tag;
							var newTag = new SearchTag { Name = sourceTag.Name };
							return newTag;
						}));
					return newGroup;
				})
				.Where(g => g.Tags.Any())
				.ToArray();

			Selection.SelectedObjects.ApplyCategories(sharedGroups, partialGroups);
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL CATEGORY TAGS for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedObjects.ApplyCategories(new SearchGroup[] { });
			EditorChanged?.Invoke(this, new EventArgs());
			UpdateData();
		}
		#endregion

		private void LoadTreeView()
		{
			treeListCategories.Nodes.Clear();

			_rootNode = treeListCategories.AppendNode(new object[] { "Search Engine Tags" }, null);
			_rootNode.StateImageIndex = 0;

			foreach (var searchSuperGroup in _groupTemplates)
			{
				var superGroupNode = treeListCategories.AppendNode(new object[] { searchSuperGroup.Name }, _rootNode);
				superGroupNode.Tag = searchSuperGroup;
				superGroupNode.StateImageIndex = 0;

				foreach (var searchGroup in searchSuperGroup.Groups)
				{
					var groupNode = treeListCategories.AppendNode(new object[] { searchGroup.Name }, superGroupNode);
					groupNode.Tag = searchGroup;
					groupNode.StateImageIndex = 0;

					foreach (var searchTag in searchGroup.Tags)
					{
						var tagNode = treeListCategories.AppendNode(new object[] { searchTag.Name }, groupNode);
						tagNode.Tag = searchTag;
					}
				}
			}
			_rootNode.Expanded = true;
		}

		private void CollapseNode(TreeListNode node)
		{
			if (node.Tag is SearchTag) return;
			foreach (TreeListNode childNode in node.Nodes)
				CollapseNode(childNode);
			if (node.Tag is SearchGroup || node.Tag is SearchSuperGroup)
			{
				node.Expanded = false;
				node.StateImageIndex = 0;
			}
		}

		private void ExpandNode(TreeListNode node)
		{
			node.ExpandAll();
			node.StateImageIndex = 1;
		}

		private void OnResetClick(object sender, EventArgs e)
		{
			ResetData();
		}

		private void OnExpandClick(object sender, EventArgs e)
		{
			ExpandNode(_rootNode);
		}

		private void OnCollapseClick(object sender, EventArgs e)
		{
			CollapseNode(_rootNode);
		}

		private void OnCategoriesBeforeCheckNode(object sender, CheckNodeEventArgs e)
		{
			if (!_handleCheckEvens) return;
			if (e.State != CheckState.Checked) return;

			var curentCheckedTagsCount = _rootNode.Nodes.SelectMany(node => node.Nodes).Count(node => node.CheckState != CheckState.Unchecked);
			var newCheckedTagsCount = curentCheckedTagsCount;
			if (e.Node.Tag is SearchGroup)
				newCheckedTagsCount += e.Node.Nodes.Count;
			else if (e.Node.Tag is SearchTag)
				newCheckedTagsCount++;
			if (newCheckedTagsCount <= MainController.Instance.Lists.SearchTags.MaxTags) return;

			MainController.Instance.PopupMessages.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", MainController.Instance.Lists.SearchTags.MaxTags));

			_handleCheckEvens = false;
			e.State = e.PrevState;
			e.CanCheck = false;
			_handleCheckEvens = true;
		}

		private void OnCategoriesAfterCheckNode(object sender, NodeEventArgs e)
		{
			if (!_handleCheckEvens) return;

			_handleCheckEvens = false;

			if (e.Node.Tag is SearchGroup && e.Node.CheckState != CheckState.Indeterminate)
				foreach (TreeListNode childNode in e.Node.Nodes)
					childNode.CheckState = e.Node.CheckState;
			else if (e.Node.Tag is SearchTag)
			{
				var groupNode = e.Node.ParentNode;
				if (e.Node.CheckState == CheckState.Checked)
					groupNode.CheckState = CheckState.Checked;
				else
				{
					if (groupNode.Nodes.Any(n => n.CheckState == CheckState.Checked))
						groupNode.CheckState = CheckState.Checked;
					else if (groupNode.Nodes.Any(n => n.CheckState == CheckState.Indeterminate))
						groupNode.CheckState = CheckState.Indeterminate;
					else
						groupNode.CheckState = CheckState.Unchecked;
				}
			}

			_handleCheckEvens = true;

			ApplyData();
		}

		private void OnCategoriesAfterCollapse(object sender, NodeEventArgs e)
		{
			e.Node.StateImageIndex = 0;
		}

		private void OnCategoriesAfterExpand(object sender, NodeEventArgs e)
		{
			e.Node.StateImageIndex = 1;
		}

		private void OnCategoriesBeforeCollapse(object sender, BeforeCollapseEventArgs e)
		{
			e.CanCollapse = !(e.Node.Tag is SearchTag);
		}

		private void OnCategoriesNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			if (e.Node.Tag is SearchSuperGroup)
			{
				if (e.Node.Nodes.Any(n => n.CheckState != CheckState.Unchecked))
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
		}
	}
}